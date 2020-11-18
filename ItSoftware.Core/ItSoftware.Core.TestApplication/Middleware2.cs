/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItSoftware.Core.TestApplication
{
	using ItSoftware.Core.HttpHost;
	using Microsoft.Owin;
	using AppFunc = Func<IDictionary<string, object>, Task>;
	public class Middleware2 : ItsMiddleware
	{
		public override async Task InvokeDown( IOwinContext context )
		{			
			await base.WriteResponseStringAsync( context, "<h1>Middleware 2 - Down</h1>" );			
		}

		public override async Task InvokeUp( IOwinContext context )
		{
			await base.WriteResponseStringAsync( context, "<h1>Middleware 2 - Up</h1>" );
		}
	}
}
*/