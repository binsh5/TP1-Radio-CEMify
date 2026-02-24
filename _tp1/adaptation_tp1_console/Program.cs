using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using static System.Net.Mime.MediaTypeNames;

namespace adaptation_tp1_console
{
    public class Program
    {

        #region TODO 00 : Prendre connaissance des constantes et variables fournies, ainsi que de la classe Morceau

        //------------ Constantes et variables --------------------------------
        const string BIBLIOTHEQUE = "../../../Bibliotheque.csv";

        // DÉCOMMENTER LA LIGNE SUIVANTE LORSQUE VOUS SEREZ À LA PARTIE 2
        //static Utilisateur utilisateurCourant = null;

        //---------------------------------------------------------------------
        #endregion


        static void Main(string[] args)
        {
            List<Morceau> repertoireRadioEtudiante = new List<Morceau>(); // Partie 1
            List<Morceau> listeLectureUtilisateur = new List<Morceau>(); // Partie 2


            #region TODO 11 : Chargements des listes à partir des fichiers CSV (5%)
            // TODO 11 : Appeler la méthode ChargerMorceaux() pour charger le répertoire musical de la radio étudiante, ainsi
            // que la liste de lecture de l'utilisateur SI APPROPRIÉ
            Utilisateur utilisateur = PageDémarrage();
            string pNomDuFichier = utilisateur.NomFichierListeLecture;
            List<Morceau> pListeMorceaux = new List<Morceau>();
            ChargerMorceaux(pNomDuFichier, pListeMorceaux);
            #endregion

            #region TODO 12 : Lier l'ensemble des fonctions (18%)
            /// TODO 12 :
            /// Effectuer la logique du menu utilisateur dans une boucle qui prend fin seulement lorsque l'utilisateur décide de quitter.
            /// Astuces : la boucle while et le switch-case vous seront utiles ;)
            bool a = true;
            int index;
            Console.Clear();
            while (a == true) // while pour que le menu sois en boucle tant que l'utilisateur n'a pas choisi de quitter
            {
                AfficherMenu(); // afficher le menu à chaque tour de boucle
                int.TryParse(Console.ReadLine(), out int option);
                switch (option) // switch pour que ce sois plus facile à lire et à faire les choix
                {
                    case 1: //aafficher la liste de morceaux
                        Console.Clear();
                        AfficherListeDeMorceaux(pListeMorceaux);
                        break;
                    case 2: // ajouter un morceau à la liste
                        Console.Clear();
                        Console.WriteLine("Enter les paramètres suivant:"); // demander les informations nécessaires pour ajouter un morceau à la liste
                        Console.WriteLine("Artiste :");
                        string artiste = Console.ReadLine();
                        Console.WriteLine("Album :");
                        string album = Console.ReadLine();
                        Console.WriteLine("Titre :");
                        string titre = Console.ReadLine();
                        Console.WriteLine("Cote sur 5:");
                        int.TryParse(Console.ReadLine(), out int cote);
                        Console.WriteLine("Durée en secondes");
                        int.TryParse(Console.ReadLine(), out int duree);
                        OpérationAjouter(pListeMorceaux, artiste, album, titre, cote, duree); // appeler la méthode pour ajouter un morceau à la liste
                        break;
                    case 3: // supprimer un morceau de la liste
                        Console.Clear();
                        Console.WriteLine("Enter la position du morceau à supprimer dans la liste:"); // demander l'index du morceau à supprimer
                        int.TryParse(Console.ReadLine(), out index);
                        OpérationSupprimer(pListeMorceaux, index); // appeler la méthode pour supprimer un morceau de la liste
                        break;
                    case 4: // modifier la cote d'un morceau de la liste
                        Console.Clear();
                        Console.WriteLine("Enter la position du morceau au quel vous voulez modifier dans la cote:"); // demander l'index du morceau à modifier
                        int.TryParse(Console.ReadLine(), out index);
                        Console.WriteLine("Enter la nouvelle cote sur 5 :");
                        int.TryParse(Console.ReadLine(), out cote);
                        OpérationModiferCote(pListeMorceaux, index, cote); // appeler la méthode pour modifier la cote d'un morceau de la liste
                        break;
                    case 5: // trier les morceaux de la liste en ordre décroissant de cote
                        Console.Clear();
                        OpérationTrier(pListeMorceaux);
                        Console.WriteLine("La liste a été trié en ordre décroissant de cote");
                        break;
                    case 6: // afficher les information du morceau courant
                        Console.Clear();
                        Console.WriteLine("Enter la position du morceau à afficher dans la liste:"); // demander l'index du morceau à afficher
                        int.TryParse(Console.ReadLine(), out index);
                        AfficherMorceauCourant(pListeMorceaux, index); // appeler la méthode pour afficher les informations d'un morceau de la liste
                        break;
                    case 7: // afficher les stats des morceaux de la liste (# des morceaux et durée total)
                        Console.Clear();
                        AfficherStats(pListeMorceaux); // appeler la méthode pour afficher les statistiques de la liste de morceaux
                        break;
                    case 8:
                        OpérationQuitter(pNomDuFichier, pListeMorceaux); // appeler la méthode pour enregistrer les modifications au moment de quitter
                        a = false;
                        break;
                    default: Console.WriteLine("option invalide"); // si l'utilisateur entre une option invalide, on affiche un message d'erreur et on recommence la boucle
                        break;
                }
            }
            #endregion

        }

