using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LuizPauloPradoBlog.Repository.Interface;
using LuizPauloPradoBlog.Repository;
using LuizPauloPradoBlog.Repository.Model;
using System.Collections.Generic;
using LuizPauloPradoBlog.WebApi.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Web.Http.Results;

namespace LuizPauloPradoBlog.WebApi.Tests
{
    [TestClass]
    public class CarControllerTest
    {
        private ICarRepository _repository;

        [TestInitialize]
        public void Initialize()
        {
            var carSamples = new List<Car>();
            carSamples.Add(new Car() { Name = "Fiesta", Model = "Fiesta SE", YearOfManufacture = 2015 });
            carSamples.Add(new Car() { Name = "Golf", Model = "Golf Sport", YearOfManufacture = 2015 });
            carSamples.Add(new Car() { Name = "Civic", Model = "Civic S", YearOfManufacture = 2016 });

            _repository = new CarRepository(carSamples);
        }

        [TestMethod]
        public void ShouldGetAllCars()
        {
            var controller = new CarController(_repository);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var result = controller.Get() as OkNegotiatedContentResult<IEnumerable<Car>>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(3, result.Content.Count());
        }

        [TestMethod]
        public void ShouldGetCar()
        {
            var controller = new CarController(_repository);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var result = controller.Get("Fiesta SE") as OkNegotiatedContentResult<Car>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(result.Content.Model, "Fiesta SE");
        }

        [TestMethod]
        public void ShouldPostCar()
        {
            var controller = new CarController(_repository);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var newCar = new Car() { Name = "IX 35", Model = "IX 35 Special", YearOfManufacture = 2016 };

            var result = controller.Post(newCar) as CreatedNegotiatedContentResult<Car>;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.AreEqual(result.Content.Name, newCar.Name);
            Assert.AreEqual(result.Content.Model, newCar.Model);
            Assert.AreEqual(result.Content.YearOfManufacture, newCar.YearOfManufacture);
        }

        [TestMethod]
        public void ShouldReturnNotFound()
        {
            var controller = new CarController(_repository);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var result = controller.Get("Ferrari F7");

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
