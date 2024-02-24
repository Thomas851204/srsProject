using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using srsProject.Models;

namespace srsProject.Controllers
{
 public class SrsProjectController : Controller
{
    private readonly srsProjectContext _context;

    public IActionResult Index()
    {
        return View();
    }

    public SrsProjectController(srsProjectContext context)
    {
        _context = context; 
    }

    // Displays a list of owners
    public async Task<IActionResult> Owner()
    {
        return View(await _context.Owners.ToListAsync());
    }
    //Get to EditOwner view
    public IActionResult EditOwner(int ownerId)
    {
        var owner = _context.Owners.Find(ownerId);

        if (owner == null)
        {
            return NotFound();
        }

        return View(owner);
    }

    //Edit owners' data 
    [HttpPost]
    public  IActionResult UpdateOwner(Owner ownerModel)
    {
        if (ModelState.IsValid)
        {
            try
            {       
                _context.Update(ownerModel);
                _context.SaveChanges();
                return  RedirectToAction("Owner");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
        }
        return View("EditOwner", ownerModel);
    }
    //Get to AddOwner view
    public IActionResult AddOwner()
    {
        return View();
    }
    //Add new owner to database
    [HttpPost]
    public IActionResult SaveOwner(Owner ownerModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Owners.Add(ownerModel);
                _context.SaveChanges();
                return RedirectToAction("Owner");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("AddOwner", ownerModel);
            }
        }
        return View("AddOwner", ownerModel);
    }
    //Lists cars
    public async Task<IActionResult> Car()
    {
        
        return View(await _context.Cars.ToListAsync());
    }
    //Get to EditCar view
    public IActionResult EditCar(int carId)
    {
        var car = _context.Cars.Find(carId);

        if (car == null)
        {
            return NotFound();
        }

        return View(car);
    }
    //Edit cars data 
    [HttpPost]
    public  IActionResult UpdateCar(Car carModel)
    {
        if (ModelState.IsValid)
        {
            try
            {       
                _context.Update(carModel);
                _context.SaveChanges();
                return  RedirectToAction("Car");
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
        }
        return View("EditCar", carModel);
    }
    //Get to AddCar view
    public IActionResult AddCar()
    {
        return View();
    }
    //Add new car to database
    [HttpPost]
    public IActionResult SaveCar(Car carModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Cars.Add(carModel);
                _context.SaveChanges();
                return RedirectToAction("Car");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to save changes. Please try again.");
                return View("AddCar", carModel);
            }
        }
        return View("AddCar", carModel);
    }
    //Calls stored procedure in SQL
    public IActionResult ListOwnerCars(int ownerId)
    {
        var cars = _context.Cars.FromSqlRaw("EXECUTE GetCarsByUserId @OwnerId", new Microsoft.Data.SqlClient.SqlParameter("@OwnerId", ownerId)).ToList();
        return View(cars);
    }
}
}
