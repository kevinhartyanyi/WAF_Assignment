using Client.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Data;

namespace Client.ViewModel
{
    //public class SelectedCategoryConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return value;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is CategoryViewModel)
    //            return value;
    //        return null;
    //    }
    //}
    public class OrderViewModel : ViewModelBase
    {
        private readonly ClientAPIService _service;
        private ObservableCollection<OrdViewModel> _categories;
        private ObservableCollection<ProductViewModel> _products;
        private CategoryViewModel _selectedCategory;
        private ProductViewModel _selectedProduct;

        public ObservableCollection<OrdViewModel> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        public List<OrdViewModel> CategoryForCombo
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

        public DelegateCommand CloseOrdersCommand { get; private set; }

        public event EventHandler CloseOrders;



        public OrderViewModel(ClientAPIService service)
        {
            _service = service;

            RefreshListsCommand = new DelegateCommand(_ => LoadListsAsync());

            SelectCategoryCommand = new DelegateCommand(_ => LoadItemsAsync(SelectedCategory));

            CloseOrdersCommand = new DelegateCommand(_ => CloseOrders?.Invoke(this, EventArgs.Empty));

        }

        private async void LoadListsAsync()
        {
            try
            {
                Categories = new ObservableCollection<OrdViewModel>((await _service.LoadOrdersAsync()).Select(list =>
                {
                    var listVm = (OrdViewModel)list;
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
    }
}
