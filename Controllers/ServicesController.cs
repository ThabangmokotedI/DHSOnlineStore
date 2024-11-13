using Microsoft.AspNetCore.Mvc;
using DHSOnlineStore.Models;

public class ServicesController : Controller
{
    public IActionResult Index()
    {
        var model = new ServicesViewModel
        {
            SecurityServices = new List<string>
            {
                "Stationary and Patrolling Guards",
                "Gate and Boom Guarding",
                "CCTV Installation and Monitoring",
                "Alarm Installation",
                "Access Control",
                "Armed Reaction",
                "Fire/Evacuation Emergency",
                "Health/Accident Emergency"
            },
            CleaningServices = new List<string>
            {
                "Floor Cleaning",
                "Wall Cleaning",
                "Window Cleaning",
                "Furniture Cleaning",
                "Kitchen Cleaning",
                "Surface Cleaning",
                "Exterior Cleaning"
            },
            ICTServices = new List<string>
            {
                "ICT Hardware Sales & Maintenance",
                "Software & Mobile Application Development",
                "IT Infrastructure Audit and Consultancy",
                "Audio-Visual Media Creation",
                "Social Media Management",
                "Networking and Security",
                "Web Application Development and Hosting",
                "Air Condition Installation, Repair, and Maintenance"
            }
        };

        return View(model);
    }
}