        #region Partie 1 (~65%)

        #region TODO 01 : Charger le répertoire musical de la radio étudiante (7%)
        /// Nom de la méthode : ChargerMorceaux()
        /// <summary>
        /// Lit les informations dans le fichier CSV et les charge en mémoire dans une liste.
        /// </summary>
        /// <param name="pNomDuFichier"> nom du fichier à lire</param>
        /// <param name="pListeMorceaux"> liste de morceaux à remplir </param>
        /// <returns>  liste de morceaux. Remplie si le fichier existe, vide s'il n'existe pas  </returns>
        /// ----------------------------------------------------------------------------------------------------
        internal static List<Morceau> ChargerMorceaux(string pNomDuFichier, List<Morceau> pListeMorceaux)
        {
            List<string> pListe = new List<string>();
            if (!File.Exists(BIBLIOTHEQUE))
            {
                
            }

            using (StreamReader reader = new StreamReader(BIBLIOTHEQUE))
            {
                string ligne;
                while (!reader.EndOfStream)
                {
                    ligne = reader.ReadLine();
                    string[] info = ligne.Split('|');
                    string[] duretemp = info[4].Split(":");
                    int dure = 60 * int.Parse(duretemp[0]) * 60 + int.Parse(duretemp[1]);
                    Morceau morceau = new Morceau(info[0], info[1], info[2], int.Parse(info[3]), dure);
                    pListeMorceaux.Add(morceau);
                }
            }
            return pListeMorceaux;
        }
        #endregion

        #region TODO 02 : Afficher le répertoire musical (4%)
        /// Nom de la méthode : AfficherListeDeMorceaux()
        /// <summary>
        /// Affiche la version lisible des morceaux d'une liste dans la console.
        /// </summary>
        /// <param name="pListeMorceaux"> liste de morceaux </param>
        /// ----------------------------------------------------------------------------------------
        static void AfficherListeDeMorceaux(List<Morceau> pListeMorceaux)
        {
            for (int i = 0; i < pListeMorceaux.Count; i++)
            {
                Console.WriteLine(pListeMorceaux[i].ToString());
            }
        }
        #endregion

        #region TODO 03 : Ajouter un morceau au répertoire (3%)
        /// Nom de la méthode : OpérationAjouter()
        /// <summary>
        /// Permet d'ajouter un nouvel objet Morceau à une liste de morceaux
        /// </summary>
        /// <param name="pListeMorceaux"> liste de morceaux </param>
        /// <param name="pArtiste"> nom de l'artiste </param>
        /// <param name="pAlbum"> titre de l'album </param>
        /// <param name="pTitre"> titre du morceau </param>
        /// <param name="pCote"> cote d'appréciation (1 à 5) </param>
        /// <param name="pDurée"> durée du morceau en secondes </param>
        /// <returns>  liste de morceaux contenant une chanson additionnelle  </returns>
        /// ----------------------------------------------------------------------------------------
        internal static List<Morceau> OpérationAjouter(List<Morceau> pListeMorceaux, string pArtiste, string pAlbum, string pTitre, int pCote, int pDurée)
        {
            Morceau morceau = new Morceau(pArtiste, pAlbum, pTitre, pCote, pDurée);
            pListeMorceaux.Add(morceau);
            return pListeMorceaux;
        }
        #endregion

