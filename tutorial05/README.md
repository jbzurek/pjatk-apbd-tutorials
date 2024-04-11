Stwórz aplikację REST API, która pozwoli nam zarządzać danymi zwierząt w bazie danych schroniska dla kliniki weterynaryjnej.

Zwierze opisywane jest przez:
- ID 
- imię,
- kategorię (np. pies, kot)
- masę
- kolor sierści

Chcielibyśmy mieć możliwość:

- pobierania listy zwierząt
- pobierania konkretnego zwierzęcia po ID
- dodawania zwierzęcia 
- edycji zwierzęcia 
- usuwania zwierzęcia

Ponadto chcielibyśmy zapisywać informacje na temat wizyt zwierzęcia:
- chcielibyśmy mieć możliwość pobrania listy wizyt powiązanych z danym zwierzęciem
- chcielibyśmy mieć możliwość dodawania nowych wizyt

Wizyta obejmuje następujące informacje:
- datę wizyty 
- zwierzę
- opis wizyty
- cenę wizyty

1. Przygotuj aplikację z REST API z odpowiednimi końcówkami HTTP - GET, POST, PUT, DELETE.
2. Upewnij się, że struktura końcówek jest zgodna z zasadami projektowania końcówek REST.
3. Jako bazę danych przygotuj statyczną kolekcję z obiektami.
4. Możesz wykorzystać zarówno podejście MinimalAPI lub skorzystać z wersji API wykorzystującej klasy kontrolerów.
5. Przetestuj przygotowaną aplikację za pomocą Postman'a