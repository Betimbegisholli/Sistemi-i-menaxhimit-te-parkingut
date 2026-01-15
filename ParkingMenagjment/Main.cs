using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using ParkingMenagjment;


namespace ParkingMenagjment
{

    class Program
    {
        static void Main(string[] args)  // Metoda main
        {
            int kapaciteti = 28;
            Parkingu parkingu = new Parkingu(kapaciteti);   // Inicializimi i Klases Parkingu 

            int id = 1;


            // Shtimi i vendeve te parkimit me ane te loopave 
            for (int i = 0; i < 20; i++)
                parkingu.ShtoVendeParkimi(new VendiParkimit(id++, TipiVendit.standard, 2.0));

            for (int i = 0; i < 5; i++)
                parkingu.ShtoVendeParkimi(new VendiParkimit(id++, TipiVendit.electric, 3.0));

            for (int i = 0; i < 3; i++)
                parkingu.ShtoVendeParkimi(new VendiParkimit(id++, TipiVendit.personaMeAftesiTeKufizuara, 4.0));

            Console.WriteLine("Regjistrimi i automjeteve për parking (shkruaj 'dil' për dalje)");


            // Inputat per parkimin e automjetit
            while (true)
            {
                Console.Write("\nZgjidh tipin (Makina / Motociklete / Kamion / dil): ");
                string tipiInput = Console.ReadLine()?.ToLower();

                if (tipiInput == "dil")         
                    break;                      // Dalim nga loopa

                Automjeti automjeti = null;     // Inicializimi i automjetit me vler fillestare null (te zbrazet)

                // Kontrolloj tipin nese eshte makine
                if (tipiInput == "makina")
                {
                    Console.Write("Shkruani marken: ");
                    string marka = Console.ReadLine() ?? "";

                    Console.Write("Shkruani modelin: ");
                    string modeli = Console.ReadLine() ?? "";

                    Console.Write("Shkruani targen: ");
                    string targa = Console.ReadLine() ?? "";

                    Console.Write("A është elektrike? (po/jo): ");
                    bool electric = Console.ReadLine()?.ToLower() == "po";

                    Console.Write("A është për persona me aftësi të kufizuara? (po/jo): ");
                    bool meAftesi = Console.ReadLine()?.ToLower() == "po";

                    automjeti = new Makina(marka, modeli, targa, electric, meAftesi); // Inicializmi Makines me referenc Automjetin
                }

                // Kontrolloj tipin nese eshte motociklete
                else if (tipiInput == "motociklete")
                {
                    Console.Write("Shkruani marken: ");
                    string marka = Console.ReadLine() ?? "";

                    Console.Write("Shkruani modelin: ");
                    string modeli = Console.ReadLine() ?? "";

                    Console.Write("Shkruani targen: ");
                    string targa = Console.ReadLine() ?? "";

                    automjeti = new Motocikleta(marka, modeli, targa);      // Inicializmi motocikletes me referenc Automjetin
                }

                // Kontrolloj tipin nese eshte kamion
                else if (tipiInput == "kamion")
                {
                    Console.Write("Shkruani marken: ");
                    string marka = Console.ReadLine() ?? "";

                    Console.Write("Shkruani modelin: ");
                    string modeli = Console.ReadLine() ?? "";

                    Console.Write("Shkruani targen: ");
                    string targa = Console.ReadLine() ?? "";

                    automjeti = new Kamion(marka, modeli, targa);       // Inicializmi kamionit me referenc Automjetin
                }
                else
                {
                    Console.WriteLine("Tip i pavlefshëm.");
                    continue;
                }

                if (automjeti != null)
                {
                    parkingu.Parko(automjeti);      // Parkojm automjetin me metoden e Parko(); te klases Parkingu
                }
            }

            // DALJA: kërko automjetin sipas targës dhe nxirr atë
            Console.Write("\nShkruani targen e automjetit për dalje: ");
            string targaDalje = Console.ReadLine();

            bool uGjet = false;      // vlera fillestare e variables uGjet vendoset false

            foreach (VendiParkimit vend in parkingu.VendParkimi)
            {
                if (vend.eshteIzene && vend.automjetiParkuar.Targa == targaDalje) //kontrollojm nese eshte vndi i zene dhe perputhet targa
                {
                    parkingu.Dalja(vend.automjetiParkuar);
                    uGjet = true;
                    break; // ndal loopen pasi  te nxjerrim automjetin
                }
            }

            if (!uGjet)
            {
                Console.WriteLine("Nuk u gjet asnjë automjet me këtë targë.");
            }

            // Shfaq automjetet aktualisht të parkuara
            Console.WriteLine("\nAutomjetet aktualisht të parkuara:");

           
           // Shfaq te dhenat per secilin vendparkimi te zene!!
            foreach (VendiParkimit vend in parkingu.VendParkimi)
            {
                if (vend.eshteIzene)
                {
                    Console.Write($"Vendi {vend.ID}: ");
                    vend.automjetiParkuar.ShfaqTeDhenat();
                }
            }
        }
    }
}
