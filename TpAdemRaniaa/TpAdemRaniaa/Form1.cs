using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TpAdemRaniaa
{
    public partial class Form1 : Form
    {
        // Declaration du DataSet
        private DataSet dataSet;
        private SqlDataAdapter dataAdapterEtudiants;
        private SqlDataAdapter dataAdapterCours;
        private SqlDataAdapter dataAdapterInscriptions;

        public Form1()
        {
            InitializeComponent();

            SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder();
            cs.DataSource = "(local)";
            cs.InitialCatalog = "Ecole";
            cs.UserID = "sa";
            cs.Password = "sysadm";

            SqlConnection con = new SqlConnection();
            con.ConnectionString = cs.ConnectionString;
            // Initialisation du DataSet
            dataSet = new DataSet();

            // DataAdapter pour les etudiants
            dataAdapterEtudiants = new SqlDataAdapter("SELECT * FROM Etudiant", con);
            SqlCommandBuilder builderEtudiants = new SqlCommandBuilder(dataAdapterEtudiants);

            // DataAdapter pour les cours
            dataAdapterCours = new SqlDataAdapter("SELECT * FROM Cours", con);
            SqlCommandBuilder builderCours = new SqlCommandBuilder(dataAdapterCours);

            // DataAdapter pour les inscriptions
            dataAdapterInscriptions = new SqlDataAdapter("SELECT * FROM Inscreption", con);
            SqlCommandBuilder builderInscriptions = new SqlCommandBuilder(dataAdapterInscriptions);

            // Remplir les tables du DataSet
            dataAdapterEtudiants.Fill(dataSet, "Etudiant");
            dataAdapterCours.Fill(dataSet, "Cours");
            dataAdapterInscriptions.Fill(dataSet, "Inscreption");

            // Associer les DataGridView aux tables
            dataGridView1.DataSource = dataSet.Tables["Etudiant"];
            dataGridView2.DataSource = dataSet.Tables["Cours"];
            dataGridView3.DataSource = dataSet.Tables["Inscreption"];
        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            // Verifier la selection dans le ComboBox
            string selection = comboBox1.SelectedItem.ToString();

            if (selection == "Etudiant")
            {
                // Validation de l'entree de donnees pour les etudiants
                if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Le nom et le prenom sont obligatoires pour un etudiant.");
                    return;
                }

                if (!int.TryParse(textBox4.Text, out int note))
                {
                    MessageBox.Show("La note doit etre un entier.");
                    return;
                }

                // Creation d'une nouvelle ligne pour la table des etudiants
                DataRow newRow = dataSet.Tables["Etudiant"].NewRow();
                newRow["ID"] = textBox9.Text;
                newRow["Nom"] = textBox1.Text;        // Nom de l'etudiant
                newRow["Prenom"] = textBox2.Text;     // Prenom de l'etudiant
                newRow["Matricule"] = textBox3.Text;  // Matricule de l'etudiant
                newRow["Note"] = note;                // Note de l'etudiant

                // Ajout de la nouvelle ligne dans le DataSet
                dataSet.Tables["Etudiant"].Rows.Add(newRow);

                // Mise a jour du DataGridView
                dataGridView1.DataSource = dataSet.Tables["Etudiant"];

                MessageBox.Show("L'etudiant a ete ajoute.");
            }
            else if (selection == "Cours")
            {
                // Validation de l'entree de donnees pour les cours
                if (string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox7.Text))
                {
                    MessageBox.Show("Le nom et la description du cours sont obligatoires.");
                    return;
                }

                if (!int.TryParse(textBox6.Text, out int nbHeures))
                {
                    MessageBox.Show("Le nombre d'heures doit etre un entier.");
                    return;
                }

                // Creation d'une nouvelle ligne pour la table des cours
                DataRow newRow = dataSet.Tables["Cours"].NewRow();
                newRow["Id"] = textBox5.Text;
                newRow["Nom"] = textBox8.Text;           // Nom du cours
                newRow["Description"] = textBox7.Text;   // Description du cours
                newRow["Nombre heures"] = nbHeures;       // Nombre d'heures

                // Ajout de la nouvelle ligne dans le DataSet
                dataSet.Tables["Cours"].Rows.Add(newRow);

                // Mise a jour du DataGridView
                dataGridView2.DataSource = dataSet.Tables["Cours"];

                MessageBox.Show("Le cours a ete ajoute.");
            }
            else if (selection == "Inscreption")
            {
                // Validation de l'entree de donnees pour les inscriptions
                if (string.IsNullOrWhiteSpace(textBox11.Text) || string.IsNullOrWhiteSpace(textBox12.Text))
                {
                    MessageBox.Show("Les ID etudiant et ID cours sont obligatoires.");
                    return;
                }

                // Verifier que la date
                if (!DateTime.TryParse(textBox10.Text, out DateTime dateInscription))
                {
                    MessageBox.Show("La date d'inscription n'est pas valide.");
                    return;
                }

                // Creation d'une nouvelle ligne pour la table des inscriptions
                DataRow newRow = dataSet.Tables["Inscreption"].NewRow();
                newRow["Id"] = textBox13.Text;
                newRow["Id_etudiant"] = textBox12.Text;   // ID de l'etudiant
                newRow["Id_Cour"] = textBox11.Text;      // ID du cours
                newRow["Date_inscreption"] = dateInscription;  // Date d'inscription

                // Ajout de la nouvelle ligne dans le DataSet
                dataSet.Tables["Inscreption"].Rows.Add(newRow);

                // Mise a jour du DataGridView
                dataGridView3.DataSource = dataSet.Tables["Inscreption"];

                MessageBox.Show("L'inscription a ete ajoutee.");
            }
            else
            {
                MessageBox.Show("Veuillez selectionner une option valide.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Verifier la selection dans le ComboBox 
            string selection = comboBox1.SelectedItem.ToString();
            DataGridView dgv = null;

            try
            {
                if (selection == "Etudiant")
                {
                    dgv = dataGridView1;
                }
                else if (selection == "Cours")
                {
                    dgv = dataGridView2;
                }
                else if (selection == "Inscreption")
                {
                    dgv = dataGridView3;
                }
                else
                {
                    MessageBox.Show("Veuillez selectionner une option valide.");
                    return;
                }

                // Verifier si une ligne est selectionnee dans le DataGridView
                if (dgv.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez selectionner une ligne a modifier.");
                    return;
                }

                // Selectionner la ligne a modifier
                DataGridViewRow selectedRow = dgv.SelectedRows[0];
                DataRow dataRow = ((DataRowView)selectedRow.DataBoundItem).Row;

                // Modifier les donnees en fonction de la table selectionnee
                if (selection == "Etudiant")
                {
                    dataRow["Nom"] = textBox1.Text;
                    dataRow["Prenom"] = textBox2.Text;
                    dataRow["Matricule"] = textBox3.Text;

                    if (int.TryParse(textBox4.Text, out int note))
                    {
                        dataRow["Note"] = note;
                    }
                    else
                    {
                        MessageBox.Show("La note doit etre un entier.");
                        return;
                    }

                    dataAdapterEtudiants.Update(dataSet, "Etudiant");
                    MessageBox.Show("L'etudiant a ete modifie.");
                }
                else if (selection == "Cours")
                {
                    dataRow["Nom"] = textBox8.Text;
                    dataRow["Description"] = textBox7.Text;

                    if (int.TryParse(textBox6.Text, out int nbHeures))
                    {
                        dataRow["Nombre heures"] = nbHeures;
                    }
                    else
                    {
                        MessageBox.Show("Le nombre d'heures doit etre un entier.");
                        return;
                    }

                    dataAdapterCours.Update(dataSet, "Cours");
                    MessageBox.Show("Le cours a ete modifie.");
                }
                else if (selection == "Inscreption")
                {
                    dataRow["Id_etudiant"] = textBox12.Text;
                    dataRow["Id_Cour"] = textBox11.Text;

                    if (DateTime.TryParse(textBox10.Text, out DateTime dateInscription))
                    {
                        dataRow["Date_inscreption"] = dateInscription;
                    }
                    else
                    {
                        MessageBox.Show("La date d'inscription n'est pas valide.");
                        return;
                    }

                    dataAdapterInscriptions.Update(dataSet, "Inscreption");
                    MessageBox.Show("L'inscription a ete modifiee.");
                }

                // Rafraichir le DataGridView apres modification
                dgv.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Verifier la selection dans le ComboBox
            string selection = comboBox1.SelectedItem.ToString();

            try
            {
                if (selection == "Etudiant")
                {
                    // Mise a jour de la base de donnees pour les etudiants
                    dataAdapterEtudiants.Update(dataSet, "Etudiant");
                    MessageBox.Show("Les modifications des etudiants ont ete enregistrees dans la base de donnees.");
                }
                else if (selection == "Cours")
                {
                    // Mise a jour de la base de donnees pour les cours
                    dataAdapterCours.Update(dataSet, "Cours");
                    MessageBox.Show("Les modifications des cours ont ete enregistrees dans la base de donnees.");
                }
                else if (selection == "Inscreption")
                {
                    // Mise a jour de la base de donnees pour les inscriptions
                    dataAdapterInscriptions.Update(dataSet, "Inscreption");
                    MessageBox.Show("Les modifications des inscriptions ont ete enregistrees dans la base de donnees.");
                }
                else
                {
                    MessageBox.Show("Veuillez selectionner une option valide.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'enregistrement : " + ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Verifier la selection dans le ComboBox
            string selection = comboBox1.SelectedItem.ToString();
            DataGridView dgv = null;

            try
            {
                if (selection == "Etudiant")
                {
                    dgv = dataGridView1;
                }
                else if (selection == "Cours")
                {
                    dgv = dataGridView2;
                }
                else if (selection == "Inscreption")
                {
                    dgv = dataGridView3;
                }
                else
                {
                    MessageBox.Show("Veuillez selectionner une option valide.");
                    return;
                }

                // Verifier si une ligne est selectionnee dans le DataGridView
                if (dgv.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez selectionner une ligne a supprimer.");
                    return;
                }

                // Selectionner la ligne a supprimer
                DataGridViewRow selectedRow = dgv.SelectedRows[0];
                DataRow dataRow = ((DataRowView)selectedRow.DataBoundItem).Row;

                // Supprimer la ligne du DataSet
                dataRow.Delete();

                // Mettre a jour le DataGridView
                dgv.Refresh();

                // Mettre a jour la base de donnees
                if (selection == "Etudiant")
                {
                    dataAdapterEtudiants.Update(dataSet, "Etudiant");
                    MessageBox.Show("L'etudiant a ete supprime.");
                }
                else if (selection == "Cours")
                {
                    dataAdapterCours.Update(dataSet, "Cours");
                    MessageBox.Show("Le cours a ete supprime.");
                }
                else if (selection == "Inscreption")
                {
                    dataAdapterInscriptions.Update(dataSet, "Inscreption");
                    MessageBox.Show("L'inscription a ete supprimee.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la suppression : " + ex.Message);
            }
        }
    }
}
