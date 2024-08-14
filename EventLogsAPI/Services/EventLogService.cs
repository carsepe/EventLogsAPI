using EventLogsAPI.Models;
using EventLogsAPI.Repositories;

namespace EventLogsAPI.Services
{
    public class EventLogService
    {
        private readonly IEventLogRepository _eventLogRepository;

        public EventLogService(IEventLogRepository eventLogRepository)
        {
            _eventLogRepository = eventLogRepository;
        }

        public async Task AddEventLogAsync(EventLog eventLog)
        {
            await _eventLogRepository.AddEventLogAsync(eventLog);
        }

        public async Task<IEnumerable<EventLog>> GetEventLogsAsync(string eventType, DateTime? startDate, DateTime? endDate)
        {
            return await _eventLogRepository.GetEventLogsAsync(eventType, startDate, endDate);
        }
    }
}
