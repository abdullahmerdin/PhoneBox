using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhoneBox.Entities;
using PhoneBox.Entities.Identity;
using PhoneBox.Models;
using PhoneBox.Repositories.Abstracts;
using System.Collections.Generic;

namespace PhoneBox.Controllers
{
    public class PhoneNumbersController : Controller
    {
        readonly IPhoneNumberRepository _phoneNumberRepository;
        readonly UserManager<AppUser> _userManager;

        public PhoneNumbersController(IPhoneNumberRepository phoneNumberRepository, UserManager<AppUser> userManager)
        {
            _phoneNumberRepository = phoneNumberRepository;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _phoneNumberRepository.GetAll().Include(x => x.AppUser);
            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            List<SelectListItem> userList = (from x in _userManager.Users.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.FirstName + " " + x.LastName,
                                                 Value = x.Id.ToString()
                                             }).ToList();
            ViewBag.userList = userList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPhoneNumberVM model)
        {
            if (ModelState.IsValid)
            {
                await _phoneNumberRepository.AddAsync(new() { AppUserId = model.UserId, Number = model.Number, CreatedTime = DateTime.Now });
                return RedirectToAction("GetAll");

            }
            throw new Exception("Ekleme işlemi esnasında bir hata meydana geldi");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            PhoneNumber phoneNumber = _phoneNumberRepository.GetByIdWithDetails(id);
            ViewBag.appUser = phoneNumber.AppUser;
            if (phoneNumber != null)
                return View(phoneNumber);
            else
                throw new Exception("Belirtilen id ile eşleşen bir telefon kaydı bulunamadı.");

        }

        [HttpPost]
        public IActionResult Update(PhoneNumber model)
        {
            if (ModelState.IsValid)
            {
                _phoneNumberRepository.Update(model);
                return RedirectToAction("GetAll");
            }
            throw new Exception("Güncelleme işlemi esnasında bir hata meydana geldi");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _phoneNumberRepository.DeleteAsync(id);
            return RedirectToAction("GetAll");
        }
    }
}
