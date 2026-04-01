using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using Anastasia423WPF;

namespace OhmsLawCalculator.Tests
{
    [TestClass]
    public class OhmLawCalculatorTests
    {
        [TestMethod]
        public void CalculateCurrent_WithValidData_ReturnsCorrectResult()
        {
            // Arrange
            double voltage = 10;
            double resistance = 2;

            // Act
            double current = voltage / resistance;

            // Assert
            Assert.AreEqual(5.0, current);
        }

        [TestMethod]
        public void CalculateVoltage_WithValidData_ReturnsCorrectResult()
        {
            // Arrange
            double current = 3;
            double resistance = 4;

            // Act
            double voltage = current * resistance;

            // Assert
            Assert.AreEqual(12.0, voltage);
        }

        [TestMethod]
        public void CalculateResistance_WithValidData_ReturnsCorrectResult()
        {
            // Arrange
            double voltage = 20;
            double current = 4;

            // Act
            double resistance = voltage / current;

            // Assert
            Assert.AreEqual(5.0, resistance);
        }

        [TestMethod]
        public void CalculateCurrent_WithDecimalValues_ReturnsCorrectResult()
        {
            // Arrange
            double voltage = 10.5;
            double resistance = 2.5;

            // Act
            double current = voltage / resistance;

            // Assert
            Assert.AreEqual(4.2, current, 0.01);
        }

        [TestMethod]
        public void CalculateVoltage_WithLargeValues_ReturnsCorrectResult()
        {
            // Arrange
            double current = 100;
            double resistance = 1000;

            // Act
            double voltage = current * resistance;

            // Assert
            Assert.AreEqual(100000, voltage);
        }

        [TestMethod]
        [DataRow("10.5", true)]
        [DataRow("abc", false)]
        [DataRow("", false)]
        public void ParseInput_WithVariousFormats_ReturnsCorrectResult(string input, bool shouldBeValid)
        {
            // Act
            bool isValid = double.TryParse(input.Replace(".", ","),
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out double result);

            // Assert
            Assert.AreEqual(shouldBeValid, isValid);
        }
    }
}