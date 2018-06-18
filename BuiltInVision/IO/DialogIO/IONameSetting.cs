using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalvoScanner.IO.DialogIO
{
    public partial class IONameSetting : Form
    {
        int m_nIndex = 0;
        public int IOIndex
        {
            get { return m_nIndex; }
            set { m_nIndex = value; }
        }

        string m_strName = "";
        public string IOName
        {
            get { return m_strName; }
            set { m_strName = value; }
        }

        public IONameSetting()
        {
            InitializeComponent();
        }

        private void IONameSetting_Load(object sender, EventArgs e)
        {
            label_io_index.Text = Convert.ToString(m_nIndex);
            textBox_name.Text = m_strName;
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            m_strName = textBox_name.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}
