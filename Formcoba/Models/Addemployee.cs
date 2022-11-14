using System.ComponentModel.DataAnnotations.Schema;

namespace Form.Models
{
    public class Addemployee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        [NotMapped]
        public IFormFile file { get; set; }

        public string Departement { get; set; }
        public string confirmation { get; set; }

        public string? alasan { get; set; }

    }
}
