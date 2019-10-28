using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Dapper;

namespace SakilaFilms
{
    public partial class MainForm : Form
    {
        private String logginText;
        private String connectionString =
            "Server = localhost; Database=sakila; Uid=staff; Pwd=$up3r$3cr3t;";
        public MainForm()
        {
            InitializeComponent();
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            List<Film> films = new List<Film>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string sql = $"SELECT title FROM film WHERE " +
                $"title LIKE '%{searchTextBox.Text}%'";

            films = connection.Query<Film>(sql).ToList();
            filmsListBox.DataSource = films;
            filmsListBox.DisplayMember = "title";
            connection.Close();
        }

        private void filmsListBox_DoubleClick(object sender, EventArgs e)
        {
            Film selectedFilm = filmsListBox.SelectedItem as Film;
            FilmDetails detailsForm = new FilmDetails(selectedFilm);

            DialogResult result = detailsForm.ShowDialog(this);
            if (DialogResult == DialogResult.OK)
            {

            }
            else
            {

            }
            detailsForm.Dispose();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            logginText = "";
            logginText = loginButton.Text;
            if (logginText.Equals("Log in"))
            {
                if (userTextBox.Text.Equals("guest"))
                {
                    if (passwordTextBox.Text.Equals("£123"))
                    {
                        loginButton.Text = "Log out";
                        label2.Enabled = true;
                        searchTextBox.Enabled = true;
                        findButton.Enabled = true;
                        filmsListBox.Enabled = true;

                    }
                }
            }
            if (logginText.Equals("Log out"))
            {
                loginButton.Text = "Log in";
                label2.Enabled = false;
                searchTextBox.Enabled = false;
                findButton.Enabled = false;
                filmsListBox.Enabled = false;
                filmsListBox.DataSource = null;
                filmsListBox.Items.Clear();
                searchTextBox.Text = string.Empty;
                userTextBox.Text = string.Empty;
                passwordTextBox.Text = string.Empty;
            }
        }

        private void updateEmailButton_Click(object sender, EventArgs e)
        {
            MySqlConnection cnn = new MySqlConnection(connectionString);
            string sql = $"UPDATE staff SET email = '%{emailTextBox.Text}%' WHERE " +
                $"username = {userTextBox}";

            try
            {
                var rowsAffected = cnn.Execute(sql);
                if (rowsAffected == 1)
                {
                    MessageBox.Show("Your email adress has been updated!", "Sakila Staff", MessageBoxButtons.OK);
                    emailTextBox.Text = string.Empty;
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException exception)
            {
                Console.WriteLine("Warning: Exception thrown " + exception.Message);
            }

            cnn.Close();        
        }
    }
}

