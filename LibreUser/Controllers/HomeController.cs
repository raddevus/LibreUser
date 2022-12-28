using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibreUser.Models;

namespace LibreUser.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost("Save")]
    public ActionResult Save([FromForm] String guid, [FromForm] String hashedPwd = null){

        // SqliteProvider sp = new SqliteProvider();
        // WriteUsage(sp,"SaveToken",guid);

        // Had to new up the SqliteProvider to insure it was initialized properly
        // for use with UserData
        SqliteUserProvider sp = new SqliteUserProvider();
        User user = new User(guid, hashedPwd);
        UserData ud = new UserData(sp,user);
        ud.ConfigureInsert();
        user = sp.GetOrInsert();
        
        var jsonResult = new {success=true,user=user};
        return new JsonResult(jsonResult);
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
