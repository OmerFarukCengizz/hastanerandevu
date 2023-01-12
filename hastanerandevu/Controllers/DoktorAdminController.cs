using hastanerandevu.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc;

namespace hastanerandevu.Controllers
{
    public class DoktorAdminController : Controller
    {
        // GET: DoktorAdmin
        hastaneEntities db= new hastaneEntities();
        public ActionResult Index(string ara, int sayfa = 1)
        {
            List<doktorlar> degerler= db.doktorlar.ToList();
            return View(db.doktorlar.Where(s => s.DOKTORAD.ToLower().Contains(ara) || ara == null).ToList().ToPagedList(sayfa, 15));
        }
        Class1 cs = new Class1();
        public ActionResult Ekleme()
        {
            List<SelectListItem> firmas = (from x in db.sehirler.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.SEHIRAD,
                                               Value = x.SEHIRID.ToString(),
                                           }).ToList();
            ViewBag.frms = firmas;
            
            List<SelectListItem> degerler = (from i in db.hastaneler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.HASTANEAD,
                                                 Value = i.HASTANEID.ToString(),
                                             }).ToList();
            ViewBag.dgr = degerler;
            List<SelectListItem> marks = (from n in db.klinikler.ToList()
                                          select new SelectListItem
                                          {
                                              Text = n.KLINIKAD,
                                              Value = n.KLINIKID.ToString(),
                                          }).ToList();
            ViewBag.mrklr = marks;
            ViewBag.HastaList = new SelectList(hastaneget(), "HASTANEID", "HASTANEAD");
            return View();
        }
        [HttpPost]
        public ActionResult Ekleme(doktorlar p1)
        {
            doktorlar ab = new doktorlar();
            ab.HASTANEID = p1.HASTANEID;
            ab.KLINIKID = p1.KLINIKID;
            ab.DOKTORAD = p1.DOKTORAD;
         
            db.doktorlar.Add(ab);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var urnler = db.doktorlar.Find(id);
            db.doktorlar.Remove(urnler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Güncelle(int id)
        {
            var ur = db.doktorlar.Find(id);
            List<SelectListItem> degerler = (from i in db.hastaneler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.HASTANEAD,
                                                 Value = i.HASTANEID.ToString(),
                                             }).ToList();
            ViewBag.dgr = degerler;
            List<SelectListItem> marks = (from n in db.klinikler.ToList()
                                          select new SelectListItem
                                          {
                                              Text = n.KLINIKAD,
                                              Value = n.KLINIKID.ToString(),
                                          }).ToList();
            ViewBag.mrklr = marks;
            return View("Güncelle", ur);
        }
        public ActionResult Guncel(doktorlar p1)
        {
            var u = db.doktorlar.Find(p1.DOKTORID);
            var fr = db.hastaneler.Where(m => m.HASTANEID == p1.hastaneler.HASTANEID).FirstOrDefault();
            u.HASTANEID = fr.HASTANEID;
            var mrk = db.klinikler.Where(m => m.KLINIKID == p1.klinikler.KLINIKID).FirstOrDefault();
            u.KLINIKID = mrk.KLINIKID;
            u.DOKTORAD = p1.DOKTORAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public List<hastaneler> hastaneget()
        {
            List<hastaneler> hastaneler = db.hastaneler.ToList();

            return hastaneler;
        }


        public ActionResult klinikgetir(int HASTANEID)
        {
            List<klinikler> selectlist = db.klinikler.Where(x => x.HASTANEID == HASTANEID).ToList();
            ViewBag.Kliniklist = new SelectList(selectlist, "KLINIKID", "KLINIKAD");
            return PartialView("klinikgoster");
        }
    }
}