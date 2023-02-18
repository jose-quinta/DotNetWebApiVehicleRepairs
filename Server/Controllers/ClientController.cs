using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase {
        private readonly ApplicationDbContext _context;
        public ClientController(ApplicationDbContext context) => _context = context;
        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get() {
            var response = await _context.Clients.ToListAsync();

            if (response == null)
                return BadRequest($"There are no clients or is {response}");

            foreach (var item in response) {
                var vehicles = await _context.Vehicles.Where(x => x.ClientId == item.Id).ToListAsync();
                foreach (var vehicle in vehicles) {
                    vehicle.Client = null!;
                    var repairs = await _context.Repairs.Where(x => x.VehicleId == vehicle.Id).ToListAsync();
                    foreach (var repair in repairs) {
                        repair.Vehicle = null!;
                    }
                    vehicle.Repairs = repairs;
                }
            }

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetById(int id) {
            var response = await _context.Clients.FindAsync(id);

            if (response == null)
                return BadRequest($"The client does not exist or is {response}");

            var vehicles = await _context.Vehicles.Where(x => x.ClientId == response.Id).ToListAsync();
            foreach (var item in vehicles) {
                item.Client = null!;
                var repairs = await _context.Repairs.Where(x => x.VehicleId == item.Id).ToListAsync();
                foreach (var repair in repairs) {
                    repair.Vehicle = null!;
                }
                item.Repairs = repairs;
            }

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<List<Client>>> Post(ClientDto request) {
            if (request == null)
                return BadRequest($"Client data is empty or is {request}");

            var _request = new Client() {
                DNI = request.DNI,
                Lastname = request.Lastname,
                Name = request.Name,
                Address = request.Address,
                Phonenumber = request.Phonenumber
            };

            await _context.AddAsync(_request);
            await _context.SaveChangesAsync();

            var response = await _context.Clients.ToListAsync();

            foreach (var item in response) {
                var vehicles = await _context.Vehicles.Where(x => x.ClientId == item.Id).ToListAsync();
                foreach (var vehicle in vehicles) {
                    vehicle.Client = null!;
                    var repairs = await _context.Repairs.Where(x => x.VehicleId == vehicle.Id).ToListAsync();
                    foreach (var repair in repairs) {
                        repair.Vehicle = null!;
                    }
                    vehicle.Repairs = repairs;
                }
            }

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Client>> Put(int id, ClientDto request) {
            if (request == null)
                return BadRequest($"Client data is empty or is {request}");

            var response = await _context.Clients.FindAsync(id);
            if (response == null)
                return BadRequest($"The client does not exist or is {response}");

            response.DNI = request.DNI;
            response.Lastname = request.Lastname;
            response.Name = request.Name;
            response.Address = request.Address;
            response.Phonenumber = request.Phonenumber;

            await _context.SaveChangesAsync();

            var vehicles = await _context.Vehicles.Where(x => x.ClientId == response.Id).ToListAsync();
            foreach (var item in vehicles) {
                item.Client = null!;
                var repairs = await _context.Repairs.Where(x => x.VehicleId == item.Id).ToListAsync();
                foreach (var repair in repairs) {
                    repair.Vehicle = null!;
                }
                item.Repairs = repairs;
            }

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Client>> Delete(int id) {
            var response = await _context.Clients.FindAsync(id);
            if (response == null)
                return BadRequest($"The client does not exist or is {response}");

            _context.Remove(response);
            await _context.SaveChangesAsync();

            var vehicles = await _context.Vehicles.Where(x => x.ClientId == response.Id).ToListAsync();
            foreach (var item in vehicles) {
                item.Client = null!;
                var repairs = await _context.Repairs.Where(x => x.VehicleId == item.Id).ToListAsync();
                foreach (var repair in repairs) {
                    repair.Vehicle = null!;
                }
                item.Repairs = repairs;
            }

            return Ok(response);
        }
    }
}