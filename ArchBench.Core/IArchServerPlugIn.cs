using System;

namespace ArchBench
{
    public interface IArchServerPlugIn
    {
        String Name        { get; }
        String Description { get; }
        String Author      { get; }
        String Version     { get; }

        IArchServerPlugInHost Host { get; set; }

        void Initialize();
        void Dispose();
    }
}
