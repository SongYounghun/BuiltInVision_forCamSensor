using GalvoScanner.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GalvoScanner.IO.DialogIO
{
    public partial class IOControl : Form
    {
        IDIO m_io = DIOBase.GetInstanceInterface();

        public IOControl()
        {
            InitializeComponent();
        }

        private void IOControl_Load(object sender, EventArgs e)
        {
            if (m_io == null)
            {
                Close();
                return;
            }

            DIOBase io = (DIOBase)m_io;

            m_io.LoadIOMap(DefPath.IOMap);
            for (int i = 0; i < m_io.GetInputCount(); i++)
            {
                ListViewItem item = new ListViewItem(String.Format("{0}", i));
                item.SubItems.Add(io.ListInputNames[i]);
                item.BackColor = Color.White;
                listView_input.Items.Add(item);
            }

            for (int i = 0; i < m_io.GetOutputCount(); i++)
            {
                ListViewItem item = new ListViewItem(String.Format("{0}", i));
                item.SubItems.Add(io.ListOutputNames[i]);
                item.BackColor = Color.White;
                listView_output.Items.Add(item);
            }
        }

        private void IOControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(Application.StartupPath.ToString() + "\\Config");
                if (!di.Exists)
                {
                    di.Create();
                }

                m_io.SaveIOMap(DefPath.IOMap);

            }
            catch (Exception E)
            {
                throw E;
            }

            e.Cancel = true;
            Hide();
        }

        private void listView_output_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView_output.SelectedItems.Count == 1)
            {
                ListView.SelectedListViewItemCollection items = listView_output.SelectedItems;
                ListViewItem lvItem = items[0];

                int ind = Convert.ToInt32(lvItem.SubItems[0].Text);

                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    IONameSetting nameSetting = new IONameSetting();
                    nameSetting.IOIndex = ind;
                    nameSetting.IOName = lvItem.SubItems[1].Text;
                    if (nameSetting.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        lvItem.SubItems[1].Text = nameSetting.IOName;
                        DIOBase io = (DIOBase)m_io;
                        io.ListOutputNames[ind] = nameSetting.IOName;
                    }
                }
                else if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    if (lvItem.Checked)
                    {
                        m_io.WriteOutBit(ind, 1);
                        lvItem.BackColor = Color.YellowGreen;
                    }
                    else
                    {
                        m_io.WriteOutBit(ind, 0);
                        lvItem.BackColor = Color.White;
                    }
                }
            }

        }

        private void listView_output_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            int ind = Convert.ToInt32(e.Item.SubItems[0].Text);
            if (e.Item.Checked)
            {
                m_io.WriteOutBit(ind, 1);
                e.Item.BackColor = Color.YellowGreen;
            }
            else
            {
                m_io.WriteOutBit(ind, 0);
                e.Item.BackColor = Color.White;
            }

        }

        private void timer_inputMonitor_Tick(object sender, EventArgs e)
        {
            int inputCnt = m_io.GetInputCount();
            for (int i = 0; i < inputCnt; i++)
            {
                uint value = 0;
                m_io.ReadInBit(i, ref value);

                if (value == 0)
                {
                    listView_input.Items[i].Checked = false;
                    listView_input.Items[i].BackColor = Color.White;
                }
                else
                {
                    listView_input.Items[i].Checked = true;
                    listView_input.Items[i].BackColor = Color.YellowGreen;
                }
            }
        }

        private void IOControl_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void timer_outputMonitor_Tick(object sender, EventArgs e)
        {
            int outputCnt = m_io.GetOutputCount();
            for (int i = 0; i < outputCnt; i++)
            {
                uint value = 0;
                m_io.ReadOutBit(i, ref value);

                if (value == 0)
                {
                    listView_output.Items[i].Checked = false;
                    listView_output.Items[i].BackColor = Color.White;
                }
                else
                {
                    listView_output.Items[i].Checked = true;
                    listView_output.Items[i].BackColor = Color.YellowGreen;
                }
            }
        }
    }
}
