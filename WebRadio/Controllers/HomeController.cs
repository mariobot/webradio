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
            return View();
        }

        public PartialViewResult Countries()
        {
            List<Countries> myDeserializedObjList;

            if (Session["Countries"] is null)
            {                
                string result = Util.Services.GET("http://www.radio-browser.info/webservice/json/countries");                 
                myDeserializedObjList = (List<Countries>)JsonConvert.DeserializeObject(Convert.ToString(result), typeof(List<Countries>));
                Session["Countries"] = myDeserializedObjList;
            }
            else
                myDeserializedObjList = (List<Countries>)Session["Countries"];
            
            return PartialView(myDeserializedObjList);
        }

        public PartialViewResult Languages()
        {
            List<Languages> myDeserializedObjList;

            if (Session["Languages"] is null)
            {
                string result = Util.Services.GET("http://www.radio-browser.info/webservice/json/languages");
                myDeserializedObjList = (List<Languages>)JsonConvert.DeserializeObject(result, typeof(List<Languages>));
                Session["Languages"] = myDeserializedObjList;
            }
            else
                myDeserializedObjList = (List<Languages>)Session["Languages"];

            return PartialView(myDeserializedObjList);
        }

        public PartialViewResult Tags()
        {
            List<Tags> myDeserializedObjList;

            if (Session["Tags"] is null)
            {
                string result = Util.Services.GET("http://www.radio-browser.info/webservice/json/tags");
                myDeserializedObjList = (List<Tags>)JsonConvert.DeserializeObject(result, typeof(List<Tags>));
                Session["Tags"] = myDeserializedObjList;
            }
            else
                myDeserializedObjList = (List<Tags>)Session["Tags"];

            return PartialView(myDeserializedObjList);
        }

        public async Task<ActionResult> ByCountry(string id) {

            string result = await Util.Services.GETAsync("http://www.radio-browser.info/webservice/json/stations/bycountryexact/"+id+"");
            List<RadioStation> myDeserializedObjList = (List<RadioStation>)JsonConvert.DeserializeObject(result, typeof(List<RadioStation>));
            Session["RadioList"] = myDeserializedObjList;

            return RedirectToAction("RadioList");
        }

        public async Task<ActionResult> ByTag(string id)
        {
            string result = await Util.Services.GETAsync("http://www.radio-browser.info/webservice/json/stations/bytagexact/"+id+"");
            List<RadioStation> myDeserializedObjList = (List<RadioStation>)JsonConvert.DeserializeObject(result, typeof(List<RadioStation>));
            Session["RadioList"] = myDeserializedObjList;

            return RedirectToAction("RadioList");
        }

        public async Task<ActionResult> ByLanguage(string id)
        {
            string result = await Util.Services.GETAsync("http://www.radio-browser.info/webservice/json/stations/bylanguageexact/" + id + "");
            List<RadioStation> myDeserializedObjList = (List<RadioStation>)JsonConvert.DeserializeObject(result, typeof(List<RadioStation>));
            Session["RadioList"] = myDeserializedObjList;

            return RedirectToAction("RadioList");
        }

        [HttpGet]
        public ActionResult RadioList()
        {
            List<RadioStation> _listRadio = (List<RadioStation>)Session["RadioList"];

            if (_listRadio is null)
                return View(new List<RadioStation>());
            else
                return View(_listRadio);
            
        }
    }
}