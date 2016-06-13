using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Deluxe.Framework.Message;
using SQLiteNetExtensions.Attributes;
using SQLite.Net.Attributes;
using Deluxe.Framework;

namespace Deluxe.Model.Entities
{
    [Table(nameof(Buying))]
    public class Buying : EntityBase
    {
        private List<BuyingDetail> orderDetails;
        private Guid clientId;
        private Client client;
        private User user;

        public Buying(User user)
        {
            orderDetails = new List<BuyingDetail>();
            this.user = user;
        }

        [Column(nameof(Date))]
        public DateTime Date { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<BuyingDetail> OrderDetails
        {
            get { return orderDetails; }
            set { orderDetails = value; NotifyPropertyChanged(); }
        }

        [Ignore]
        public ObservableCollection<BuyingDetail> OrderDetailsInUI => new ObservableCollection<BuyingDetail>(OrderDetails);

        [ForeignKey(typeof(Client)), Column(nameof(ClientId))]
        public Guid ClientId
        {
            get { return clientId; }
            set { clientId = value; }
        }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Client Client
        {
            get { return client; }
            set { client = value; }
        }

        private Guid userId;
        [ForeignKey(typeof(User)), Column(nameof(UserId))]
        public Guid UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public void Transation()
        {
            foreach (BuyingDetail orderDetail in OrderDetails)
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
