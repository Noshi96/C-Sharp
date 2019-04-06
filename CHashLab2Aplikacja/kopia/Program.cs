using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CHashLab1;

namespace CHashLab2Aplikacja
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Html html1, html2, html3, html4;
            html1 = new Html(3, "lab2_1.csv");
            html1.zapisPliku(html1.kodWynikowy(), 1);
            html2 = new Html(3, "lab2_2.csv");
            html2.zapisPliku(html2.kodWynikowy(), 2);
            html3 = new Html(3, "lab2_3.csv");
            html3.zapisPliku(html3.kodWynikowy(), 3);
            html4 = new Html(3, "lab2_4.csv");
            html4.zapisPliku(html4.kodWynikowy(), 4);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
