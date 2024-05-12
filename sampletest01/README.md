# Przykładowe kolokwium

W niniejszym zadaniu musisz zaprojektować aplikację web API, która będzie obsługiwać bazę danych
zaprezentowaną na diagramie. Tworzymy serwis, który powinien pozwolić na przeglądanie danych na
temat recept, lekarzy i pacjentów. Korzystamy z SqlConnection/SqlCommand.

<p align="center">
  <img src="/images/diagram.png"/>
</p>

1. Zaprojektuj końcówkę, która będzie zwracać dane na temat wszystkich recept. Jako opcjonalny
   parametr chcemy przyjmować nazwisko lekarza. W przypadku kiedy wartość nie zostanie podana,
   chcemy zwrócić wszystkie wpisy na temat recept. Dane powinny być domyślnie zwracane od
   ostatnio wydanej recepty (malejąco). Poniżej zaprezentowany jest przykład żądania na jakie
   powinna odpowiadać końcówka. Zwracamy dane w postaci: „IdPrescription, Date, DueDate,
   PatientLastName, DoctorLastName”.

```
1. GET /api/prescriptions HTTP/1.1
2. Host: localhost:5000
```

2. Zaprojektuj końcówkę, która ma służyć wstawianiu nowych danych na temat recepty. Pamiętaj o
   zwracaniu poprawnych błędów. Upewnij się, że data „DueDate” jest starsza niż „Date”. Poniżej
   przykład żądania. Przesyłany format daty może być dowolny. W zadaniu zwracamy obiekt
   Prescription wraz z nowo wygenerowanym kluczem głównym.

```
1. POST /api/prescriptions HTTP/1.1
2. Host: localhost:5000
3. Content-Type: application/json
4.
5. {
6.    "Date": "4/22/2020",
7.    "DueDate": "4/22/2020",
8.    "IdPatient": 1,
9.    "IdDoctor": 2
10. }
```

3. Pamiętaj o:
    - poprawnym nazywaniu zmiennych, kontrolera itp.
    - utrzymaniu odpowiedniej struktury kodu
    - pamiętaj o wstrzykiwaniu zależności
    - pamiętaj o obsłudze błędów

4. Kod umieść w nowym repozytorium. Pamiętaj o pliku .gitignore i push’owaniu zmian na
   serwer.