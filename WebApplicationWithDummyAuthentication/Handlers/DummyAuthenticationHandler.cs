using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using WebApplicationWithDummyAuthentication.Options;

namespace WebApplicationWithDummyAuthentication.Handlers
{
    public class DummyAuthenticationHandler : AuthenticationHandler<DummyAuthenticationOptions>
    {
        public DummyAuthenticationHandler(
            IOptionsMonitor<DummyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            StringValues authHeader;
            bool isSuccessful = Request.Headers.TryGetValue("Custom-Auth-Handler", out authHeader);

            if (!isSuccessful)
            {
                return Task.FromResult(AuthenticateResult.Fail("The request doesn't have a special custom authentication header."));
            }

            // Create a ClaimsPrincipal from your header.
            // Claim is a key-value pair. We can use FirstName or EmailAddress as a key.
            // User has the role X means that we have one claim. We have predefined set of claim types from System.Security.Claims.ClaimTypes,
            // but you can create your own claim.

            // The next important thing is Identity. You can think of the Identity as about a document which
            // has a list of claims and an authentication type. In Core MVC we use System.Security.Claims.ClaimsIdentity.

            // The next important thing which is located above the Indentity is Principal.
            // You can think about the Principal as about a user itself.
            // The Principal can contain more than one Identity associated with a user.
            // You can retrieve all claims of all identities by using the Principal.
            // If you have more than one identity then you can manage a user access to different parts
            // of a web application.
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "My Name")
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, Scheme.Name));
            var ticket = new AuthenticationTicket(claimsPrincipal, new AuthenticationProperties { IsPersistent = false }, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}