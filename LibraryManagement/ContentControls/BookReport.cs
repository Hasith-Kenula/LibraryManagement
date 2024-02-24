using database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.ContentControls
{
    public class BookReport
    {
        public int AvaiableBookCount { get; set; }
        public int BorrowedBookCount { get; set; }
        public BookReport()
        {
            var getAvailableBooks = new BookData();
            var getBorrowedBooks = new BorrowData();

            this.AvaiableBookCount = getAvailableBooks.SelectAvailableBookCount();
            this.BorrowedBookCount = getBorrowedBooks.SelectTotalBorrowings();
        }
    }
}
