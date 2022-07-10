namespace BusinessObject
{
    public class VietnameseCustomer : Customer
    {
        public int CustomerType { get; set; } //1. Living; 2. Business; 3. Production 
        public int Quota { get; set; }

        public VietnameseCustomer()
        {
        }

        public VietnameseCustomer(Customer customer)
        {
            Id = customer.Id;
            FullName = customer.FullName;
            Address = customer.Address;
            Quantity = customer.Quantity;
            UnitPrice = customer.UnitPrice;
        }
    }
}