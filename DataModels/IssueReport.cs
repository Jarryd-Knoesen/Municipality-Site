using System.ComponentModel.DataAnnotations;

namespace PROG7312_P1_V1.DataModels
{
    public class IssueReport
    {
        [Key]
        public string IssueId { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public byte[] ImageBytes { get; set; } // stores the image data

        [Required]
        public DateTime ReportedDate { get; set; } = DateTime.Now;
    }
}
