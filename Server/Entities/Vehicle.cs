using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities {
    public class Vehicle {
        [Key]
        public int Id { get; set; }
        public string Tuition { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; } = default!;
        public List<Repair> Repairs { get; set; } = new List<Repair>();
    }
}