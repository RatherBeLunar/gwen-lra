using System;
using System.Drawing;

namespace Gwen.Controls
{
    public class MessageBox : Gwen.Controls.WindowControl
    {
        private readonly Button m_Button;
        public EventHandler<EventArgs> Dismissed;
        public ControlBase Container;
        public System.Windows.Forms.DialogResult Result { get; set; }
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
            var wrapped = Skin.DefaultFont.WordWrap(text, 200);
            foreach (var line in wrapped)
            {
                AddLine(line);
            }
            Container = new ControlBase(m_Panel);
            Container.Margin = new Margin(0, 40, 0, 5);
            Container.Dock = Dock.Bottom;
            Container.Height = 30;
            m_Panel.Layout();
            m_Panel.SizeToChildren(true, true);
            SizeToChildren(true, true);
            Layout();
            m_Button = new Button(Container);
            m_Button.Text = "Okay";
            m_Button.Clicked += (o, e) =>
                {
                    Result = System.Windows.Forms.DialogResult.OK;
                    Close();
                    DismissedHandler(o, e);
                };
            m_Button.Margin = Margin.One;
            m_Button.Width = 70;
            m_Button.Dock = Dock.Right;
            Container.SizeToChildren(false, true);
            if (cancelbutton)
            {
                Button btn = new Button(Container);
                btn.Margin = new Margin(1, 1, 7, 1);
                btn.Dock = Dock.Right;
                btn.Name = "Cancel";
                btn.Text = "Cancel";
                btn.Width = 70;
                btn.Clicked += (o, e) =>
                {
                    Result = System.Windows.Forms.DialogResult.Cancel;
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
                Dismissed.Invoke(this, EventArgs.Empty);
        }
        private void AddLine(string line)
        {
            Label add = new Label(m_Panel);
            add.Margin = new Margin(0, 0, 0, 0);
            add.Alignment = Pos.CenterH | Pos.Top;
            add.Dock = Dock.Top;
            add.AutoSizeToContents = true;
            add.Text = line;
        }
    }
}
