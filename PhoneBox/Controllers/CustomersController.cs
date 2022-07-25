using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBox.Entities;
using PhoneBox.Models;
using PhoneBox.Repositories.Abstracts;
using System.Security.Claims;

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
        [Authorize(Policy = "GetAllCustomers")]
        public IActionResult GetAll()
        {
            var result = _customerRepository.GetAll();
            return View(result);
        }

        [HttpGet]
        [Authorize(Policy = "AddCustomer")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "AddCustomer")]
        public async Task<IActionResult> Add(AddCustomerVM model)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.AddAsync(new()
                {
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
        [Authorize(Policy = "UpdateCustomer")]
        public IActionResult Update(int id)
        {
            Customer? customer = _customerRepository.Get(x => x.Id == id);
            if (customer != null)
                return View(customer);
            else
                throw new Exception("Belirtilen id ile eşleşen bir müşteri bulunamadı.");
        }

        [HttpPost]
        [Authorize(Policy = "UpdateCustomer")]
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
        [Authorize(Policy = "DeleteCustomer")]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerRepository.DeleteAsync(id);
            return RedirectToAction("GetAll");
        }
    }
}
