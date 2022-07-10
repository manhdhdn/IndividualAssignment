namespace BusinessObject
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; } 
    }
}
