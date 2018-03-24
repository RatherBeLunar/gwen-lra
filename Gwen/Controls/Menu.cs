using Gwen.ControlInternal;
using System;
using System.Drawing;
using System.Linq;

namespace Gwen.Controls
{
    /// <summary>
    /// Popup menu.
    /// </summary>
    public class Menu : ScrollControl
    {
        #region Properties

        /// <summary>
        /// Determines whether the menu should be disposed on close.
        /// </summary>
        public bool DeleteOnClose { get { return m_DeleteOnClose; } set { m_DeleteOnClose = value; } }

        public bool IconMarginDisabled { get { return m_DisableIconMargin; } set { m_DisableIconMargin = value; } }

        protected override Margin PanelMargin
        {
            get
            {
                return Margin.Zero;
            }
        }
        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="parent">Parent control.</param>
        public Menu(ControlBase parent)
            : base(parent)
        {
            SetBounds(0, 0, 10, 10);
            IconMarginDisabled = false;

            AutoHideBars = false;
            EnableScroll(false, true);
            DeleteOnClose = false;
            AutoSizeToContents = true;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds a divider menu item.
        /// </summary>
        public virtual void AddDivider()
        {
            MenuDivider divider = new MenuDivider(this);
            divider.Dock = Pos.Top;
            divider.Margin = new Margin(IconMarginDisabled ? 0 : 24, 0, 4, 0);
        }

        /// <summary>
        /// Adds a new menu item.
        /// </summary>
        /// <param name="text">Item text.</param>
        /// <returns>Newly created control.</returns>
        public virtual MenuItem AddItem(string text)
        {
            return AddItem(text, String.Empty);
        }

        /// <summary>
        /// Adds a new menu item.
        /// </summary>
        /// <param name="text">Item text.</param>
        /// <param name="iconName">Icon texture name.</param>
        /// <param name="accelerator">Accelerator for this item.</param>
        /// <returns>Newly created control.</returns>
        public virtual MenuItem AddItem(string text, string iconName, string accelerator = "")
        {
            MenuItem item = new MenuItem(this);
            item.Padding = Padding.Four;
            item.SetText(text);
            item.SetImage(iconName);
            item.SetAccelerator(accelerator);

            OnAddItem(item);

            return item;
        }

        /// <summary>
        /// Closes the current menu.
        /// </summary>
        public virtual void Close()
        {
            //System.Diagnostics.Debug.Print("Menu.Close: {0}", this);
            ScrollToTop();
            IsHidden = true;
            if (DeleteOnClose)
            {
                DelayedDelete();
            }
        }

        /// <summary>
        /// Closes all submenus.
        /// </summary>
        public virtual void CloseAll()
        {
            //System.Diagnostics.Debug.Print("Menu.CloseAll: {0}", this);
            var copy = Children.ToArray();
            foreach (var child in copy)
            {
                if (child is MenuItem)
                    (child as MenuItem).CloseMenu();
            }
        }

        /// <summary>
        /// Closes all submenus and the current menu.
        /// </summary>
        public override void CloseMenus()
        {
            //System.Diagnostics.Debug.Print("Menu.CloseMenus: {0}", this);
            base.CloseMenus();
            CloseAll();
            Close();
        }

        /// <summary>
        /// Indicates whether any (sub)menu is open.
        /// </summary>
        /// <returns></returns>
        public virtual bool IsMenuOpen()
        {
            return Children.Any(child => { if (child is MenuItem) return (child as MenuItem).IsMenuOpen; return false; });
        }

        /// <summary>
        ///  Opens the menu.
        /// </summary>
        /// <param name="pos">Unused.</param>
        public void Open(Pos pos)
        {
            IsHidden = false;
            BringToFront();
            Point mouse = Input.InputHandler.MousePosition;
            SetPosition(mouse.X, mouse.Y);
            Invalidate();
        }


        #endregion Methods

        internal override bool IsMenuComponent { get { return true; } }

        /// <summary>
        /// Determines whether the menu should open on mouse hover.
        /// </summary>
        protected virtual bool ShouldHoverOpenMenu { get { return true; } }

        public override Size GetSizeToFitContents()
        {
            Size ret = m_Panel.GetSizeToFitContents() + PanelMargin.Size + Padding.Size;
            if (Y + ret.Height > GetCanvas().Height)
            {
                ret.Height = GetCanvas().Height - Y;
                EnableScroll(false, true);
                ret.Width += VScrollWidth;
            }
            else
            {
                EnableScroll(false, false);
            }
            return ret;
        }

        /// <summary>
        /// Add item handler.
        /// </summary>
        /// <param name="item">Item added.</param>
        protected virtual void OnAddItem(MenuItem item)
        {
            item.TextPadding = new Padding(IconMarginDisabled ? 0 : 24, 0, 16, 0);
            item.Dock = Pos.Top;
            item.Alignment = Pos.CenterV | Pos.Left;
            item.HoverEnter += OnHoverItem;
            item.SizeToChildren();
            SizeToChildren();
            // Do this here - after Top Docking these values mean nothing in layout
            //  int w = item.Width + 10 + 32;
            //  if (w < Width) w = Width;
            //   SetSize(w, Height);
        }

        /// <summary>
        /// Mouse hover handler.
        /// </summary>
        /// <param name="control">Event source.</param>
        protected virtual void OnHoverItem(ControlBase control, EventArgs args)
        {
            if (!ShouldHoverOpenMenu) return;

            MenuItem item = control as MenuItem;
            if (null == item) return;
            if (item.IsMenuOpen) return;

            CloseAll();
            item.OpenMenu();
        }

        /// <summary>
        /// Renders the control using specified skin.
        /// </summary>
        /// <param name="skin">Skin to use.</param>
        protected override void Render(Skin.SkinBase skin)
        {
            skin.DrawMenu(this, IconMarginDisabled);
        }

        /// <summary>
        /// Renders under the actual control (shadows etc).
        /// </summary>
        /// <param name="skin">Skin to use.</param>
        protected override void RenderUnder(Skin.SkinBase skin)
        {
            base.RenderUnder(skin);
            skin.DrawShadow(this);
        }

        private bool m_DeleteOnClose;
        private bool m_DisableIconMargin;
    }
}