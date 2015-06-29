using System;
using System.Collections.Specialized;
using System.Net;

namespace Common
{
    public class WebClientEx : WebClient
    {
        private static volatile WebClientEx instance;
        private static readonly object syncRoot = new Object();

        private WebClientEx() { }

        public CookieContainer CookieContainer
        {
            get { return _container; }
        }

        /// <summary>
        /// Need call Initialize static method before use
        /// </summary>
        public static WebClientEx Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new WebClientEx();
                    }
                }

                return instance;
            }
        }

        private static readonly CookieContainer _container = new CookieContainer();

        private static string _loginUrl;
        private static string _logOffUrl;
        private static string _getAntiForgeryTokenUrl;

        public static void Initialize(string loginUrl, string logOffUrl, string getAntiForgeryTokenUrl)
        {
            _loginUrl = loginUrl;
            _logOffUrl = logOffUrl;
            _getAntiForgeryTokenUrl = getAntiForgeryTokenUrl;
        }

        public bool Login(string user, string pass)
        {
            var data = new NameValueCollection();
            data["username"] = user;
            data["password"] = pass;
            data["__RequestVerificationToken"] = GetAntiForgeryToken();

            UploadValues(_loginUrl, "POST", data);

            return _container.Count == 2;
        }

        public void LogOff()
        {
            var data = new NameValueCollection();
            data["__RequestVerificationToken"] = GetAntiForgeryToken();

            UploadValues(_logOffUrl, "POST", data);
        }

        private string GetAntiForgeryToken()
        {
            var htmlToken = DownloadString(_getAntiForgeryTokenUrl);

            var token = htmlToken.Replace("<input name=\"__RequestVerificationToken\" type=\"hidden\" value=\"", "").Replace("\" />", "");

            return token;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest r = base.GetWebRequest(address);
            var request = r as HttpWebRequest;
            if (request != null)
            {
                request.CookieContainer = _container;
            }
            return r;
        }

        protected override WebResponse GetWebResponse(WebRequest request, IAsyncResult result)
        {
            WebResponse response = base.GetWebResponse(request, result);
            ReadCookies(response);
            return response;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            ReadCookies(response);
            return response;
        }

        private void ReadCookies(WebResponse r)
        {
            var response = r as HttpWebResponse;
            if (response != null)
            {
                CookieCollection cookies = response.Cookies;
                _container.Add(cookies);
            }
        }
    }
}
