using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestFileStream.Models;
using TestFileStream.Utility;

namespace TestFileStream.Controllers
{
    public class WordDocumentsController : Controller
    {
        private readonly PDFDocContext _context;

        public WordDocumentsController(PDFDocContext context)
        {
            _context = context;
        }

        // GET: WordDocuments
        public async Task<IActionResult> Index(string searchString)
        {
            IList<WordDocument> results;
            if(searchString == null)
            {
                results = await _context.WordDocument.ToListAsync();
            }
            else
            {
                results = await _context.WordDocument.FromSql("FTSearch @p0", searchString).ToListAsync();
            }
            return View(results);
        }

        // GET: WordDocuments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordDocument = await _context.WordDocument
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (wordDocument == null)
            {
                return NotFound();
            }
            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = wordDocument.Title + wordDocument.FileType,
                Inline = true
            };
            Response.Headers.Add("Content-Disposition", cd.ToString());
            return File(wordDocument.Content, MIMEType.GetMimeType(wordDocument.FileType));
        }

        // GET: WordDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WordDocuments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file)
        {
            var fileName = file.FileName.Split(".");
            byte[] fileContent;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileContent = memoryStream.ToArray();
            }
            var doc = new WordDocument() {
                Guid = Guid.NewGuid(),
                Title = fileName[0],
                FileType = "." + fileName[1],
                Content = fileContent
            };
            await _context.WordDocument.AddAsync(doc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: WordDocuments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordDocument = await _context.WordDocument.FindAsync(id);
            if (wordDocument == null)
            {
                return NotFound();
            }
            return View(wordDocument);
        }

        // POST: WordDocuments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Guid")] WordDocument wordDocument, IFormFile file)
        {
            if (id != wordDocument.Guid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var fileName = file.FileName.Split(".");
                    byte[] fileContent;
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        fileContent = memoryStream.ToArray();
                    }
                    wordDocument.Title = fileName[0];
                    wordDocument.FileType = "." + fileName[1];
                    wordDocument.Content = fileContent;
                    _context.Update(wordDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WordDocumentExists(wordDocument.Guid))
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
            return View(wordDocument);
        }

        // GET: WordDocuments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wordDocument = await _context.WordDocument
                .FirstOrDefaultAsync(m => m.Guid == id);
            if (wordDocument == null)
            {
                return NotFound();
            }

            return View(wordDocument);
        }

        // POST: WordDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var wordDocument = await _context.WordDocument.FindAsync(id);
            _context.WordDocument.Remove(wordDocument);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WordDocumentExists(Guid id)
        {
            return _context.WordDocument.Any(e => e.Guid == id);
        }
    }
}
