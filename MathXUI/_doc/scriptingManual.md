# Variables
	* [variableName]=[expression]
## DataTypes
	* Double
	* Boolean
	* Tensor

# Expressions
## Aritmetic operations
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

# Conditions
	* if [expression]
		'body'
	  endif
	* [expression] can be Boolean or Double

# Loops
	* while [expression]
		'body'
	  endwhile
	* [expression] can be Boolean or Double

# Functions
## Inbuilt functions
	* (i)ncrement([expression])
	* (pow)er([expressionBase],[expressionExponent])
	* sin([expression])
	* print([expression])
## User defined functions
### Inline
	* [functionName]([param1],...)=[expression]

