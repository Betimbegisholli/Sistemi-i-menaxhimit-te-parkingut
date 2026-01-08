using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Mirësevini në sistemin e menaxhimit të parkimit!");
       Vetura makina1 = new Makina("Toyota", "Corolla", "AA123BB");
        Console.WriteLine($"Marka: {makina1.Marka}, Modeli: {makina1.Modeli}, Targa: {makina1.Targa}");

        IkohaParkimit kohaParkimit = makina1 as IkohaParkimit;
        if (kohaParkimit != null)
        {
            kohaParkimit.FillimiIParkimit(DateTime.Now);
            // Simulimi i parkimit për 2 orë
            System.Threading.Thread.Sleep(2000); // Përdoret për të simuluar vonesën
            kohaParkimit.MbarimiIParkimit(DateTime.Now.AddHours(2));
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