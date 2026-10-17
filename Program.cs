ShoppingList list = new ShoppingList("items.txt");
list.Load();

while (true)
{
    Console.WriteLine();
    list.Print();
    Console.WriteLine();
    Console.WriteLine("1. Lägg till vara");
    Console.WriteLine("2. Ta bort vara");
    Console.WriteLine("3. Spara");
    Console.WriteLine("4. Sök vara");
    Console.WriteLine("5. Avsluta");
    Console.Write("Välj: ");

string choiceInput = Console.ReadLine();
if (!int.TryParse(choiceInput, out int choice))
{
    Console.WriteLine("Fel: du måste skriva ett nummer.");
    continue;
}
    if (choice == 1)
    {
        Console.Write("Namn: ");
        string name = Console.ReadLine();
       
       Console.Write("Pris: ");
string priceInput = Console.ReadLine();

if (!int.TryParse(priceInput, out int price))
{
    Console.WriteLine("Fel: pris måste vara ett tal.");
    continue;
}      
try
{
    list.Add(new Item(name, price));
    Console.WriteLine("Varan har lagts till.");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Fel: {ex.Message}");
}
 catch (InvalidOperationException ex)
    {
        Console.WriteLine($"Fel: {ex.Message}");
    }
}
 else if (choice == 2)
{
    Console.Write("Nummer: ");
    string input = Console.ReadLine();

    if (!int.TryParse(input, out int number))
    {
        Console.WriteLine("Fel: du måste skriva ett nummer.");
        continue;
    }

    // NY DEL – detta är fixen för Fel 5
    if (number < 1 || number > list.Count)
    {
        Console.WriteLine("Fel: numret finns inte i listan.");
        continue;
    }
    list.RemoveAt(number);
}
    else if (choice == 3)
    {
        list.Save();
    }
    else if (choice == 4)
    {
        Console.Write("Namn att söka efter: ");
        string wanted = Console.ReadLine();
        Item found = list.Find(wanted);

        if (found == null)
        {
            Console.WriteLine("Varan finns inte i listan.");
        }
        else
        {
            Console.WriteLine($"Hittade: {found}");
        }
    }
    else if (choice == 5)
    {
        break;
    }
}