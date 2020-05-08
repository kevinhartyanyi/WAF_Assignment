using Client.Model;
using Client.View;
using Client.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ClientAPIService _service;
        private MainViewModel _mainViewModel;
        private LoginViewModel _loginViewModel;
        private OrderViewModel _orderViewModel;
        private MainWindow _mainView;
        private OrderWindow _orderView;
        private LoginWindow _loginView;

        public App()
        {
            Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            _service = new ClientAPIService(ConfigurationManager.AppSettings["baseAddress"]);

            _loginViewModel = new LoginViewModel(_service);

            _loginViewModel.LogintSucceeded += ViewModel_LoginSucceeded;
            _loginViewModel.LoginFailed += ViewModel_LoginFailed;
            _loginViewModel.MessageApplication += ViewModel_MessageApplication;

            _loginView = new LoginWindow
            {
                DataContext = _loginViewModel
            };

            _orderViewModel = new OrderViewModel(_service);
            _orderViewModel.CloseOrders += ViewModel_CloseOrders;

            _orderView = new OrderWindow
            {
                DataContext = _orderViewModel
            };

            _mainViewModel = new MainViewModel(_service);
            _mainViewModel.LogoutSucceeded += ViewModel_LogoutSucceeded;
            _mainViewModel.MessageApplication += ViewModel_MessageApplication;
            _mainViewModel.OpenOrders += ViewModel_OpenOrders;

            _mainView = new MainWindow
            {
                DataContext = _mainViewModel
            };
            _mainView.Show();
            //_loginView.Show();
        }

        private void ViewModel_LoginSucceeded(object sender, EventArgs e)
        {
            _loginView.Hide();
            _mainView.Show();
        }

        private void ViewModel_OpenOrders(object sender, EventArgs e)
        {
            _mainView.Hide();
            _orderView.Show();
        }

        private void ViewModel_CloseOrders(object sender, EventArgs e)
        {
            _orderView.Hide();
            _mainView.Show();
        }

        private void ViewModel_LoginFailed(object sender, EventArgs e)
        {
            MessageBox.Show("Login failed!", "TodoList", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }

        private void ViewModel_LogoutSucceeded(object sender, EventArgs e)
        {
            _mainView.Hide();
            _loginView.Show();
        }

        private void ViewModel_MessageApplication(object sender, MessageEventArgs e)
        {
            MessageBox.Show(e.Message, "TodoList", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        
    }
}
