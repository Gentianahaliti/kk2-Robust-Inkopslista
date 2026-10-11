# Felrapport

## Fel 1

### Problem:  
När jag körde dotnet run kraschade programmet direkt.
Felmeddelandet visade 
at ShoppingList.Load() in C:\Users\Administrator\OneDrive\Documents\kk2-startkod-main\ShoppingList.cs:line 90
at Program.<Main>$(String[] args) in C:\Users\Administrator\OneDrive\Documents\kk2-startkod-main\Program.cs:line 2

### Orsak:  
En rad i items.txt saknade namn eller pris.
Programmet gör Split(';') och förväntar sig två delar.
När raden inte har två delar finns inte parts[1], och då kraschar programmet.

### Lösning:  
Jag la till enkla kontroller i Load() så felaktiga rader hoppas över.
Jag kontrollerar:
om raden är tom, om raden saknar semikolon, om priset inte går att läsa som ett tal.

## Fel 2

### Problem:
När jag skrev text istället för ett nummer i menyn kraschade programmet.
Felmeddelandet visade - FSystem.FormatException: The input string was not in a correct format.

### Orsak:
Programmet använder int.Parse() för att läsa menyvalet.
Om användaren skriver text går det inte att göra om till ett tal, och då kraschar programmet.

### Lösning:
Jag bytte till int.TryParse() så programmet först kontrollerar om inmatningen är ett tal.
Om det inte är ett tal visas ett felmeddelande istället för att programmet kraschar.

## Fel 3

### Problem:
När jag skrev text istället för ett nummer i priset kraschade programmet.
Felmeddelandet visade - System.FormatException: The input string was not in a correct format.

### Orsak:
Programmet använder int.Parse() för att läsa priset.
Om användaren skriver text går det inte att göra om till ett tal, och då kraschar programmet.

### Lösning:
Jag bytte till int.TryParse() så programmet först kontrollerar om priset är ett tal.
Om det inte är ett tal visas ett felmeddelande istället för att programmet kraschar.

## Fel 4

### Problem:
När jag skrev text istället för ett nummer vid “Ta bort vara” kraschade programmet.
Felmeddelandet visade - System.FormatException: The input string was not in a correct format.

### Orsak:
Programmet använder int.Parse() för att läsa numret som ska tas bort.
Om användaren skriver text går det inte att göra om till ett tal, och då kraschar programmet.

c Lösning:
Jag bytte till int.TryParse() så programmet först kontrollerar om inmatningen är ett tal.
Om det inte är ett tal visas ett felmeddelande istället för att programmet kraschar.

## Fel 5

### Problem:
När jag försökte ta bort ett nummer som inte fanns i listan (t.ex. 1 när listan var tom, eller 5 när listan bara hade en vara) kraschade programmet.
Felmeddelandet visade - System.ArgumentOutOfRangeException: Index was out of range.
Programmet avslutades direkt och gick inte tillbaka till menyn.

### Orsak:
Programmet använder RemoveAt(number) utan att kontrollera om numret finns i listan.
Om användaren skriver ett nummer som är mindre än 1 eller större än antalet varor, försöker programmet ta bort ett index som inte finns.
Då kastas ArgumentOutOfRangeException.

### Lösning:
Jag la till en kontroll som gör att numret måste vara i listan innan RemoveAt körs.
Om numret inte finns med visas ett felmeddelande istället för att programmet kraschar.
Jag ändrade också RemoveAt(number) till RemoveAt(number - 1) så att användaren kan skriva 1 för första varan.

## Fel 6

### Problem:
När jag lade till en vara visades den korrekt i listan, men totalsumman blev ändå 0 kr, även om varan hade ett pris.
Exempel -glass - 50 kr Totalt: 0 kr
Programmet räknade alltså inte ihop priserna och visade alltid 0 kr på Total.

### Orsak:
I metoden Total() börjar loopen på i = 1 i stället för 0:
Listor i C# börjar alltid på index 0, så den första varan räknas aldrig med.
Om listan bara har en vara körs loopen inte alls, vilket gör att summan alltid blir 0.

### Lösning:
Jag ändrade loopen så att den börjar på i = 0 i stället för 1.
Då räknas alla varor med i totalsumman:
Efter ändringen visas rätt totalsumma Exempel: glass - 50 kr Totalt: 50 kr.

## Fel 7

### Problem:
När jag skrev in en vara som gjorde att totalsumman blev högre än budgeten som var satt på 500kr lades varan ändå till i listan.

### Orsak:
Det fanns inget som kontrollerade om totalen + priset blev mer än budgettaket.
Programmet räknade inte alls med budgetgränsen, så alla varor accepterades även om man gick över 500 kr.

### Lösning:
Jag la till en kontroll som jämför totalen + priset med budgeten.
Om summan blir högre än 500 kr visas ett felmeddelande och varan läggs inte till.
Efter ändringen fungerar budgettaket som det ska.

## Designval – budgettak
Jag valde att hantera budgettaket direkt i Program.cs i stället för att kasta ett undantag i Add-metoden.
Anledningen är att Program.cs redan kontrollerar inmatning och visar felmeddelanden för användaren.
Det är därför enklare att stoppa varan där och låta programmet fortsätta köra utan att krascha.
Det gör också att användaren får ett tydligt meddelande och kan fortsätta använda programmet direkt.