﻿using System;
using System.Drawing;
using Gwen.ControlInternal;

namespace Gwen.Controls
{
    /// <summary>
    /// Control with multiple tabs that can be reordered and dragged.
    /// </summary>
    public class TabControl : Container
    {
        private readonly TabStrip m_TabStrip;
        private readonly ScrollBarButton[] m_Scroll;
        private TabButton m_CurrentButton;
        private int m_ScrollOffset;

        /// <summary>
        /// Invoked when a tab has been added.
        /// </summary>
		public event GwenEventHandler<EventArgs> TabAdded;

        /// <summary>
        /// Invoked when a tab has been removed.
        /// </summary>
		public event GwenEventHandler<EventArgs> TabRemoved;

        /// <summary>
        /// Determines if tabs can be reordered by dragging.
        /// </summary>
        public bool AllowReorder { get { return m_TabStrip.AllowReorder; } set { m_TabStrip.AllowReorder = value; } }

        /// <summary>
        /// Currently active tab button.
        /// </summary>
        public TabButton CurrentButton { get { return m_CurrentButton; } }

        /// <summary>
        /// Current tab strip position.
        /// </summary>
        public Pos TabStripPosition { get { return m_TabStrip.StripPosition; }set { m_TabStrip.StripPosition = value; } }

        /// <summary>
        /// Tab strip.
        /// </summary>
        public TabStrip TabStrip { get { return m_TabStrip; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="TabControl"/> class.
        /// </summary>
        /// <param name="parent">Parent control.</param>
        public TabControl(ControlBase parent)
            : base(parent)
		{
			m_Scroll = new ScrollBarButton[2];
            m_ScrollOffset = 0;

            m_TabStrip = new TabStrip(null);
            m_TabStrip.StripPosition = Pos.Top;

            // Make this some special control?
            m_Scroll[0] = new ScrollBarButton(null);
            m_Scroll[0].SetDirectionLeft();
            m_Scroll[0].Clicked += ScrollPressedLeft;
            m_Scroll[0].SetSize(14, 16);

            m_Scroll[1] = new ScrollBarButton(null);
            m_Scroll[1].SetDirectionRight();
            m_Scroll[1].Clicked += ScrollPressedRight;
            m_Scroll[1].SetSize(14, 16);

            PrivateChildren.Add(m_TabStrip);
			PrivateChildren.Add(m_Scroll[0]);
			PrivateChildren.Add(m_Scroll[1]);

            IsTabable = false;
        }
        protected override void RenderPanel(Skin.SkinBase skin)
        {
            base.RenderPanel(skin);
            skin.DrawTabControl(this);
        }
        /// <summary>
        /// Adds a new page/tab.
        /// </summary>
        /// <param name="label">Tab label.</param>
        /// <param name="page">Page contents.</param>
        /// <returns>Newly created control.</returns>
        public TabPage AddPage(string label, ControlBase page = null)
        {
			TabButton button = new TabButton(m_TabStrip);
            var tabpage = new TabPage(this, button);
            button.SetText(label);
            button.Page = tabpage;
            button.IsTabable = false;
            if (page != null)
            {
                tabpage.AddChild(page);
            }
            AddPage(button);
            return tabpage;
        }

        /// <summary>
        /// Adds a page/tab.
        /// </summary>
        /// <param name="button">Page to add. (well, it's a TabButton which is a parent to the page).</param>
        public void AddPage(TabButton button)
        {
            ControlBase page = button.Page;
            page.Parent = this;
            page.IsHidden = true;
            page.Margin = new Margin(6, 6, 6, 6);
            page.Dock = Pos.Fill;

            button.Parent = m_TabStrip;
            button.Dock = Pos.Left;
            if (button.TabControl != null)
                button.TabControl.UnsubscribeTabEvent(button);
            button.TabControl = this;
            button.Clicked += OnTabPressed;

            if (null == m_CurrentButton)
            {
                button.Press();
            }

            if (TabAdded != null)
                TabAdded.Invoke(this, EventArgs.Empty);

            Invalidate();
        }

        private void UnsubscribeTabEvent(TabButton button)
        {
            button.Clicked -= OnTabPressed;
        }

        /// <summary>
        /// Handler for tab selection.
        /// </summary>
        /// <param name="control">Event source (TabButton).</param>
		internal virtual void OnTabPressed(ControlBase control, EventArgs args)
        {
            TabButton button = control as TabButton;
            if (null == button) return;

            ControlBase page = button.Page;
            if (null == page) return;

            if (m_CurrentButton == button)
                return;

            if (null != m_CurrentButton)
            {
                ControlBase page2 = m_CurrentButton.Page;
                if (page2 != null)
                {
                    page2.IsHidden = true;
                }
                m_CurrentButton.Redraw();
                m_CurrentButton = null;
            }

            m_CurrentButton = button;

            page.IsHidden = false;

            m_TabStrip.Invalidate();
            Invalidate();
        }

        /// <summary>
        /// Function invoked after layout.
        /// </summary>
        /// <param name="skin">Skin to use.</param>
        protected override void PrepareLayout()
        {
            base.PrepareLayout();
            HandleOverflow();
        }

        /// <summary>
        /// Handler for tab removing.
        /// </summary>
        /// <param name="button"></param>
        internal virtual void OnLoseTab(TabButton button)
        {
            if (m_CurrentButton == button)
                m_CurrentButton = null;

            //TODO: Select a tab if any exist.

            if (TabRemoved != null)
				TabRemoved.Invoke(this, EventArgs.Empty);

            Invalidate();
        }

        /// <summary>
        /// Number of tabs in the control.
        /// </summary>
        public int TabCount { get { return m_TabStrip.Children.Count; } }

        private void HandleOverflow()
        {
            var TabsSize = m_TabStrip.GetSizeToFitContents();

            // Only enable the scrollers if the tabs are at the top.
            // This is a limitation we should explore.
            // Really TabControl should have derivitives for tabs placed elsewhere where we could specialize 
            // some functions like this for each direction.
            bool needed = TabsSize.Width > Width && m_TabStrip.Dock == Pos.Top;

            m_Scroll[0].IsHidden = !needed;
            m_Scroll[1].IsHidden = !needed;

            if (!needed) return;

            m_ScrollOffset = Util.Clamp(m_ScrollOffset, 0, TabsSize.Width - Width + 32);

            m_TabStrip.Margin = new Margin(m_ScrollOffset*-1, 0, 0, 0);
            m_Scroll[0].SetPosition(Width - 30, 5);
            m_Scroll[1].SetPosition(m_Scroll[0].Right, 5);
        }

        protected virtual void ScrollPressedLeft(ControlBase control, EventArgs args)
        {
            m_ScrollOffset -= 120;
        }

        protected virtual void ScrollPressedRight(ControlBase control, EventArgs args)
        {
            m_ScrollOffset += 120;
        }
    }
}
