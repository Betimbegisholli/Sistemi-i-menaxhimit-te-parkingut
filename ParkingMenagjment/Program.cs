using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;


namespace ParkingSystem
{

    class Program
    {
        static void Main(string[] args)
        {

            Parkingu parkingu = new Parkingu();

            Makina makina = new Makina("Toyota", "Corolla", "AA123BB");
            
            VendiParkimit vendi1 = new VendiParkimit(1, TipiVendit.standard, 2.0);
          
            parkingu.ShtoVendeParkimi(vendi1);

            parkingu.Parko(makina);
            System.Threading.Thread.Sleep(100000);
            parkingu.Dalja(makina);

        }

    }

    //------------------------------------------------------------------------------------------------------------------------------
    //interfaces

    interface IkohaParkimit
    {
        void FillimiIParkimit(DateTime kohaFillimit);
        void MbarimiIParkimit(DateTime kohaMbarimit);
    }

    //emuns

    public enum TipiAutomjetit
    {
        Vetura,
        Motocikleta,
        Kamion
    }

    public enum TipiVendit
    {
        electric,

        standard,

        personaMeAftesiTeKufizuara
    }

    //Klasa Automjetit (Klasa Prind)!

    public abstract class Automjeti : IkohaParkimit
    {
        public string Marka { get; set; }
        public string Tipi { get; set; }
        public string Targa { get; set; }
        public TipiAutomjetit Lloji { get; set; }

        public DateTime KohaHyrjes { get; set; }
        public DateTime KohaDaljes { get; set; }

        void IkohaParkimit.FillimiIParkimit(DateTime kohaFillimit)
        {
            KohaHyrjes = kohaFillimit;
            Console.WriteLine($"Makina {Marka} me targa {Targa} hyri ne parking ne: {KohaHyrjes}");
        }

        void IkohaParkimit.MbarimiIParkimit(DateTime kohaMbarimit)
        {
            KohaDaljes = kohaMbarimit;
            Console.WriteLine($"Makina {Marka} me targa {Targa} doli nga parkingu ne: {KohaDaljes}");
        }

        public Automjeti(string marka, string tipi, string targa, TipiAutomjetit lloji)
        {
            Marka = marka;
            Tipi = tipi;
            Lloji = lloji;
            Targa = targa;

        }

        public void ShfaqTeDhenat()
        {
            Console.WriteLine($"Marka: {Marka}, Tipi: {Tipi}, Targa: {Targa}, Lloji: {Lloji}");
        }
    }

    // Klasat femije!!!

    public class Makina : Automjeti
    {
        public Makina(string marka, string tipi, string targa)
            : base(marka, tipi, targa, TipiAutomjetit.Vetura)
        {

        }


    }

    public class Motocikleta : Automjeti
    {
        public Motocikleta(string marka, string tipi, string targa)
            : base(marka, tipi, targa, TipiAutomjetit.Motocikleta)
        {

        }


    }
    public class Kamion : Automjeti
    {
        public Kamion(string marka, string tipi, string targa)
            : base(marka, tipi, targa, TipiAutomjetit.Kamion)
        {

        }


    }


    // Klasa Vendit te Parkimit (logjika)!!
    public class VendiParkimit
    {
        public int ID { get; set; }
        public TipiVendit Tipi { get; set; }

        public bool eshteIzene { get; set; }

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
            }
            else
            {
                automjetiParkuar = automjeti;
                eshteIzene = true;

                ((IkohaParkimit)automjeti).FillimiIParkimit(DateTime.Now);
                Console.WriteLine($"Automjeti me targa {automjeti.Targa} është parkuar në vendin {ID}.");
            }
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
    public class Parkingu
    {
        public List<VendiParkimit> VendParkimi { get; set; }

        public Parkingu ()
        {
            VendParkimi = new List<VendiParkimit>();
        }
        public void ShtoVendeParkimi(VendiParkimit vendi)
        {
            VendParkimi.Add(vendi);
            Console.WriteLine($"Vendi i parkimit me ID {vendi.ID} është shtuar.");
        }

        public void Parko (Automjeti automjeti)
        {
            foreach (VendiParkimit vend in VendParkimi)
            {
                if (!vend.eshteIzene)
                {
                    vend.parkoAutomjetin(automjeti);
                    return;
                }
                else
                {
                    Console.WriteLine("Nuk ka vende te lira!");
                }
            }
        }

        public void Dalja (Automjeti automjeti)
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
                else
                {
                    Console.WriteLine("Ky automjet nuk u gjet ne parking!");
                }
            }
        }

        private double LlogaritTarifen(Automjeti automjeti, double ore, double tarifa)
        {
            double koeficienti = 
            automjeti.Lloji == TipiAutomjetit.Motocikleta ? 0.5 :
            automjeti.Lloji == TipiAutomjetit.Kamion ? 2.0 :
            1.0;
            return ore * tarifa * koeficienti;
        }

        private void ArkivoTeDhenat (Automjeti automjeti, VendiParkimit vendi, double tarifa)
        {
            string path = "teDhenatParkimit.txt";
            string teDhenat = $"{DateTime.Now}, {automjeti.Targa}, {automjeti.Lloji}, VendID:{vendi.ID}, Tarifa:{tarifa:F2} EUR";
            File.AppendAllText(path, teDhenat + Environment.NewLine);
        
        }

        

    }

}
