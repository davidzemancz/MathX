﻿﻿﻿# MathX

## Úvodem

MathX je prográmek, který poskytuje API pro interpretaci vlastního "programovacího/skriptovacího" jazyka pro práci s výrazy a funkcemi. V nich mohou figurovat proměnné typu číslo či vektor (do budoucna i matice). Příklady členěné do sekcí s komentáři naleznete projektu MathXExamples. Pro "koncového" uživatele je pak projekt MathXUI, který je nad API postaven a zastřešuje správu procesů, zpracování příkazů ze souboru či kreslení jednoduchých grafů (alfa verze).

## Uživatelské rozhraní MathXUI

* Spouští se ze .sln MathX ve Visual studiu
* Slouží pro koncového uživatele, který si chce vyzkoušet možnosti knihovny MathX
* Je vybaveno několika funkčními okny a příklady
* Práci s procesy zajišťuje třída ProcessManager, jejímž jediným atributem je slovník procesů, kde klíčem je Id procesu

### Hlavní okno

* V sekci **Processes** je zobrazen seznam procesů, jejich proměnné a definované funkce
* V sekci **Shortcuts** jsou tlačítka pro rychlé spuštění (... víc funkcí vlastně program stejně nenabízí :)
  * Tlačítkem Save lze uložit aktuální stav programu do .json souboru, tlačítkem Open jej pak lze otevřít
  * Tlačítkem NewScript lze v jednoduchém textovém editoru vytvářet a upravovat kód
  * Tlačítko **Graph** otevírá okno pro zobrazování grafů
  * Tlačítko **Console** otevírá okno s jednoduchou konzolí
* V sekci **Examples** pak najdete jednoduché příklady

#### Životní cyklus hlavního okna

* Načtení (Form.Load)
  * Vytvoří se výchozí Process s Id=1 a vloží se do slovníku procesů ve statické třídě ProcessManager
* Aktivace (Form.Activate)
  * Ze třídy ProcessManager se načtou procesy a jejich proměnné a funkce do ListBoxu
  * Slovníkům Functions a Variables všech procesů se přiřadí událost UnsavedChanges - když přidám novou proměnnou či funkci, program mě před zavřením vyzve k uložení
* Zavírání (Form.Closing)
  * Před zavřením se program dotáže na uložení stavu, došlo-li ke změnám

### Editor skriptů

* V horní části je combobox pro výběru procesu -> s jeho proměnnými a funkcemi bude skript pracovat
* Umožňuje zapisování scriptů s jednoduchým zvýzrazěním syntaxe
* Skript lze spustit klávesou F5 nebo v Actions -> Run
* V dolní čási je sekce Output, do níž je vypysován výstup skriptu

### Konzole

* V horní části je combobox pro výběr procesu s jehož proměnnými a funkcemi bude konzole pracovat
* Jednoduché konzolové prostředí s výčtem zadaných příkazů a políčkem pro vstup
* Možnost vyhodnocování výrazů, definování proměnných či funkcí
* Nemá smysl používat příkazy s klíčovými slovy jako if nebo while

## Interpretovaný jazyk MathX

* Definice proměnné: [variableName]=[expression]

    * Definice čísla: [variableName]=205

    * Definice vektoru: [variableName]=[1,2,5,7]

* Rozpoznání typu probíhá automaticky, podporované typy jsou **Boolean**, **Double** a **Vector**

* Aritmetické operace s čísly

    * Sčítání: [expression1]+[expresson2]
    * Odčítání: [expression1]-[expresson2]
    * Násobení: [expression1]*[expresson2]
    * Dělení: [expression1]/[expresson2]
    * Umocňování: [expression1]^[expresson2]

* Operace s vektory

  * Sčítání: [expression1]+[expresson2]
  * Odčítání: [expression1]-[expresson2]
  * Skalární součin: [expression1]*[expresson2]

* Podmínka: 

  if [expression]

  ...

  else

  ...
  endif

  * Výraz v podmínce může být typu Boolean, nebo Double (True ~ >0)

* Cyklus:

  while  [expression]

  ...
  endwhile

  * Výraz v podmínce opět může být typu Boolean, nebo Double (True ~ >0)

