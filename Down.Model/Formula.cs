using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawn.Model
{
    public class Formula : EntityBase
    {

        private decimal from;
        public decimal From { get { return from; } set { from = value; NotifyPropertyChanged(); } }


        private decimal to;
        public decimal To { get { return to; } set { to = value; NotifyPropertyChanged(); } }


        private decimal price;
        public decimal Price { get { return price; } set { price = value; NotifyPropertyChanged(); } }

        public Product Product { get; set; }
    }
}
