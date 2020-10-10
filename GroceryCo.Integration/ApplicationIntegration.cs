using System;
using System.IO;
using Xunit;

namespace GroceryCo.Integration
{
    public class ApplicationIntegration
    {
        [Fact]
        public void ShouldPrintInvoice()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;

            string testDataDirectory = Path.Join(path.Replace("/bin/Debug/netcoreapp3.1/", ""), "TestData");

            string invoice = new Application().PrintInvoice(testDataDirectory);
            
            Assert.Equal("Apple 1.99\n-0.5Orange 1.39\n-0.695Total 2.185", invoice);
        }
    }
}