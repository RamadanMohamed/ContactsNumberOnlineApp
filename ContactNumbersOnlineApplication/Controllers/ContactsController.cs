using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ContactNumbersOnlineApplication.Models;
using ContactNumbersOnlineApplication;
using ContactNumbersOnlineApplication.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using ContactNumbersOnlineApplication.SignalR;

namespace ContactApp.Controllers
{
    public class ContactsController : Controller
    {
        private readonly IContactRepository _contactRepository;
        private readonly IContactLockHub _hubContext;

        public ContactsController(IContactRepository contactRepository, IContactLockHub hubContext)
        {
            _contactRepository = contactRepository;
            _hubContext = hubContext;
        }

        // GET: Contacts
        public async Task<IActionResult> Index(string searchString, int? pageNumber, int pageSize = 5)
        {
            int currentPageNumber = pageNumber ?? 1;
            var contacts = await _contactRepository.GetPaginatedAsync(currentPageNumber, pageSize, searchString);
            return View(contacts);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // GET: Contacts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contacts/Create    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Address,Notes,LockedByUserId,IsLocked")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                await _contactRepository.AddAsync(contact);
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            var lockSuccess = await _contactRepository.LockContactAsync(id.Value);

            if (!lockSuccess)
            {
                // Notify other clients that the contact has been locked
                await _hubContext.LockContact(contact.Id);
                TempData["SuccessMessage"] = "This contact is currently being edited by another user.";
                return RedirectToAction(nameof(Index));
            }

            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var contact = await _contactRepository.GetByIdAsync(model.Id);
                    if (contact == null)
                    {
                        return NotFound();
                    }

                    // Update the contact in your repository
                    await _contactRepository.UpdateAsync(contact); 

                    // Unlock the contact after update
                    await _contactRepository.UnlockContactAsync(model.Id);

                    TempData["SuccessMessage"] = "Contact updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
               
                    ModelState.AddModelError("", "An error occurred while updating the contact.");
                }
            }
        
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _contactRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }

}
