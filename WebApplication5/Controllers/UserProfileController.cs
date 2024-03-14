using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication5.Data;

namespace WebApplication5.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly DbcoursesContext _context;

        public UserProfileController(UserManager<User> userManager, DbcoursesContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Отримання фотографії за ідентифікатором PhotoUrlId
            var photo = await _context.Photos.FindAsync(user.PhotoUrlID);
            if (photo != null)
            {
                // Передача PhotoUrl у представлення
                ViewData["PhotoUrl"] = photo.PhotoUrl;
            }
            else
            {
                ViewData["PhotoUrl"] = null; // Якщо фотографія не знайдена
            }

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Передача користувача у представлення для редагування
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                // Оновлення даних користувача
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                // інші поля для оновлення

                // Збереження змін у базі даних
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // Якщо виникла помилка перевірки валідації, повертаємо користувачу сторінку редагування з повідомленнями про помилки
            return View(model);
        }

    }
}
