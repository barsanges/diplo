//-----------------------------------------------------------------------
// <rights file="Attaquer.cs" author="Arnaud de LATOUR">
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
    /// Ordre d'attaque.
    /// </summary>
    public class Attaquer : OrdreAbstrait
    {
        #region Champs

        /// <summary>
        /// Nom de la région attaquée.
        /// </summary>
        private String nomRegionAttaquee;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe Attaquer.
        /// </summary>
        /// <param name="unite">Type de l'unité concernée par l'ordre.</param>
        /// <param name="belligerant">Belligérant auquel appartient l'unité concernée.</param>
        /// <param name="regionAttaquante">Nom de la région où est donné l'ordre.</param>
        /// <param name="regionAttaquee">Nom de la région attaquée.</param>
        public Attaquer(EUnite unite, EBelligerant belligerant, String regionAttaquante, String regionAttaquee)
            : base(unite, belligerant, regionAttaquante)
        {
            this.nomRegionAttaquee = regionAttaquee;
            this.TypeOrdre = EOrdre.Attaque;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Nom de la région attaquée.
        /// </summary>
        public String RegionAttaquee { get { return this.nomRegionAttaquee; } }

        #endregion
    }
}
