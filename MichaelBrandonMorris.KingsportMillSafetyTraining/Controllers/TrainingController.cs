﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data;
using MichaelBrandonMorris.KingsportMillSafetyTraining.Models.Data.ViewModels;
using Microsoft.AspNet.Identity;
using MoreLinq;

namespace MichaelBrandonMorris.KingsportMillSafetyTraining.Controllers
{
    [Authorize]
    public class TrainingController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            var role = GetCurrentUserRole();

            if (role == null)
            {
                return RedirectToAction("SelectRole");
            }

            var model = _db.GetSlideshowViewModel(role);
            return View(model);
        }

        private Role GetCurrentUserRole()
        {
            var user = _db.GetUser(User.Identity.GetUserId());

            if (user == null)
            {
                throw new Exception();
            }

            return user.Role;
        }

        [HttpGet]
        public ActionResult Quiz()
        {
            var role = GetCurrentUserRole();
            var model = _db.GetQuizViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Quiz(IList<QuizSlideViewModel> model)
        {
            // TODO
            return null;
        }


        [HttpGet]
        public ActionResult SelectRole()
        {
            var roles = _db.GetRoles(x => x.Index);
            roles = roles.Take(roles.Count - 1).ToList();
            return View(roles);
        }

        [HttpPost]
        public ActionResult SelectRole(int? roleId)
        {
            var user = _db.GetUser(User.Identity.GetUserId());

            user.Role = roleId == null
                ? _db.GetRoles().MaxBy(x => x.Index)
                : _db.GetRole(roleId.Value);

            _db.Edit(user);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}