using System.ComponentModel.DataAnnotations;

namespace PROG7312_P1_V1.DataModels
{
    public class Events
    {
        [Key]
        public string EventId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        [Required]
        public DateTime EventDate { get; set; }    // Date and time of the event

        [Required]
        public string Location { get; set; }

        [Required]
        public byte[] ImageBytes { get; set; }
    }
}
