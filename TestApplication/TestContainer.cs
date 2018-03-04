using System;
using Gwen;
using Gwen.Controls;
using TestApplication.Tests;
namespace TestApplication
{
    public class TestContainer : ControlBase
    {
        //todo
        // scroll control needs to be not set by percent by controls (tree control scrolls slow af)
        // tree control multi select should only be if holding ctrl, right?
        public TestContainer(ControlBase parent) : base(parent)
        {
            Dock = Pos.Fill;
            Create();
        }
        public void DbgCreate()
        {
            var tree = new TreeTest(this);
        }
        public void Create()
        {
            TabControl tabcontrol = new TabControl(this);
            tabcontrol.Dock = Pos.Fill;

            var page = tabcontrol.AddPage("Labels");
            var label = new LabelTest(page);

            page = tabcontrol.AddPage("Buttons");
            var btn = new ButtonTest(page);

            page = tabcontrol.AddPage("Tab Control");
            var tab = new TabTest(page);

            page = tabcontrol.AddPage("Layout");
            var layout = new LayoutTest(page);

            page = tabcontrol.AddPage("TreeControl");
            var tree = new TreeTest(page);

            page = tabcontrol.AddPage("Textbox");
            var textbox = new TextBoxTest(page);
            page.FocusTab();
        }
    }
}
