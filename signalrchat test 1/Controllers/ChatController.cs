using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using signalrchat_test_1.Models;

namespace signalrchat_test_1.Controllers
{
    public class ChatController : Controller
    {
        public static string Userid;
        string a;
        // GET: Chat
        public ActionResult Index(int id)
        {
            if (id != null || id !=0)
            {
                Userid = id.ToString();
            }            
            return View();
        }
        public int GetId()
        {  
            return Convert.ToInt32(Userid);            
        }
        IFirebaseConfig Config = new FirebaseConfig
        {
            AuthSecret = "VD9fJ5TXXJcJm9D98sdi0qDKm06ot0yhobFin8l5",
            BasePath = "https://overlorddemo-d37ee.firebaseio.com/"
        };
        IFirebaseClient client;
        public async Task<ActionResult> Insert()
        {
            client = new FireSharp.FirebaseClient(Config);
            //string a;
            //if (client != null)
            //{
            //    a = "Connection successful";
            //}
            //else
            //{
            //    a = "not successful";
            //}
            
            var test=new Test { Id=4, Name="fff"};
            SetResponse response = await client.SetTaskAsync("test/"+test.Id, test);
            Test result = response.ResultAs<Test>();

             a="Data "+result.Id+" "+result.Name;
            ViewBag.a = a;
            return View();
        }
        //public ActionResult Insert()
        //{
           
        //    return View();
        //}
    }
}