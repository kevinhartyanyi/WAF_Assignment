using Client.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Client.ViewModel
{
    public class RegisterViewModel : ViewModelBase
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

        public string FullName { get; set; }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public event EventHandler RegisterSuccess;


        /// <summary>
        /// Nézetmodell példányosítása.
        /// </summary>
        /// <param name="model">A modell.</param>
        public RegisterViewModel(ClientAPIService model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _model = model;
            UserName = string.Empty;
            IsLoading = false;

            LoginCommand = new DelegateCommand(_ => !IsLoading, param => RegisterStart(param as PasswordBox));
        }

        /// <summary>
        /// Bejelentkezés
        /// </summary>
        /// <param name="passwordBox">Jelszótároló vezérlő.</param>
        private async void RegisterStart(PasswordBox passwordBox)
        {
            if (passwordBox == null)
                return;

            try
            {
                IsLoading = true;
                bool result = await _model.RegisterAsync(FullName, UserName, passwordBox.Password);
                IsLoading = false;
                if(result)
                {
                    RegisterSuccess?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    OnMessageApplication($"Név foglalt");
                }
            }
            catch (NetworkException ex)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
    }
}
