using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrackingSystem;
using TrackingSystem.Static;

namespace TrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private readonly DBTrackingContext _context;
        private readonly ILogger<ActivitiesController> logger;

        public ActivitiesController(DBTrackingContext context, ILogger<ActivitiesController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/Activities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
        {
            try
            {
                var activities = await _context.Activities.ToListAsync();
                return Ok(activities);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing GET in {nameof(GetActivities)}");
                return StatusCode(500, Messages.Error500Message);
            }
           
        }

        // GET: api/Activities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            try
            {
                var activity = await _context.Activities.FindAsync(id);

                if (activity == null)
                {
                    logger.LogWarning($"Record Not Found: {nameof(GetActivity)} - ID: {id}");
                    return NotFound();
                }
                return Ok(activity);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error Performing GET in {nameof(GetActivity)}");
                return StatusCode(500, Messages.Error500Message);
            }
           
        }

        

        // PUT: api/Activities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, Activity activity)
        {
            if (id != activity.Id)
            {
                logger.LogWarning($"Update ID invalid in {nameof(PutActivity)} - ID: {id}");
                return BadRequest();
            }

            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ActivityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogError(ex, $"Error Performing GET in {nameof(PutActivity)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        // POST: api/Activities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Activity>> PostActivity(Activity activity)
        {
            try
            {
                if (_context.Activities == null)
                {
                    return Problem("Entity set 'DBTrackingContext.Activities'  is null.");
                }
                _context.Activities.Add(activity);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetActivity", new { id = activity.Id }, activity);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing POST in {nameof(PostActivity)}", activity);
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Activities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            try
            {
                var activity = await _context.Activities.FindAsync(id);
                if (activity == null)
                {
                    logger.LogWarning($"{nameof(Activity)} record not found in {nameof(DeleteActivity)} - ID: {id}");
                    return NotFound();
                }

                _context.Activities.Remove(activity);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteActivity)}");
                return StatusCode(500, Messages.Error500Message);
            }
               
        }

        private bool ActivityExists(int id)
        {
            return (_context.Activities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
