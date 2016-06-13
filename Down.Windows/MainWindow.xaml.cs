using Deluxe.Framework;
using Deluxe.Framework.Message;
using Deluxe.ModelView;
using Deluxe.Windows.Pages;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SQLite.Net.Platform.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Deluxe.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private ViewModel viewModel;

        public ViewModel ViewModel
        {
            get
            {
                if (viewModel == null)
                {
                    viewModel = new ViewModel(new SQLitePlatformWin32(), DatabasePath, File.Exists(DatabasePath));
                }
                return viewModel;
            }
            set { viewModel = value; }
        }

        public string DatabasePath { get; set; }



        public MainWindow()
        {
            DatabasePath = "DatabaseDeluxe.db";
            AddMenuItem();
            InitMenu();
            this.DataContext = ViewModel;
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            MessageManager.ShowMessage += MessageManager_ShowMessage;
            OpenPageManager.OpenPage += OpenPageManager_OpenPage;

        }

        private void InitMenu()
        {
            var menuItems = new List<dynamic>();
            foreach (dynamic item in ViewModel.GetMenuItems())
            {
                var contentString = item.ContentInString.ToString();
                if (contentString != null)
                {
                    Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == contentString.ToString());
                    item.Content = Activator.CreateInstance(type);
                }
                menuItems.Add(item);
            }
            ViewModel.SetPageForManuItem(menuItems);
        }


        private void OpenPageManager_OpenPage(OpenPageEventArgs e)
        {

        }

        private void AddMenuItem()
        {
            var dicMenuItem = new Dictionary<string, object>();
            dicMenuItem.Add("Main", new Main { DataContext = ViewModel });
            dicMenuItem.Add("Add new product", new NewProduct { DataContext = ViewModel });
            dicMenuItem.Add("New Order", new NewOrder { DataContext = ViewModel });
            dicMenuItem.Add("View Order", new ViewOrder { DataContext = ViewModel });
            dicMenuItem.Add("Change order", new ChangeOrder { DataContext = ViewModel });

            foreach (var item in dicMenuItem)
            {
                ViewModel.AddMenuItem(item.Key, item.Value);
            }
        }

        private async void MessageManager_ShowMessage(MessageManagerEventArgs e)
        {
            var message = e.Message;
            var messageSetting = new MetroDialogSettings();
            var messageDialogStyle = MessageDialogStyle.Affirmative;
            switch (message.Choices.Count)
            {
                case 1:
                    messageSetting.AffirmativeButtonText = message.Choices.Single().Title;
                    break;
                case 2:
                    messageSetting.AffirmativeButtonText = message.Choices[0].Title;
                    messageSetting.NegativeButtonText = message.Choices[1].Title;
                    messageDialogStyle = MessageDialogStyle.AffirmativeAndNegative;
                    break;
                case 3:
                    messageSetting.AffirmativeButtonText = message.Choices[0].Title;
                    messageSetting.NegativeButtonText = message.Choices[1].Title;
                    messageSetting.FirstAuxiliaryButtonText = message.Choices[2].Title;
                    messageDialogStyle = MessageDialogStyle.AffirmativeAndNegativeAndDoubleAuxiliary;
                    break;
                default:
                    break;
            }

            await this.ShowMessageAsync(message.Title, message.Detail,
                messageDialogStyle, messageSetting);

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowLoginAsync();

        }

        private void OpenMenu(object menu)
        {

        }

        private async void ShowLoginAsync()
        {
            var result = await this.ShowLoginAsync("Authentication", "Enter your password", new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, DefaultButtonFocus = MessageDialogResult.Affirmative });
            if (result == null)
            {
                ShowLoginAsync();
            }
            if (!viewModel.CheckLogin(result.Username, result.Password))
            {
                ShowLoginAsync();
            }
        }

        private void UIElement_OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MenuToggleButton.IsChecked = false;
        }

        private void MenuPopupButton_OnClick(object sender, RoutedEventArgs e)
        {
            //var sampleMessageDialog = new SampleMessageDialog
            //{
            //    Message = { Text = ((ButtonBase)sender).Content.ToString() }
            //};

            //await DialogHost.Show(sampleMessageDialog, "RootDialog");
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CreateDemoData();
        }
    }
}
