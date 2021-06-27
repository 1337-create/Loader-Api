using System;
using System.Diagnostics;

namespace Anubis.System.Exceptions
{
    public class ExBaseException : Exception
    {
        private readonly StackTrace m_StackTrace;
        public ExBaseException(string message) 
            : base(message)
        {
            m_StackTrace = new StackTrace( this, true );
        }

        public int ExceptionLine()
        {
            var frame = m_StackTrace.GetFrame( 0 );
            if(frame != null)
            {
                return frame.GetFileLineNumber();
            }

            return 0;
        }
        public StackTrace GetStackTrace()
            => m_StackTrace;
        public bool IsBaseOf<T>() where T : ExBaseException
            => this is T;
    }
}
