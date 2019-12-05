using LSP.EventBus;
using LSP.EventBus.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo.WinForm46
{
    public partial class Form2 : Form
    {
        private delegate void updateText(string s);

        public Form2()
        {
            InitializeComponent();

            /*EventBusFactory.DefaultEventBus.Subscribe<PayloadEvent<string>>("login", (s) => {
                this.textBox1.Text += s.Payload + Environment.NewLine;
            });*/

            EventBusFactory.DefaultEventBus.Subscribe<PayloadEvent<string>>("async", OnEvent);
            EventBusFactory.DefaultEventBus.Subscribe<PayloadEvent<string>>("sync", OnSync);
        }

        private void OnSync(PayloadEvent<string> obj)
        {
            this.textBox1.Text += obj.Payload + Environment.NewLine;
        }

        private void OnEvent(PayloadEvent<string> obj)
        {
            if (this.textBox1.InvokeRequired)
            {
                this.textBox1.Invoke(new updateText(update), new object[] { obj.Payload });
            }
        }

        private void update(string s)
        {
            this.textBox1.Text += s + "async" + Environment.NewLine;
        }
    }
}
