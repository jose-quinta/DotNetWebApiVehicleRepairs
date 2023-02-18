using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities {
    public class Repair {
        [Key]
        public int Id { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now;
        public int Km { get; set; } = 0;
        public string Fault { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; } = DateTime.Now;
        public string Repaired { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty;
        public int VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public Vehicle Vehicle { get; set; } = default!;
    }
}