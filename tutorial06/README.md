W trakcie niniejszych ćwiczeń do wykonania jest prosta aplikacja REST API, która umożliwia wykonanie operacji
pozwalających na modyfikowanie danych w bazie SQL Server. Razem z zadaniem załączony jest skrypt pozwalający na
stworzenie tabelki Animals i wypełnienie jej danymi. Komunikacja z bazą danych powinna odbywać się poprzez klasy
SqlConnection/SqlCommand.

[Animal table](https://www.dropbox.com/scl/fi/7zlgcloxelv8vjz3060kw/animal_table.png?rlkey=2c73izm5ijydlwd6fdju3lakc&dl=0)

Dane serwera: db-mssql16.pjwstk.edu.pl

1. Dodaj kontroler Animals.
2. Dodaj metodę/endpoint pozwalającą na uzyskanie listy zwierząt.
   Końcówka powinna reagować na żądanie typu HTTP GET wysłane na
   adres /api/animals.
    - Końcówka powinna pozwolić na przyjęcie parametru w query string,
      który określa sortowanie. Parametr nazywa się orderBy. Przykład:
      api/animals?orderBy=name
    - Parametr jako dostępne wartości przyjmuje: name, description,
      category, area. Możemy sortować wyłącznie po jednej kolumnie.
      Sortowanie jest zawsze w kierunku „ascending”.
    - Domyślne sortowanie (kiedy w żądaniu nie zostanie przekazany
      parametr w query string) powinna odbywać się po kolumnie name.
3. Dodaj metodę/endpoint pozwalający na dodanie nowego zwierzęcia.
    - Metoda powinna odpowiadać na żądanie HTTP POST na adres
      api/animals.
    - Metoda powinna przyjmować dane w postaci JSON2.
4. Dodaj metodę/endpoint pozwalający na aktualizację danych konkretnego
   zwierzęcia.
    - Metoda powinna odpowiadać na żądanie HTTP PUT wysłane na
      adres /api/animals/{idAnimal}.
    - Metoda przyjmuje dane w postaci JSON’a
    - Zakładamy, że klucze główne nie ulegają modyfikacji (kolumna
      IdAnimal).
5. Dodaj metodę/endpoint do usuwania danych na temat konkretnego
   zwierzęcia.
    - Metoda powinna odpowiadać na żądanie HTTP DELETE wysłane
      na adres /api/animals/{idAnimal}.
6. Pamiętaj o poprawnych kodach HTTP.
7. Postaraj się skorzystać z wbudowanego mechanizmu do
   DependencyInjection.
8. Dbaj o walidację danych.
9. Dbaj o nazewnictwo i styl.