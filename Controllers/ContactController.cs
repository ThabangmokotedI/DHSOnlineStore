using Microsoft.AspNetCore.Mvc;
using DHSOnlineStore.Models;

public class ContactController : Controller
{
    public IActionResult Index()
    {
        var model = new ContactViewModel
        {
            Address = "19 Blackthorn Street, Mandela View, Bloemfontein, 9301",
            PhoneNumber = "068 096 4807",
            Email = "sanele.mbhele@darkhawk.co.za",
            WorkingHours = "Mon - Fri: 9am - 5pm"
        };

        return View(model);
    }
}
