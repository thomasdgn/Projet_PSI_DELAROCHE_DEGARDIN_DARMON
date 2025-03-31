// See https://aka.ms/new-console-template for more information
using MySql.Data.MySqlClient;
using Projet_PSI_DELAROCHE_DEGARDIN_DARMON;
using static Projet_PSI_DELAROCHE_DEGARDIN_DARMON.Graphe<T>;

class Program
{
    static void Main(string[] args)
    {
        // Test importation du .csv :

        var grapheMetro = new Graphe<Station>();


        // Import données via MySQL :

        string cheminSQL = "server=localhost;user=root;password=root;database=metro;";

        ImporteurMySQL.Charger(cheminSQL, grapheMetro);

        Console.WriteLine("===== PLAN DU MÉTRO DE PARIS =====");

        while (true)
        {
            Console.WriteLine("\nMenu :");
            Console.WriteLine("1. Lister toutes les stations");
            Console.WriteLine("2. Rechercher une station");
            Console.WriteLine("3. Calculer un itinéraire (chemin le plus court)");
            Console.WriteLine("4. Quitter");
            Console.Write("Votre choix : ");
            var choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    ListerStations(grapheMetro);
                    break;
                case "2":
                    RechercherStation(grapheMetro);
                    break;
                case "3":
                    // CalculerItineraire(grapheMetro); (Clément doit s'en occuper)
                    break;
                case "4":
                    Console.WriteLine("À bientôt !");
                    return;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
        }


        // Début de l'interface graphique :

        static void ListerStations(Graphe<Station> graphe)
        {
            Console.WriteLine("\n--- Liste des stations ---");

            foreach (var noeud in graphe.Noeuds)
            {
                Console.WriteLine($"- {noeud.Valeur}");
            }
        }

        static void RechercherStation(Graphe<Station> graphe)
        {
            Console.Write("\nEntrez un mot-clé pour rechercher une station : ");
            string saisie = Console.ReadLine()?.ToLower();

            var resultats = graphe.Noeuds
                .Where(n => n.Valeur.Nom.ToLower().Contains(saisie))
                .Select(n => n.Valeur)
                .ToList();

            if (resultats.Count == 0)
            {
                Console.WriteLine("Aucune station trouvée.");
            }

            else
            {
                Console.WriteLine("Résultats :");

                foreach (var station in resultats)
                {
                    Console.WriteLine($"- {station}");
                }
            }
        }

        // Exécution de l'algorithme de Bellman-Ford

        string filePath = "MetroParis.xlsx"; // Remplace par le chemin complet si nécessaire
        string stationSource = "Nation";     // Station de départ

        var (edges, nameToId, idToName) = ExcelGraphLoader.LoadEdgesWithStationNames(filePath);

        if (!nameToId.TryGetValue(stationSource, out int sourceId))
        {
            Console.WriteLine($"⚠️ Station source inconnue : {stationSource}");
            return;
        }

        int nodeCount = nameToId.Count;

        if (BellmanFord.ComputeShortestPaths(nodeCount, edges, sourceId, out var distances))
        {
            Console.WriteLine($"\n🟩 Distances depuis la station '{stationSource}' :\n");

            for (int i = 0; i < nodeCount; i++)
            {
                string target = idToName[i];
                string distanceStr = distances[i] == int.MaxValue ? "∞" : $"{distances[i]} min";
                Console.WriteLine($"- {stationSource} → {target} : {distanceStr}");
            }
        }

        else
        {
            Console.WriteLine("❌ Un cycle de poids négatif a été détecté dans le graphe.");
        }
    }
}