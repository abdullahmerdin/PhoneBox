using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBox.Entities;
using PhoneBox.Models;
using PhoneBox.Repositories.Abstracts;

namespace PhoneBox.Controllers
{
    public class CustomersController : Controller
    {
        readonly ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Authorize(Policy = "GetAllPhoneNumber")]
        public IActionResult GetAll()
        {
            var result = _customerRepository.GetAll();
            return View(result);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCustomerVM model)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.AddAsync(new() { 
                    Address = model.Address,
                    CompanyName = model.CompanyName,
                    CreatedTime = DateTime.Now,
                    Email = model.Email,
                    FirstName = model.FirstName,    
                    JobTitle = model.JobTitle,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                });
                return RedirectToAction("GetAll");

            }
            throw new Exception("Ekleme işlemi esnasında bir hata meydana geldi");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Customer? customer = _customerRepository.Get(x => x.Id == id);
            if (customer != null)
                return View(customer);
            else
                throw new Exception("Belirtilen id ile eşleşen bir müşteri bulunamadı.");
        }

        [HttpPost]
        public IActionResult Update(Customer model)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Update(model);
                return RedirectToAction("GetAll");
            }
            throw new Exception("Güncelleme işlemi esnasında bir hata meydana geldi");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerRepository.DeleteAsync(id);
            return RedirectToAction("GetAll");
        }
    }
}
