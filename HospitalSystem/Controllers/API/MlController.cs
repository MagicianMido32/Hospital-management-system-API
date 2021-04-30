using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HospitalSystem.Models;

namespace HospitalSystem.Controllers.API
{
    public class MlController : ApiController
    {
        private readonly ApplicationDbContext _context;
        public MlController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        
        [HttpGet]
        public IHttpActionResult GetWhenNoBeds(int id)
        {
            return Ok("2");
        }

        [HttpGet]
        public IHttpActionResult ForecastBeds(int id, int timeSteps)
        {
            return Ok("2");
        }

        [HttpGet]
        public IHttpActionResult GetWhenNoBlood(int id, string type)
        {
            return Ok("2");
        }

        [HttpGet]
        public IHttpActionResult ForecastBlood(int id, int timeSteps, string type)
        {
            return Ok("2");
        }

    }
}
