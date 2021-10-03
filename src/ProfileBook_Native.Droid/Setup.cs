using Microsoft.Extensions.Logging;
using MvvmCross.Platforms.Android.Core;
using ProfileBook_Native.Core;
using Serilog;
using Serilog.Extensions.Logging;

namespace ProfileBook_Native.Droid
{
    public class Setup : MvxAndroidSetup<App>
    {
        #region -- Overrides --

        protected override ILoggerProvider CreateLogProvider() => new SerilogLoggerProvider();

        protected override ILoggerFactory CreateLogFactory()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.AndroidLog()
                .CreateLogger();

            return new SerilogLoggerFactory();
        }

        #endregion
    }
}
