using Microsoft.Extensions.Options;
using System;

namespace WebApp
{
    public class ConstantLoseOrWinService : ILooseOrWinService
    {
        readonly int _oneOutOf;

        public ConstantLoseOrWinService( IOptionsSnapshot<WinOrLooseOptions> options )
        {
            _oneOutOf = options.Value.OneOutOf;
        }

        public bool DecideWin()
        {
            return Environment.TickCount % _oneOutOf == 0;
        }
    }
}