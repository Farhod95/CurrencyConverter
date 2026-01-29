using CurrencyConverter.Data;
using CurrencyConverter.Models;
using System.Text.Json;
using System.Threading.Tasks;

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
            foreach (var item in this.Context.valyutalar)
            {
                string id = item.Id.PadRight(5);
                string mamlakat = item.Mamlakat.PadRight(20);
                string nomi = item.ValyutaNomi.PadRight(20);
                string kodi = item.Kodi;

                Console.WriteLine($"{id} {mamlakat} {nomi} {kodi}");
            }
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
            string valyutaKodi = Console.ReadLine();
            
            switch (valyutaKodi)
            {
                case "USD":
                    {
                        Console.WriteLine($" 1 USD (AQSH Dollari) = {somKusrsi} So'm");
                        break;
                    }
                case "EUR":
                    {
                        Console.WriteLine($" 1 EUR (Evro) = {somKusrsi / rootobject.usd.eur} So'm");
                        break;
                    }
                case "GBP":
                    {
                        Console.WriteLine($" 1 GBP (Funt Sterling) = {somKusrsi / rootobject.usd.gbp} So'm");
                        break;
                    }
                case "RUB":
                    {
                        Console.WriteLine($" 1 RUB (Rossiya Rubli) = {somKusrsi / rootobject.usd.rub} So'm");
                        break;
                    }
                case "KZT":
                    {
                        Console.WriteLine($" 1 KZT (Qozoq Tengesi) = {somKusrsi / rootobject.usd.kzt} So'm");
                        break;
                    }
                case "CHF":
                    {
                        Console.WriteLine($" 1 CHF (Shveytsariya Franki) = {somKusrsi / rootobject.usd.chf} So'm");
                        break;
                    }
                case "CNY":
                    {
                        Console.WriteLine($" 1 CNY (Xitoy Yuani) = {somKusrsi / rootobject.usd.cny} So'm");
                        break;
                    }
                case "KGS":
                    {
                        Console.WriteLine($" 1 KGS (Qirg'iz Somi) = {somKusrsi / rootobject.usd.kgs} So'm");
                        break;
                    }
                case "TRY":
                    {
                        Console.WriteLine($" 1 TRY (Turk Lirasi) = {somKusrsi / rootobject.usd._try} So'm");
                        break;
                    }
                default:
                    {
                        Console.WriteLine(" Noto'g'ri kod kiritildi !"); 
                        break;
                    }
            }
        }

        public async Task ValyutaAyriboshlash()
        {
            ValyutalarniKorish();
            Console.Write(" Birinchi valyutani kodini kiriting: ");
            string valyutaKodi1 = Console.ReadLine();

            Console.Write($" {valyutaKodi1} miqdorini kiriitng: ");
            float valyutaMiqdori = float.Parse(Console.ReadLine());

            Console.Write(" Ikkinchi valyutani kodini kiriting: ");
            string valyutaKodi2 = Console.ReadLine();

            var json = await client.GetStringAsync("https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@2026.1.27/v1/currencies/usd.json");
            Rootobject rootobject = JsonSerializer.Deserialize<Rootobject>(json);


        }
    }
}
