using database.Data;
using LibraryManagement;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : UserControl
    {
        
        public Report()
        {
            InitializeComponent();
            LoadChart();

        }
        public void LoadChart() 
        {
            try
            {
                var bookReport = new BookReport();
                var memberReport = new MemberReport();

                availableBooksSeries.Values = new ChartValues<int> { bookReport.AvaiableBookCount };
                BorrowedBooksSeries.Values = new ChartValues<int> { bookReport.BorrowedBookCount };

                activeMembers.Values = new ChartValues<int> { memberReport.ActiveMembers };
                deActiveMembers.Values = new ChartValues<int> { memberReport.DeActiveMembers };
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message,"Exceptional Error Report Class --> LoadChart()");
            }

        }

    }
}
