using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database.Models
{
    public class Borrow
    {
        public int ID { get; set; }
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public string BorrowDate { get; set; }
        public string ReturnDate { get; set;}
        public string RemainingDays { get; set; }
        
    }
}
