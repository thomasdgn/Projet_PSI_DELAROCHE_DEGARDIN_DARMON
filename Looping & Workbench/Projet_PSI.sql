DROP DATABASE IF EXISTS livinparis;
CREATE DATABASE IF NOT EXISTS livinparis;
USE livinparis;

CREATE TABLE Utilisateur(
   IdUtilisateur VARCHAR(50),
   nom VARCHAR(50) NOT NULL,
   prenom VARCHAR(50) NOT NULL,
   adresse VARCHAR(50) NOT NULL,
   numero_de_telephone VARCHAR(50) NOT NULL,
   email VARCHAR(50) NOT NULL,
   mdp VARCHAR(50) NOT NULL,
   PRIMARY KEY(IdUtilisateur)
);

CREATE TABLE Recette(
   nomRecette VARCHAR(50),
   PRIMARY KEY(nomRecette)
);

CREATE TABLE Client(
   IdUtilisateur VARCHAR(50),
   PRIMARY KEY(IdUtilisateur),
   FOREIGN KEY(IdUtilisateur) REFERENCES Utilisateur(IdUtilisateur)
);

CREATE TABLE Cuisinier(
   IdUtilisateur VARCHAR(50),
   PRIMARY KEY(IdUtilisateur),
   FOREIGN KEY(IdUtilisateur) REFERENCES Utilisateur(IdUtilisateur)
);

CREATE TABLE Panier_de_commandes(
   IdPanier VARCHAR(50),
   IdUtilisateur VARCHAR(50) NOT NULL,
   PRIMARY KEY(IdPanier),
   FOREIGN KEY(IdUtilisateur) REFERENCES Client(IdUtilisateur)
);

CREATE TABLE Entreprise(
   IdUtilisateur VARCHAR(50),
   PRIMARY KEY(IdUtilisateur),
   FOREIGN KEY(IdUtilisateur) REFERENCES Client(IdUtilisateur)
);

CREATE TABLE Commande(
   IdCommande VARCHAR(50),
   adresse VARCHAR(50) NOT NULL,
   IdPanier VARCHAR(50) NOT NULL,
   PRIMARY KEY(IdCommande),
   FOREIGN KEY(IdPanier) REFERENCES Panier_de_commandes(IdPanier)
);

CREATE TABLE Plat(
   IdPlat VARCHAR(50),
   nomPlat VARCHAR(50) NOT NULL,
   nationnalite VARCHAR(50),
   type VARCHAR(50),
   nb INT NOT NULL,
   prix DOUBLE NOT NULL,
   photo VARCHAR(50) NOT NULL,
   regime_special VARCHAR(50),
   date_de_fabrication DATE NOT NULL,
   date_de_peremption DATE NOT NULL,
   nomRecette VARCHAR(50),
   IdCommande VARCHAR(50) NOT NULL,
   IdUtilisateur VARCHAR(50) NOT NULL,
   PRIMARY KEY(IdPlat),
   UNIQUE(photo),
   FOREIGN KEY(nomRecette) REFERENCES Recette(nomRecette),
   FOREIGN KEY(IdCommande) REFERENCES Commande(IdCommande),
   FOREIGN KEY(IdUtilisateur) REFERENCES Cuisinier(IdUtilisateur)
);

CREATE TABLE Ingrédients(
   nomIngrédient VARCHAR(50),
   IdPlat VARCHAR(50) NOT NULL,
   PRIMARY KEY(nomIngrédient),
   FOREIGN KEY(IdPlat) REFERENCES Plat(IdPlat)
);

CREATE TABLE Compose_de(
   nomIngrédient VARCHAR(50),
   nomRecette VARCHAR(50),
   PRIMARY KEY(nomIngrédient, nomRecette),
   FOREIGN KEY(nomIngrédient) REFERENCES Ingrédients(nomIngrédient),
   FOREIGN KEY(nomRecette) REFERENCES Recette(nomRecette)
);

CREATE TABLE Allergique(
   IdUtilisateur VARCHAR(50),
   nomIngrédient VARCHAR(50),
   PRIMARY KEY(IdUtilisateur, nomIngrédient),
   FOREIGN KEY(IdUtilisateur) REFERENCES Client(IdUtilisateur),
   FOREIGN KEY(nomIngrédient) REFERENCES Ingrédients(nomIngrédient)
);

INSERT INTO Utilisateur VALUES ("A2TDGDelaroche", "Delaroche", "Orane", "188 Rue Gallieni", "0749992897", "orane.delaroche@edu.devinci.fr", "root");
INSERT INTO Utilisateur VALUES ("A2TDGDarmon", "Darmon", "Clément", "12 Avenue Léonard de Vinci", "0695800686", "clement.darmon@edu.devinci.fr", "root");
INSERT INTO Utilisateur VALUES ("A2TDGDegardin", "Degardin", "Thomas", "12 Avenue Léonard de Vinci", "0649443259", "thomas.degardin@edu.devinci.fr", "root");

