using System;
using Gwen.Controls;
using Gwen;

namespace TestApplication.Tests
{
    public class LabelTest : ControlTest
    {
        public LabelTest(ControlBase parent) : base(parent)
        {
            CreateAlign(Pos.Top | Pos.Left);
            CreateAlign(Pos.Top | Pos.CenterH);
            CreateAlign(Pos.Top | Pos.Right);
            row++;
            counter = 0;
            CreateAlign(Pos.Left | Pos.CenterV);
            CreateAlign(Pos.Center);
            CreateAlign(Pos.Right | Pos.CenterV);
            row++;
            counter = 0;
            CreateAlign(Pos.Bottom | Pos.Left);
            CreateAlign(Pos.Bottom | Pos.CenterH);
            CreateAlign(Pos.Bottom | Pos.Right);
            row++;
            Create("Autosized label");
            row++;
            var label = Create("Autosize, padding, centerh");
            label.TextPadding = new Padding(10, 10, 10, 10);
            label.Alignment = Pos.CenterH;
            row++;
            label = Create("Autosize, padding, centerv");
            label.TextPadding = new Padding(10, 10, 10, 10);
            label.Alignment = Pos.CenterV;
            label.SizeToChildren();
            row++;
            label = Create("Autosize, padding, left");
            label.TextPadding = new Padding(10, 10, 10, 10);
            label.Alignment = Pos.Left;
            row++;
            label = Create("Autosize, padding, right");
            label.TextPadding = new Padding(10, 10, 10, 10);
            label.Alignment = Pos.Right;
            row++;
        }
        private Label Create(string text)
        {
            Label label = new Label(Parent);
            label.Y += row * 50;
            label.Text = text;
            label.DrawDebugOutlines = true;
            return label;
        }
        private int row = 0;
        private int counter = 0;
        private void CreateAlign(Pos align)
        {
            Label label = new Label(Parent);
            label.SetSize(50, 50);
            string text = "";
            if (align.HasFlag(Pos.Top))
                text += "T";
            if (align.HasFlag(Pos.Bottom))
                text += "B";
            if (align.HasFlag(Pos.Right))
                text += "R";
            if (align.HasFlag(Pos.Left))
                text += "L";
            if (align == Pos.Center)
            {
                text = "C";
            }
            label.Text = text;
            label.Alignment = align;
            label.X += 50 * counter++;
            label.Y += 50 * row;
            label.AutoSizeToContents = false;
            label.DrawDebugOutlines = true;
        }
    }
}
