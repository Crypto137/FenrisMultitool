using LibFenris.DataManagement;

namespace FenrisMultitool
{
    class Program
    {
        static void Main(string[] args)
        {
            DataManager dataManager = new();
            dataManager.ParseFiles(args);
            dataManager.Export();

            Console.WriteLine("Finished");
            Console.ReadLine();
        }
    }
}
