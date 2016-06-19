using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;
using Deluxe.Framework;


namespace Deluxe.Model.Entities
{
    [Table(nameof(Product))]
    public class Product : EntityBase
    {
        private string id;
        private string name;
        private string description;
        private decimal price;
        private decimal count;
        private List<Formula> formulas;
        private List<BuyingDetail> buyingDetails;
        private string icon;

        public Product()
        {
            formulas = new List<Formula>();
        }

        [Column(nameof(ID))]
        public string ID
        {
            get
            {
                return id;
            }
            set { id = value; }
        }

        [Column(nameof(Name))]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [Column(nameof(Description))]
        public string Description
        {
            get { return description; }
            set { description = value; }
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
            set { count = value; NotifyPropertyChanged(); }
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

        public string Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                NotifyPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
