using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities {
    public class VehicleDto {
        public string Tuition { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public int ClientId { get; set; }
    }
}