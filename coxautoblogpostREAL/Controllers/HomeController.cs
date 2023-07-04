using coxautoblogpostREAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace coxautoblogpostREAL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult UserPost(UserPost model)
        {
            return View();
        }

        public ActionResult ViewPosts(UserPost model)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "CAI-C1VK063";
            builder.InitialCatalog = "evieDB";
            builder.IntegratedSecurity = true;

            string connectionString = builder.ConnectionString;
            List<UserPost> posts = new List<UserPost>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT author, title, postText FROM posts";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserPost post = new UserPost
                    {
                        Name = reader.GetString(0),
                        Title = reader.GetString(1),
                        Content = reader.GetString(2)
                    };

                    posts.Add(post);
                }

                reader.Close();
            }

            return View(posts);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




    }
}