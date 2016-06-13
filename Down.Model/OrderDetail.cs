using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using Deluxe.Framework;

namespace Deluxe.Model.Entities
{
    [Table(nameof(BuyingDetail))]
    public class BuyingDetail : EntityBase
    {
        private Guid orderId;
        private Buying order;
        private Product product;
        private Guid productId;
        private decimal count;

        [ForeignKey(typeof(Buying)), Column(nameof(BuyingId))]
        public Guid BuyingId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Buying Buying
        {
            get { return order; }
            set { order = value; }
        }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        [ForeignKey(typeof(Product))]
        public Guid ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        [Ignore]
        public decimal TotalPrice { get { return CalculetOrderDetail(); } }

        public decimal Count
        {
            get { return count; }
            set
            {
                count = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(TotalPrice));
            }
        }


        public decimal GetPrice()
        {
            if (Product == null)
                return 0;
            decimal result = Product.Price;
            if (Product.Formulas.Any())
            {
                foreach (Formula formula in Product.Formulas)
                {
                    if (formula.To >= count)
                    {
                        result = formula.Price;
                    }
                }
            }
            return result;
        }

        public decimal CalculetOrderDetail()
        {
            return count * GetPrice();
        }



    }
}
