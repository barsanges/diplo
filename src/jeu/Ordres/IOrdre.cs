//-----------------------------------------------------------------------
// <rights file="IOrdre.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Jeu.Ordres
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using diplo.Geographie;
    using diplo.Geographie.Enumerations;

    /// <summary>
    /// Interface utilisée pour l'ensemble des ordres.
    /// </summary>
    /// <remarks>
    /// Une classe abstraite aurait été beaucoup plus appropriée.
    /// </remarks>
    public interface IOrdre
    {
        #region Propriétés

        /// <summary>
        /// Type de l'unité concernée par l'ordre.
        /// </summary>
        EUnite Unite { get; }

        /// <summary>
        /// Belligérant auquel appartient l'unité concernée.
        /// </summary>
        EBelligerant Belligerant { get; }

        /// <summary>
        /// Nom de la région où est donné l'ordre.
        /// </summary>
        String Region { get; }

        #endregion
    }
}