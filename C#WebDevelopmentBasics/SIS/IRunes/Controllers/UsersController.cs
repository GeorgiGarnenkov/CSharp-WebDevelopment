using System.Linq;
using IRunes.Contracts;
using IRunes.Data.Models;
using IRunes.Services;
using SIS.HTTP.Cookies;
using SIS.HTTP.Requests;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses;
using SIS.HTTP.Responses.Contracts;
using SIS.WebServer.Results;

namespace IRunes.Controllers
{
    public class UsersController : BaseController
    {
        IHashService hashService;

        public UsersController()
        {
            this.hashService = new HashService();
        }

        public IHttpResponse Login(IHttpRequest request)
        {
            var username = this.GetUsername(request);

            if (username != null)
            {
                this.ViewBag["username"] = username;
                return this.View("home/index");
            }

            return this.View();
        }

        public IHttpResponse LoginPost(IHttpRequest request)
        {
            string username = request.FormData["username"].ToString();
            string password = request.FormData["password"].ToString();

            string hashedPassword = this.hashService.Hash(password);

            var user = db.Users.FirstOrDefault(x => x.Username == username && x.Password == hashedPassword);

            if (user == null)
            {
                return this.BadRequestError("Invalid username or password!", "users/login");
            }

            request.Session.AddParameter("username", username);

            var userCookieValue = this.userCookieService.GetUserCookie(username);

            this.ViewBag["username"] = username;

            this.IsUserAuthenticated = true;

            var response = this.View("home/index");

            response.Cookies.Add(new HttpCookie("IRunes_auth", userCookieValue));

            return response;
        }

        public IHttpResponse Logout(IHttpRequest request)
        {
            var username = this.GetUsername(request);

            if (username == null)
            {
                return this.View("home/index");
            }

            var response = new RedirectResult("/");

            var cookie = request.Cookies.GetCookie("IRunes_auth");

            cookie.Delete();

            response.Cookies.Add(cookie);

            return response;
        }

        public IHttpResponse Register(IHttpRequest request)
        {
            return this.View();
        }

        public IHttpResponse RegisterPost(IHttpRequest request)
        {
            string username = request.FormData["username"].ToString();
            string password = request.FormData["password"].ToString();
            string confirmPassword = request.FormData["confirm-password"].ToString();
            string email = request.FormData["email"].ToString();

            if (string.IsNullOrWhiteSpace(username) || username.Length < 6)
            {
                return this.BadRequestError("Username should be 6 or more characters long!", "users/register");
            }

            if (this.db.Users.Any(x => x.Username == username))
            {
                return this.BadRequestError("Username already exist!", "users/register");
            }

            if (password != confirmPassword)
            {
                return this.BadRequestError("Confirm password does not match password!", "users/register");
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            {
                return this.BadRequestError("Password should be 6 or more characters long!", "users/register");
            }

            string hashedPassword = this.hashService.Hash(password);

            User user = new User
            {
                Username = username,
                Password = hashedPassword,
                Email = email
            };

            db.Users.Add(user);
            db.SaveChanges();

            return this.View("users/login");
        }
    }
}