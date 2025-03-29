using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ExcelDataReader;
using System.Globalization;

namespace Projet_PSI_DELAROCHE_DEGARDIN_DARMON
{
    internal class LectureCSV
    {
        public static List<Station> Stations = new();
        public static Dictionary<int, Noeud<Station>> Noeuds = new();

        
        public static void ChargerInfosCSV(string chemin, Graphe<Station> graphe)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using var stream = File.Open(chemin, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            var dataSet = reader.AsDataSet();


            var tableNoeuds = dataSet.Tables["Noeuds"];
            var tableArcs = dataSet.Tables["Arcs"];

            Dictionary<int, Noeud<Station>> noeuds = new();

            // --- Lecture des stations ---
            foreach (DataRow row in tableNoeuds.Rows.Cast<DataRow>().Skip(1))
            {
                if (!int.TryParse(row[0]?.ToString(), out int id)) continue;

                string ligne = row[3]?.ToString();
                string nom = row[4]?.ToString();
                if (!double.TryParse(row[5]?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lon)) continue;
                if (!double.TryParse(row[6]?.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out double lat)) continue;

                var station = new Station(id, nom, ligne, lat, lon);
                var noeud = new Noeud<Station>(station);

                noeuds[id] = noeud;
                graphe.AjouterNoeud(noeud);
            }

            // --- Lecture des liaisons ---
            foreach (DataRow row in tableArcs.Rows.Cast<DataRow>().Skip(1))
            {
                if (!int.TryParse(row["ID Station"]?.ToString(), out int idSource)) continue;

                if (int.TryParse(row["Suivant"]?.ToString(), out int idDest) && noeuds.ContainsKey(idSource) && noeuds.ContainsKey(idDest))
                {
                    int poids = TryGetTemps(row["Temps entre 2 stations"]?.ToString());
                    graphe.AjouterLien(noeuds[idSource], noeuds[idDest], poids);
                }

                if (int.TryParse(row["Précédent"]?.ToString(), out int idPred) && noeuds.ContainsKey(idPred) && noeuds.ContainsKey(idSource))
                {
                    int poids = TryGetTemps(row["Temps entre 2 stations"]?.ToString());
                    graphe.AjouterLien(noeuds[idPred], noeuds[idSource], poids);
                }

                // Cas du changement de ligne (correspondance)
                if (int.TryParse(row["Temps de Changement"]?.ToString(), out int changement) && changement > 0)
                {
                    graphe.AjouterLien(noeuds[idSource], noeuds[idSource], changement); // lien station vers elle-même
                }
            }
        }

        private static int TryGetTemps(string valeur)
        {
            return int.TryParse(valeur, out int res) ? res : 1;
        }
    }
}
