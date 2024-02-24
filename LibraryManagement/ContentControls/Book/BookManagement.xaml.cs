using database.Data;
using database.Models;
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

namespace LibraryManagement.ContentControls
{

    public partial class BookManagement : UserControl
    {
        public BookManagement()
        {
            InitializeComponent();
            LoadBookData();

        }
        public void LoadBookData() 
        {

            try
            {
                BookData bookdata = new BookData();
                var booksList = bookdata.selectAllBookData();
                List<Book> books = booksList;
                BookDataGrid.ItemsSource = books;
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message,"Exception Error in BookManagement Class --> LoadBookData()",MessageBoxButton.OK);
            }
        }
        private void cb_editUpdate_Checked(object sender, RoutedEventArgs e)
        {
            cb_Add.IsChecked = false;
            Book_Edit_Panel.Visibility  = Visibility.Visible;

        }
        private void cb_editUpdate_Unchecked(object sender, RoutedEventArgs e)
        {
            Book_Edit_Panel.Visibility = Visibility.Hidden;
        }
        private void cb_Add_Checked(object sender, RoutedEventArgs e)
        {
            cb_editUpdate.IsChecked = false;
            Book_Add_Panel.Visibility = Visibility.Visible;
        }
        private void cb_Add_Unchecked(object sender, RoutedEventArgs e)
        {
            Book_Add_Panel.Visibility = Visibility.Hidden;
        }
        private void btn_update_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txt_UpdateID.Text))
                {
                    var editbook = new Book()
                    {
                        ID = Convert.ToInt32(txt_UpdateID.Text),
                        Title = txt_UpdateTitle.Text,
                        Publisher = txt_UpdatePublisher.Text,
                        Author = txt_UpdateAuthor.Text,
                        ISBN = txt_UpdateISBN.Text,
                        Category = txt_UpdateCategory.Text,
                        QTY = Convert.ToInt32(txt_UpdateQTY.Text),
                    };

                    var bookData = new BookData();

                    if (bookData.UpdateBookData(editbook))
                    {
                        MessageBox.Show("Update Successful", "Update Done", MessageBoxButton.OK);
                        LoadBookData();
                    }
                    else
                    {
                        MessageBox.Show("No Row Were Updated", "Update Fail", MessageBoxButton.OK);
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception Error in BookManagement Class --> btn_update_Click()", MessageBoxButton.OK);
            }
        }
        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_AddTitle.Text) && !string.IsNullOrEmpty(txt_AddPublisher.Text) && !string.IsNullOrEmpty(txt_AddAuthor.Text) && !string.IsNullOrEmpty(txt_AddISBN.Text) && !string.IsNullOrEmpty(txt_AddCategory.Text))
                {
                    var newBook = new Book() 
                    {
                        Title = txt_AddTitle.Text,
                        Publisher = txt_AddPublisher.Text,
                        Author = txt_AddAuthor.Text,
                        ISBN = txt_AddISBN.Text,
                        Category = txt_AddCategory.Text,
                        QTY = Convert.ToInt32(txt_AddQTY.Text),
                    };
                    var bookData = new BookData();
                    if (bookData.AddBookData(newBook))
                    {
                        MessageBox.Show("Insert Successful", "Insert Done", MessageBoxButton.OK);
                        LoadBookData();
                    }
                    else 
                    {
                        MessageBox.Show("Insert Fail", "Insert Fail", MessageBoxButton.OK);
                    }
                }
            } 
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Exception Error BookManagement Class --> btn_Add_Click()");
            }
        }
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            try 
            {
                var bookData = new BookData();

                var searchResult = bookData.SearchBooks(SearchBox.Text);

                BookDataGrid.ItemsSource = searchResult;
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message, "Exception Error BookManagement Class --> SearchBox_TextChanged()");
            }
        }
        private void txt_UpdateID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_UpdateID.Text) && int.TryParse(txt_UpdateID.Text, out int validate))
                {
                    var bookData = new BookData();
                    var bookDetail = bookData.SelectEditBook(Convert.ToInt32(txt_UpdateID.Text));

                    txt_UpdateTitle.Text = bookDetail.Title;
                    txt_UpdatePublisher.Text = bookDetail.Publisher;
                    txt_UpdateAuthor.Text = bookDetail.Author;
                    txt_UpdateISBN.Text = bookDetail.ISBN;
                    txt_UpdateCategory.Text = bookDetail.Category;
                    txt_UpdateQTY.Text = bookDetail.QTY.ToString();

                }
                else
                {
                    txt_UpdateTitle.Clear();
                    txt_UpdatePublisher.Clear();
                    txt_UpdateAuthor.Clear();
                    txt_UpdateISBN.Clear();
                    txt_UpdateCategory.Clear();
                    txt_UpdateQTY.Clear();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exceptional Error BookManagement Class --> txt_ID_TextChanged()", MessageBoxButton.OK);
            }
        }
    }
}
