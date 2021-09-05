# Issues
* Nefunguji vyrazu jako (u+v)[1], kde u,v jsou vektory ... ale neni to chyba, spis vlastnost
* Pri Run() processu, kdyz jsem v podmince resp. cyklu, ktery je FALSE, stejne vyhodnocuji podminky, resp. cykly, coz muze delat brajgl

# Solved issues
* Priorita operatoru
	* Napr. vyraz 2*4-2*6-5 se vyhodnoti jako (2*4)-((2*6)-5), coz ale neodpovida (2*4)-(2*6)-5
	* Je to dusledek sekvencniho vyhodnocovani -> nutno s tim neco vymyslet

# TODO
* Desetinna cisla
* Klavesove zkratky
* Pripravit priklady programu
* Podminka ElseIf
* Dodelat funkce pro logaritmy, taky konstanty jako pi, e
* Ruzne typy Exception, jako UndefinedVariableException, NullVariableException atp...
