using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static adaptation_tp1_console.Utilisateur;

namespace adaptation_tp1_console
{
    internal class Utilisateur
    {
        public enum TypeUtilisateur //les type d'utilisateur possible
        {
            Invité,
            Étudiant
        }

        private string m_motDePasse; //declaration des champs de la classe
        private string m_nomUtilisateur;
        private TypeUtilisateur m_statut;

        public int MotDePasse { get; private set; }

        public string MotDePasseMasqué //propriété qui retourne une chaîne de caractères composée d'astérisques de la même longueur que le mot de passe réel
        {
            get { return new string('*', m_motDePasse.Length); }
        }
        public string NomFichierListeLecture //propriété qui retourne une chaîne de caractères formée du nom d'utilisateur suivi de "_ListeLecture.csv"
        {
            get
            {
                    return $"{m_nomUtilisateur}_ListeLecture.csv";
            }
        }
        public string NomUtilisateur //propriété qui retourne le nom d'utilisateur
        {
            get { return m_nomUtilisateur; }
        }
        public TypeUtilisateur Statut
        {
            get { return m_statut; }
        }

        public void ModifierMDP(string nouveauMotDePasse) //méthode qui modifie le mot de passe de l'utilisateur
        {
            m_motDePasse = nouveauMotDePasse;
        }

        public Utilisateur() //constructeur par défaut qui initialise les champs de l'utilisateur invité
        {
            m_nomUtilisateur = "invité";
            m_motDePasse = null;
            m_statut = TypeUtilisateur.Invité;

        }
        public Utilisateur(string nom, string motDePasse) //constructeur qui initialise les champs de l'utilisateur étudiant avec les valeurs passées en paramètre
        {
            m_motDePasse = motDePasse;
            m_nomUtilisateur = nom;
            m_statut = TypeUtilisateur.Étudiant;
        }
    }
}
