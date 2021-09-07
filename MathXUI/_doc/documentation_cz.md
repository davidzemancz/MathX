# MathX

## Třídy a struktury

####  MathX.Processes.Process : class, IDisposable

* Process je nosná datová struktura
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

## Interpretovaný jazyk MathX

- Definice proměnné: [variableName]=[expression]

- Rozpoznání typu probíhá automaticky, podporované typy jsou **Boolean**, **Double** a **Vector**

- Aritmetické operace s čísly

  - Sčítání: [expression1]+[expresson2]
  * Odčítání: [expression1]-[expresson2]
  - Násobení: [expression1]*[expresson2]
  - Dělení: [expression1]/[expresson2]
  - Umocňování: [expression1]^[expresson2]

- Operace s vektory

  - Sčítání: [expression1]+[expresson2]
  - Odčítání: [expression1]-[expresson2]
  - Skalární součin: [expression1]*[expresson2]

- Podmínka: 

  if [expression]

  ...

  else

  ...
  endif

  * Výraz v podmínce může být typu Boolean, nebo Double (True ~ >0)

- Cyklus:

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

# Uživatelské rozhraní MathXUI





​	

