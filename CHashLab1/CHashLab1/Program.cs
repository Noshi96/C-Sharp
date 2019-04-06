using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace CHashLab1
{
    class Html
    {
        string html, daneTabeli;

        public Html() { }

        public Html(int wiersze, int kolumny, int nr, string[] daneTablica)
        {
            if (wiersze != 0 && kolumny != 0)
            {
                string str = "";
                string dane = "";
                bool flaga = false;
                bool flaga2 = false;
                int iterator = 0;

                str = rozpocznijHtml(str);
                for (int i = 0; i < wiersze; i++)
                {
                    if (i == 0)
                    {
                        str = dodajNaglowek(str);
                        str = dodajWiersz(str);
                        flaga = true;
                    }
                    else if (i > 0 && flaga)
                    {
                        str = dodajCialo(str);
                        str = dodajWiersz(str);
                        flaga = false;
                    }
                    else if (i == wiersze - 1 && i >= 2)
                    {
                        str = dodajStopke(str);
                        str = dodajWiersz(str);
                    }
                    else
                    {
                        str = dodajWiersz(str);
                    }
                    for (int j = 0; j < kolumny; j++)
                    {
                        if (nr == 1)
                        {
                            dane = "" + i + j;
                        }
                        else if (nr == 5) // Test 6, dane pobierane z klawiatury
                        {
                            dane = wprowadzDane(i, j);
                        }
                        else if (daneTablica != null)
                        {
                            dane = daneTablica[iterator];
                        }
                        else
                        {
                            dane = "komorka " + i + j;
                        }
                        if (i == 0)
                        {
                            str = dodajPoleNaglowka(str, dane);
                        }
                        else
                        {
                            str = dodajKomorke(str, dane);
                        }
                        iterator++;
                    }
                    if (i == 0)
                    {
                        str = zakonczWiersz(str);
                        str = zakonczNaglowek(str);
                        flaga2 = true;
                    }
                    else if (i == wiersze - 1 && flaga2 && i < 2)
                    {
                        str = zakonczWiersz(str);
                        str = zakonczCialo(str);
                        flaga2 = false;
                    }
                    else if (i == wiersze - 2 && flaga2 && i >= 2)
                    {
                        str = zakonczWiersz(str);
                        str = zakonczCialo(str);
                        flaga2 = false;
                    }
                    else if (i == wiersze - 1 && i >= 2)
                    {
                        str = zakonczStopke(str);
                    }
                    else
                    {
                        str = zakonczWiersz(str);
                    }
                }
                str = zakonczHtml(str);
                html = str;
            }
            else
            {
                Console.Write("Podaj inne wymiary niż 0");
            }
        }
        // Dane z pliku
        public Html(int nr)
        {
            int iterator = 0;
            string str = "";
            bool flaga = false;
            bool flaga2 = false;
            var lines = File.ReadAllLines("lab1.csv");

            str = rozpocznijHtml(str);
            foreach (var lin in lines)
            {
                if (iterator == 0)
                {
                    str = dodajNaglowek(str);
                    str = dodajWiersz(str);
                    flaga = true;
                }
                else if (iterator > 0 && flaga)
                {
                    str = dodajCialo(str);
                    str = dodajWiersz(str);
                    flaga = false;
                }
                else if (iterator == lines.Length - 1 && iterator >= 2)
                {
                    str = dodajStopke(str);
                    str = dodajWiersz(str);
                }
                else
                {
                    str = dodajWiersz(str);
                }
                var cols = lin.Split(';');
                foreach (var col in cols)
                {
                    if (iterator == 0)
                    {
                        str = dodajPoleNaglowka(str, col);
                    }
                    else
                    {
                        str = dodajKomorke(str, col);
                    }
                }
                if (iterator == 0)
                {
                    str = zakonczWiersz(str);
                    str = zakonczNaglowek(str);
                    flaga2 = true;
                }
                else if (iterator == lines.Length - 1 && flaga2 && iterator < 2)
                {
                    str = zakonczWiersz(str);
                    str = zakonczCialo(str);
                    flaga2 = false;
                }
                else if (iterator == lines.Length - 2 && flaga2 && iterator >= 2)
                {
                    str = zakonczWiersz(str);
                    str = zakonczCialo(str);
                    flaga2 = false;
                }
                else if (iterator == lines.Length - 1 && iterator >= 2)
                {
                    str = zakonczStopke(str);
                }
                else
                {
                    str = zakonczWiersz(str);
                }
                iterator++;
            }
            str = zakonczHtml(str);
            html = str;
        }

        public string rozpocznijHtml(string str)
        {
            str += "<!DOCTYPE html> \n<html> \n <style>\n thead {color:green;} \n tbody {color:blue;} \n tfoot {color:red;} \n table, th, td { border: 1px solid black; }\n<body>\n </style>\n\n<table> \n";
            return str;
        }
        public string zakonczHtml(string str)
        {
            str += "</table>\n\n</body>\n</html> \n";
            return str;
        }
        public string dodajWiersz(string str)
        {
            str += "   <tr> \n";
            return str;
        }
        public string zakonczWiersz(string str)
        {
            str += "   </tr> \n";
            return str;
        }
        public string dodajPoleNaglowka(string str, string dane)
        {
            str += "    <th> \n     " + dane + " \n    </th> \n";
            return str;
        }
        public string dodajKomorke(string str, string dane)
        {
            str += "    <td> \n     " + dane + " \n    </td> \n";
            return str;
        }
        public string dodajNaglowek(string str)
        {
            str += " <thead> \n";
            return str;
        }
        public string zakonczNaglowek(string str)
        {
            str += " </thead> \n";
            return str;
        }
        public string dodajCialo(string str)
        {
            str += " <tbody> \n";
            return str;
        }
        public string zakonczCialo(string str)
        {
            str += " </tbody> \n";
            return str;
        }
        public string dodajStopke(string str)
        {
            str += "  <tfoot> \n";
            return str;
        }
        public string zakonczStopke(string str)
        {
            str += "</tfoot> \n";
            return str;
        }
        public void wyswietlKod()
        {
            Console.Write(html.ToString());
        }
        public string kodWynikowy()
        {
            return html.ToString();
        }
        public string wprowadzDane(int i, int j)
        {
            Console.Write("Podaj dane dla komorki: " + j + i + "\n");
            daneTabeli = Console.ReadLine();
            return daneTabeli;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            void zapisPliku(string str, int nr)
            {
                File.WriteAllText("index" + nr + ".html", str);
            }
            void test1(int wiersze, int kolumny)
            {
                //2x2
                string[] tabString = null;
                Html obiekt;
                obiekt = new Html(wiersze, kolumny, 1, tabString);
                zapisPliku(obiekt.kodWynikowy(), 1);
            }
            void test2(int wiersze, int kolumny)
            {
                //2x3
                string[] tabString = null;
                Html obiekt;
                obiekt = new Html(wiersze, kolumny, 2, tabString);
                zapisPliku(obiekt.kodWynikowy(), 2);
            }
            void test3(int nr)
            {
                //odczyt z pliku
                Html obiekt;
                obiekt = new Html(nr);
                zapisPliku(obiekt.kodWynikowy(), 3);
            }
            void test4(int wiersze, int kolumny)
            {
                //2x5
                Html obiekt;
                string[] tabString = null;
                obiekt = new Html(wiersze, kolumny, 4, tabString);
                zapisPliku(obiekt.kodWynikowy(), 4);
            }
            void test6(int wiersze, int kolumny)
            {
                // Dane dodane przy pomocy metod z klawiatury
                Html obiekt;
                string[] tabString = null;
                obiekt = new Html(wiersze, kolumny, 5, tabString);
                zapisPliku(obiekt.kodWynikowy(), 5);
            }
            void test7(int wiersze, int kolumny, string[] tabString)
            {   //Dane podane w konstruktorze
                //2x3
                Html obiekt;
                obiekt = new Html(wiersze, kolumny, 6, tabString);
                zapisPliku(obiekt.kodWynikowy(), 6);
                //obiekt.wyswietlKod(); //- Odkomentowanie wyświetla kod html w konsoli, można też w innych testach stosować               
            }

            string[] dane = { "imie", "nazwisko", "rok", "Mariusz", "Pudzian", "3000" };
            test1(2, 2); //  2x2                                     html1
            test2(2, 3); //  2x3                                     html2
            test3(3);    //  Odczyt z pliku                          html3
            test4(2, 5); //  2x5                                     html4
            //test6(3, 3);   // Dane przez metody i z klawiatury, odkomentowanie sprawi ze bedziemy mogli podawac wartosci tabelki wierszami       html5
            test7(2, 3, dane);    // Dane w konstruktorze             html6
        }
    }
}
