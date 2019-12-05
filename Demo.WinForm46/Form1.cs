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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            new Form2().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EventBusFactory.DefaultEventBus.Publish("async", new PayloadEvent<string>("hello"), TimeSpan.Zero);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EventBusFactory.DefaultEventBus.Publish("sync", new PayloadEvent<string>("world"));
        }
    }
}
