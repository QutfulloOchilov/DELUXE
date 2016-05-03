using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dawn.Framework.Message
{
    public class Message : IMessage
    {
        private string detail;
        private string solution;
        private string title;
        private MessageLevel messageLevel;
        private ObservableCollection<MessageChoice> choices;

        public Message(MessageLevel messageLevel)
        {
            choices = new ObservableCollection<MessageChoice>();
            this.messageLevel = messageLevel;

        }

        public Message(MessageLevel messageLevel, string title, string detail, string solution) : this(messageLevel)
        {
            this.title = title;
            this.detail = detail;
            this.solution = solution;
        }

        public Message(MessageLevel messageLevel, string title, string detail, string solution, MessageChoice choice) : this(messageLevel, title, detail, solution)
        {
            this.choices.Add(choice);
        }



        public ObservableCollection<MessageChoice> Choices
        {
            get
            {
                if (!choices.Any())
                {
                    return new ObservableCollection<MessageChoice> { new MessageChoice("OK") };
                }
                return choices;
            }

            set
            {
                this.choices = value;
            }
        }

        public string Detail
        {
            get
            {
                return detail;
            }

            set
            {
                this.detail = value;
            }
        }

        public MessageLevel MessageLevel
        {
            get
            {
                return messageLevel;
            }

            set
            {
                this.messageLevel = value;
            }
        }

        public string Solution
        {
            get
            {
                return solution;
            }

            set
            {
                this.solution = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                this.title = value;
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
