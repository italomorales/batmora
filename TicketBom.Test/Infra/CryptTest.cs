using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicketBom.Infra.Crypto;

namespace TicketBom.Test.Infra
{
    [TestClass]
    public class CryptTest : BaseTest
    {

        [TestMethod]
        public void ShouldEncryptDecrypt()
        {
            const string text = "email=italo.morales@hotmail.com";

            var crypted = text.EncryptUrl();

            var decrypted = crypted.DecryptUrl();

            Assert.IsTrue(decrypted == text);

        }
    }
}
