using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using hastanerandevu.Models.Entities;

namespace hastanerandevu.Controllers
{
    public class KlinikAdminController : Controller
    {
        // GET: Kategoriler
        hastaneEntities db = new hastaneEntities();
        public ActionResult Index()
        {
            var degerler = db.klinikler.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult Ekleme()
        {
            List<SelectListItem> ils = (from x in db.hastaneler.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.HASTANEAD,
                                            Value = x.HASTANEID.ToString(),
                                        }).ToList();
            ViewBag.klk = ils;
            return View();
        }
        [HttpPost]
        public ActionResult Ekleme(klinikler p1)
        {
            var ktg = db.hastaneler.Where(m => m.HASTANEID == p1.hastaneler.HASTANEID).FirstOrDefault();
            p1.hastaneler = ktg;
            db.klinikler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var kln = db.klinikler.Find(id);
            db.klinikler.Remove(kln);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Güncelle(int id)
        {
            var klnk = db.klinikler.Find(id);
            List<SelectListItem> ils = (from x in db.hastaneler.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.HASTANEAD,
                                            Value = x.HASTANEID.ToString(),
                                        }).ToList();
            ViewBag.klk = ils;
            return View("Güncelle", klnk);
        }
        public ActionResult Guncel(klinikler p1)
        {
            var k = db.klinikler.Find(p1.KLINIKID);
            k.KLINIKAD = p1.KLINIKAD;
            var fr = db.hastaneler.Where(m => m.HASTANEID == p1.hastaneler.HASTANEID).FirstOrDefault();
            k.HASTANEID = fr.HASTANEID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}