using Microsoft.AspNetCore.Mvc;
using EventLogsAPI.Models;
using EventLogsAPI.Services;

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

        // Endpoint para crear un nuevo registro de evento
        [HttpPost]
        public async Task<IActionResult> CreateEventLog([FromBody] EventLog eventLog)
        {
            // Validación del modelo; si no es válido, devuelve errores de validación
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devuelve los errores de validación si el modelo no es válido
            }

            // Llama al servicio para agregar el nuevo registro de evento
            await _eventLogService.AddEventLogAsync(eventLog);
            return Ok(eventLog); // Devuelve el evento creado
        }

        // Endpoint para obtener registros de eventos filtrados por tipo y fecha
        [HttpGet]
        public async Task<IActionResult> GetEventLogs([FromQuery] string eventType, [FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            // Llama al servicio para obtener los registros de eventos filtrados
            var events = await _eventLogService.GetEventLogsAsync(eventType, startDate, endDate);
            return Ok(events); // Devuelve la lista de eventos filtrados
        }
    }
}
