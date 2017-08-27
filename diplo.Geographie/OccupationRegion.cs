//-----------------------------------------------------------------------
// <rights file="OccupationRegion.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Geographie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using diplo.Geographie.Enumerations;

    /// <summary>
    /// Données concernant l'occupation d'une région par une armée.
    /// </summary>
    public class OccupationRegion
    {
        #region Champs

        /// <summary>
        /// Nom de la région.
        /// </summary>
        private String nomRegion;

        /// <summary>
        /// Type de l'unité présente dans la région.
        /// </summary>
        private EUnite typeUnite;

        /// <summary>
        /// Nom de la puissance possédant l'unité dans la région.
        /// </summary>
        private EBelligerant possesseurUnite;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe OccupationRegion.
        /// </summary>
        /// <param name="nomRegion">Nom de la région concernée.</param>
        public OccupationRegion(String nomRegion)
        {
            this.nomRegion = nomRegion;
            this.typeUnite = EUnite.Aucune;
            this.possesseurUnite = EBelligerant.Aucun;

            this.TypeFutureUnite = EUnite.Aucune;
            this.FuturPossesseurUnite = EBelligerant.Aucun;
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe OccupationRegion.
        /// </summary>
        /// <param name="nomRegion">Nom de la région concernée.</param>
        /// <param name="typeUnite">Type de l'unité présente dans la région.</param>
        /// <param name="possesseurUnite">Possesseur de l'unité présente dans la région.</param>
        public OccupationRegion(String nomRegion, EUnite typeUnite, EBelligerant possesseurUnite)
        {
            this.nomRegion = nomRegion;
            this.typeUnite = typeUnite;
            this.possesseurUnite = possesseurUnite;

            this.TypeFutureUnite = EUnite.Aucune;
            this.FuturPossesseurUnite = EBelligerant.Aucun;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Nom de la région.
        /// </summary>
        public String Region { get { return this.nomRegion; } }

        /// <summary>
        /// Type de l'unité présente dans la région.
        /// </summary>
        public EUnite TypeUnite { get { return this.typeUnite; } }

        /// <summary>
        /// Type de la prochaine unité arrivant dans la région.
        /// </summary>
        public EUnite TypeFutureUnite { get; private set; }

        /// <summary>
        /// Nom de la puissance possédant l'unité dans la région.
        /// </summary>
        public EBelligerant PossesseurUnite { get { return this.possesseurUnite; } }

        /// <summary>
        /// Nom de la puissance possédant la prochaine unité dans la région.
        /// </summary>
        public EBelligerant FuturPossesseurUnite { get; private set; }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Détruit l'unité présente dans une région, ou abandonne celle-ci.
        /// </summary>
        public void DetruitOuAbandonne()
        {
            this.typeUnite = this.TypeFutureUnite;
            this.possesseurUnite = this.FuturPossesseurUnite;
        }

        /// <summary>
        /// Change l'unité présente dans la région.
        /// </summary>
        /// <param name="nouvelleUnite">Nouvelle unité présente dans la région.</param>
        /// <param name="nouveauPossesseur">Nom de la puissance possédant la nouvelle unité.</param>
        public void ChangeUnite(EUnite nouvelleUnite, EBelligerant nouveauPossesseur)
        {
            this.typeUnite = nouvelleUnite;
            this.possesseurUnite = nouveauPossesseur;

            this.TypeFutureUnite = nouvelleUnite;
            this.FuturPossesseurUnite = nouveauPossesseur;
        }

        /// <summary>
        /// Nettoie les champs correspondants à l'occupation future (unité, belligérant).
        /// </summary>
        public void NettoieChampsFuturs()
        {
            this.TypeFutureUnite = EUnite.Aucune;
            this.FuturPossesseurUnite = EBelligerant.Aucun;
        }

        /// <summary>
        /// Recrute dans la région.
        /// </summary>
        /// <param name="unite">Type d'unité à recruter.</param>
        /// <param name="belligerant">Belligérant pour lequel recruter.</param>
        public void Recrute(EUnite unite, EBelligerant belligerant)
        {
            this.typeUnite = unite;
            this.possesseurUnite = belligerant;
        }

        /// <summary>
        /// Congédie l'unité présente dans la région.
        /// </summary>
        public void Congedie()
        {
            this.typeUnite = EUnite.Aucune;
            this.possesseurUnite = EBelligerant.Aucun;
        }

        #endregion
    }
}
