using Microsoft.AspNetCore.Mvc;
using PROG7312_P1_V1.DataModels;
using PROG7312_P1_V1.Languages;
using PROG7312_P1_V1.Services;

namespace PROG7312_P1_V1.Controllers
{
    public class ReportIssueController : Controller
    {
        private readonly ReportIssueService _reportIssueService;

        public ReportIssueController(ReportIssueService reportIssueService)
        {
            _reportIssueService = reportIssueService;
        }

        public IActionResult ReportIssue()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitIssue(string Category, string Location, string Description, IFormFile ImageFile)
        {
            //Save the image
            byte[] imageSaved = await _reportIssueService.SaveImage(ImageFile);

            // Save the issue report
            await _reportIssueService.SaveIssueReport(new DataModels.IssueReport
            {
                Category = Category, 
                Location = Location, 
                Description = Description, 
                ImageBytes = imageSaved
            });
                
            // Redirect to a confirmation page or back to the report issue page
            TempData["SuccessMessage"] = Labels.txtIssueReportSuccess;
            return RedirectToAction("ReportIssue");
        }
    }
}
