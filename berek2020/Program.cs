using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Berek2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var dolgozok = beolvasDolgozok("..\\..\\..\\src/berek2020.txt");

            Console.WriteLine("3. feladat:");
            Console.WriteLine($"Dolgozók száma: {dolgozok.Count}");

            Console.WriteLine("4. feladat:");
            var atlagber = dolgozok.Average(d => d.bere) / 1000.0;
            Console.WriteLine($"Átlagbér: {Math.Round(atlagber, 1)} ezer forint");

            Console.WriteLine("5. feladat:");
            Console.Write("Kérem adja meg a részleg nevét: ");
            string reszleginput = Console.ReadLine();

            Console.WriteLine("6. feladat:");
            var maxberdolgozo = dolgozok
                .Where(d => d.reszleg.Equals(reszleginput, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(d => d.bere)
                .FirstOrDefault();
            if (maxberdolgozo != null)
            {
                Console.WriteLine($"{maxberdolgozo.neve}; {maxberdolgozo.neme}; {maxberdolgozo.reszleg}; {maxberdolgozo.belepeseve}; {maxberdolgozo.bere}");
            }
            else
            {
                Console.WriteLine("A megadott részleg nem létezik a cégnél!");
            }

            Console.WriteLine("7. feladat:");
            var reszlegstatisztika = dolgozok
                .GroupBy(d => d.reszleg)
                .Select(g => new { reszleg = g.Key, szam = g.Count() });
            foreach (var reszleg in reszlegstatisztika)
            {
                Console.WriteLine($"{reszleg.reszleg}: {reszleg.szam} fő");
            }
        }

        static List<Berek> beolvasDolgozok(string fajlut)
        {
            var dolgozok = new List<Berek>();
            var sorok = File.ReadAllLines(fajlut);
            foreach (var sor in sorok.Skip(1))
            {
                var reszek = sor.Split(';');
                var dolgozo = new Berek
                {
                    neve = reszek[0],
                    neme = reszek[1],
                    reszleg = reszek[2],
                    belepeseve = int.Parse(reszek[3]),
                    bere = int.Parse(reszek[4])
                };
                dolgozok.Add(dolgozo);
            }
            return dolgozok;
        }
    }
}