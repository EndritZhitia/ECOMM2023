using System.ComponentModel.DataAnnotations.Schema;

namespace ECOMM2023.Models
{
    public class Service
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [ForeignKey("DepartmentId")]

        public int ? DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
