using prjLottoApp.Models;
using prjMvcDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class AController : Controller
    {
        public ActionResult demoFileUpLoad()
        {
            return View();
        }
        [HttpPost]
        public ActionResult demoFileUpLoad(HttpPostedFileBase photo)
        {
            photo.SaveAs(@"C:\Users\Student\Desktop\MVC\slnMvcDemo\prjMvcDemo\image\test.jpg");
            return View();
        }

        public string demoResponse()
        {
            //觸發事件都是由Response開始(客戶端為Request
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.Filter.Close();
            Response.WriteFile(@"C:\QN\01.jpg");
            Response.End();
            return "";
        }
        //static int count = 0;  //如果使用STATIC的話會存在記憶體中不會被刪除,但是不會被分類是否為不同人使用ex:N人點100次就會Nx100
        
        //public ActionResult showCount()
        //{
        //    count++;
        //    ViewBag.con = count;
        //    return View();
        //}
        public ActionResult showCountByCookie()
        {
            int count=0;
            HttpCookie x = Request.Cookies["kk"];

            if (x != null)
                count = Convert.ToInt32(x.Value);
            count++;
            x = new HttpCookie ("kk");
            x.Value = count.ToString();
            x.Expires = DateTime.Now.AddDays(20);
            Response.Cookies.Add(x);

            ViewBag.con = count;
            return View();
        }
        public ActionResult showCountBySession()
        {
            int count = 0;
            if (Session["kk"] != null)
                count = (int)Session["kk"];
            count++;
            Session["kk"] = count;
            ViewBag.con = count;
            return View();
        }
        public ActionResult demoForm()
        {
            ViewBag.Ans = "?";
            ViewBag.a = " ";
            ViewBag.b = " ";
            ViewBag.c = " ";
            if (!string.IsNullOrEmpty(Request.Form["txtA"]))
            {
                int a = Convert.ToInt32(Request.Form["txtA"]);
                int b = Convert.ToInt32(Request.Form["txtB"]);
                int c = Convert.ToInt32(Request.Form["txtC"]);
                ViewBag.c = c.ToString();
                ViewBag.a = a.ToString();
                ViewBag.b = b.ToString();
                double r1 = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
                double r2 = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);

                ViewBag.Ans = $"{Math.Round(r1,2)},{Math.Round(r2,2)}";
            }
            return View();
        }

        public string demoRequest()
        {
            // Request 會回傳使用者輸入的productID號碼 ex:http://網址/A/demoRequest?productId= 1 會回傳xbox加入購物車成功
            string id = Request.QueryString["productId"];
            if (id == "1")
                return "XBox 加入購物車成功";
            else if (id == "2")
                return "PS5 加入購物車成功";
            else if (id == "3")
                return "switch 加入購物車成功";
            return "找不到該產品資料";
        }

        public string demoParameter(int? productId)
        {
            //如果把productid改成id可以連  "?p[roductid="  都不用打     ex :https://localhost:44390/A/demoParameter/2
            //改成用?可以自動轉型+參數可以不用打
            if (productId == 1)
                return "XBox 加入購物車成功";
            else if (productId == 2)
                return "PS5 加入購物車成功";
            else if (productId == 3)
                return "switch 加入購物車成功";
            return "找不到該產品資料";
        }
        public string queryById(int? id)
        {
            
            if (id==null)
            return "找不到該產品資料";
            SqlConnection con = new SqlConnection();
            
            con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
            con.Open();
            SqlCommand com = new SqlCommand("select*from tCustomer WHERE fId="+id.ToString(),con);
            SqlDataReader reader = com.ExecuteReader();
            string s= "找不到該產品資料";
            if (reader.Read())
                s = reader["fName"].ToString() + "/" + reader["fPhone"].ToString();
            con.Close();
            return s;
        }

        public string sayHellow()
        {
            return "Hellow";
        }
        public string lottl()
        {
            return (new CLottoGen()).getLotto();
        }
        // GET: A
        public ActionResult bindinById(int? id)
        {
            CCustomer x = null;
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand com = new SqlCommand("select*from tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    x = new CCustomer();
                    x.fName = reader["fName"].ToString();
                    x.fId = (int)reader["fId"];
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail = reader["fEmail"].ToString();
                }
                con.Close();
            }
            return View(x);
        }
        public string testingdelete(int? id)
        {
            if (id == null)
                return "沒有指定PK";
            (new CCustomerFactory()).delete((int)id);
            return "刪除成功";
        }

        public string testqueryById(int?id)
        {

            return "客戶名稱:" + (new CCustomerFactory()).queryById((int)id).fName;
        }
        public string testqueryAll()
        {

            return "客戶人數:" + (new CCustomerFactory()).queryAll().Count(); 
        }
        public string Update()
        {
            CCustomer x = new CCustomer()
            {
                fId = 5,
                fName = "COW",
                fPhone = "123",
                fEmail = "COW@gmail.com",
                fAddress = "Linkao",
                fPassword = "123456"
            };
            (new CCustomerFactory()).Update(x);
            return "修改成功";
        }
        public string testingInsert()
        {
            CCustomer x = new CCustomer()
            {
                fName = "judy",
                fPhone = "0932165454",
                fEmail = "judy@gmail.com",
                fAddress = "Taipei",
                fPassword = "12345"
            };
            (new CCustomerFactory()).insert(x);
            return "新增成功";
        }

        public ActionResult showById(int? id)
        { 
            CCustomer x = null;
            if (id != null)
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = @"Data Source=.;Initial Catalog=dbDemo;Integrated Security=True";
                con.Open();
                SqlCommand com = new SqlCommand("select*from tCustomer WHERE fId=" + id.ToString(), con);
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    x = new CCustomer();
                    x.fName = reader["fName"].ToString();
                    x.fId= (int)reader["fId"];
                    x.fPhone = reader["fPhone"].ToString();
                    x.fEmail= reader["fEmail"].ToString();
                    ViewBag.kk = x;

                }
                    
                con.Close();            
            }
            return View(x);
        }
    }
}