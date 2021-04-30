using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalSystem.Models;

namespace HospitalSystem.Controllers.API
{
    public class HospitalsController : ApiController
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

        //GET /api/hospitals

        [HttpGet]
        public IHttpActionResult GetHospitals(int? id = null)
        {
            if (id == null)
            {
                return Ok(_context.Hospitals.ToList());

            }

            return Ok(_context.Hospitals.Single(h => h.Id == id));

        }

        // need a dto here , also the problem of id
        [HttpPost]
        public IHttpActionResult CreateHospital(Hospital hospital)
        {
            _context.Hospitals.Add(hospital);
            _context.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + hospital.Id), hospital);


        }

        [HttpPut]
        public IHttpActionResult UpdateHospital(int id, Hospital hospital)
        {
            var hospitalInDb = _context.Hospitals.Single(h => h.Id == id);

            MapHospital(hospitalInDb, hospital);
            _context.SaveChanges();
            return Ok(hospitalInDb);
        }

        [HttpDelete]
        public IHttpActionResult DeleteHospital(int id)
        {
            var hospitalInDb = _context.Hospitals.Single(h => h.Id == id);
            _context.Hospitals.Remove(hospitalInDb);
            _context.SaveChanges();
            return Ok();
        }

        private static void MapHospital(Hospital hospitalInDb, Hospital hospital)
        {
            hospitalInDb.Name = hospital.Name;
            hospitalInDb.Location = hospital.Location;
            hospitalInDb.Coordinates = hospital.Coordinates;
            hospitalInDb.Phone = hospital.Phone;
            hospitalInDb.NumberOfBeds = hospital.NumberOfBeds;
            hospitalInDb.NumberOfBedsAvailable = hospital.NumberOfBedsAvailable;
            hospitalInDb.NumberOfBloodBags = hospital.NumberOfBloodBags;

            hospitalInDb.A_Plus = hospital.A_Plus;
            hospitalInDb.A_Minus = hospital.A_Minus;
            hospitalInDb.B_Plus = hospital.B_Plus;
            hospitalInDb.B_Minus = hospital.B_Minus;
            hospitalInDb.AB_Plus = hospital.AB_Plus;
            hospitalInDb.AB_Minus = hospital.AB_Minus;
            hospitalInDb.O_Plus = hospital.O_Plus;
            hospitalInDb.O_Minus = hospital.O_Minus;


        }

        /*===============================*/
    }
}
