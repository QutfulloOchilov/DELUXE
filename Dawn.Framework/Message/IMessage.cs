using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Dawn.Framework.Message
{
    public interface IMessage : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        ObservableCollection<MessageChoice> Choices { get; set; }

        /// <summary>
        /// 
        /// </summary>
        MessageLevel MessageLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Solution { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        string Detail { get; set; }
    }

    public enum MessageLevel
    {
        Error,
        Warning,
        Information
    }
}
