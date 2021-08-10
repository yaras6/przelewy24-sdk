using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace P24.UnitTest
{
    [TestClass]
    public class Przelewy24Tests
    {
        private readonly Przelewy24 _przelewy24;
        private readonly int _userId;

        public Przelewy24Tests()
        {
            _userId = int.Parse(ConfigurationManager.AppSettings["P24.UserId"]);
            _przelewy24 = new Przelewy24(ConfigurationManager.AppSettings["P24.BaseUrl"],
                _userId,
                ConfigurationManager.AppSettings["P24.UserSecret"],
                ConfigurationManager.AppSettings["P24.CRC"]);
            
        }

        [TestMethod]
        public async Task TestConnectionTest()
        {
            var response = await _przelewy24.TestConnectionAsync();

            Assert.AreEqual(response.Data, true);
        }

        [TestMethod]
        public async Task NewTransactionTest()
        {
            var data = new P24TransactionRequest
            {
                MerchantId = _userId,
                PosId = _userId,
                SessionId = "test1",
                Amount = 100,
                Currency = "PLN",
                Description = "Testowe zamówienie z UnitTest",
                Email = "yaras6@wp.pl",
                Country = "PL",
                Language = "pl",
                UrlReturn = "http://localhost/przelewy24-test",
            };
            var response = await _przelewy24.NewTransactionAsync(data);

            Assert.IsNull(response.Error);
            Assert.IsNotNull(response.Data.Token);
        }
        
    }
}
