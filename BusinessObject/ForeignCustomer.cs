namespace BusinessObject
{
    public class ForeignCustomer : Customer
    {
        public string Nationality { get; set; } = null!;

        public ForeignCustomer()
        {
        }

        public ForeignCustomer(Customer customer)
        {
            Id = customer.Id;
            FullName = customer.FullName;
            Address = customer.Address;
            Quantity = customer.Quantity;
            UnitPrice = customer.UnitPrice;
        }
    }
}
