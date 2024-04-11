# Aplikacja do zarządzania załadunkiem kontenerów

Aplikacja została stworzona w celu zarządzania procesem załadunku kontenerów na kontenerowiec oraz ich transportu.
Kontenery mogą być transportowane różnymi rodzajami pojazdów, takimi jak statki, pociągi, ciężarówki itp. Projektowany
system będzie się skupiał na załadunku kontenerów na kontenerowiec, czyli statek wyposażony w specjalne prowadnice
umożliwiające przewóz kontenerów.

## Wymagania dotyczące kontenerów

### Wspólne cechy kontenerów

- Masa ładunku (w kilogramach)
- Wysokość (w centymetrach)
- Waga własna (waga samego kontenera, w kilogramach)
- Głębokość (w centymetrach)
- Numer seryjny (format numeru: KON-C-1)
- Maksymalna ładowność danego kontenera w kilogramach

### Operacje na kontenerach

- Opróżnienie ładunku
- Załadowanie kontenera daną masą ładunku
- Wyjątek OverfillException w przypadku przekroczenia maksymalnej ładowności

### Kontenery na płyny (L)

- Implementacja interfejsu IHazardNotifier
- Wypełnienie do 50% pojemności dla niebezpiecznego ładunku
- Wypełnienie do 90% pojemności dla zwykłego ładunku
- Wyjątek przy naruszeniu reguł

### Kontenery na gaz (G)

- Implementacja interfejsu IHazardNotifier
- Pozostawienie 5% ładunku w kontenerze podczas opróżniania
- Wyjątek przy przekroczeniu maksymalnej ładowności

### Kontener chłodniczy (C)

- Rodzaj produktu i temperatura przechowywania
- Przechowywanie wyłącznie produktów tego samego typu
- Temperatura kontenera nie niższa niż wymagana przez produkt

## Kontenerowiec

- Lista kontenerów transportowanych przez statek
- Maksymalna prędkość statku (w węzłach)
- Maksymalna liczba kontenerów do przewiezienia
- Maksymalna waga wszystkich kontenerów transportowanych przez statek (w tonach)

## Obsługiwane operacje

- Tworzenie kontenera danego typu
- Załadowanie ładunku do kontenera
- Załadowanie kontenera na statek
- Załadowanie listy kontenerów na statek
- Usunięcie kontenera ze statku
- Rozładowanie kontenera
- Zastąpienie kontenera na statku o danym numerze innym kontenerem
- Przeniesienie kontenera między dwoma statkami
- Wypisanie informacji o kontenerze
- Wypisanie informacji o statku i jego ładunku

### Symulacja działania aplikacji

Zadanie dla chętnych. Spróbuj przygotować interfejs konsolowy, który pozwoli na realizację wszystkich funkcji. Przykład
działania interfejsu został pokazany poniżej.

```
Lista kontenerowców:
Brak
Lista kontenerów:
Brak
Możliwe akcje:

Dodaj kontenerowiec
```

Użytkownik wybiera 1. System prosi po kolei o podanie wszystkich niezbędnych danych. Po zakończeniu, system wyświetla
ponownie ekran główny.

```
Lista kontenerowców:
Statek 1 (speed=10, maxContainerNum=100, maxWeight=40000)
Lista kontenerów:
Brak
Możliwe akcje:

Dodaj kontenerowiec
Usuń kontenerowiec
Dodaj kontener
```

Po dodaniu kontenera, pojawi się on na liście kontenerów. Następnie użytkownik ma możliwość umieszczenia kontenera na
statku, usunięcia danego kontenera...
