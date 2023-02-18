using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase {
        private readonly ApplicationDbContext _context;
        public VehicleController(ApplicationDbContext context) => _context = context;
        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> Get() {
            var response = await _context.Vehicles.ToListAsync();

            if (response == null)
                return BadRequest($"There are no vehicles or is {response}");

            foreach (var item in response) {
                var client = await _context.Clients.FindAsync(item.ClientId);
                client!.Vehicles = new List<Vehicle>();
                item.Client = client;
                var repairs = await _context.Repairs.Where(x => x.VehicleId == item.Id).ToListAsync();
                foreach (var vehicle in repairs) {
                    vehicle.Vehicle = null!;
                }
                item.Repairs = repairs;
            }

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetById(int id) {
            var response = await _context.Vehicles.FindAsync(id);

            if (response == null)
                return BadRequest($"The vehicle does not exist or is {response}");

            var client = await _context.Clients.FindAsync(response.ClientId);
            client!.Vehicles = new List<Vehicle>();
            response.Client = client;
            var repairs = await _context.Repairs.Where(x => x.VehicleId == response.Id).ToListAsync();
            foreach (var item in repairs) {
                item.Vehicle = new Vehicle();
            }
            response.Repairs = repairs;

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<List<Vehicle>>> Post(VehicleDto request) {
            if (request == null)
                return BadRequest($"Vehicle data is empty or is {request}");

            var _request = new Vehicle() {
                Tuition = request.Tuition,
                Brand = request.Brand,
                Model = request.Model,
                Color = request.Color,
                RegistrationDate = request.RegistrationDate,
                ClientId = request.ClientId
            };

            await _context.AddAsync(_request);
            await _context.SaveChangesAsync();

            var response = await _context.Vehicles.ToListAsync();

            foreach (var item in response) {
                var client = await _context.Clients.FindAsync(item.ClientId);
                client!.Vehicles = new List<Vehicle>();
                item.Client = client;
                var repairs = await _context.Repairs.Where(x => x.VehicleId == item.Id).ToListAsync();
                foreach (var vehicle in repairs) {
                    vehicle.Vehicle = null!;
                }
                item.Repairs = repairs;
            }

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Vehicle>> Put(int id, VehicleDto request) {
            if (request == null)
                return BadRequest($"Vehicle data is empty or is {request}");

            var response = await _context.Vehicles.FindAsync(id);

            if (response == null)
                return BadRequest($"The vehicle does not exist or is {response}");

            response.Tuition = request.Tuition;
            response.Brand = request.Brand;
            response.Model = request.Model;
            response.Color = request.Color;
            response.RegistrationDate = request.RegistrationDate;
            response.ClientId = request.ClientId;

            await _context.SaveChangesAsync();

            var client = await _context.Clients.FindAsync(response.ClientId);
            client!.Vehicles = new List<Vehicle>();
            response.Client = client;
            var repairs = await _context.Repairs.Where(x => x.VehicleId == response.Id).ToListAsync();
            foreach (var item in repairs) {
                item.Vehicle = new Vehicle();
            }
            response.Repairs = repairs;

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Vehicle>> Delete(int id) {
            var response = await _context.Vehicles.FindAsync(id);

            if (response == null)
                return BadRequest($"The vehicle does not exist or is {response}");

            _context.Remove(response);
            await _context.SaveChangesAsync();

            var client = await _context.Clients.FindAsync(response.ClientId);
            client!.Vehicles = new List<Vehicle>();
            response.Client = client;
            var repairs = await _context.Repairs.Where(x => x.VehicleId == response.Id).ToListAsync();
            foreach (var item in repairs) {
                item.Vehicle = new Vehicle();
            }
            response.Repairs = repairs;

            return Ok(response);
        }
    }
}