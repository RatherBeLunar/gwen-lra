using System;
using System.Diagnostics;
using Gwen.Controls;
using Gwen;
namespace TestApplication
{
    public class TextBoxTest : ControlTest
    {
        public TextBoxTest(ControlBase parent) : base(parent)
        {
            TextBox tb = new TextBox(parent);
            tb.Width = 100;
            tb.SetPosition(0, 100);
            tb.Text = "You can write in me";
            tb = new TextBox(parent);
            tb.Width = 100;
            tb.SetPosition(0, 200);
            tb.Height = 50;
            tb.Text = "my size is different";
            var mtb = new MultilineTextBox(parent);
            mtb.Width = 100;
            mtb.SetPosition(0, 300);
            mtb.Height = 50;
            mtb.Text = "im multilined. lol..";
            var nud = new NumericUpDown(parent);
            nud.SetPosition(0, 400);
            nud.Min = 0;
            nud.Max = 10;
            nud = new NumericUpDown(parent);
            nud.SetPosition(0, 500);
            nud.Min = -10;
            nud.Max = 10;
            nud.Disable();
        }
    }
}
