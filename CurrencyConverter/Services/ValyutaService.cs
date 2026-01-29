using CurrencyConverter.Data;
using CurrencyConverter.Models;
using System.Text.Json;

namespace CurrencyConverter.Services
{
    public class ValyutaService
    {
        public DbContext Context { get; set; }
        HttpClient client { get; set; }
        public ValyutaService()
        {
            this.Context = new DbContext();
            this.client = new HttpClient();
        }

        public void ValyutalarniKorish()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════╦══════════════════════╦══════════════════════╦══════════╗");
            Console.WriteLine("║  ID  ║      MAMLAKAT        ║     VALYUTA NOMI     ║   KODI   ║");
            Console.WriteLine("╠══════╬══════════════════════╬══════════════════════╬══════════╣");
            Console.ResetColor();

            foreach (var item in this.Context.valyutalar)
            {
                // Sarlavha qatorini o'tkazib yuborish (agar kodingizda bo'lsa)
                if (item.Id == "Id") continue;

                Console.WriteLine($"║ {item.Id.PadRight(4)} ║ {item.Mamlakat.PadRight(20)} ║ {item.ValyutaNomi.PadRight(20)} ║ {item.Kodi.PadCenter(8)} ║");
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╚══════╩══════════════════════╩══════════════════════╩══════════╝");
            Console.ResetColor();
        }
        public bool QaytaIshgaTushir()
        {
            Console.WriteLine();
            Console.Write(" Dasturni qayta ishga tushirishni istaysizmi? (yes/no): ");
            return Console.ReadLine().ToLower() == "yes";
        }

        public async Task DollorniKursi()
        {
            var json = await client.GetStringAsync("https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@2026.1.27/v1/currencies/usd.json");
            Rootobject rootobject = JsonSerializer.Deserialize<Rootobject>(json);
            float somKusrsi = rootobject.usd.uzs;

            ValyutalarniKorish();

            Console.Write("\n Kerkli valyuta kodini kiriting (M: USD, EUR):  ");
            string valyutaKodi = Console.ReadLine().ToUpper();

            switch (valyutaKodi)
            {
                case "USD":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 1 USD (AQSH Dollari) = {somKusrsi} So'm");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }
                case "EUR":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 1 EUR (Evro) = {somKusrsi / rootobject.usd.eur} So'm");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }
                case "GBP":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 1 GBP (Funt Sterling) = {somKusrsi / rootobject.usd.gbp} So'm");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }
                case "RUB":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 1 RUB (Rossiya Rubli) = {somKusrsi / rootobject.usd.rub} So'm");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }
                case "KZT":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 1 KZT (Qozoq Tengesi) = {somKusrsi / rootobject.usd.kzt} So'm");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }
                case "CHF":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 1 CHF (Shveytsariya Franki) = {somKusrsi / rootobject.usd.chf} So'm");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }
                case "CNY":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 1 CNY (Xitoy Yuani) = {somKusrsi / rootobject.usd.cny} So'm");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }
                case "KGS":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 1 KGS (Qirg'iz Somi) = {somKusrsi / rootobject.usd.kgs} So'm");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }
                case "TRY":
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write($" 1 TRY (Turk Lirasi) = {somKusrsi / rootobject.usd._try} So'm");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }
                default:
                    {
                        Console.WriteLine();
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" Noto'g'ri kod kiritildi !");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                    }
            }
        }

        public async Task ValyutaAyriboshlash()
        {
            ValyutalarniKorish();

            var kurslar = await Context.ObyektniListgaOgirish();

            Console.Write("Qaysi valyutadan (masalan, EUR): ");
            string kod1 = Console.ReadLine().ToUpper();
            var mambaKurs = kurslar.FirstOrDefault(x => x.Kodi == kod1);
            if ( mambaKurs == null)
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Kod kiritishda hatolik mavjud !");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }
            Console.Write("Miqdorni kiriting: ");
            if (float.TryParse(Console.ReadLine(), out float miqdor)) { }
            else
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Miqdorni kiritishda hatolik mavjud !");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }

            Console.Write("Qaysi valyutaga (masalan, UZS): ");
            string kod2 = Console.ReadLine().ToUpper();
            var maqsadKurs = kurslar.FirstOrDefault(x => x.Kodi == kod2);
            if (maqsadKurs == null)
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Kod kiritishda hatolik mavjud !");
                Console.ResetColor();
                Console.WriteLine();
                return;
            }

            if (mambaKurs != null && maqsadKurs != null)
            {
                float natija = (miqdor / mambaKurs.Qiymat) * maqsadKurs.Qiymat;

                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($" >>> {miqdor} {kod1} = {natija:N2} {kod2} <<< ");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" Ma'lumotlarda hatolik mavjud !");
                Console.ResetColor();
                Console.WriteLine();
            }
        }
    }
    public static class StringExtensions
    {
        public static string PadCenter(this string str, int length)
        {
            int spaces = length - str.Length;
            int padLeft = spaces / 2 + str.Length;
            return str.PadLeft(padLeft).PadRight(length);
        }
    }
}
