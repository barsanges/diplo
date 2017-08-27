//-----------------------------------------------------------------------
// <rights file="Ajuster.cs" author="Arnaud de LATOUR">
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
    /// Ordre d'ajustement (recrutement ou congédiement d'une unité).
    /// </summary>
    public class Ajuster
    {
        #region Constructeur

        /// <summary>
        /// Initialise une nouvelle instance de la classe Ajuster.
        /// </summary>
        /// <param name="recrutement">Type d'ajustement à effectuer.</param>
        /// <param name="unite">Type de l'unité concernée par l'ordre.</param>
        /// <param name="belligerant">Belligérant auquel appartient / va appartenir l'unité concernée.</param>
        /// <param name="nomRegion">Nom de la région concernée.</param>
        public Ajuster(EAjustement recrutement, EUnite unite, EBelligerant belligerant, String nomRegion)
        {
            this.Recrutement = recrutement;
            this.Region = nomRegion;
            this.Unite = unite;
            this.Belligerant = belligerant;
        }

        #endregion

        #region Propriétés

        /// <summary>
        /// Type d'ajustement à effectuer.
        /// </summary>
        public EAjustement Recrutement { get; private set; }

        /// <summary>
        /// Type de l'unité concernée par l'ordre.
        /// </summary>
        public EUnite Unite { get; private set; }

        /// <summary>
        /// Belligérant auquel appartient / va appartenir l'unité concernée.
        /// </summary>
        public EBelligerant Belligerant { get; private set; }

        /// <summary>
        /// Nom de la région concernée.
        /// </summary>
        public String Region { get; private set; }

        #endregion
    }
}