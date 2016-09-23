using System.Collections.Generic;
using System.Linq;
using MyRestAPI.Models;

namespace MyRestAPI.Services
{
    public class InMemMyProductsRepository : IMyProductsRepository
    {

        private List<MyProducts> _products = new List<MyProducts>
        {
            new MyProducts { Id = 1, ProductName = "CD",  Price = 47, ContactEmail = "xyz1@abc.com" },
            new MyProducts { Id = 2, ProductName = "DVD", Price = 38, ContactEmail = "xyz2@def.com" },
            new MyProducts { Id = 3, ProductName = "BRD", Price = 86, ContactEmail = "xyz3@hij.net" },
            new MyProducts { Id = 4, ProductName = "HDD", Price = 62, ContactEmail = "xyz4@klm.org" },
            new MyProducts { Id = 5, ProductName = "SSD", Price = 93, ContactEmail = "xyz5@nop.com" },
        };

        public void AddMyProducts(MyProducts product)
        {
            product.Id = _products.Max(p => p.Id) + 1;
            _products.Add(product);
        }

        public void DeleteMyProducts(int id)
        {
            var product = _products.Find(p => p.Id == id);
            _products.Remove(product);
        }

        public IEnumerable<MyProducts> GetAllMyProducts()
        {
            return _products;
        }

        public MyProducts GetMyProducts(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void UpdateMyProducts(MyProducts product)
        {
            var x = _products.Find(p => p.Id == product.Id);
            _products[_products.IndexOf(x)] = product;
        }
    }
}