        #region TODO 04 : Supprimer un morceau du répertoire (2%)
        /// Nom de la méthode : OpérationSupprimer()
        /// <summary>
        /// Permet de supprimer un objet Morceau d'une liste de morceaux
        /// </summary>
        /// <param name="pListeMorceaux"> liste de morceaux </param>
        /// <param name="pIndexMorceau"> index du morceau à supprimer dans la liste </param>
        /// <returns>  liste de morceaux contenant une chanson en moins  </returns>
        /// ----------------------------------------------------------------------------------------
        internal static List<Morceau> OpérationSupprimer(List<Morceau> pListeMorceaux, int pIndexMorceau)
        {
            pListeMorceaux.RemoveAt(pIndexMorceau);
            return pListeMorceaux;
        }
        #endregion

        #region TODO 05 : Modifier la cote d'appréciation d'un morceau (2%)
        /// Nom de la méthode : OpérationModiferCote()
        /// <summary>
        /// Permet de modifier la cote d'un morceau d'une liste.
        /// </summary>
        /// <param name="pListeMorceaux"> liste de morceaux </param>
        /// <param name="pIndexMorceau"> index du morceau à supprimer dans la liste </param>
        /// <param name="pCote"> valeur de la nouvelle cote </param>
        /// <returns>  liste de morceaux modifiée  </returns>
        /// ----------------------------------------------------------------------------------------
        internal static List<Morceau> OpérationModiferCote(List<Morceau> pListeMorceaux, int pIndexMorceau, int pCote)
        {
            string pArtiste = pListeMorceaux[pIndexMorceau].Artiste;
            string pAlbum = pListeMorceaux[pIndexMorceau].Album;
            string pTitre = pListeMorceaux[pIndexMorceau].Titre;
            int pDuré = pListeMorceaux[pIndexMorceau].Durée;
            Morceau morceau = new Morceau(pArtiste, pAlbum, pTitre, pCote, pDuré);
            pListeMorceaux[pIndexMorceau] = morceau;
            return pListeMorceaux;
        }
        #endregion

        #region TODO 06 : Trier les morceaux du répertoire selon leur cote d'appréciation (10%)
        /// Nom de la méthode : OpérationTrier()
        /// <summary>
        /// Permet de trier en ordre DÉCROISSANT les morceaux d'une liste selon leur cote d'appréciation.
        /// </summary>
        /// <param name="pListeMorceaux"> liste de morceaux </param>
        /// <returns>  liste de morceaux triée  </returns>
        /// ----------------------------------------------------------------------------------------
        internal static List<Morceau> OpérationTrier(List<Morceau> pListeMorceaux) // a refaire
        {
            for (int i = 0; i < pListeMorceaux.Count; i++)
            {
                for (int j = 1; j < pListeMorceaux.Count; j++)
                {
                    if (pListeMorceaux[i].Cote > pListeMorceaux[j].Cote)
                    {
                        string pArtiste = pListeMorceaux[i].Artiste;
                        string pAlbum = pListeMorceaux[i].Album;
                        string pTitre = pListeMorceaux[i].Titre;
                        int pCote = pListeMorceaux[i].Cote;
                        int pDuré = pListeMorceaux[i].Durée;
                        Morceau morceau = new Morceau(pArtiste, pAlbum, pTitre, pCote, pDuré);
                        pListeMorceaux[i] = pListeMorceaux[j];
                        pListeMorceaux[j] = morceau;
                    }
                }
            }
            return pListeMorceaux;
        }
        #endregion

