//-----------------------------------------------------------------------
// <rights file="Program.cs" author="Arnaud de LATOUR">
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
    /// Point d'entrée utilisé pour tester les méthodes du projet diplo.Geographie.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Fonction de test.
        /// </summary>
        static void Main()
        {
            /// Test de l'assistant de construction de matrice d'adjacence :
            String fichierAdjacenceParcimonieux = @"C:\Users\Arnaud\Documents\diplo\Cartes & parties\Essai.txt";
            AssistantConstructionMatriceAdjacence.RemplitNouveauFichier(fichierAdjacenceParcimonieux);
        }
    }
}