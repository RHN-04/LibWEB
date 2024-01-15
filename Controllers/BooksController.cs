using LibWEB.Data;
using LibWEB.Models;
using LibWEB.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace LibWEB.Controllers
{
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly LibContext _context;

        public BooksController(LibContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var printPublishings = await _context.PrintPublishings.ToListAsync();

            var authorPrintPublishings = await _context.AuthorPrintPublishings
                .Include(ap => ap.AuthorNavigation)
                .ToListAsync();

            var genrePrintPublishings = await _context.GenrePrintPublishings
                .Include(gp => gp.GenreNavigation)
                .ToListAsync();

            var booksFromDb = printPublishings
                .GroupJoin(
                    authorPrintPublishings,
                    pp => pp.Id,
                    ap => ap.PrintPublishing,
                    (pp, apGroup) => new { PrintPublishing = pp, AuthorPrintPublishings = apGroup.ToList() }
                )
                .GroupJoin(
                    genrePrintPublishings,
                    ppAp => ppAp.PrintPublishing.Id,
                    gp => gp.PrintPublishing,
                    (ppAp, gpGroup) => new { ppAp.PrintPublishing, ppAp.AuthorPrintPublishings, GenrePrintPublishings = gpGroup.ToList() }
                )
                .ToList();

            var booksViewModels = booksFromDb.Select(book => new BookViewModel
            {
                Id = book.PrintPublishing.Id,
                Title = book.PrintPublishing.Name,
                AgeRestriction = book.PrintPublishing.AgeRestriction,
                YearOfPublishing = book.PrintPublishing.YearOfPublishing,
                Description = book.PrintPublishing.Description,
                Numbers = book.PrintPublishing.Numbers,
                ImageId = book.PrintPublishing.ImageId,
                SelectedAuthors = book.AuthorPrintPublishings
                    .Select(ap => new AuthorViewModel
                    {
                        Surname = ap?.AuthorNavigation?.Surname,
                        Name = ap?.AuthorNavigation?.Name,
                        Patronymic = ap?.AuthorNavigation?.Patronymic,
                    })
                    .ToList(),
                SelectedGenres = book.GenrePrintPublishings
                    .Select(gp => gp?.GenreNavigation?.NameGenre)
                    .ToList()
            }).ToList();

            return View(booksViewModels);
        }

        [Authorize(Roles = "библиотекарь")]
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateBookViewModel();
            model.Authors = _context.Authors.ToList();
            model.Genres = _context.Genres.ToList();
            return View(model);
        }

        [Authorize(Roles = "библиотекарь")]
        [HttpPost]
        public IActionResult Create(CreateBookViewModel model, IFormFile imageFile, string? newGenre)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                if (!IsImageFile(imageFile))
                {
                    ModelState.AddModelError("ImageFile", "Выбранный файл не является изображением. Пожалуйста, выберите файл с расширением .jpeg, .jpg или .png.");
                    model.Authors = _context.Authors.ToList();
                    model.Genres = _context.Genres.ToList();
                    return View(model);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                var imagePath = Path.Combine("wwwroot", "images", uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                model.ImageId = uniqueFileName;
            }


            if (ModelState.IsValid)
            {
                var newBook = new PrintPublishing
                {
                    Name = model.Title,
                    AgeRestriction = model.AgeRestriction,
                    YearOfPublishing = model.YearOfPublishing,
                    Description = model.Description,
                    Numbers = model.Numbers,
                    ImageId = model.ImageId
                };

                _context.PrintPublishings.Add(newBook);
                _context.SaveChanges();

                if (model.SelectedAuthorIds != null && model.SelectedAuthorIds.Any())
                {
                    foreach (var authorId in model.SelectedAuthorIds)
                    {
                        var authorBookLink = new AuthorPrintPublishing
                        {
                            Author = authorId,
                            PrintPublishing = newBook.Id
                        };

                        _context.AuthorPrintPublishings.Add(authorBookLink);
                    }

                    _context.SaveChanges();
                }

                if (model.SelectedGenreIds != null && model.SelectedGenreIds.Any())
                {
                    foreach (var genreId in model.SelectedGenreIds)
                    {
                        var genrePrintPublishingLink = new GenrePrintPublishing
                        {
                            Genre = genreId,
                            PrintPublishing = newBook.Id
                        };

                        _context.GenrePrintPublishings.Add(genrePrintPublishingLink);
                    }

                    _context.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            if (!string.IsNullOrEmpty(newGenre))
            {
                var genre = new Genre { NameGenre = newGenre };
                _context.Genres.Add(genre);
                _context.SaveChanges();
                model.SelectedGenreIds = new List<int> { genre.Id };
            }

            model.Authors = _context.Authors.ToList();
            model.Genres = _context.Genres.ToList();
            return View(model);
        }


        private bool IsImageFile(IFormFile file)
        {
            if (file.ContentType.ToLower() == "image/jpeg" ||
                file.ContentType.ToLower() == "image/png" ||
                file.ContentType.ToLower() == "image/jpg")
            {
                return true;
            }
            return false;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var printPublishing = await _context.PrintPublishings
                .Where(pp => pp.Id == id)
                .FirstOrDefaultAsync();

            if (printPublishing == null)
            {
                return NotFound();
            }

            var authorPrintPublishings = await _context.AuthorPrintPublishings
                .Include(ap => ap.AuthorNavigation)
                .Where(ap => ap.PrintPublishing == printPublishing.Id)
                .ToListAsync();

            var genrePrintPublishings = await _context.GenrePrintPublishings
                .Include(gp => gp.GenreNavigation)
                .Where(gp => gp.PrintPublishing == printPublishing.Id)
                .ToListAsync();

            var bookViewModel = new BookViewModel
            {
                Id = printPublishing.Id,
                Title = printPublishing.Name,
                AgeRestriction = printPublishing.AgeRestriction,
                YearOfPublishing = printPublishing.YearOfPublishing,
                Description = printPublishing.Description,
                Numbers = printPublishing.Numbers,
                ImageId = printPublishing.ImageId,
                SelectedAuthors = authorPrintPublishings
                    .Select(ap => new AuthorViewModel
                    {
                        Surname = ap?.AuthorNavigation?.Surname,
                        Name = ap?.AuthorNavigation?.Name,
                        Patronymic = ap?.AuthorNavigation?.Patronymic,
                    })
                    .ToList(),
                SelectedGenres = genrePrintPublishings
                    .Select(gp => gp?.GenreNavigation?.NameGenre)
                    .ToList()
            };

            return View(bookViewModel);
        }

        [Authorize(Roles = "библиотекарь")]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var bookToUpdate = await _context.PrintPublishings
                .Where(pp => pp.Id == id)
                .Select(pp => new
                {
                    PrintPublishing = pp,
                    AuthorPrintPublishings = _context.AuthorPrintPublishings
                        .Where(ap => ap.PrintPublishing == pp.Id)
                        .ToList(),
                    GenrePrintPublishings = _context.GenrePrintPublishings
                        .Where(gp => gp.PrintPublishing == pp.Id)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (bookToUpdate == null)
            {
                return NotFound();
            }

            var printPublishing = bookToUpdate.PrintPublishing;
            var authorPrintPublishings = bookToUpdate.AuthorPrintPublishings;
            var genrePrintPublishings = bookToUpdate.GenrePrintPublishings;
            var authorsForBook = _context.Authors
                .AsEnumerable()
                .Where(a => authorPrintPublishings.Any(ap => ap.AuthorNavigation != null && ap.AuthorNavigation.Id == a.Id))
                .ToList();



            var genresForBook = _context.Genres
                .AsEnumerable()
                .Where(g => genrePrintPublishings.Any(gp => gp.GenreNavigation != null && gp.GenreNavigation.Id == g.Id))
                .ToList();

            var model = new UpdateBookViewModel
            {
                Id = printPublishing.Id,
                Title = printPublishing.Name,
                AgeRestriction = printPublishing.AgeRestriction,
                YearOfPublishing = printPublishing.YearOfPublishing,
                Description = printPublishing.Description,
                Numbers = printPublishing.Numbers,
                ImageId = printPublishing.ImageId,
                SelectedAuthorIds = authorPrintPublishings?.Select(ap => ap.AuthorNavigation?.Id ?? 0).ToList() ?? new List<int>(),
                SelectedGenreIds = genrePrintPublishings?.Select(gp => gp.GenreNavigation?.Id ?? 0).ToList() ?? new List<int>(),
                Authors = _context.Authors.ToList(),
                Genres = _context.Genres.ToList()
            };

            return View(model);
        }

        [Authorize(Roles = "библиотекарь")]
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBookViewModel model, IFormFile imageFile, int id)
        {
            ModelState.Clear();

            var bookToUpdateInfo = await _context.PrintPublishings
                .FirstOrDefaultAsync(pp => pp.Id == id);

            if (bookToUpdateInfo == null)
            {
                return NotFound();
            }
            var selectedGenreIds = model.SelectedGenreIds ?? new List<int>();
            var existingGenreIds = await _context.GenrePrintPublishings
                .Where(gp => gp.PrintPublishing == bookToUpdateInfo.Id)
                .Select(gp => gp.Genre)
                .ToListAsync();

            var genresToRemove = await _context.GenrePrintPublishings
                .Where(gp => gp.PrintPublishing == bookToUpdateInfo.Id && !selectedGenreIds.Contains(gp.Genre))
                .ToListAsync();

            foreach (var genreToRemove in genresToRemove)
            {
                _context.GenrePrintPublishings.Remove(genreToRemove);
            }

            var genresToAdd = selectedGenreIds
                .Where(genreId => !existingGenreIds.Contains(genreId))
                .Select(genreId => new GenrePrintPublishing
                {
                    PrintPublishing = bookToUpdateInfo.Id,
                    Genre = genreId
                });

            foreach (var genreToAdd in genresToAdd)
            {
                _context.GenrePrintPublishings.Add(genreToAdd);
            }

            var selectedAuthorIds = model.SelectedAuthorIds ?? new List<int>();
            var existingAuthorIds = await _context.AuthorPrintPublishings
                .Where(ap => ap.PrintPublishing == bookToUpdateInfo.Id)
                .Select(ap => ap.Author.GetValueOrDefault())
                .ToListAsync();

            var authorsToRemove = await _context.AuthorPrintPublishings
                .Where(ap => ap.PrintPublishing == bookToUpdateInfo.Id && !selectedAuthorIds.Contains(ap.Author.GetValueOrDefault()))
                .ToListAsync();

            foreach (var authorToRemove in authorsToRemove)
            {
                _context.AuthorPrintPublishings.Remove(authorToRemove);
            }

            var authorsToAdd = selectedAuthorIds
                .Where(authorId => !existingAuthorIds.Contains(authorId))
                .Select(authorId => new AuthorPrintPublishing
                {
                    PrintPublishing = bookToUpdateInfo.Id,
                    Author = authorId
                });

            foreach (var authorToAdd in authorsToAdd)
            {
                _context.AuthorPrintPublishings.Add(authorToAdd);
            }

            bookToUpdateInfo.Name = model.Title;
            bookToUpdateInfo.AgeRestriction = model.AgeRestriction;
            bookToUpdateInfo.YearOfPublishing = model.YearOfPublishing;
            bookToUpdateInfo.Description = model.Description;
            bookToUpdateInfo.Numbers = model.Numbers;

            if (imageFile != null && imageFile.Length > 0)
            {
                if (!IsImageFile(imageFile))
                {
                    ModelState.AddModelError("ImageFile", "Выбранный файл не является изображением. Пожалуйста, выберите файл с расширением .jpeg, .jpg или .png.");
                    return View(model);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                var imagePath = Path.Combine("wwwroot", "images", uniqueFileName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }

                bookToUpdateInfo.ImageId = uniqueFileName;
            }

            if (!ModelState.IsValid)
            {
                model.Genres = _context.Genres.ToList();
                model.Authors = _context.Authors.ToList();
                return View(model);
            }

            _context.Entry(bookToUpdateInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "библиотекарь")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {

            var bookToDelete = _context.PrintPublishings
                .FirstOrDefault(pp => pp.Id == id);

            if (bookToDelete == null)
            {
                return NotFound();
            }

            string imagePath = Path.Combine("wwwroot/images", bookToDelete.ImageId);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            var authorPrintPublishings = _context.AuthorPrintPublishings
                .Where(ap => ap.PrintPublishing == bookToDelete.Id)
                .ToList();

            var genrePrintPublishings = _context.GenrePrintPublishings
                .Where(gp => gp.PrintPublishing == bookToDelete.Id)
                .ToList();

            _context.AuthorPrintPublishings.RemoveRange(authorPrintPublishings);
            _context.GenrePrintPublishings.RemoveRange(genrePrintPublishings);
            _context.PrintPublishings.Remove(bookToDelete);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToAction("Index");
        }

    }
}
