using BusinessObject;

namespace DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public IEnumerable<Customer> GetBills(bool VorF) => DataContext.GetAll(VorF);

        public void InsertBill(Customer customer) => DataContext.Add(customer);

        public void UpdateBill(Customer customer) => DataContext.Update(customer);

        public void DeleteBill(int customerId) => DataContext.Remove(customerId);
    }
}
