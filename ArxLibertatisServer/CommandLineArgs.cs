using Plexdata.ArgumentParser.Attributes;

namespace ArxLibertatisServer
{
    [HelpUtilize]
    [ParametersGroup]
    public class CommandLineArgs
    {
        [HelpSummary("Port the server is supposed to listen on")]
        [OptionParameter(SolidLabel = "port", DefaultValue = 8888)]
        public ushort Port { get; set; } = 8888;
    }
}
