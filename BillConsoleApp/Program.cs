using BusinessObject;
using DataAccess.Repository;

namespace ConsoleApp
{
    public class Program
    {
        private static readonly ICustomerRepository repository = new CustomerRepository();

        public static void Main()
        {
            int choice;

            do
            {
                choice = MainMenu();

                switch (choice)
                {
                    case 1:
                        ShowAllBill();
                        PauseScreen();
                        break;
                    case 2:
                        int subChoice;

                        do
                        {
                            subChoice = AddBillMenu();

                            switch (subChoice)
                            {
                                case 1:
                                    AddForeignBill();
                                    PauseScreen();
                                    break;
                                case 2:
                                    AddVietnameseBill();
                                    PauseScreen();
                                    break;
                                default:
                                    break;
                            }
                        } while (subChoice != 3);
                        break;
                    case 3:
                        CaculateConsumed();
                        PauseScreen();
                        break;
                    case 4:
                        CaculateForeignAverage();
                        PauseScreen();
                        break;
                    default:
                        break;
                }
            }
            while (choice != 5);
        }

        public static int MainMenu()
        {
            Console.Clear();
            Console.WriteLine("=========================================|");
            Console.WriteLine("Welcome to Bill Management by Huu Manh   |");
            Console.WriteLine("Choose one of these options below:       |");
            Console.WriteLine("1. Show all bills.                       |");
            Console.WriteLine("2. Add new bill.                         |");
            Console.WriteLine("3. Calculate total KW consumed.          |");
            Console.WriteLine("4. Calculate average bill of foreigners. |");
            Console.WriteLine("5. Quit.                                 |");
            Console.WriteLine("=========================================|");
            Console.Write("Your choice: ");

            return ValidateChoice(1, 5);
        }

        public static int AddBillMenu()
        {
            Console.Clear();
            Console.WriteLine("=========================================|");
            Console.WriteLine("              ADD NEW BILL               |");
            Console.WriteLine("Choose one of these options below:       |");
            Console.WriteLine("1. Foreign Customer.                     |");
            Console.WriteLine("2. Vietnamese Customer.                  |");
            Console.WriteLine("3. Quit.                                 |");
            Console.WriteLine("=========================================|");
            Console.Write("Your choice: ");

            return ValidateChoice(1, 3);
        }

        public static Customer AddNewBill()
        {
            Customer customer = new();

            Console.Write("\nEnter ID: ");
            customer.Id = (int)ValidateNumber(1, int.MaxValue);

            Console.Write("Enter full name: ");
            var fullName = Console.ReadLine() ?? "";
            customer.FullName = fullName == "" ? "Default Name" : fullName;
            Console.WriteLine();

            Console.Write("Enter address: ");
            var address = Console.ReadLine() ?? "";
            customer.Address = address == "" ? "Default Address" : address;
            Console.WriteLine();

            Console.Write("Enter quantity (KW): ");
            customer.Quantity = (int)ValidateNumber(1, int.MaxValue);

            Console.Write("Enter unit price: ");
            customer.UnitPrice = ValidateNumber(1, decimal.MaxValue);

            return customer;
        }

        public static void AddForeignBill()
        {
            ForeignCustomer foreign = new(AddNewBill());

            Console.Write("Enter nationality: ");
            var nationality = Console.ReadLine() ?? "";
            foreign.Nationality = nationality == "" ? "Default National" : nationality;
            Console.WriteLine();

            repository.InsertBill(foreign);
        }

        public static void AddVietnameseBill()
        {
            VietnameseCustomer vietnamese = new(AddNewBill());

            Console.WriteLine("1. Living");
            Console.WriteLine("2. Business");
            Console.WriteLine("3. Production");
            Console.Write("Enter customer type: ");
            vietnamese.CustomerType = ValidateChoice(1, 3);
            Console.WriteLine();

            Console.Write("Enter quota: ");
            vietnamese.Quota = (int)ValidateNumber(1, int.MaxValue);
            Console.WriteLine();

            repository.InsertBill(vietnamese);
        }

