using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetTyp_A1B1C1_Gleichseitig()
        {
            var logger = new GetTypLoggerMock();
            var dreieck = new Dreieck(logger);
            var a = 1;
            var b = 1;
            var c = 1;
            var expected = DreieckTyp.Gleichseitig;

            var actual = dreieck.GetTyp(a, b, c);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTyp_A1B2C2_Gleichschenklig()
        {
            var logger = new GetTypLoggerMock();
            var dreieck = new Dreieck(logger);
            var a = 1;
            var b = 2;
            var c = 2;
            var expected = DreieckTyp.Gleichschenklig;

            var actual = dreieck.GetTyp(a, b, c);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTyp_A1B3C4_Normal()
        {
            var mock = new Mock<IGetTypLogger>();
            mock.Setup(m => m.LogType(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DreieckTyp>()));
            var dreieck = new Dreieck(mock.Object);
            var a = 1;
            var b = 3;
            var c = 4;
            var expected = DreieckTyp.Normal;

            var actual = dreieck.GetTyp(a, b, c);

            Assert.AreEqual(expected, actual);
        }
    }

    public class Dreieck
    {
        private IGetTypLogger _logger;

        public Dreieck(IGetTypLogger logger)
        {
            _logger = logger;
        }

        public DreieckTyp GetTyp(int a, int b, int c)
        {
            DreieckTyp typ;

            if (a == b && b == c && c == a)
                typ = DreieckTyp.Gleichseitig;
            else if (a == b || b == c || c == a)
                typ = DreieckTyp.Gleichschenklig;
            else
                typ = DreieckTyp.Normal;

            _logger.LogType(a,b,c,typ);
            return typ;
        }
    }

    public interface IGetTypLogger
    {
        void LogType(int a, int b, int c, DreieckTyp typ);
    }

    public class GetTypLoggerMock : IGetTypLogger
    {
        public void LogType(int a, int b, int c, DreieckTyp typ)
        {
        }
    }

    public class GetTypLogger : IGetTypLogger
    {
        private List<string> _typeRequests;

        public GetTypLogger()
        {
            _typeRequests = new List<string>();
        }

        public void LogType(int a, int b, int c, DreieckTyp typ)
        {
            throw new Exception();
            _typeRequests.Add(a + "/" + b + "/" + "/" + c + " = " + typ);
        }
    }



    public enum DreieckTyp
    {
        Normal,
        Gleichseitig,
        Gleichschenklig
    }
}
