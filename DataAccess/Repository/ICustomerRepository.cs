using BusinessObject;

namespace DataAccess.Repository
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetBills(bool VorF);

        void InsertBill(Customer customer);

        void UpdateBill(Customer customer);

        void DeleteBill(int customerId);
    }
}
