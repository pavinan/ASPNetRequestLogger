using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Bharat.ASPNetRequestLogger
{
    public class TextLoggerModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(OnBeginRequest);
        }

        #endregion

        public void OnBeginRequest(object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            try
            {
                var logFilter = ConfigurationManager.AppSettings["asp_url_filter"] ?? Environment.GetEnvironmentVariable("asp_url_filter") ?? "";

                if (!string.IsNullOrWhiteSpace(logFilter))
                {
                    var regex = new Regex(logFilter, RegexOptions.IgnoreCase);

                    if (!regex.IsMatch(context.Request.Url.Authority))
                    {
                        return;
                    }
                }

                var md5 = new MD5CryptoServiceProvider();
                var domainHash = md5.ComputeHash(Encoding.UTF8.GetBytes(context.Request.Url.Authority.ToLower()));
                var domainUUID = string.Join("", domainHash.Select(x => x.ToString("x2")));
                md5.Dispose();

                var tempFolder = context.Server.MapPath("/ASP_LOGS/");

                Directory.CreateDirectory(tempFolder);

                var requestLogBuilder = new StringBuilder();

                requestLogBuilder.AppendLine("URL:");
                requestLogBuilder.AppendLine(context.Request.Url.ToString());
                requestLogBuilder.AppendLine();

                requestLogBuilder.AppendLine("Headers:");

                foreach (string headerKey in context.Request.Headers)
                {
                    requestLogBuilder.AppendLine($"{headerKey}: {context.Request.Headers[headerKey]}");
                }
                requestLogBuilder.AppendLine();

                requestLogBuilder.AppendLine("Body:");

                var bodyReader = new StreamReader(context.Request.InputStream);
                var bodyStr = bodyReader.ReadToEnd();
                requestLogBuilder.AppendLine(bodyStr);

                var logFileName = tempFolder + Guid.NewGuid().ToString("n") + ".txt";

                File.WriteAllText(logFileName, requestLogBuilder.ToString());
            }
            catch
            {

            }
            finally
            {
                context.Request.InputStream.Position = 0;
            }

        }
    }
}
