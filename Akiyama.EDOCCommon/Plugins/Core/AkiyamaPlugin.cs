using Akiyama.EDOCCommon.Plugins.Context;
using Akiyama.EDOCCommon.Plugins.Workers;
using Observatory.Framework.Interfaces;

namespace Akiyama.EDOCCommon.Plugins.Core
{
    /// <summary>
    /// A wrapper for plugins for Observatory Core.<br />
    /// This class is mainly to allow more generic typing for use in <seealso cref="PluginContext"/>, as such, its use is completely option.
    /// </summary>
    /// <remarks>
    /// <b>Note</b>: This class is <see langword="abstract"/> and cannot be instantiated directly. To use, have your "main" plugin class (if any) extend this one:<br />
    /// <code>
    /// public class MyPlugin : AkiyamaPlugin { }
    /// </code>
    /// </remarks>
    public abstract class AkiyamaPlugin
    {

        /// <summary>
        /// This plugins Context
        /// </summary>
        public PluginContext Context { get; set; }

        /// <summary>
        /// Creates an instance of <see cref="AkiyamaPlugin"/> and automatically creates a <see cref="PluginContext"/> with the specified <paramref name="core"/> and <paramref name="worker"/> instances.<br />
        /// To create a plugin instance with an already created <see cref="PluginContext"/>, see <seealso cref="AkiyamaPlugin(PluginContext)"/>.
        /// </summary>
        /// <param name="core">The <see cref="IObservatoryCore"/> instance to associate with this plugin instance</param>
        /// <param name="worker">The <see cref="IWorkerObject"/> instance to associate with this plugin instance</param>
        public AkiyamaPlugin(IObservatoryCore core, IWorkerObject worker)
        {
            Context = new(core, worker);
        }

        /// <summary>
        /// Creates an instance of <see cref="AkiyamaPlugin"/> with the specified already created <see cref="PluginContext"/>.
        /// </summary>
        /// <param name="context">The <see cref="PluginContext"/> to associate with this instance</param>
        public AkiyamaPlugin(PluginContext context)
        {
            Context = context;
        }

    }
}
