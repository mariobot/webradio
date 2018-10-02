using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRadio.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace WebRadio.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string result = Util.Services.GET_V1("http://www.radio-browser.info/webservice/json/stations/bystate/caldas");

            List<RadioStation> myDeserializedObjList = (List<RadioStation>)JsonConvert.DeserializeObject(result, typeof(List<RadioStation>));

            return View();
        }

        public PartialViewResult Countries()
        {
            string result = Util.Services.GET_V1("http://www.radio-browser.info/webservice/json/countries");

            List<Countries> myDeserializedObjList = (List<Countries>)JsonConvert.DeserializeObject(result, typeof(List<Countries>));

            return PartialView(myDeserializedObjList);
        }

        public PartialViewResult Tags()
        {
            string result = Util.Services.GET_V1("http://www.radio-browser.info/webservice/json/tags");

            List<Tags> myDeserializedObjList = (List<Tags>)JsonConvert.DeserializeObject(result, typeof(List<Tags>));

            return PartialView(myDeserializedObjList);
        }

        public ActionResult ByCountry(string id) {

            string result = Util.Services.GET_V1("http://www.radio-browser.info/webservice/json/stations/bycountryexact/"+id+"");

            List<RadioStation> myDeserializedObjList = (List<RadioStation>)JsonConvert.DeserializeObject(result, typeof(List<RadioStation>));

            Session["RadioList"] = myDeserializedObjList;

            return RedirectToAction("RadioList");
        }

        public ActionResult ByTag(string id)
        {
            string result = Util.Services.GET_V1("http://www.radio-browser.info/webservice/json/stations/bytagexact/"+id+"");

            List<RadioStation> myDeserializedObjList = (List<RadioStation>)JsonConvert.DeserializeObject(result, typeof(List<RadioStation>));

            Session["RadioList"] = myDeserializedObjList;

            return RedirectToAction("RadioList");
        }

        [HttpGet]
        public ActionResult RadioList()
        {
            List<RadioStation> _listRadio = (List<RadioStation>)Session["RadioList"];

            if (_listRadio.Count > 0)
                return View(_listRadio);
            else
                return View(new List<RadioStation>());
        }
    }
}