using DotnetPdfGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DotnetPdfGenerator.Controllers
{
    public class PdfGeneratorController : Controller
    {
        public IActionResult Index(string model)
        {
            var employees = JsonSerializer.Deserialize<IEnumerable<Employee>>(model);
            return View(employees);
        }
    }
}
