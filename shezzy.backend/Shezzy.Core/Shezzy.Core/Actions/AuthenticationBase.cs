using Microsoft.AspNetCore.Authentication;
using System.Text.Encodings.Web;

namespace Shezzy.Core.Actions
{
	public class AuthenticationBase
	{
		public async Task HandleOnRemoteFailure(RemoteFailureContext context)
		{
			context.Response.StatusCode = 500;
			context.Response.ContentType = "text/html";

			await context.Response.WriteAsync("<html><body>");
			await context.Response.WriteAsync($"A remote failure has occurred: <br>" +
				context?.Failure?.Message.Split(Environment.NewLine)
				.Select(s => $"{HtmlEncoder.Default.Encode(s)}<br>")
				.Aggregate((s1, s2) => $"{s1} {s2}"));

			if (context?.Properties != null)
			{
				await context.Response.WriteAsync("Properties:<br>");

				//context.Properties.Items.ToList().ForEach(async pair =>
				//{
				//	await context.Response.WriteAsync($"-{HtmlEncoder.Default.Encode(pair.Key)}={HtmlEncoder.Default.Encode(pair.Value ?? "")}<br>");
				//});
				foreach (var pair in context.Properties.Items)
				{
					await context.Response.WriteAsync($"-{HtmlEncoder.Default.Encode(pair.Key)}={HtmlEncoder.Default.Encode(pair.Value ?? "")}<br>");
				}
			}

			if (context != null)
			{
				await context.Response.WriteAsync("<a href=\"/\">Home</a>");
				await context.Response.WriteAsync("</body></html>");
			}

			// context.Response.Redirect("/error?FailureMessage=" + UrlEncoder.Default.Encode(context.Failure.Message));

			context?.HandleResponse();
		}
	}
}
