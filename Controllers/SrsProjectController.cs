using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using srsProject.Models;

namespace srsProject.Controllers
{
 public class SrsProjectController(srsProjectContext context) : Controller
{
    private readonly srsProjectContext _context = context;

    public IActionResult Index()
    {
        return View();
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
    //Delete owner
    public IActionResult DeleteOwner(int ownerId)
    {
        var owner = _context.Owners.Find(ownerId);
        if (owner == null)
        {
            return NotFound();
        }
        var ownerships = _context.Ownerships.Where(o => o.OwnerId == ownerId);
        _context.Ownerships.RemoveRange(ownerships);
        _context.Owners.Remove(owner);
        _context.SaveChanges();
        return RedirectToAction("Owner");
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
    //Delete car
    public IActionResult DeleteCar(int carId)
    {
        var car = _context.Cars.Find(carId);
        if (car == null)
        {
            return NotFound();
        }
        var ownerships = _context.Ownerships.Where(o => o.CarId == carId);
        _context.Ownerships.RemoveRange(ownerships);
        _context.Cars.Remove(car);
        _context.SaveChanges();
        return RedirectToAction("Car");
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
        var owner = _context.Owners.FirstOrDefault(owner =>owner.Id == ownerId);
        var cars = _context.Cars.FromSqlRaw("EXECUTE GetCarsByUserId @OwnerId", new Microsoft.Data.SqlClient.SqlParameter("@OwnerId", ownerId)).ToList();
        ViewData["OwnerId"]=ownerId;
        if (owner !=null)
        {
        ViewData["OwnerName"]=owner.FirstName+" "+owner.LastName;
        }
        return View(cars);
    }
    //Remove car from owner
    public IActionResult RemoveCarFromOwner(int ownerId, int carId)
    {
        var ownership = _context.Ownerships.FirstOrDefault(o => o.OwnerId == ownerId && o.CarId == carId);

        if (ownership != null)
        {
            _context.Ownerships.Remove(ownership);
            _context.SaveChanges();
        }

        return RedirectToAction("ListOwnerCars", new { ownerId });
    }
    //Get to AvailableCars view
    public IActionResult AvailableCars(int ownerId)
    {
        var ownedCarIds = _context.Ownerships.Where(o => o.OwnerId == ownerId).Select(o => o.CarId).ToList();
        var availableCars = _context.Cars.Where(c => !ownedCarIds.Contains(c.Id)).ToList();
        ViewData["OwnerId"] = ownerId;
        return View(availableCars);
    }
    //Add a car to the owner
    [HttpPost]
    public IActionResult AddCarsToOwner(int ownerId, int[] selectedCars)
    {
        if (selectedCars != null)
        {
            foreach (var carId in selectedCars)
            {
                
                var existingOwnership = _context.Ownerships.FirstOrDefault(o => o.OwnerId == ownerId && o.CarId == carId);
                if (existingOwnership == null)
                {
                  
                    var owner = _context.Owners.Find(ownerId);
                    var car = _context.Cars.Find(carId);
                    
                    if (owner != null && car!= null)
                    {    
                    var newOwnership = new Ownership { OwnerId = ownerId, CarId = carId, Owner = owner, Car = car };

                    _context.Ownerships.Add(newOwnership);
                    }
                }
            }
            _context.SaveChanges();
        }
        return RedirectToAction("ListOwnerCars", new { ownerId });
    }
}
}
