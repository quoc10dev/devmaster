using CMS.ImplementClass;
using CMS.Interface;
using CMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;

namespace CMS.Controllers
{
    [Authorize]
    public class NhanVienController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private IWebHostEnvironment _webHostEnvironment;
        private IOptions<UploadPath> _uploadPathConfig;

        public NhanVienController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment, IOptions<UploadPath> uploadPathConfig)
        {
            _unitofWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
            _uploadPathConfig = uploadPathConfig;
        }

        [HttpPost]
        public async Task<IActionResult> NhanViensSearchBy(ModelHolder md)
        {
            var model = new ModelHolder();
            model.CreateNhanVien = new CreateNhanVien { NhanVien = new Models.NhanVien() };
            model.PhongBans = await _unitofWork.PresetTable.GetAllPhongBan();
            model.ChucDanhs = await _unitofWork.PresetTable.GetAllChucDanh();
            model.NhanViens = await _unitofWork.NhanVien.NhanViensSearchBy(md.SearchNhanVien);
            model.SearchNhanVien = md.SearchNhanVien;
            return View(nameof(NhanVienList),model);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteNhanVien(int idnv, string avtPath)
        {
            if(avtPath != null && avtPath != "")
            {
                if (RemoveFile(avtPath))
                {
                    await _unitofWork.NhanVien.DeleteNhanVien(idnv);
                    return Json(true);
                }
                else
                {
                    return Json(false);
                }
            }
            await _unitofWork.NhanVien.DeleteNhanVien(idnv);
            return Json(true);
        }

        [HttpPost]
        public async Task<IActionResult> EditNhanVienProfile(ModelHolder model)
        {
            if(ModelState.IsValid)
            {
                if (model.EditNhanVien.isRemoveFile)
                {
                    if (RemoveFile(model.NhanVien.AnhNhanVien))
                    {
                        await _unitofWork.NhanVien.EditNhanVien(model.NhanVien, "");
                        return RedirectToAction(nameof(NhanVienProfile), new { idnv = model.NhanVien.Id });
                    }
                    else
                    {
                        TempData["ErrorMess"] = "Không thể xóa ảnh, vui lòng thử lại!";
                    }
                }else if (model.EditNhanVien.Avt != null)
                {
                    var fileName = model.EditNhanVien.Avt.FileName;
                    if (await UploadFile(model.EditNhanVien.Avt, fileName, FolderPath.AvtNhanVien, model.NhanVien.AnhNhanVien))
                    {
                        await _unitofWork.NhanVien.EditNhanVien(model.NhanVien, $"/{FolderPath.AvtNhanVien}{fileName}");
                        return RedirectToAction(nameof(NhanVienProfile), new { idnv = model.NhanVien.Id });
                    }
                    else
                    {
                        TempData["ErrorMess"] = "Không thể tải lên ảnh, vui lòng thử lại!";
                    }
                }
                else
                {
                    await _unitofWork.NhanVien.EditNhanVien(model.NhanVien);
                    return RedirectToAction(nameof(NhanVienProfile), new { idnv = model.NhanVien.Id });
                }
               
            }
            var backModel = new ModelHolder();
            backModel.PhongBans = await _unitofWork.PresetTable.GetAllPhongBan();
            backModel.ChucDanhs = await _unitofWork.PresetTable.GetAllChucDanh();
            backModel.NhanVien = await _unitofWork.NhanVien.GetProfileNhanVien(model.NhanVien.Id);
            backModel.NhanVien.TenNV = model.NhanVien.TenNV;
            backModel.NhanVien.MaNV = model.NhanVien.MaNV;
            backModel.NhanVien.IdChucDanh = model.NhanVien.IdChucDanh;
            backModel.NhanVien.IdPhongBan = model.NhanVien.IdPhongBan;
            backModel.NhanVien.CCCD = model.NhanVien.CCCD;
            backModel.NhanVien.NgaySinh = model.NhanVien.NgaySinh;
            backModel.NhanVien.DiaChi = model.NhanVien.DiaChi;
            backModel.NhanVien.Sdt = model.NhanVien.Sdt;
            TempData["InvalidNVModel"] = "Model is Invalid!";
            return View(nameof(NhanVienProfile), backModel);
        }
        public async Task<IActionResult> NhanVienProfile(int idnv)
        {
            var model = new ModelHolder();
            model.NhanVien = await _unitofWork.NhanVien.GetProfileNhanVien(idnv);
            model.PhongBans = await _unitofWork.PresetTable.GetAllPhongBan();
            model.ChucDanhs = await _unitofWork.PresetTable.GetAllChucDanh();
            return View(model);
        }

        public async Task<IActionResult> NhanVienList()
        {
            var model = new ModelHolder();
            model.CreateNhanVien = new CreateNhanVien { NhanVien = new Models.NhanVien() };
            model.SearchNhanVien = new Models.NhanVien();
            model.PhongBans = await _unitofWork.PresetTable.GetAllPhongBan();
            model.ChucDanhs = await _unitofWork.PresetTable.GetAllChucDanh();
            model.NhanViens = await _unitofWork.NhanVien.GetAllNhanVien();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNhanVien(ModelHolder model)
        {
            if(ModelState.IsValid)
            {
                if(model.CreateNhanVien.Avt != null)
                {
                    var fileName = model.CreateNhanVien.Avt.FileName;
                    if(await UploadFile(model.CreateNhanVien.Avt,fileName,FolderPath.AvtNhanVien, ""))
                    {
                        await _unitofWork.NhanVien.CreateNhanVien(model.CreateNhanVien, $"/{FolderPath.AvtNhanVien}{fileName}");
                        return RedirectToAction(nameof(NhanVienList));
                    }
                    else
                    {
                        TempData["ErrorMess"] = "Không thể tải lên file PDF, vui lòng thử lại!";
                        TempData["InvalidModel"] = "Model is Invalid!";
                        var modelBack1 = new ModelHolder();
                        modelBack1.CreateNhanVien = model.CreateNhanVien;
                        modelBack1.PhongBans = await _unitofWork.PresetTable.GetAllPhongBan();
                        modelBack1.ChucDanhs = await _unitofWork.PresetTable.GetAllChucDanh();
                        modelBack1.NhanViens = await _unitofWork.NhanVien.GetAllNhanVien();
                        modelBack1.SearchNhanVien = new Models.NhanVien();
                        return View(nameof(NhanVienList), modelBack1);
                    }
                }
                else
                {
                    await _unitofWork.NhanVien.CreateNhanVien(model.CreateNhanVien, "");
                    return RedirectToAction(nameof(NhanVienList));
                }              
            }
            var modelBack = new ModelHolder();
            modelBack.CreateNhanVien = model.CreateNhanVien;
            modelBack.PhongBans = await _unitofWork.PresetTable.GetAllPhongBan();
            modelBack.ChucDanhs = await _unitofWork.PresetTable.GetAllChucDanh();
            modelBack.NhanViens = await _unitofWork.NhanVien.GetAllNhanVien();
            modelBack.SearchNhanVien = new Models.NhanVien();
            TempData["InvalidModel"] = "Model is Invalid!";
            return View(nameof(NhanVienList), modelBack);
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
        public bool RemoveFile(string filePath)
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
    }
}
