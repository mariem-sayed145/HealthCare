using HealthCareAPI.Data;
using HealthCareAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCareAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly HealthCareDbContext _context;

        public AppointmentsController(HealthCareDbContext context)
        {
            _context = context;
        }

        // GET: api/Appointments

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            return await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .ToListAsync();
        }

        // GET: api/Appointments/id

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound();
            }

            return appointment;
        }

        // POST: api/Appointments

        [HttpPost]
        public async Task<ActionResult<Appointment>> CreateAppointment(
            Appointment appointment)
        {
            var patientExists = await _context.Patients
                .AnyAsync(p => p.Id == appointment.PatientId);

            var doctorExists = await _context.Doctors
                .AnyAsync(d => d.Id == appointment.DoctorId);

            if (!patientExists || !doctorExists)
            {
                return BadRequest(
                    "Patient or Doctor does not exist.");
            }

            _context.Appointments.Add(appointment);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAppointment),
                new { id = appointment.Id },
                appointment);
        }

        // PUT: api/Appointments/5

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(
            int id,
            Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return BadRequest();
            }

            var existingAppointment = await _context.Appointments
                .FindAsync(id);

            if (existingAppointment == null)
            {
                return NotFound();
            }

            var patientExists = await _context.Patients
                .AnyAsync(p => p.Id == appointment.PatientId);

            var doctorExists = await _context.Doctors
                .AnyAsync(d => d.Id == appointment.DoctorId);

            if (!patientExists || !doctorExists)
            {
                return BadRequest(
                    "Patient or Doctor does not exist.");
            }

            existingAppointment.PatientId = appointment.PatientId;

            existingAppointment.DoctorId = appointment.DoctorId;

            existingAppointment.AppointmentDate =
                appointment.AppointmentDate;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Appointments/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
            {
                return NotFound();
            }

            _context.Appointments.Remove(appointment);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}