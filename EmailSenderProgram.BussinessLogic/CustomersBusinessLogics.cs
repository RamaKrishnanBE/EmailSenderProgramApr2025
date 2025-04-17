using EmailSenderProgram.DataAccess;

namespace EmailSenderProgram.BussinessLogic
{
    public class CustomersBusinessLogics
    {
        public List<Customer> GetCustomersForWelcomeMail()
        {
            //If the customer is newly registered, one day back in time
            DateTime today = DateTime.Today;
            DateTime yesterdayStart = today.AddDays(-1);

            return DataLayer.ListCustomers().Where(c => c.CreatedDateTime.Date == yesterdayStart.Date).ToList();
        }

        public List<Customer> GetCustomersForComebackMail()
        {
            var orderedCustomersEmail = DataLayer.ListOrders().Select(o => o.CustomerEmail);
            return DataLayer.ListCustomers().Where(c => !orderedCustomersEmail.Contains(c.Email)).ToList();
        }
    }
}
