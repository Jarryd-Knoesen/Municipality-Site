using System.Security.Cryptography;

namespace PROG7312_P1_V1.Models
{
    public class IssueViewModel
    {
        public string Category { get; set; }
        public string Location { get; set; }
        public string Issue { get; set; }
        public IFormFile ImageFile { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
