using System;

namespace GroceryCo
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = args[0];
            string invoice;

            try
            {
                invoice = new Application().PrintInvoice(directory);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }

            Console.WriteLine(invoice);
        }
    }
}