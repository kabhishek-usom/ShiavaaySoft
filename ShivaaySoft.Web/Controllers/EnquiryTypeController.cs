using Microsoft.AspNetCore.Mvc;
using ShivaaySoft.Entities;
using ShivaaySoft.Repositories;
using ShivaaySoft.Repositories.Interfaces;
using ShivaaySoft.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShivaaySoft.Web.Controllers
{
    public class EnquiryTypeController : Controller
    {
        private readonly IEnquiryTypeRepository _enquiryTypeRepository;
        public EnquiryTypeController(IEnquiryTypeRepository enquiryTypeRepository)
        {
            _enquiryTypeRepository = enquiryTypeRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(EnquiryTypeViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EnquiryTypeEntity type = new EnquiryTypeEntity
                    {
                        Title = vm.Title,
                    };
                    await _enquiryTypeRepository.AddAsync(type);
                    TempData["message"] = $"Enquiry Type added successfully";
                    return RedirectToAction("Index");
                }
                return View(vm);
            }
            catch (Exception ex)
            {
                TempData["message"] = $"{ex} data contains some invald items";
                throw ex;
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
            var result = _enquiryTypeRepository.GetAll();

            // if there is any search value, filter results
            if (!string.IsNullOrEmpty(searchValue))
            {
                result = result.Where(m => m.Title.ToLower().Contains(searchValue.ToLower()));
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
                EnquiryTypeEntity result = _enquiryTypeRepository.GetFirstOrDefault(a => a.Id.Equals(id));
                if (result == null)
                {
                    return RedirectToAction("Index");
                }
                EnquiryTypeViewModel vm = new EnquiryTypeViewModel
                {
                    Title = result.Title,
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
        public async Task<IActionResult> Edit(EnquiryTypeViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EnquiryTypeEntity result = _enquiryTypeRepository.GetFirstOrDefault(a => a.Id.Equals(vm.Id)); // equals Returns a value indicating whether this instance is equal to a specified object.
                    if (result == null)
                    {
                        return RedirectToAction("Index");
                    }
                    result.Title = vm.Title;
                    await _enquiryTypeRepository.UpdateAsync(result);
                    TempData["message"] = $"Type Updated successfully";
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
                EnquiryTypeEntity result = _enquiryTypeRepository.GetFirstOrDefault(a => a.Id.Equals(id));
                if (result == null)
                {
                    TempData["message"] = $"Null Record is invalid";
                }
                _enquiryTypeRepository.RemoveAsync(result);
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
