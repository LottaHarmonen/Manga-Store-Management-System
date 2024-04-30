using System;
using System.Collections.Generic;
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
using Common.Models;
using Common.Services;
using DataAccess;
using DataAccess.Entities;

namespace BookStoreUI.Views
{
    /// <summary>
    /// Interaction logic for BookStoreManagerView.xaml
    /// </summary>
    public partial class BookStoreManagerView : UserControl
    {
        static BookStoreContext context = new BookStoreContext();
        BookRepository repo = new BookRepository(context);
        StoreRepository repo1 = new StoreRepository(context);

        public BookStoreManagerView()
        {
            InitializeComponent();

            var BookModelList = repo.GetAll();

            foreach (var book in BookModelList)
            {
                AllBooksListView.Items.Add($"{book.Name}");
            }

            ComboboxStores.ItemsSource = repo1.GetAll();
            ComboboxStores.SelectedValue = ComboboxStores.Items[0];

            repo.BookListChanged += BookRepoOnBookListChanged;
            repo.StoreBookListChanged += BookRepoStoreBookListChanged;
        }

       
        private void BookRepoStoreBookListChanged()
        {
            var chosenStore = (StoreModel)ComboboxStores.SelectedItem;
            var storeByName = repo1.GetAll();

            foreach (var storemodel in storeByName)
            {
                if (storemodel.Name == chosenStore.Name)
                {
                    var store = new StoreModel
                    {
                        Name = storemodel.Name,
                        BuildingNumber = storemodel.BuildingNumber,
                        City = storemodel.City,
                        PostalCode = storemodel.PostalCode,
                        StoreId = storemodel.StoreId,
                        Streetname = storemodel.Streetname,
                    };
                    var allBooksInChosenStore = repo.GetAllByStore(store);
                    BooksPerStore.ItemsSource = allBooksInChosenStore.Select(kv => new { Title = kv.Key, Quantity = kv.Value }).ToList();
                }
            }
        }

        private void BookRepoOnBookListChanged()
        {
            AllBooksListView.Items.Clear();
            var BookModelList = repo.GetAll();

            foreach (var book in BookModelList)
            {
                AllBooksListView.Items.Add($"{book.Name}");
            }
        }

        private void ComboboxStores_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BookRepoStoreBookListChanged();
        }

        private void TransferBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedItem = AllBooksListView.SelectedItem;
            var selectedStore = ComboboxStores.SelectedItem;

            if (selectedItem == null || selectedStore == null)
            {
                MessageBox.Show("No book or store selected");
            }
            else
            {

                repo.TransferBooks(selectedItem, selectedStore);
            }
        }

        private void RemoveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedBook = BooksPerStore.SelectedItem;
            var selectedStore = ComboboxStores.SelectedItem;

            if (BooksPerStore.SelectedItem == null)
            {
                MessageBox.Show("No book selected");
            }
            else
            {
                repo.DeleteBooksFromStore(selectedBook, selectedStore);
            }
        }
    }
}
