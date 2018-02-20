using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using ProjektPracownia.Controllers;
using ProjektPracownia.Data;
using ProjektPracownia.Models;
using System;
using System.Threading.Tasks;

namespace ProjektPracownia.tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public async Task CarMakesRedirectsToCarEdit()
        {
            var options = new DbContextOptionsBuilder<FaultsContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            options.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var _dbContext = new FaultsContext(options.Options);

            var carMakesControler = new CarMakesController(_dbContext);
            var result = await carMakesControler.Index() as RedirectToActionResult;


            Assert.AreEqual(result.ControllerName, "CarEdit");
            Assert.AreEqual(result.ActionName, "Index");

        }

        [TestMethod]
        public async Task CarMakesDetailDoesntAcceptNull()
        {
            var options = new DbContextOptionsBuilder<FaultsContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            options.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var _dbContext = new FaultsContext(options.Options);

            var carMakesControler = new CarMakesController(_dbContext);
            var result = await carMakesControler.Details(null) as ActionResult;


            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public async Task FaultSearchReturnsEmptyJsonIfVersionDoesntExist()
        {
            var options = new DbContextOptionsBuilder<FaultsContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            options.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var _dbContext = new FaultsContext(options.Options);

            var carMakesControler = new FaultSearchController(_dbContext);
            var result = await carMakesControler.GetVersionFaults("500") as JsonResult;
            string json =  JsonConvert.SerializeObject(result.Value);
            Console.Write(json);


            Assert.AreEqual(json, "[]");

        }

        [TestMethod]
        public async Task FaultSearchReturnsNotFoundIfVersionIs0 ()
        {
            var options = new DbContextOptionsBuilder<FaultsContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            options.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var _dbContext = new FaultsContext(options.Options);

            var carMakesControler = new FaultSearchController(_dbContext);
            var result = await carMakesControler.GetVersionFaults("0") as ActionResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public async Task CarSearchHasProperTextSet()
        {
            var options = new DbContextOptionsBuilder<FaultsContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            options.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var _dbContext = new FaultsContext(options.Options);

            var carMakesControler = new CarSearchController(_dbContext);
            var result = await carMakesControler.Index() as ViewResult;

            Assert.AreEqual(result.ViewData["faultsText"], "Cars with issue");

        }

        [TestMethod]
        public async Task FaultCantBeDisconnectedIfNoVersionGiven()
        {
            var options = new DbContextOptionsBuilder<FaultsContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            options.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var _dbContext = new FaultsContext(options.Options);

            var carMakesControler = new CarFaultsController(_dbContext);
            var result = await carMakesControler.RemoveConnection(2,null) as ActionResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public async Task FaultCantBeDisconnectedIfIDLesserThan0()
        {
            var options = new DbContextOptionsBuilder<FaultsContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            options.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var _dbContext = new FaultsContext(options.Options);

            var carMakesControler = new CarFaultsController(_dbContext);
            var result = await carMakesControler.RemoveConnection(2, null-3) as ActionResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public async Task FaultCantBeDisconnectedIfIDVersionNotFound()
        {
            var options = new DbContextOptionsBuilder<FaultsContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            options.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var _dbContext = new FaultsContext(options.Options);

            var carMakesControler = new CarFaultsController(_dbContext);
            var result = await carMakesControler.RemoveConnection(2,32343) as ActionResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public async Task VersionCantBeEditedWithoutIDSpecified()
        {
            var options = new DbContextOptionsBuilder<FaultsContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            options.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var _dbContext = new FaultsContext(options.Options);

            var carMakesControler = new CarVersionsController(_dbContext);
            var result = await carMakesControler.Edit(null) as ActionResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public async Task VersionCantBeEditedWithoutVersionExisting()
        {
            var options = new DbContextOptionsBuilder<FaultsContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            options.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var _dbContext = new FaultsContext(options.Options);

            var carMakesControler = new CarVersionsController(_dbContext);
            var result = await carMakesControler.Edit(-1) as ActionResult;

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }
    }
}
