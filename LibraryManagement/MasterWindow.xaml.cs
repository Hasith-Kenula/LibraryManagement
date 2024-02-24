using database.Data;
using database.Models;
using LibraryManagement.ContentControls;
using LibraryManagement.ContentControls.ReturnWindow;
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

namespace LibraryManagement
{
    public partial class MasterWindow : Window
    {
        public MasterWindow()
        {
            InitializeComponent();
            MainContent.Content = new MemberManagement();
            Radio_MemberManagement.IsChecked= true;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal || WindowState == WindowState.Maximized) { 
            
                WindowState = WindowState.Minimized;

            }
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Radio_MemberManagement_Checked(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new MemberManagement();
        }

        private void Radio_BookManagement_Checked(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new BookManagement();
        }

        private void Radio_BorrowingManagement_Checked(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new BorrowingMangement();
        }

        private void Radio_Report_Checked(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Report();
        }

        private void btn_maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else 
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Radio_Return_Checked(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new ReturnWindow();
        }
    }
}
