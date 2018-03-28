using System;
using System.Diagnostics;
using Gwen.Controls;
using Gwen;
namespace TestApplication
{
    public class TreeTest : ControlTest
    {
        public TreeTest(ControlBase parent) : base(parent)
        {
            TreeControl tree = new TreeControl(parent);
            tree.Dock = Dock.Fill;
            tree.AllowMultiSelect = true;
            for (int i = 0; i < 10; i++)
            {
                var root = tree.AddNode("root " + i);
                if (i ==  0)
                {
                    for (int ix = 0; ix < 100; ix++)
                    {
                        var node = root.AddNode("for large scale");
                    }
                }
                for (int ix = 0; ix < 10; ix++)
                {
                    var node = root.AddNode("child " + ix + " of root " + i);
                    if (i == 0)
                        node.AddNode("subnode that has a really long label so we can trigger the scrollbar................");
                }
                if (i == 1 || i == 5 || i == 8)
                    root.ExpandAll();
            }
            tree.SizeToChildren();
        }
    }
}
