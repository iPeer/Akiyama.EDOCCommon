using Akiyama.EDOCCommon.Plugins.Core;
using Akiyama.EDOCCommon.Plugins.Workers;
using Observatory.Framework;
using Observatory.Framework.Interfaces;

namespace Akiyama.EDOCCommon.Plugins.Context
{

    public enum PropertyOverwriteType
    {
        UPDATE,
        ERROR
    }

    /// <summary>
    /// Provides context information and pointers for this plugin
    /// </summary>
    public class PluginContext
    {


        IObservatoryCore _core;
        /// <summary>
        /// The <seealso cref="IObservatoryCore"/> object tied to this instance
        /// </summary>
        public IObservatoryCore ObservatoryCore { get => _core; internal set { _core = value; } }
        /// <summary>
        /// [Alias of <see cref="ObservatoryCore"/>]<br /><inheritdoc cref="ObservatoryCore"/>
        /// </summary>
        public IObservatoryCore Core { get => _core; internal set { _core = value; } }

        private IWorkerObject _worker;
        /// <summary>
        /// The <seealso cref="IWorkerObject"/> object tied to this instance.<br />
        /// If type enforcement is required for this object, you can cast or use <seealso cref="GetWorker{T}"/>
        /// </summary>
        public IWorkerObject Worker { get => _worker; internal set { _worker = value; } }

        /// <summary>
        /// The <seealso cref="PluginUI"/> object tied to this instance.<br />
        /// <seealso cref="PluginUI"/>: <inheritdoc cref="PluginUI"/>
        /// </summary>
        public PluginUI PluginUI;

        /// <summary>
        /// A <see cref="UserControl"/> instance used with <see cref="PluginUI.UIType.Panel"/> type Plugin UIs.
        /// </summary>
        public UserControl UI;

        /// <summary>
        /// Used to log error information for this plugin. See also: <seealso cref="IObservatoryCore.GetPluginErrorLogger"/>.
        /// </summary>
        public Action<Exception, string> ErrorLogger;

        private AkiyamaPlugin _base;
        /// <summary>
        /// The generic Plugin base for this plugin.
        /// </summary>
        public AkiyamaPlugin BasePlugin { get => _base; set { _base = value; } }

        /// <summary>
        /// The data storage directory for this context.<br/>
        /// <b>Note</b>: This value must be set at plugin initialisation. It cannot be set automatically by this library.
        /// </summary>
        public string PluginStorageFolder { get; set; }

        private readonly Dictionary<string, object> _genericProperties = [];

        /// <summary>
        /// The procedure used when attempting to overwrite properties via <see cref="SetProperty(string, object)"/>.<br />
        /// - When set to <see cref="PropertyOverwriteType.UPDATE"/>, an existing value will be updated.<br />
        /// - When set to <see cref="PropertyOverwriteType.ERROR"/>, an exception will be raised when trying to update the value.<br />
        /// </summary>
        public PropertyOverwriteType GenericPropertyOverwriteType { get; set; } = PropertyOverwriteType.UPDATE;
        /// <summary>
        /// Instantiate an instance of this class with the specified <seealso cref="IObservatoryCore"/> instance.
        /// </summary>
        /// <param name="core">The <seealso cref="IObservatoryCore"/> instance to associate with this context.</param>
        public PluginContext(IObservatoryCore core)
        {
            _core = core;
        }

        /// <summary>
        /// Instantiate an instance of this class with the specified <seealso cref="IObservatoryCore"/> and <seealso cref="IWorkerObject"/> instances.
        /// </summary>
        /// <param name="core">The <seealso cref="IObservatoryCore"/> instance to associate with this context.</param>
        /// <param name="worker">The <seealso cref="IWorkerObject"/> instance to associate with this context.</param>
        public PluginContext(IObservatoryCore core, IWorkerObject worker)
        {
            _core = core;
            _worker = worker;
        }

        /// <summary>
        /// Returns this <seealso cref="PluginContext"/>'s Worker instance as type <typeparamref name="T"/>. Throws <see cref="NullReferenceException"/> if this instance's Worker is null.
        /// </summary>
        /// <typeparam name="T">The type of your Worker class</typeparam>
        /// <returns><see cref="Worker"/> as type <typeparamref name="T"/></returns>
        /// <exception cref="NullReferenceException"></exception>
        public T GetWorker<T>()
        {
            if (_worker == null)
            {
                throw new NullReferenceException("Worker is null");
            }
            return (T)_worker;
        }

