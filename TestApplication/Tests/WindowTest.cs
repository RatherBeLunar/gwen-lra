using System;
using System.Diagnostics;
using Gwen.Controls;
using Gwen;
namespace TestApplication
{
    public class WindowTest : ControlTest
    {
        public WindowTest(ControlBase parent) : base(parent)
        {
            Button btn = new Button(parent);
            btn.Text = "Open Window";
            btn.Clicked += (sender, arguments) =>
            {
                CreateWindow();
            };
            btn = new Button(parent);
            btn.Y += 50;
            btn.Text = "Open Modal";
            btn.Clicked += (sender, arguments) =>
            {
                CreateModal(false);
            };
            btn = new Button(parent);
            btn.Y += 100;
            btn.Text = "Open Modal with dim";
            btn.Clicked += (sender, arguments) =>
            {
                CreateModal(true);
            };
            btn = new Button(parent);
            btn.Y += 150;
            btn.Text = "Open Messagebox";
            btn.Clicked += (sender, arguments) =>
            {
                MessageBox mb = new MessageBox(Parent,"This is a test for a messagebox, It word wraps and all that jazz.","Caption",true);
            };
        }
        private void CreateWindow()
        {
            WindowControl win = new WindowControl(Parent, "Hello World");
            win.SetSize(100, 100);
            Button close = new Button(win);
            close.Text = "Close";
            close.SetPosition(200,200);
            win.SizeToChildren();
            close.Clicked += (sender, arguments) => win.Close();
        }
        private void CreateModal(bool dim)
        {
            WindowControl win = new WindowControl(Parent, "Modal", !dim);
            if (dim)
            win.MakeModal(true);
            win.SetSize(100, 100);
            Button close = new Button(win);
            close.Text = "Close";
            close.Clicked += (sender, arguments) => win.Close();
            Button add = new Button(win);
            add.Text = "Add New";
            add.Clicked += (sender, arguments) => CreateWindow();
        }
    }
}
