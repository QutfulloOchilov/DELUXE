using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawn.Model
{
    public class OrderDetail : EntityBase
    {
        private Guid orderId;

        public Guid OrderId
        {
            get { return orderId; }
            set { orderId = value; }
        }

        private Order order;

        public Order Order
        {
            get { return order; }
            set { order = value; }
        }

        private Product product;

        public Product Product
        {
            get { return product; }
            set { product = value; }
        }

        private Guid productId;

        public Guid ProductId
        {
            get { return productId; }
            set { productId = value; }
        }


        private decimal count;

        public decimal Count
        {
            get { return count; }
            set
            {
                count = value;
                NotifyPropertyChanged();
            }
        }

        public decimal GetPrice()
        {
            decimal result = Product.Price;
            if (Product.Formulas.Any())
            {
                foreach (Formula formula in Product.Formulas)
                {
                    if (formula.To >= Count)
                    {
                        result = formula.Price;
                    }
                }
            }
            return result;
        }

        public decimal CalculetOrderDetail()
        {
            return Count * GetPrice();
        }

    }
}
