namespace ArchBench
{
    public interface IArchServerPlugIn
    {
        string Name        { get; }
        string Description { get; }
        string Author      { get; }
        string Version     { get; }
        bool   Enabled     { get; set; }

        IArchServerPlugInHost Host { get; set; }

        void Initialize();
        void Dispose();
    }
}
