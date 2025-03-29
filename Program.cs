// See https://aka.ms/new-console-template for more information
using MySql.Data.MySqlClient;
using Projet_PSI_DELAROCHE_DEGARDIN_DARMON;

class Program
{
    static void Main(string[] args)
    {
        // ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
        // ⚙️ 1. Création du graphe de test
        var graphe = new Graphe<string>();

        var A = new Noeud<string>("A");
        var B = new Noeud<string>("B");
        var C = new Noeud<string>("C");
        var D = new Noeud<string>("D");

        graphe.AjouterNoeud(A);
        graphe.AjouterNoeud(B);
        graphe.AjouterNoeud(C);
        graphe.AjouterNoeud(D);

        graphe.AjouterLien(A, B);
        graphe.AjouterLien(A, C);
        graphe.AjouterLien(B, D);
        graphe.AjouterLien(C, D);

        // ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
        // 📋 2. Affichage des structures
        Console.WriteLine("===== Liste d'adjacence =====");
        graphe.AfficherListeAdjacence();

        Console.WriteLine("\n===== Matrice d'adjacence =====");
        graphe.AfficherMatriceAdjacence();

        // ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
        // 🔄 3. Parcours BFS
        Console.WriteLine("\n===== Parcours en largeur (BFS) depuis A =====");
        graphe.BFS(A);

        // 🌿 4. Parcours DFS
        Console.WriteLine("\n===== Parcours en profondeur (DFS) depuis A =====");
        graphe.DFS(A);

        // 5. Test Connexité
        Console.WriteLine("\n===== Test de connexité =====");
        bool connexe = graphe.EstConnexe();
        Console.WriteLine(connexe ? "Le graphe est connexe." : "Le graphe n'est pas connexe.");

        // 6. Test Cycle
        Console.WriteLine("\n===== Détection de cycle =====");
        bool aCycle = graphe.ContientCycle();
        Console.WriteLine(aCycle ? "Le graphe contient un cycle." : "Le graphe ne contient pas de cycle.");


        // 7. Test importation du .csv
        var grapheMetro = new Graphe<Station>();

        // Chemin vers le fichier Excel
        //string cheminFichier = "MetroParis.xlsx"; // à adapter si dans un dossier

        // Importation

        //LectureCSV.ChargerInfosCSV(cheminFichier, grapheMetro);

        //Console.WriteLine($"Nombre de stations : {grapheMetro.Noeuds.Count}");
        //Console.WriteLine($"Nombre de liaisons : {grapheMetro.Liens.Count}");

        // Exemple : afficher la liste d'adjacence
        //graphe.AfficherListeAdjacence();


        // Autre méthode via MySQL :

        string cheminSQL = "server=localhost;user=root;password=root;database=metro;";

        // Petit test :

        using var connection = new MySqlConnection(cheminSQL);

        try
        {
            connection.Open();
            Console.WriteLine("✅ Connexion réussie à MySQL !");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Échec de la connexion : " + ex.Message);
        }


         
        ImporteurMySQL.Charger(cheminSQL, grapheMetro);

        Console.WriteLine($"Stations : {grapheMetro.Noeuds.Count}");
        Console.WriteLine($"Liaisons : {grapheMetro.Liens.Count}");


    }
}