* Logické operátory
  * & - AND
  * | - OR
  
* Definice inline funkce: [functionName([paramName1],...)]=[expression]

* Volání funkce: functionName(paramValue1,...)

#### Výchozí funkce

* (i)ncrement([expression])
* (pow)er([expressionBase],[expressionExponent])
* sin([expression])
* sqrt([expression])
* print([expression])

## Struktura programu

* Celý program je členěn na tyto čási
  * BaseApi - obecné třídy jako BaseStatus (reprezentace stavu) či BaseDictionary (slovní s eventy na přidání či odebrání)
  * BaseApiUI - obecné třídy uživatelské rozhraní
    * BaseForm - parent všech formulářů
    * BaseTextBoxForm - formulář s textboxem
    * BaseCodeEditor - RichTextBox se zvýrazněním syntaxe
  * MathX - samotná knihovna, obsahuje namespaces Primitives (tj. Expression, Statement ...), Processes (tj. Process), Utils (pomocné třídy)
  * MathXExamples - příklady pro potenciálního uživatele
  * MathXUI - WinForms uživatelské rozhraní

## Třídy a struktury

####  MathX.Processes.Process : class, IDisposable

* Process je nosná datová struktura, pod kterou jsou uložené proměnné a funkce. Také poskytuje MemoryStream pro načítání vstupu a čtení výstupu
* Má Id, State a slovník proměnných (**Variable**) a funkcí (**Function**)
  * State je výčtový typ hodnot Pending, Running, Stopped
    * Výchozím stavem je Pending, během volání funkce Run() je ve stavu Running

* Pro práci se vstupem a výstupem procesu, tj. zpracovávání příkazů a jejích výsledky, slouží MemoryStream Input a Output
  * Viz MathXExample
* Zpracování příkazů zapsaných do MemoryStream Input proběhne zavoláním metody Run()

#### MathX.Primitives.Variable : struct, IVariableValue

* Struktura reprezentující proměnnou 
* Má vlastnosti Name, Value, DataType, Temporary
  * DataType je výčtový typ hodnot Double, Boolean, Vector
  * Temporary je True právě když Name začíná podtržítkem
* Má definované operátory pro sčítání, odčítání, násobení, dělení, porovnání
* Lze ji implicitně přetypovat na Vector nebo Double
* Metoda ToString() vrací text ve formátu $"{Name} = {Value}", vyjma pokud je Temporary, pak vrací jen $"{Value}"
* Hodnotou Variable může být libovolný objekt, tedy i opět Variable

#### MathX.Primitives.Function : class

* Třída reprezentující funkci, umožňuje její definici, ověření parametrů a volání
* Má vlastnosti Name, Expression (tj. v podstatě návratová hodnota) a ParametersNames
* Metoda ToString() vrací text ve formátu $"{Name}({ParametersNames}) = {Expression}"
* K volání funkce slouží metoda Call(), kam eventuálně předám parametry, pokud jsem tak neučinil v konstruktoru

#### MathX.Primitives.Expression : class

* Třída pro práci a vyhodnocování výrazů

#### MathX.Primitives.Statement: class

* Třída pro práci a vyhodnocování příkazů (výraz je příkaz, který vypíše svou vlastní hodnotu)

#### MathX.Datatypes.Vector : class, IVariableValue

* Třída pro reprezentaci datového typu vektor
* Může posloužit zároveň jako pole (viz příklad BubbleSort)

## Možná rozšíření

* Doplnit klíčové slovo elseIf - k tomu by bylo nutno překopat třídu Statement
* Umožnit komentování kódu
* Umožnit nějak načítání dat do Programu ... jeden input pro kód, druhý pro data
* Umožnit paralelizaci procesů (v .net by neměl být problém)
* Rozšíření práce s vektory (porovnávání, kolmost ...)
* Datový typ Matrix a API pro práci s maticemi

## Závěr

Program se hodí pro zpracování výrazů či pro tvorbu skriptů na zpracování dat. Uživatelské rozhraní slouží spíše jako ukázka možností, nežli prakticky použitelný program. 













​	

