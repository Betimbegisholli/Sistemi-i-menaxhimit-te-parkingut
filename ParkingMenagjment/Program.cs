using System;

namespace ParkingMenagjment
{
    
    // Klasa Vendit te Parkimit (logjika)!!
    public class VendiParkimit
    {
        public int ID { get; set; }
        public TipiVendit Tipi { get; set; }

        public bool eshteIzene { get; private set; }

        public double tarifa = 0;

        public Automjeti automjetiParkuar { get; set; }

        public VendiParkimit(int id, TipiVendit tipi, double tarifa)
        {
            ID = id;
            Tipi = tipi;
            this.tarifa = tarifa;

        }

        public void parkoAutomjetin(Automjeti automjeti)
        {
            if (eshteIzene)
            {
                Console.WriteLine("Vendi i parkimit është i zënë.");
                return;
            }

            // Kontrollimi i titpit
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

    // Klasa e parkingut ne pergjithesi
    public class Parkingu  //parkingu standart
    {
        public List<VendiParkimit> VendParkimi { get; set; }

        public int Kapaciteti { get; set; }
        public Parkingu(int kapaciteti)
        {
            VendParkimi = new List<VendiParkimit>();

            this.Kapaciteti = kapaciteti;
        }
        public void ShtoVendeParkimi(VendiParkimit vendi)
        {
            if (VendParkimi.Count >= Kapaciteti)
            {
                Console.WriteLine("Nuk mund të shtoni më vende parkimi, kapaciteti është arritur.");
                return;
            }
            VendParkimi.Add(vendi);
            Console.WriteLine($"Vendi i parkimit me ID {vendi.ID} është shtuar.");
        }

        public void Parko(Automjeti automjeti)
        {


            foreach (VendiParkimit vend in VendParkimi)
            {
                if (!vend.eshteIzene)
                {
                    vend.parkoAutomjetin(automjeti);
                    return;
                }


            }
            Console.WriteLine("Nuk ka vende te lira!");
        }

        public void Dalja(Automjeti automjeti)
        {
            foreach (VendiParkimit vend in VendParkimi)
            {
                if (vend.automjetiParkuar == automjeti)
                {
                    vend.liroVendin();
                    double ore = (automjeti.KohaDaljes - automjeti.KohaHyrjes).TotalHours;
                    double tarifa = LlogaritTarifen(automjeti, ore, vend.tarifa);
                    ArkivoTeDhenat(automjeti, vend, tarifa);
                    Console.WriteLine($"Tarifa totale: {tarifa} EUR");
                    return;
                }


            }
            Console.WriteLine("Ky automjet nuk u gjet ne parking!");
        }

        private double LlogaritTarifen(Automjeti automjeti, double ore, double tarifa)
        {
            double koeficienti =
            automjeti.Lloji == TipiAutomjetit.Motocikleta ? 0.5 :
            automjeti.Lloji == TipiAutomjetit.Kamion ? 2.0 :
            1.0;
            return ore * tarifa * koeficienti;
        }

        private void ArkivoTeDhenat(Automjeti automjeti, VendiParkimit vendi, double tarifa)
        {
            string path = "teDhenatParkimit.txt";
            string teDhenat = $"{DateTime.Now}, {automjeti.Targa}, {automjeti.Lloji}, VendID:{vendi.ID}, Tarifa:{tarifa:F2} EUR";
            File.AppendAllText(path, teDhenat + Environment.NewLine);

        }



    }
}