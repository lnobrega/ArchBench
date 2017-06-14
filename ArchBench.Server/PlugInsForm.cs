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
            mPlugInsListView.SelectedIndexChanged +=
                ( sender, args ) => mSettingsButton.Enabled = mPlugInsListView.SelectedItems.Count > 0;
            mPlugInsListView.SelectedIndexChanged +=
                ( sender, args ) => mRemoveButton.Enabled = mPlugInsListView.SelectedItems.Count > 0;

            PlugInsManager = aManager;

            foreach ( var plugin in PlugInsManager.PlugIns )
            {
                AppendPlugIn( plugin );
            }
        }

        private IPlugInsManager PlugInsManager { get; set; }

        private void OnAppend( object sender, EventArgs e )
        {
            OpenFileDialog dialog = new OpenFileDialog() { Multiselect = false };
            dialog.Multiselect = true;
            dialog.Filter = @"Arch.Bench PlugIn File (*.dll)|*.dll";

            if ( dialog.ShowDialog() == DialogResult.OK )
            {
                foreach ( var name in dialog.FileNames )
                {
                    var plugins = PlugInsManager.AddPlugIn( name );
                    foreach ( var plugin in plugins )
                    {
                        AppendPlugIn( plugin );
                    }
                }
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

        private void OnSettings( object sender, EventArgs e )
        {
            if ( mPlugInsListView.SelectedItems.Count == 0 ) return;

            var dialog = new PlugInsSettingsForm( mPlugInsListView.SelectedItems[0].Tag as IArchServerPlugIn );
            dialog.ShowDialog();
        }
    }
}
