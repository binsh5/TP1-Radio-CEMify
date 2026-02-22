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
        private TypeUtilisateur m_statut;

        public int MotDePasse { get; private set; }

        public string MotDePasseMasqué
        {
            get { return new string('*', m_motDePasse.Length); }
        }
        public string NomFichierListeLecture
        {
            get
            {
                    return $"{m_nomUtilisateur}_ListeLecture.csv";
            }
        }
        public string NomUtilisateur
        {
            get { return m_nomUtilisateur; }
        }
        public TypeUtilisateur Statut
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
            m_statut = TypeUtilisateur.Invité;

        }
        public Utilisateur(string nom, string motDePasse)
        {
            m_motDePasse = motDePasse;
            m_nomUtilisateur = nom;
            m_statut = TypeUtilisateur.Étudiant;
        }
    }
}
