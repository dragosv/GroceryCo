namespace GroceryCo.Integration
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions.TestingHelpers;
    using Xunit;

    public class ApplicationIntegration
    {
        [Fact]
        public void ShouldPrintInvoice()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { Path.Combine(folder, "Order.json"), new MockFileData("{\"Items\":[{\"Product\":\"Apple\"},{\"Product\":\"Orange\"}]}") },
                { Path.Combine(folder, "Products.json"), new MockFileData("[{\"Name\":\"Apple\",\"Price\":1.99,\"Categories\":[\"fruit\"]},{\"Name\":\"Orange\",\"Price\":1.39,\"Categories\":[\"fruit\"]},{\"Name\":\"Celery\",\"Price\":0.99,\"Categories\":[\"vegetable\"]}]") },
                { Path.Combine(folder, "Promotions.json"), new MockFileData("[{\"Type\":\"OnSalePromotion\",\"Product\":\"Apple\",\"SalePrice\":1.49},{\"Type\":\"GroupPromotion\",\"Product\":\"Orange\",\"Price\":3.0,\"Quantity\":2},{\"Type\":\"AdditionalPromotion\",\"Category\":\"fruit\",\"Percent\":50}],") },
            });

            string invoice = new Application(fileSystem).PrintInvoice(folder);

            Assert.Equal("Apple 1.99\n-0.5Orange 1.39\n-0.695Total 2.185", invoice);
        }
    }
}