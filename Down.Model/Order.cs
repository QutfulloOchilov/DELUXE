using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Dawn.Framework.Message;

namespace Dawn.Model
{
    public class Order : EntityBase
    {
        private List<OrderDetail> orderDetails;
        private Client client;
        private User user;

        public Order(User user)
        {
            orderDetails = new List<OrderDetail>();
            this.user = user;
        }

        public DateTime Date { get; set; }

        public List<OrderDetail> OrderDetails
        {
            get { return orderDetails; }
            set { orderDetails = value; NotifyPropertyChanged(); }
        }

        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public void Transation()
        {
            foreach (OrderDetail orderDetail in OrderDetails)
            {
                if (orderDetail.Count > orderDetail.Product.Count)
                {
                    var choice = new MessageChoice("OK");
                    var errorCountMessage = new Message(MessageLevel.Error, title: "Mashulot dar anbor kam ast", detail: $"Mahsuloti {orderDetail.Product} bo miqdori {orderDetail.Count - orderDetail.Product.Count}", solution: "Iltimos miqdori durust ro dokhil namoed", choice: choice);
                    Messages.Add(errorCountMessage);
                    MessageManager.OnMessageManagerEvent(errorCountMessage);
                }
            }

        }






    }
}
