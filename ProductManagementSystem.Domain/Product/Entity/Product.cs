using ProductManagementSystem.Domain.BaseEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Domain.Product.Entity
{
    public class Product:BaseEntity<long>
    {

        #region props
        public string Name {  get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        #endregion



        #region methods
        public void CreateProduct(string name,string description ,decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
            CreationDate = DateTime.Now;
        }

        public void UpdateProduct(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }
        #endregion
    }
}
