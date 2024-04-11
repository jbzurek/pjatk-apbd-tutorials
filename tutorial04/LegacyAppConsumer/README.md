W niniejszym zadaniu musimy zrefaktoryzować kod istniejącej aplikacji. Po otwarciu solucji zobaczymy dwa projekty:

1. LegacyApp - to jest aplikacja, którą będziemy chcieli zrefaktoryzować. Znajdziemy w niej klasę UserService, która
   wymaga
   refaktoryzacji, szczególnie metoda AddUser.
2. LegacyAppConsumer - to jest przykład aplikacji, która wykorzystuje LegacyApp.
   Refaktoryzacja polega na tym, że nie zmieniamy działania istniejącej aplikacji. Zakładamy, że aplikacja działa
   poprawnie. Mamy zrefaktoryzować klasę UserService wraz z metodą AddUser.

Uwaga: W projekcie LegacyApp znajdziemy klasy, które symulują odpytywanie zewnętrznych źródeł danych poprzez użycie
Thread.Wait. Kluczową klasą jest UserDataAccess.

Pamiętajmy o następujących założeniach:
- Kod w aplikacji LegacyAppConsumer musi się cały czas kompilować i działać, również po procesie refaktoryzacji. Nie
  chcemy, żeby po naszej refaktoryzacji kod innych aplikacji nagle przestał działać.
- Nie możemy w żaden sposób modyfikować kodu z projektu LegacyAppConsumer.
- Kieruj się zasadami SOLID, testowalnością kodu i jego czytelnością.
- Staraj się zapanować nad strukturą programu, pamiętając o metrykach cohesion i coupling.
- Postaraj się wykorzystać w rozwiązaniu testy jednostkowe.