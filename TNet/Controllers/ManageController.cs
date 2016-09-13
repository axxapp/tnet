﻿using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Util;
using System.IO;
using WeChatApp.Models;
using TNet.Models;
using TNet.EF;
using TNet.BLL;
using TNet.Authorize;
using TNet.Util;
using log4net;
using System.Reflection;
using System;

namespace TNet.Controllers
{
    public class ManageController : Controller
    {
        [HttpGet]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        public ActionResult Login(ManageUserViewModel model) {
            ManageUser user = ManageUserService.GetManageUserByUserName(model.UserName);
            if (user==null) {
                ModelState.AddModelError("","没有找到该用户名.");
                return View(model);
            }
            string md5Password = string.Empty;
            md5Password= Crypto.GetPwdhash(model.ClearPassword, user.MD5Salt);
            if (md5Password.ToUpper()!=user.Password.ToUpper()) {
                ModelState.AddModelError("", "密码不正确.");
                return View(model);
            }
            Session["ManageUser"] = user;
            return RedirectToAction("Index", "Manage");
        }

        /// <summary>
        /// 后台管理首页
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult Index()
        {
            
            return View();
        }

        /// <summary>
        /// 商品列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult MercList(int pageIndex=0) {
            int pageCount = 0;
            int pageSize = 10;
            List<Merc> entities = MercService.GetALL();
            List<Merc> pageList=  entities.Pager<Merc>(pageIndex, pageSize, out pageCount);


            List<MercViewModel> viewModels = pageList.Select(model => {
                MercViewModel viewModel = new MercViewModel();
                viewModel.CopyFromBase(model);
                return viewModel;
            }).ToList();


            //获取产品类型
            List<MercType> mercTypes = MercTypeService.GetALL();

            viewModels =viewModels.Select(model => {
                model.MercTypeName = mercTypes.Where(en => en.idtype == model.idtype).FirstOrDefault().name;
                return model;
            }).ToList();

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            

            return View(viewModels);
        }

        /// <summary>
        /// 创建/编辑商品
        /// </summary>
        /// <param name="idmerc"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult MercEdit(int idmerc=0) {
            MercViewModel model = new MercViewModel();
            if (idmerc>0) {
                Merc merc = MercService.GetMerc(idmerc);
                if ( merc!=null) { model.CopyFromBase(merc); }
            }
            List<MercType> entities = MercTypeService.GetALL();
            List<MercTypeViewModel> mercTypes= entities.Select(en => {
                MercTypeViewModel viewModel = new MercTypeViewModel();
                viewModel.CopyFromBase(en);
                return viewModel;
            }).ToList();

