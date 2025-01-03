﻿/*using Microsoft.AspNetCore.Mvc;

namespace SocialMediaAppAna.Controllers
{
    public class GroupsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}*/
using SocialMediaAppAna.Data;
using SocialMediaAppAna.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using AspNetCoreGeneratedDocument;


namespace SocialMediaApp.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public GroupsController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        //metode

        //Toti utilizatorii pot vedea toate grupurile existente 
        // Buton cu join - nu faci parte din grup
        // Buton cu vizualizare - faci parte din grup

        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Index()
        {
            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
                ViewBag.Alert = TempData["messageType"];
            }

            var groups = from Group in db.Groups.Include("User")
                         select Group;

            ViewBag.Groups = groups;

            var currentUser = await _userManager.GetUserAsync(User);
            ViewBag.UserCurent = currentUser;


            return View();
        }

        //Afisarea grupului cu tot cu mesaje 
        // daca faci parte din grup





        //formularul in care se completeaza datele unui nou grup
        [Authorize(Roles = "User,Admin")]
        public IActionResult New()
        {
            return View();
        }

        //adaugare in bd
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult New(Group gr)
        {
            //moderatorul este cel care creaza grupul
            gr.UserId = _userManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                db.Groups.Add(gr);
                db.SaveChanges();
                TempData["message"] = "Grupul a fost adaugat";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                return View(gr);
            }
        }

        // adminul poate sterge orice grup
        // utilizatorul doar grupurile create de el
        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Delete(int id)
        {
            Group group = db.Groups.Where(group => group.Id == id)
                                .First();

            if ((group.UserId == _userManager.GetUserId(User)) || User.IsInRole("Admin"))
            {
                db.Groups.Remove(group);
                db.SaveChanges();
                TempData["message"] = "Grupul a fost sters";
                TempData["messageType"] = "alert-success";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["message"] = "Nu aveti dreptul sa stergeti un grup care nu va apartine";
                TempData["messageType"] = "alert-danger";
                return RedirectToAction("Index");
            }

        }


    }
}
