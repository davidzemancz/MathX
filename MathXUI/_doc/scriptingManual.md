# Variables
	* [variableName]=[expression]
## DataTypes
	* Double
	* Boolean
	* Vector

# Expressions
## Aritmetic operations with numbers
	* Adition:			[expression1]+[expresson2]
	* Substraction:		[expression1]-[expresson2]
	* Multiplication:	[expression1]*[expresson2]
	* Division:			[expression1]/[expresson2]
	* Power:			[expression1]^[expresson2]
## Priority
	* 1: adition, substraction
	* 2: multiplication, division
	* 3: power
	* 4: parentheses
## Aritmetic operations with vectors
	* Adition:			[expression1]+[expresson2]
	* Substraction:		[expression1]-[expresson2]
	* Dot product:		[expression1]*[expresson2]

# Conditions
	* if [expression]
		'body'
	  endif
	* [expression] can be Boolean or Double
	* Logical operators are &,| 

# Loops
	* while [expression]
		'body'
	  endwhile
	* [expression] can be Boolean or Double
	* Logical operators are &,| 

# Functions
## Inbuilt functions
	* (i)ncrement([expression])
	* (pow)er([expressionBase],[expressionExponent])
	* sin([expression])
	* print([expression])
## User defined functions
### Inline
	* [functionName]([paramName1],...)=[expression]
	* If paramName is same as variable name, during function call variable value is replaced by paramValue, then restored

