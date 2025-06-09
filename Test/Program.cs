
using Final.Repositories;
using Final.Repositories.Implementations.BankAccount;
using Final.Repositories.Implementations.Transfer;
using Final.Repositories.Implementations.User;
using Final.Repositories.Implementations.UsersPerAcc;
using Final.Repositories.Interfaces.BankAccount;
using Final.Repositories.Interfaces.Transfer;
using Final.Repositories.Interfaces.User;
using Final.Repositories.Interfaces.UsersPerAcc;

namespace Test
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ConnectionFactory.Initialize("Server=DESKTOP-IG0MKVO;Database=Final;Trusted_Connection=True;TrustServerCertificate=True;");

            UserRepository userRepository = new UserRepository();
            await foreach (var a in userRepository.RetrieveCollectionAsync(new UserFilter()))
            {
                Console.WriteLine(a);
            }

            BankAccountRepository bankAccountRepository = new BankAccountRepository();
            await foreach (var a in bankAccountRepository.RetrieveCollectionAsync(new BankAccountFilter()))
            {
                Console.WriteLine(a);
            }

            TransferRepository transferRepository = new TransferRepository();
            await foreach (var a in transferRepository.RetrieveCollectionAsync(new TransferFilter()))
            {
                Console.WriteLine(a);
            }

            UsersPerAccRepository usersPerAccRepository = new UsersPerAccRepository();
            await foreach (var a in usersPerAccRepository.RetrieveCollectionAsync(new UsersPerAccFilter()))
            {
                Console.WriteLine(a);
            }
        }
    }
}
