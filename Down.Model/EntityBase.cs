using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dawn.Framework;
using Dawn.Framework.Message;
using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace Dawn.Model
{

    public abstract class EntityBase : IEntity
    {
        private Guid id;
        private List<Message> messages;

        protected EntityBase()
        {
            messages = new List<Message>();
        }
        [Column(nameof(Id))]
        public Guid Id
        {
            get
            {
                if (id == null)
                    return Guid.NewGuid();

                return id;
            }
            set { id = value; }
        }

        [Ignore]
        public List<Message> Messages
        {
            get { return messages; }
            set
            {
                messages = value;
                NotifyPropertyChanged();
            }
        }

        #region Notify

        /// <summary>
        /// Property Changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Fire the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property that changed (defaults from CallerMemberName)</param>
        protected void NotifyPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
