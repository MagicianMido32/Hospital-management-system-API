using System.Linq;
using System.Web.Mvc;
using HospitalSystem.Models;

namespace HospitalSystem.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HospitalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);  
        }

        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        public ActionResult New()
        {
            var hospital = new Hospital();
            return View("HospitalForm", hospital);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Save(Hospital hospital)
        {
            if (!ModelState.IsValid)
            {
                return View("HospitalForm", hospital);
            }

            if (hospital.Id == 0 )
            {
                _context.Hospitals.Add(hospital);
                _context.SaveChanges();
            }
            else
            {
                var hospitalInDb = _context.Hospitals.Single(h => h.Id == hospital.Id);
                TryUpdateModel(hospitalInDb);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Hospitals");
        }


        public ActionResult Edit(int id)
        {
            var hospital = _context.Hospitals.SingleOrDefault(h => h.Id == id);
            if (hospital==null)
            {
                return HttpNotFound();
            }

            return View("HospitalForm", hospital);
        }


        public ActionResult Delete(int id)
        {
            var hospital = _context.Hospitals.SingleOrDefault(h => h.Id == id);
            if (hospital==null)
            {
                return HttpNotFound();
            }

            _context.Hospitals.Remove(hospital);
            _context.SaveChanges();
            return RedirectToAction("Index", "Hospitals");
        }
    }
}