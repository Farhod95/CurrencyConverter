using CurrencyConverter.Services;

namespace CurrencyConverter
{
    internal class Program
    {
        public ValyutaService Service { get; set; }
        public Program()
        {
            this.Service = new ValyutaService();
        }
        static async Task Main(string[] args)
        {
            var p = new Program();
            await p.Run();
        }
        public async Task Run()
        {
            Console.WriteLine(" Valyuta xisoblash dasturiga xush kelibsiz !");

            bool sorov = false;
            while (!sorov)
            {
                sorov = true;
                string kechagiKun = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                Console.WriteLine("\n Kerakli menyudan birini tanlang !\n");
                Console.WriteLine(" 1. Valyutalar ro'yxatini ko'rish");
                Console.WriteLine($" 2. Valyuta kurslarini O'zbek so'miga nisbatini ko'rish: {kechagiKun} ");
                Console.WriteLine(" 3. Valyutani boshqa valyutaga konvertatsiya qilish: ");
                Console.WriteLine();
                Console.Write(" Kerakli bo'lim raqamini kiriting: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            Console.WriteLine();
                            Service.ValyutalarniKorish();
                            if (Service.QaytaIshgaTushir()) { sorov = false; continue; }
                            else { continue; }
                        }
                    case "2":
                        {
                            Console.WriteLine();
                            await Service.DollorniKursi();
                            if (Service.QaytaIshgaTushir()) { sorov = false; continue; }
                            else { continue; }
                        }
                    case "3":
                        {
                            Console.WriteLine();
                            Console.WriteLine(" Konvertatsiya qilish ");
                            if (Service.QaytaIshgaTushir()) { sorov = false; continue; }
                            else { continue; }
                        }
                    default:
                        {
                            Console.WriteLine();
                            if (Service.QaytaIshgaTushir()) { sorov = false; continue; }
                            else { continue; }
                        }
                }
            }            
        }
    }
}
