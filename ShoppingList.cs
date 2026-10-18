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
            throw new InvalidOperationException("Budgeten på 500 kr överskrids.");
        }

        items.Add(item);
    }
    // Removes the item the user sees as number 1, 2, 3 ...
    public void RemoveAt(int number)
    {
        if (number < 1 || number > items.Count)
        {
            throw new ArgumentOutOfRangeException(
                nameof(number),
                "Numret finns inte i listan.");
        }

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
            if (string.Equals(item.Name, name, StringComparison.OrdinalIgnoreCase))
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
            Console.WriteLine("Listan är sparad.");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Kunde inte spara listan: {ex.Message}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Ingen behörighet att spara listan: {ex.Message}");
        }
    }
    public void Load()
    {
        // Om filen saknas finns det inget att läsa in.
        if (!File.Exists(path))
            return;

        foreach (string line in File.ReadAllLines(path))
        {
            // Hoppar över tomma rader.
            if (string.IsNullOrWhiteSpace(line))
                continue;

            // Delar raden i pris och namn vid semikolon.
            string[] parts = line.Split(';');

            // Raden måste innehålla både pris och namn.
            if (parts.Length != 2)
                continue;

            // Kontrollerar att priset går att läsa som ett heltal.
            if (!int.TryParse(parts[0], out int price))
                continue;

            // Hoppar över tomma namn och negativa priser.
            if (string.IsNullOrWhiteSpace(parts[1]) || price < 0)
                continue;

            // Lägger till varan med det inlästa namnet och priset.
            items.Add(new Item(parts[1], price));
        }
    }
}