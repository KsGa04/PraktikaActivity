using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using PraktikaActivity;

namespace UnitTestProject
{
    [TestClass]
    public class AuthorizationTest
    {
        [TestMethod]
        public void TestSuccessfulAuthorization()
        {
            // Arrange
            int userId = 1;
            string password = "Abc12345!";

            // Act
            bool isAuthorized = Authorization.PerformAuthorization(userId, password);

            // Assert
            Assert.IsTrue(isAuthorized, "Пользователь был успешно авторизован.");
        }
        [TestMethod]
        public void TestUnsuccessfulAuthorizationWithInvalidPassword()
        {
            // Arrange
            int userId = 1;
            string invalidPassword = "invalidPassword";

            // Act
            bool isAuthorized = Authorization.PerformAuthorization(userId, invalidPassword);

            // Assert
            Assert.IsFalse(isAuthorized, "Пользователь был авторизован с неверным паролем.");
        }
        [TestMethod]
        public void TestUnsuccessfulAuthorizationWithNonexistentUser()
        {
            // Arrange
            int nonexistentUserId = 999; // Укажите несуществующий ID пользователя
            string password = "validPassword"; // Укажите действительный пароль

            // Act
            bool isAuthorized = Authorization.PerformAuthorization(nonexistentUserId, password);

            // Assert
            Assert.IsFalse(isAuthorized, "Несуществующий пользователь был авторизован.");
        }
        [TestMethod]
        public void TestParticipantAuthorization()
        {
            // Arrange
            int userId = 2;
            string password = "wOMJfO$*00";

            // Act
            bool isAuthorized = Authorization.PerformAuthorization(userId, password);

            // Assert
            Assert.IsTrue(isAuthorized, "Пользователь с ролью Participant был успешно авторизован.");
        }

        [TestMethod]
        public void TestModeratorAuthorization()
        {
            // Arrange
            int userId = 25; // ID пользователя с ролью Moderator
            string password = "XM76e9Vu8y";

            // Act
            bool isAuthorized = Authorization.PerformAuthorization(userId, password);

            // Assert
            Assert.IsTrue(isAuthorized, "Пользователь с ролью Moderator был успешно авторизован.");
        }

        [TestMethod]
        public void TestJuryAuthorization()
        {
            // Arrange
            int userId = 43; // ID пользователя с ролью Jury
            string password = "br@ILZ";

            // Act
            bool isAuthorized = Authorization.PerformAuthorization(userId, password);

            // Assert
            Assert.IsTrue(isAuthorized, "Пользователь с ролью Jury был успешно авторизован.");
        }

        [TestMethod]
        public void TestOrganizerAuthorization()
        {
            // Arrange
            int userId = 52; // ID пользователя с ролью Organizer
            string password = "Gm87XHb56b";

            // Act
            bool isAuthorized = Authorization.PerformAuthorization(userId, password);

            // Assert
            Assert.IsTrue(isAuthorized, "Пользователь с ролью Organizer был успешно авторизован.");
        }

        [TestMethod]
        public void TestInvalidRoleAuthorization()
        {
            // Arrange
            int userId = 5; // ID пользователя с недействительной ролью
            string password = "validPassword";

            // Act
            bool isAuthorized = Authorization.PerformAuthorization(userId, password);

            // Assert
            Assert.IsFalse(isAuthorized, "Пользователь с недействительной ролью был успешно авторизован.");
        }
    }
}
