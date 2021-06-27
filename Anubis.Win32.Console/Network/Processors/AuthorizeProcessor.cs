using Anubis.Network;
using Anubis.Network.Packetize.Authorize;

using System;
using System.Linq;
using System.Text;

namespace Anubis.Win32.Server.Network.Processors
{
    public class AuthorizeProcessor : INetworkProcessor
    {
        public BaseNetworkEntity Handle( BaseNetworkEntity entity )
        {
            var authorizeEntity = entity.Unbox<AuthorizeNetworkEntity>();
            var splitted = Decode( Decode( authorizeEntity.Query ) ).Split( ':' );
            if ( splitted.Length != 3 )
            {
                authorizeEntity.ResultMessage = "Incorrect data received";
                return authorizeEntity;
            }

            if ( CheckRequest( splitted[ 0 ], splitted[ 1 ], splitted[ 2 ], out var message, out var xor_key ) )
            {
                authorizeEntity.ResultMessage = message;
                //authorizeEntity.Link = "http://rust.anubiss.cc/public/storage/57c7b63f2dea252a4c980748300a93c9/{zalupa}";
                authorizeEntity.Link = ServerContext.GetNetwork().Link + "/{zalupa}";
                authorizeEntity.Seconds = 39999;
                authorizeEntity.Key = xor_key;

                return authorizeEntity;
            }
            else
            {
                authorizeEntity.ResultMessage = message;
                return authorizeEntity;
            }
        }

        public bool IsHandlablePacket( BaseNetworkEntity entity )
            => entity.Identifier == 0xCAFE;

        private bool CheckRequest( string key, string hardware, string country, out string message, out string xor_key )
        {
            //ConsoleWriter.Error("Step 1");
            using ( var db = ServerContext.GetDb().CreateContext() )
            {
                //ConsoleWriter.Error("Step 2");
                var db_key = db.Keys.FirstOrDefault( ( x ) => x.Hash == key );
                if ( db_key != null )
                {
                    //ConsoleWriter.Error("Step 3");
                    var db_country = db.Countries.FirstOrDefault( ( x ) => x.ShortCode == country.ToLower() );
                    if ( db_country != null )
                    {
                        //ConsoleWriter.Error("Step 4");
                        if ( db_country.IsLocked )
                        {
                            //ConsoleWriter.Error("Step 5");
                            var db_region = db.Regions.FirstOrDefault( ( x ) => x.Id == db_key.RegionId );
                            if ( db_region != null )
                            {
                                //ConsoleWriter.Error("Step 6");
                                if ( db_region.IsLocked )
                                {
                                    //ConsoleWriter.Error("Step 61");
                                    if ( db_country.RegionId == db_region.Id )
                                    {
                                        //ConsoleWriter.Error("Step 62");
                                        if ( db_key.IsActivated )
                                        {
                                            //ConsoleWriter.Error("Step 63");
                                            if ( db_key.Hardware == hardware )
                                            {
                                                //ConsoleWriter.Error("Step 64");
                                                if ( DateTime.Now < db_key.ExpirationDate )
                                                {
                                                    var access = db.Accesses.First();
                                                    if ( access.IsAvailable )
                                                    {
                                                        message = $"Expiration date: {db_key.ExpirationDate}";
                                                        xor_key = access.XorKey;
                                                        return true;
                                                    }
                                                    else
                                                    {
                                                        message = access.DisabledMessage;
                                                        xor_key = "";
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    message = $"Key expired";
                                                    xor_key = "";
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                if ( db_key.Hardware == "empty" )
                                                {
                                                    db_key.Hardware = hardware;
                                                    db.SaveChanges();

                                                    if ( DateTime.Now < db_key.ExpirationDate )
                                                    {
                                                        var access = db.Accesses.First();
                                                        if ( access.IsAvailable )
                                                        {
                                                            message = $"Expiration date: {db_key.ExpirationDate}";
                                                            xor_key = access.XorKey;
                                                            return true;
                                                        }
                                                        else
                                                        {
                                                            message = access.DisabledMessage;
                                                            xor_key = "";
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        message = $"Key expired";
                                                        xor_key = "";
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            var db_period = db.Periods.FirstOrDefault( ( x ) => x.Id == db_key.PeriodId );
                                            if ( db_period != null )
                                            {
                                                var time = db_period.PeriodValue.Split( ':' );

                                                var expiration = DateTime.Now;
                                                expiration = expiration.AddDays( Convert.ToDouble( time[ 0 ] ) );
                                                expiration = expiration.AddHours( Convert.ToDouble( time[ 1 ] ) );
                                                expiration = expiration.AddMinutes( Convert.ToDouble( time[ 2 ] ) );

                                                db_key.Hardware = hardware;
                                                db_key.IsActivated = true;
                                                db_key.ActivateDate = DateTime.Now;
                                                db_key.ExpirationDate = expiration;

                                                db.SaveChanges();

                                                var access = db.Accesses.First();
                                                if ( access.IsAvailable )
                                                {
                                                    message = $"Expiration date: {db_key.ExpirationDate}";
                                                    xor_key = access.XorKey;
                                                    return true;
                                                }
                                                else
                                                {
                                                    message = access.DisabledMessage;
                                                    xor_key = "";
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                message = "Period not found";
                                                xor_key = "";
                                                return false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        message = "Country doesn't match";
                                        xor_key = "";
                                        return false;
                                    }
                                }
                                else
                                {
                                    message = "Region locked";
                                    xor_key = "";
                                    return false;
                                }
                            }
                            else
                            {
                                message = "Region not found";
                                xor_key = "";
                                return false;
                            }
                        }
                        else
                        {
                            message = "Country locked";
                            xor_key = "";
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine( country );
                        message = "Country not found";
                        xor_key = "";
                        return false;
                    }
                }
                else
                {
                    message = "Key not found";
                    xor_key = "";
                    return false;
                }
            }

            message = "";
            xor_key = "";
            return false;
        }

        private string Encode( string line )
        {
            var plainTextBytes = Encoding.UTF8.GetBytes( line );
            return Convert.ToBase64String( plainTextBytes );
        }

        private string Decode( string line )
        {
            var base64EncodedBytes = Convert.FromBase64String( line );
            return Encoding.UTF8.GetString( base64EncodedBytes );
        }
    }
}
