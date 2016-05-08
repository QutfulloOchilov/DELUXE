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
using System.Collections.ObjectModel;
using SQLiteNetExtensions.Extensions;

namespace Dawn.ModelView
{
    public class ViewModel : INotifyPropertyChanged
    {

        private DawnSQLiteConnection connection;
        private User currentUser;
        private ObservableCollection<MenuItem> menus;
        private ObservableCollection<Client> clients;
        private ObservableCollection<BuyingDetail> orderDetails;

        public ViewModel(ISQLitePlatform litePlatform, string databasePath, bool fileExists)
        {
            try
            {
                if (!fileExists)
                {
                    connection = new DawnSQLiteConnection(litePlatform, databasePath, true);
                }
                else
                {
                    connection = new DawnSQLiteConnection(litePlatform, databasePath);
                }

                InitFilds();
                CreateDefaultUser();

            }
            catch (Exception ex)
            {

                MessageManager.OnMessageManagerEvent(new Message(MessageLevel.Error) { Title = "Exception on project", Detail = ex.Message });
            }

        }

        private void InitFilds()
        {
            menus = new ObservableCollection<MenuItem>();
            clients = new ObservableCollection<Client>();
            orderDetails = new ObservableCollection<BuyingDetail>();

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

        public void AddMenuItem(string title, object page)
        {
            var menuItems = connection.Table<MenuItem>();
            if (menuItems.FirstOrDefault(m => m.Title == title) == null)
            {
                Connection.Insert(new MenuItem(title) { Content = page });
            }
        }

        private void CreateDefaultUser()
        {
            if (connection.Table<User>().FirstOrDefault() == null)
            {
                var newUser = new User
                {
                    FirstName = "Qutfullo",
                    LastName = "Ochilov",
                    Login = "qutfullo",
                    Password = "qutfullo",
                };
                connection.Insert(newUser);
            }
        }

        public ObservableCollection<MenuItem> Menus
        {
            get
            {
                if (!menus.Any())
                {
                    menus = new ObservableCollection<MenuItem>(Connection.GetAllWithChildren<MenuItem>());
                }
                return menus;

            }
            set
            {
                menus = value;
                NotifyPropertyChanged();
            }
        }

        public List<dynamic> GetMenuItems()
        {
            return new List<dynamic>(Connection.Table<MenuItem>());
        }

        public void SetPageForManuItem(List<dynamic> menuItems)
        {
            foreach (dynamic menuItem in menuItems)
            {
                Menus.FirstOrDefault(m => m.Title == menuItem.Title).Content = menuItem.Content;
            }
        }


        public ObservableCollection<Client> Clients
        {
            get
            {
                if (!clients.Any())
                {
                    clients = new ObservableCollection<Client>(Connection.GetAllWithChildren<Client>());
                }
                return clients;
            }
            set { clients = value; }
        }

        public void CreateDemoData()
        {
            var users = new List<User>
            {
                new User
                {
                    FirstName = "Dilovar",
                    LastName = "Otaev",
                    Login = "dilovar",
                    Password = "123"
                },
            };

            var clients = new List<Client>
            {
                new Client
                {
                    FirstName="Sheroz",
                    LastName="Sodiqzoda"
                },
                new Client
                {
                    FirstName="Faridun",
                    LastName="Sodiqzoda"
                },
                new Client
                {
                    FirstName="Nozim",
                    LastName="Sharifov"
                }
            };

            var products = new List<Product>
            {
                new Product
                {
                    ID="KODAK",
                    Name="Kodak ultima photo paper",
                    Price=decimal.Parse("0.7"),
                    Count=100,
                    Formulas=new List<Formula>
                    {
                        new Formula
                        {
                            From=1,
                            To=10,
                            Price=decimal.Parse("0.7")
                        },

                        new Formula
                        {
                            From=10,
                            To=30,
                            Price=decimal.Parse("0.6")
                        },

                        new Formula
                        {
                            From = 30,
                            To = 60,
                            Price=decimal.Parse("0.5")
                        },
                    }
                },
                new Product
                {
                    ID="JINLAN",
                    Name="Jinlan photo paper",
                    Price=decimal.Parse("0.65"),
                    Count=150,
                    Description="High glossy inkjet photo paper",
                    Formulas=new List<Formula>
                    {
                        new Formula
                        {
                            From=1,
                            To=10,
                            Price=decimal.Parse("0.65")

                        },
                        new Formula
                        {
                            From=10,
                            To=30,
                            Price=decimal.Parse("0.55")
                        },
                        new Formula
                        {
                            From=30,
                            To=60,
                            Price=decimal.Parse("0.45")

                        }
                    }
                },

                new Product
                {
                    ID="COPY",
                    Name="Copy paper",
                    Count=500,
                    Price=decimal.Parse("0.2"),
                    Formulas=new List<Formula>
                    {
                        new Formula
                        {
                            From=1,
                            To=20,
                            Price=decimal.Parse("0.2")
                        },
                        new Formula
                        {
                            From=20,
                            To=40,
                            Price=decimal.Parse("0.15")
                        },
                        new Formula
                        {
                            From=41,
                            Price=decimal.Parse("0.1")
                        }
                    }
                },
            };
            Connection.InsertAll(users);
            Connection.InsertAll(clients);
            Connection.InsertAllWithChildren(products);
        }

        public void CreateNewOrder(dynamic client)
        {
            var newOrder = new Buying(CurrentUser) { Client = (Client)client };
            OrderDetails = new ObservableCollection<BuyingDetail>();
            newOrder.OrderDetails = new List<BuyingDetail>(OrderDetails);
        }

        public ObservableCollection<BuyingDetail> OrderDetails
        {
            get { return orderDetails; }
            set { orderDetails = value; }
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
