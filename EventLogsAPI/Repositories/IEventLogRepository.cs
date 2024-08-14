using EventLogsAPI.Models;

namespace EventLogsAPI.Repositories
{
    public interface IEventLogRepository
    {
        Task AddEventLogAsync(EventLog eventLog);
        Task<IEnumerable<EventLog>> GetEventLogsAsync(string eventType, DateTime? startDate, DateTime? endDate);
    }
}
