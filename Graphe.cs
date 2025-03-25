using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_PSI_DELAROCHE_DEGARDIN_DARMON
{
    internal class Graphe
    {
        private List<Noeud> noeuds = new List<Noeud>();
        private List<Lien> liens = new List<Lien>();
        private Dictionary<int, List<Noeud>> listeAdjacence = new();


        public List<Noeud> Noeuds { get { return noeuds; } }
        public List<Lien> Liens { get { return liens; } }
        public Dictionary<int, List<Noeud>> ListeAdjacence { get { return listeAdjacence; } }

        public int[,] MatriceAdjacence { get; set; }



        public void AjouterNoeud(Noeud n)
        {
            if (!Noeuds.Contains(n))
            {
                Noeuds.Add(n);
                ListeAdjacence[n.Id] = new List<Noeud>();
            }
        }

        
        public void AjouterLien(Noeud depart, Noeud arrivee)
        {
            Liens.Add(new Lien(depart, arrivee));
            ListeAdjacence[depart.Id].Add(arrivee);
            ListeAdjacence[arrivee.Id].Add(depart); //utile si graphe non orienté
        }


        public void AfficherListeAdjacence()
        {
            foreach (var pair in ListeAdjacence)
            {
                Console.Write(pair.Key + ": ");
                foreach (var voisin in pair.Value)
                {
                    Console.Write(voisin.Id + " ");
                }
                Console.WriteLine();
            }
        }



        public void ConstruireMatriceAdjacence()
        {
            int n = Noeuds.Count;
            MatriceAdjacence = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    MatriceAdjacence[i, j] = 0;
                }
            }

            foreach (var lien in Liens)
            {
                int i = Noeuds.IndexOf(lien.Depart);
                int j = Noeuds.IndexOf(lien.Arrivee);
                MatriceAdjacence[i, j] = lien.Poids;
                MatriceAdjacence[j, i] = lien.Poids; // toujours pour les graphes non orientés (sinon useless)
            }
        }



        public void AfficherMatriceAdjacence()
        {
            Console.WriteLine("Matrice d'adjacence :");
            Console.Write("   ");
            for (int i = 0; i < Noeuds.Count; i++)
                Console.Write(Noeuds[i].Id +" ");
            Console.WriteLine();

            for (int i = 0; i < Noeuds.Count; i++)
            {
                Console.Write(Noeuds[i].Id +" : ");
                for (int j = 0; j < Noeuds.Count; j++)
                {
                    Console.Write(MatriceAdjacence[i, j]+ " ");
                }
                Console.WriteLine();
            }
        }
    }
}
