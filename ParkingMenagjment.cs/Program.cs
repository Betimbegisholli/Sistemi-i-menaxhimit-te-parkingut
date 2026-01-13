using System;
using System.Collections.Generic;

namespace ParkingSystem{

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Mirësevini në sistemin e menaxhimit të parkimit!");
       Vetura makina1 = new Makina("Toyota", "Corolla", "05-545-VF");
        Console.WriteLine($"Marka: {makina1.Marka}, Modeli: {makina1.Modeli}, Targa: {makina1.Targa}");

        Vetura makina2 = new Makina("Honda", "Civic", "01-656-HJ");
        Console.WriteLine($"Marka: {makina2.Marka}, Modeli: {makina2.Modeli}, Targa: {makina2.Targa}");
        //------------------------------------------------------------------

        
        Parkingu parkingu = new Parkingu(20);
            parkingu.shtoAutomjetin(makina1 as Makina);
            Console.WriteLine($"Numri i makinave në parking: {parkingu.makinat.Count}");
            //qetu kena met me shti makinat n list
        
        
        
        IkohaParkimit kohaParkimit = makina1 as IkohaParkimit;
        if (kohaParkimit != null)
        {
            kohaParkimit.FillimiIParkimit(DateTime.Now);
            // Simulimi i parkimit për 2 orë
            System.Threading.Thread.Sleep(9075); // Përdoret për të simuluar vonesën
            kohaParkimit.MbarimiIParkimit(DateTime.Now.AddHours(2));
        }
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
    Makina,
    Motocikletë,
    Kamion
}

public enum vendiParkimit
{
    electric,
    
    standard,

    personaMeAftesiTeKufizuara
}

//Objects

public abstract class Vetura
{
    public string Marka { get; set; }
    public string Modeli { get; set; }
    public string Targa { get; set; }



    public Vetura(string marka, string modeli, string targa)
    {
        Marka = marka;
        Modeli = modeli;
        
        Targa = targa;
    }
}

public class Makina : Vetura, IkohaParkimit
{
    public Makina(string marka, string modeli, string targa)
        : base(marka, modeli, targa)
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
 public List<Makina> makinat {get; set;}
 public int Kapaciteti {get; set;}

 public Parkingu(int kapaciteti){
    Kapaciteti = kapaciteti;
    makinat = new List<Makina>();
 }

 public void shtoAutomjetin(TipiAutomjetit Emri)
        {
            if(MakinaList.Count >= Kapaciteti)
            {
                Console.WriteLine("Parkingu është plot. ");
                return;
                
            }
            if (Emri == TipiAutomjetit.Makina)
            {
                MakinaList.Add(Emri);
                Console.WriteLine("Automjeti u shtua me sukses ne parking.");
            }
            else if (Emri == TipiAutomjetit.Motocikletë)
            {
                MotocikletëList.Add(Emri);
                Console.WriteLine("Automjeti u shtua me sukses ne parking.");
            }
            else if (Emri == TipiAutomjetit.Kamion)
            {
                KamionList.Add(Emri);
                Console.WriteLine("Automjeti u shtua me sukses ne parking.");
            }
            else
            {
                Console.WriteLine("Automjeti nuk eshte i pranueshem ne parking.");
            }
        }
    

}

}















































//interfaces
interface IkohaParkimit
{
     void FillimiIParkimit(DateTime kohaFillimit);
    void MbarimiIParkimit(DateTime kohaMbarimit);
}

//emuns

// public enum TipiAutomjetit
// {
//     Makina,
//     Motocikletë,
//     Kamion
// }

public enum vendiParkimit
{
    electric,
    
    standard,

    personaMeAftesiTeKufizuara
}

//Objects

public abstract class Vetura
{
    public string Marka { get; set; }
    public string Modeli { get; set; }
    public string Targa { get; set; }



    public Vetura(string marka, string modeli, string targa)
    {
        Marka = marka;
        Modeli = modeli;
        
        Targa = targa;
    }
}

public class Makina : Vetura, IkohaParkimit
{
    public Makina(string marka, string modeli, string targa)
        : base(marka, modeli, targa)
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
 public List<Makina> makinat {get; set;}
 public int Kapaciteti {get; set;}

 public Parkingu(int kapaciteti){
    Kapaciteti = kapaciteti;
    makinat = new List<Makina>();
 }
 
    

}