using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_PSI_DELAROCHE_DEGARDIN_DARMON
{
    internal class Noeud
    {
        private int id;
        public int Id { get { return id; } }

        public Noeud(int id)
        {
            this.id = id;
        }
    }
}
