//-----------------------------------------------------------------------
// <rights file="EAdjacence.cs" author="Arnaud de LATOUR">
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
    /// Ensemble des types d'ajacence possibles.
    /// </summary>
    public enum EAdjacence
    {
        NonAdjacent = 0,
        AdjacentTerre = 1,
        AdjacentMer = 2,
        AjacentTerreEtMer = 3
    }
}