### Felrapport

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
Problem:
När jag skrev text istället för ett nummer i menyn kraschade programmet.
Felmeddelandet visade - FSystem.FormatException: The input string was not in a correct format.

## Orsak:
Programmet använder int.Parse() för att läsa menyvalet.
Om användaren skriver text går det inte att göra om till ett tal, och då kraschar programmet.

## Lösning:
Jag bytte till int.TryParse() så programmet först kontrollerar om inmatningen är ett tal.
Om det inte är ett tal visas ett felmeddelande istället för att programmet kraschar.