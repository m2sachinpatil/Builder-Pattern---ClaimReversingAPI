using Abstract_Layer.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Claim_Reversing_Test.Repositories
{
    [TestFixture]
    public class DataLoaderTest
    {
        private DataLoader _dataLoader;

        [SetUp]
        public void SetUp()
        {
            _dataLoader = new DataLoader();
        }

        [Test]
        public void Load_Data_In_Data_Table_Success()
        {
            // Arrange
            var lines = new List<string>
            {
                "Product, Origin Year, Development Year, Incremental Value",
                "Comp, 1992, 1992, 110.0",
                "Comp, 1992, 1993, 170.0",
                "Comp, 1993, 1993, 200.0"
            };

            //Act
            var result = _dataLoader.LoadData(lines);


            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Rows.Count, 3);

        }

        [Test]
        public void Load_Data_In_Data_Table_Empty()
        {
            // Arrange
            var lines = new List<string>();

            //Act
            var result = _dataLoader.LoadData(lines);


            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Rows.Count, 0);
        }
        
        [Test]
        public void Load_Data_InValid_Data_Table_Exception_Catch()
        {
            // Arrange
            var lines = new List<string>
            {
                "Comp, 1992, 1992, 110.0",
                "Comp, 1992, 1993, 170.0",
                "Comp, 1993, 1993, 200.0"
            };

            //Act
            var ex = Assert.Catch<InvalidOperationException>(() => _dataLoader.LoadData(lines));

            //Assert
            Assert.AreEqual("Cannot map data.", ex.Message);
   
        }
    }
}
