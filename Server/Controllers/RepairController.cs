using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class RepairController : ControllerBase {
        private readonly ApplicationDbContext _context;
        public RepairController(ApplicationDbContext context) => _context = context;
        [HttpGet]
        public async Task<ActionResult<List<Repair>>> Get() {
            var response = await _context.Repairs.ToListAsync();

            if (response == null)
                return BadRequest($"There are no repairs or is {response}");

            foreach (var item in response) {
                var vehicle = await _context.Vehicles.FindAsync(item.VehicleId);
                vehicle!.Repairs = new List<Repair>();
                var client = await _context.Clients.FindAsync(vehicle.ClientId);
                client!.Vehicles = new List<Vehicle>();
                vehicle.Client = client;

                item.Vehicle = vehicle!;
            }

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Repair>> GetById(int id) {
            var response = await _context.Repairs.FindAsync(id);

            if (response == null)
                return BadRequest($"The repair does not exist or is {response}");

            var vehicle = await _context.Vehicles.FindAsync(response.VehicleId);
            vehicle!.Repairs = new List<Repair>();
            var client = await _context.Clients.FindAsync(vehicle.ClientId);
            client!.Vehicles = new List<Vehicle>();
            vehicle.Client = client;

            response.Vehicle = vehicle!;

            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult<List<Repair>>> Post(RepairDto request) {
            if (request == null)
                return BadRequest($"Repair data is empty or is {request}");

            var _request = new Repair() {
                EntryDate = request.EntryDate,
                Km = request.Km,
                Fault = request.Fault,
                DepartureDate = request.DepartureDate,
                Repaired = request.Repaired,
                Observation = request.Observation,
                VehicleId = request.VehicleId
            };

            await _context.AddAsync(_request);
            await _context.SaveChangesAsync();

            var response = await _context.Repairs.ToListAsync();

            if (response == null)
                return BadRequest($"There are no repairs or is {response}");

            foreach (var item in response) {
                var vehicle = await _context.Vehicles.FindAsync(item.VehicleId);
                vehicle!.Repairs = new List<Repair>();
                var client = await _context.Clients.FindAsync(vehicle.ClientId);
                client!.Vehicles = new List<Vehicle>();
                vehicle.Client = client;

                item.Vehicle = vehicle!;
            }

            return Ok(response);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<Repair>> Put(int id, RepairDto request) {
            if (request == null)
                return BadRequest($"Repair data is empty or is {request}");

            var response = await _context.Repairs.FindAsync(id);

            if (response == null)
                return BadRequest($"The repair does not exist or is {response}");

            response.EntryDate = request.EntryDate;
            response.Km = request.Km;
            response.Fault = request.Fault;
            response.DepartureDate = request.DepartureDate;
            response.Repaired = request.Repaired;
            response.Observation = request.Observation;
            response.VehicleId = request.VehicleId;

            await _context.SaveChangesAsync();

            var vehicle = await _context.Vehicles.FindAsync(response.VehicleId);
            vehicle!.Repairs = new List<Repair>();
            var client = await _context.Clients.FindAsync(vehicle.ClientId);
            client!.Vehicles = new List<Vehicle>();
            vehicle.Client = client;

            response.Vehicle = vehicle!;

            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Repair>> Delete(int id) {
            var response = await _context.Repairs.FindAsync(id);

            if (response == null)
                return BadRequest($"The repair does not exist or is {response}");

            _context.Remove(response);
            await _context.SaveChangesAsync();

            var vehicle = await _context.Vehicles.FindAsync(response.VehicleId);
            vehicle!.Repairs = new List<Repair>();
            var client = await _context.Clients.FindAsync(vehicle.ClientId);
            client!.Vehicles = new List<Vehicle>();
            vehicle.Client = client;

            response.Vehicle = vehicle!;

            return Ok(response);
        }
    }
}