using CMS.ImplementClass;
using CMS.Interface;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CMS.Controllers
{
    [Authorize]
    public class KhoaHocController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private IWebHostEnvironment _webHostEnvironment;
        private IOptions<UploadPath> _uploadPathConfig;

        public KhoaHocController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment, IOptions<UploadPath> uploadPathConfig)
        {
            _unitofWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
            _uploadPathConfig = uploadPathConfig;
        }

        [HttpPost]
        public async Task<JsonResult> DeleteNhanVienOut(int idnv, int idkh)
        {
            var capCC = await _unitofWork.NhanVien.GetCapCCById(idnv, idkh);
            if(capCC != null) {
                if(capCC.AnhChungChi != null && capCC.AnhChungChi != "")
                {
                    if(await RemoveFile(capCC.AnhChungChi))
                    {
                        await _unitofWork.NhanVien.DeleteNhanVienOutKhoaHoc(idnv, idkh);
                        return Json(true);
                    }
                    else
                    {
                        return Json(false);
                    }
                }
                else
                {
                    await _unitofWork.NhanVien.DeleteNhanVienOutKhoaHoc(idnv, idkh);
                    return Json(true);
                }
            }
            return Json(false);
        }

        [HttpPost]
        public async Task<IActionResult> EditCapCC(ModelHolder model)
        {
            if (ModelState.IsValid)
            {
                if (model.EditCapCC.isRemoveFile)
                {
                    if (await RemoveFile(model.EditCapCC.CapCC.AnhChungChi))
                    {
                        await _unitofWork.CapCC.EditCapCC(model.EditCapCC.CapCC, "");
                        return RedirectToAction(nameof(KhoaHocDetail), new { idkh = model.EditCapCC.CapCC.IdKhoaHoc });
                    }
                }
                else if (model.EditCapCC.AnhCC != null)
                {
                    string fileName = model.EditCapCC.AnhCC.FileName;
                    if (await UploadFile(model.EditCapCC.AnhCC, fileName, FolderPath.ChungChi, model.EditCapCC.CapCC.AnhChungChi))
                    {
                        await _unitofWork.CapCC.EditCapCC(model.EditCapCC.CapCC, $"/{FolderPath.ChungChi}{fileName}");
                        return RedirectToAction(nameof(KhoaHocDetail), new { idkh = model.EditCapCC.CapCC.IdKhoaHoc });
                    }
                    else
                    {
                        TempData["ErrorMess"] = "Không thể tải lên file PDF, vui lòng thử lại!";
                        TempData["InvalidEditModel"] = "Model is invalid!";
                        var capcc1 = new CreateCapCC { CapCC = new CapChungChi { IdKhoaHoc = model.EditCapCC.CapCC.IdKhoaHoc } };
                        var backModel1 = await LoadKhoaHocDetailModel(capcc1);
                        backModel1.EditCapCC = model.EditCapCC;
                        backModel1.EditCapCC.CapCC.AnhChungChi = "";
                        return View(nameof(KhoaHocDetail), backModel1);
                    }
                }
                else
                {
                    await _unitofWork.CapCC.EditCapCC(model.EditCapCC.CapCC);
                    return RedirectToAction(nameof(KhoaHocDetail), new { idkh = model.EditCapCC.CapCC.IdKhoaHoc });
                }
            }
            var capcc2 = new CreateCapCC { CapCC = new CapChungChi { IdKhoaHoc = model.EditCapCC.CapCC.IdKhoaHoc } };
            var backModel2 = await LoadKhoaHocDetailModel(capcc2);
            backModel2.EditCapCC = model.EditCapCC;
            backModel2.EditCapCC.CapCC.AnhChungChi = "";
            TempData["InvalidEditModel"] = "Model is invalid!";
            return View(nameof(KhoaHocDetail), backModel2);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCapCC(ModelHolder model)
        {
            if (ModelState.IsValid)
            {
                if (model.CreateCapCC.AnhCC != null)
                {
                    string fileName = model.CreateCapCC.AnhCC.FileName;
                    if (await UploadFile(model.CreateCapCC.AnhCC, fileName, FolderPath.ChungChi, ""))
                    {
                        await _unitofWork.CapCC.CreateCapCC(model.CreateCapCC.CapCC, $"/{FolderPath.ChungChi}{fileName}");
                        return RedirectToAction(nameof(KhoaHocDetail), new { idkh = model.CreateCapCC.CapCC.IdKhoaHoc });
                    }
                    else
                    {
                        TempData["ErrorMess"] = "Không thể tải lên file PDF, vui lòng thử lại!";
                        TempData["InvalidModel"] = "Model is invalid!";
                        var backModel1 = await LoadKhoaHocDetailModel(model.CreateCapCC);
                        return View(nameof(KhoaHocDetail), backModel1);
                    }
                }
                else
                {
                    await _unitofWork.CapCC.CreateCapCC(model.CreateCapCC.CapCC, "");
                    return RedirectToAction(nameof(KhoaHocDetail), new { idkh = model.CreateCapCC.CapCC.IdKhoaHoc });
                }
            }
            var backModel2 = await LoadKhoaHocDetailModel(model.CreateCapCC);
            return View(nameof(KhoaHocDetail), backModel2);
        }

        [HttpPost]
        public async Task<IActionResult> EditKhoaHoc(ModelHolder model)
        {
            if (ModelState.IsValid)
            {
                await _unitofWork.KhoaHoc.EditKhoaHoc(model.KhoaHoc);
                return RedirectToAction(nameof(KhoaHocDetail), new { idkh = model.KhoaHoc.Id });
            }
            var capcc = new CreateCapCC { CapCC = new CapChungChi { IdKhoaHoc = model.KhoaHoc.Id } };
            var modelBack = await LoadKhoaHocDetailModel(capcc);
            modelBack.KhoaHoc = model.KhoaHoc;
            return View(nameof(KhoaHocDetail), modelBack);
        }
        [NonAction]
        public async Task<bool> UploadFile(IFormFile file, string fileName, string folderPath, string oldPath)
        {
            try
            {
                string serverFolder = Path.Combine(_uploadPathConfig.Value.ServerDirPath, folderPath);
                if (!Directory.Exists(serverFolder))
                {
                    Directory.CreateDirectory(serverFolder);
                }
                string filePath = serverFolder + fileName;
                if (oldPath != null && oldPath != "")
                {
                    string oldFilePath = Path.Combine(_uploadPathConfig.Value.ServerDirPath, oldPath.Substring(1));

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                else
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                using (FileStream fileStream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(fileStream);
                    return true;
                }
            }
            catch (Exception ex) { }
            return false;
        }
        [NonAction]
        public async Task<bool> RemoveFile(string filePath)
        {
            try
            {
                string fileToDelete = Path.Combine(_uploadPathConfig.Value.ServerDirPath, filePath.Substring(1));

                if (System.IO.File.Exists(fileToDelete))
                {
                    System.IO.File.Delete(fileToDelete);
                }
                return true;
            }
            catch (Exception ex) { }
            return false;
        }

        [NonAction]
        public async Task<ModelHolder> LoadKhoaHocDetailModel(CreateCapCC capcc)
        {
            var model = new ModelHolder();
            model.KhoaHoc = await _unitofWork.KhoaHoc.GetKhoaHocById(capcc.CapCC.IdKhoaHoc);
            model.NhanViens = await _unitofWork.NhanVien.GetNhanViensNotInKhoaHoc(capcc.CapCC.IdKhoaHoc);
            model.LoaiDaoTaos = await _unitofWork.PresetTable.GetAllLoaiDT();
            model.QuyetDinhs = await _unitofWork.QuyetDinh.GetAllQuyetDinh();
            model.ChungChis = await _unitofWork.ChungChi.GetAllChungChi();
            model.GiaoViens = await _unitofWork.GiaoVien.GetAllGiaoVien();
            model.CreateCapCC = capcc;
            model.CapCCs = await _unitofWork.CapCC.GetAllCapCC(capcc.CapCC.IdKhoaHoc);
            model.EditCapCC = new EditCapCC { CapCC = new CapChungChi() };
            return model;
        }
        public async Task<IActionResult> KhoaHocDetail(int idkh)
        {
            var capcc = new CreateCapCC { CapCC = new CapChungChi { IdKhoaHoc = idkh } };
            var model = await LoadKhoaHocDetailModel(capcc);
            return View(model);
        }

        [HttpGet]       
        public async Task<IActionResult> KhoaHocList(int idqd)
        {
            var model = new ModelHolder();
            model.QuyetDinhs = await _unitofWork.QuyetDinh.GetAllQuyetDinh();
            model.ChungChis = await _unitofWork.ChungChi.GetAllChungChi();
            model.GiaoViens = await _unitofWork.GiaoVien.GetAllGiaoVien();
            model.KhoaHoc = new KhoaHoc();
            if (idqd != null)
            {
                model.KhoaHoc.IdQuyetDinh = idqd;
            }          
            model.KhoaHocs = await _unitofWork.KhoaHoc.GetAllKhoaHoc();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKhoaHoc(ModelHolder model)
        {
            if(ModelState.IsValid)
            {
                await _unitofWork.KhoaHoc.CreateKhoaHoc(model.KhoaHoc);
                return RedirectToAction(nameof(KhoaHocList));
            }
            var modelBack = new ModelHolder();
            modelBack.QuyetDinhs = await _unitofWork.QuyetDinh.GetAllQuyetDinh();
            modelBack.ChungChis = await _unitofWork.ChungChi.GetAllChungChi();
            modelBack.GiaoViens = await _unitofWork.GiaoVien.GetAllGiaoVien();
            modelBack.KhoaHoc = model.KhoaHoc;
            modelBack.KhoaHocs = await _unitofWork.KhoaHoc.GetAllKhoaHoc();
            TempData["InvalidModel"] = "Model is Invalid!";
            return View(nameof(KhoaHocList),modelBack);
        }
    }
}
