using Microsoft.AspNetCore.Mvc;
using RestaurantFinder.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering; // This using statement is required to utilize a viewbag

namespace RestaurantFinder.Controllers
{
  public class RestaurantsController : Controller
  {
    private readonly RestaurantFinderContext _db;

    public RestaurantsController(RestaurantFinderContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Restaurant> model = _db.Restaurants.Include(restaurant => restaurant.Cuisine).ToList(); //Include was added in the CuisinesController to the Details view, because that is where we needed to pass in the restaurants associated with that cuisine; here we use include to pass in the cuisine associated with each restaurant
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CuisineId = new SelectList(_db.Cuisines, "CuisineId", "Name"); // A ViewBag is used to shovel more things into your route that you didn't originally include in your model. In this case our ViewBag is filled with a CuisineId that is then deposited in a dropdown menu in our create view.
      return View();
    }

    [HttpPost]
    public ActionResult Create(Restaurant restaurant)
    {
      _db.Restaurants.Add(restaurant);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}