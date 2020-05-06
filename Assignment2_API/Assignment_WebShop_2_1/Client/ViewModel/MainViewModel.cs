using Client.Model;
using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Windows.Controls;
using System.Windows.Data;

namespace Client.ViewModel
{
    public class SelectedCategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CategoryViewModel)
                return value;
            return null;
        }
    }

    public class MainViewModel : ViewModelBase
    {
        private readonly ClientAPIService _service;
        private ObservableCollection<CategoryViewModel> _categories;
        private ObservableCollection<ProductViewModel> _products;
        private CategoryViewModel _selectedCategory;
        private ProductViewModel _selectedProduct;

        public ObservableCollection<CategoryViewModel> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public List<CategoryViewModel> CategoryForCombo
        {
            get => Categories.ToList();
        }

        public ObservableCollection<ProductViewModel> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public CategoryViewModel SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        public ProductViewModel SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand RefreshListsCommand { get; private set; }
        public DelegateCommand SelectCategoryCommand { get; private set; }

        public DelegateCommand LogoutCommand { get; private set; }

        public DelegateCommand IncrementProductCommand { get; private set; }

        public event EventHandler LogoutSucceeded;

        public MainViewModel(ClientAPIService service)
        {
            _service = service;

            RefreshListsCommand = new DelegateCommand(_ => LoadListsAsync());

            LogoutCommand = new DelegateCommand(_ => LogoutAsync());
            SelectCategoryCommand = new DelegateCommand(_ => LoadItemsAsync(SelectedCategory));

            IncrementProductCommand = new DelegateCommand(_ => !(SelectedProduct is null), _ => IncrementProduct(SelectedProduct));
        }

        #region Authentication

        private async void LogoutAsync()
        {
            try
            {
                await _service.LogoutAsync();
                OnLogoutSuccess();
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }

        private void OnLogoutSuccess()
        {
            LogoutSucceeded?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        private async void LoadListsAsync()
        {
            try
            {
                Categories = new ObservableCollection<CategoryViewModel>((await _service.LoadListsAsync()).Select(list =>
                {
                    var listVm = (CategoryViewModel)list;
                    return listVm;
                }));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }



        public async void LoadItemsAsync(CategoryViewModel list)
        {
            if (list is null || list.Id == 0)
            {
                Products = null;
                return;
            }
            try
            {
                Products = new ObservableCollection<ProductViewModel>((await _service.LoadItemsAsync(list.Id)).Select(item => (ProductViewModel)item));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
        

        private async void IncrementProduct(ProductViewModel item)
        {
            try
            {
                item.Amount++;
                await _service.UpdateProductAsync((ProductDTO)item);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
            
        }
    }
}
