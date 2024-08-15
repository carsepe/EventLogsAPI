using Microsoft.AspNetCore.Mvc;
using EventLogsAPI.Models;
using EventLogsAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventLogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventLogsController : ControllerBase
    {
        private readonly EventLogService _eventLogService;
        private readonly ILogger<EventLogsController> _logger;

        public EventLogsController(EventLogService eventLogService, ILogger<EventLogsController> logger)
        {
            _eventLogService = eventLogService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEventLog([FromBody] EventLog eventLog, [FromHeader(Name = "X-Source")] string source)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState no es válida.");
                return BadRequest(ModelState);
            }

            try
            {
                // Sobrescribir el EventType basado en el valor del encabezado X-Source
                if (string.IsNullOrEmpty(source) || source == "Api")
                {
                    eventLog.EventType = "Api";
                }
                else if (source == "Formulario")
                {
                    eventLog.EventType = "Formulario";
                }
                else
                {
                    _logger.LogWarning("Tipo de origen inválido: {source}", source);
                    return BadRequest("Tipo de origen no válido. Por favor, verifique el valor del encabezado 'X-Source' y asegúrese de que sea 'Api' o 'Formulario'.");
                }

                await _eventLogService.AddEventLogAsync(eventLog);
                _logger.LogInformation("Evento registrado correctamente: {eventLog}", eventLog);
                return Ok(eventLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar el evento.");
                return StatusCode(500, "Ocurrió un error al registrar el evento.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEventLogs([FromQuery] string? eventType = null, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null)
        {
            try
            {
                if (string.IsNullOrEmpty(eventType) && !startDate.HasValue && !endDate.HasValue)
                {
                    var allEvents = await _eventLogService.GetAllEventLogsAsync();
                    return Ok(allEvents);
                }

                var events = await _eventLogService.GetEventLogsAsync(eventType, startDate, endDate);
                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los eventos.");
                return StatusCode(500, "Ocurrió un error al obtener los eventos.");
            }
        }
    }
}
