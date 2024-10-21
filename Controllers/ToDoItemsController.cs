using Kotarski_Wiktor_ToDo_API.Data;
using Kotarski_Wiktor_ToDo_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
namespace Kotarski_Wiktor_ToDo_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly TodoContext _context;
        public ToDoItemsController(TodoContext context)
        {
            _context = context;
        }

        //Creat or Edit
        [HttpPost]
        public JsonResult CreateEdit(TodoItem item)
        {
            if (item.Id == 0)
            {
                _context.Items.Add(item);
            }
            else
            {
                var ItemInDb = _context.Items.Find(item.Id);

                if (ItemInDb == null) return new JsonResult(NotFound());

                ItemInDb = item;
            }

            _context.SaveChanges();
            return new JsonResult(Ok(item));
        }

        //Set Todo percent complete
        [HttpPost]
        public JsonResult SetTodoPercent(int id, int percent)
        {
            var result = _context.Items.Find(id);
            if (result == null) return new JsonResult(NotFound());

            if (percent<100 && percent>=0)
            {
                result.PercentComplete = percent;
                _context.SaveChanges();
                return new JsonResult(result);
            }
            else return new JsonResult("The percentage value must be between 0 and 100");
        }

        //Mark Todo as Done
        [HttpPatch]
        public JsonResult MarkAsDone(int id)
        {
            var result = _context.Items.Find(id);
            if(result==null) return new JsonResult(NotFound());

            result.PercentComplete = 100;
            _context.SaveChanges();
            return new JsonResult(result);
        }

        //Get one
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Items.Find(id);

            if(result == null) return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //Get Incoming ToDo for today
        [HttpGet]
        public JsonResult GetIncomingToday()
        {
            var today = DateTime.Now.Date;
            var result = _context.Items.Where(item => item.DateOfExpiry.Date == today).ToList();

            if (result == null || result.Count == 0)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //Get Incoming ToDo for next day
        [HttpGet]
        public JsonResult GetIncomingTomorrow()
        {
            var tomorrow = DateTime.Now.Date.AddDays(1);
            var result = _context.Items.Where(item => item.DateOfExpiry.Date == tomorrow).ToList();

            if (result == null || result.Count == 0)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //Get Incoming ToDo current week
        [HttpGet]
        public JsonResult GetIncomingCurrentWeek()
        {

            int today = (int)DateTime.Now.DayOfWeek;
            int daysSinceMonday = (today == 0) ? 6 : today - 1;
            //Start counting from monday
            var startOfWeek = DateTime.Now.Date.AddDays(-daysSinceMonday);
            var endOfWeek = startOfWeek.AddDays(6);

            var result = _context.Items.Where(item => (item.DateOfExpiry.Date >= startOfWeek && item.DateOfExpiry.Date <= endOfWeek)).ToList();

            if (result == null || result.Count == 0)
                return new JsonResult(NotFound());

            return new JsonResult(Ok(result));
        }

        //Get All
        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Items.ToList();

            return new JsonResult(Ok(result));
        }

        //Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Items.Find(id);
            if(result==null) return new JsonResult(NotFound());
            _context.Items.Remove(result);
            _context.SaveChanges();
            return new JsonResult(NoContent());
        }
        
    }
}
