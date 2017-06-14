using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchBench.Server
{
    public partial class PlugInsSettingsForm : Form
    {
        public PlugInsSettingsForm( IArchServerPlugIn aPlugIn )
        {
            InitializeComponent();
            mSettingsPropertyGrid.SelectedObject = new UI.DictionaryPropertyGridAdapter( aPlugIn.Parameters );
        }
    }
}
