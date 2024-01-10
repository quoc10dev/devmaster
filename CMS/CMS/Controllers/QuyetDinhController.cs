using CMS.ImplementClass;
using CMS.Interface;
using CMS.Models;
using CMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace CMS.Controllers
{
    [Authorize]
    public class QuyetDinhController : Controller
    {
        private readonly IUnitofWork _unitofWork;
        private IWebHostEnvironment _webHostEnvironment;
        private IOptions<UploadPath> _uploadPathConfig;

        public QuyetDinhController(IUnitofWork unitofWork, IWebHostEnvironment webHostEnvironment, IOptions<UploadPath> uploadPathConfig)
        {
            _unitofWork = unitofWork;
            _webHostEnvironment = webHostEnvironment;
            _uploadPathConfig = uploadPathConfig;
        }

        [HttpPost]
        public async Task<IActionResult> EditQuyetDinh(ModelHolder model)
        {
            if (ModelState.IsValid)
            {
                if (model.EditQuyetDinh.isRemoveFile)
                {
                    if (await RemoveFile(model.EditQuyetDinh.QuyetDinh.AnhQuyetDinh))
                    {
                        await _unitofWork.QuyetDinh.EditQuyetDinh(model.EditQuyetDinh.QuyetDinh, "");
                        return RedirectToAction(nameof(QuyetDinhDetail), new { idQD = model.EditQuyetDinh.QuyetDinh.Id });
                    }
                }
                else if (model.EditQuyetDinh.AnhQD != null)
                {
                    string fileName = model.EditQuyetDinh.AnhQD.FileName;
                    if (await UploadFile(model.EditQuyetDinh.AnhQD, fileName, FolderPath.QuyetDinh, model.EditQuyetDinh.QuyetDinh.AnhQuyetDinh))
                    {
                        await _unitofWork.QuyetDinh.EditQuyetDinh(model.EditQuyetDinh.QuyetDinh, $"/{FolderPath.QuyetDinh}{fileName}");
                        return RedirectToAction(nameof(QuyetDinhDetail), new { idQD = model.EditQuyetDinh.QuyetDinh.Id });
                    }
                    else
                    {
                        TempData["ErrorMess"] = "Không thể tải lên file PDF, vui lòng thử lại!";
                        TempData["InvalidEditModel"] = "Model is invalid!";
                        var backModel1 = new ModelHolder();
                        backModel1.EditQuyetDinh = model.EditQuyetDinh;
                        backModel1.QuyetDinh = await _unitofWork.QuyetDinh.GetQuyetDinhDetail(model.EditQuyetDinh.QuyetDinh.Id);
                        backModel1.QuyetDinh.SoQuyetDinh = model.EditQuyetDinh.QuyetDinh.SoQuyetDinh;
                        backModel1.QuyetDinh.NgayQuyetDinh = model.EditQuyetDinh.QuyetDinh.NgayQuyetDinh;
                        backModel1.QuyetDinh.IdDonViDaoTao = model.EditQuyetDinh.QuyetDinh.IdDonViDaoTao;
                        backModel1.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
                        return View(nameof(QuyetDinhDetail), backModel1);
                    }
                }
                else
                {
                    await _unitofWork.QuyetDinh.EditQuyetDinh(model.EditQuyetDinh.QuyetDinh);
                    return RedirectToAction(nameof(QuyetDinhDetail), new { idQD = model.EditQuyetDinh.QuyetDinh.Id });
                }
            }
            TempData["InvalidEditModel"] = "Model is invalid!";
            var backModel2 = new ModelHolder();
            backModel2.EditQuyetDinh = model.EditQuyetDinh;
            backModel2.QuyetDinh = await _unitofWork.QuyetDinh.GetQuyetDinhDetail(model.EditQuyetDinh.QuyetDinh.Id);
            backModel2.QuyetDinh.SoQuyetDinh = model.EditQuyetDinh.QuyetDinh.SoQuyetDinh;
            backModel2.QuyetDinh.NgayQuyetDinh = model.EditQuyetDinh.QuyetDinh.NgayQuyetDinh;
            backModel2.QuyetDinh.IdDonViDaoTao = model.EditQuyetDinh.QuyetDinh.IdDonViDaoTao;
            backModel2.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
            return View(nameof(QuyetDinhDetail), backModel2);
        }

        public async Task<IActionResult> QuyetDinhDetail(int idQD)
        {
            var model = new ModelHolder();
            model.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
            model.QuyetDinh = await _unitofWork.QuyetDinh.GetQuyetDinhDetail(idQD);
            model.EditQuyetDinh = new EditQuyetDinh { QuyetDinh = new QuyetDinh() };
            return View(model);
        }

        public async Task<IActionResult> QuyetDinhList()
        {
            var model = new ModelHolder();
            model.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
            model.QuyetDinhs = await _unitofWork.QuyetDinh.GetAllQuyetDinh();
            model.CreateQuyetDinh = new CreateQuyetDinh();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuyetDinh(ModelHolder model)
        {
            
            if(ModelState.IsValid)
            {
                if(model.CreateQuyetDinh.PDF != null)
                {
                    string fileName = model.CreateQuyetDinh.PDF.FileName;
                    if(await UploadFile(model.CreateQuyetDinh.PDF, fileName, FolderPath.QuyetDinh, ""))
                    {
                        await _unitofWork.QuyetDinh.CreateQuyetDinh(model.CreateQuyetDinh.QuyetDinh, $"/{FolderPath.QuyetDinh}{fileName}");
                        return RedirectToAction(nameof(QuyetDinhList));
                    }
                    else
                    {
                        TempData["ErrorMess"] = "Không thể tải lên file PDF, vui lòng thử lại!";
                        TempData["InvalidModel"] = "Model is invalid!";
                        var backModel1 = new ModelHolder();
                        backModel1.QuyetDinhs = await _unitofWork.QuyetDinh.GetAllQuyetDinh();
                        backModel1.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
                        backModel1.CreateQuyetDinh.QuyetDinh = model.CreateQuyetDinh.QuyetDinh;
                        return View(nameof(QuyetDinhList), backModel1);
                    }
                }
                else
                {
                    await _unitofWork.QuyetDinh.CreateQuyetDinh(model.CreateQuyetDinh.QuyetDinh, "");
                    return RedirectToAction(nameof(QuyetDinhList));
                }
            }
            var backModel2 = new ModelHolder();
            backModel2.QuyetDinhs = await _unitofWork.QuyetDinh.GetAllQuyetDinh();
            backModel2.DVDTs = await _unitofWork.DVDT.GetAllDVDT();
            backModel2.CreateQuyetDinh = model.CreateQuyetDinh;
            TempData["InvalidModel"] = "Model is invalid!";
            return View(nameof(QuyetDinhList), backModel2);
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
    }
}
