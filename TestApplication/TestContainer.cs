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
        // window has no font scaling for title
        // on ishidden change, fire mouseup
        public TestContainer(ControlBase parent) : base(parent)
        {
            Dock = Pos.Fill;
            CreateStatusbar();
            Create();
        }
        public void DbgCreate()
        {
            var test = new LabelTest(this);
        }
        public void CreateStatusbar()
        {
            StatusBar sb = new StatusBar(this);
            Label left = new Label(sb);
            left.Text = "Statusbar left";
            sb.AddControl(left, false);

            Button br = new Button(sb);
            br.Text = "Right button";
            sb.AddControl(br, true);
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
            page = tabcontrol.AddPage("Window");
            var window = new WindowTest(page);
            page = tabcontrol.AddPage("Container");
            var container = new ContainerTest(page);
            page = tabcontrol.AddPage("Menu");
            var menu = new MenuTest(page);
            page = tabcontrol.AddPage("Property");
            var prop = new PropertyTest(page);
            page = tabcontrol.AddPage("Slider");
            var slider = new SliderTest(page);
            page=tabcontrol.AddPage("Category");
            var cat = new CategoryTest(page);
            page.FocusTab();
        }
    }
}
