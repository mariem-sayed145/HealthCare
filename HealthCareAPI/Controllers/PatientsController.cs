using HealthCareAPI.Data;
using HealthCareAPI.Models;
using Microsoft.AspNetCore.Mvc ;
using Microsoft.EntityFrameworkCore;

namespace HealthCareAPI.Controllers;
[Route("api/[controller]")]
[ApiController]

public class PatientsController : ControllerBase
{
    private readonly HealthCareDbContext _context;
    
    public PatientsController(HealthCareDbContext context)
    {
        _context = context;
    }
    
    // Get Patients 
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Patient>>> GetPatients()
    {
        return await _context.Patients.ToListAsync();
    }
    
    // Get patient by id 
    [HttpGet("{id}")]
    public async Task<ActionResult<Patient>> GetPatient(string id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if (patient == null)
        {
            return NotFound();
        }
        return patient;
    }
    
    // Create patient 
    [HttpPost]
    public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
    {
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
    }
    
    //Update Patient 
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePatient(int id, Patient patient)
    {
        if (id != patient.Id)
        {
            return BadRequest();
        }

        var existingPatient = await _context.Patients.FindAsync(id);

        if (existingPatient == null)
        {
            return NotFound();
        }
        
        existingPatient.Name = patient.Name;
        existingPatient.DateOfBirth = patient.DateOfBirth;
        await _context.SaveChangesAsync();
        return NoContent();
        
    }
    
    //Delete Patient 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var patient = await _context.Patients.FindAsync(id);

        if (patient == null)
        {
            return NotFound();
        }

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
}