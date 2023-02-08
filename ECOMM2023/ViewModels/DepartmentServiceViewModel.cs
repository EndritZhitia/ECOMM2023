using ECOMM2023.Models;

namespace ECOMM2023.ViewModels
{
    public class DepartmentServiceViewModel
    {
        public List<Department> Departments { get; set; }
        public List<Service> Services { get; set; }

        public int? DepartmentId { get; set; }

        public string? DepartmentName { get; set; }
        public string? Name { get; set; }
    }
}
