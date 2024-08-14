using EventLogsAPI.Data;
using EventLogsAPI.Models;
using EventLogsAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventLogsAPI.Services
{
    public class EventLogService
    {
        private readonly ApplicationDbContext _context;

        public EventLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddEventLogAsync(EventLog eventLog)
        {
            _context.EventLogs.Add(eventLog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventLog>> GetEventLogsAsync(string eventType, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.EventLogs.AsQueryable();

            if (!string.IsNullOrEmpty(eventType))
            {
                query = query.Where(e => e.EventType == eventType);
            }

            if (startDate.HasValue)
            {
                query = query.Where(e => e.EventDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(e => e.EventDate <= endDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<EventLog>> GetAllEventLogsAsync()
        {
            return await _context.EventLogs.ToListAsync();
        }
    }

}
