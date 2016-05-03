using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;

namespace Dawn.Model.Entities
{
    [Table(nameof(Client))]
    public class Client : Person
    {
        private List<Buying> orders;

        public Client()
        {
            orders = new List<Buying>();

        }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Buying> Orders
        {
            get { return orders; }
            set { orders = value; }
        }


    }
}
