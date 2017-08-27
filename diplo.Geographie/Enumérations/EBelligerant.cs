//-----------------------------------------------------------------------
// <rights file="EBelligerant.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Geographie.Enumerations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Ensemble des belligérants.
    /// </summary>
    public enum EBelligerant
    {
        Aucun = 0,
        Lautrec = 1, // France
        Barsanges = 2, // diplo
        Empire = 3, // Saint Empire
        Mélinde = 4, // Sicile
        Brémontrée = 5, // Angleterre
        Gretz = 6, // Union de Kalmar (Union de Thyr : Gretz et Vélage)
        Palavin = 7, // Pologne-Lituanie
        Thymée = 8 // Macédoine
    }
}