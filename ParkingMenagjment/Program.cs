using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace ParkingSystem{

class Program
{
    static void Main(string[] args)
    {
        
    Parkingu  parkingu = new Parkingu(10);

    Makina makina = new Makina("Toyota", "Corolla", "AA123BB");
    Motocikleta moto = new Motocikleta("Yamaha", "YZF-R3", "CC456DD");
    Kamion kamion = new Kamion("Volvo", "FH16", "EE789FF");


    
    VendiParkimit vendi1 = new VendiParkimit(1, TipiVendit.standard, 2.0);
    VendiParkimit vendi2 = new VendiParkimit(2, TipiVendit.electric, 3.0);
    VendiParkimit vendi3 = new VendiParkimit(3, TipiVendit.personaMeAftesiTeKufizuara, 1.5);

    parkingu.ShtoVendeParkimi(vendi1);
    parkingu.ShtoVendeParkimi(vendi2);
    parkingu.ShtoVendeParkimi(vendi3);
  
    
    
    
    
        
        
        
        
       
       
       
       
       
       
       
       
       
        // IkohaParkimit kohaParkimit = makina as IkohaParkimit;
        // if (kohaParkimit != null)
        // {
        //     kohaParkimit.FillimiIParkimit(DateTime.Now);
        //     // Simulimi i parkimit për 2 orë
        //     System.Threading.Thread.Sleep(9075); // Përdoret për të simuluar vonesën
        //     kohaParkimit.MbarimiIParkimit(DateTime.Now.AddHours(2));
        // }
    }

}

//-----------------------------------------------------------------------------------------------------
//interfaces----------------------------------------------------------------------------------------
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

public enum  TipiVendit
{
    electric,
    
    standard,

    personaMeAftesiTeKufizuara
}

//Objects

public abstract class Automjeti: IkohaParkimit
{
    public string Marka { get; set; }
    public string Tipi { get; set; }
    public string Targa { get; set; }
    public TipiAutomjetit Lloji { get; set; }

    public DateTime KohaHyrjes { get; set; }
    public DateTime KohaDaljes { get; set; }

   void IkohaParkimit.FillimiIParkimit(DateTime kohaFillimit)
    {   KohaHyrjes = kohaFillimit;
        Console.WriteLine($"Makina {Marka} me targa {Targa} hyri ne parking ne: {KohaHyrjes}");
    }

    void IkohaParkimit.MbarimiIParkimit(DateTime kohaMbarimit)
    {   KohaDaljes = kohaMbarimit;
        Console.WriteLine($"Makina {Marka} me targa {Targa} doli nga parkingu ne: {KohaDaljes}");
    }

    public Automjeti(string marka, string tipi, string targa , TipiAutomjetit lloji)
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
                Console.WriteLine($"Automjeti me targa {automjetiParkuar.Targa} është larguar nga vendi {ID}.");
                automjetiParkuar = null;
                eshteIzene = false;
            }
        }

    }

// kapaciteti
public class Parkingu
{
 public List<VendiParkimit> vendiParkimit {get; set;}
 public void ShtoVendeParkimi(VendiParkimit vendi)
{
    vendiParkimit.Add(vendi);
    Console.WriteLine($"Vendi i parkimit me ID {vendi.ID} është shtuar.");
}

 






//------------------Tarifa------------------------------------
 public void Tarifa(Automjeti automjeti, double oreParkim)
 {
    double tarifa = 0;
    switch (automjeti.Lloji)
    {
        case TipiAutomjetit.Vetura:
        
            tarifa = 2 * oreParkim;
            break;
        case TipiAutomjetit.Motocikleta:
            tarifa = 1 * oreParkim;
            break;
        case TipiAutomjetit.Kamion:
            tarifa = 5.0 * oreParkim;
            break;
    }
    Console.WriteLine($"Tarifa për {automjeti.Lloji} me targa {automjeti.Targa} për {oreParkim} orë është: {tarifa} EUR");
 }




































//------------------shtoAutomjetin------------------------------------
 public void shtoAutomjetin(Automjeti automjeti)
        {
            int Totali = veturat.Count + motocikleta.Count + kamionet.Count;
                
                if (Totali >= Kapaciteti)
            {
                Console.WriteLine("Parkingu është plot.");
                return;
            }

            switch (automjeti.Lloji)
            {
                    case TipiAutomjetit.Vetura:
                        veturat.Add(automjeti);
                        Console.WriteLine("Vetura u shtua në parking.");
                        break;
                    case TipiAutomjetit.Motocikleta:
                        motocikleta.Add(automjeti);
                        Console.WriteLine("Motocikleta u shtua në parking.");
                        break;
                    case TipiAutomjetit.Kamion:
                        kamionet.Add(automjeti);
                        Console.WriteLine("Kamioni u shtua në parking.");

                        break;
                
            }
            // Console.WriteLine($"{automjeti.Lloji} u shtua në parking.");
        }
    public void ArkivoTeDhenat (Automjeti automjeti)
        {
            string path = "teDhenatParkimit.txt";

            string teDhenat =
             $"{automjeti.Marka}," +
             $"{automjeti.Tipi}," +
             $"{automjeti.Targa}," +
             $"{automjeti.Lloji}," +
             $"{DateTime.Now}";
        
            File.AppendAllText(path, teDhenat+ Environment.NewLine);
        }
}

    

}















































// //interfaces
// interface IkohaParkimit
// {
//      void FillimiIParkimit(DateTime kohaFillimit);
//     void MbarimiIParkimit(DateTime kohaMbarimit);
// }

// //emuns

// // public enum TipiAutomjetit
// // {
// //     Makina,
// //     Motocikletë,
// //     Kamion
// // }

// public enum vendiParkimit
// {
//     electric,
    
//     standard,

//     personaMeAftesiTeKufizuara
// }

// //Objects

// public abstract class Vetura
// {
//     public string Marka { get; set; }
//     public string Modeli { get; set; }
//     public string Targa { get; set; }



//     public Vetura(string marka, string modeli, string targa)
//     {
//         Marka = marka;
//         Modeli = modeli;
        
//         Targa = targa;
//     }
// }

// public class Makina : Vetura, IkohaParkimit
// {
//     public Makina(string marka, string modeli, string targa)
//         : base(marka, modeli, targa)
//     {
//     }
//     public void FillimiIParkimit(DateTime kohaFillimit)
//     {
//         Console.WriteLine($"Koha e fillimit të parkimit: {kohaFillimit}");
//     }

//     public void MbarimiIParkimit(DateTime kohaMbarimit)
//     {
//         Console.WriteLine($"Koha e mbarimit të parkimit: {kohaMbarimit}");
//     }
// }

// // kapaciteti
// public class Parkingu
// {
//  public List<Makina> makinat {get; set;}
//  public int Kapaciteti {get; set;}

//  public Parkingu(int kapaciteti){
//     Kapaciteti = kapaciteti;
//     makinat = new List<Makina>();
//  }
 
    

// }