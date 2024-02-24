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

namespace LibraryManagement
{
    public partial class MainWindow : Window
    {
        


        public MainWindow()
        {
            InitializeComponent();
        }

        private void title_bar_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var Login = new Admin() 
                {
                    ID = txt_userName.Text,
                    Password = txt_password.Text,
                };

                var LoginDatabase = new AdminData();

                if (LoginDatabase.validation(Login)) {

                    
                    var masterWindows = new MasterWindow();
                    masterWindows.Show();
                    this.Hide();

                }
                else
                {
                    MessageBox.Show("Invalid Login ID or Password");

                }
            }
            catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }
        }
    }
}
