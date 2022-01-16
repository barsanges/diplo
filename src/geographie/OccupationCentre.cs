//-----------------------------------------------------------------------
// <rights file="OccupationCentre.cs" author="Arnaud de LATOUR">
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
    /// Données concernant l'occupation d'un centre par une armée.
    /// </summary>
    public class OccupationCentre : OccupationRegion
    {
        #region Champs

        /// <summary>
        /// Nom de la puissance possédant le centre dans la région.
        /// </summary>
        private EBelligerant possesseurCentre;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe OccupationCentre.
        /// </summary>
        /// <param name="nomRegion">Nom de la région concernée.</param>
        public OccupationCentre(String nomRegion)
            : base(nomRegion)
        {
            this.possesseurCentre = EBelligerant.Aucun;
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe OccupationCentre.
        /// </summary>
        /// <param name="nomRegion">Nom de la région concernée.</param>
        /// <param name="possesseurCentre">Possesseur du centre dans la région.</param>
        public OccupationCentre(String nomRegion, EBelligerant possesseurCentre)
            : base(nomRegion)
        {
            this.possesseurCentre = possesseurCentre;
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe OccupationCentre.
        /// </summary>
        /// <param name="nomRegion">Nom de la région concernée.</param>
        /// <param name="typeUnite">Type de l'unité présente dans la région.</param>
        /// <param name="possesseurUnite">Possesseur de l'unité présente dans la région.</param>
        public OccupationCentre(String nomRegion, EUnite typeUnite, EBelligerant possesseurUnite)
            : base(nomRegion, typeUnite, possesseurUnite)
        {
            this.possesseurCentre = EBelligerant.Aucun;
        }

        /// <summary>
        /// Initialise une nouvelle instance de la classe OccupationCentre.
        /// </summary>
        /// <param name="nomRegion">Nom de la région concernée.</param>
        /// <param name="typeUnite">Type de l'unité présente dans la région.</param>
        /// <param name="possesseurUnite">Possesseur de l'unité présente dans la région.</param>
        /// <param name="possesseurCentre">Possesseur du centre dans la région.</param>
        public OccupationCentre(String nomRegion, EUnite typeUnite, EBelligerant possesseurUnite, EBelligerant possesseurCentre)
            : base(nomRegion, typeUnite, possesseurUnite)
        {
            this.possesseurCentre = possesseurCentre;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Nom de la puissance possédant le centre dans la région.
        /// </summary>
        public EBelligerant PossesseurCentre { get { return this.possesseurCentre; } }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Change l'allégeance du centre présent dans la région.
        /// </summary>
        /// <param name="nouveauPossesseur">Nom de la puissance à laquelle revient le centre.</param>
        public void ChangeAllegeanceCentre(EBelligerant nouveauPossesseur)
        {
            this.possesseurCentre = nouveauPossesseur;
        }

        #endregion
    }
}
