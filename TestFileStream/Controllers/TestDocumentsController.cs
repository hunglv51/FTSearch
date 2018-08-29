using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestFileStream.Models;

namespace TestFileStream.Controllers
{
    public class TestDocumentsController : Controller
    {
        private readonly PDFDocContext _context;

        public TestDocumentsController(PDFDocContext context)
        {
            _context = context;
        }

        // GET: TestDocuments
        public async Task<IActionResult> Index()
        {
            return View(await _context.TestDocument.ToListAsync());
        }

        // GET: TestDocuments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDocument = await _context.TestDocument
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (testDocument == null)
            {
                return NotFound();
            }

            return View(testDocument);
        }

        // GET: TestDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TestDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Guid,Title,FileType")] TestDocument testDocument)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testDocument);
        }

        // GET: TestDocuments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDocument = await _context.TestDocument.FindAsync(id);
            if (testDocument == null)
            {
                return NotFound();
            }
            return View(testDocument);
        }

        // POST: TestDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Guid,Title,FileType")] TestDocument testDocument)
        {
            if (id != testDocument.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestDocumentExists(testDocument.Guid))
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
            return View(testDocument);
        }

        // GET: TestDocuments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testDocument = await _context.TestDocument
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (testDocument == null)
            {
                return NotFound();
            }

            return View(testDocument);
        }

        // POST: TestDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var testDocument = await _context.TestDocument.FindAsync(id);
            _context.TestDocument.Remove(testDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestDocumentExists(int id)
        {
            return _context.TestDocument.Any(e => e.Guid == id);
        }
    }
}
