using HealthCareAPI.Data;
using HealthCareAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly HealthCareDbContext _context;

        public DoctorsController(HealthCareDbContext context)
        {
            _context = context;
        }

        //Get Doctors 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Doctor>>> GetDoctors()
        {
            return await _context.Doctors.ToListAsync();
        }

        //Get Doctor by id 

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return doctor;
        }

        //Create doctor 

        [HttpPost]
        public async Task<ActionResult<Doctor>> CreateDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDoctor),
                new { id = doctor.Id },
                doctor);
        }

        // edit 

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor(
            int id,
            Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return BadRequest();
            }

            var existingDoctor = await _context.Doctors.FindAsync(id);

            if (existingDoctor == null)
            {
                return NotFound();
            }

            existingDoctor.Name = doctor.Name;

            existingDoctor.Specialization = doctor.Specialization;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Delete 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);

            if (doctor == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doctor);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}