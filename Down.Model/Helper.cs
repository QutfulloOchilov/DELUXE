using Dawn.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Dawn.Model
{
    public class Helper
    {
        public static IEnumerable<Type> GetAllEntities()
        {
            return new Type[]
            {
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
