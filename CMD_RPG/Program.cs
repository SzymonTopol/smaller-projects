using CMD_CLASS_RPG;
using System.Runtime.InteropServices;

int CURRENT_MAP = 0;

//Setup postaci i enemy na wszystkie mapy

List<Character> protagonists = new List<Character> { 
    new Warrior(mhealth:10, mmana:6, damage:2,damage_increase:1),
    new Paladin(mhealth:8, mmana:8, damage:1),
    new Mage(mhealth:6,mmana:10, damage:1,heal:3)};

List<List<Character>> level_mobs = new List<List<Character>>
{
    //1
    new List<Character>{
        new Character(mhealth:3,mmana:0, damage:1),
        new Character(mhealth:3,mmana:0, damage:1)
    },
    //2
    new List<Character>{
        new Character(mhealth:2,mmana:0, damage:2),
        new Character(mhealth:2,mmana:0, damage:2),
        new Character(mhealth:2,mmana:0, damage:2),
        new Character(mhealth:2,mmana:0, damage:2),
        new Character(mhealth:2,mmana:0, damage:2),
    },
    //3
    new List<Character>{
        new Character(mhealth:6,mmana:0, damage:2),
        new Character(mhealth:6,mmana:0, damage:2)
    },
    //4
    new List<Character>{
        new Character(mhealth:3,mmana:0, damage:1),
        new Character(mhealth:3,mmana:0, damage:1),
        new Character(mhealth:2,mmana:0, damage:2),
        new Character(mhealth:2,mmana:0, damage:2),
        new Character(mhealth:6,mmana:0, damage:2),
        new Character(mhealth:6,mmana:0, damage:2)
    },
    //5
    new List<Character> { new Character(mhealth : 20, mmana : 0, damage : 3) }
};

//Funkcja od pojedynczej bitwy (+pomocnicze)
bool battle(List<Character> protagonists, List<Character> mobs)
{
    Console.Clear();
    //Odświeżenie postaci
    for (int i = 0; i < protagonists.Count; i++) { protagonists[i].restore_max_stats(); }

    bool is_battle_ongoing = true;

    Random rnd = new Random();

    Console.WriteLine("Przeciwko wam stoi " + mobs.Count + " przeciwników");

    do
    {
        //GRACZ

        for (int i = 0; i < protagonists.Count; i++)
        {
            if (mobs.Count == 0) return true;
            bool did_action = true;

            do
            {
                write_mob_stats(mobs);
                Console.WriteLine("Obecna postać: " + protagonists[i].ToString());
                Console.WriteLine("HP: " + protagonists[i].HEALTH + " MANA: " + protagonists[i].MANA + " DAMAGE: " + protagonists[i].DAMAGE);
                Console.WriteLine("1 - atakuj, 2 - wykonaj ruch specjalny (2 many)");

                if(!int.TryParse(Console.ReadLine(), out int choice)){
                    Console.WriteLine("Niepoprawny wybór");
                    did_action = false;
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        did_action = battle_attack(protagonists[i], mobs);
                        break;
                    case 2:
                        did_action = battle_action(protagonists[i], protagonists);
                        break;
                    default:
                        did_action = false;
                        Console.WriteLine("Niepoprawny wybór");
                        break;
                }

            }
                while (!did_action);

            mobs.RemoveAll(mob => mob.HEALTH <= 0);
     
        }

        //AI

        Console.WriteLine();
        foreach (var mob in mobs)
        {
            int chosen_protagonist = rnd.Next(0, protagonists.Count);
            protagonists[chosen_protagonist].HEALTH -= mob.DAMAGE;
            Console.WriteLine("Mob attacked " + protagonists[chosen_protagonist].ToString() + " dealing " + mob.DAMAGE + " \n" + protagonists[chosen_protagonist].ToString() + " has " + protagonists[chosen_protagonist].HEALTH + " health left.");
            if (protagonists[chosen_protagonist].HEALTH <= 0)
            {
                protagonists.Remove(protagonists[chosen_protagonist]);
            }
            
        }
        Console.WriteLine();

        //=============== koniec tury ===============

        if (protagonists.Count == 0) return false;

    }
    while (is_battle_ongoing);

        return false;
}

bool battle_attack(Character protagonist, List<Character> mobs)
{
    Console.WriteLine("Szykujesz się do ataku");

    if(protagonist is Mage mage)
    {
        Console.Clear();

        mage.attack(mobs);

        return true;
    }

    Console.WriteLine("Wybierz cel 1 - " + mobs.Count);

    if (!int.TryParse(Console.ReadLine(), out int target)) return false;

    if(target < 1 || target > mobs.Count) return false;
    
    Console.Clear();

    protagonist.attack(mobs[--target]);

    return true;
}

