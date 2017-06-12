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
    public partial class PlugInsForm : Form
    {
        public PlugInsForm( IPlugInsManager aManager )
        {
            InitializeComponent();

            PlugInsManager = aManager;

            foreach ( var plugin in PlugInsManager.PlugIns )
            {
                AppendPlugIn( plugin );
            }
        }

        private IPlugInsManager PlugInsManager { get; set; }

        private void OnAppendPlugIn( object sender, EventArgs e )
        {
            OpenFileDialog dialog = new OpenFileDialog() { Multiselect = false };
            dialog.Filter = @"Arch.Bench PlugIn File (*.dll)|*.dll";

            if ( dialog.ShowDialog() == DialogResult.OK )
            {
                var plugin = PlugInsManager.AddPlugIn( dialog.FileName );
                if ( plugin != null ) AppendPlugIn( plugin );
            }
        }

        private void OnRemovePlugIn( object sender, EventArgs e )
        {
            foreach ( ListViewItem item in mPlugInsListView.SelectedItems )
            {
                var plugin = (IArchServerPlugIn) item.Tag;
                if ( plugin == null ) continue;

                PlugInsManager.Remove( plugin );
                item.Remove();
            }
        }

        private void AppendPlugIn( IArchServerPlugIn aPlugIn )
        {
            var item = new ListViewItem
            {
                Text = aPlugIn.Name,
                Checked = true,
                ImageIndex = 0,
                Tag = aPlugIn
            };

            item.SubItems.Add( aPlugIn.Version );
            item.SubItems.Add( aPlugIn.Author );
            item.SubItems.Add( aPlugIn.Description );

            mPlugInsListView.Items.Add( item );
        }

        private void OnItemChecked( object sender, ItemCheckedEventArgs e )
        {
            e.Item.ImageIndex = e.Item.Checked ? 0 : 1;
            e.Item.ForeColor = e.Item.Checked ? Color.Empty : Color.Gray;

            var plugin = (IArchServerPlugIn) e.Item.Tag;
            if ( plugin != null ) plugin.Enabled = e.Item.Checked;
        }
    }
}
