using Base.Api;
using MathX.Primitives;
using MathX.Primitives.Utils;
using MathX.Processes;
using System;
using System.IO;

namespace MathXExample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Procesy

            /*
             * Zakladni nosnou jednotkou je proces
             *  - Pod procesem jsou ulozene promene
             *  - Take jsou pod nim definovane funkce
             *  - Zprostredkovava vstupni a vystupni stream
             */
            Process process = new Process("1");

            #endregion

            Console.WriteLine("Pro pokračování do sekce Výrazy stiskněte klávesu");
            Console.ReadKey();

            #region Vyrazy

            Console.WriteLine("Zadej první výraz");
            string strExpr1 = Console.ReadLine();

            /*
             * Vyraz je posloupnost znaku, ktera se vyhodnoti a nabude tim hodnoty
             *  - Vyhodnoceni vyrazu se vola metodou Evaluate(out BaseStatus status), ktera vraci vysledek a stav
             *  - Vysledek vyrazu je v objektu typu Variable, ktery si nese datovy typ, jmeno a hodnotu
             */
            Expression expression1 = new Expression(process, strExpr1);
            Variable result1 = expression1.Evaluate(out BaseStatus status);
            status.ThrowIfError();

            Console.WriteLine("Zadej druhý výraz");
            string strExpr2 = Console.ReadLine();

            Expression expression2 = new Expression(process, strExpr2);
            Variable result2 = expression2.Evaluate(out status);
            status.ThrowIfError();

            /*
             * S promennymi lze provadet bezne aritmeticke operace a porovnani
             */
            Console.WriteLine($"Součet výrazů: {result1 + result2}");
            Console.WriteLine($"Je první větší rovno než druhý: {result1 >= result2}");

            #endregion

            Console.WriteLine("Pro pokračování do sekce Příkazy stiskněte klávesu");
            Console.ReadKey();

            #region Prikazy

            Console.WriteLine("Zadej příkaz");
            string strStat1 = Console.ReadLine();

            /*
             * Prikaz je posloupnost znaku, ktera se vykona, ale nenabyva hodnoty (tedy napr. prirazeni, definice funkce, klicova slova)
             */
            Statement statement1 = new Statement(process, strStat1);
            
            /*
             * Informace o prikazu rika, zda jde o zacatek ci konec cyklu, podminky... a vyhodnoti je
             */
            StatementInfo statement1Info = statement1.GetInfo(out status);
            status.ThrowIfError();

            statement1.Execute(statement1Info, out status);
            status.ThrowIfError();

            // Vystup najdu v process.Output streamu
            using (StreamReader outputReader =  new StreamReader(process.Output, null, true, -1, true))
            {
                Console.WriteLine(outputReader.ReadToEnd());
            }

            #endregion

            Console.WriteLine("Pro pokračování do sekce Funkce stiskněte klávesu");
            Console.ReadKey();

            #region Funkce

            Function function = new Function(process, "f", "x^2", new[] {"x"});
            Console.WriteLine(function.ToString());
            Variable result = function.Call(new []{ (Variable)5.0 }, out status);
            Console.WriteLine($"f(5)={result.Value}");
            status.ThrowIfError();

            #endregion

            Console.WriteLine("Pro pokračování do sekce Vstupy a výstupy stiskněte klávesu");
            Console.ReadKey();

            #region Vstupy a vystupy

            // Vstupni i vystupni stream lze clearnout (vytvori se nova instance)
            process.ClearInput();
            process.ClearOutput();

            /*
             * Vstupni stream procesu
             *  - Do nej lze zapisovat prikazy, ktere se nasledne vykonaji zavolanim funkce Run()
             *  - Zapis probiha obvykle pomoci tridy StreamWriter
             *  - POZOR, stream nesmim zavrit, tedy nutno nastavit parameter leaveOpen=true
            */
            MemoryStream inputStream = process.Input;
            using (StreamWriter streamWriter = new StreamWriter(inputStream, null, -1, true))
            {
                streamWriter.WriteLine("x=6*2");
                streamWriter.WriteLine("y=7-(4*3)");
                streamWriter.WriteLine("add(a,b)=a+b");
                streamWriter.WriteLine("z=add(x,y)");
            }
            // Musim na zacatek streamu
            inputStream.Seek(0, SeekOrigin.Begin); 

            // Vykonam prikazy
            process.Run(out status);
            status.ThrowIfError();

            /*
             * Vstupni stream procesu
             *  - Opet POZOR, nazavrit stream
            */
            MemoryStream outputStream = process.Output;
            using (StreamReader streamReader = new StreamReader(outputStream, null, true, -1, true))
            {
                string output = streamReader.ReadToEnd();
                Console.WriteLine(output);
            }

            // Clearnu vstupni a vystupni stream
            process.ClearInput();
            process.ClearOutput();

            #endregion

            Console.WriteLine("Pro ukončení programu stiskněte klávesu");
            Console.ReadKey();

            #region Dispose

            /*
             * S Dispose procesu se uzavrou vsechny vstupni a vystupni streamy (lze take vlozit do using)
             */
            process.Dispose();

            #endregion
        }
    }
}
