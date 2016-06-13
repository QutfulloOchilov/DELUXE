using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deluxe.Framework;
using Deluxe.Framework.Message;
using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace Deluxe.Framework
{

    public class EntityBase : IEntity
    {
        private Guid guid;
        private List<Deluxe.Framework.Message.Message> messages;

        public EntityBase()
        {
            messages = new List<Deluxe.Framework.Message.Message>();
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
        public List<Deluxe.Framework.Message.Message> Messages
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
