WeaponManager wm = new WeaponManager();
wm.Run();

class WeaponManager {
	List<Weapon> _weapons = new List<Weapon>();

	public WeaponManager() {
		
	}

	public void Run() {
		bool run = true;
		int number = 0;
		Console.WriteLine("Welcome to the Weapon Manager Program!");
		while (run) {
			PrintMenu();
			do {
				Console.Write("\nPick from the menu options: ");
			} while (!int.TryParse(Console.ReadLine(), out number));

			if (number == 5) run = false;
			else if (number == 1) {
				PrintWeaponList();
			} else if (number == 2) {
				AddWeapon();
			} else if (number == 3) {
				SaveWeaponList();
			} else if (number == 4) {
				_weapons = LoadWeaponList();
			}
		}
	}

	private void PrintMenu() {
		Console.WriteLine("\n1 - print weapon list");
		Console.WriteLine("2 - add weapon");
		Console.WriteLine("3 - save weapon list");
		Console.WriteLine("4 - load weapon list");
		Console.WriteLine("5 - exit program");
	}

	private void PrintWeaponList() {
		foreach(Weapon weapon in _weapons) {
			Console.WriteLine(weapon);
		}
	}

	private void AddWeapon() {
		string? name;
		do {
			Console.Write("Enter weapon name: ");
			name = Console.ReadLine();
		} while (string.IsNullOrEmpty(name));

		int damage;
		do {
			Console.Write("Enter damage: ");
		} while (!int.TryParse(Console.ReadLine(), out damage));

		double weight;
		do {
			Console.Write("Enter weight: ");
		} while(!double.TryParse(Console.ReadLine(), out weight));

		_weapons.Add(new Weapon(name, damage, weight));
	}

	private void SaveWeaponList() {
		List<string> csvWeaponStrings = new List<string>();

		foreach(Weapon weapon in _weapons) {
			csvWeaponStrings.Add($"{weapon.Name}, {weapon.Damage}, {weapon.Weight}");
		}

		File.WriteAllLines("Weapons.csv", csvWeaponStrings);
	}

	private List<Weapon> LoadWeaponList() {
		string[] weaponStringArr = File.ReadAllLines("Weapons.csv");

		List<Weapon> weapons = new List<Weapon>();

		foreach (string weaponString in weaponStringArr) {
			string[] token = weaponString.Split(",");
			weapons.Add(new Weapon(
				token[0], 
				Convert.ToInt32(token[1]), 
				Convert.ToDouble(token[2])));
		}

		return weapons;
	}
}

public record Weapon (string Name, int Damage, double Weight);