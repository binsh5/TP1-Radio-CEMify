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
        public enum TypeUtilisateur
        {
            Invité,
            Étudiant
        }
        private string m_motDePasse;
        private string m_nomUtilisateur;
        private string m_statut;

        public string MotDePasse
        {
            get { return m_motDePasse; }
        }

        public string MotDePasseMasqué
        {
            get { return m_motDePasse; }
        }
        public string NomFichierListeLecture
        {
            get { }
        }
        public string NomUtilisateur
        {
            get { return m_nomUtilisateur; }
        }
        public string Statut
        {
            get { return m_statut; }
        }

        public void ModifierMDP(string nouveauMotDePasse)
        {
            m_motDePasse = nouveauMotDePasse;
        }

        public Utilisateur()
        {
            m_nomUtilisateur = "invité";
            m_motDePasse = null;
            m_statut = TypeUtilisateur.Invité.ToString();

        }
        public Utilisateur(string nom, string motDePasse)
        {
            m_motDePasse = motDePasse;
            m_nomUtilisateur = nom;
            m_statut = TypeUtilisateur.Étudiant.ToString();
        }
    }
}
