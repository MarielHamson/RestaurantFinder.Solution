using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantFinder.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantFinder.Controllers
{
  public class CuisinesController : Controller
  {
    private readonly RestaurantFinderContext _db; // field; type is RestaurantFinderContext

    public CuisinesController(RestaurantFinderContext db) // constructor; set db property to RestaurantFinder context
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Cuisine> model = _db.Cuisines.ToList(); // references db cuisines table & creates a list, saved to model
      return View(model);
    }
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Cuisine cuisine)
    {
      _db.Cuisines.Add(cuisine);
      _db.SaveChanges();
      return RedirectToAction("Index"); // Instead of the View method, RedirectToAction allows you to trigger another route. In this case, we are triggering the index route.
    }

    public ActionResult Details(int id)
    {
      Cuisine thisCuisine = _db.Cuisines.Include(cuisine => cuisine.Restaurants).FirstOrDefault(cuisine =>cuisine.CuisineId == id); // passes list of restaurants associated with that cuisine ID. Saves result of search of cuisine ID to thisCuisine, returns thisCuisine in View.

      return View(thisCuisine);
    }

    public ActionResult Edit (int id)
    {
      Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      return View(thisCuisine);
    }

    [HttpPost]
    public ActionResult Edit(Cuisine cuisine)
    {
      _db.Entry(cuisine).State = EntityState.Modified; // need to change state of entry so that Entity knows to looks for changes
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      return View(thisCuisine);
    }

    [HttpPost, ActionName("Delete")] // method signatures are the same for both delete methods, so we have to change name to DeleteConfirm; need ActionName to be "Delete" so that they can still match
    public ActionResult DeleteConfirmed(int id)
    {
      Cuisine thisCuisine = _db.Cuisines.FirstOrDefault(cuisine => cuisine.CuisineId == id);
      _db.Cuisines.Remove(thisCuisine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}