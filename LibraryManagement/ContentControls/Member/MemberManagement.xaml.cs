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
    public partial class MemberManagement : UserControl
    {
        public MemberManagement()
        {
            InitializeComponent();
            Load();
            SearchBox.Focus();

        }
        public void Load() 
        {
            var SelectData = new MemberData();
            var data = SelectData.SelectAllMemberData();
            List<Member> members = data;
            MemberDataGrid.ItemsSource = members;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("click");
            var row = sender as DataGridRow;
            var member = row.DataContext as Member;
            MessageBox.Show($"click {member.Name}");
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            var member = new Member()
            {
                Name = txt_editName.Text,
                Email = txt_editEmail.Text,
                Contact_No = txt_editContactNo.Text,
            };

            var memberData = new MemberData();

            if (!string.IsNullOrEmpty(txt_editId.Text))
            {

                bool update = memberData.UpdateMember(member, Convert.ToInt32(txt_editId.Text));

                if (update)
                {
                    MessageBox.Show("Update successful!", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                    Load();
                }
                else
                {
                    MessageBox.Show("No rows were updated.", "Fail", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void txt_editId_TextChanged(object sender, TextChangedEventArgs e)
        {
            var memberData = new MemberData();
            if (!string.IsNullOrEmpty(txt_editId.Text) && int.TryParse(txt_editId.Text , out int result))
            {
                var memberDetails = memberData.SelectEditMember(Convert.ToInt32(txt_editId.Text));


                txt_editName.Text = memberDetails.Name;
                txt_editEmail.Text = memberDetails.Email;
                txt_editContactNo.Text = memberDetails.Contact_No;
            }
            else
            {
                txt_editName.Clear();
                txt_editEmail.Clear();
                txt_editContactNo.Clear();

            }
        }

        private void cb_edit_Checked(object sender, RoutedEventArgs e)
        {
            Member_Edit_Panel.Visibility = Visibility.Visible;
            cb_Add.IsChecked = false;
            
        }

        private void cb_edit_Unchecked(object sender, RoutedEventArgs e)
        {
            Member_Edit_Panel.Visibility = Visibility.Hidden;
        }

        private void cb_Add_Checked(object sender, RoutedEventArgs e)
        {
            Member_Add_Panel.Visibility= Visibility.Visible;
            cb_edit.IsChecked = false;
            

        }

        private void cb_Add_Unchecked(object sender, RoutedEventArgs e)
        {
            Member_Add_Panel.Visibility = Visibility.Hidden;
        }

        private void Add_Member_Click(object sender, RoutedEventArgs e)
        {
            
            try 
            {
                if (!string.IsNullOrEmpty(txt_AddName.Text) && !string.IsNullOrEmpty(txt_AddEmail.Text) && !string.IsNullOrEmpty(txt_AddContactNo.Text))
                {
                    Member member = new Member() 
                    {
                        Name = txt_AddName.Text,
                        Email = txt_AddEmail.Text,
                        Contact_No = txt_AddContactNo.Text,
                    };
                    MemberData memberData = new MemberData();
                    if (memberData.AddMember(member))
                    {
                        MessageBox.Show("Member Insert Successfully", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load();
                    }
                    else 
                    {
                        MessageBox.Show("Insert Fali", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else 
                {
                    MessageBox.Show("Complete All Feilds","Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void btn_delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txt_editId.Text))
                {
                    var memberData = new MemberData();
                    if (memberData.DeleteMember(Convert.ToInt32(txt_editId.Text)))
                    {
                        MessageBox.Show("Delete Successfull", "Delete", MessageBoxButton.OK);
                        Load();
                    }
                    else
                    {
                        MessageBox.Show("No Row were Delete!", "Delete", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try 
            {
                string searchText = SearchBox.Text;
                var memberData = new MemberData();
                var searchMemberDetails = memberData.SearchMember(searchText);

                MemberDataGrid.ItemsSource = searchMemberDetails; 
            } 
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}