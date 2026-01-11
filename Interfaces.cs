using System;
using System.Collections.Generic;
using System.IO;

namespace ParkingSystem
{
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
}