using Client.Model;
using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace Client.ViewModel
{
    public class SelectedOrderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is OrdViewModel)
                return value;
            return null;
        }
    }
    public class OrderViewModel : ViewModelBase
    {
        private readonly ClientAPIService _service;
        private ObservableCollection<OrdViewModel> _categories;
        private ObservableCollection<ProductViewModel> _products;
        private OrdViewModel _selectedCategory;
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

        public OrdViewModel SelectedCategory
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

        public string SearchText { get; set; }

        public Visibility AskDeliver { get; set; }

        public DelegateCommand RefreshListsCommand { get; private set; }
        public DelegateCommand SelectCategoryCommand { get; private set; }

        public DelegateCommand SearchName { get; private set; }
        public DelegateCommand SearchAddress { get; private set; }
        public DelegateCommand SearchDelivered { get; private set; }
        public DelegateCommand CloseOrdersCommand { get; private set; }

        public DelegateCommand Deliver { get; private set; }
        public DelegateCommand StartDeliver { get; private set; }

        public DelegateCommand StopDeliver { get; private set; }


        public event EventHandler CloseOrders;



        public OrderViewModel(ClientAPIService service)
        {
            _service = service;

            RefreshListsCommand = new DelegateCommand(_ => LoadListsAsync());

            SelectCategoryCommand = new DelegateCommand(_ => LoadItemsAsync(SelectedCategory));

            SearchName = new DelegateCommand(_ => MySearch(SearchText, 0));

            SearchAddress = new DelegateCommand(_ => MySearch(SearchText, 1));

            SearchDelivered = new DelegateCommand(_ => MySearch(SearchText, 2));

            Deliver = new DelegateCommand(_ => !(SelectedCategory is null) && !SelectedCategory.Delivered, _ => {
                AskDeliver = Visibility.Visible;
                OnPropertyChanged("AskDeliver");
            });

            StartDeliver = new DelegateCommand(_ => CompleteDelivery(SelectedCategory));

            StopDeliver = new DelegateCommand(_ => {
                AskDeliver = Visibility.Hidden;
                OnPropertyChanged("AskDeliver");
            });


            CloseOrdersCommand = new DelegateCommand(_ => CloseOrders?.Invoke(this, EventArgs.Empty));

            SearchText = "";

            AskDeliver = Visibility.Hidden;

        }

        private async void CompleteDelivery(OrdViewModel ord)
        {
            try
            {
                ord.Delivered = true;

                //List<ProductDTO> pros = new List<ProductDTO>();

                //foreach (var item in ord.OrderedProducts)
                //{
                //    pros.Add(new ProductDTO {
                //        Amount = (await _service.LoadProductAsync(item.product.Id)).Amount - item.amount,
                //        Id = item.product.Id,
                //        Available = item.product.Available,
                //        Category = new CategoryDTO { Id = item.product.Category.Id, Name = item.product.Category.Name },
                //        Description = item.product.Description,
                //        Image = item.product.Image,
                //        Manufacturer = item.product.Manufacturer,
                //        ModelID = item.product.ModelID,
                //        Price = item.product.Price
                //    });
                //}

                //List<BasketElemDTO> newOrdProducts = new List<BasketElemDTO>();
                //foreach (var x in ord.OrderedProducts)
                //{
                //    newOrdProducts.Add(new BasketElemDTO
                //    {
                //        amount = x.amount,
                //        Id = x.Id,
                //        product = new ProductDTO
                //        {
                //            Id = x.product.Id,
                //            Amount = x.product.Amount - x.amount,
                //            Available = x.product.Available,
                //            Category = new CategoryDTO { Id = x.product.Category.Id, Name = x.product.Category.Name },
                //            Description = x.product.Description,
                //            Image = x.product.Image,
                //            Manufacturer = x.product.Manufacturer,
                //            ModelID = x.product.ModelID,
                //            Price = x.product.Price
                //        }
                //    });
                //}

                //ord.OrderedProducts = newOrdProducts;

                foreach (var item in ord.OrderedProducts)
                {
                    item.product.Amount = item.product.Amount - item.amount;
                }

                //foreach (var item in pros)
                //{
                //    await _service.UpdateProductAsync(item);
                //}

                await _service.UpdateOrderAsync((OrderDTO)ord);
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }

            AskDeliver = Visibility.Hidden;
            OnPropertyChanged("AskDeliver");
        }


        private async void MySearch(string text, int typ)
        {
            // typ
            // 0 -> Name
            // 1 -> Date
            // 2 -> Delivered
            try
            {
                if (typ == 0)
                {
                    Categories = new ObservableCollection<OrdViewModel>((await _service.LoadOrdersAsync())
                    .Where(x => x.UserName.Contains(text))
                    .Select(list =>
                    {
                        var listVm = (OrdViewModel)list;
                        return listVm;
                    }));
                }
                else if (typ == 1)
                {
                    Categories = new ObservableCollection<OrdViewModel>((await _service.LoadOrdersAsync())
                    .Where(x => x.Address.Contains(text))
                    .Select(list =>
                    {
                        var listVm = (OrdViewModel)list;
                        return listVm;
                    }));
                }
                else if (typ == 2)
                {
                    Categories = new ObservableCollection<OrdViewModel>((await _service.LoadOrdersAsync())
                    .Where(x => x.Delivered.ToString().Contains(text))
                    .Select(list =>
                    {
                        var listVm = (OrdViewModel)list;
                        return listVm;
                    }));
                }
                
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
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
        public async void LoadItemsAsync(OrdViewModel list)
        {
            
            if (list is null || list.Id == 0)
            {
                Products = null;
                return;
            }

            List<BasketElemDTO> elems = list.OrderedProducts;
            try
            {
                var order = await _service.LoadOrderProductAsync(list.Id);
                List<ProductDTO> pro = new List<ProductDTO>(order.OrderedProducts.Select(x => new ProductDTO
                {
                    Amount = x.amount,
                    Available = x.product.Available,
                    Description = x.product.Description,
                    Id = x.product.Id,
                    Image = x.product.Image,
                    Manufacturer = x.product.Manufacturer,
                    ModelID = x.product.ModelID,
                    Price = x.product.Price,
                    Category = x.product.Category
                }));
                Products = new ObservableCollection<ProductViewModel>(pro.Select(item => (ProductViewModel)item));
            }
            catch (Exception ex) when (ex is NetworkException || ex is HttpRequestException)
            {
                OnMessageApplication($"Unexpected error occured! ({ex.Message})");
            }
        }
    }
}
