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
            bool sorov = false;
            while (!sorov)
            {
                Console.Clear();
                sorov = true;
                // --- BANNER QISMI ---
                Console.ForegroundColor = ConsoleColor.Yellow;               
                Console.WriteLine("\n====================================================");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("   VALYUTA HISOBLASH DASTURIGA XUSH KELIBSIZ!");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("====================================================\n");
                Console.ResetColor();

                string kechagiKun = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                Console.WriteLine(" Kerakli menyudan birini tanlang !\n");
                Console.WriteLine(" [1] Valyutalar ro'yxatini ko'rish");
                Console.WriteLine($" [2] Valyuta kurslari (O‘zbek so‘mi (UZS) ga nisbatan) - {kechagiKun} kun uchun");
                Console.WriteLine(" [3] Konvertatsiya qilish");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" [0] Chiqish");
                Console.ResetColor();

                Console.Write("\n >>> Bo'limni tanlang: ");

                switch (Console.ReadLine())
                {
                    case "0":
                        {
                            Console.WriteLine();
                            return;
                        }
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
                            await Service.ValyutaAyriboshlash();
                            if (Service.QaytaIshgaTushir()) { sorov = false; continue; }
                            else { continue; }
                        }
                    default:
                        {
                            Console.WriteLine();
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(" Noto'gri bo'lim tanlandi !");
                            Console.ResetColor();
                            Console.WriteLine();
                            if (Service.QaytaIshgaTushir()) { sorov = false; continue; }
                            else { continue; }
                        }
                }
            }
        }
    }
}
