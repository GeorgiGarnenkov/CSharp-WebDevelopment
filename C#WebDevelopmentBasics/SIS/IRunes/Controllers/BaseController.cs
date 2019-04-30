using IRunes.Data;
using IRunes.Services;
using IRunes.Services.Contracts;
using SIS.HTTP.Enums;
using SIS.WebServer.Results;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using SIS.HTTP.Requests.Contracts;
using SIS.HTTP.Responses.Contracts;

namespace IRunes.Controllers
{
    public abstract class BaseController
    {
        private const string VIEWS_FOLDER_PATH = "../../../views";
        private const string FILE_EXTENTION = ".html";
        private const string DEFAULT_CONTROLER_NAME = "Controller";
        private const string LAYOUT = "_Layout";
        private const string ERROR_VIEW_PATH = "error/error";

        protected IRunesDbContext db;

        protected BaseController()
        {
            this.db = new IRunesDbContext();
            this.userCookieService = new UserCookieService();
            this.ViewBag = new Dictionary<string, string>();
        }

        private string GetCurrentControllerName
        {
            get { return this.GetType().Name.Replace(DEFAULT_CONTROLER_NAME, string.Empty); }
        }

        protected bool IsUserAuthenticated { get; set; } = false;

        protected IUserCookieService userCookieService { get; }

        protected Dictionary<string, string> ViewBag { get; set; }
        

        protected IHttpResponse View(string viewName = null)
        {
            string bodyContent = GetViewContent(viewName);

            this.SetViewBagParameters(bodyContent);

            string fullViewContent = GetViewContent(LAYOUT);

            return new HtmlResult(HttpResponseStatusCode.Ok, fullViewContent);
        }

        protected IHttpResponse BadRequestError(string massage = "Page Not Found", string currentViewPath = "home/index")
        {
            this.ViewBag["errorMassage"] = massage;

            StringBuilder bodyContent = new StringBuilder();
            bodyContent.Append(GetViewContent(ERROR_VIEW_PATH));
            bodyContent.Append(GetViewContent(currentViewPath));

            this.SetViewBagParameters(bodyContent.ToString());

            string fullViewContent = GetViewContent(LAYOUT);

            return new HtmlResult(HttpResponseStatusCode.BadRequest, fullViewContent);
        }

        private void SetViewBagParameters(string bodyContent)
        {
            this.ViewBag["body"] = bodyContent;
            this.ViewBag["NonAuthenticated"] = "";
            this.ViewBag["Authenticated"] = "";

            if (IsUserAuthenticated)
            {
                this.ViewBag["NonAuthenticated"] = "d-none";
            }
            else
            {
                this.ViewBag["Authenticated"] = "d-none";
            }
        }

        protected string GetViewContent(string viewName)
        {
            string controlerName = GetCurrentControllerName;
            string actionName = new StackFrame(2).GetMethod().Name;

            string fullPath = $"{VIEWS_FOLDER_PATH}/{viewName}{FILE_EXTENTION}";

            if (viewName == null)
            {
                fullPath = $"{VIEWS_FOLDER_PATH}/{controlerName}/{actionName}{FILE_EXTENTION}";
            }

            var fileContent = File.ReadAllText(fullPath);

            foreach (var viewBagKey in ViewBag.Keys)
            {
                string placeHolder = $"{{{{{viewBagKey}}}}}";

                if (fileContent.Contains(viewBagKey))
                {
                    fileContent = fileContent.Replace(placeHolder, this.ViewBag[viewBagKey]);
                }
            }

            return fileContent;
        }

        protected string GetUsername(IHttpRequest request)
        {
            if (!request.Cookies.ContainsCookie("IRunes_auth"))
            {
                return null;
            }

            this.IsUserAuthenticated = true;

            var cookie = request.Cookies.GetCookie("IRunes_auth");
            var cookieContent = cookie.Value;
            var userName = this.userCookieService.GetUserData(cookieContent);
            return userName;
        }
    }
}