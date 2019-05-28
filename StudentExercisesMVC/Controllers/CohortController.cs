using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StudentExercisesMVC.Models;
using StudentExercisesMVC.Repositories;

namespace StudentExercisesMVC.Controllers
{
    public class CohortController : Controller
    {
        private readonly IConfiguration _config;

        public CohortController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Cohort
        public ActionResult Index()
        {
            var cohorts = CohortsRepository.GetCohorts();
            return View(cohorts);
        }

        // GET: Cohort/Details/5
        public ActionResult Details(int id)
        {
            var cohort = CohortsRepository.GetCohort(id);
            return View(cohort);
        }

        // GET: Cohort/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cohort/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([FromForm] Cohort cohort)
        {
            try
            {
                // TODO: Add insert logic here
                CohortsRepository.CreateCohort(cohort);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cohort/Edit/5
        public ActionResult Edit(int id)
        {
            var cohort = CohortsRepository.GetCohort(id);
            return View(cohort);
        }

        // POST: Cohort/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Cohort cohort)
        {
            try
            {
                // TODO: Add update logic here
                CohortsRepository.EditCohort(id, cohort);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Cohort/Delete/5
        public ActionResult Delete(int id)
        {
            var cohort = CohortsRepository.GetCohort(id);
            return View(cohort);
        }

        // POST: Cohort/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Cohort cohort)
        {
            try
            {
                // TODO: Add delete logic here
                CohortsRepository.DeleteCohort(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}