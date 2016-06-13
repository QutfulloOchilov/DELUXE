using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensionsAsync;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Deluxe.Model.Entities
{
    [Table(nameof(User))]
    public class User : Person
    {
        private string login;
        private string password;
        private List<Buying> orders;

        public User()
        {
            orders = new List<Buying>();
        }

        [Column(nameof(Login))]
        public string Login
        {
            get { return login; }
            set { login = value; }
        }

        [Column(nameof(Password))]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        [OneToMany(CascadeOperations =CascadeOperation.All)]
        public List<Buying> Orders
        {
            get { return orders; }
            set { orders = value; }
        }

    }
}
