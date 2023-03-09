using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni_easy
{
	public class Komunikace : IDisposable
	{
		public DateTime DatumCas { get; private set; } = DateTime.Now;

		//české nastavení pro vypsání dne v týdnu a měsíce, např středa a březen
		private CultureInfo culture = new CultureInfo("cs-CZ");

		public Database database { get; private set; }

		public string Jmeno { get; private set; } = "";

		public string Prijmeni { get; private set; } = "";

		public Komunikace(Database database)
		{
			this.database = database;

			//načte pojištěné ze souboru databasePojistenych.csv
			database.Nacti();
			Console.WriteLine("Načteno ze souboru \"databasePojistenych.csv\"");
			PokracujLibovolnouKlavesou();
			Console.ReadKey();
		}

		//zajistí zadání údajů od uživatele, předá k zápisu do databáze třídě Database
		public void PridejPojisteneho()
		{
			int telefon;
			int vek;
			Console.WriteLine("\nZadejte jméno pojištěného:");
			while (string.IsNullOrWhiteSpace(Jmeno = Console.ReadLine()))
			{
				Console.WriteLine("Chybné zadání, zadejte jméno znovu");
			}

			Console.WriteLine("Zadejte příjmení pojištěného:");
			while (string.IsNullOrWhiteSpace(Prijmeni = Console.ReadLine()))
			{
				Console.WriteLine("Chybné zadání, zadejte příjmení znovu");
			}

			Console.WriteLine("Zadejte telefonní číslo:");
			while (!int.TryParse(Console.ReadLine(), out telefon))
			{
				Console.WriteLine("Chybné zadání, zadejte telefon znovu");
			}

			Console.WriteLine("Zadejte věk:");
			while (!int.TryParse(Console.ReadLine(), out vek))
			{
				Console.WriteLine("Chybné zadání, zadejte věk znovu");
			}

			database.SeznamPojistenych.Add(new Zaznam(Jmeno, Prijmeni, telefon, vek));
			Console.Write("\nData byla uložena.");
			PokracujLibovolnouKlavesou();
		}

		//vypíše seznam všech pojištěných, kompletní údaje,
		//vyžádá si z databáze prostřednictvím třídy Database
		public void VypisPojistene()
		{
			List<Zaznam> seznamPojistenych = database.ZaznamyKVypsani();
			Console.WriteLine("\nSeznam pojištěných:\n");
			Zaznam.Hlavicka();

			foreach (Zaznam rec in seznamPojistenych)
			{
				Console.WriteLine(rec);
			}
			PokracujLibovolnouKlavesou();
		}

		//zajistí zadání údajů od uživatele, předá k vyhlednání do databáze třídě Database
		public void VyhledejPojisteneho()
		{
			Console.WriteLine("\nZadejte jméno:");
			while (string.IsNullOrWhiteSpace(Jmeno = Console.ReadLine()))
			{
				Console.WriteLine("Chybné zadání, zadejte jméno znovu");
			}

			Console.WriteLine("Zadejte příjmení:");

			while (string.IsNullOrWhiteSpace(Prijmeni = Console.ReadLine()))
			{
				Console.WriteLine("Chybné zadání, zadejte příjmení znovu");
			}

			//testuje, zda byl nalezen hledaný záznam
			if (database.HledejZaznam(Jmeno, Prijmeni).Count == 0)
			{
				Console.WriteLine("\nZáznam nebyl nenalezen");
				PokracujLibovolnouKlavesou();
			}
			//vypíše hledané záznamy (jméno, příjmení, věk, telefon)
			else
			{
				Console.WriteLine("\nSeznam vyhledaných pojištěných:\n");
				Zaznam.Hlavicka();
				foreach (Zaznam record in database.HledejZaznam(Jmeno, Prijmeni))
				{
					Console.WriteLine(record);
				}
				PokracujLibovolnouKlavesou();
			}
		}

		//vypíše pokračování libovolnou klávesou
		public void PokracujLibovolnouKlavesou()
		{
			Console.WriteLine("\nPokračujte libovolnou klávesou...");
		}

		//vypíše úvodní obrazovku na novou stránku
		public void UvodniObrazovka()
		{
			Console.Clear();
			Console.WriteLine("===============================================");
			Console.WriteLine("               Evidence pojištěných            ");
			Console.WriteLine("===============================================");
			Console.WriteLine(" dnes je {0} - {1}", DatumCas.ToString("dddd", culture),
													DatumCas.ToString("d. MMMM yyyy", culture));
			Console.WriteLine();
		}

		// Volá se na konci using contextu
		public void Dispose()
		{
			//uloží pojištěné do souboru databasePojistenych.csv
			database.Uloz();
			//Console.WriteLine("\nUloženo do souboru \"databasePojistenych.csv\"");
		}
	}
}
