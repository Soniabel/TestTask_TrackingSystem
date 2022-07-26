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
    public class ActivityTypesController : ControllerBase
    {
        private readonly DBTrackingContext _context;
        private readonly ILogger<ActivityTypesController> logger;

        public ActivityTypesController(DBTrackingContext context, ILogger<ActivityTypesController> logger)
        {
            this.logger = logger;
            _context = context;
        }

        // GET: api/ActivityTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActivityType>>> GetActivityTypes()
        {
            try
            {
                if (_context.ActivityTypes == null)
                {
                    logger.LogWarning($"Records Not Found: {nameof(GetActivityTypes)}");
                    return NotFound();
                }
                var activityTypes = await _context.ActivityTypes.ToListAsync();
                return Ok(activityTypes);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error Performing GET in {nameof(GetActivityTypes)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/ActivityTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityType>> GetActivityType(int id)
        {
            try
            {
                var activityType = await _context.ActivityTypes.FindAsync(id);

                if (activityType == null)
                {
                    return NotFound();
                }

                return Ok(activityType);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error Performing GET in {nameof(GetActivityType)}");
                return StatusCode(500, Messages.Error500Message);
            }
          
        }

        // PUT: api/ActivityTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivityType(int id, ActivityType activityType)
        {
            if (id != activityType.Id)
            {
                logger.LogWarning($"Update ID invalid in {nameof(PutActivityType)} - ID: {id}");
                return BadRequest();
            }

            _context.Entry(activityType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ActivityTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogError(ex, $"Error Performing GET in {nameof(PutActivityType)}");
                    return StatusCode(500, Messages.Error500Message);
                    
                }
            }

            return NoContent();
        }

        // POST: api/ActivityTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ActivityType>> PostActivityType(ActivityType activityType)
        {
            try
            {
                if (_context.ActivityTypes == null)
                {
                    return Problem("Entity set 'DBTrackingContext.ActivityTypes'  is null.");
                }
                _context.ActivityTypes.Add(activityType);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetActivityType", new { id = activityType.Id }, activityType);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error Performing POST in {nameof(PostActivityType)}", activityType);
                return StatusCode(500, Messages.Error500Message);
            }
          
        }

        // DELETE: api/ActivityTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivityType(int id)
        {
            try
            {
                var activityType = await _context.ActivityTypes.FindAsync(id);
                if (activityType == null)
                {
                    logger.LogWarning($"{nameof(ActivityType)} record not found in {nameof(DeleteActivityType)} - ID: {id}");
                    return NotFound();
                }

                _context.ActivityTypes.Remove(activityType);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteActivityType)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private bool ActivityTypeExists(int id)
        {
            return (_context.ActivityTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
