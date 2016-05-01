using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawn.Model
{
    public interface IEntity: INotifyPropertyChanged
    {

        Guid Id
        {
            get;
            set;
        }


    }
}
