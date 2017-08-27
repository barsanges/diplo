//-----------------------------------------------------------------------
// <rights file="Coordonnees.cs" author="Arnaud de LATOUR">
//     Propriété intellectuelle.
// </rights>
//-----------------------------------------------------------------------

namespace diplo.Geographie
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Coordonnées d'un point dans l'image de la carte.
    /// </summary>
    public class Coordonnees
    {
        #region Champs

        /// <summary>
        /// Coordonnée en x.
        /// </summary>
        private Int32 x;

        /// <summary>
        /// Coordonnée en y.
        /// </summary>
        private Int32 y;

        #endregion

        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe Coordonnees.
        /// </summary>
        /// <param name="x">Coordonnée en x.</param>
        /// <param name="y">Coordonnée en y.</param>
        public Coordonnees(Int32 x, Int32 y)
        {
            this.x = x;
            this.y = y;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Coordonnée en x.
        /// </summary>
        public Int32 X { get { return this.x; } }

        /// <summary>
        /// Coordonnée en y.
        /// </summary>
        public Int32 Y { get { return this.y; } }

        #endregion

        #region Méthodes publiques

        /// <summary>
        /// Convertit des coordonnées en chaîne de caractères.
        /// </summary>
        /// <returns>Coordonnées sous la forme d'une chaîne de caractères.</returns>
        public String Convertit()
        {
            String coordonneesConverties = String.Format("{0}:{1}", this.x, this.y);
            return coordonneesConverties;
        }

        #endregion

        #region Méthodes publiques statiques

        /// <summary>
        /// Convertit une chaîne de caractères en coordonnées.
        /// </summary>
        /// <param name="donnees">Chaîne de caractères à convertir.</param>
        /// <returns>Nouveau doublet de coordonnées.</returns>
        public static Coordonnees Convertit(String donnees)
        {
            String[] donneesDetaillees = donnees.Split(':');
            Int32 abscisse = Int32.Parse(donneesDetaillees[0]);
            Int32 ordonnee = Int32.Parse(donneesDetaillees[1]);

            Coordonnees nouvellesCoordonnees = new Coordonnees(abscisse, ordonnee);
            return nouvellesCoordonnees;
        }

        #endregion
    }
}