        public static void CaculateConsumed()
        {
            var bills = repository.GetBills(true);
            decimal sum = 0;

            foreach (var bill in bills)
            {
                sum += bill.Total;
            }
            Console.WriteLine($"\nVietnamese Customer: {sum}");

            bills = repository.GetBills(false);
            sum = 0;

            foreach (var bill in bills)
            {
                sum += bill.Total;
            }
            Console.WriteLine($"\nForeign Customer: {sum}");
        }

        public static void CaculateForeignAverage()
        {
            var bills = repository.GetBills(false);
            decimal sum = 0;

            foreach (var bill in bills)
            {
                sum += bill.Total;
            }

            Console.WriteLine($"\nForeign Customer: {sum / bills.Count()}");
        }

        public static void ShowAllBill()
        {
            var bills = repository.GetBills(true);

            Console.WriteLine("\nVietnamese Customer:");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| ID |        Full Name        |            Address            |Customer Type|Quantity|Unit Price|Quota|   Toltal   |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

            foreach (var bill in bills)
            {
                Console.Write('|');
                Console.Write(Align(bill.Id.ToString(), 4));
                Console.Write(Align(bill.FullName, 25));
                Console.Write(Align(bill.Address, 31));

                switch (((VietnameseCustomer)bill).CustomerType)
                {
                    case 1:
                        Console.Write(Align("Living", 13));
                        break;
                    case 2:
                        Console.Write(Align("Business", 13));
                        break;
                    default:
                        Console.Write(Align("Production", 13));
                        break;
                }

                Console.Write(Align(bill.Quantity.ToString() + "KW", 8));
                Console.Write(Align(bill.UnitPrice.ToString(), 10));
                Console.Write(Align(((VietnameseCustomer)bill).Quota.ToString(), 5));
                Console.WriteLine(Align(bill.Total.ToString(), 12));
            }

            bills = repository.GetBills(false);

            Console.WriteLine("\nForeign Customer:");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("| ID |        Full Name        |            Address            |    Nationality    |Quantity|Unit Price|   Toltal   |");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

            foreach (var bill in bills)
            {
                Console.Write('|');
                Console.Write(Align(bill.Id.ToString(), 4));
                Console.Write(Align(bill.FullName, 25));
                Console.Write(Align(bill.Address, 31));
                Console.Write(Align(((ForeignCustomer)bill).Nationality, 19));
                Console.Write(Align(bill.Quantity.ToString() + "KW", 8));
                Console.Write(Align(bill.UnitPrice.ToString(), 10));
                Console.WriteLine(Align(bill.Total.ToString(), 12));
            }
        }

        public static decimal ValidateNumber(decimal begin, decimal end)
        {
            decimal choice;

            try
            {
                choice = decimal.Parse(Console.ReadLine() ?? "0");
            }
            catch
            {
                choice = 0;
            }

            while (choice < begin || choice > end)
            {
                Console.Beep();
                Console.WriteLine();
                Console.Write("Invalid input, enter again: ");

                try
                {
                    choice = decimal.Parse(Console.ReadLine() ?? "0");

                }
                catch
                {
                    choice = 0;
                }
            }

            Console.WriteLine();
            return choice;
        }

        public static int ValidateChoice(int begin, int end)
        {
            int choice;

            try
            {
                choice = int.Parse(Console.ReadKey(false).KeyChar.ToString());
            }
            catch
            {
                choice = 0;
            }

            while (choice < begin || choice > end)
            {
                Console.Beep();
                Console.WriteLine();
                Console.Write("Invalid input, choose again: ");

                try
                {
                    choice = int.Parse(Console.ReadKey(false).KeyChar.ToString());

                }
                catch
                {
                    choice = 0;
                }
            }

            Console.WriteLine();
            return choice;
        }

        public static void PauseScreen()
        {
            Console.Write("\nPress any key to continue..");
            Console.ReadKey(false);
        }

        public static string Align(string text, int length)
        {
            int blank = length - text.Length;

            if (blank % 2 != 0)
            {
                text = ' ' + text;
            }

            for (int i = 0; i < blank / 2; i++)
            {
                text = ' ' + text + ' ';
            }

            return text + '|';
        }
    }
}