using System;
using Gwen.Controls;
namespace TestApplication
{
    public class ButtonTest : ControlTest
    {
        public ButtonTest(ControlBase parent) : base(parent)
        {
            var btn = CreateButton("auto sized button");
            btn.TextPadding = new Gwen.Padding(10, 10, 10, 10);
            btn.AutoSizeToContents = true;
            posy += 50;
            btn = CreateButton("manual sized button");
            posy += 50;
            btn.AutoSizeToContents = false;
            btn = CreateButton("left aligned button");
            posy += 50;
            btn.AutoSizeToContents = false;
            btn.Alignment = Gwen.Pos.Left | Gwen.Pos.CenterV;
            btn = CreateButton("right align button.");
            posy += 50;
            btn.Alignment = Gwen.Pos.Right | Gwen.Pos.CenterV;
            btn.AutoSizeToContents = false;
        }
        private int posx = 0;
        private int posy = 0;
        private Button CreateButton(string text)
        {
            Button btn = new Button(Parent);
            btn.Text = text;
            btn.SetPosition(posx, posy);

            return btn;
        }
    }
}
