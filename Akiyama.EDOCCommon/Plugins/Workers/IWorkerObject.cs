using Observatory.Framework.Interfaces;

namespace Akiyama.EDOCCommon.Plugins.Workers
{
    public interface IWorkerObject
    {
        /// <inheritdoc cref="IObservatoryPlugin.Guid"/>
        public static Guid Guid { get => new("90911ABE-B7DB-4150-0001-FFFFFFFFFFFF"); }
    }

}
