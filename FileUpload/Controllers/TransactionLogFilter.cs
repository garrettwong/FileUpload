using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FileUpload.Controllers
{
    public class TransactionLogFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // pre-processing
            Debug.WriteLine("ACTION 1 DEBUG pre-processing logging");


            var request = actionContext.Request;
            if (request != null)
            {
                // log all this
                var httpContent = request.Content;
                var result = httpContent.ReadAsStringAsync().Result;

                // "OK"
                //var reasonPhrase = request.ReasonPhrase;

                //var httpRequestMessage = request.RequestMessage;

                //var requestUri = request.RequestMessage.RequestUri.AbsoluteUri;

                //// "POST"
                //var method = request.RequestMessage.Method.Method;

                //var isSuccessStatusCode = request.IsSuccessStatusCode;

                //// need headers/body
                //var headers = request.RequestMessage.Headers;
                //var authHeaders = headers.Authorization;

                //var headerKeys = headers.ToList();

                if (actionContext.ActionArguments != null && actionContext.ActionArguments.Count > 0)
                {
                    var postArgs = actionContext.ActionArguments.ToList();
                }



            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception != null)
            {
                LogException(actionExecutedContext.Request, actionExecutedContext.Exception);
                return;
            } 

            var objectContent = actionExecutedContext.Response.Content as ObjectContent;
            if (objectContent != null)
            {
                var type = objectContent.ObjectType; //type of the returned object
                var value = objectContent.Value; //holding the returned value
            }

            var response = actionExecutedContext.Response;
            if (response != null)
            {
                // log all this
                var httpContent = response.Content;

                // "OK"
                var reasonPhrase = response.ReasonPhrase;

                var httpRequestMessage = response.RequestMessage;

                var requestUri = response.RequestMessage.RequestUri.AbsoluteUri;

                // "POST"
                var method = response.RequestMessage.Method.Method;

                var isSuccessStatusCode = response.IsSuccessStatusCode;

                // need headers/body
                var headers = response.RequestMessage.Headers;
                var authHeaders = headers.Authorization;

                var headerKeys = headers.ToList();

                var content = response.RequestMessage.Content;
                var result = content.ReadAsStringAsync().Result;

                LogInfo(actionExecutedContext.Request, actionExecutedContext.Response);
            }

            Debug.WriteLine("ACTION 1 DEBUG  OnActionExecuted Response " + actionExecutedContext.Response.StatusCode.ToString());
        }

        public void LogInfo(HttpRequestMessage request, HttpResponseMessage response)
        {
            log4net.GlobalContext.Properties["LogFileName"] = @"C:\\Data\\file1";
            log4net.Config.XmlConfigurator.Configure();
            var log = log4net.LogManager.GetLogger(this.GetType());

            var sb = new StringBuilder();

            sb.Append($"{request.Method.Method},{request.RequestUri},{request.Headers.ToString()},{request.Content.ToString()}");
            sb.Append($"{response.RequestMessage.Method.Method},{response.RequestMessage.RequestUri},{response.RequestMessage.Headers.ToString()},{response.RequestMessage.Content.ToString()},{response.StatusCode.ToString()}");

            log.Info(sb.ToString());
        }
        public void LogException(HttpRequestMessage request, Exception exception)
        {
            log4net.GlobalContext.Properties["LogFileName"] = @"C:\\Data\\file1";
            log4net.Config.XmlConfigurator.Configure();
            var log = log4net.LogManager.GetLogger(this.GetType());

            var sb = new StringBuilder();

            sb.Append($"{request.Method.Method},{request.RequestUri},{request.Headers.ToString()},{request.Content.ToString()}");
            sb.Append("Exception has occurred: " + exception.ToString());

            log.Error(sb.ToString());
        }
    }
}