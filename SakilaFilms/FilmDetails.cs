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
    public partial class FilmDetails : Form
    {
        Film _selectedFilm;
        private String connectionString =
            "Server = localhost; Database=sakila; Uid=client; Pwd=$3cr3t3t;";
        public FilmDetails(Film selectedFilm)
        {
            InitializeComponent();
            _selectedFilm = selectedFilm;
            List<Film> films = new List<Film>();
            MySqlConnection connection = new MySqlConnection(connectionString);
            string sql = $"SELECT title, description, rating as 'rating' FROM film WHERE " +
                $"film_id = {selectedFilm.film_id}";
            films = connection.Query<Film>(sql).ToList();
            Film book = films.FirstOrDefault();

            filmLabel.Text = selectedFilm.title;
            descriptionListBox.Text = selectedFilm.description;
            ratingLabel.Text = selectedFilm.rating;
        }
    }
}
