using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;


namespace ParkingSystem{

class Program
{
    static void Main(string[] args)
    {
        
    Parkingu parkingu = new Parkingu(5);

    Makina makina1 = new Makina("BMW", "X5", "KS-123-AB" , TipiAutomjetit.Vetura);
    makina1.ShfaqTeDhenat();
    makina1.FillimiIParkimit(DateTime.Now);
    makina1.MbarimiIParkimit(DateTime.Now.AddHours(3.35));
    parkingu.Tarifa(makina1, 3.35);
    parkingu.shtoAutomjetin(makina1);
    parkingu.ArkivoTeDhenat(makina1);
        
        
        
        
       
       
       
       
       
       
       
       
       
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

public enum vendiParkimit
{
    electric,
    
    standard,

    personaMeAftesiTeKufizuara
}

//Objects

public abstract class Automjeti
{
    public string Marka { get; set; }
    public string Tipi { get; set; }
    public string Targa { get; set; }
    public TipiAutomjetit Lloji { get; set; }


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

public class Makina : Automjeti, IkohaParkimit
{
    public Makina(string marka, string tipi, string targa, TipiAutomjetit lloji)
        : base(marka, tipi, targa, lloji)
        {
            
        }
    
    public void FillimiIParkimit(DateTime kohaFillimit)
    {
        Console.WriteLine($"Koha e fillimit të parkimit: {kohaFillimit}");
    }

    public void MbarimiIParkimit(DateTime kohaMbarimit)
    {
        Console.WriteLine($"Koha e mbarimit të parkimit: {kohaMbarimit}");
    }
}

// kapaciteti
public class Parkingu
{
 public List<Automjeti> veturat {get; set;}
 public List<Automjeti> motocikleta {get; set;}
 public List<Automjeti> kamionet {get; set;}
 public int Kapaciteti {get; set;}

 public Parkingu(int kapaciteti){
    Kapaciteti = kapaciteti;
    veturat = new List<Automjeti>();
    motocikleta = new List<Automjeti>();
    kamionet = new List<Automjeti>();
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