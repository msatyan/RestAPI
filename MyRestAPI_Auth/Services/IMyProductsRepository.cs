using System.Collections.Generic;
using MyRestAPI.Models;

namespace MyRestAPI.Services
{
    public interface IMyProductsRepository
    {
        IEnumerable<MyProducts> GetAllMyProducts();

        void AddMyProducts(MyProducts product);
        MyProducts GetMyProducts(int id);
        void UpdateMyProducts(MyProducts product);
        void DeleteMyProducts(int id);
    }
}
