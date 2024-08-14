using System.ComponentModel.DataAnnotations;

namespace EventLogsAPI.Models
{
    public class EventLog
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La fecha del evento es obligatoria")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El tipo de evento es obligatorio")]
        public string EventType { get; set; }
    }
}
