using System;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApplicationWithDummyAuthentication.Options;

namespace WebApplicationWithDummyAuthentication.Handlers
{
    public class TokenBasedAuthenticationHandler : AuthenticationHandler<TokenBasedAuthenticationOptions>
    {
        public IServiceProvider ServiceProvider { get; set; }

        public TokenBasedAuthenticationHandler(
            IOptionsMonitor<TokenBasedAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var headers = Request.Headers;
            string token = null;
            // var token = "X-Auth-Token".GetHeaderOrCookieValue(Request);

            if (string.IsNullOrEmpty(token))
            {
                return Task.FromResult(AuthenticateResult.Fail("Token is null or empty."));
            }

            return Task.FromResult(AuthenticateResult.Fail("Test fail message."));
        }
    }
}