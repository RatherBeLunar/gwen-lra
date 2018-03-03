using System;
using Gwen;
using Gwen.Controls;
using TestApplication.Tests;
namespace TestApplication
{
    public class TestContainer : ControlBase
    {
        public TestContainer(ControlBase parent) : base(parent)
        {
            Dock = Pos.Fill;
            Create();
        }
        public void DbgCreate()
        {
            var label = new LabelTest(this);
        }
        public void Create()
        {
            TabControl tabcontrol = new TabControl(this);
            tabcontrol.Dock = Pos.Fill;
            var page = tabcontrol.AddPage("Labels");

            var label = new LabelTest(page);
            page = tabcontrol.AddPage("Buttons");
            ButtonTest btn = new ButtonTest(page);
        }
    }
}
