using coxautoblogpostREAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace coxautoblogpostREAL.Controllers
{
    public class UserController : Controller
    {
        

        [HttpPost]
        public ActionResult Create(UserPost userPost)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "CAI-C1VK063";
            builder.InitialCatalog = "evieDB";
            builder.IntegratedSecurity = true;

            string connectionString = builder.ConnectionString;
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            int id = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT id FROM posts WHERE id = (SELECT max(id) FROM posts)";
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                string newid = "";
                while (reader.Read())
                {
                   
                     newid = reader[0].ToString();
                }
                connection.Close();
                id = int.Parse(newid);
                id = id + 1;
                query = "INSERT INTO posts (id, author, postText, title, date) VALUES (@Id, @Name, @Content, @Title,@Date)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", userPost.Name);
                command.Parameters.AddWithValue("@Content", userPost.Content);
                command.Parameters.AddWithValue("@Title", userPost.Title);
                command.Parameters.AddWithValue("@Date", date);
                

                connection.Open();
                command.ExecuteNonQuery();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
