using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskMenager.Data;
using TaskMenager.Models;
using System.Collections.Generic;
using System.Linq;

namespace TaskMenager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly TaskManagerContext _context;

        public TasksController(TaskManagerContext context)
        {
            _context = context;
        }

        // GET: api/tasks
        [HttpGet]
        public IEnumerable<TaskItem> GetTasks()
        {
            return _context.Tasks.ToList();
        }

        // GET: api/tasks/5
        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public IActionResult CreateTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Ok(task);
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, TaskItem updatedTask)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.ImagePath = updatedTask.ImagePath;
            task.Latitude = updatedTask.Latitude;
            task.Longitude = updatedTask.Longitude;

            _context.SaveChanges();
            return Ok(task);
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}/status")]
        public IActionResult UpdateStatus(int id, [FromBody] int status)
        {
            var task = _context.Tasks.Find(id);
            if (task == null)
                return NotFound();

            task.Status = status;
            _context.SaveChanges();

            return Ok(task);
        }

        [HttpGet("created/{userId}")]
        public IActionResult GetCreatedByMe(int userId)
        {
            return Ok(_context.Tasks
                .Where(t => t.CreatedByUserId == userId)
                .ToList());
        }

        [HttpGet("assigned/{userId}")]
        public IActionResult GetAssignedToMe(int userId)
        {
            return Ok(_context.Tasks
                .Where(t => t.AssignedUserId == userId)
                .ToList());
        }

        [HttpGet("status/{status}")]
        public IActionResult GetByStatus(int status)
        {
            return Ok(_context.Tasks
                .Where(t => t.Status == status)
                .ToList());
        }


    }
}
