using Pojisteni_easy;

//třída Komunikace slouží k oddělení databáze od programu
Database database= new Database();

using (Komunikace komunikace = new Komunikace(database))
{
	char volba = '0';
	
	while (volba != '4')
	{
		//vypsání úvodní obrazovky
		komunikace.UvodniObrazovka();

		Console.WriteLine("Vyberte si akci:");
		Console.WriteLine("1 - Přidat nového pojištěného");
		Console.WriteLine("2 - Vypsat všechny pojištěné");
		Console.WriteLine("3 - Vyhledat pojistěného");
		Console.WriteLine("4 - Konec");
		volba = Console.ReadKey().KeyChar;
		Console.WriteLine();

		//volba akce s pojistěnými
		switch (volba)
		{
			case '1':
				komunikace.PridejPojisteneho();
				break;
			case '2':
				komunikace.VypisPojistene();
				break;
			case '3':
				komunikace.VyhledejPojisteneho();
				break;
			case '4':
				Console.WriteLine("\nLibovolnou klávesou ukončíte program ...");
				break;
			default:
				Console.WriteLine("\nNeplatná volba, stiskněte libovolnou klávesu a opakujte volbu");
				break;
		}
		Console.ReadKey();
	}
}