INSERT INTO Recette VALUES ("Pâtes aux fromages");

INSERT INTO Client VALUES ("A2TDGDelaroche");
INSERT INTO Client VALUES ("A2TDGDegardin");

INSERT INTO Cuisinier VALUES ("A2TDGDarmon");

INSERT INTO Panier_de_commandes VALUES ("Panier01", "A2TDGDelaroche");

INSERT INTO Entreprise VALUES ("A2TDGDegardin");

INSERT INTO Commande VALUES ("Commande01", "188 Rue Gallieni", "Panier01");

INSERT INTO Plat VALUES ("Plat01", "Pâtes", "italienne", "plat principal", 2, 10, "photo", "aucun régime spécial", '2025-02-27', '2025-02-28', "Pâtes aux fromages", "Commande01", "A2TDGDarmon");

INSERT INTO Ingrédients VALUES ("pâtes", "Plat01");
INSERT INTO Ingrédients VALUES ("fromage", "Plat01");
INSERT INTO Ingrédients VALUES ("gluten", "Plat01");
INSERT INTO Ingrédients VALUES ("lactose", "Plat01");

INSERT INTO Compose_de VALUES ("pâtes", "Pâtes aux fromages");
INSERT INTO Compose_de VALUES ("fromage", "Pâtes aux fromages");
INSERT INTO Compose_de VALUES ("gluten", "Pâtes aux fromages");
INSERT INTO Compose_de VALUES ("lactose", "Pâtes aux fromages");

INSERT INTO Allergique VALUES ("A2TDGDelaroche", "gluten");
INSERT INTO Allergique VALUES ("A2TDGDegardin", "lactose");

SHOW Tables; #Affiche toutes les tables

DESC Utilisateur; /* Donne la structure de la table Utilisateur */
SELECT * FROM Utilisateur; /* Affiche l'entiereté de la table Utilisateur */
SELECT COUNT(*) FROM Utilisateur; /* Donne le nombre d'utilisateurs inséré */

DESC Recette;
SELECT * FROM Recette;
SELECT COUNT(*) FROM Recette;

DESC Client;
SELECT * FROM Client;
SELECT COUNT(*) FROM Client;

DESC Cuisinier;
SELECT * FROM Cuisinier;
SELECT COUNT(*) FROM Cuisinier;

DESC Panier_de_commandes;
SELECT * FROM Panier_de_commandes;
SELECT COUNT(*) FROM Panier_de_commandes;

DESC Entreprise;
SELECT * FROM Entreprise;
SELECT COUNT(*) FROM Entreprise;

DESC Commande;
SELECT * FROM Commande;
SELECT COUNT(*) FROM Commande;

DESC Plat;
SELECT * FROM Plat;
SELECT COUNT(*) FROM Plat;

DESC Ingrédients;
SELECT * FROM Ingrédients;
SELECT COUNT(*) FROM Ingrédients;

DESC Compose_de;
SELECT * FROM Compose_de;
SELECT COUNT(*) FROM Compose_de;

DESC Allergique;
SELECT * FROM Allergique;
SELECT COUNT(*) FROM Allergique;

SELECT nom, prenom, adresse FROM Utilisateur WHERE adresse = "12 Avenue Léonard de Vinci"; /* Affichage de la liste des utilisateurs inscrits à une adresse donnée. */

SELECT * FROM Utilisateur WHERE nom = 'Darmon'; /* Affichage des données d'un utilisateur en particulier. */

SELECT * FROM Utilisateur WHERE mdp = 'root'; /* Affichage des données des utilisateurs ayant le même mot de passe. */

SELECT email FROM Utilisateur WHERE mdp = 'root'; /* Affichage des emails des utilisateurs ayant le même mot de passe. */

SELECT nom, prenom FROM Utilisateur WHERE mdp = 'root'
ORDER BY nom ASC; /* Affichage des noms et prénoms des utilisateurs ayant le même mot de passe, mais par ordre alphabétique. */

SELECT * FROM Utilisateur WHERE nom like 'De%'; /* Affichage des données des utilisateurs ayant un nom commençant par 'De'. */

SELECT nom, prenom FROM Utilisateur WHERE adresse like '%onar%'; /* Affichage des noms et prénoms des utilisateurs dont l'adresse contient les lettres 'onar'. */

SELECT nom, prenom FROM Utilisateur WHERE adresse like '12 Avenue Léonard de Vinci' and mdp = 'root'; /* Affichage des noms et prénoms des utilisateurs ayant telle adresse et tel mot de passe. */

SELECT nomPlat FROM Plat WHERE type = 'entrée'; /* Affichage des plats étant qualifié comme des entrées (soit aucun pour cet exemple-ci). */