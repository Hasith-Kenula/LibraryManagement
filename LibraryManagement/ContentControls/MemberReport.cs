using database.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.ContentControls
{
    public class MemberReport
    {
        public int ActiveMembers { get; set; }
        public int DeActiveMembers { get; set; }
        public MemberReport()
        {
            var allMembers = new MemberData();
            var borrowedMembers = new BorrowData();

            this.ActiveMembers = borrowedMembers.GetBorrowedMembers();
            this.DeActiveMembers = allMembers.GetAllMembers() - this.ActiveMembers;

           
        }

    }
}
