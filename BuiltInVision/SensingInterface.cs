using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BuiltInVision
{
    public partial class SensingInterface : Form
    {
        public SensingInterface()
        {
            InitializeComponent();
        }

        private void SensingInterface_Load(object sender, EventArgs e)
        {

        }

        private void SensingInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }        
    }
}
