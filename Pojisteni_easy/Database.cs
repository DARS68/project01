using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni_easy
{
	public class Database
	{
		public List<Zaznam> SeznamPojistenych { get; private set; }

		//inizializace konstruktoru
		//naplnění demo daty při prvním spuštění, je zakomentováno
		//používá se načtení/uložení souboru databasePojistenych.csv
		public Database()
		{
			SeznamPojistenych = new List<Zaznam>();
			//{
			//	new("David", "Král", 606543210, 26),
			//	new("Petra", "Malinovskaya", 770123456, 45),
			//	new("Marek", "Holubec", 773456789, 34),
			//	new("Eliška", "Konečná", 725678901, 40),
			//	new("Adam", "Havel", 609012345, 25),
			//	new("Veronika", "Fialová", 771234567, 43),
			//	new("Filip", "Mareš", 703456789, 24),
			//	new("Tereza", "Kučerová", 728901234, 37),
			//	new("Ondřej", "Štěpán", 774321987, 35),
			//	new("Michaela", "Vondráčková", 727890123, 47),
			//};
		}

		//vrátí seznam všech pojištěných, uložených v databázi
		public List<Zaznam> ZaznamyKVypsani()
		{
			return SeznamPojistenych;
		}

		//vyhledá pojištěného (musí existovat jména a příjmení současně)
		public List<Zaznam> HledejZaznam(string Jmeno, string Prijmeni)
		{
			// Pro každý záznam aplikuje lambda funkci v metodě Where
			return SeznamPojistenych.Where(record => record.Jmeno.Contains(Jmeno) && record.Prijmeni.Contains(Prijmeni)).ToList();
		}

		public void Uloz()
		{
			//otevření souboru pro zápis
			using (StreamWriter strWr = new StreamWriter(@"databasePojistenych.csv"))
			{
				//projetí záznamů pojištěných
				foreach (Zaznam record in SeznamPojistenych)
				{
					string radek = record.DoRadkuCsv();
					strWr.WriteLine(radek);
				}
				// vyprázdnění bufferu
				strWr.Flush();
			}
		}

		public void Nacti()
		{
			SeznamPojistenych.Clear();

			// Otevře soubor pro čtení
			using (StreamReader strRe = new StreamReader(@"databasePojistenych.csv"))
			{
				string nacteno;
				// čte řádek po řádku
				while ((nacteno = strRe.ReadLine()) != null)
				{
					Zaznam zaznam = Zaznam.VytvorZaznamZTextu(nacteno);

					// přidá uživatele s danými hodnotami přes třídu Database
					SeznamPojistenych.Add(zaznam);
				}
			}
		}
	}
}

