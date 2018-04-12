using System;
using System.Drawing;

namespace Gwen.Controls
{
    public class MessageBox : Gwen.Controls.WindowControl
    {
        private readonly Button m_Button;
        public EventHandler<DialogResult> Dismissed;
        public ControlBase Container;
        public DialogResult Result { get; set; }
        public string Text { get; private set; }
        public bool Modal = false;
        public static MessageBox Show(Canvas canvas, string text, string title, bool cancancel = false)
        {
            var ret = new MessageBox(canvas, text, title, cancancel);
            ret.ShowCentered();
            return ret;
        }
        public MessageBox(Gwen.Controls.ControlBase ctrl, string text, string title, bool cancelbutton = false) : base(ctrl, title)
        {
            var charsize = Skin.Renderer.MeasureText(Skin.DefaultFont, "_").X;
            int maxwidth = charsize * 30;
            int maxwidth2 = charsize * 50;
            var wrapped1 = Skin.DefaultFont.WordWrap(text, maxwidth);
            var wrapped2 = Skin.DefaultFont.WordWrap(text, maxwidth2);
            var wrap = wrapped2;
            // this is a cheat that doesnt work perfectly, but decently for making
            // short messageboxes appear ok
            if (wrapped1.Count == wrapped2.Count)
            {
                wrap = wrapped1;
            }
            foreach (var line in wrap)
            {
                AddLine(line);
            }
            Container = new ControlBase(m_Panel);
            Container.Margin = new Margin(0, 40, 0, 5);
            Container.Dock = Dock.Bottom;
            Container.AutoSizeToContents = true;
            m_Panel.Layout();
            m_Panel.SizeToChildren(true, true);
            SizeToChildren(true, true);
            Layout();
            m_Button = new Button(Container);
            m_Button.Text = "Okay";
            m_Button.Clicked += (o, e) =>
                {
                    Result = DialogResult.OK;
                    Close();
                    DismissedHandler(o, e);
                };
            m_Button.Margin = Margin.One;
            m_Button.Dock = Dock.Right;
            Container.SizeToChildren(false, true);
            if (cancelbutton)
            {
                Button btn = new Button(Container);
                btn.Margin = new Margin(1, 1, 7, 1);
                btn.Dock = Dock.Right;
                btn.Name = "Cancel";
                btn.Text = "Cancel";
                btn.Clicked += (o, e) =>
                {
                    Result = DialogResult.Cancel;
                    Close();
                    DismissedHandler(o, e);
                };
            }
            Align.Center(this);
            Invalidate();
        }

        private void DismissedHandler(ControlBase control, EventArgs args)
        {
            if (Dismissed != null)
                Dismissed.Invoke(this, Result);
        }
        private void AddLine(string line)
        {
            Label add = new Label(m_Panel);
            add.Margin = new Margin(0, 0, 0, 0);
            add.Alignment = Pos.Left | Pos.Top;
            add.Dock = Dock.Top;
            add.AutoSizeToContents = true;
            add.Text = line;
        }
    }
}
