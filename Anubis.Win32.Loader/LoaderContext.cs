using Anubis.Network.Packetize.Authorize;
using Anubis.System;
using Anubis.Win32.Loader.Environment;
using Anubis.Win32.Loader.Hardware;
using Anubis.Win32.Loader.Network;
using Anubis.Win32.Loader.Security;
using Anubis.Win32.Loader.Terminal;

using System;

namespace Anubis.Win32.Loader
{
    public static class LoaderContext
    {
        private static bool m_Builded = false;

        public static void Build()
        {
            ExObject.Instantiate<HardwareExObject>();
            ExObject.Instantiate<NetworkExObject>();
            ExObject.Instantiate<TerminalExObject>();
            ExObject.Instantiate<SecurityExObject>();
            ExObject.Instantiate<EnvExObject>();

            m_Builded = true;
        }
        public static void Execute()
        {
            if(!m_Builded)
            {
                return;
            }

            var terminal = ExObject.FindObjectOfType<TerminalExObject>();
            var network = ExObject.FindObjectOfType<NetworkExObject>();
            var hardware = ExObject.FindObjectOfType<HardwareExObject>();
            var security = ExObject.FindObjectOfType<SecurityExObject>();
            var env = ExObject.FindObjectOfType<EnvExObject>();

            terminal.Write().InfoCL( "Preparing PC... " );
            if ( env.Prepare() )
            {
                terminal.Write().Success( "Done" );
                terminal.Write().InfoCL( "Establishing connection... " );
                if ( network.Connect() )
                {
                    terminal.Write().Success( "Connected" );
                    terminal.Write().Info( "Put your key: " );
                    var key = terminal.Read().GetLineAs<string>();
                    var locale = hardware.GetTwoLetterLocaleCode();
                    var hardwareId = hardware.GetHardwareIdentifier();

                    terminal.Write().InfoCL( "Sending request... " );

                    var response = network.SendAsync<AuthorizeNetworkEntity>(new AuthorizeNetworkEntity()
                    {
                        Query = security.ParseRegKey( $"{key}:{locale}:{hardwareId}" )
                    });

                    if(response != null)
                    {
                        terminal.Write().Success( "Ok" );

                        if ( response.Seconds > 0 )
                        {
                            terminal.Write().InfoCL( "Bind security... " );
                            if ( env.Execute( response.Link ) )
                            {
                                terminal.Write().Success( "Done" );
                            }
                            else
                            {
                                terminal.Write().Error( "Can't bind security" );
                            }
                        }
                        else
                        {
                            terminal.Write().Error( response.ResultMessage );
                        }
                    }
                    else
                    {
                        terminal.Write().Error( "Connection error. Please try again later" );
                    }
                }
            }
            else
            {
                terminal.Write().Error( "Can't prepare PC. Please contact with administrator" );
            }

            Console.ReadKey();
            env.Shutdown();
        }

        private static void HandleException(Exception ex)
        {

        }
    }
}
