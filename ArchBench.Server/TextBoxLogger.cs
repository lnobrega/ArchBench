using System;
using System.Windows.Forms;

namespace ArchBench.Server
{
    class TextBoxLogger : IArchServerLogger
    {
        public TextBoxLogger( TextBox aTexBox )
        {
            this.TextBox     = aTexBox;
            this.IndentLevel = 0;
            this.IsNewLine   = true;
        }

        public TextBox TextBox     { get; set; }
        public int     IndentLevel { get; set; }
        public bool    IsNewLine   { get; set; }

        // This delegate enables asynchronous calls for setting the text property on a TextBox control.
        delegate void AppendTextCallback( string aText );

        public void Write( string aMessage )
        {
            if ( this.IsNewLine )
            {
                for ( int i = 0 ; i < this.IndentLevel ; i++ )
                    this.TextBox.AppendText( "    " );
                this.IsNewLine = false;
            }
            AppendText( aMessage );
        }

        public void Write( string aFormat, params object[] aArgs )
        {
            this.Write( string.Format( aFormat, aArgs ) );
        }


        public void WriteLine()
        {
            AppendText( Environment.NewLine );
            this.IsNewLine = true;
        }

        public void WriteLine( string aFormat, params object[] aArgs )
        {
            this.WriteLine( string.Format( aFormat, aArgs ) );
        }

        public void WriteLine( string aMessage )
        {
            this.Write( aMessage );
            this.WriteLine();
        }

        private void AppendText( string aText )
        {
            // InvokeRequired required compares the thread ID of the calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if ( this.TextBox.InvokeRequired )
            {
                AppendTextCallback callback = AppendText;
                this.TextBox.Invoke( callback, new object[] { aText } );
            }
            else
            {
                this.TextBox.AppendText( aText );
            }
        }
    }
}
