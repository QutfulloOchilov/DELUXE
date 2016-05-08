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

namespace Dawn.Framework
{

    public abstract class EntityBase : IEntity
    {
        private Guid guid;
        private List<Dawn.Framework.Message.Message> messages;

        protected EntityBase()
        {
            messages = new List<Dawn.Framework.Message.Message>();
        }

        [Column(nameof(Guid)), PrimaryKey()]
        public Guid Guid
        {
            get
            {
                if (guid.ToString().StartsWith("000"))
                    guid = Guid.NewGuid();

                return guid;
            }
            set { guid = value; }
        }

        [Ignore]
        public List<Dawn.Framework.Message.Message> Messages
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
