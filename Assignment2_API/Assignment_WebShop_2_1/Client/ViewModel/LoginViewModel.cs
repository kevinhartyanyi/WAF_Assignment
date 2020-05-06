using Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Client.ViewModel
{
    /// <summary>
    /// A bejelentkezés nézetmodell típusa.
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private readonly ClientAPIService _model;
        private bool _isLoading;

        /// <summary>
        /// Bejelentkezés parancs lekérdezése.
        /// </summary>
        public DelegateCommand LoginCommand { get; private set; }

        /// <summary>
        /// Felhasználónév lekérdezése, vagy beállítása.
        /// </summary>
        public string UserName { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Sikeres bejelentkezés eseménye.
        /// </summary>
        public event EventHandler LogintSucceeded;

        /// <summary>
        /// Sikertelen bejelentkezés eseménye.
        /// </summary>
        public event EventHandler LoginFailed;

        /// <summary>
        /// Nézetmodell példányosítása.
        /// </summary>
        /// <param name="model">A modell.</param>
        public LoginViewModel(ClientAPIService model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _model = model;
            UserName = string.Empty;
            IsLoading = false;

            LoginCommand = new DelegateCommand(_ => !IsLoading, param => LoginAsync(param as PasswordBox));
        }

        /// <summary>
        /// Bejelentkezés
        /// </summary>
        /// <param name="passwordBox">Jelszótároló vezérlő.</param>
        private async void LoginAsync(PasswordBox passwordBox)
        {
            if (passwordBox == null)
                return;

            try
            {
                IsLoading = true;
                bool result = await _model.LoginAsync(UserName, passwordBox.Password);
                IsLoading = false;

                if (result)
                    OnLoginSuccess();
                else
                    OnLoginFailed();
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        /// <summary>
        /// Sikeres bejelentkezés eseménykiváltása.
        /// </summary>
        private void OnLoginSuccess()
        {
            LogintSucceeded?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Sikertelen bejelentkezés eseménykiváltása.
        /// </summary>
        private void OnLoginFailed()
        {
            LoginFailed?.Invoke(this, EventArgs.Empty);
        }
    }
}
