using CafeAutomationCodeFirst.Data;
using CafeAutomationCodeFirst.Models;
using CafeAutomationCodeFirst.Repository.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAutomationCodeFirst.Repository
{
    public class CategoryRepository : RepositoryBase<Category, int>
    {
        private CafeContext cafeContext = new CafeContext();
        public void AddProduct(Category category,Product product)
        {
            cafeContext.Categories.First(x => x.Id == category.Id).Products.Add(product);
            cafeContext.SaveChanges();
        }
    }
}
