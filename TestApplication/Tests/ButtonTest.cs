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
            btn.Tooltip = "With tooltip";
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
            btn = CreateButton("disabled");
            btn.IsDisabled = true;
            btn.Width += 50;
            posy += 50;
            btn = CreateButton("toggle");
            btn.IsToggle = true;
            btn.Toggle();
            btn.Width += 50;
            posy += 50;
            btn = CreateButton("left aligned button");
            btn.Width += 50;
            posy += 50;
            btn.AutoSizeToContents = false;
            btn.Alignment = Gwen.Pos.Left | Gwen.Pos.CenterV;
            btn = CreateButton("right align button.");
            btn.Width += 50;
            posy += 30;
            btn.Alignment = Gwen.Pos.Right | Gwen.Pos.CenterV;
            btn.AutoSizeToContents = false;
            Checkbox checkbox = new Checkbox(Parent);
            checkbox.SetPosition(posx, posy);
            posy += 30;
            Checkbox checkedbox = new Checkbox(Parent);
            checkedbox.SetPosition(posx, posy);
            checkedbox.IsChecked = true;
            checkedbox.Text = "checked";
            posy += 30;
            Checkbox lcheckbox = new Checkbox(Parent);
            lcheckbox.Text = "checkbox";
            lcheckbox.SetPosition(posx, posy);
            Checkbox disabledlcheckbox = new Checkbox(Parent);
            disabledlcheckbox.Text = "disabled";
            disabledlcheckbox.Disable();
            disabledlcheckbox.SetPosition(posx + 100, posy);
            disabledlcheckbox = new Checkbox(Parent);
            disabledlcheckbox.Text = "disabled";
            disabledlcheckbox.IsChecked = true;
            disabledlcheckbox.Disable();
            disabledlcheckbox.SetPosition(posx + 200, posy);
            posy += 30;
            RadioButtonGroup group = new RadioButtonGroup(Parent);
            group.Text = "radio button group";
            group.SetPosition(posx, posy);
            group.AddOption("Radio 1").Tooltip = "tooltip 1";
            group.AddOption("Radio 2").Tooltip = "tooltip 2";
            var dc = group.AddOption("disabledChecked");
            dc.IsChecked = true;
            dc.Disable();
            group.AddOption("disabled").Disable();
            posy += 100;
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
