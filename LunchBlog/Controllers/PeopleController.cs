using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LunchBlog.Models;

namespace LunchBlog.Controllers
{
    public class PeopleController : Controller
    {
        private readonly LunchBlogDBContext _context;

        public PeopleController(LunchBlogDBContext context)
        {
            _context = context;    
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var lunchBlogDBContext = _context.People.Include(p => p.Experience);
            return View(await lunchBlogDBContext.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .Include(p => p.Experience)
                .SingleOrDefaultAsync(m => m.PeopleId == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["ExperienceName"] = new SelectList(_context.Experiences, "ExperienceId", "Name");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PeopleId,Name,Description,ExperienceId")] People people)
        {
            if (ModelState.IsValid)
            {
                _context.Add(people);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ExperienceId"] = new SelectList(_context.Experiences, "ExperienceId", "ExperienceId", people.ExperienceId);
            return View(people);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People.SingleOrDefaultAsync(m => m.PeopleId == id);
            if (people == null)
            {
                return NotFound();
            }
            ViewData["ExperienceId"] = new SelectList(_context.Experiences, "ExperienceId", "ExperienceId", people.ExperienceId);
            return View(people);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PeopleId,Name,Description,ExperienceId")] People people)
        {
            if (id != people.PeopleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(people);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeopleExists(people.PeopleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ExperienceId"] = new SelectList(_context.Experiences, "ExperienceId", "ExperienceId", people.ExperienceId);
            return View(people);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var people = await _context.People
                .Include(p => p.Experience)
                .SingleOrDefaultAsync(m => m.PeopleId == id);
            if (people == null)
            {
                return NotFound();
            }

            return View(people);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var people = await _context.People.SingleOrDefaultAsync(m => m.PeopleId == id);
            _context.People.Remove(people);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PeopleExists(int id)
        {
            return _context.People.Any(e => e.PeopleId == id);
        }
    }
}
