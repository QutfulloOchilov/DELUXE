using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawn.Model
{
    public class User : Person
    {
        private string login;
        private string password;
        private List<Order> orders;

        public User()
        {
            orders = new List<Order>();
        }
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }


        public List<Order> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

    }
}
