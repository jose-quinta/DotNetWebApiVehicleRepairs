using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities {
    public class RepairDto {
        public DateTime EntryDate { get; set; } = DateTime.Now;
        public int Km { get; set; } = 0;
        public string Fault { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; } = DateTime.Now;
        public string Repaired { get; set; } = string.Empty;
        public string Observation { get; set; } = string.Empty;
        public int VehicleId { get; set; }
    }
}