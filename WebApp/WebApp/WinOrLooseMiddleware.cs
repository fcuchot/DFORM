using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class WinOrLooseMiddleware
    {
        readonly RequestDelegate _next;

        public WinOrLooseMiddleware( RequestDelegate next )
        {
            _next = next;
        }

        public async Task Invoke( HttpContext context, ILooseOrWinService s )
        {
            if( s.DecideWin() )
            {
                await context.Response.WriteAsync( "OK, you win! => " );
                await _next( context );
            }
            else
            {
                await context.Response.WriteAsync( "LOOSE!" );
            }
        }
    }

}
