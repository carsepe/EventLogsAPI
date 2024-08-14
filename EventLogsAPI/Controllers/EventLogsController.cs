using Microsoft.AspNetCore.Mvc;
using EventLogsAPI.Models;
using EventLogsAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace EventLogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventLogsController : ControllerBase
    {
        private readonly EventLogService _eventLogService;

        public EventLogsController(EventLogService eventLogService)
        {
            _eventLogService = eventLogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventLog([FromBody] EventLog eventLog, [FromHeader(Name = "X-Source")] string source)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Determinar el tipo de evento según el origen
            if (string.IsNullOrEmpty(source) || source == "Api")
            {
                eventLog.EventType = "Api";
            }
            else if (source == "Formulario")
            {
                eventLog.EventType = "Formulario";
            }

            await _eventLogService.AddEventLogAsync(eventLog);
            return Ok(eventLog);
        }


        [HttpGet]
        public async Task<IActionResult> GetEventLogs([FromQuery] string? eventType = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            if (string.IsNullOrEmpty(eventType) && !startDate.HasValue && !endDate.HasValue)
            {
                var allEvents = await _eventLogService.GetAllEventLogsAsync();
                return Ok(allEvents);
            }

            var events = await _eventLogService.GetEventLogsAsync(eventType, startDate, endDate);
            return Ok(events);
        }


    }
}
