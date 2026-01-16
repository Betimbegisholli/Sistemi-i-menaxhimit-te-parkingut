using System;
using System.Collections.Generic;
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

            Console.WriteLine("Regjistrimi i automjeteve për parking (shkruaj 'Ndal' për dalje)");


            // Inputat per parkimin e automjetit
            while (true)
            {
                try
                {
                     Console.Write("\nZgjidh tipin (Makina / Motociklete / Kamion / Ndal): ");
                string tipiInput = Console.ReadLine()?.ToLower();

                if (tipiInput == "ndal")
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
                    string electricInput = Console.ReadLine()?.ToLower() ?? "";
                    if (electricInput != "po" && electricInput != "jo")
                        throw new FormatException("Duhet te shkruash 'po' ose 'jo'");
                    bool electric = electricInput == "po";

                    Console.Write("A është për persona me aftësi të kufizuara? (po/jo): ");
                    string meAftesiInput = Console.ReadLine()?.ToLower() ?? "";
                    if (meAftesiInput != "po" && meAftesiInput != "jo")
                        throw new FormatException("Duhet te shkruash 'po' ose 'jo'");
                    bool meAftesi = meAftesiInput == "po";

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
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
               
            }

            // DALJA: kërko automjetin sipas targës dhe nxirr atë
            Console.Write("\nA deshironi të nxirrni ndonje automjet? (po/jo): ");
            string deshironiDalje = Console.ReadLine()?.ToLower();

            if (deshironiDalje == "po")
            {
                Console.Write("Shkruani targen e automjetit për dalje: ");
                string targaDalje = Console.ReadLine();

                bool uGjet = false;

                foreach (VendiParkimit vend in parkingu.VendParkimi)
                {
                    if (vend.eshteIzene && vend.automjetiParkuar.Targa == targaDalje)
                    {
                        parkingu.Dalja(vend.automjetiParkuar);
                        uGjet = true;
                        break;
                    }
                }

                if (!uGjet)
                {
                    Console.WriteLine("Nuk u gjet asnjë automjet me këtë targë.");
                }
            }
            else
            {
                Console.WriteLine("Nuk po nxirret asnjë automjet.");
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
