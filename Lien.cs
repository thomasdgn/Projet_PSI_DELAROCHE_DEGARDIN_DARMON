using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_PSI_DELAROCHE_DEGARDIN_DARMON
{
    internal class Lien
    {
        private Noeud depart;
        private Noeud arrivee;
        private int poids = 1;

        public Noeud Depart { get { return depart; } }
        public Noeud Arrivee { get { return arrivee; } }
        public int Poids { get { return poids; } }



        public Lien(Noeud depart, Noeud arrivee, int poids = 1)
        {
            this.depart = depart;
            this.arrivee = arrivee;
            this.poids = poids;
        }


    }
}
