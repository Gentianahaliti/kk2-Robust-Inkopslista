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
