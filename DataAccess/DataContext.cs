using BusinessObject;

namespace DataAccess
{
    public class DataContext
    {
        private static readonly List<Customer> CustomerList = new()
        {
            new VietnameseCustomer()
            {
                Id = 1,
                FullName = "Doan Huu Manh",
                Address = "1, tp Thu Duc, HCM City",
                CustomerType = 1,
                Quantity = 100,
                UnitPrice = 1700,
                Quota = 50,
                Total = 297500
            },

            new VietnameseCustomer()
            {
                Id = 2,
                FullName = "Huu Manh 2",
                Address = "2, tp Thu Duc, HCM City",
                CustomerType = 2,
                Quantity = 150,
                UnitPrice = 1700,
                Quota = 50,
                Total = 510000
            },

            new VietnameseCustomer()
            {
                Id = 3,
                FullName = "Huu Manh 3",
                Address = "3, tp Thu Duc, HCM City",
                CustomerType = 3,
                Quantity = 200,
                UnitPrice = 1700,
                Quota = 50,
                Total = 722500
            },

            new ForeignCustomer()
            {
                Id = 4,
                FullName = "Huu Manh 4",
                Address = "Some where on Earth",
                Nationality = "China",
                Quantity = 90,
                UnitPrice = 2100,
                Total = 189000
            },

            new ForeignCustomer()
            {
                Id = 5,
                FullName = "Huu Manh 5",
                Address = "Some where on Mars",
                Nationality = "Japan",
                Quantity = 80,
                UnitPrice = 2100,
                Total = 168000
            }
        };

        public static IEnumerable<Customer> GetAll(bool VorF) // True: Vietnamese; False: Foreign
        {
            if (VorF)
            {
                return CustomerList.Where(c => c.GetType() == typeof(VietnameseCustomer)).ToList();
            }

            return CustomerList.Where(c => c.GetType() == typeof(ForeignCustomer)).ToList();
        }

        public static void Add(Customer customer)
        {
            if (CustomerExits(customer.Id))
            {
                Console.WriteLine("Duplicate!");
                return;
            }

            switch (customer.GetType().Name)
            {
                case "VietnameseCustomer":
                    if (customer.Quantity > ((VietnameseCustomer)customer).Quota)
                    {
                        customer.Total = ((VietnameseCustomer)customer).Quota * customer.UnitPrice + (customer.Quantity - ((VietnameseCustomer)customer).Quota) * customer.UnitPrice * (decimal)2.5;
                    }
                    else
                    {
                        customer.Total = customer.Quantity * customer.UnitPrice;
                    }
                    break;
                default:
                    customer.Total = customer.Quantity * customer.UnitPrice;
                    break;
            }

            CustomerList.Add(customer);
            Console.WriteLine("Success!");
        }

        public static void Update(Customer customer)
        {
            var pro = CustomerList.Find(c => c.Id == customer.Id);

            if (pro != null)
            {
                CustomerList[CustomerList.IndexOf(pro)] = customer;
            }
        }

        public static void Remove(int customerId)
        {
            var customer = CustomerList.Find(c => c.Id == customerId);

            if (customer != null)
            {
                CustomerList.Remove(customer);
            }
        }

        public static bool CustomerExits(int Id) => CustomerList.Any(c => c.Id == Id);
    }
}