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