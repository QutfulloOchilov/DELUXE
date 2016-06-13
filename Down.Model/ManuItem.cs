using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Windows.Input;

namespace Deluxe.Model
{
    [Table(nameof(MenuItem))]
    public class MenuItem : INotifyPropertyChanged
    {
        public MenuItem()
        {
        }

        private string title;

        public MenuItem(string title) : this()
        {
            this.title = title;
        }

        [Column(nameof(Title))]
        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged();
            }
        }

        private object content;

        [Ignore]
        public object Content
        {
            get { return content; }
            set { content = value; NotifyPropertyChanged(); NotifyPropertyChanged(nameof(ContentInString)); }
        }

        private string contentInString;
        [Column(nameof(ContentInString))]
        public string ContentInString
        {
            get
            {
                if (string.IsNullOrEmpty(contentInString))
                {
                    contentInString = Content.ToString();
                }
                return contentInString;
            }
            set { contentInString = value; }
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
