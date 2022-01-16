//-----------------------------------------------------------------------
// <rights file="Region.cs" author="Arnaud de LATOUR">
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
    /// Descriptif d'une région.
    /// </summary>
    /// <remarks>
    /// Cette classe est purement descriptive, et n'est
    /// pas modifiable en dehors de la construction.
    /// </remarks>
    public class Region
    {
        #region Champs

        /// <summary>
        /// Nom de la région.
        /// </summary>
        private String nom;

        /// <summary>
        /// Abréviation du nom de la région.
        /// </summary>
        private String abreviation;

        /// <summary>
        /// Type de la région (côtière, intérieure ou maritime).
        /// </summary>
        private ETypeRegion typeRegion;

        /// <summary>
        /// Coordonnées utilisées pour placer une éventuelle unité dans la région.
        /// </summary>
        private Coordonnees coordonneesUnite;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe Region.
        /// </summary>
        /// <param name="nom">Nom de la région.</param>
        /// <param name="abreviation">Abréviation du nom de la région.</param>
        /// <param name="typeRegion">Type de la région (côtière, intérieure ou maritime).</param>
        /// <param name="coordonneesUnite">Coordonnées utilisées pour placer une éventuelle unité dans la région.</param>
        public Region(String nom, String abreviation, ETypeRegion typeRegion, Coordonnees coordonneesUnite)
        {
            this.nom = nom;
            this.abreviation = abreviation;
            this.typeRegion = typeRegion;
            this.coordonneesUnite = coordonneesUnite;
        }

        #endregion

        #region Propriétés en lecture seule

        /// <summary>
        /// Indique si la région est un centre ou non.
        /// </summary>
        public virtual Boolean EstUnCentre { get { return false; } }

        /// <summary>
        /// Nom de la région.
        /// </summary>
        public String Nom { get { return this.abreviation; } }

        /// <summary>
        /// Abréviation du nom de la région.
        /// </summary>
        public String Abreviation { get { return this.abreviation; } }

        /// <summary>
        /// Type de la région (côtière, intérieure ou maritime).
        /// </summary>
        public ETypeRegion TypeRegion { get { return this.typeRegion; } }

        /// <summary>
        /// Coordonnées utilisées pour placer une éventuelle unité dans la région.
        /// </summary>
        public Coordonnees CoordonneesUnite { get { return this.coordonneesUnite; } }

        #endregion
    }
}