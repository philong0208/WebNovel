using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebNovel.Models;
using WebNovel.Models.ViewModels;

namespace WebNovel.Controllers
{
    public class UsersController : Controller
    {
        private readonly WebNovelContext _context;

        public UsersController(WebNovelContext context)
        {
            _context = context;
        }

        // GET: Users
        [Authorize(Roles ="1")]
        public async Task<IActionResult> Index()
        {
            var webNovelContext = _context.Users.Include(u => u.Role);
            return View(await webNovelContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        [Authorize(Roles = "1")]
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
            CreateUserViewModel userVM = new CreateUserViewModel();
            return View(userVM);
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel userVM)
        {
            User user = new User();
            if (ModelState.IsValid)
            {
                if(UserExists(userVM.UserID))
                {
                    user.UserId = userVM.UserID;
                    user.UserEmail = userVM.Email;
                    user.UserName = userVM.UserName;
                    user.UserPassword = CreateMD5("1234");
                    user.RoleId = userVM.RoleID;

                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Thêm người dung mới thành công";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["AddUserError"] = "ID đã tồn tại, vui lòng chọn ID khác!";
                }
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", user.RoleId);
            TempData["error"] = "Thêm người dùng thất bại";
            return View(user);
        }

        // GET: Users/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            var userVM = new EditUserViewModel();
            userVM.UserID = user.UserId;
            userVM.UserName = user.UserName;
            userVM.oldPassword = "";
            userVM.Email = user.UserEmail;
            userVM.RoleID = user.RoleId;
            if (user == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", user.RoleId);
            return View(userVM);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserViewModel userVM)
        {
            if (id != userVM.UserID)
            {
                return NotFound();
            }

            User user = new User();
            user = _context.Users.Single(n => n.UserId == userVM.UserID);
            if (ModelState.IsValid)
            {
                var oldPassword = CreateMD5(userVM.oldPassword);
                var newPassword = CreateMD5(userVM.newPassword);

                if(!user.UserPassword.Equals(oldPassword))
                {
                    TempData["error"] = "Sai mật khẩu cũ";
                    return View(userVM);
                }    
                
                user.UserName = userVM.UserName;
                user.UserEmail = userVM.Email;
                user.UserPassword = newPassword;

                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Sửa thông tin người dùng thành công";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = user.UserId });
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", user.RoleId);
            TempData["error"] = "Sửa thông tin người dùng thất bại";
            return View(userVM);
        }

        [Authorize(Roles = "1")]
        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'WebNovelContext.Users'  is null.");
            }
            var user = _context.Users.Include("Novels").Single(u => u.UserId == id);
            {
                if (user.Novels.Count > 0)
                {
                    TempData["error"] = "Không thể xóa vì người dùng " + user.UserId + " đã có ít nhất 1 tiểu thuyết!";
                    return RedirectToAction(nameof(Index));
                }
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            TempData["success"] = "Xóa người dùng thành công";
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
          return _context.Users.Any(e => e.UserId == id);
        }

        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }
    }
}
