using CMS.Interface;
using CMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Authorize]
    public class ChungChiController : Controller
    {
        private readonly IUnitofWork _unitofWork;

        public ChungChiController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        [HttpPost]
        public async Task<IActionResult> EditChungChi(ModelHolder model)
        {
            if (ModelState.IsValid)
            {
                await _unitofWork.ChungChi.EditChungChi(model.ChungChi);
                return RedirectToAction(nameof(ChungChiList));
            }
            var backModel = new ModelHolder();
            backModel.ChungChis = await _unitofWork.ChungChi.GetAllChungChi();
            backModel.NhomChungChis = await _unitofWork.PresetTable.GetAllNhomCC();
            backModel.ChungChi = model.ChungChi;
            TempData["InvalidModel"] = "Model is Invalid!";
            TempData["NewOrEdit"] = "edit";
            return View(nameof(ChungChiList), backModel);
        }

        public async Task<IActionResult> ChungChiList()
        {
            var model = new ModelHolder();
            model.ChungChis = await _unitofWork.ChungChi.GetAllChungChi();
            model.NhomChungChis = await _unitofWork.PresetTable.GetAllNhomCC();
            model.ChungChi = new Models.ChungChi();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChungChi(ModelHolder model)
        {
            if(ModelState.IsValid)
            {
                await _unitofWork.ChungChi.CreateChungChi(model.ChungChi);
                return RedirectToAction(nameof(ChungChiList));
            }
            var backModel = new ModelHolder();
            backModel.ChungChis = await _unitofWork.ChungChi.GetAllChungChi();
            backModel.NhomChungChis = await _unitofWork.PresetTable.GetAllNhomCC();
            backModel.ChungChi = model.ChungChi;
            TempData["InvalidModel"] = "Model is Invalid!";
            return View(nameof(ChungChiList), backModel);
        }
    }
}
