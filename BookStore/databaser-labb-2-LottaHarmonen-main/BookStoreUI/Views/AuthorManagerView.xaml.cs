using Common.Services;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace BookStoreUI.Views
{
    /// <summary>
    /// Interaction logic for AuthorManagerView.xaml
    /// </summary>
    public partial class AuthorManagerView : UserControl
    {
        static BookStoreContext context = new BookStoreContext();
        BookRepository repo = new BookRepository(context);
        AuthorRepository repo1 = new AuthorRepository(context);

        public AuthorManagerView()
        {
            InitializeComponent();

            AllAuthorsView.ItemsSource = repo1.GetAll();

            repo1.AuthorListChanged += OnAuthorListChanged;
        }

        private void OnAuthorListChanged()
        {

            AllAuthorsView.ItemsSource = repo1.GetAll();

        }

        private void AllAuthorsView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var author = (AuthorModel)AllAuthorsView.SelectedItem;

            if (author == null)
            {
                OnAuthorListChanged();
            }
            else
            {
                FirstName.Text = author.Firstname;


                if (author.Lastname == null)
                {
                    LastName.Text = "Unkown";
                }
                else
                {
                    LastName.Text = author.Lastname;
                }

                DatOfBirth.Text = author.DateOfBirth.ToString();
            }


            
        }

        private void UpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {

            //get the chosen authorModel from the listView
            var chosenAuthorModel = (AuthorModel)AllAuthorsView.SelectedValue;

            //create a new author based on the information in the boxes
            string boxDateText = DatOfBirth.Text;
            DateOnly dateOfBirth;
            if (DateOnly.TryParseExact(boxDateText, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dateOfBirth))
            {
                AuthorModel newAuthorInformation = new AuthorModel()
                {
                    Firstname = FirstName.Text,
                    Lastname = LastName.Text,
                    DateOfBirth = dateOfBirth,
                };

                repo1.Update(newAuthorInformation, chosenAuthorModel);
            }


        }


        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //Check if theres is a user with exact same firstname + lastname 

            var firstnameInput = FirstName.Text;
            var lastnameInput = LastName.Text;

            string boxDateText = DatOfBirth.Text;
            DateOnly dateOfBirthInput;
            if (DateOnly.TryParseExact(boxDateText, "yyyy-MM-dd", CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out dateOfBirthInput))
            {

                AuthorModel newAuthorModel = new AuthorModel()
                {
                    Firstname = FirstName.Text,
                    Lastname = LastName.Text,
                    DateOfBirth = dateOfBirthInput,
                };

                repo1.Add(newAuthorModel);
            }
            else
            {
                MessageBox.Show("Check date of birth input");
            }
        }
    }
}
