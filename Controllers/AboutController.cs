using Microsoft.AspNetCore.Mvc;
using DHSOnlineStore.Models;

public class AboutController : Controller
{
    public IActionResult Index()
    {
        var model = new AboutViewModel
        {
            CompanyDescription = "Dark Hawk Security (PTY) LTD is a newly registered company based in Bloemfontein, Free State, with a vision to provide top-quality security services locally and globally.",
            Mission = "To be the best security company that provides exceptional quality service to our clients.",
            Vision = "To be one of the most recommended security companies in South Africa within 5 years.",
            Values = new List<string>
            {
                "Excellence",
                "Professionalism",
                "Accuracy",
                "Determination",
                "Discipline"
            }
        };

        return View(model);
    }
}
