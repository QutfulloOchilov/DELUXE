using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using Deluxe.Framework;

namespace Deluxe.Model.Entities
{
    [Table(nameof(Formula))]
    public class Formula : EntityBase
    {
        private decimal from;
        private decimal to;
        private decimal price;
        private Product product;
        private Guid productId;

        public Formula()
        {

        }

        [Column(nameof(From))]
        public decimal From
        {
            get { return from; }
            set
            {
                from = value;
                NotifyPropertyChanged();
            }
        }

        [Column(nameof(To))]
        public decimal To
        {
            get { return to; }
            set
            {
                to = value;
                NotifyPropertyChanged();
            }
        }

        [Column(nameof(Price))]
        public decimal Price
        {
            get { return price; }
            set
            {
                price = value;
                NotifyPropertyChanged();
            }
        }

        [ForeignKey(typeof(Product)), Column(nameof(ProductId))]
        public Guid ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        public override string ToString()
        {
            return $"From {From}/ To {To}/ Price {Price}";
        }

    }
}
