using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using WebApplication1.Models;
using WebApplication1.Service;
using WebApplication1.Models.DTO;
using FireSharp;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        static IFirebaseConfig config = new FirebaseConfig { BasePath = "https://cntt3-79ca3-default-rtdb.firebaseio.com" };
        static IFirebaseClient client = new FirebaseClient(config);

        public ActionResult Index()
        {
            RealtimeData realtimeDatas = new Firebase().GetFireBase();
            Session["value"] = realtimeDatas.Tempurature;
            return View(realtimeDatas);
        }
        public ActionResult history()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("HistoryDataDHT");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<HistoryData>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    list.Add(JsonConvert.DeserializeObject<HistoryData>(((JProperty)item).Value.ToString()));
                }
            }

            return View(list);

            //return View(new Firebase().GetHistoryFireBase());
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
    }
}