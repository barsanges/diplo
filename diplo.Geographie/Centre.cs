//-----------------------------------------------------------------------
// <rights file="Centre.cs" author="Arnaud de LATOUR">
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
    /// Descriptif d'une région contenant un centre.
    /// </summary>
    /// <remarks>
    /// Cette classe est purement descriptive, et n'est
    /// pas modifiable en dehors de la construction.
    /// </remarks>
    public class Centre : Region
    {
        #region Champs

        /// <summary>
        /// Belligérant pouvant recruter dans ce centre.
        /// </summary>
        private EBelligerant recrutement;

        /// <summary>
        /// Coordonnées utilisées pour placer le centre dans la région.
        /// </summary>
        private Coordonnees coordonneesCentre;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe Centre.
        /// </summary>
        /// <param name="nom">Nom de la région.</param>
        /// <param name="abreviation">Abréviation du nom de la région.</param>
        /// <param name="typeRegion">Type de la région (côtière, intérieure ou maritime).</param>
        /// <param name="coordonneesUnite">Coordonnées utilisées pour placer une éventuelle unité dans la région.</param>
        /// <param name="coordonneesCentre">Coordonnées utilisées pour placer le centre dans la région.</param>
        /// <param name="recrutement">Belligérant pouvant recruter dans ce centre.</param>
        public Centre(
            String nom,
            String abreviation,
            ETypeRegion typeRegion,
            Coordonnees coordonneesUnite,
            Coordonnees coordonneesCentre,
            EBelligerant recrutement)
            : base(nom, abreviation, typeRegion, coordonneesUnite)
        {
            this.coordonneesCentre = coordonneesCentre;
            this.recrutement = recrutement;
        }

        #endregion

        #region Propriétés en lecture seule

        /// <summary>
        /// Indique si la région est un centre ou non.
        /// </summary>
        public override Boolean EstUnCentre { get { return true; } }

        /// <summary>
        /// Belligérant pouvant recruter dans ce centre.
        /// </summary>
        public EBelligerant Recrutement { get { return this.recrutement; } }

        /// <summary>
        /// Coordonnées utilisées pour placer le centre dans la région.
        /// </summary>
        public Coordonnees CoordonneesCentre { get { return this.coordonneesCentre; } }

        #endregion
    }
}
