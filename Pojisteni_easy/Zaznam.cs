using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pojisteni_easy
{
	public class Zaznam
	{
		public string Jmeno { get; private set; }
		public string Prijmeni { get; private set; }
		public int Telefon { get; private set; }
		public int Vek { get; private set; }

		private Database database;

		//inicializace konstruktoru
		public Zaznam(string jmeno, string prijmeni, int telefon, int vek)
		{
			Jmeno = jmeno;
			Prijmeni = prijmeni;
			Telefon = telefon;
			Vek = vek;
		}

		//vypisuje údaje pojištěného ve správném odsazení, 15 nebo 5 znaků, místo tabulátoru 
		public override string ToString()
		{
			return string.Format("{0,-15} {1,-15} {2,-5} {3,-15}", Jmeno, Prijmeni, Vek, Telefon);
		}

		//naformátuje pro uložení do csv souboru - údaje jsou odděleny čárkou
		public string DoRadkuCsv()
		{
			return Jmeno + "," + Prijmeni + "," + Telefon.ToString() + "," + Vek.ToString();
		}

		//vypisuje hlavičku na stránku výpisů (seznam nebo vyhledávání)
		//ve správném odsazení, 15 nebo 5 znaků, místo tabulátoru 
		public static void Hlavicka()
		{
			Console.WriteLine("{0,-15} {1,-15} {2,-5} {3,-15}", "Jméno", "Příjmení", "Věk", "Telefon");
			Console.WriteLine("===============================================");
		}

		//metoda vytvoří z jednoho přečteného řádku z csv jednotlivé řetězce slov
		//a předá zpět k uložení do kolekce
		public static Zaznam VytvorZaznamZTextu(string nacteno)
		{
			// rozdělí string řádku podle čárek do pole stringů
			string[] rozdeleno = nacteno.Split(',');

			//přiřadí do řetězců prvek pole podle indexu
			string jmeno = rozdeleno[0];
			string prijmeni = rozdeleno[1];
			int telefon = int.Parse(rozdeleno[2]);
			int vek = int.Parse(rozdeleno[3]);

			return new Zaznam(jmeno, prijmeni, telefon, vek);
		}
	}
}


