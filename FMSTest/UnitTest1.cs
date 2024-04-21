using System;
using System.Collections.Generic;
using System.Linq;
using FlightManagementSystem.Controllers;
using FlightManagementSystem.Data;
using FlightManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FMSTest
{
    [TestClass]
    public class FlightControllerTests
    {
        [TestMethod]
        public void GetFlights_ReturnsListOfFlights()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var flights = new List<Flight>
                {
                    new Flight { Id = 1, FlightNumber = "FL123", DepartureDate = DateTime.Now, DepartureLocation = "New York", ArrivalLocation = "Los Angeles", AircraftType = "Boeing 737" },
                    new Flight { Id = 2, FlightNumber = "FL456", DepartureDate = DateTime.Now, DepartureLocation = "Los Angeles", ArrivalLocation = "Chicago", AircraftType = "Airbus A320" },
                    new Flight { Id = 3, FlightNumber = "FL789", DepartureDate = DateTime.Now, DepartureLocation = "Chicago", ArrivalLocation = "New York", AircraftType = "Boeing 787" }
                };
                context.Flights.AddRange(flights);
                context.SaveChanges();
            }

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new FlightController(context);

                var result = controller.GetFlights().Result;

                Assert.IsNotNull(result);
                var actionResult = result as ActionResult<IEnumerable<Flight>>;
                Assert.IsNotNull(actionResult);
                var flights = actionResult.Value;
                Assert.IsNotNull(flights);
                Assert.AreEqual(3, flights.Count());
            }
        }

        [TestMethod]
        public async Task AddFlight_ReturnsCreatedResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new FlightController(context);

                var newFlight = new Flight
                {
                    FlightNumber = "FL001",
                    DepartureDate = DateTime.Now,
                    DepartureLocation = "New York",
                    ArrivalLocation = "Los Angeles",
                    AircraftType = "Boeing 747"
                };
                var result = await controller.PostFlight(newFlight);

                Console.WriteLine("Result: " + result);

                Assert.IsNotNull(result);
                Assert.IsInstanceOfType(result, typeof(ActionResult<Flight>));

                var actionResult = result as ActionResult<Flight>;
                Assert.IsNotNull(actionResult);

                var createdResult = actionResult.Result as CreatedAtActionResult;
                Assert.IsNotNull(createdResult);

                Assert.AreEqual(201, createdResult.StatusCode);
                Assert.AreEqual("GetFlight", createdResult.ActionName);
            }
        }
        [TestMethod]
        public async Task UpdateFlight_ReturnsNoContentResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new FlightController(context);

                var updatedFlight = new Flight
                {
                    Id = 1,
                    FlightNumber = "FL001",
                    DepartureDate = DateTime.Now.AddDays(1),
                    DepartureLocation = "New York",
                    ArrivalLocation = "Los Angeles",
                    AircraftType = "Boeing 747"
                };
                var result = await controller.PutFlight(1, updatedFlight);

                Assert.IsInstanceOfType(result, typeof(NoContentResult));
            }
        }

        [TestMethod]
        public async Task DeleteFlight_ReturnsNoContentResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new FlightController(context);
                var result = await controller.DeleteFlight(1);

                Assert.IsInstanceOfType(result, typeof(NoContentResult));
            }
        }
    }
}
