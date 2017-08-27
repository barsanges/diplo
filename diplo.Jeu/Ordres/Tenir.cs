//-----------------------------------------------------------------------
// <rights file="Tenir.cs" author="Arnaud de LATOUR">
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
    /// Ordre de défense.
    /// </summary>
    public class Tenir : OrdreAbstrait
    {
        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe Tenir.
        /// </summary>
        /// <param name="unite">Type de l'unité concernée par l'ordre.</param>
        /// <param name="belligerant">Belligérant auquel appartient l'unité concernée.</param>
        /// <param name="nomRegion">Nom de la région où est donné l'ordre.</param>
        public Tenir(EUnite unite, EBelligerant belligerant, String nomRegion) : base(unite, belligerant, nomRegion)
        {
            this.TypeOrdre = EOrdre.Tenir;
        }

        #endregion
    }
}
