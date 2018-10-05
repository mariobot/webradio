using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebRadio.Models;

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

        public ActionResult AddRadioToFavs(RadioStation _radioStation)
        {
            List<RadioStation> _listFavs;

            if (Session["ListFavs"] is null)
            {
                _listFavs = new List<RadioStation>();
                _listFavs.Add(_radioStation);                
            }
            else
            {
                _listFavs = (List<RadioStation>)Session["ListFavs"];
                _listFavs.Add(_radioStation);
            }

            Session["ListFavs"] = _listFavs;

            return RedirectToAction("RadioList");
        }

        public ActionResult FavStation()
        {
            List<RadioStation> _listFavs;

            if (Session["ListFavs"] is null)
                _listFavs = new List<RadioStation>();
            else
                _listFavs = (List<RadioStation>)Session["ListFavs"];
            
            return View(_listFavs);
        }

        public ActionResult DeleteRadioToFavs(RadioStation _radioStation)
        {
            List<RadioStation> _listFavs;

            _listFavs = (List<RadioStation>)Session["ListFavs"];
            _listFavs.RemoveAll(r => r.id == _radioStation.id);            
            Session["ListFavs"] = _listFavs;

            return RedirectToAction("FavStation");            
        }
    }
}