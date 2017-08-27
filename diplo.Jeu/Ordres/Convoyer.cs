//-----------------------------------------------------------------------
// <rights file="Convoyer.cs" author="Arnaud de LATOUR">
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
    /// Ordre de convoi (flottes uniquement).
    /// </summary>
    public class Convoyer : OrdreAbstrait
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
        /// Initialise une nouvelle instance de la classe Convoyer.
        /// </summary>
        /// <param name="belligerant">Belligérant auquel appartient l'unité concernée.</param>
        /// <param name="nomRegion">Nom de la région où est donné l'ordre.</param>
        /// <param name="ordreSoutenu">Ordre correspondant à l'attaque convoyée via cet ordre.</param>
        /// <param name="regionSoutenue">Nom de la région attaquante soutenue à l'aide de l'ordre.</param>
        /// <param name="regionAttaquee">Nom de la région contre laquelle l'ordre induit un soutien.</param>
        public Convoyer(EBelligerant belligerant, String nomRegion, String regionSoutenue, String regionAttaquee)
            : base(EUnite.Flotte, belligerant, nomRegion)
        {
            this.regionSoutenue = regionSoutenue;
            this.regionAttaquee = regionAttaquee;
            this.TypeOrdre = EOrdre.Convoi;
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