        /// <summary>
        /// Returns this <seealso cref="PluginContext"/>'s BasePlugin instance as type <typeparamref name="T"/>. Throws <see cref="NullReferenceException"/> if this instance's Worker is null.
        /// </summary>
        /// <typeparam name="T">The type of your AkiyamaPlugin class</typeparam>
        /// <returns><see cref="BasePlugin"/> as type <typeparamref name="T"/></returns>
        /// <exception cref="NullReferenceException"></exception>
        public T GetBase<T>() where T : AkiyamaPlugin => GetBasePlugin<T>();
        /// <inheritdoc cref="GetBase{T}"/>
        public T GetBasePlugin<T>() where T : AkiyamaPlugin
        {
            if (_base == null)
            {
                throw new NullReferenceException("BasePlugin is null");
            }
            return (T)_base;
        }

        /// <summary>
        /// Sets a generic property within this Context instance. Usually to store information inside the Context which the default class structure doesn't facilitate.<br />
        /// <b>Note</b>: This method does not store the direct typing of the values provided. You are responsible for remembering the correct types when requesting the properties via <see cref="GetProperty{T}(string)"/> or <see cref="GetProperty{T}(string, T)"/><br/><br />
        /// If the Context's <see cref="GenericPropertyOverwriteType"/> is set to <c>ERROR</c> and a property overwrite is attempted, an <seealso cref="InvalidOperationException"/> will be thrown.
        /// </summary>
        /// <param name="propertyName">The name of the property to store or update</param>
        /// <param name="propertyValue">The value to store for this property</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void SetProperty(string propertyName, object propertyValue)
        {
            if (_genericProperties.ContainsKey(propertyName))
            {
                if (GenericPropertyOverwriteType == PropertyOverwriteType.ERROR)
                {
                    throw new InvalidOperationException("Now allowed to overwrite existing generic properties.");
                }
                _genericProperties[propertyName] = propertyValue;
            }
            else
            {
                _genericProperties[propertyName] = propertyValue;
            }
        }

        /// <summary>
        /// Returns the generic property <paramref name="propertyName"/> for this Context. A <see cref="KeyNotFoundException"/> is thrown if the specified <paramref name="propertyName"/> is not in the store.
        /// </summary>
        /// <typeparam name="T">The type of the property being retrieved</typeparam>
        /// <param name="propertyName">The name of the property being retrieved</param>
        /// <returns>The value of the property maching <paramref name="propertyName"/> returned as type <typeparamref name="T"/>.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public T GetProperty<T>(string propertyName)
        {
            if (_genericProperties.ContainsKey(propertyName))
            {
                return (T)_genericProperties[propertyName];
            }
            throw new KeyNotFoundException(propertyName);
        }

        /// <summary>
        /// Returns the generic property <paramref name="propertyName"/> for this Context. If the specified <paramref name="propertyName"/> is not found, then <paramref name="defaultValue"/> will be used instead.
        /// </summary>
        /// <typeparam name="T">The type of the property being retrieved</typeparam>
        /// <param name="propertyName">The name of the property being retrieved</param>
        /// <param name="defaultValue">The value to be returned if <paramref name="propertyName"/> is not found</param>
        /// <returns>The value of the property maching <paramref name="propertyName"/> returned as type <typeparamref name="T"/> if found, otherwise <paramref name="defaultValue"/></returns>
        public T GetProperty<T>(string propertyName, T defaultValue)
        {
            try
            {
                return GetProperty<T>(propertyName);
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Removes property <paramref name="propertyName"/> from the property store.
        /// </summary>
        /// <param name="propertyName">The name of the property to remove</param>
        public void DeleteProperty(string propertyName)
        {
            _genericProperties.Remove(propertyName);
        }

        /// <summary>
        /// [Alias of <see cref="DeleteProperty(string)"/>]<br />
        /// <inheritdoc cref="DeleteProperty(string)"/>
        /// </summary>
        /// <param name="propertyName"></param>
        public void RemoveProperty(string propertyName) => DeleteProperty(propertyName);

        public T GetUI<T>() where T : UserControl
        {
            if (UI == null)
            {
                throw new NullReferenceException("UI is null");
            }
            return (T)UI;
        }

    }
}
