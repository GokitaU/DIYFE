
namespace MVC3Template.SearchIndex
{
    public class LuceneData
    {
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public string SearchContent { get; set; }
        //public string ShortText { get; set; }
        //public string LinkText { get; set; }
    }
}