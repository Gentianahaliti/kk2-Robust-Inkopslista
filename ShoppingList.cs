// Holds the items and takes care of loading and saving them.
class ShoppingList
{
    private List<Item> items = new List<Item>();
    private string path;

    public ShoppingList(string path)
    {
        this.path = path;
    }

    
public void Add(Item item)
{
    if (Total() + item.Price > 500)
    {
        throw new InvalidOperationException(
            "Budgeten på 500 kr överskrids.");
    }

    items.Add(item);
}
 // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        items.RemoveAt(number - 1);
    }
public int Count
{
    get { return items.Count; }
}

    // Adds up the price of every item on the list.
    public int Total()
    {
        int sum = 0;

        for (int i = 0; i < items.Count; i++)
        {
            sum += items[i].Price;
        }

        return sum;
    }

    // Looks up an item by its name. Returns null if there is no such item.
    public Item Find(string name)
    {
        foreach (Item item in items)
        {
            if (item.Name == name)
            {
                return item;
            }
        }

        return null;
    }

    public void Print()
    {
        for (int i = 0; i < items.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {items[i]}");
        }

        Console.WriteLine($"Totalt: {Total()} kr");
    }

    // Writes one item per line, as "price;name".
    public void Save()
    {
        List<string> lines = new List<string>();

        foreach (Item item in items)
        {
            lines.Add($"{item.Price};{item.Name}");
        }

        try
        {
            File.WriteAllText(path, string.Join("\r\n", lines) + "\r\n");
        }
        catch
        {
        }

        Console.WriteLine("Listan är sparad.");
    }

    // Reads the file back into the list.
   public void Load()
{
    // Läser in alla rader från filen
    foreach (var line in File.ReadAllLines("items.txt"))
    {
        // Om raden är tom eller bara mellanslag, då struntar vi i den
        if (string.IsNullOrWhiteSpace(line))
            continue;

        // Delar upp raden vid semikolon, så vi får namn och pris
        var parts = line.Split(';');

        // Om det inte finns två delar (namn och pris), då är raden fel och vi hoppar över den
        if (parts.Length < 2)
            continue;

        // Försöker göra om priset till ett heltal
        int price;
        if (!int.TryParse(parts[1], out price))
            continue; // Om priset inte är ett tal, då hoppar vi över raden

        // Här vet vi att raden är okej, så vi lägger till varan i listan
        items.Add(new Item(parts[0], price));
    }
}
}