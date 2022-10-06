using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Biblioteca.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Biblioteca.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly appDBcontext _context;
        private readonly IWebHostEnvironment env;

        public AuthorsController(appDBcontext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _context.authors.ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.authors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,fullName,lastName,biography,photo")] Author author)
        {
            if (ModelState.IsValid)
            {
                var archive = HttpContext.Request.Form.Files;
                if (archive != null && archive.Count > 0)
                {
                    var archivePhoto = archive[0];
                    var pathDestiny = Path.Combine(env.WebRootPath, "assets/authors");
                    if (archivePhoto.Length > 0)
                    {
                        var archiveDestiny = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivePhoto.FileName);

                        using (var filestream = new FileStream(Path.Combine(pathDestiny, archiveDestiny), FileMode.Create))
                        {
                            archivePhoto.CopyTo(filestream);
                            author.photo = archiveDestiny;
                        };

                    }
                }
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,fullName,lastName,biography,photo")] Author author)
        {
            if (id != author.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var archive = HttpContext.Request.Form.Files;
                    if (archive != null && archive.Count > 0)
                    {
                        var archivePhoto = archive[0];
                        var pathDestiny = Path.Combine(env.WebRootPath, "assets/authors");
                        if (archivePhoto.Length > 0)
                        {
                            var archiveDestiny = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivePhoto.FileName);

                            if (!string.IsNullOrEmpty(author.photo))
                            {
                                string previousPhoto = Path.Combine(pathDestiny, author.photo);
                                if (System.IO.File.Exists(previousPhoto))
                                    System.IO.File.Delete(previousPhoto);
                            }

                            using (var filestream = new FileStream(Path.Combine(pathDestiny, archiveDestiny), FileMode.Create))
                            {
                                archivePhoto.CopyTo(filestream);
                                author.photo = archiveDestiny;
                            }
                        }
                    }

                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.ID))
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
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _context.authors
                .FirstOrDefaultAsync(m => m.ID == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.authors.FindAsync(id);
            _context.authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _context.authors.Any(e => e.ID == id);
        }
    }
}
