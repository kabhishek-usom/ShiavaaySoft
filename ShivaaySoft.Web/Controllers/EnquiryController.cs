using Microsoft.AspNetCore.Mvc;
using ShivaaySoft.Entities;
using ShivaaySoft.Repositories.Interfaces;
using ShivaaySoft.Web.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShivaaySoft.Web.Controllers
{
    public class EnquiryController : Controller
    {
        private readonly IEnquiryTypeRepository _enquiryTypeRepository;
        private readonly IEnquiryRepository _enquiryRepository;
        public EnquiryController(IEnquiryRepository enquiryRepository, IEnquiryTypeRepository enquiryTypeRepository)
        {
            _enquiryRepository = enquiryRepository;
            _enquiryTypeRepository = enquiryTypeRepository;
        }
                
        public IActionResult Index()
        {
            ViewBag.EnquiryType = _enquiryTypeRepository.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(EnquiryViewModel Vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                     EnquiryEntity enquiry = new EnquiryEntity
                     {
                       FirstName = Vm.FirstName,
                       LastName = Vm.LastName,
                       Dob = Vm.Dob,
                       Gender = Vm.Gender,
                       EnquiryTypeId = Vm.EnquiryTypeId,
                    };
                    await _enquiryRepository.AddAsync(enquiry);
                    TempData["message"] = $"Enquiry added successfully";
                    return RedirectToAction("Index");
                }
                TempData["message"] = $"All Field Are Required";
                return View(Vm);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"{ex} data contains some invald items";
                throw;
            }
        }


        [HttpPost]
        public IActionResult GetList()
        {
            var draw = Request.Form["draw"].FirstOrDefault(); // get total page size
            var start = Request.Form["start"].FirstOrDefault(); // get starte length size from request.
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault(); // check if there is any search characters passed
            int pageSize = length != null ? Convert.ToInt32(length) : 1;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var result = _enquiryRepository.GetAll();

            // if there is any search value, filter results
            if (!string.IsNullOrEmpty(searchValue))
            {
                result = result.Where(m => m.FirstName.ToLower().Contains(searchValue.ToLower()) 
                                        || m.LastName.ToLower().Contains(searchValue.ToLower())                                        
                                        || m.Gender.ToLower().Contains(searchValue.ToLower())
                                     );
            }
            // get total records acount
            recordsTotal = result.Count();
            //get page data
            var data = result.Skip(skip).Take(pageSize).ToList();
            var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(jsonData);
        }


        public IActionResult Edit(int id)
        {
            try
            {
                ViewBag.EnquiryType = _enquiryTypeRepository.GetAll();
                EnquiryEntity result = _enquiryRepository.GetFirstOrDefault(a => a.Id.Equals(id));
                if (result == null)
                {
                    return RedirectToAction("Index");
                }
                EnquiryViewModel vm = new EnquiryViewModel
                {
                    FirstName = result.FirstName,
                    LastName = result.LastName,
                    Dob = result.Dob,
                    Gender = result.Gender,
                    EnquiryTypeId = result.EnquiryTypeId,   
                };
                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"{ex} data contains some invald items";
                throw ex;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EnquiryViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EnquiryEntity result = _enquiryRepository.GetFirstOrDefault(a => a.Id.Equals(vm.Id)); // equals Returns a value indicating whether this instance is equal to a specified object.
                    if (result == null)
                    {
                        return RedirectToAction("Index");
                    }
                    result.FirstName = vm.FirstName;
                    result.LastName = vm.LastName;
                    result.Dob = vm.Dob;
                    result.Gender = vm.Gender;
                    result.EnquiryTypeId = vm.EnquiryTypeId;    
                    await _enquiryRepository.UpdateAsync(result);
                    TempData["message"] = $"Enquiry Updated successfully";
                    return RedirectToAction("Index");
                }
                TempData["message"] = $"Data is invalid";
                return RedirectToAction("Index");
            }
            catch (System.Exception ex)
            {
                TempData["Message"] = $"{ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                EnquiryEntity result = _enquiryRepository.GetFirstOrDefault(a => a.Id.Equals(id));
                if (result == null)
                {
                    TempData["message"] = $"Null Record is invalid";
                }
                _enquiryRepository.RemoveAsync(result);
                TempData["message"] = $"Deleted Record Successfully";
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }       
    }
}
