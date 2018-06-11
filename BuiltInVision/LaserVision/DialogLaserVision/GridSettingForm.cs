using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalvoScanner.LaserCanvas.DialogLaserGalvo
{
    public partial class GridSettingForm : Form
    {
        private double m_gridStep = 10;
        public double GridStep
        {
            get { return m_gridStep; }
            set { m_gridStep = value; }
        }

        public GridSettingForm()
        {
            InitializeComponent();
        }

        public GridSettingForm(double gridStep)
        {
            InitializeComponent();
            m_gridStep = gridStep;
        }

        private void GridSettingForm_Load(object sender, EventArgs e)
        {
            try
            {
                textBox_grid_step.Text = String.Format("{0}", m_gridStep);
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void button_set_Click(object sender, EventArgs e)
        {
            try
            {
                GridStep = Double.Parse(textBox_grid_step.Text.ToString());
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
            }
        }

        private void textBox_grid_step_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == Convert.ToChar(Keys.Return))
                {
                    GridStep = Double.Parse(textBox_grid_step.Text.ToString());
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Close();
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
            }
        }
    }
}
