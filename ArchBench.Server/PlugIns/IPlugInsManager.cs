using System.Collections.Generic;

namespace ArchBench.Server
{
    public interface IPlugInsManager
    {
        /// <summary>
        /// A Collection of all Plugins Found and Loaded by the FindPlugins() Method
        /// </summary>
        IEnumerable<IArchServerPlugIn> PlugIns { get; }

        IEnumerable<IArchServerPlugIn> AddPlugIn( string aFileName );

        void Remove( IArchServerPlugIn aPlugIn );

        /// <summary>
        /// Search for a PlugIn
        /// </summary>
        /// <param name="aName">The name of the PlugIn</param>
        /// <returns></returns>
        IArchServerPlugIn Find( string aName );

        /// <summary>
        /// Unloads and Closes all AvailablePlugins
        /// </summary>
        void ClosePlugIns();
    }
}