using CurrencyConverter.Models;
using System.Text.Json;

namespace CurrencyConverter.Data
{
    public class DbContext
    {
        public List<Valyuta> valyutalar;
        public List<KursModel> kursModelsList;
        HttpClient client { get; set; }
        public DbContext()
        {
            this.client = new HttpClient();
            this.kursModelsList = new List<KursModel>();
            this.valyutalar = new List<Valyuta>();
            valyutalar = new List<Valyuta>
            {
                new Valyuta() { Id = "Id",  Mamlakat = "Mamlakat",             ValyutaNomi = "ValyutaNomi",      Kodi = "Kodi" },
                new Valyuta() { Id = "1",  Mamlakat = "AQSH",             ValyutaNomi = "AQSH Dollari",      Kodi = "USD" },
                new Valyuta() { Id = "2",  Mamlakat = "Evropa Ittifoqi",  ValyutaNomi = "Evro",              Kodi = "EUR" },
                new Valyuta() { Id = "3",  Mamlakat = "O‘zbekiston",      ValyutaNomi = "O‘zbek so‘mi",      Kodi = "UZS" },
                new Valyuta() { Id = "4",  Mamlakat = "Buyuk Britaniya",  ValyutaNomi = "Funt Sterling",     Kodi = "GBP" },
                new Valyuta() { Id = "5",  Mamlakat = "Rossiya",          ValyutaNomi = "Rossiya Rubli",     Kodi = "RUB" },
                new Valyuta() { Id = "6",  Mamlakat = "Qozog‘iston",      ValyutaNomi = "Qozoq Tengesi",     Kodi = "KZT" },
                new Valyuta() { Id = "7",  Mamlakat = "Shveytsariya",     ValyutaNomi = "Shveytsariya Franki",Kodi = "CHF" },
                new Valyuta() { Id = "8",  Mamlakat = "Xitoy",            ValyutaNomi = "Xitoy Yuani",       Kodi = "CNY" },
                new Valyuta() { Id = "9",  Mamlakat = "Qirg‘iziston",     ValyutaNomi = "Qirg‘iz Somi",      Kodi = "KGS" },
                new Valyuta() { Id = "10", Mamlakat = "Turkiya",          ValyutaNomi = "Turk Lirasi",       Kodi = "TRY" }
            };
        }

        public async Task<List<KursModel>> ObyektniListgaOgirish()
        {
            
            this.kursModelsList.Clear();

            var json = await client.GetStringAsync("https://cdn.jsdelivr.net/npm/@fawazahmed0/currency-api@latest/v1/currencies/usd.json");
            Rootobject rootobject = JsonSerializer.Deserialize<Rootobject>(json);

            var properties = typeof(UsdcData).GetProperties();

            foreach (var prop in properties)
            {
               
                var value = prop.GetValue(rootobject.usd);

                if (value != null)
                {
                    string kodi = prop.Name.Replace("_", "");
                    float qiymat = (float)value;

                    kursModelsList.Add(new KursModel { Kodi = kodi.ToUpper(), Qiymat = qiymat });
                }
            }
            return kursModelsList;
        }
    }
}
