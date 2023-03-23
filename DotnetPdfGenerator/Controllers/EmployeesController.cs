using DotnetPdfGenerator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PuppeteerSharp;
using System.Text.Json;

namespace DotnetPdfGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        [HttpPost("get-report")]
        public async Task<IActionResult> GetEmployeesReport(IEnumerable<Employee> employees)
        {
            var options = new LaunchOptions
            {
                Headless = true,
            };

            var browserFetcher = new BrowserFetcher();
            await browserFetcher.DownloadAsync();

            using var browser = await Puppeteer.LaunchAsync(options);
            using var page = await browser.NewPageAsync();
            using var memoryStream = new MemoryStream();

            var url = Url.ActionLink("Index", "PdfGenerator", new {model = JsonSerializer.Serialize(employees)});
            await page.GoToAsync(url);

            var pdfStream = await page.PdfDataAsync();

            return File(pdfStream, "application/pdf", "employeesReport.pdf");
        }
    }
}
