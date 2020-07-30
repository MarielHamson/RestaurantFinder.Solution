using Microsoft.AspNetCore.Mvc;

namespace RestaurantFinder.Controllers
{
  public class HomeController : Controller // The prefix of Controller is what the folder will be named inside of our Views folder. In this case the folder will be name 'Home'
  {

    [HttpGet("/")] // Without this route decorator the home page of our local host will be 'home', with the decorator it will just be blank
    public ActionResult Index() // the action result will be the the name of the file inside of our home folder, accept when we use a return of RedirectToAction()
    {
      return View();
    }
  }
}