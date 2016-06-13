using Deluxe.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Deluxe.Model.Entities
{
    public abstract class Person : EntityBase
    {

        private string id;
        public string ID
        {
            get
            {
                if (string.IsNullOrEmpty(id))
                {
                    id = firstName.Substring(0, 3) + lastName.Substring(0, 3);
                }
                return id.ToUpper();
            }
            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        private string firstName;
        public string FirstName { get { return firstName; } set { firstName = value; NotifyPropertyChanged(); } }


        private string lastName;
        public string LastName { get { return lastName; } set { lastName = value; NotifyPropertyChanged(); } }

    }

}
