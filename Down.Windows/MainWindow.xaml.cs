using Dawn.Framework.Message;
using Dawn.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using SQLite.Net.Platform.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace Dawn.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        ViewModel viewModel;
        public string DatabasePath { get; set; }
        public MainWindow()
        {
            DatabasePath = "DatabaseDawn.db";
            viewModel = new ViewModel(new SQLitePlatformWin32(), DatabasePath, File.Exists(DatabasePath));
            this.DataContext = viewModel;
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            MessageManager.ShowMessage += MessageManager_ShowMessage;

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
    }
}
