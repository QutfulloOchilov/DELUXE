using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Reflection;
using SQLite.Net.Interop;
using Dawn.Model;
using Dawn.Model.Entities;
using Dawn.Framework.Message;

namespace Dawn.ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {

        private DawnSQLiteConnection connection;
        private User currentUser;

        public ViewModel(ISQLitePlatform litePlatform, string databasePath, bool fileExists)
        {
            if (!fileExists)
            {
                connection = new DawnSQLiteConnection(litePlatform, databasePath, true);
            }
            else
            {
                connection = new DawnSQLiteConnection(litePlatform, databasePath);
            }

        }

        /// <summary>
        /// SQLite connection
        /// </summary>
        public DawnSQLiteConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

        public bool CheckLogin(string login, string password)
        {
            bool result = false;
            try
            {
                var resultCheck = Connection.Table<User>().FirstOrDefault(u => u.Login == login && u.Password == password);
                if (resultCheck != null)
                {

                    result = true;
                    CurrentUser = resultCheck;
                }
            }
            catch (Exception ex)
            {
                MessageManager.OnMessageManagerEvent(new Message(MessageLevel.Error) { Title = "Exception on project", Detail = ex.Message });
            }
            return result;
        }


        public User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = value;
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
