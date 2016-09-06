using System.Collections.Generic;
using MyAspCoreRestAPI.Models;

namespace MyAspCoreRestAPI.Services
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
