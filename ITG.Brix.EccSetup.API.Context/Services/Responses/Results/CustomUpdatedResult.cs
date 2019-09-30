using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.API.Context.Services.Responses.Results
{
    public class CustomUpdatedResult : NoContentResult
    {
        public CustomUpdatedResult(string eTagValue)
            : base()
        {
            ETagValue = eTagValue;
        }

        public string ETagValue { get; set; }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            SetETagHeader(context);

            return base.ExecuteResultAsync(context);
        }

        public override void ExecuteResult(ActionContext context)
        {
            SetETagHeader(context);

            base.ExecuteResult(context);
        }

        private void SetETagHeader(ActionContext context)
        {
            if (!context.HttpContext.Response.Headers.ContainsKey("ETag"))
            {
                context.HttpContext.Response.Headers.Add("ETag", "\"" + ETagValue + "\"");
            }
            else
            {
                context.HttpContext.Response.Headers["ETag"] = "\"" + ETagValue + "\"";
            }

            if (!context.HttpContext.Response.Headers.ContainsKey("Access-Control-Expose-Headers"))
            {
                context.HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "Content-Length, ETag");
            }
        }
    }
}
