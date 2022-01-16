//-----------------------------------------------------------------------
// <rights file="OrdreAbstrait.cs" author="Arnaud de LATOUR">
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
    /// Classe abstraite utilisée pour l'ensemble des ordres (hormis l'ordre d'ajustement).
    /// </summary>
    public abstract class OrdreAbstrait
    {
        #region Champs

        /// <summary>
        /// Type de l'unité concernée par l'ordre.
        /// </summary>
        protected EUnite unite;

        /// <summary>
        /// Belligérant auquel appartient l'unité concernée.
        /// </summary>
        protected EBelligerant belligerant;

        /// <summary>
        /// Nom de la région où est donné l'ordre.
        /// </summary>
        protected String nomRegion;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe OrdreAbstrait.
        /// </summary>
        /// <param name="unite">Type de l'unité concernée par l'ordre.</param>
        /// <param name="belligerant">Belligérant auquel appartient l'unité concernée.</param>
        /// <param name="nomRegion">Nom de la région où est donné l'ordre.</param>
        public OrdreAbstrait(EUnite unite, EBelligerant belligerant, String nomRegion)
        {
            this.unite = unite;
            this.belligerant = belligerant;
            this.nomRegion = nomRegion;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Type de l'unité concernée par l'ordre.
        /// </summary>
        public EUnite Unite { get { return this.unite; } }

        /// <summary>
        /// Belligérant auquel appartient l'unité concernée.
        /// </summary>
        public EBelligerant Belligerant { get { return this.belligerant; } }

        /// <summary>
        /// Nom de la région où est donné l'ordre.
        /// </summary>
        public String Region { get { return this.nomRegion; } }

        #endregion

        #region Propriétés virtuelles

        /// <summary>
        /// Nature de l'ordre.
        /// </summary>
        public EOrdre TypeOrdre { get; set; }

        #endregion
    }
}
