using Deluxe.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Deluxe.Model
{
    public class Helper
    {
        public static IEnumerable<Type> GetAllEntities()
        {
            return new Type[]
            {
                typeof(MenuItem),
                typeof(User),
                typeof(Client),
                typeof(Product),
                typeof(Buying),
                typeof(BuyingDetail),
                typeof(Formula),
            };
        }

    }
}
