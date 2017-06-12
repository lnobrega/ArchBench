using System;
using System.Windows.Forms;

namespace ArchBench.Server
{
    class TextBoxLogger : IArchServerLogger
    {
        public TextBoxLogger( TextBox aTexBox )
        {
            TextBox     = aTexBox;
            IndentLevel = 0;
            IsNewLine   = true;
        }

        public TextBox TextBox     { get; set; }
        public int     IndentLevel { get; set; }
        public bool    IsNewLine   { get; set; }

        // This delegate enables asynchronous calls for setting the text property on a TextBox control.
        delegate void AppendTextCallback( string aText );

        public void Write( string aMessage )
        {
            if ( IsNewLine )
            {
                for ( var i = 0 ; i < IndentLevel ; i++ )
                    TextBox.AppendText( "    " );
                IsNewLine = false;
            }
            AppendText( aMessage );
        }

        public void Write( string aFormat, params object[] aArgs )
        {
            Write( string.Format( aFormat, aArgs ) );
        }


        public void WriteLine()
        {
            AppendText( Environment.NewLine );
            IsNewLine = true;
        }

        public void WriteLine( string aFormat, params object[] aArgs )
        {
            WriteLine( string.Format( aFormat, aArgs ) );
        }

        public void WriteLine( string aMessage )
        {
            Write( aMessage );
            WriteLine();
        }

        private void AppendText( string aText )
        {
            // InvokeRequired required compares the thread ID of the calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if ( TextBox.InvokeRequired )
            {
                AppendTextCallback callback = AppendText;
                TextBox.Invoke( callback, aText );
            }
            else
            {
                TextBox.AppendText( aText );
            }
        }
    }
}
