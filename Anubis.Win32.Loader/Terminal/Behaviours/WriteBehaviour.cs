using Anubis.System;

using Sys = System;

namespace Anubis.Win32.Loader.Terminal.Behaviours
{
    public class WriteBehaviour : ExBehaviour
    {
        public void Info( string what )
            => WriteLine( WriteBehaviourType.Info, what );
        public void Debug( string what )
            => WriteLine( WriteBehaviourType.Debug, what );
        public void Error( string what )
            => WriteLine( WriteBehaviourType.Error, what );
        public void Warning( string what )
            => WriteLine( WriteBehaviourType.Warning, what );
        public void Success( string what )
            => WriteLine( WriteBehaviourType.Success, what );

        public void InfoCL( string what )
           => Write( WriteBehaviourType.Info, what );
        public void DebugCL( string what )
            => Write( WriteBehaviourType.Debug, what );
        public void ErrorCL( string what )
            => Write( WriteBehaviourType.Error, what );
        public void WarningCL( string what )
            => Write( WriteBehaviourType.Warning, what );
        public void SuccessCL( string what )
            => Write( WriteBehaviourType.Success, what );

        public void Clear()
            => Sys.Console.Clear();

        private void WriteLine( WriteBehaviourType type, string what )
            => Write( type, what + Sys.Environment.NewLine );
        private void Write(WriteBehaviourType type, string what)
        {
            Sys.Console.ForegroundColor = ( Sys.ConsoleColor )type;
            Sys.Console.Write( what );
            Sys.Console.ResetColor();
        }
    }
}
