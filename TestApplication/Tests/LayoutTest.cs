using System;
using System.Diagnostics;
using Gwen.Controls;
using Gwen;
namespace TestApplication
{
    public class LayoutTest : ControlTest
    {
        ControlBase Container;
        public LayoutTest(ControlBase parent) : base(parent)
        {
            Container = new ControlBase(parent);
            Container.Dock = Pos.Fill;
            Container.Padding = new Padding(0,50,0,0);
            var btn = CreateButton(Pos.Left);
            btn = CreateButton(Pos.Top, "margin bottom");
            btn.Margin = new Margin(0, 0, 0, 10);
            CreateButton(Pos.Right);
            CreateButton(Pos.Bottom);
            btn = CreateButton(Pos.Left, "margin 8");
            btn.Margin = Margin.Eight;
            btn = CreateButton(Pos.Left);
            btn = CreateButton(Pos.Right, "side margin");
            btn.Margin = new Margin(5, 0, 5, 0);
            Button button = new Button(Container);
            button.Text = "Fill Click to reorganize";
            button.Clicked += (o, e) =>
            {
                var child = (Button)Container.Children[Container.Children.Count - 1];
                child.SendToBack();
            };
            button.Dock = Pos.Fill;
        }
        private Button CreateButton(Pos dock, string info = "")
        {
            Button button = new Button(Container);
            button.Text = Enum.GetName(typeof(Pos), dock) + " " + info;
            button.Dock = dock;
            return button;
        }
    }
}
