using Microsoft.AspNetCore.Mvc;
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
            IQueryable<PhoneNumber> result = _phoneNumberRepository.GetAll();
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
                bool result = await _phoneNumberRepository.AddAsync(new() { AppUserId = model.UserId, Number = model.Number, CreatedTime = DateTime.Now });
                if (result)
                    return RedirectToAction("GetAll");
                else

                    throw new Exception("Ekleme işlemi esnasında bir hata meydana geldi");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            PhoneNumber phoneNumber = _phoneNumberRepository.Get(x => x.Id == id);
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
                bool result = _phoneNumberRepository.Update(model);
                if (result)
                    return RedirectToAction("GetAll");
                else
                    throw new Exception("Güncelleme işlemi esnasında bir hata meydana geldi");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _phoneNumberRepository.DeleteAsync(id);
            if (result)
                return RedirectToAction("GetAll");
            else
                throw new Exception("Belirtilen id ile eşleşen bir telefon kaydı bulunamadı.");
        }
    }
}
