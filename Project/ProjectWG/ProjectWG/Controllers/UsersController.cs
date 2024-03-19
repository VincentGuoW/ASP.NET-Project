using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectWG.Models;

namespace ProjectWG.Controllers
{
    public class UsersController : Controller
    {
        private readonly UsedVehicleContext _context;

        public UsersController(UsedVehicleContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }


        //GET LOGIN******************************************************
        public IActionResult Login()
        {
            return View();
        }
        //Post Login***************************************************************************

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Users users)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.User
                    .FirstOrDefaultAsync(m => m.Username == users.Username && m.Password == users.Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("Username", user.Username);
                    //HttpContext.Session.SetString("Email", users.Email);
                    return RedirectToAction("Welcome");
                }
                else
                {
                    ViewBag.ErrorMessage = "Wrong username/password";
                    /*return RedirectToAction("Login");*/
                }
            }
            return View(users);
        }
        //GET Welcome******************************************************
        
 
        public IActionResult Welcome()
        {

            var username = HttpContext.Session.GetString("Username");
            ViewBag.Username = username;
            return View();
        }



        //***************************************************************************

        // GET: Users/Create
        public IActionResult Create()
        {
           
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password")] Users users)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.User.FirstOrDefaultAsync(m => m.Username == users.Username);
                /*ViewBag.ErrorMessage = "";*/
                //make sure there won't be same username!!
                if (user == null)
                {
                    _context.Add(users);
                    await _context.SaveChangesAsync();
                    /*return RedirectToAction(nameof(Index));*/
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    /* return RedirectToAction(nameof(Welcome));*/
                    ViewBag.ErrorMessage="Username already used, please choose a different username.";
                   

                }
              
            }
            return View(users);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.User.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password")] Users users)
        {
            if (id != users.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.Id))
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
            return View(users);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.User.FindAsync(id);
            if (users != null)
            {
                _context.User.Remove(users);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
