using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Entities {
    public class ClientDto {
        public string DNI { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Phonenumber { get; set; } = string.Empty;
    }
}