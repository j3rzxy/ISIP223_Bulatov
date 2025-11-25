using ISIP223_Bulatov.Models;

namespace TextRPG // Рекомендуется вынести в отдельное пространство имён
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Для корректного отображения русских символов
            var game = new Game();
            game.Start();
        }
    }
}