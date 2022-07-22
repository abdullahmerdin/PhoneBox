using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhoneBox.Entities;
using PhoneBox.Models;
using PhoneBox.Repositories.Abstracts;

namespace PhoneBox.Controllers
{
    public class PhoneNumbersController : Controller
    {
        readonly IPhoneNumberRepository _phoneNumberRepository;

        public PhoneNumbersController(IPhoneNumberRepository phoneNumberRepository)
        {
            _phoneNumberRepository = phoneNumberRepository;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPhoneNumberVM model)
        {
            if (ModelState.IsValid)
            {
                await _phoneNumberRepository.AddAsync(new() { AppUserId = model.UserId, Number = model.Number,  CreatedTime = DateTime.Now});
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
