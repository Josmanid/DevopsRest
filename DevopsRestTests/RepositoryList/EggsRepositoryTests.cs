using Microsoft.VisualStudio.TestTools.UnitTesting;
using DevopsRest.RepositoryList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevopsRest.Models;
using System.Reflection;

namespace DevopsRest.RepositoryList.Tests
{
    [TestClass()]
    public class EggsRepositoryTests
    {

        private EggsRepository _repository;
        [TestInitialize]
        public void setup() {
            //Arrange
            _repository = new EggsRepository();
        }
        [TestMethod()]
        public void GetEggByIdTest() {
            // Act i already have som eggs to look at
            Egg PositiveEgg = _repository.GetById(1);
            Egg NegativeEgg = _repository.GetById(999);

            // Assert
            Assert.AreEqual(1,PositiveEgg.Id);
            Assert.AreEqual("Arla",PositiveEgg.Name);
            Assert.IsNull(NegativeEgg);

        }

    }
}