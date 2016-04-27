using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace ArchBench.Server
{
    public partial class ArchServerForm : Form
    {
        #region Fields
        private HttpServer.HttpServer mServer;
        private readonly IArchServerLogger mLogger;
        private readonly ModulePlugIns mModulePlugIns;
        #endregion

        public ArchServerForm()
        {
            InitializeComponent();
            mLogger = new TextBoxLogger( mOutput );
            mModulePlugIns = new ModulePlugIns( mLogger );
        }

        private void OnExit(object sender, EventArgs e)
        {
            if ( mServer != null ) mServer.Stop();
            mModulePlugIns.PlugInsManager.ClosePlugIns();
            Application.Exit();
        }

        private void OnConnect(object sender, EventArgs e)
        {
            mConnectTool.Checked = ! mConnectTool.Checked;
            if (mConnectTool.Checked)
            {
                mServer = new HttpServer.HttpServer();

//                mServer.Add( new ModuleHello( mLogger ) );
                mServer.Add( mModulePlugIns );

                mServer.Start( IPAddress.Any, int.Parse( mPort.Text ) );
                mLogger.WriteLine( String.Format( "Server online on port {0}", mPort.Text ) );
            }
            else
            {
                mServer.Stop();
                mServer = null;
            }
        }

        private void OnPlugIn( object sender, EventArgs e )
        {
            OpenFileDialog dialog = new OpenFileDialog() { Multiselect = false };
            dialog.Filter = @"Arch.Bench PlugIn File (*.dll)|*.dll";

            if ( dialog.ShowDialog() == DialogResult.OK )
            {
                mModulePlugIns.PlugInsManager.AddPlugIn( dialog.FileName );
                mLogger.WriteLine( String.Format( "Added PlugIn from {0}", 
                    System.IO.Path.GetFileName( dialog.FileName ) ) );
            }
        }

        private void OnRegistServer( object sender, EventArgs evt )
        {
            try 
            {
                TcpClient client = new TcpClient( mRemoteServerAddress.Text, 9000 );

                Byte[] data = Encoding.ASCII.GetBytes( String.Format( "{0}{1}-{2}", 
                    mRegistButton.Checked ? '+' : '-',  GetServerIP(), mPort.Text ) );         

                NetworkStream stream = client.GetStream();
                stream.Write( data, 0, data.Length );
                stream.Close();        
 
                client.Close();         
            } 
            catch ( SocketException e ) 
            {
               mLogger.WriteLine( String.Format( "SocketException: {0}", e ) );
            }

        }

        private static string GetServerIP()
        {
            IPHostEntry host = Dns.GetHostEntry( Dns.GetHostName() );
            foreach ( IPAddress ip in host.AddressList )
            {
                if ( ip.AddressFamily == AddressFamily.InterNetwork ) return ip.ToString();
            }
            return "0.0.0.0";        
        }
    }
}
