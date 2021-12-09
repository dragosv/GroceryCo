// <copyright file="ProductSerializerFacts.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace GroceryCo.Tests
{
    using GroceryCo.Library;
    using Xunit;

    public class ProductSerializerFacts
    {
        private const string Serialized =
            "[{\"Name\":\"Apple\",\"Price\":1.99,\"Categories\":[\"fruit\"]},{\"Name\":\"Orange\",\"Price\":1.39,\"Categories\":[\"fruit\"]},{\"Name\":\"Celery\",\"Price\":0.99,\"Categories\":[\"vegetable\"]}]";

        private readonly Product firstProduct;
        private readonly Product secondProduct;
        private readonly Product thirdProduct;
        private readonly Product[] products;

        public ProductSerializerFacts()
        {
            this.firstProduct = new Product { Name = "Apple", Price = 1.99, Categories = { "fruit" } };
            this.secondProduct = new Product { Name = "Orange", Price = 1.39, Categories = { "fruit" } };
            this.thirdProduct = new Product { Name = "Celery", Price = 0.99, Categories = { "vegetable" } };

            this.products = new[] { this.firstProduct, this.secondProduct, this.thirdProduct };
        }

        [Fact]
        public void ShouldSerialize()
        {
            var serializer = new ProductsSerializer();

            var output = ProductsSerializer.Serialize(this.products);

            Assert.Equal(Serialized, output);
        }

        [Fact]
        public void ShouldDeserialize()
        {
            var serializer = new ProductsSerializer();

            var deserialized = ProductsSerializer.Deserialize(Serialized);

            Assert.Equal(this.products, deserialized);
        }
    }
}