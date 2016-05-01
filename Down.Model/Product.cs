using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawn.Model
{
    public class Product : EntityBase
    {
        private List<Formula> formulas;
        public Product()
        {
            formulas = new List<Formula>();
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private decimal price;

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        private decimal count;

        public decimal Count
        {
            get { return count; }
            set { count = value; }
        }


        public List<Formula> Formulas
        {
            get { return formulas; }
            set { formulas = value; }
        }




    }
}
