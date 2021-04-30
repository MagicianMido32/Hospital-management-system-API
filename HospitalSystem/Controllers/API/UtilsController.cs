using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalSystem.Models;

namespace HospitalSystem.Controllers.API
{
    public class UtilsController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public UtilsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        [HttpGet]
        public IHttpActionResult GetHospitalByName(string name)
        {
            return Ok(_context.Hospitals.Single(h=>h.Name==name));
        }

        [HttpGet]
        public IHttpActionResult QueryHospitalsByName(string query)
        {
            return Ok(_context.Hospitals.Where(h => h.Name.Contains(query)).ToList());
        }

        [HttpGet]
        public IHttpActionResult FindBlood(int id, string type)
        {
            var hospitalInDb = _context.Hospitals.Single(h => h.Id == id);
            var amount = 0;
            switch (type)
            {
                case "A_Plus":
                    amount = hospitalInDb.A_Plus;
                    break;

                case "A_Minus":
                    amount = hospitalInDb.A_Minus;
                    break;

                case "B_Plus":
                    amount = hospitalInDb.B_Plus;
                    break;

                case "B_Minus":
                    amount = hospitalInDb.B_Minus;
                    break;

                case "AB_Plus":
                    amount = hospitalInDb.AB_Plus;
                    break;

                case "AB_Minus":
                    amount = hospitalInDb.AB_Minus;
                    break;

                case "O_Plus":
                    amount = hospitalInDb.O_Plus;
                    break;

                case "O_Minus":
                    amount = hospitalInDb.O_Minus;
                    break;
            }
            return Ok(amount);
        }

        [HttpGet]
        public IHttpActionResult FindBed(int id)
        {
            var hospitalInDb = _context.Hospitals.Single(h => h.Id == id);
            return Ok(hospitalInDb.NumberOfBedsAvailable);
        }

        [HttpGet]
        public IHttpActionResult FindNearestHospital(string currentCoordinates)
        {
            //33.7511839,31.4431256


            var coords = currentCoordinates.Split(',');

            var longitude = double.Parse(coords[0]);

            var latitude = double.Parse(coords[1]);

            var hospitals = _context.Hospitals.ToList();


            var minDistance = double.MaxValue;
            var minHospital = hospitals[0];
            foreach (var hospital in hospitals)
            {
                var coords2 = hospital.Coordinates.Split(',');
                var longitude2 = double.Parse(coords2[0]);
                var latitude2 = double.Parse(coords2[1]);

                var distance = DistanceAlgorithm.DistanceBetweenPlaces(longitude, latitude, longitude2, latitude2);

                if (!(distance < minDistance)) continue;
                minDistance = distance;
                minHospital = hospital;

            }
            return Ok(minHospital);
        }

        [HttpGet]
        public IHttpActionResult FindNearestHospitals(string currentCoordinates)
        {
            //33.7511839,31.4431256


            var coords = currentCoordinates.Split(',');

            var longitude = double.Parse(coords[0]);

            var latitude = double.Parse(coords[1]);

            var hospitals = _context.Hospitals;


            var hospitalsDistances = new List<KeyValuePair<string, double>>();

            foreach (var hospital in hospitals)
            {
                var coords2 = hospital.Coordinates.Split(',');
                var longitude2 = double.Parse(coords2[0]);
                var latitude2 = double.Parse(coords2[1]);

                var distance = DistanceAlgorithm.DistanceBetweenPlaces(longitude, latitude, longitude2, latitude2);
                hospitalsDistances.Add(new KeyValuePair<string, double>(hospital.Name, distance));

            }

            return Ok(hospitalsDistances.OrderBy(p => p.Value).ToList());
        }

        [HttpGet]
        public IHttpActionResult ReserveBed(int id, int number)
        {
            var hospital = _context.Hospitals.Single(h => h.Id == id);
            if (hospital.NumberOfBedsAvailable <= 0)
                return BadRequest("No more beds available");
            hospital.NumberOfBedsAvailable -= number;
            _context.SaveChanges();
            return Ok($"{number} beds reserved in {hospital.Name} : {hospital.Id}");
        }

        [HttpGet]
        public IHttpActionResult ReleaseBed(int id, int number)
        {
            var hospital = _context.Hospitals.Single(h => h.Id == id);

            if (hospital.NumberOfBedsAvailable + number > hospital.NumberOfBeds)
                return BadRequest("No reserved beds or number is too big");
            hospital.NumberOfBedsAvailable += number;
            _context.SaveChanges();
            return Ok($"{number} beds released in {hospital.Name} : {hospital.Id}");
        }



    }
}
