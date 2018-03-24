using System;
using System.Diagnostics;
using Gwen.Controls;
using Gwen;
namespace TestApplication
{
    public class PropertyTest : ControlTest
    {
        public PropertyTest(ControlBase parent) : base(parent)
        {
            PropertyTable table = new PropertyTable(parent);
            table.AutoSizeToContents = true;
            table.Width=100;
            table.Height = 100;
            InitTable(table);
            PropertyTree tree = new PropertyTree(parent);
            tree.DrawDebugOutlines = true;
            tree.Height = 100;
            tree.Width = 200;
            tree.Y = 205;
            var t = tree.Add("Tree Test");
            InitTable(t);
            t = tree.Add("Tree Test 2");
            InitTable(t);
            InitTable(t);
        }
        private void InitTable(PropertyTable table)
        {
            table.Add("text", "val");
            table.Add("check", new CheckProperty(null));
            table.Add("Key", new KeyProperty(table));
        }
    }
}
