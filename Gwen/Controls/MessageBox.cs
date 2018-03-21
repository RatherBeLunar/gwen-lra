﻿using System;
using System.Drawing;

namespace Gwen.Controls
{
    /// <summary>
    /// Simple message box.
    /// </summary>
    public class MessageBox : WindowControl
    {
        private readonly Button m_Button;
        private readonly Label m_Label; // should be rich label with maxwidth = parent

        /// <summary>
        /// Invoked when the message box has been dismissed.
        /// </summary>
        public GwenEventHandler<EventArgs> Dismissed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBox"/> class.
        /// </summary>
        /// <param name="parent">Parent control.</param>
        /// <param name="text">Message to display.</param>
        /// <param name="caption">Window caption.</param>
        public MessageBox(ControlBase parent, string text, string caption = "") 
            : base(parent, caption, true)
        {
            DeleteOnClose = true;

            m_Label = new Label(m_Panel);
            m_Label.Text = text;
            m_Label.Margin = Margin.Five;
            m_Label.Dock = Pos.Top;
            m_Label.Alignment = Pos.Center;

            m_Button = new Button(m_Panel);
            m_Button.Text = "OK"; // todo: parametrize buttons
            m_Button.Clicked += CloseButtonPressed;
            m_Button.Clicked += DismissedHandler;
            m_Button.Margin = Margin.Five;
            m_Button.SetSize(50, 20);

            Align.Center(this);
        }

		private void DismissedHandler(ControlBase control, EventArgs args)
        {
            if (Dismissed != null)
                Dismissed.Invoke(this, EventArgs.Empty);
        }

        protected override void ProcessLayout(Size size)
        {
            //todo
/*/
            Align.PlaceDownLeft(m_Button, m_Label, 10);
            Align.CenterHorizontally(m_Button);
            m_Panel.SizeToChildren();
            m_Panel.Height += 10;
            SizeToChildren();*/
            base.ProcessLayout(size);
        }
    }
}
