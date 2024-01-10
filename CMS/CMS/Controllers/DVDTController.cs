using CMS.Interface;
using CMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class DVDTController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public DVDTController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<IActionResult> EditDVDT(ModelHolder model)
        {
            if(ModelState.IsValid)
            {
                await _unitofWork.DVDT.EditDVDT(model.DVDT);
                return RedirectToAction(nameof(DVDTList));
            }
            var backModel = new ModelHolder();
            backModel.DVDT = model.DVDT;
            backModel.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
            TempData["NewOrEdit"] = "New or Edit";
            return View(nameof(DVDTList), backModel);
        }

        public async Task<IActionResult> DVDTList()
        {
            var model = new ModelHolder();
            model.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
            model.DVDT = new Models.DonViDaoTao();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDVDT(ModelHolder model)
        {
            if (ModelState.IsValid)
            {
                await _unitofWork.DVDT.CreateDVDT(model.DVDT);
                return RedirectToAction(nameof(DVDTList));
            }
            var backModel = new ModelHolder();
            backModel.DVDT = model.DVDT;
            backModel.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
            TempData["InvalidModel"] = "Model is invalid!";
            return View(nameof(DVDTList), backModel);
        }
    }
}
