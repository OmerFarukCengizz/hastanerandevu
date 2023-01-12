using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using hastanerandevu.Models.Entities;
using System.Security.Cryptography.Xml;

namespace hastanerandevu.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        
        hastaneEntities db=new hastaneEntities();
        public ActionResult Index()
        {
            var degerler=db.hastaneler.ToList();
            return View(degerler);
        }
        Class1 cs = new Class1();

        public ActionResult Cascading()
        {
            ViewBag.SehirList = new SelectList(ilgetir(), "SEHIRID", "SEHIRAD");
            return View();
        }
        public List<sehirler> ilgetir()
        {
           List<sehirler> sehirler=db.sehirler.ToList();

            return sehirler;
        }
        public ActionResult ilcegetir(int SEHIRID)
        {
            List<ilceler> selectlist=db.ilceler.Where(x=>x.SEHIRID==SEHIRID).ToList();
            ViewBag.Ilcelist= new SelectList(selectlist,"ILCEID", "ILCEAD");
            return PartialView("ilcegoster");
        }
        public ActionResult hastanegetir(int ILCEID)
        {
            List<hastaneler> selectlist = db.hastaneler.Where(x => x.ILCEID == ILCEID).ToList();
            ViewBag.Hastanelist = new SelectList(selectlist, "HASTANEID", "HASTANEAD");
            return PartialView("hastanegoster");
        }
        public ActionResult klinikgetir(int HASTANEID)
        {
            List<klinikler> selectlist = db.klinikler.Where(x => x.HASTANEID == HASTANEID).ToList();
            ViewBag.Kliniklist = new SelectList(selectlist, "KLINIKID", "KLINIKAD");
            return PartialView("klinikgoster");
        }
        public ActionResult doktorgetir(int KLINIKID)
        {
            List<doktorlar> selectlist = db.doktorlar.Where(x => x.KLINIKID == KLINIKID).ToList();
            ViewBag.Doktorlist = new SelectList(selectlist, "DOKTORID", "DOKTORAD");
            return PartialView("doktorgoster");
        }
       
        public ActionResult AsiRandevu()
        {      
              return View();
        }
      
        [HttpPost]
        public ActionResult AsiRandevuAl(string data)
        {
            randevular rande = new randevular();
            var dataObject = JsonConvert.DeserializeObject<randevular>(data);
            var kullaniciadi = User.Identity.Name;
            var kullanici = db.user.FirstOrDefault(x => x.USERTC == kullaniciadi);
            rande.USERID = kullanici.USERID;
            rande.DOKTORID = dataObject.DOKTORID;
            rande.RANDEVUTARIH = dataObject.RANDEVUTARIH;
            rande.RANDEVUSAAT = dataObject.RANDEVUSAAT;
            rande.RANDEVUTUR = dataObject.RANDEVUTUR;
            
            db.randevular.Add(rande);
            db.SaveChanges();
            return Json(true);
        }
     
        public ActionResult AileHekimi()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AilehekimiRandevuAl(string data)
        {
            randevular rande = new randevular();
            var dataObject = JsonConvert.DeserializeObject<randevular>(data);
            var kullaniciadi = User.Identity.Name;
            var kullanici = db.user.FirstOrDefault(x => x.USERTC == kullaniciadi);
            rande.USERID = kullanici.USERID;
            rande.DOKTORID = dataObject.DOKTORID; 
            rande.RANDEVUTARIH = dataObject.RANDEVUTARIH;
            rande.RANDEVUSAAT = dataObject.RANDEVUSAAT;
            rande.RANDEVUTUR = dataObject.RANDEVUTUR;
            db.randevular.Add(rande);
            db.SaveChanges();
            return Json(true);
        }
    
        public ActionResult Arama()
        {
            return View();
        }
       
        [HttpPost]
        public ActionResult Arama(Class1 id)
        {
            var model=db.doktorlar.Where(x=>x.KLINIKID==id.KLINIKID).ToList();
            var firma = db.doktorlar.Where(x => x.KLINIKID == id.KLINIKID).Select(x => x.DOKTORID).FirstOrDefault();
            ViewBag.viewfirma = firma;
            return View("Arama",model);
        }
        public ActionResult Doktor()
        {      
            return View();
        }
        [HttpPost]
        public ActionResult DoktorRandevuAl(string data,doktorlar p1)
        {
            var u = db.doktorlar.Find(p1.DOKTORID);
            randevular rande = new randevular();
            var dataObject = JsonConvert.DeserializeObject<randevular>(data);
            var kullaniciadi = User.Identity.Name;
            var kullanici = db.user.FirstOrDefault(x => x.USERTC == kullaniciadi);
            rande.USERID = kullanici.USERID;
            rande.DOKTORID = dataObject.DOKTORID;
            rande.RANDEVUTARIH = dataObject.RANDEVUTARIH;
            rande.RANDEVUSAAT = dataObject.RANDEVUSAAT;
            rande.RANDEVUTUR = dataObject.RANDEVUTUR;
            db.randevular.Add(rande);
            db.SaveChanges();
            return Json(true);
        }
        public ActionResult Goster()
        {
            var kullaniciadi = User.Identity.Name;
            var kullanici = db.user.FirstOrDefault(x => x.USERTC == kullaniciadi);
            var model = db.randevular.Where(x => x.USERID == kullanici.USERID).ToList();
            return View(model);
        }
    }
}