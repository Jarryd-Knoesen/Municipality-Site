using System.ComponentModel.DataAnnotations;

namespace PROG7312_P1_V1.DataModels
{
    public class Announcements
    {
        [Key]
        public string AnnouncementId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }
    }
}
