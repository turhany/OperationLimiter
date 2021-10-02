using System;
using System.Threading.Tasks;

namespace OperationLimiter.Samples
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var operationLimiter = new OperationLimiter(2, OperationLimitType.Minute);

            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"Operation {i}: {DateTime.Now.ToLongTimeString()}");

                await operationLimiter.LimitAsync();
            }

            Console.ReadLine();
        } 
    }
}