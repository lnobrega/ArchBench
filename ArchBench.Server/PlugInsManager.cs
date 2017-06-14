using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ArchBench.Server
{
    /// <summary>
    /// Summary description for PlugInsManager.
    /// </summary>
    public class PlugInsManager : IPlugInsManager
    {
        private readonly IList<IArchServerPlugIn> mPlugIns = new List<IArchServerPlugIn>();
        private readonly IArchServerPlugInHost    mHost;

        /// <summary>
        /// Constructor of the Class
        /// </summary>
        public PlugInsManager( IArchServerPlugInHost aHost )
        {
            mHost = aHost;
        }

        /// <summary>
        /// A Collection of all Plugins Found and Loaded by the FindPlugins() Method
        /// </summary>
        public IEnumerable<IArchServerPlugIn> PlugIns => mPlugIns;

        public IEnumerable<IArchServerPlugIn> AddPlugIn( string aFileName )
        {
            // reate a new assembly from the plugin file we're adding..
            Assembly assembly = Assembly.LoadFrom( aFileName );

            var instances = new List<IArchServerPlugIn>();

            // Next we'll loop through all the Types found in the assembly
            foreach ( Type type in assembly.GetTypes() )
            {
                if ( ! type.IsPublic ) continue;
                if ( type.IsAbstract ) continue;

                // Gets a type object of the interface we need the plugins to match
                Type typeInterface = type.GetInterface( "ArchBench.IArchServerPlugIn", true );

                // Make sure the interface we want to use actually exists
                if ( typeInterface == null ) continue;

                // Create a new instance and store the instance in the collection for later use
                var instance = (IArchServerPlugIn) Activator.CreateInstance( assembly.GetType( type.ToString() ) );

                // Set the Plugin's host to this class which inherited IPluginHost
                instance.Host = mHost;

                // Call the initialization sub of the plugin
                instance.Initialize();

                //Add the new plugin to our collection here
                mPlugIns.Add( instance );

                instance.Enabled = true;
                instances.Add( instance );
            }

            return instances;
        }

        public void Remove( IArchServerPlugIn aPlugIn )
        {
            if ( mPlugIns.Contains( aPlugIn ) ) mPlugIns.Remove( aPlugIn );
        }

        /// <summary>
        /// Search for a PlugIn
        /// </summary>
        /// <param name="aName">The name of the PlugIn</param>
        /// <returns></returns>
        public IArchServerPlugIn Find( string aName )
        {
            return mPlugIns.FirstOrDefault( p => p.Name == aName );
        }

        /// <summary>
        /// Unloads and Closes all AvailablePlugins
        /// </summary>
        public void ClosePlugIns()
        {
            foreach ( IArchServerPlugIn plugin in mPlugIns )
            {
                // Close all plugin instances
                plugin.Dispose();
            }

            //Finally, clear our collection of available plugins
            mPlugIns.Clear();
        }
    }
}