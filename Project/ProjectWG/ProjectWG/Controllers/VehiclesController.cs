using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using Microsoft.EntityFrameworkCore;
using ProjectWG.Models;

namespace ProjectWG.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly UsedVehicleContext _context;

        public VehiclesController(UsedVehicleContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        /*  public async Task<IActionResult> Index()
          {
              return View(await _context.Vehicle.ToListAsync());
          }*/





        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Vehicle == null)//vehile
            {
                return Problem("Entity set 'MvcMovieContext.Staff' is null.");
            }
            var staffs = from m in _context.Vehicle //vehile
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                staffs = staffs.Where(s => s.Make!.Contains(searchString));   //make
            }
            return View(await staffs.ToListAsync());
        }



        //*********************************************************************************************************************//


        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            string Username = HttpContext.Session.GetString("Username");
            if (!string.IsNullOrEmpty(Username))
            {

                ViewBag.Username = Username;
                ;
                return View();
            }
            else
            {
                // Handle the case where the session does not have a username, possibly due to session expiration or not being logged in
                // Redirect to login page or show an error message
                return RedirectToAction("Login", "Users");
            }


        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*   [HttpPost]
           [ValidateAntiForgeryToken]
           public async Task<IActionResult> Create([Bind("Id,UserName,Make,Year,BodyType,Color,Picture")] Vehicles vehicles)
           {

               if (ModelState.IsValid)
               {
                   if (vehicles.PictureFile != null && vehicles.PictureFile.Length > 0)
                   {
                       var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" }; // Update as necessary
                       var extension = Path.GetExtension(vehicles.PictureFile.FileName).ToLowerInvariant();

                       if (!allowedExtensions.Contains(extension))
                       {
                           ModelState.AddModelError("PictureFile", "Invalid file type");
                       }
                       else
                       {
                           var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                           var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vehicles.PictureFile.FileName); // Ensuring unique file name
                           var filePath = Path.Combine(uploadPath, fileName);

                           using (var stream = new FileStream(filePath, FileMode.Create))
                           {
                               await vehicles.PictureFile.CopyToAsync(stream);
                           }

                           vehicles.Picture = fileName; // Save the file name in the Picture property
                       }
                   }

                   _context.Add(vehicles);
                   await _context.SaveChangesAsync();
                   return RedirectToAction(nameof(Index));
               }
               else
               {
                   // Handle the case where the session does not have a username, possibly due to session expiration or not being logged in
                   // Redirect to login page or show an error message
                   return View(vehicles);
               } 

           }


   */


        //**************************************************************************************************//
        /*  [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Create([Bind("Id,UserName,Make,Year,BodyType,Color,Picture")] Vehicles vehicles)
          {

              if (ModelState.IsValid)
              {
                  if (vehicles.PictureFile != null && vehicles.PictureFile.Length > 0)
                  {
                      var fileName = Guid.NewGuid().ToString() + Path.GetExtension(vehicles.PictureFile.FileName);
                      var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                      using (var stream = new FileStream(filePath, FileMode.Create))
                      {
                          vehicles.PictureFile.CopyTo(stream); // Using CopyTo instead of CopyToAsync
                      }

                      vehicles.Picture = fileName; // Save the file name in the Picture property
                  }

                  _context.Add(vehicles);
                  await _context.SaveChangesAsync();
                  return RedirectToAction(nameof(Index));
              }

              return View(vehicles);
          }*/

        //**************************************************************************************************//

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Make,Year,BodyType,Color,Picture")] Vehicles vehicles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(vehicles);
        }
        //**************************************************************************************************//


































        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicle.FindAsync(id);
            if (vehicles == null)
            {
                return NotFound();
            }
            return View(vehicles);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Make,Year,BodyType,Color,Picture")] Vehicles vehicles)
        {
            if (id != vehicles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehiclesExists(vehicles.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicles);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicles = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicles == null)
            {
                return NotFound();
            }

            return View(vehicles);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicles = await _context.Vehicle.FindAsync(id);
            if (vehicles != null)
            {
                _context.Vehicle.Remove(vehicles);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VehiclesExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
