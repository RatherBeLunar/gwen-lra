using System;
using System.Diagnostics;
using Gwen.Controls;
using Gwen;
namespace TestApplication
{
    public class ProgressBarTest : ControlTest
    {

        public ProgressBarTest(ControlBase parent) : base(parent)
        {
            ProgressBar p = new ProgressBar(parent);
            p.AutoLabel = true;
            ProgressBar p2 = new ProgressBar(parent);
            p2.Y += 100;
            p2.AutoLabel = false;
            p2.Height = 32;
            p2.Value = 0.025f;
            ProgressBar p3 = new ProgressBar(parent);
            p3.AutoLabel = false;
            p3.IsHorizontal = false;
            p3.Y += 100;
            p3.X += 100;
            p3.Width = 15;
            p3.Height = 100;
            // p.SizeToChildren(false,true);
            new System.Threading.Thread(() =>
            {
                while (true)
                {
                    System.Threading.Thread.Sleep(100);
                    p.Value += 0.01f;
                    if (p.Value >= 1)
                        p.Value = 0;
                    // p2.Value = p.Value;
                    p3.Value = p.Value;
                }
            })
            { IsBackground = true }.Start();
        }
    }
}
