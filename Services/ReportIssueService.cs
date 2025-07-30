using Microsoft.AspNetCore.Mvc.ModelBinding;
using PROG7312_P1_V1.Database;
using PROG7312_P1_V1.DataModels;
using PROG7312_P1_V1.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROG7312_P1_V1.Services
{
    public class ReportIssueService
    {
        private readonly AppDbContext _context;

        public ReportIssueService(AppDbContext context)
        {
            _context = context;
        }

        // Will save the report
        public async Task<bool> SaveIssueReport(IssueReport issue)
        
        {
            issue.IssueId = Guid.NewGuid().ToString();
            _context.IssueReports.Add(issue);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<byte[]> SaveImage(IFormFile ImageFile)
        {
            // Access the uploaded image file
            IFormFile image = ImageFile;

            // variable to hold the image bytes
            byte[] imageBytes = null;

            if (image != null && image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    imageBytes = ms.ToArray();
                    //issue.ImageBytes = imageBytes;
                }
                // Now imageBytes holds the image content
                return imageBytes;
            }

            return null; // No image file provided or file is empty
        }
    }
}
