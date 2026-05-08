using Akiyama.EDOCCommon.Plugins.Context;
using Akiyama.EDOCCommon.Plugins.Workers;
using Observatory.Framework.Interfaces;

namespace Akiyama.EDOCCommon.Plugins.Core
{
    public abstract class AkiyamaPlugin
    {

        public PluginContext Context { get; set; }
        public AkiyamaPlugin(IObservatoryCore core, IWorkerObject worker)
        {
            Context = new(core, worker);
        }

        public AkiyamaPlugin(PluginContext context)
        {
            Context = context;
        }

    }
}
