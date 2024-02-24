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
using System.Windows.Shapes;

namespace LibraryManagement.ContentControls
{

    public partial class BorrowingMangement : UserControl
    {
        public BorrowingMangement()
        {

            InitializeComponent();
            LoadMemberData();
            LoadBookData();
            LoadBorrowData();
        }
        public void LoadMemberData()
        {
            try
            {
                var memberData = new MemberData();
                List<Member> members = memberData.SelectSpecialMembers();
                MemberDataGrid.ItemsSource = members;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BorrowingManagement Class --> LoadMemberData()", MessageBoxButton.OK);
            }
        }
        public void LoadBookData()
        {
            try
            {
                var booksSpecialData = new BookData();
                List<Book> books = booksSpecialData.SelectBooksSpecialData();
                BookDataGrid.ItemsSource = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BorrowingManagement Class --> LoadBookData()", MessageBoxButton.OK);
            }
        }
        public void LoadBorrowData()
        {
            try
            {
                var borrowData = new BorrowData();
                List<Borrow> borrowings = borrowData.SelectAllBorrowData();
                BorrowingDataGrid.ItemsSource = borrowings;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BorrowingManagement Class --> LoadBorrowData()", MessageBoxButton.OK);
            }
        }
        private void MemberSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var memberData = new MemberData();
                List<Member> members = memberData.SearchSpecialMembers(MemberSearchBox.Text);
                MemberDataGrid.ItemsSource = members;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BorrowingManagement Class --> MemberSearchBox_TextChanged()", MessageBoxButton.OK);
            }
        }

        private void BookSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var bookData = new BookData();

                List<Book> books = bookData.SearchBooksSpecialData(BookSearchBox.Text);

                BookDataGrid.ItemsSource = books;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BorrowingManagement Class --> BookSearchBox_TextChanged()", MessageBoxButton.OK);
            }
        }

        private void cb_Return_Checked(object sender, RoutedEventArgs e)
        {
            cb_Edit.IsChecked = false;
            cb_Reserve.IsChecked = false;
            Return_Panel.Visibility = Visibility.Visible;


        }
        private void cb_Return_Unchecked(object sender, RoutedEventArgs e)
        {
            Return_Panel.Visibility = Visibility.Hidden;
        }
        private void cb_Reserve_Checked(object sender, RoutedEventArgs e)
        {
            cb_Edit.IsChecked = false;
            cb_Return.IsChecked = false;
            Reservation_Panel.Visibility = Visibility.Visible;
            txt_BorrowDate.Text = GenerateToday();
            txt_ReservationReturnDate.Text = GenerateReturnDate();
        }

        private void cb_Reserve_Unchecked(object sender, RoutedEventArgs e)
        {
            Reservation_Panel.Visibility = Visibility.Hidden;
        }

        private void cb_Edit_Checked(object sender, RoutedEventArgs e)
        {
            cb_Return.IsChecked = false;
            cb_Reserve.IsChecked = false;

            Edit_Panel.Visibility = Visibility.Visible;
        }

        private void cb_Edit_Unchecked(object sender, RoutedEventArgs e)
        {
            Edit_Panel.Visibility = Visibility.Hidden;
        }
        public string GenerateToday()
        {
            DateTime today = DateTime.Today;
            return today.ToString("yyyy-MM-dd");
        }
        public string GenerateReturnDate()
        {
            DateTime today = DateTime.Today;
            DateTime returnDate = today.AddDays(14);
            return returnDate.ToString("yyyy-MM-dd");
        }
        private void btn_reserve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_ReservationBookID.Text) && !string.IsNullOrEmpty(txt_ReservationMemberID.Text) && !string.IsNullOrEmpty(txt_ReservationReturnDate.Text))
                {
                    var borrow = new Borrow()
                    {
                        BookID = Convert.ToInt32(txt_ReservationBookID.Text),
                        MemberID = Convert.ToInt32(txt_ReservationMemberID.Text),
                        BorrowDate = txt_BorrowDate.Text,
                        ReturnDate = txt_ReservationReturnDate.Text
                    };

                    var memberAvailability = new MemberData();

                    if (memberAvailability.CheckMemberAvailability(borrow.MemberID))
                    {
                        var checkMemberBorrowCount = new BorrowData();

                        if (checkMemberBorrowCount.CheckMemberBorrowCount(borrow.MemberID))
                        {
                            var checkStock = new BookData();

                            if (checkStock.CheckBookStock(borrow.BookID) > 0)
                            {
                                var bookReserve = new BorrowData();

                                if (bookReserve.Reservation(borrow))
                                {
                                    var updateBookStock = new BookData();
                                    updateBookStock.UpdateBookStockAfterReserveBook(borrow.BookID);
                                    MessageBox.Show("Borrow a Book", "Done", MessageBoxButton.OK);
                                    LoadBookData();
                                    LoadBorrowData();
                                }
                                else
                                {
                                    MessageBox.Show("Borrow a Fail", "Error", MessageBoxButton.OK);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Can't Reserve that Book Because Book was Out of Stock");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Member Already Get 2 Books", "OOPS!", MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid Member ID", "OOPS!", MessageBoxButton.OK);
                    }




                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BorrowingManagement Class --> btn_reserve_Click()", MessageBoxButton.OK);
            }
        }
        private void btn_Return_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txt_ReturnBookID.Text) && !string.IsNullOrEmpty(txt_ReturnBorrowID.Text) && !string.IsNullOrEmpty(txt_ReturnMemberID.Text))
                {
                    var _return = new Borrow()
                    {
                        BookID = Convert.ToInt32(txt_ReturnBookID.Text),
                        MemberID = Convert.ToInt32(txt_ReturnMemberID.Text),
                        ID = Convert.ToInt32(txt_ReturnBorrowID.Text)
                    };
                    var getBorrowDate = new BorrowData();
                    var addReturn = new Return()
                    {
                        BookID = _return.BookID,
                        MemberID = _return.MemberID,
                        BorrowID = _return.ID,
                        BorrowDate = getBorrowDate.GetBorrowDate(_return.ID),
                        ReturnedDate = GenerateToday()
                    };
                    var deleteBorrow = new BorrowData();
                    if (deleteBorrow.DeleteBorrow(_return))
                    {
                        var updateBookStock = new BookData();
                        updateBookStock.UpdateBookStockAfterReturnBook(addReturn.BookID);

                        var addReturnBook = new ReturnData();
                        addReturnBook.AddReturnBook(addReturn);
                        MessageBox.Show("Book was Return");
                        LoadBookData();
                        LoadBorrowData();
                    }
                    else
                    {

                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BorrowingManagement Class --> btn_Return_Click()", MessageBoxButton.OK);
            }
        }
        private void BorrowingSearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var searchResult = new BorrowData();
                BorrowingDataGrid.ItemsSource = searchResult.SearchBorrowData(BorrowingSearchBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BorrowingManagement Class -->BorrowingSearchBox_TextChanged()", MessageBoxButton.OK);
            }
        }

        private void txt_ReturnBorrowID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_ReturnBorrowID.Text) && int.TryParse(txt_ReturnBorrowID.Text, out int validation))
            {
                try
                {
                    var getReturnDetail = new BorrowData();
                    var details = getReturnDetail.SelectSpecailBorrowData(Convert.ToInt32(txt_ReturnBorrowID.Text));
                    txt_ReturnBookID.Text = details.BookID.ToString();
                    txt_ReturnMemberID.Text = details.MemberID.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "BorrowingManagement Class -->txt_ReturnBorrowID_TextChanged()", MessageBoxButton.OK);
                }
            }
            else
            {
                txt_ReturnMemberID.Clear();
                txt_ReturnBookID.Clear();
            }
        }
        private void txt_EditBorrowID_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_EditBorrowID.Text) && int.TryParse(txt_EditBorrowID.Text, out int validate))
                {
                    var someBorrowData = new BorrowData();
                    var details = someBorrowData.SelectEditBorrowDetails(Convert.ToInt32(txt_EditBorrowID.Text));
                    txt_EditBookID.Text = details.BookID.ToString();
                    txt_EditMemberID.Text = details.MemberID.ToString();
                }
                else
                {
                    txt_EditBorrowID.Clear();
                    txt_EditBookID.Clear();
                    txt_EditMemberID.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "BorrowingManagement Class -->txt_EditBorrowID_TextChanged()", MessageBoxButton.OK);
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                var updateBookData = new BookData();
                var getBookFromBorrowData = new BorrowData();
                var memberBorrowCount = new BorrowData();
                var memberID = new BorrowData();
                if (memberBorrowCount.CheckMemberBorrowCount(Convert.ToInt32(txt_EditMemberID.Text)) || memberID.GetMemberID(Convert.ToInt32(txt_EditBorrowID.Text)) == Convert.ToInt64(txt_EditMemberID.Text))
                {
                    if (updateBookData.UpdateBookQTY(getBookFromBorrowData.GetBookID(Convert.ToInt32(txt_EditBorrowID.Text))))
                    {
                        var borrow = new Borrow()
                        {
                            BookID = Convert.ToInt32(txt_EditBookID.Text),
                            MemberID = Convert.ToInt32(txt_EditMemberID.Text),
                            ID = Convert.ToInt32(txt_EditBorrowID.Text)
                        };
                        var updateBorrowData = new BorrowData();
                        updateBorrowData.UpdateBorrowData(borrow);

                        var updateNewBookStock = new BookData();
                        updateNewBookStock.UpdateBookStockAfterReserveBook(borrow.BookID);
                        MessageBox.Show("Edit Successfull", "Done", MessageBoxButton.OK);
                        LoadBookData();
                        LoadBorrowData();
                    }
                }
                else 
                {
                    MessageBox.Show("Member Already Get 2 Books", "Can't Edit", MessageBoxButton.OK);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
