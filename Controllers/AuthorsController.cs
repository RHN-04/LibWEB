using LibWEB.Data;
using LibWEB.Models;
using LibWEB.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibWEB.Controllers
{
    [Authorize(Roles = "библиотекарь")]
    public class AuthorsController : Controller
	{
		private readonly ILogger<AuthorsController> _logger;
		private readonly LibContext _context;

		public AuthorsController (LibContext context)
		{
			_context = context;
		}

        public async Task<IActionResult> Index()
        {
            var authorsViewModel = await _context.Authors
                .Include(a => a.CountryNavigation)
                .Select(a => new AuthorViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Surname = a.Surname,
                    Patronymic = a.Patronymic,
                    CountryName = a.CountryNavigation.CountryName
                })
                .ToListAsync();

            return View(authorsViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateAuthorViewModel
            {
                Countries = _context.Countries.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorViewModel model)
        {
            _logger?.LogInformation("Create method called");

            if (ModelState.IsValid)
            {
                var newAuthor = new Author
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Patronymic = model.Patronymic
                };

                if (model.Country != 0)
                {
                    newAuthor.Country = model.Country;
                }
                else if (!string.IsNullOrEmpty(model.NewCountry))
                {
                    var existingCountry = _context.Countries.FirstOrDefault(c => c.CountryName == model.NewCountry);

                    if (existingCountry != null)
                    {
                        newAuthor.Country = existingCountry.Id;
                    }
                    else
                    {
                        var newCountry = new Country { CountryName = model.NewCountry };
                        _context.Countries.Add(newCountry);
                        await _context.SaveChangesAsync();
                        newAuthor.Country = newCountry.Id;
                    }
                }

                _context.Authors.Add(newAuthor);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            model.Countries = _context.Countries.ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var viewModel = new UpdateAuthorViewModel
            {
                Id = author.Id,
                Name = author.Name,
                Surname = author.Surname,
                Patronymic = author.Patronymic,
                Country = author.Country, 
                Countries = await _context.Countries.ToListAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var author = await _context.Authors.FindAsync(model.Id);

                if (author == null)
                {
                    return NotFound();
                }

                author.Name = model.Name;
                author.Surname = model.Surname;
                author.Patronymic = model.Patronymic;
                author.Country = model.Country;

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            model.Countries = await _context.Countries.ToListAsync();
            return View(model);
        }

    }
}