        #region TODO 07 : Afficher les informations d'un morceau (3%)
        /// Nom de la méthode : AfficherMorceauCourant()
        /// <summary>
        /// Permet d'afficher l'ensemble des informations d'un morceau (i.e. titre du morceau,
        /// nom de l'artiste, titre de l'album, cote d'appréciation, durée)
        /// </summary>
        /// <param name="pListeMorceaux"> liste de morceaux </param>
        /// <param name="pIndexMorceau"> index du morceau sélectionné </param>
        /// ----------------------------------------------------------------------------------------
        static void AfficherMorceauCourant(List<Morceau> pListeMorceaux, int pIndexMorceau)
        {
            string pArtiste = pListeMorceaux[pIndexMorceau].Artiste;
            string pAlbum = pListeMorceaux[pIndexMorceau].Album;
            string pTitre = pListeMorceaux[pIndexMorceau].Titre;
            int pCote = pListeMorceaux[pIndexMorceau].Cote;
            int pDuré = pListeMorceaux[pIndexMorceau].Durée;
            Console.WriteLine($"{pTitre} de l'album {pAlbum} par {pArtiste}. Ce morceaux a une cote de {pCote} et est d'une durée de {pDuré} seconde");
        }
        #endregion

        #region TODO 08 : Afficher les statistiques du répertoire (5%)
        /// Nom de la méthode : AfficherStats()
        /// <summary>
        /// Permet d'afficher les statistiques (# de chansons, durée totale en hh:mm:ss) d'une liste de morceaux
        /// </summary>
        /// <param name="pListeMorceaux"> liste de morceaux </param>
        /// ----------------------------------------------------------------------------------------
        static void AfficherStats(List<Morceau> pListeMorceaux)
        {
            int temp = 0;
            for (int i = 0; i < pListeMorceaux.Count; i++)
            {
                Console.WriteLine($"#{i}. {pListeMorceaux[i].Titre}");
                temp += pListeMorceaux[i].Durée;
            }
            int h = temp / 3600;
            int min = (temp % 3600) / 60;
            int sec = temp % 60;
            Console.WriteLine($"{h}:{min}:{sec}");
        }
        #endregion

        #region TODO 09 : Enregistrer les modifications au moment de quitter (5%)
        /// Nom de la méthode : OpérationQuitter()
        /// <summary>
        /// Au moment de quitter le programme, on sauvegarde la liste dans le fichier approprié.
        /// On sépare chaque information d'un morceau avec le séparateur "|".
        /// La durée d'un morceau doit être affichée dans le format mm:ss.
        /// Voici un exemple 
        ///  artiste|album|titre|cote|minutes:secondes
        ///  Susan Boyle|I Dreamed A Dream|Wild Horses|1|04:53
        /// </summary>
        /// <param name="pNomDuFichier"> nom du fichier dans lequel sauvegarder les informations </param>
        /// <param name="pListeMorceaux"> liste de morceaux à sauvegarder </param>
        /// -----------------------------------------------------------------------------------------------
        static void OpérationQuitter(string pNomDuFichier, List<Morceau> pListeMorceaux)
        {
            for (int i = 0; i < pListeMorceaux.Count; i++)
            {
                string pArtiste = pListeMorceaux[i].Artiste;
                string pAlbum = pListeMorceaux[i].Album;
                string pTitre = pListeMorceaux[i].Titre;
                int pCote = pListeMorceaux[i].Cote;
                int pDuré = pListeMorceaux[i].Durée;
                int min = pDuré / 60;
                string temps = $"{min}:{(pDuré - (min * 60))}";
                using (StreamWriter writer = new StreamWriter(BIBLIOTHEQUE, false))
                {
                    writer.WriteLine($"{pArtiste}|{pAlbum}|{pTitre}|{pCote}|{temps}");
                }
            }
        }
        #endregion

