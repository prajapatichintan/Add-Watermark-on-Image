using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using Watermark;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace WaterMarkImage.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string imgName)
        {
            ViewBag.ImgName = imgName;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        

      

        public ActionResult WaterMarkImage(HttpPostedFileBase fileToUpload)
        {
            using (Image image = Image.FromStream(fileToUpload.InputStream, true, false))
            {
                string name = Path.GetFileNameWithoutExtension(fileToUpload.FileName);
                var ext = Path.GetExtension(fileToUpload.FileName);
                string myfile = name + ext;
                var saveImagePath = Path.Combine(Server.MapPath("~/ImgWatermark"), myfile);
                Image watermarkImage = Image.FromFile(Server.MapPath("/Img/watermarklogo.png"));
                Watermarker objWatermarker = new Watermarker(image);
                for (int i = 0; i < image.Height; i++)
                {
                    for (int j = 0; j < image.Width; j++)
                    {

                        // Set the properties for the logo
                        objWatermarker.Position = WatermarkPosition.Absolute;
                        objWatermarker.PositionX = j;
                        objWatermarker.PositionY = i;
                        objWatermarker.Margin = new Padding(20);
                        objWatermarker.Opacity = 0.5f;
                        objWatermarker.TransparentColor = Color.White;
                        objWatermarker.ScaleRatio = 3;
                        // Draw the logo
                        objWatermarker.DrawImage(watermarkImage);
                        //Draw the Text
                        //objWatermarker.DrawText("WaterMarkDemo")

                        j = j + 400;// watermark image width 
                    }
                    i = i + 120;//
                }
                objWatermarker.Image.Save(saveImagePath);

                return RedirectToAction("Index", new { imgName = myfile });
            }
        }

    }
}