            model.mercTypes = mercTypes;
            return View(model);
        }

        /// <summary>
        /// 编辑商品
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult MercEdit(MercViewModel model) {

            Merc merc = new Merc();
            model.CopyToBase(merc);
            if (merc.idmerc == 0)
            {
                //新增
                merc = MercService.Add(merc);
            }
            else {
                //编辑
                merc = MercService.Edit(merc);
            }
            

            //修改后重新加载
            model.CopyFromBase(merc); 
            List<MercType> entities = MercTypeService.GetALL();
            List<MercTypeViewModel> mercTypes = entities.Select(en => {
                MercTypeViewModel viewModel = new MercTypeViewModel();
                viewModel.CopyFromBase(en);
                return viewModel;
            }).ToList();

            model.mercTypes = mercTypes;

            ModelState.AddModelError("", "保存成功.");
            return View(model);
        }

        /// <summary>
        /// 产品类型列表
        /// </summary>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult MercTypeList(int pageIndex=0) {
            int pageCount = 0;
            int pageSize = 10;
            List<MercType> entities = MercTypeService.GetALL();
            List<MercType> pageList = entities.Pager<MercType>(pageIndex, pageSize, out pageCount);


            List<MercTypeViewModel> viewModels = pageList.Select(model => {
                MercTypeViewModel viewModel = new MercTypeViewModel();
                viewModel.CopyFromBase(model);
                return viewModel;
            }).ToList();

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;

            return View(viewModels);
        }

        /// <summary>
        /// 新增\编辑产品类型
        /// </summary>
        /// <param name="idtype"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult MercTypeEdit(int idtype=0)
        {
            MercTypeViewModel model = new MercTypeViewModel();
            if (idtype > 0)
            {
                MercType mercType = MercTypeService.GetMercType(idtype);
                if (mercType != null) { model.CopyFromBase(mercType); }
            }
           
            return View(model);
        }

        /// <summary>
        /// 新增\编辑产品类型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult MercTypeEdit(MercTypeViewModel model)
        {
            MercType mercType = new MercType();
            model.CopyToBase(mercType);
            if (mercType.idtype == 0)
            {
                //新增
                mercType = MercTypeService.Add(mercType);
            }
            else
            {
                //编辑
                mercType = MercTypeService.Edit(mercType);
            }

            //修改后重新加载
            model.CopyFromBase(mercType);
           
            return View(model);
        }

        /// <summary>
        /// 产品规格列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult SpecList(int idmerc, int pageIndex=0) {
            int pageCount = 0;
            int pageSize = 10;
            List<Spec> entities = SpecService.GetSpecsByIdMerc(idmerc);
            List<Spec> pageList = entities.Pager<Spec>(pageIndex, pageSize, out pageCount);
            
            List<SpecViewModel> viewModels = pageList.Select(model => {
                SpecViewModel viewModel = new SpecViewModel();
                viewModel.CopyFromBase(model);
                return viewModel;
            }).ToList();
            
            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;


            return View(viewModels);
        }

        /// <summary>
        /// 新增\编辑产品规格
        /// </summary>
        /// <param name="idspec"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpGet]
        public ActionResult SpecEdit(int idspec = 0)
        {
            SpecViewModel model = new SpecViewModel();
            if (idspec > 0)
            {
                Spec spec = SpecService.GetSpecs(idspec);
                if (spec != null) { model.CopyFromBase(spec); }
            }

            return View(model);
        }

        /// <summary>
        /// 新增\编辑产品规格
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        [HttpPost]
        public ActionResult SpecEdit(SpecViewModel model)
        {
            Spec spec = new Spec();
            model.CopyToBase(spec);
            if (spec.idspec == 0)
            {
                //新增
                spec = SpecService.Add(spec);
            }
            else
            {
                //编辑
                spec = SpecService.Edit(spec);
            }

            //修改后重新加载
            model.CopyFromBase(spec);

            return View(model);
        }

        /// <summary>
        /// 产品图片管理
        /// </summary>
        /// <param name="mercId"></param>
        /// <returns></returns>
        [ManageLoginValidation]
        public ActionResult MercImageList(int mercId,int pageIndex=0) {
            int pageCount = 0;
            int pageSize = 10;
            List<MercImage> entities = MercImageService.GetMercImagesByMercId(mercId);
            List<MercImage> pageList = entities.Pager<MercImage>(pageIndex, pageSize, out pageCount);
            
            List<MercImageViewModel> viewModels = pageList.Select(model => {
                MercImageViewModel viewModel = new MercImageViewModel();
                viewModel.CopyFromBase(model);
                return viewModel;
            }).ToList();

            ViewData["pageCount"] = pageCount;
            ViewData["pageIndex"] = pageIndex;
            ViewData["mercId"] = mercId;

            return View(viewModels);
        }

        /// <summary>
        /// 产品图片信息编辑
        /// </summary>
        /// <param name="ercImageId"></param>
        /// <returns></returns>
        [HttpGet]
        [ManageLoginValidation]
        public ActionResult MercImageEdit(int idmerc ,int MercImageId=0) {
            MercImageViewModel mercImageModel = new MercImageViewModel();
            if (MercImageId == 0)
            {
                mercImageModel.idmerc = idmerc;
            }
            else {
                MercImage mercImage =   MercImageService.GetMercImage(MercImageId);
                mercImageModel.CopyFromBase(mercImage);
            }
            return View(mercImageModel);
        }

        /// <summary>
        /// 产品图片信息编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ManageLoginValidation]
        public ActionResult MercImageEdit(MercImageViewModel model)
        {
            MercImage mercImage = new MercImage();
            model.CopyToBase(mercImage);
            if (model.MercImageId == 0)
            {
                mercImage.MercImageId = IdentifyService.GetMaxIdentifyID<MercImage>(en => en.MercImageId)+1;
                MercImageService.Add(mercImage);
            }
            else
            { 
                MercImageService.Edit(mercImage);
            }
            return View();
        }


        public ActionResult UploadMercImage(int mercId) {
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            string GUID = System.Guid.NewGuid().ToString();
            string path = "~/Resouce/Images/" ;
            string filename = string.Empty;
            string message = string.Empty;
            try
            {
                if (Request.Files != null && Request.Files.Count > 0)
                {
                    if (Request.Files.Count > 1)
                    {
                        return Content("请选择文件.");
                    }
                    foreach (string upload in Request.Files)
                    {
                        if (!Request.Files[upload].HasFile())
                        {
                            return Content("文件大小不能0.");
                        }


                        if (!CheckFileType((HttpPostedFileWrapper)((HttpFileCollectionWrapper)Request.Files)[upload]))
                        {
                            return Content("文件类型只能是jpg,bmp,gif,PNG.");
                        }

                        //获取文件后缀名
                        string originFileName = Request.Files[upload].FileName;
                        string originFileNameSuffix = string.Empty;
                        int lastIndex = originFileName.LastIndexOf(".");
                        if (lastIndex < 0)
                        {
                            return Content("文件类型只能是jpg,bmp,gif,PNG.");
                        }
                        originFileNameSuffix = originFileName.Substring(lastIndex, originFileName.Length - lastIndex);

                        filename = GUID + originFileNameSuffix;
                        if (!Directory.Exists(Server.MapPath(path)))
                        {
                            Directory.CreateDirectory(Server.MapPath(path));
                        }

                        Request.Files[upload].SaveAs(Path.Combine(Server.MapPath(path), filename));

                    }

                }
                else
                {
                    return Content("没有选择要上传的文件.");
                }
            }
            catch (Exception ex)
            {
                return Content("没有选择要上传的文件.");
                log.Error(ex.ToString());
            }
            return Content("文件上传成功。");


        }

        /// <summary>
        /// 判断上传文件类型
        /// </summary>
        private bool CheckFileType(HttpPostedFileWrapper postedFile)
        {

            bool result = true;
            /*  文件扩展名说明  
            jpg：255216  
            bmp：6677  
            gif：7173  
            PNG：13780
            pdf：3780  
            */
            int[] fileTypes = new int[] { 255216, 6677, 7173, 13780, 3780 };
            string fileTypeStr = "255216, 6677, 7173, 13780, 3780";
            ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            int fileLength = postedFile.ContentLength;
            if (fileLength <= 0)
            {
                return false;
            }
            byte[] imgArray = new byte[fileLength];

            postedFile.InputStream.Read(imgArray, 0, fileLength);

            System.IO.MemoryStream fs = new System.IO.MemoryStream();
            fs = new System.IO.MemoryStream(imgArray);
            System.IO.BinaryReader r = new System.IO.BinaryReader(fs);
            string fileclass = string.Empty;
            byte buffer;
            try
            {
                buffer = r.ReadByte();
                fileclass = buffer.ToString();
                buffer = r.ReadByte();
                fileclass += buffer.ToString();
            }
            catch (Exception ex)
            {
                result = false;
                log.Error(ex.ToString());
            }
            finally
            {
                r.Close();
                fs.Close();
                r.Dispose();
                fs.Dispose();
            }
            if (fileTypeStr.IndexOf(fileclass) < 0)
            {
                result = false;
            }

            return result;

        }

        [HttpGet]
        public ActionResult Menu()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token=" + Pub.accessToken;
            string data = HttpHelp.Get(url);
            ViewBag.Menu = new HtmlString(data);
            return View();
        }


        public ActionResult GetMenu()
        {

            return View();
        }

        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public JsonResult GetMaterialList(RMaterialListParamM mode)
        {
             
            string url = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token=" + Pub.accessToken;
            JavaScriptSerializer s = new JavaScriptSerializer();
            string responseContent = HttpHelp.Post(url, s.Serialize(mode));
            return Json(responseContent);
        }

        /// <summary>
        /// 获取指定图片信息
        /// </summary>
        /// <param name="media_id"></param>
        /// <returns>存储路径</returns>
        public string GetImageById(RMaterialItemParamM mode)
        {
            string filePath = Server.MapPath("~/Content/Images/Material/" + mode.media_id + ".jpg");
            if (!System.IO.File.Exists(filePath))
            {
                HttpHelp bll = new HttpHelp();
                string url = "https://api.weixin.qq.com/cgi-bin/material/get_material?access_token=" + Pub.accessToken;
                JavaScriptSerializer Serializer = new JavaScriptSerializer();
                filePath = bll.DownLoad(url, Serializer.Serialize(mode), mode.media_id);
            }
            return Pub.urlconvertor(filePath);
        }


        /// <summary>
        /// 获取指定的素材信息
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public string GetMaterialDetail(RMaterialItemParamM mode)
        { 
            string url = "https://api.weixin.qq.com/cgi-bin/material/get_material?access_token=" + Pub.accessToken;
            JavaScriptSerializer s = new JavaScriptSerializer();
            string responseContent = HttpHelp.Post(url, s.Serialize(mode));
            return responseContent;
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public JsonResult UpdateMenu(string menu)
        { 
            string url = " https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + Pub.accessToken;
            string responseContent = HttpHelp.Post(url, menu);
            return Json(responseContent);
        }
    }
}
