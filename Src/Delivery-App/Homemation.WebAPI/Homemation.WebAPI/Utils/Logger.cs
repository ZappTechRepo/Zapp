using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homemation.WebAPI.Utils
{
    public class Logger
    {
        private static Object _lock = new Object();

        private static String FormatException( Exception ex )
        {
            return String.Format( "\r\n===================================================================================================\r\nError Message : {0}\r\n\r\n Stack Trace : {1}\r\n\r\n Target Site : {2}\r\n===================================================================================================\r\n", ex.Message, ex.StackTrace, ex.TargetSite, ex.InnerException );
        }

        internal static void LogException( object exception )
        {
            throw new NotImplementedException();
        }

        public static void Write( String message )
        {
            Write( message, System.Configuration.ConfigurationManager.AppSettings[ "LogPath" ] + "/RunnerLog_" + DateTime.Now.ToString( "yyyyMMdd" ) + ".log" );
        }

        public static void Write( String message, String filePath )
        {
            lock ( _lock )
            {
                string path = filePath.Replace( "\\", "/" );
                path = path.Substring( 0, path.LastIndexOf( "/" ) );
                if ( !System.IO.Directory.Exists( path ) )
                    System.IO.Directory.CreateDirectory( path );
                using ( System.IO.StreamWriter sw = new System.IO.StreamWriter( filePath, true ) )
                {
                    sw.WriteLine( String.Format( "{0}\t:\t{1}", DateTime.Now, message ) );
                    sw.Flush();
                    sw.Close();
                }
            }
        }

        public static void LogException( Exception ex )
        {
            Write( FormatException( ex ), System.Configuration.ConfigurationManager.AppSettings[ "LogPath" ] + "/WebLog_" + DateTime.Now.ToString( "yyyyMMdd" ) + ".log" );
        }
        public static void LogException( Exception ex, String filePath )
        {
            Write( FormatException( ex ), filePath );
        }
    }
}