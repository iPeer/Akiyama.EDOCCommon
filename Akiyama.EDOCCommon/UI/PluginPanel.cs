using Akiyama.EDOCCommon.Plugins.Context;

namespace Akiyama.EDOCCommon.UI
{
    public class PluginPanel : Panel
    {

        /*
         * This class was put together with a lot of looking around at other peoples' plugin code because there is absolutely zero fucking documation on this shit
         * from the creator of Observatory Core.
         * 
         * Info sources:
         *  https://github.com/fredjk-gh/ObservatoryPlugins/blob/main/ObservatoryArchivist/UI/ArchivistPanel.cs
         *  https://github.com/fredjk-gh/ObservatoryPlugins/blob/main/ObservatoryArchivist/UI/ArchivistUI.cs
         * 
         */

        private PluginContext _context;
        private TableLayoutPanel _tableLayoutPanel;

        private bool _controlAdded = false;

        /// <summary>
        /// Creates an empty panel for this plugin to use as a UI.<br />
        /// Use <see cref="AddUserControl(UserControl)"/> after the fact to add a UserControl containing your desired form controls.
        /// </summary>
        /// <param name="context">The context object for this instance</param>
        public PluginPanel(PluginContext context) : this(context, null) { }
        /// <summary>
        /// Creates an UI panel for this plugin with <paramref name="uiLayout"/> as its control layout.
        /// </summary>
        /// <param name="context">The context object for this instance</param>
        /// <param name="uiLayout">An instance of a <see cref="UserControl"/> containing UI elements to add</param>
        public PluginPanel(PluginContext context, UserControl uiLayout = null)
        {
            _context = context;

            AutoScroll = true;
            DoubleBuffered = true;

            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;

            Controls.Add(_tableLayoutPanel = new());
            _tableLayoutPanel.Dock = DockStyle.Fill;
            _tableLayoutPanel.ColumnStyles.Clear();
            _tableLayoutPanel.ColumnStyles.Add(new()
            {
                SizeType = SizeType.Percent,
                Width = 100,
            });

            _tableLayoutPanel.RowStyles.Clear();
            _tableLayoutPanel.RowStyles.Add(new()
            {
                SizeType = SizeType.Percent,
                Height = 100,
            });
            if (uiLayout != null)
            {
                _controlAdded = true;
                _tableLayoutPanel.Controls.Add(_context.UI = uiLayout);
                _context.UI.Dock = DockStyle.Fill;
            }

        }

        /// <summary>
        /// Registers the specified <see cref="UserControl"/> as this UI's layout.<br />
        /// Raises <seealso cref="InvalidOperationException"/> if a control has already been registered.
        /// </summary>
        /// <param name="uc">The <see cref="UserControl"/> instance to register</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void AddUserControl(UserControl uc)
        {
            if (_controlAdded)
            {
                throw new InvalidOperationException("Panel already has a UserControl instance registered.");
            }
            _tableLayoutPanel.Controls.Add(_context.UI = uc);
            _context.UI.Dock = DockStyle.Fill;
            _controlAdded = true;
        }

    }
}
