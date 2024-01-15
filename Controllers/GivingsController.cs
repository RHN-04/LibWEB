using LibWEB.Data;
using LibWEB.Models;
using LibWEB.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibWEB.Controllers
{

    public class GivingsController : Controller
    {
        private readonly LibContext _context;

        public GivingsController(LibContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "библиотекарь")]
        public IActionResult Create(int bookId)
        {
            var books = _context.PrintPublishings.ToList();
            var readers = _context.Readers.ToList() ?? new List<Reader>();

            var readersSelectList = new SelectList(readers.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = $"{r.Surname} {r.Name} {r.Patronymic}"
            }), "Value", "Text");

            var selectedBook = books.FirstOrDefault(b => b.Id == bookId);
            var bookName = selectedBook != null ? selectedBook.Name : "Название не найдено";

            var viewModel = new CreateGivingViewModel
            {
                Readers = readersSelectList,
                GivingDate = DateTime.Now,
                ReceivingDeadlineDate = DateTime.Now.AddDays(14),
                BookId = bookId,
                BookName = bookName
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "библиотекарь")]
        public IActionResult Create(CreateGivingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var book = _context.PrintPublishings.Find(viewModel.BookId);
                var reader = _context.Readers.Find(viewModel.ReaderId);

                if (book != null && book.Numbers > 0 && reader != null)
                {
                    var readerAge = CalculateAge(reader.DateOfBirth);
                    if (readerAge < book.AgeRestriction)
                    {
                        TempData["ErrorMessage"] = "Невозможно выдать книгу, так как она не соответствует возрастному ограничению читателя.";
                        viewModel.Readers = GetReadersSelectList();
                        return View(viewModel);
                    }

                    var giving = new Giving
                    {
                        GivingDate = viewModel.GivingDate,
                        Reader = viewModel.ReaderId,
                        PrintPublishing = viewModel.BookId,
                        ReceivingDeadlineDate = viewModel.ReceivingDeadlineDate
                    };

                    _context.Givings.Add(giving);
                    _context.SaveChanges();

                    book.Numbers--;
                    _context.Update(book);
                    _context.SaveChanges();

                    TempData["SuccessMessage"] = "Книга успешно выдана читателю.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Невозможно выдать книгу, так как она отсутствует в наличии или читатель не найден.";
                }
            }
            viewModel.Readers = GetReadersSelectList();
            return View(viewModel);
        }

        private SelectList GetReadersSelectList()
        {
            var readers = _context.Readers.ToList();
            return new SelectList(readers.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = $"{r.Surname} {r.Name} {r.Patronymic}"
            }), "Value", "Text");
        }


        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }

        [Authorize]
        public IActionResult Index()
        {
            List<Giving> givings;

            if (User.IsInRole("библиотекарь"))
            {
                givings = _context.Givings.Include(g => g.ReaderNavigation).Include(g => g.PrintPublishingNavigation).ToList();
            }
            else if (User.IsInRole("читатель"))
            {
                string userEmail = User.Identity.Name;
                givings = _context.Givings.Include(g => g.ReaderNavigation).Include(g => g.PrintPublishingNavigation)
                    .Where(g => g.ReaderNavigation.EmailAddress == userEmail).ToList();
            }
            else
            {
                return Forbid();
            }

            var viewModel = new List<GivingViewModel>();

            foreach (var giving in givings)
            {
                var givingViewModel = new GivingViewModel
                {
                    Id = giving.Id,
                    BookId = giving.PrintPublishing,
                    BookTitle = giving.PrintPublishingNavigation.Name,
                    ReaderId = giving.Reader,
                    ReaderFullName = giving.ReaderNavigation.GetFullName(),
                    Readers = new SelectList(_context.Readers, "Id", "FullName"),
                    ReceivingDeadlineDate = giving.ReceivingDeadlineDate,
                    ReceivingDate = giving.ReceivingDate,
                };
                viewModel.Add(givingViewModel);
            }

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "библиотекарь")]
        public IActionResult Update(int id)
        {
            var giving = _context.Givings
                .Include(g => g.PrintPublishingNavigation)
                .Include(g => g.ReaderNavigation)
                .FirstOrDefault(g => g.Id == id);

            if (giving == null)
            {
                return NotFound();
            }

            var updateGivingViewModel = new UpdateGivingViewModel
            {
                Id = giving.Id,
                BookId = giving.PrintPublishing,
                BookName = giving.PrintPublishingNavigation.Name,
                ReaderId = giving.Reader,
                ReaderFullName = $"{giving.ReaderNavigation.Surname} {giving.ReaderNavigation.Name} {giving.ReaderNavigation.Patronymic}",
                GivingDate = giving.GivingDate,
                ReceivingDate = giving.ReceivingDate,
                ReceivingDeadlineDate = giving.ReceivingDeadlineDate,
            };


            return View(updateGivingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "библиотекарь")]
        public IActionResult Update(int id, UpdateGivingViewModel viewModel)
        {
            var giving = _context.Givings
                .Include(g => g.PrintPublishingNavigation)
                .FirstOrDefault(g => g.Id == id);

            if (giving == null)
            {
                return NotFound();
            }

            if (!giving.ReceivingDate.HasValue && viewModel.ReceivingDate.HasValue)
            {
                if (viewModel.ReceivingDate <= DateTime.Today && viewModel.ReceivingDate >= giving.GivingDate)
                {
                    giving.ReceivingDate = viewModel.ReceivingDate;

                    var book = _context.PrintPublishings.FirstOrDefault(b => b.Id == giving.PrintPublishing);
                    if (book != null)
                    {
                        book.Numbers += 1;
                    }
                }
                else
                {
                    ModelState.AddModelError("ReceivingDate", "Неверное значение для ReceivingDate");
                    TempData["ErrorMessage"] = "Невозможно обновить выдачу с установленной датой возврата.";
                    return View(viewModel);
                }
            }

            giving.ReceivingDeadlineDate = viewModel.ReceivingDeadlineDate;
            _context.SaveChanges();

            TempData["SuccessMessage"] = "Выдача успешно обновлена.";
            return RedirectToAction(nameof(Index));
        }
    }
}
