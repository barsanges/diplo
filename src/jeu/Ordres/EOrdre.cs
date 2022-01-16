//-----------------------------------------------------------------------
// <rights file="EOrdre.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Jeu.Ordres
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Ensemble des ordres possibles;
    /// </summary>
    public enum EOrdre
    {
        Attaque,
        AttaqueReussie,
        AttaqueEchouee,
        Convoi,
        ConvoiCoupe,
        SoutienDefensif,
        SoutienDefensifCoupe,
        SoutienOffensif,
        SoutienOffensifCoupe,
        Tenir
    }
}