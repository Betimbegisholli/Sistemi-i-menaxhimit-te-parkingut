using System;

namespace ParkingMenagjment
{
    
    // Klasa Vendit te Parkimit!!
    public class VendiParkimit
    {

        // Atributet e klases
        public int ID { get; set; }
        public TipiVendit Tipi { get; set; }

        public bool eshteIzene { get; private set; }

        public double tarifa = 0;

        public Automjeti automjetiParkuar { get; set; }


        // Konstruktori
        public VendiParkimit(int id, TipiVendit tipi, double tarifa)
        {
            ID = id;
            Tipi = tipi;
            this.tarifa = tarifa;

        }


        // Metoda per parkim te automjetit
        public void parkoAutomjetin(Automjeti automjeti)
        {
            if (eshteIzene)
            {
                Console.WriteLine("Vendi i parkimit është i zënë.");
                return;
            }

            // Kontrollimi i tipit
            if (Tipi == TipiVendit.electric)
            {
                if (!(automjeti is Makina m && m.electric))
                {
                    Console.WriteLine("Vetem makina elektrike");
                    return;
                }
            }

            if (Tipi == TipiVendit.personaMeAftesiTeKufizuara)
            {
                if (!(automjeti is Makina m && m.personaMeAftesiTeKufizuara))
                {
                    Console.WriteLine("Vetem per persona me aftesi te kufizuara!");
                    return;
                }
            }

            // lejohen te gjitha veturat standard
            automjetiParkuar = automjeti;
            eshteIzene = true;

            ((IkohaParkimit)automjeti).FillimiIParkimit(DateTime.Now);
            Console.WriteLine($"Automjeti me targa {automjeti.Targa} është parkuar në vendin {ID}.");

        }


        // Metoda per lirimin e vendit te parkimit
        public void liroVendin()
        {
            if (!eshteIzene)
            {
                Console.WriteLine("Vendi i parkimit është i lirë.");
            }
            else
            {
                ((IkohaParkimit)automjetiParkuar).MbarimiIParkimit(DateTime.Now);
                Console.WriteLine($"Automjeti me targa {automjetiParkuar.Targa} është larguar nga vendi {ID}.");
                automjetiParkuar = null;
                eshteIzene = false;
            }
        }

    }

    // Klasa e parkingut
    public class Parkingu  
    {

        // Atributet 
        public List<VendiParkimit> VendParkimi { get; set; }  // List e tipit VendiParkimt 

        public int Kapaciteti { get; set; }

        // Konstruktori
        public Parkingu(int kapaciteti)
        {
            VendParkimi = new List<VendiParkimit>();  // inicializimi i listes brenda konstruktorit

            this.Kapaciteti = kapaciteti;
        }

        // Metoda per te shtuar vende te parkimit
        public void ShtoVendeParkimi(VendiParkimit vendi)
        {
            if (VendParkimi.Count >= Kapaciteti) // Kontrollojm kapacitetin e vendeve te parkimit
            {
                Console.WriteLine("Nuk mund të shtoni më vende parkimi, kapaciteti është arritur.");
                return;
            }
            VendParkimi.Add(vendi);
            Console.WriteLine($"Vendi i parkimit me ID {vendi.ID} është shtuar.");
        }


        // Metoda Parko
        public void Parko(Automjeti automjeti)
        {

            // Me ane te loopes kontrollojm seclin vend nese eshte i zene ose jo, dhe parkojm automjetin
            foreach (VendiParkimit vend in VendParkimi)
            {
                if (!vend.eshteIzene)
                {
                    vend.parkoAutomjetin(automjeti);   // therrsim metoden parkoAutomjetin();
                    return;
                }


            }
            Console.WriteLine("Nuk ka vende te lira!");
        }


        // Metoda per Dalja per nxirrjen e automjetit
        public void Dalja(Automjeti automjeti)
        {
            // Kontrolloj secilin vend nese ka automjet per ta nxjerr!!
            foreach (VendiParkimit vend in VendParkimi)
            {
                if (vend.automjetiParkuar == automjeti)
                {
                    vend.liroVendin();  // therrasim metoden liroVendin();
                    double ore = (automjeti.KohaDaljes - automjeti.KohaHyrjes).TotalHours;
                    double tarifa = LlogaritTarifen(automjeti, ore, vend.tarifa); // therrsim metoden LlogaritTarifen();
                    ArkivoTeDhenat(automjeti, vend, tarifa);    // therrasim metoden ArkivoTedhenat();
                    Console.WriteLine($"Tarifa totale: {tarifa} EUR");
                    return;
                }


            }
            Console.WriteLine("Ky automjet nuk u gjet ne parking!");
        }

        
        // Metoda per llogaritjen e tarifes
        private double LlogaritTarifen(Automjeti automjeti, double ore, double tarifa)
        {

            // llogaritja me ane te (ternary operator) zevendesues i if/else!! 
            double koeficienti =
            automjeti.Lloji == TipiAutomjetit.Motocikleta ? 0.5 :
            automjeti.Lloji == TipiAutomjetit.Kamion ? 2.0 :
            1.0;
            return ore * tarifa * koeficienti;
        }

        // Metoda per arkivimin e te dhenave
        private void ArkivoTeDhenat(Automjeti automjeti, VendiParkimit vendi, double tarifa)
        {
            string path = "teDhenatParkimit.txt"; // krijojm nje string me emrin e file-it
            string teDhenat = $"{DateTime.Now}, {automjeti.Targa}, {automjeti.Lloji}, VendID:{vendi.ID}, Tarifa:{tarifa:F2} EUR";

            try
            {
                
            File.AppendAllText(path, teDhenat + Environment.NewLine);  // shton tekstin ne fund te file-it
            } 

            catch (Exception ex)
            {
                Console.WriteLine("Gabim gjatë ruajtjes së të dhënave: " + ex.Message);
            }

        }



    }
}