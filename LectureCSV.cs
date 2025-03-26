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

            // --- Lecture des stations ---
            foreach (DataRow row in tableNoeuds.Rows.Cast<DataRow>().Skip(1))
            {
                if (string.IsNullOrWhiteSpace(row[0]?.ToString())) continue;

                int id = int.Parse(row[0].ToString());
                string ligne = row[3].ToString();
                string nom = row[4].ToString();
                string valeurLongitude = row[5]?.ToString()?.Trim();
                if (!double.TryParse(valeurLongitude, NumberStyles.Float, CultureInfo.InvariantCulture, out double longitude))
                {
                    Console.WriteLine($"Erreur de conversion longitude à la ligne : {valeurLongitude}");
                    continue; // on saute cette station
                }
                string valeurLatitude = row[6]?.ToString()?.Trim();
                if (!double.TryParse(valeurLatitude, NumberStyles.Float, CultureInfo.InvariantCulture, out double latitude))
                {
                    Console.WriteLine($"Erreur de conversion latitude à la ligne : {valeurLatitude}");
                    continue;
                }


                var station = new Station(id, nom, ligne, longitude, latitude);
                Stations.Add(station);

                var noeud = new Noeud<Station>(station);
                Noeuds[id] = noeud;
                graphe.AjouterNoeud(noeud);
            }

            // --- Lecture des liaisons ---
            foreach (DataRow row in tableArcs.Rows.Cast<DataRow>().Skip(1))
            {
                if (!int.TryParse(row["Station Id"]?.ToString(), out int idStation)) continue;

                // Précédent
                if (int.TryParse(row["Précédent"]?.ToString(), out int precedentId))
                {
                    double poids = double.TryParse(row["Temps entre 2 stations"]?.ToString(), out double t1) ? t1 : 1;
                    graphe.AjouterLien(Noeuds[precedentId], Noeuds[idStation], (int)poids);
                }

                // Suivant
                if (int.TryParse(row["Suivant"]?.ToString(), out int suivantId))
                {
                    double poids = double.TryParse(row["Temps entre 2 stations"]?.ToString(), out double t2) ? t2 : 1;
                    graphe.AjouterLien(Noeuds[idStation], Noeuds[suivantId], (int)poids);
                }
            }
        }
    }
}
