using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace Form.Models.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }    
        public string Name { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }

        public string file { get; set; }

        public string Departement { get; set; }
        public string confirmation { get; set; }

        public string? alasan { get; set; }  

    }
}
