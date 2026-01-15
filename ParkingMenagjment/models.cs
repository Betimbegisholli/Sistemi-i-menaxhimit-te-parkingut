using System;

namespace ParkingMenagjment
{
    
    //Klasa Automjetit (Klasa Prind)!

    public abstract class Automjeti : IkohaParkimit  // trashegon interface-in ikohaparkimit
    {
        // Atributet
        public string Marka { get; set; }
        public string Tipi { get; set; }
        public string Targa { get; set; }
        public TipiAutomjetit Lloji { get; set; }

        public DateTime KohaHyrjes { get; set; }
        public DateTime KohaDaljes { get; set; }


        // Logjika e metodave te trasheguara nga interface-i ikohaParkimit
        void IkohaParkimit.FillimiIParkimit(DateTime kohaFillimit)
        {
            KohaHyrjes = kohaFillimit;
            Console.WriteLine($"Lloji: {Lloji}, {Marka} me targa {Targa} hyriii ne parking ne: {KohaHyrjes}");
        }

        void IkohaParkimit.MbarimiIParkimit(DateTime kohaMbarimit)
        {
            KohaDaljes = kohaMbarimit;
            Console.WriteLine($"Lloji: {Lloji}, {Marka} me targa {Targa} doli nga parkingu ne: {KohaDaljes}");
        }

        // Konstruktori
        public Automjeti(string marka, string tipi, string targa, TipiAutomjetit lloji)
        {
            Marka = marka;
            Tipi = tipi;
            Lloji = lloji;
            Targa = targa;

        }

        // Metoda per mi shfaq te dhenat
        public void ShfaqTeDhenat()
        {
            Console.WriteLine($"Marka: {Marka}, Tipi: {Tipi}, Targa: {Targa}, ");
        }
    }

    // Klasat femije!!!
    public class Makina : Automjeti  // Trashegon klasen Automjeti
    {   
        // Atributet
        public bool electric { get; set; }
        public bool personaMeAftesiTeKufizuara { get; set; }

        // Konstruktori
        public Makina(string marka, string tipi, string targa,
        bool electric = false, bool meAftesiTeKufizuara = false)
            : base(marka, tipi, targa, TipiAutomjetit.Vetura)
        {
            this.electric = electric;
            personaMeAftesiTeKufizuara = meAftesiTeKufizuara;
        }


    }

    public class Motocikleta : Automjeti  // Trashegon klasen Automjeti
    {
        // Konstruktori
        public Motocikleta(string marka, string tipi, string targa)
            : base(marka, tipi, targa, TipiAutomjetit.Motocikleta)
        {

        }


    }
    public class Kamion : Automjeti  // Trashegon klasen Automjeti
    {
        // Konstruktori
        public Kamion(string marka, string tipi, string targa)
            : base(marka, tipi, targa, TipiAutomjetit.Kamion)
        {

        }


    }
}