using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using hastanerandevu.Models.Entities;

namespace hastanerandevu.Controllers
{
    public class AdminPController : Controller
    {
        // GET: AdminP
        hastaneEntities db = new hastaneEntities();
        public ActionResult Index()
        {
            var degerler = db.hastaneler.ToList();        
            return View(degerler);
        }
        Class1 cs = new Class1();
        [HttpGet]

        public ActionResult Ekleme()
        {
            List<SelectListItem> sehir = (from x in db.sehirler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.SEHIRAD,
                                               Value = x.SEHIRID.ToString(),
                                           }).ToList();
            ViewBag.frms = sehir;

            List<SelectListItem> degerler = (from i in db.ilceler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.ILCEAD,
                                                 Value = i.ILCEID.ToString(),
                                             }).ToList();
            ViewBag.dgr = degerler;
            ViewBag.SehirList = new SelectList(ilgetir(), "SEHIRID", "SEHIRAD");       
            return View();
        }
        [HttpPost]
        public ActionResult Ekleme(hastaneler p1)
        {
            hastaneler ab = new hastaneler();
            ab.HASTANEAD = p1.HASTANEAD;
            ab.SEHIRID= p1.SEHIRID;
            ab.ILCEID=p1.ILCEID;
            db.hastaneler.Add(ab);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var hstnlr = db.hastaneler.Find(id);
            db.hastaneler.Remove(hstnlr);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Güncelle(int id)
        {
            var ur = db.hastaneler.Find(id);
            List<SelectListItem> ils = (from x in db.sehirler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.SEHIRAD,
                                               Value = x.SEHIRID.ToString(),
                                           }).ToList();
            ViewBag.shr = ils;
            List<SelectListItem> ilces = (from i in db.ilceler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.ILCEAD,
                                                 Value = i.ILCEID.ToString(),
                                             }).ToList();
            ViewBag.ilc = ilces;
            return View("Güncelle", ur);
        }
        public ActionResult Guncel(hastaneler p1)
        {
            var u = db.hastaneler.Find(p1.HASTANEID);
            u.HASTANEAD = p1.HASTANEAD;
            var fr = db.sehirler.Where(m => m.SEHIRID == p1.sehirler.SEHIRID).FirstOrDefault();
            u.SEHIRID = fr.SEHIRID;
            var mrk = db.ilceler.Where(m => m.ILCEID == p1.ilceler.ILCEID).FirstOrDefault();
            u.ILCEID = mrk.ILCEID;

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public List<sehirler> ilgetir()
        {
            List<sehirler> sehirler = db.sehirler.ToList();

            return sehirler;
        }
        public ActionResult ilcegetir(int SEHIRID)
        {
            List<ilceler> selectlist = db.ilceler.Where(x => x.SEHIRID == SEHIRID).ToList();
            ViewBag.Ilcelist = new SelectList(selectlist, "ILCEID", "ILCEAD");
            return PartialView("ilcegoster");
        }
    }
}