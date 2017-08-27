//-----------------------------------------------------------------------
// <rights file="SoutenirDefensif.cs" author="Arnaud de LATOUR">
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
    /// Ordre de soutien défensif.
    /// </summary>
    public class SoutenirDefensif : OrdreAbstrait
    {
        #region Champs

        /// <summary>
        /// Nom de la région soutenue (défensivement) à l'aide de l'ordre.
        /// </summary>
        private String regionSoutenue;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe SoutenirDefensif.
        /// </summary>
        /// <param name="unite">Type de l'unité concernée par l'ordre.</param>
        /// <param name="belligerant">Belligérant auquel appartient l'unité concernée.</param>
        /// <param name="regionSoutenant">Nom de la région où est donné l'ordre.</param>
        /// <param name="regionSoutenue">Nom de la région soutenue (défensivement) à l'aide de l'ordre.</param>
        public SoutenirDefensif(EUnite unite, EBelligerant belligerant, String regionSoutenant, String regionSoutenue)
            : base(unite, belligerant, regionSoutenant)
        {
            this.regionSoutenue = regionSoutenue;
            this.TypeOrdre = EOrdre.SoutienDefensif;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Nom de la région soutenue (défensivement) à l'aide de l'ordre.
        /// </summary>
        public String RegionSoutenue { get { return this.regionSoutenue; } }

        #endregion
    }
}
