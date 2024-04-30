using Common.Services;
using DataAccess;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
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
using System.Globalization;

namespace BookStoreUI
{
    /// <summary>
    /// Interaction logic for BookManagerView.xaml
    /// </summary>
    public partial class BookManagerView : UserControl
    {
        static BookStoreContext context = new BookStoreContext();
        BookRepository bookRepository = new BookRepository(context);
        AuthorRepository authorRepository = new AuthorRepository(context);
        IllustratorRepository illustratorRepository = new IllustratorRepository(context);
        SeriesRepository seriesRepository = new SeriesRepository(context);


        public BookManagerView()
        {
            InitializeComponent();

            BoxAuthor.ItemsSource = authorRepository.GetAll();
            BoxIllustrator.ItemsSource = illustratorRepository.GetAll();
            BoxIllustrator2.ItemsSource = illustratorRepository.GetAll();
            BoxAuthor2.ItemsSource = authorRepository.GetAll();
            BoxSeries.ItemsSource = seriesRepository.GetAll();

            BookRepoOnBookListChanged();

            bookRepository.BookListChanged += BookRepoOnBookListChanged;

            AllBooksView.ItemsSource = bookRepository.GetAll();
        }


        private void BookRepoOnBookListChanged()
        {
            AllBooksView.ItemsSource = bookRepository.GetAll();
        }


        private void AllBooksView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BoxAuthor2.Text = string.Empty;
            BoxIllustrator2.Text = string.Empty;

            var bookmodel = (BookModel)AllBooksView.SelectedItem;

            if (bookmodel == null)
            {
                return;
            }

            var book = bookRepository.getById(bookmodel.Isbn);
            
            if (book == null)
            {
                BookRepoOnBookListChanged();
            }
            else
            {
                BoxISBN.Text = book.Isbn.ToString();
                BoxTitle.Text = book.Name;
                BoxLanguage.Text = book.Language;

                var author = authorRepository.GetById(book.Isbn);
                
                if (author.Count() == 1)
                {
                    BoxAuthor.Text = author.ElementAt(0).Firstname;
                }
                else if (author.Count() >= 2)
                {
                    BoxAuthor.Text = author.ElementAt(0).Firstname;
                    BoxAuthor2.Text = author.ElementAt(1).Firstname;
                    
                }

                var illustrators = illustratorRepository.GetById(book.Isbn);

                if (illustrators.Count() == 1)
                {
                    BoxIllustrator.Text = illustrators.ElementAt(0).Firstname;
                }
                else if (illustrators.Count() >= 2)
                {
                    BoxIllustrator.Text = illustrators.ElementAt(0).Firstname;
                    BoxIllustrator2.Text = illustrators.ElementAt(1).Firstname;
                }

                var series = seriesRepository.GetById(book.Isbn);
                BoxSeries.Text = series.Name;


                var date = bookRepository.GetDate(book.Isbn);
                BoxDate.Text = date.PublicationDate.ToString();
            }
        }


        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(BoxISBN.Text) || string.IsNullOrWhiteSpace(BoxAuthor.Text) ||
         string.IsNullOrWhiteSpace(BoxDate.Text) || string.IsNullOrWhiteSpace(BoxIllustrator.Text) ||
         string.IsNullOrWhiteSpace(BoxTitle.Text) || string.IsNullOrWhiteSpace(BoxLanguage.Text))
            {
                MessageBox.Show("Fill in all the information needed");
                return;
            }

            string cleanedIsbn = new string(BoxISBN.Text.Where(char.IsDigit).ToArray());

            if (cleanedIsbn.Length != 13 || !long.TryParse(cleanedIsbn, out long isbn))
            {
                MessageBox.Show("Invalid ISBN format. Please enter a 13-digit numeric ISBN.");
                return;
            }

            if (bookRepository.IfISBNExists(isbn))
            {
                MessageBox.Show("Book with this ISBN13 already exists in the inventory");
                return;
            }

            if (!DateOnly.TryParseExact(BoxDate.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var publicationDate))
            {
                MessageBox.Show("Invalid date format. Use yyyy-MM-dd");
                return;
            }

            var selectedAuthorModel = authorRepository.GetByName(BoxAuthor.Text);
            var selectedIllustratorModel = illustratorRepository.GetByName(BoxIllustrator.Text);
            var selectedSeriesModel = seriesRepository.GetByName(BoxSeries.Text);

            var newbook = new BookModel
            {
                Name = BoxTitle.Text,
                Isbn = isbn,
                Language = BoxLanguage.Text,
            };

            var spec = new BookSpecificationModel()
            {
                Isbn = isbn,
                PublicationDate = publicationDate,
                AuthorId = (int)selectedAuthorModel?.AuthorId,
                IllustratorId = (int)selectedIllustratorModel?.IllustratorId,
                SeriesId = (int)selectedSeriesModel?.SeriesId,
            };


            bookRepository.Add(newbook);
            bookRepository.AddSpec(spec);

            if (BoxAuthor2 != null)
            {
                AuthorModel selectedFirstName = (AuthorModel)BoxAuthor2.SelectedItem;

                var specAuthor2 = new BookSpecificationModel()
                {
                    Isbn = isbn,
                    PublicationDate = publicationDate,
                    AuthorId = selectedFirstName.AuthorId,
                    IllustratorId = (int)selectedIllustratorModel?.IllustratorId,
                    SeriesId = (int)selectedSeriesModel?.SeriesId,
                };

                bookRepository.AddSpec(specAuthor2);

            }


            MessageBox.Show("Book added successfully");

        }

        private void UpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var allbooks = bookRepository.GetAll();

            bool isMatchFound = false;
            bool emptyInput = false;

            foreach (var book in allbooks)
            {
                if (BoxISBN.Text == "" || BoxAuthor == null || BoxDate.Text == null || BoxIllustrator.Text == null || BoxTitle.Text == "" || BoxLanguage.Text == "")
                {
                    emptyInput = true;
                    break;
                }

                else if (book.Isbn == Convert.ToInt64(BoxISBN.Text))
                {
                    isMatchFound = true;
                    break;
                }
            }

            if (emptyInput)
            {
                MessageBox.Show("Fill in all the information needed");
            }
            else
            {
                if (isMatchFound)
                {
                    var newbook = new BookModel
                    {
                        Name = BoxTitle.Text,
                        Isbn = Convert.ToInt64(BoxISBN.Text),
                        Language = BoxLanguage.Text,
                    };

                    var selectedAuthorModel = authorRepository.GetByName(BoxAuthor.Text);
                    var selectedIllustratorModel = illustratorRepository.GetByName(BoxIllustrator.Text);
                    var selectedSeriesModel = seriesRepository.GetByName(BoxSeries.Text);

                    string boxDateText = BoxDate.Text;
                    DateOnly publicationDate;
                    if (DateOnly.TryParseExact(boxDateText, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out publicationDate))
                    {
                        var spec = new BookSpecificationModel()
                        {
                            Isbn = Convert.ToInt64(BoxISBN.Text),
                            PublicationDate = publicationDate,
                            AuthorId = selectedAuthorModel.AuthorId,
                            IllustratorId = selectedIllustratorModel.IllustratorId,
                            SeriesId = selectedSeriesModel.SeriesId,
                        };

                        bookRepository.Update(newbook, spec);

                    }
                }
                else
                {
                    MessageBox.Show("Book with this ISBN doesn't exist");
                }
            }
        }
    }
}