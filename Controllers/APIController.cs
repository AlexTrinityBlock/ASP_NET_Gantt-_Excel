using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_NET_Gantt__Excel.Models;
using Newtonsoft.Json;

namespace ASP_NET_Gantt__Excel.Controllers
{

    public class APIController : Controller
    {
        [HttpGet]
        public ActionResult getTaskInfo(string data)
        {
            MySQLModel mySQLModel = new MySQLModel();
            var jsonData = new { data= mySQLModel.getTaskData()};
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        /**
         * 
         */
        [HttpPost]
        public ActionResult postTaskInfo(string data)
        {
            MySQLModel mySQLModel = new MySQLModel();
            var taskDataObj = JsonConvert.DeserializeObject<TaskData>(data);
            mySQLModel.setTaskData(taskDataObj);
            return Content("");
        }
    }
}