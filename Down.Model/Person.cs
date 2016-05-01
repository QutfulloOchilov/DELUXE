using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Dawn.Model
{
    public abstract class Person : EntityBase
    {
        private string firstName;
        public string FirstName { get { return firstName; } set { firstName = value; NotifyPropertyChanged(); } }


        private string lastName;
        public string LastName { get { return lastName; } set { lastName = value; NotifyPropertyChanged(); } }

    }

}
