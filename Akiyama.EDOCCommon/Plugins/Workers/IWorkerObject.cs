using Observatory.Framework.Interfaces;

namespace Akiyama.EDOCCommon.Plugins.Workers
{
    public interface IWorkerObject : IObservatoryWorker
    {
        /// <inheritdoc cref="IObservatoryPlugin.Guid"/>
        public new abstract static Guid Guid { get; }

    }
}
