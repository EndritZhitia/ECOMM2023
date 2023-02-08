using ECOMM2023.Models;
using ECOMM2023.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ECOMM2023.Controllers
{
    [Authorize(Roles = "Admin,Saler")]

    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        [AllowAnonymous]

        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {
            var products = from s in _context.Products
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.ProductName.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }
            int pageSize = 3;
            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        

        /* public async Task<IActionResult> Index()
           {
               return View(await _context.Products.ToListAsync());
           }

           */

        // GET: Products/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        

        public async Task<IActionResult> Create(CreateProductViewModel product)
        {
            if (ModelState.IsValid)
            {
                byte[] imagebytes = null;

                if (product.Image.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await product.Image.CopyToAsync(stream);
                        imagebytes = stream.ToArray();
                    }
                }
                else
                {
                    using (var stream = new MemoryStream())
                    {
                        await product.Image.CopyToAsync(stream);
                        imagebytes = stream.ToArray();
                    }

                }



                Product insertProduct = new Product
                {

                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    Category = product.Category,
                    Producer = product.Producer,
                    Image = imagebytes

                };


                _context.Add(insertProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }





            CreateProductViewModel model = new CreateProductViewModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Category = product.Category,
                Producer = product.Producer,




            };
            return View(model);
        }


        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
    

        public async Task<IActionResult> Edit(int id, CreateProductViewModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    byte[] imagebytes = null;

                    if (product.Image.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await product.Image.CopyToAsync(stream);
                            imagebytes = stream.ToArray();
                        }
                    }

                    var databaseArticle = _context.Products.Where(x => x.Id.Equals(product.Id)).FirstOrDefault();

                    databaseArticle.ProductName = product.ProductName;
                    databaseArticle.Description = product.Description;
                    databaseArticle.Price = product.Price;
                    databaseArticle.Category = product.Category;
                    databaseArticle.Producer = product.Producer;
                    databaseArticle.Image = imagebytes;




                    _context.Update(databaseArticle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}