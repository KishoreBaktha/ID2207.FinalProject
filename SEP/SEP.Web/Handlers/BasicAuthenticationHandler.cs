//using System;
//using System.Net.Http.Headers;
//using System.Security.Claims;
//using System.Text;
//using System.Text.Encodings.Web;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Options;
//using SEP.Web.Models;
//using SEP.Web.Services;

//namespace SEP.Web.Handlers
//{
//	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
//    {
//		private readonly IEmployeeService _employeeService;

//        public BasicAuthenticationHandler(
//            IOptionsMonitor<AuthenticationSchemeOptions> options,
//            ILoggerFactory logger,
//            UrlEncoder encoder,
//            ISystemClock clock,
//			IEmployeeService employeeService)
//            : base(options, logger, encoder, clock)
//        {
//			_employeeService = employeeService;
//        }

//        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
//        {
//            if (!Request.Headers.ContainsKey("Authorization"))
//                return AuthenticateResult.Fail("Missing Authorization Header");

//            Employee user = null;
//            try
//            {
//                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
//                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
//                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
//                var username = credentials[0];
//                var password = credentials[1];
//				user = _employeeService.ValidateCredential(username, password);
//            }
//            catch
//            {
//                return AuthenticateResult.Fail("Invalid Authorization Header");
//            }

//            if (user == null)
//                return AuthenticateResult.Fail("Invalid Username or Password");

//            var claims = new[] {
//				new Claim(ClaimTypes.NameIdentifier, user.Username),
//                new Claim(ClaimTypes.Name, user.Username),
//            };
//            var identity = new ClaimsIdentity(claims, Scheme.Name);
//            var principal = new ClaimsPrincipal(identity);
//            var ticket = new AuthenticationTicket(principal, Scheme.Name);

//            return AuthenticateResult.Success(ticket);
//        }
//    }
//}
