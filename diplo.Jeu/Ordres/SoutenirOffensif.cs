//-----------------------------------------------------------------------
// <rights file="SoutenirOffensif.cs" author="Arnaud de LATOUR">
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
    /// Ordre de soutien offensif.
    /// </summary>
    public class SoutenirOffensif : OrdreAbstrait
    {
        #region Champs

        /// <summary>
        /// Nom de la région attaquante soutenue à l'aide de l'ordre.
        /// </summary>
        private String regionSoutenue;

        /// <summary>
        /// Nom de la région contre laquelle l'ordre induit un soutien.
        /// </summary>
        private String regionAttaquee;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe SoutenirOffensif.
        /// </summary>
        /// <param name="unite">Type de l'unité concernée par l'ordre.</param>
        /// <param name="belligerant">Belligérant auquel appartient l'unité concernée.</param>
        /// <param name="regionSoutenant"> Nom de la région où est donné l'ordre.</param>
        /// <param name="regionSoutenue">Nom de la région attaquante soutenue à l'aide de l'ordre.</param>
        /// <param name="regionAttaquee">Nom de la région contre laquelle l'ordre induit un soutien.</param>
        public SoutenirOffensif(
            EUnite unite,
            EBelligerant belligerant,
            String regionSoutenant,
            String regionSoutenue,
            String regionAttaquee)
            : base(unite, belligerant, regionSoutenant)
        {
            this.regionSoutenue = regionSoutenue;
            this.regionAttaquee = regionAttaquee;
            this.TypeOrdre = EOrdre.SoutienOffensif;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Nom de la région attaquante soutenue à l'aide de l'ordre.
        /// </summary>
        public String RegionSoutenue { get { return this.regionSoutenue; } }

        /// <summary>
        /// Nom de la région contre laquelle l'ordre induit un soutien.
        /// </summary>
        public String RegionAttaquee { get { return this.regionAttaquee; } }

        #endregion
    }
}