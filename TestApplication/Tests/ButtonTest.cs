using System;
using System.Diagnostics;
using Gwen.Controls;
namespace TestApplication
{
    public class ButtonTest : ControlTest
    {
        public ButtonTest(ControlBase parent) : base(parent)
        {
            var btn = CreateButton("auto sized button");
            btn.AutoSizeToContents = true;
            posy += 50;
            btn = CreateButton("auto sized padding button");
            btn.Padding = new Gwen.Padding(10, 10, 10, 10);
            btn.AutoSizeToContents = true;
            posy += 50;
            btn = CreateButton("Event Test Button");
            btn.Padding = new Gwen.Padding(10, 10, 10, 10);
            btn.AutoSizeToContents = true;
            btn.Clicked += (o, e) =>
            {
                Console.WriteLine("Clicked");
            };
            btn.DoubleClicked += (o, e) =>
            {
                Console.WriteLine("Double Clicked");
            };
            btn.Pressed += (o, e) =>
            {
                Console.WriteLine("Pressed");
            };
            btn.Released += (o, e) =>
            {
                Console.WriteLine("Released");
            };
            posy += 50;
            btn = CreateButton("manual sized button");
            btn.Width += 100;
            posy += 50;
            btn.AutoSizeToContents = false;
            btn = CreateButton("left aligned button");
            btn.Width += 50;
            posy += 50;
            btn.AutoSizeToContents = false;
            btn.Alignment = Gwen.Pos.Left | Gwen.Pos.CenterV;
            btn = CreateButton("right align button.");
            btn.Width += 50;
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
