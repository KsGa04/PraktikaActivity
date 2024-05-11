using Microsoft.VisualStudio.TestTools.UnitTesting;
using PraktikaActivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject
{
    /// <summary>
    /// Сводное описание для ValidationPhoneTest
    /// </summary>
    [TestClass]
    public class ValidationPhoneTest
    {
        [TestMethod]
        public void TestValidPhoneNumber()
        {
            // Arrange
            string validPhoneNumber = "+7(123) - 456-78-90";

            // Act
            bool isPhoneNumberValid = RgistrationOfJuryModerator.IsPhoneNumberValid(validPhoneNumber);

            // Assert
            Assert.IsTrue(isPhoneNumberValid, "Валидный номер телефона не был распознан как валидный.");
        }

        [TestMethod]
        public void TestInvalidPhoneNumberWithIncorrectFormat()
        {
            // Arrange
            string invalidPhoneNumber = "7(123)-456-78-90";

            // Act
            bool isPhoneNumberValid = RgistrationOfJuryModerator.IsPhoneNumberValid(invalidPhoneNumber);

            // Assert
            Assert.IsFalse(isPhoneNumberValid, "Невалидный номер телефона был распознан как валидный.");
        }

        [TestMethod]
        public void TestInvalidPhoneNumberWithMissingPrefix()
        {
            // Arrange
            string invalidPhoneNumber = "(123) - 456-78-90";

            // Act
            bool isPhoneNumberValid = RgistrationOfJuryModerator.IsPhoneNumberValid(invalidPhoneNumber);

            // Assert
            Assert.IsFalse(isPhoneNumberValid, "Невалидный номер телефона был распознан как валидный.");
        }

        [TestMethod]
        public void TestInvalidPhoneNumberWithMissingDigits()
        {
            // Arrange
            string invalidPhoneNumber = "+7(123) - 45-78-90";

            // Act
            bool isPhoneNumberValid = RgistrationOfJuryModerator.IsPhoneNumberValid(invalidPhoneNumber);

            // Assert
            Assert.IsFalse(isPhoneNumberValid, "Невалидный номер телефона был распознан как валидный.");
        }

        [TestMethod]
        public void TestEmptyPhoneNumber()
        {
            // Arrange
            string emptyPhoneNumber = "";

            // Act
            bool isPhoneNumberValid = RgistrationOfJuryModerator.IsPhoneNumberValid(emptyPhoneNumber);

            // Assert
            Assert.IsFalse(isPhoneNumberValid, "Пустая строка была распознана как валидный номер телефона.");
        }
    }
}
