using CMS.Interface;
using CMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class GiaoVienController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public GiaoVienController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<IActionResult> EditGiaoVien(ModelHolder model)
        {
            if (ModelState.IsValid)
            {
                await _unitofWork.GiaoVien.EditGiaoVien(model.GiaoVien);
                return RedirectToAction(nameof(GiaoVienList));
            }
            var backModel = new ModelHolder();
            backModel.GiaoVien = model.GiaoVien;
            backModel.GiaoViens = await _unitofWork.GiaoVien.GetAllGiaoVien();
            backModel.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
            TempData["NewOrEdit"] = "Edit Mode";
            return View(nameof(GiaoVienList), backModel);
        }

        public async Task<IActionResult> GiaoVienList()
        {
            var model = new ModelHolder();
            model.GiaoViens = await _unitofWork.GiaoVien.GetAllGiaoVien();
            model.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
            model.GiaoVien = new Models.GiaoVien();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGiaoVien(ModelHolder model)
        {
            if(ModelState.IsValid)
            {
                await _unitofWork.GiaoVien.CreateGiaoVien(model.GiaoVien);
                return RedirectToAction(nameof(GiaoVienList));
            }
            var modelBack = new ModelHolder();
            modelBack.GiaoVien = model.GiaoVien;
            modelBack.GiaoViens = await _unitofWork.GiaoVien.GetAllGiaoVien();
            modelBack.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
            TempData["InvalidModel"] = "Model is Invalid";
            return View(nameof(GiaoVienList), modelBack);
        }
    }
}
