﻿using Base.Api;
using MathX.Primitives;
using MathX.Primitives.Utils;
using MathX.Processes;
using System;

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
            Variable result2 = expression1.Evaluate(out status);
            status.ThrowIfError();

            /*
             * S promennymi lze provadet bezne aritmeticke operace a porovnani
             */
            Console.WriteLine($"Součet výrazů: {result1 + result2}");
            Console.WriteLine($"Porovnání výrazů: {result1 >= result2}");

            #endregion

            Console.ReadKey();

            #region Prikazy

            Console.WriteLine("Zadej příkaz");
            string strStat1 = Console.ReadLine();

            /*
             * Prikaz je posloupnost znaku, ktera se vykona, ale nenabyva hodnoty (tedy napr. prirazeni, definice funkce, klicova slova)
             */
            Statement statement1 = new Statement(process, strStat1);
            
            /*
             * Informace o prikazu rika, zda jde o zacatek ci konec cyklu, podminky...
             */
            StatementInfo statement1Info = statement1.GetInfo(out status);
            status.ThrowIfError();

            statement1.Execute(statement1Info, out status);
            status.ThrowIfError();

            #endregion

            Console.ReadKey();

            #region Funkce

            #endregion

            Console.ReadKey();

            #region Vstupy a vystupy

            #endregion

            #region Dispose

            /*
             * S Dispose procesu se uzavrou vsechny vstupni a vystupni streamy (lze take vlozit do using)
             */
            process.Dispose();

            #endregion
        }
    }
}