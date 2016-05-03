using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Dawn.Model.Entities
{
    [Table(nameof(Product))]
    public class Product : EntityBase
    {
        private string name;
        private decimal price;
        private decimal count;
        private List<Formula> formulas;
        private List<BuyingDetail> buyingDetails;

        public Product()
        {
            formulas = new List<Formula>();
        }
        [Column(nameof(Name))]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Column(nameof(Price))]
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        [Column(nameof(Count))]
        public decimal Count
        {
            get { return count; }
            set { count = value; }
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Formula> Formulas
        {
            get { return formulas; }
            set { formulas = value; }
        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BuyingDetail> BuyingDetails
        {
            get { return buyingDetails; }
            set { buyingDetails = value; }
        }

    }
}
