namespace GalvoScanner.IO.DialogIO
{
    partial class IOControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer_IO = new System.Windows.Forms.SplitContainer();
            this.listView_input = new System.Windows.Forms.ListView();
            this.columnHeader_in_index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_in_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView_output = new System.Windows.Forms.ListView();
            this.columnHeader_out_index = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_out_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer_inputMonitor = new System.Windows.Forms.Timer(this.components);
            this.timer_outputMonitor = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_IO)).BeginInit();
            this.splitContainer_IO.Panel1.SuspendLayout();
            this.splitContainer_IO.Panel2.SuspendLayout();
            this.splitContainer_IO.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer_IO
            // 
            this.splitContainer_IO.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer_IO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_IO.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_IO.Name = "splitContainer_IO";
            // 
            // splitContainer_IO.Panel1
            // 
            this.splitContainer_IO.Panel1.Controls.Add(this.listView_input);
            // 
            // splitContainer_IO.Panel2
            // 
            this.splitContainer_IO.Panel2.Controls.Add(this.listView_output);
            this.splitContainer_IO.Size = new System.Drawing.Size(718, 612);
            this.splitContainer_IO.SplitterDistance = 356;
            this.splitContainer_IO.TabIndex = 0;
            // 
            // listView_input
            // 
            this.listView_input.CheckBoxes = true;
            this.listView_input.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_in_index,
            this.columnHeader_in_name});
            this.listView_input.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_input.FullRowSelect = true;
            this.listView_input.GridLines = true;
            this.listView_input.Location = new System.Drawing.Point(0, 0);
            this.listView_input.Name = "listView_input";
            this.listView_input.Size = new System.Drawing.Size(352, 608);
            this.listView_input.TabIndex = 0;
            this.listView_input.UseCompatibleStateImageBehavior = false;
            this.listView_input.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_in_index
            // 
            this.columnHeader_in_index.Text = "Input index";
            this.columnHeader_in_index.Width = 84;
            // 
            // columnHeader_in_name
            // 
            this.columnHeader_in_name.Text = "name";
            this.columnHeader_in_name.Width = 243;
            // 
            // listView_output
            // 
            this.listView_output.CheckBoxes = true;
            this.listView_output.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_out_index,
            this.columnHeader_out_name});
            this.listView_output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView_output.FullRowSelect = true;
            this.listView_output.GridLines = true;
            this.listView_output.Location = new System.Drawing.Point(0, 0);
            this.listView_output.MultiSelect = false;
            this.listView_output.Name = "listView_output";
            this.listView_output.Size = new System.Drawing.Size(354, 608);
            this.listView_output.TabIndex = 0;
            this.listView_output.UseCompatibleStateImageBehavior = false;
            this.listView_output.View = System.Windows.Forms.View.Details;
            this.listView_output.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listView_output_ItemChecked);
            this.listView_output.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_output_MouseDoubleClick);
            // 
            // columnHeader_out_index
            // 
            this.columnHeader_out_index.Text = "Output index";
            this.columnHeader_out_index.Width = 86;
            // 
            // columnHeader_out_name
            // 
            this.columnHeader_out_name.Text = "name";
            this.columnHeader_out_name.Width = 224;
            // 
            // timer_inputMonitor
            // 
            this.timer_inputMonitor.Enabled = true;
            this.timer_inputMonitor.Tick += new System.EventHandler(this.timer_inputMonitor_Tick);
            // 
            // timer_outputMonitor
            // 
            this.timer_outputMonitor.Enabled = true;
            this.timer_outputMonitor.Tick += new System.EventHandler(this.timer_outputMonitor_Tick);
            // 
            // IOControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 612);
            this.Controls.Add(this.splitContainer_IO);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IOControl";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IOControl";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.IOControl_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.IOControl_FormClosed);
            this.Load += new System.EventHandler(this.IOControl_Load);
            this.splitContainer_IO.Panel1.ResumeLayout(false);
            this.splitContainer_IO.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_IO)).EndInit();
            this.splitContainer_IO.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer_IO;
        private System.Windows.Forms.ListView listView_input;
        private System.Windows.Forms.ListView listView_output;
        private System.Windows.Forms.ColumnHeader columnHeader_in_index;
        private System.Windows.Forms.ColumnHeader columnHeader_in_name;
        private System.Windows.Forms.ColumnHeader columnHeader_out_index;
        private System.Windows.Forms.ColumnHeader columnHeader_out_name;
        private System.Windows.Forms.Timer timer_inputMonitor;
        private System.Windows.Forms.Timer timer_outputMonitor;

    }
}