bool battle_action(Character protagonist, List<Character> protagonists)
{
    object compare_val = protagonist;

    switch (compare_val)
    {
        case Warrior or Paladin:
            Console.WriteLine("Wybierz cel 1 - " + protagonists.Count);

            if (!int.TryParse(Console.ReadLine(), out int target)) return false;

            if(target < 1 || target >  protagonists.Count) return false;

            Console.Clear();
            protagonist.action(protagonists[--target]);

            return true;

        case Mage:
            Console.Clear();
            ((Mage)protagonist).action(protagonists);

             return true;
        default:
            return false;
    }
}

void write_mob_stats(List<Character> mobs)
{
    Console.WriteLine("==============");
    for (int i = 0; i < mobs.Count; i++)
    {
        Console.WriteLine(i + 1 + ". Mob \t" + mobs[i].HEALTH + " HP |\t" + mobs[i].DAMAGE + " DMG");
    }
    Console.WriteLine("==============");
}

//sprawdzanie czy kampania trwa

void battle_wrapper()
{
    bool is_battle_won = battle(protagonists, level_mobs[CURRENT_MAP]);

    if (!is_battle_won)
    {
        Console.WriteLine("Pomimo wielu wysiłków, nasza banda bohaterów została pokonana");
        Environment.Exit(1);
    }
    else
    {
        Console.WriteLine("Przeciwnicy pokonani!");
        CURRENT_MAP++;
    }
}

//Początek "Kampanii"

Console.WriteLine("\\DA GAME IN CMD/");

Console.WriteLine("To niesłychana historia odległej krainy.");
Console.WriteLine("Grupka bohaterów - wojownik, mag i paladyn udali się na niebezpieczną wyprawę, \naby wyzwolić osadę Psanów spod nękających ich potworów");
Console.WriteLine("Przed nimi pierwsza potyczka...");
Console.ReadKey();
battle_wrapper(); // Bitwa 1
Console.ReadKey();

Console.WriteLine("Po pierwszej udanej potyczce, przed bohaterami stanęło nie lada wyzwanie przejścia przez strome pasma górskie Vicekarno.\nNie wiedzieli jednak, że po męczącej wspinaczce, na szczycie czychała na nich silna przeciwniczka oraz jej armia simpów. ");

Console.ReadKey();
battle_wrapper(); // Bitwa 2
Console.ReadKey();

Console.WriteLine("Kolejne zwycięstwo umocniło ich w drodze na szczyt ich podróży - uratowanie Psanowa. \nNasi bohaterowie szli dalej - a raczej płynęli humorzastą rzeką Ludowką. \nJej nieobliczalnosc wynikala z niczego innego jak drapieżnych pekepów, wijących się bestii. \nTeraz również nie znały litości i owinęły się swymi potężnymi ogonami o burtę statku bohaterów. \nJak teraz poradzi sobie nasze trio? ");

Console.ReadKey();
battle_wrapper(); // Bitwa 3
Console.ReadKey();

Console.WriteLine("Po burzliwej przeprawie morskiej, mag, palladyn i wojownik docierają do krainy Talabonii, \ngdzie udało im się odpocząć i nabrać sił po poprzednich wymagających przeprawach. \nKiedy jednak próbowali opuścić miasto i ruszyć w dalszą podróż, na ich drodze stanął wściekły Benitrycz, \nwyrażając swoje niezadowolenie próbą przejścia przez jego most. ");

Console.ReadKey();
battle_wrapper(); // Bitwa 4
Console.ReadKey();

Console.WriteLine("Po udanej przeprawie zagubieni bohaterowie udali się zgodnie z kompasem na północny wschód. \nCoraz bliżej celu trafili na wędrowca, który okazał się mieszkańcem Psanów. \nWidząc strach w jego oczach, pośpiesznie kontynuowali trasę i trafili na cel swojej podróży. \nIch oczom ukazał się straszny widok, spustoszenie wioski. A na samym jej środku poczwara z piekła rodem. \nZ 4 głowami i 6 rękami rzuciła się na nich...");

Console.ReadKey();
battle_wrapper(); // Bitwa 5
Console.ReadKey();

Console.WriteLine("Żadna przeszkoda nie była straszna naszym bohaterom. \nMimo preturbacji na krętej ścieżce do Psanów, trio skutecznie pokonało potwory zagrażające wiosce \ni w ten sposób na zawsze zażegnało panujące w wiosce spustoszenie, ciągły niepokój i smutek.");

Console.ReadKey();

Console.WriteLine("THE END");