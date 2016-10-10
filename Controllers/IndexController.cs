using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using System.Drawing.Imaging;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;

namespace TAJdamaProject.Controllers
{
    
    public class IndexController : Controller
    {
        SqlConnection con;
        SqlCommand cmd;
        public static DataTable newdt=new DataTable();
        public static string QRCODE;
        // GET: Index
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(TAJdamaProject.Models.TajDama obj)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["mycon"].ConnectionString);
            con.Open();
            cmd = new SqlCommand("SP_INSERT", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FIRSTNAME", obj.objHome.Fname);
            cmd.Parameters.AddWithValue("@LASTNAME", obj.objHome.Lname);
            cmd.Parameters.AddWithValue("@ROOMNUMBER", obj.objHome.RoomNo);
            cmd.Parameters.AddWithValue("@CHECKIN", obj.objHome.Checkin);
            cmd.Parameters.AddWithValue("@CHECKOUT", obj.objHome.Checkout);
            cmd.Parameters.AddWithValue("@GuestType", obj.objHome.DDLValue);
            //cmd.Parameters.AddWithValue("@CUSTOMERID",TextBox6.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);

            if (dt.Rows.Count > 0)
            {
                //cmd.ExecuteNonQuery();
                Response.Write("<script>alert('DATA SAVED')</script>");
                string[] str = new string[] { obj.objHome.Fname, obj.objHome.Lname, obj.objHome.RoomNo, obj.objHome.Checkin.Replace("/", ""), obj.objHome.Checkout.Replace("/", ""), Convert.ToString(dt.Rows[0]["ID"]), obj.objHome.DDLValue };
                TAJdamaProject.Models.GET_APPCODE ab = new Models.GET_APPCODE();
                //GET_APPCODE ab = new GET_APPCODE();
                string obj1 = ab.QR(str);
                HttpContext.Session["encryptedData"] = obj1.ToString();
                QRCODE = obj1.ToString();
                HttpContext.Session["data"] = dt;
                newdt = dt;
                return new Rotativa.ActionAsPdf("QRcode");
                //return RedirectToAction("QRcode");
            }
            return View();
        }

        [HttpGet]
        public ActionResult QRcode()
        {
            DataTable dt = new DataTable();
            //dt = HttpContext.Session["data"] as DataTable;
            dt = newdt;
            List<TAJdamaProject.Models.ModelAppcode> list = new List<Models.ModelAppcode>();
            if (dt.Rows.Count > 0)
            {
                Models.ModelAppcode obj = new Models.ModelAppcode();
                obj.Fname = Convert.ToString(dt.Rows[0]["FIRSTNAME"]);
                obj.Lname = Convert.ToString(dt.Rows[0]["LASTNAME"]);
                obj.RoomNo = Convert.ToString(dt.Rows[0]["ROOMNUMBER"]);
                obj.Checkin = Convert.ToString(dt.Rows[0]["CHECKIN"]);
                obj.Checkout = Convert.ToString(dt.Rows[0]["CHECKOUT"]);
                obj.ID = Convert.ToString(dt.Rows[0]["ID"]);
                //string qrcde = HttpContext.Session["encryptedData"].ToString();
                string qrcde = QRCODE;
                QRCodeEncoder encoder = new QRCodeEncoder();
                Bitmap img = encoder.Encode(qrcde);
                string strImagePath = Server.MapPath("~/Image") + "//img" + Convert.ToString(dt.Rows[0]["ID"]) + ".jpeg";
                img.Save(strImagePath, ImageFormat.Jpeg);
                obj.ImgURL = "img" + Convert.ToString(dt.Rows[0]["ID"]) + ".jpeg";
                //obj.ImgURL = "http://tajdama.mindtears.com/Image/img" + Convert.ToString(dt.Rows[0]["ID"]) + ".jpeg";
                list.Add(obj);
            }
           //return new Rotativa.ActionAsPdf("QRcode", list);
            return View(list);
        }
        [HttpGet]
        public ActionResult getPDF()
        {
            return new Rotativa.ActionAsPdf("QRcode");
        }
        [HttpPost]
        public ActionResult getPDF(string obj)
        {
            return new Rotativa.ActionAsPdf("QRcode");
        }

        [HttpPost]
        public ActionResult QRcode(TAJdamaProject.Models.TajDama obj)
        {
            return new Rotativa.ActionAsPdf("QRcode");
            //return View();
        }
    }
}