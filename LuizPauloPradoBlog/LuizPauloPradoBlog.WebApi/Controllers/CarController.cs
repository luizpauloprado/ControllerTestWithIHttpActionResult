using LuizPauloPradoBlog.Repository;
using LuizPauloPradoBlog.Repository.Interface;
using LuizPauloPradoBlog.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LuizPauloPradoBlog.WebApi.Controllers
{
    public class CarController : ApiController
    {
        private ICarRepository _repository;

        public CarController(ICarRepository repository)
        {
            _repository = repository;
        }

        public IHttpActionResult Get()
        {
            var cars = _repository.GetAll();

            return Ok(cars);
        }

        public IHttpActionResult Get(string model)
        {
            var car = _repository.Get(model);

            if (car == null)
                return NotFound();
            
            return Ok(car);
        }

        public IHttpActionResult Post(Car car)
        {
            _repository.Add(car);

            return Created("localhost", car);
        }
    }
}
