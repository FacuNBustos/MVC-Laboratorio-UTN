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
    public class BooksController : Controller
    {
        private readonly appDBcontext _context;
        private readonly IWebHostEnvironment env;

        public BooksController(appDBcontext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _context.books.Include(a => a.gender)
                .Include(b => b.author).ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.books
                .Include(a => a.gender)
                .Include(b => b.author)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["genderList"] = new SelectList(_context.genderes, "ID", "description");
            ViewData["authorList"] = new SelectList(_context.authors, "ID", "fullName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,title,resume,publicationDate,photo,authorID,genderID")] Book book)
        {
            if (ModelState.IsValid)
            {
                var archive = HttpContext.Request.Form.Files;
                if (archive != null && archive.Count > 0)
                {
                    var archivePhoto = archive[0];
                    var pathDestiny = Path.Combine(env.WebRootPath, "assets/books");
                    if (archivePhoto.Length > 0)
                    {
                        var archiveDestiny = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivePhoto.FileName);

                        using (var filestream = new FileStream(Path.Combine(pathDestiny, archiveDestiny), FileMode.Create))
                        {
                            archivePhoto.CopyTo(filestream);
                            book.photo = archiveDestiny;
                        };

                    }
                }
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["genderID"] = new SelectList(_context.genderes, "ID", "description", book.genderID);
            ViewData["authorID"] = new SelectList(_context.authors, "ID", "fullName", book.authorID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,title,resume,publicationDate,photo,authorID,genderID")] Book book)
        {
            if (id != book.ID)
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
                        var pathDestiny = Path.Combine(env.WebRootPath, "assets/books");
                        if (archivePhoto.Length > 0)
                        {
                            var archiveDestiny = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(archivePhoto.FileName);

                            if (!string.IsNullOrEmpty(book.photo))
                            {
                                string previousPhoto = Path.Combine(pathDestiny, book.photo);
                                if (System.IO.File.Exists(previousPhoto))
                                    System.IO.File.Delete(previousPhoto);
                            }

                            using (var filestream = new FileStream(Path.Combine(pathDestiny, archiveDestiny), FileMode.Create))
                            {
                                archivePhoto.CopyTo(filestream);
                                book.photo = archiveDestiny;
                            }
                        }
                    }
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.ID))
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
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.books
                .FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.books.FindAsync(id);
            _context.books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.books.Any(e => e.ID == id);
        }
    }
}