        #region TODO 10 : Afficher le menu utilisateur dans la console (4%)
        /// Nom de la méthode : AfficherMenu()
        /// <summary>
        /// Permet d'afficher les options du menu dans la console
        /// </summary>
        /// -----------------------------------------------------------------------------------------------
        static void AfficherMenu()
        {
            Console.WriteLine("===Menu===");
            Console.WriteLine();
            Console.WriteLine("1. Afficher la liste de morceaux");
            Console.WriteLine("2. Ajouter un morceaux à la liste");
            Console.WriteLine("3. Supprimer un morceaux de la liste");
            Console.WriteLine("4. Modifer la cote d'un morceaux de la liste");
            Console.WriteLine("5. Trier les morceaux de la liste en order décroissant de cote");
            Console.WriteLine("6. Afficher les information du morceau courant");
            Console.WriteLine("7. Afficher les stats des morceaux de la liste (# des morceaux et durée total)");
            Console.WriteLine("8. Quitter");
        }

        #endregion



        #endregion Partie 1

        #region Partie 2 (~35%)

        // TODO 13 : créer une classe Utilisateur à l'aide du diagramme de classe fournit (23%)

        #region TODO 14 : Page de connexion (7%)
        /// Nom de la méthode : PageDémarrage()
        /// <summary>
        /// Permet d'afficher les options de connexions au démarrage : "se créer un compte" et "continuer en tant qu'invité"
        /// </summary>
        /// -----------------------------------------------------------------------------------------------
        static Utilisateur PageDémarrage()
        {
            bool a = false;
            Utilisateur utilisateur = new Utilisateur();
            while (a == false) // while pour que ce sois en boucle tant que l'utilisateur n'a pas fait un choix valide
            {
                Console.WriteLine("===Bienvenue à la radio étudiante===");
                Console.WriteLine();
                Console.WriteLine("1. Se créer un compte");
                Console.WriteLine("2. Continuer en tant qu'invité");
                char.TryParse(Console.ReadLine(), out char choix);
                switch (choix) // switch pour que ce sois plus facile à lire et à faire les choix
                {
                    case '1':
                        bool b = false;
                        while (b == false) // while pour que ce sois en boucle tant que l'utilisateur n'a pas entré des informations valides
                        {
                            Console.Write("Entrer un nom d'utilisateur : ");
                            string nom = Console.ReadLine();
                            Console.WriteLine("Enter un mot de passe : ");
                            string mdp = Console.ReadLine();
                            Console.WriteLine("Confirmer le mot de passe : ");
                            string verif = Console.ReadLine();
                            if (mdp == verif) // if pour vérifier que le mot de passe et la vérification sont les mêmes
                            {
                                Console.WriteLine("Compte créé");
                                Utilisateur utilisateurConnecter = new Utilisateur(nom, mdp);
                                b = true;
                                utilisateur = utilisateurConnecter;
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Mot de passe invalide");
                            }
                        }
                        a = true;
                        break;
                    case '2': // si l'utilisateur choisit de continuer en tant qu'invité, on crée un utilisateur avec les informations par défaut
                        Utilisateur invite = new Utilisateur();
                        a = true;
                        utilisateur = invite;
                        break;
                    default: // si l'utilisateur entre un choix invalide, on affiche un message d'erreur et on recommence la boucle
                        Console.WriteLine("Choix invalide.");
                        break;
                }
            }
            return utilisateur; // on retourne l'utilisateur qui a été créé ou invité pour que les autres méthodes puissent l'utiliser
        }
        #endregion

        #region TODO 15 : Afficher le profil d'un utilisateur connecté (2%)
        /// Nom de la méthode : AfficherProfil()
        /// <summary>
        /// Permet d'afficher les informations d'un utilisateur : son nom d'utilisateur, son mot de passe caché par des *, son statut 
        /// </summary>
        /// -----------------------------------------------------------------------------------------------
        static void AfficherProfil(Utilisateur utilisateur)
        {
            string nom = utilisateur.NomUtilisateur; // on récupère les informations de l'utilisateur à afficher
            string mdp = utilisateur.MotDePasseMasqué;
            string statut = utilisateur.Statut.ToString();
            Console.WriteLine($"Nom d'utilisateur : {nom}");
            Console.WriteLine($"Mot de passe : {mdp}");
            Console.WriteLine($"Statut : {statut}");
        }
        #endregion



        #endregion Partie 2

    }
}
