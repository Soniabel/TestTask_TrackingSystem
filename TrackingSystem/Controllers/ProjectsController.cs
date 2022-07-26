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
    public class ProjectsController : ControllerBase
    {
        private readonly DBTrackingContext _context;
        private readonly ILogger<ProjectsController> logger;

        public ProjectsController(DBTrackingContext context, ILogger<ProjectsController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetProjects()
        {
            try
            {
                var projects = await _context.Projects.ToListAsync();
                return Ok(projects);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing GET in {nameof(GetProject)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Projects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProject(int id)
        {
            try
            {
                var project = await _context.Projects.FindAsync(id);

                if (project == null)
                {
                    logger.LogWarning($"Record Not Found: {nameof(GetProject)} - ID: {id}");
                    return NotFound();
                }
                return Ok(project);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing GET in {nameof(GetProject)}");
                return StatusCode(500, Messages.Error500Message);
            }
           
        }

        // PUT: api/Projects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, Project project)
        {
            if (id != project.Id)
            {
                logger.LogWarning($"Update ID invalid in {nameof(PutProject)} - ID: {id}");
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProjectExists(id))
                {
                    return NotFound();
                }
                else
                {
                    logger.LogError(ex, $"Error Performing GET in {nameof(PutProject)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        // POST: api/Projects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(Project project)
        {
          if (_context.Projects == null)
          {
              return Problem("Entity set 'DBTrackingContext.Projects'  is null.");
          }

            try 
            {
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProject", new { id = project.Id }, project);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing POST in {nameof(PostProject)}");
                return StatusCode(500, Messages.Error500Message);
            }
            
        }

        // DELETE: api/Projects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            try
            {
                var project = await _context.Projects.FindAsync(id);
                if (project == null)
                {
                    logger.LogWarning($"{nameof(Project)} record not found in {nameof(DeleteProject)} - ID: {id}");
                    return NotFound();
                }

                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error Performing DELETE in {nameof(DeleteProject)}");
                return StatusCode(500, Messages.Error500Message);
            }
           
        }

        private bool ProjectExists(int id)
        {
            return (_context.Projects?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
