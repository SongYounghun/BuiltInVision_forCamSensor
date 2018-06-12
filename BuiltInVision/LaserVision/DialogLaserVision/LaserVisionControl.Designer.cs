namespace GalvoScanner.LaserVision.DialogLaserVision
{
    partial class LaserVisionControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_one_shot = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_continuous_stop = new System.Windows.Forms.Button();
            this.button_continuous = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.button_Line = new System.Windows.Forms.Button();
            this.button_Straight = new System.Windows.Forms.Button();
            this.button_file_loadImage = new System.Windows.Forms.Button();
            this.button_file_saveImage = new System.Windows.Forms.Button();
            this.button_new_vision_recipe = new System.Windows.Forms.Button();
            this.textBox_recipe_path = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_file_loadRecipe = new System.Windows.Forms.Button();
            this.button_file_saveRecipe = new System.Windows.Forms.Button();
            this.checkBox_vision_visible = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_result_viewer = new System.Windows.Forms.Button();
            this.button_processing_viewer = new System.Windows.Forms.Button();
            this.button_origin_viewer = new System.Windows.Forms.Button();
            this.button_result_reset = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button_hough_line = new System.Windows.Forms.Button();
            this.button_fit_circle = new System.Windows.Forms.Button();
            this.button_one_cycle = new System.Windows.Forms.Button();
            this.button_list_clear = new System.Windows.Forms.Button();
            this.button_processing_result = new System.Windows.Forms.Button();
            this.listView_image_process = new System.Windows.Forms.ListView();
            this.button_template_match = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox_markobj_index = new System.Windows.Forms.CheckBox();
            this.checkBox_markgroup_index = new System.Windows.Forms.CheckBox();
            this.textBox_markgroup_index = new System.Windows.Forms.TextBox();
            this.button_moveNmark_obj = new System.Windows.Forms.Button();
            this.button_mark_obj = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_markobj_index = new System.Windows.Forms.TextBox();
            this.button_move_obj = new System.Windows.Forms.Button();
            this.checkBox_autosave_enable = new System.Windows.Forms.CheckBox();
            this.textBox_autosave_path = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_set_path_folder_autosave = new System.Windows.Forms.Button();
            this.textBox_autosave_interval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_one_shot
            // 
            this.button_one_shot.Location = new System.Drawing.Point(6, 20);
            this.button_one_shot.Name = "button_one_shot";
            this.button_one_shot.Size = new System.Drawing.Size(82, 23);
            this.button_one_shot.TabIndex = 1;
            this.button_one_shot.Text = "One shot";
            this.button_one_shot.UseVisualStyleBackColor = true;
            this.button_one_shot.Click += new System.EventHandler(this.button_one_shot_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.button_continuous_stop);
            this.groupBox1.Controls.Add(this.button_continuous);
            this.groupBox1.Controls.Add(this.button_one_shot);
            this.groupBox1.Location = new System.Drawing.Point(3, 33);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(302, 52);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Camera";
            // 
            // button_continuous_stop
            // 
            this.button_continuous_stop.Location = new System.Drawing.Point(182, 20);
            this.button_continuous_stop.Name = "button_continuous_stop";
            this.button_continuous_stop.Size = new System.Drawing.Size(82, 23);
            this.button_continuous_stop.TabIndex = 3;
            this.button_continuous_stop.Text = "Stop";
            this.button_continuous_stop.UseVisualStyleBackColor = true;
            this.button_continuous_stop.Click += new System.EventHandler(this.button_continuous_stop_Click);
            // 
            // button_continuous
            // 
            this.button_continuous.Location = new System.Drawing.Point(94, 20);
            this.button_continuous.Name = "button_continuous";
            this.button_continuous.Size = new System.Drawing.Size(82, 23);
            this.button_continuous.TabIndex = 2;
            this.button_continuous.Text = "Continuous";
            this.button_continuous.UseVisualStyleBackColor = true;
            this.button_continuous.Click += new System.EventHandler(this.button_continuous_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.button_Clear);
            this.groupBox2.Controls.Add(this.button_Line);
            this.groupBox2.Controls.Add(this.button_Straight);
            this.groupBox2.Controls.Add(this.button_file_loadImage);
            this.groupBox2.Controls.Add(this.button_file_saveImage);
            this.groupBox2.Location = new System.Drawing.Point(4, 91);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(302, 74);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "File";
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(182, 22);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(82, 23);
            this.button_Clear.TabIndex = 4;
            this.button_Clear.Text = "Clear";
            this.button_Clear.UseVisualStyleBackColor = true;
            this.button_Clear.Click += new System.EventHandler(this.button_Clear_Click);
            // 
            // button_Line
            // 
            this.button_Line.Location = new System.Drawing.Point(6, 22);
            this.button_Line.Name = "button_Line";
            this.button_Line.Size = new System.Drawing.Size(82, 23);
            this.button_Line.TabIndex = 3;
            this.button_Line.Text = "Line";
            this.button_Line.UseVisualStyleBackColor = true;
            this.button_Line.Click += new System.EventHandler(this.button_Line_Click);
            // 
            // button_Straight
            // 
            this.button_Straight.Location = new System.Drawing.Point(94, 22);
            this.button_Straight.Name = "button_Straight";
            this.button_Straight.Size = new System.Drawing.Size(82, 23);
            this.button_Straight.TabIndex = 3;
            this.button_Straight.Text = "Straight";
            this.button_Straight.UseVisualStyleBackColor = true;
            this.button_Straight.Click += new System.EventHandler(this.button_Straight_Click);
            // 
            // button_file_loadImage
            // 
            this.button_file_loadImage.Location = new System.Drawing.Point(139, 47);
            this.button_file_loadImage.Name = "button_file_loadImage";
            this.button_file_loadImage.Size = new System.Drawing.Size(125, 23);
            this.button_file_loadImage.TabIndex = 2;
            this.button_file_loadImage.Text = "Load image";
            this.button_file_loadImage.UseVisualStyleBackColor = true;
            this.button_file_loadImage.Click += new System.EventHandler(this.button_file_loadImage_Click);
            // 
            // button_file_saveImage
            // 
            this.button_file_saveImage.Location = new System.Drawing.Point(6, 47);
            this.button_file_saveImage.Name = "button_file_saveImage";
            this.button_file_saveImage.Size = new System.Drawing.Size(125, 23);
            this.button_file_saveImage.TabIndex = 1;
            this.button_file_saveImage.Text = "Save image";
            this.button_file_saveImage.UseVisualStyleBackColor = true;
            this.button_file_saveImage.Click += new System.EventHandler(this.button_file_saveImage_Click);
            // 
            // button_new_vision_recipe
            // 
            this.button_new_vision_recipe.Location = new System.Drawing.Point(9, 166);
            this.button_new_vision_recipe.Name = "button_new_vision_recipe";
            this.button_new_vision_recipe.Size = new System.Drawing.Size(82, 23);
            this.button_new_vision_recipe.TabIndex = 9;
            this.button_new_vision_recipe.Text = "New";
            this.button_new_vision_recipe.UseVisualStyleBackColor = true;
            this.button_new_vision_recipe.Visible = false;
            this.button_new_vision_recipe.Click += new System.EventHandler(this.button_new_vision_recipe_Click);
            // 
            // textBox_recipe_path
            // 
            this.textBox_recipe_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_recipe_path.Location = new System.Drawing.Point(97, 195);
            this.textBox_recipe_path.Name = "textBox_recipe_path";
            this.textBox_recipe_path.ReadOnly = true;
            this.textBox_recipe_path.Size = new System.Drawing.Size(200, 21);
            this.textBox_recipe_path.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 198);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Recipe path : ";
            // 
            // button_file_loadRecipe
            // 
            this.button_file_loadRecipe.Location = new System.Drawing.Point(164, 166);
            this.button_file_loadRecipe.Name = "button_file_loadRecipe";
            this.button_file_loadRecipe.Size = new System.Drawing.Size(135, 23);
            this.button_file_loadRecipe.TabIndex = 4;
            this.button_file_loadRecipe.Text = "Load recipe";
            this.button_file_loadRecipe.UseVisualStyleBackColor = true;
            this.button_file_loadRecipe.Click += new System.EventHandler(this.button_file_loadRecipe_Click);
            // 
            // button_file_saveRecipe
            // 
            this.button_file_saveRecipe.Location = new System.Drawing.Point(10, 166);
            this.button_file_saveRecipe.Name = "button_file_saveRecipe";
            this.button_file_saveRecipe.Size = new System.Drawing.Size(135, 23);
            this.button_file_saveRecipe.TabIndex = 3;
            this.button_file_saveRecipe.Text = "Save recipe";
            this.button_file_saveRecipe.UseVisualStyleBackColor = true;
            this.button_file_saveRecipe.Click += new System.EventHandler(this.button_file_saveRecipe_Click);
            // 
            // checkBox_vision_visible
            // 
            this.checkBox_vision_visible.AutoSize = true;
            this.checkBox_vision_visible.Checked = true;
            this.checkBox_vision_visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_vision_visible.Location = new System.Drawing.Point(9, 11);
            this.checkBox_vision_visible.Name = "checkBox_vision_visible";
            this.checkBox_vision_visible.Size = new System.Drawing.Size(99, 16);
            this.checkBox_vision_visible.TabIndex = 5;
            this.checkBox_vision_visible.Text = "Vision visible";
            this.checkBox_vision_visible.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.button_result_viewer);
            this.groupBox3.Controls.Add(this.button_processing_viewer);
            this.groupBox3.Controls.Add(this.button_origin_viewer);
            this.groupBox3.Location = new System.Drawing.Point(4, 226);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(302, 52);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Image viewer";
            // 
            // button_result_viewer
            // 
            this.button_result_viewer.Location = new System.Drawing.Point(174, 20);
            this.button_result_viewer.Name = "button_result_viewer";
            this.button_result_viewer.Size = new System.Drawing.Size(78, 23);
            this.button_result_viewer.TabIndex = 4;
            this.button_result_viewer.Text = "Result";
            this.button_result_viewer.UseVisualStyleBackColor = true;
            this.button_result_viewer.Click += new System.EventHandler(this.button_result_viewer_Click_1);
            // 
            // button_processing_viewer
            // 
            this.button_processing_viewer.Location = new System.Drawing.Point(90, 20);
            this.button_processing_viewer.Name = "button_processing_viewer";
            this.button_processing_viewer.Size = new System.Drawing.Size(78, 23);
            this.button_processing_viewer.TabIndex = 2;
            this.button_processing_viewer.Text = "Processing";
            this.button_processing_viewer.UseVisualStyleBackColor = true;
            this.button_processing_viewer.Click += new System.EventHandler(this.button_result_viewer_Click);
            // 
            // button_origin_viewer
            // 
            this.button_origin_viewer.Location = new System.Drawing.Point(6, 20);
            this.button_origin_viewer.Name = "button_origin_viewer";
            this.button_origin_viewer.Size = new System.Drawing.Size(78, 23);
            this.button_origin_viewer.TabIndex = 1;
            this.button_origin_viewer.Text = "Grabed";
            this.button_origin_viewer.UseVisualStyleBackColor = true;
            this.button_origin_viewer.Click += new System.EventHandler(this.button_origin_viewer_Click);
            // 
            // button_result_reset
            // 
            this.button_result_reset.Location = new System.Drawing.Point(6, 20);
            this.button_result_reset.Name = "button_result_reset";
            this.button_result_reset.Size = new System.Drawing.Size(142, 23);
            this.button_result_reset.TabIndex = 3;
            this.button_result_reset.Text = "Grabed -> Processing";
            this.button_result_reset.UseVisualStyleBackColor = true;
            this.button_result_reset.Click += new System.EventHandler(this.button_result_reset_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.button_hough_line);
            this.groupBox4.Controls.Add(this.button_fit_circle);
            this.groupBox4.Controls.Add(this.button_one_cycle);
            this.groupBox4.Controls.Add(this.button_list_clear);
            this.groupBox4.Controls.Add(this.button_processing_result);
            this.groupBox4.Controls.Add(this.listView_image_process);
            this.groupBox4.Controls.Add(this.button_template_match);
            this.groupBox4.Controls.Add(this.button_result_reset);
            this.groupBox4.Location = new System.Drawing.Point(3, 284);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(302, 202);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Image process";
            // 
            // button_hough_line
            // 
            this.button_hough_line.Location = new System.Drawing.Point(7, 78);
            this.button_hough_line.Name = "button_hough_line";
            this.button_hough_line.Size = new System.Drawing.Size(142, 23);
            this.button_hough_line.TabIndex = 9;
            this.button_hough_line.Text = "Hough Line";
            this.button_hough_line.UseVisualStyleBackColor = true;
            this.button_hough_line.Click += new System.EventHandler(this.button_hough_line_Click);
            // 
            // button_fit_circle
            // 
            this.button_fit_circle.Location = new System.Drawing.Point(7, 108);
            this.button_fit_circle.Name = "button_fit_circle";
            this.button_fit_circle.Size = new System.Drawing.Size(142, 23);
            this.button_fit_circle.TabIndex = 8;
            this.button_fit_circle.Text = "Fit Circle";
            this.button_fit_circle.UseVisualStyleBackColor = true;
            this.button_fit_circle.Visible = false;
            this.button_fit_circle.Click += new System.EventHandler(this.button_fit_circle_Click);
            // 
            // button_one_cycle
            // 
            this.button_one_cycle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_one_cycle.Location = new System.Drawing.Point(154, 163);
            this.button_one_cycle.Name = "button_one_cycle";
            this.button_one_cycle.Size = new System.Drawing.Size(142, 33);
            this.button_one_cycle.TabIndex = 7;
            this.button_one_cycle.Text = "One cycle process";
            this.button_one_cycle.UseVisualStyleBackColor = true;
            this.button_one_cycle.Click += new System.EventHandler(this.button_one_cycle_Click);
            // 
            // button_list_clear
            // 
            this.button_list_clear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_list_clear.Location = new System.Drawing.Point(154, 134);
            this.button_list_clear.Name = "button_list_clear";
            this.button_list_clear.Size = new System.Drawing.Size(142, 23);
            this.button_list_clear.TabIndex = 6;
            this.button_list_clear.Text = "Clear";
            this.button_list_clear.UseVisualStyleBackColor = true;
            this.button_list_clear.Click += new System.EventHandler(this.button_list_clear_Click);
            // 
            // button_processing_result
            // 
            this.button_processing_result.Location = new System.Drawing.Point(7, 138);
            this.button_processing_result.Name = "button_processing_result";
            this.button_processing_result.Size = new System.Drawing.Size(142, 23);
            this.button_processing_result.TabIndex = 5;
            this.button_processing_result.Text = "Processing -> Result";
            this.button_processing_result.UseVisualStyleBackColor = true;
            this.button_processing_result.Click += new System.EventHandler(this.button_processing_result_Click);
            // 
            // listView_image_process
            // 
            this.listView_image_process.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_image_process.Location = new System.Drawing.Point(154, 20);
            this.listView_image_process.Name = "listView_image_process";
            this.listView_image_process.Size = new System.Drawing.Size(142, 108);
            this.listView_image_process.TabIndex = 4;
            this.listView_image_process.UseCompatibleStateImageBehavior = false;
            this.listView_image_process.View = System.Windows.Forms.View.List;
            // 
            // button_template_match
            // 
            this.button_template_match.Location = new System.Drawing.Point(6, 49);
            this.button_template_match.Name = "button_template_match";
            this.button_template_match.Size = new System.Drawing.Size(142, 23);
            this.button_template_match.TabIndex = 1;
            this.button_template_match.Text = "Template match";
            this.button_template_match.UseVisualStyleBackColor = true;
            this.button_template_match.Click += new System.EventHandler(this.button_template_match_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.checkBox_markobj_index);
            this.groupBox5.Controls.Add(this.checkBox_markgroup_index);
            this.groupBox5.Controls.Add(this.textBox_markgroup_index);
            this.groupBox5.Controls.Add(this.button_moveNmark_obj);
            this.groupBox5.Controls.Add(this.button_mark_obj);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.textBox_markobj_index);
            this.groupBox5.Controls.Add(this.button_move_obj);
            this.groupBox5.Location = new System.Drawing.Point(3, 589);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(302, 150);
            this.groupBox5.TabIndex = 7;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Mark target position";
            this.groupBox5.Visible = false;
            // 
            // checkBox_markobj_index
            // 
            this.checkBox_markobj_index.AutoSize = true;
            this.checkBox_markobj_index.Checked = true;
            this.checkBox_markobj_index.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_markobj_index.Location = new System.Drawing.Point(6, 43);
            this.checkBox_markobj_index.Name = "checkBox_markobj_index";
            this.checkBox_markobj_index.Size = new System.Drawing.Size(107, 16);
            this.checkBox_markobj_index.TabIndex = 9;
            this.checkBox_markobj_index.Text = "Object index : ";
            this.checkBox_markobj_index.UseVisualStyleBackColor = true;
            this.checkBox_markobj_index.CheckedChanged += new System.EventHandler(this.checkBox_markobj_index_CheckedChanged);
            // 
            // checkBox_markgroup_index
            // 
            this.checkBox_markgroup_index.AutoSize = true;
            this.checkBox_markgroup_index.Location = new System.Drawing.Point(6, 20);
            this.checkBox_markgroup_index.Name = "checkBox_markgroup_index";
            this.checkBox_markgroup_index.Size = new System.Drawing.Size(105, 16);
            this.checkBox_markgroup_index.TabIndex = 8;
            this.checkBox_markgroup_index.Text = "Group index : ";
            this.checkBox_markgroup_index.UseVisualStyleBackColor = true;
            this.checkBox_markgroup_index.CheckedChanged += new System.EventHandler(this.checkBox_markgroup_index_CheckedChanged);
            // 
            // textBox_markgroup_index
            // 
            this.textBox_markgroup_index.Enabled = false;
            this.textBox_markgroup_index.Location = new System.Drawing.Point(117, 18);
            this.textBox_markgroup_index.Name = "textBox_markgroup_index";
            this.textBox_markgroup_index.Size = new System.Drawing.Size(85, 21);
            this.textBox_markgroup_index.TabIndex = 7;
            this.textBox_markgroup_index.TextChanged += new System.EventHandler(this.textBox_markgroup_index_TextChanged);
            // 
            // button_moveNmark_obj
            // 
            this.button_moveNmark_obj.Location = new System.Drawing.Point(6, 115);
            this.button_moveNmark_obj.Name = "button_moveNmark_obj";
            this.button_moveNmark_obj.Size = new System.Drawing.Size(218, 23);
            this.button_moveNmark_obj.TabIndex = 5;
            this.button_moveNmark_obj.Text = "Move and Mark object";
            this.button_moveNmark_obj.UseVisualStyleBackColor = true;
            this.button_moveNmark_obj.Click += new System.EventHandler(this.button_moveNmark_obj_Click);
            // 
            // button_mark_obj
            // 
            this.button_mark_obj.Location = new System.Drawing.Point(137, 86);
            this.button_mark_obj.Name = "button_mark_obj";
            this.button_mark_obj.Size = new System.Drawing.Size(87, 23);
            this.button_mark_obj.TabIndex = 4;
            this.button_mark_obj.Text = "Mark object";
            this.button_mark_obj.UseVisualStyleBackColor = true;
            this.button_mark_obj.Click += new System.EventHandler(this.button_mark_obj_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(94, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "---->";
            // 
            // textBox_markobj_index
            // 
            this.textBox_markobj_index.Location = new System.Drawing.Point(117, 41);
            this.textBox_markobj_index.Name = "textBox_markobj_index";
            this.textBox_markobj_index.Size = new System.Drawing.Size(85, 21);
            this.textBox_markobj_index.TabIndex = 2;
            this.textBox_markobj_index.TextChanged += new System.EventHandler(this.textBox_markobj_index_TextChanged);
            // 
            // button_move_obj
            // 
            this.button_move_obj.Location = new System.Drawing.Point(6, 86);
            this.button_move_obj.Name = "button_move_obj";
            this.button_move_obj.Size = new System.Drawing.Size(82, 23);
            this.button_move_obj.TabIndex = 0;
            this.button_move_obj.Text = "Move object";
            this.button_move_obj.UseVisualStyleBackColor = true;
            this.button_move_obj.Click += new System.EventHandler(this.button_move_obj_Click);
            // 
            // checkBox_autosave_enable
            // 
            this.checkBox_autosave_enable.AutoSize = true;
            this.checkBox_autosave_enable.Location = new System.Drawing.Point(6, 20);
            this.checkBox_autosave_enable.Name = "checkBox_autosave_enable";
            this.checkBox_autosave_enable.Size = new System.Drawing.Size(63, 16);
            this.checkBox_autosave_enable.TabIndex = 0;
            this.checkBox_autosave_enable.Text = "Enable";
            this.checkBox_autosave_enable.UseVisualStyleBackColor = true;
            this.checkBox_autosave_enable.CheckedChanged += new System.EventHandler(this.checkBox_autosave_enable_CheckedChanged);
            // 
            // textBox_autosave_path
            // 
            this.textBox_autosave_path.Location = new System.Drawing.Point(65, 42);
            this.textBox_autosave_path.Name = "textBox_autosave_path";
            this.textBox_autosave_path.ReadOnly = true;
            this.textBox_autosave_path.Size = new System.Drawing.Size(178, 21);
            this.textBox_autosave_path.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Path : ";
            // 
            // button_set_path_folder_autosave
            // 
            this.button_set_path_folder_autosave.Location = new System.Drawing.Point(258, 42);
            this.button_set_path_folder_autosave.Name = "button_set_path_folder_autosave";
            this.button_set_path_folder_autosave.Size = new System.Drawing.Size(36, 18);
            this.button_set_path_folder_autosave.TabIndex = 3;
            this.button_set_path_folder_autosave.Text = "...";
            this.button_set_path_folder_autosave.UseVisualStyleBackColor = true;
            this.button_set_path_folder_autosave.Click += new System.EventHandler(this.button_set_path_folder_autosave_Click);
            // 
            // textBox_autosave_interval
            // 
            this.textBox_autosave_interval.Location = new System.Drawing.Point(65, 69);
            this.textBox_autosave_interval.Name = "textBox_autosave_interval";
            this.textBox_autosave_interval.Size = new System.Drawing.Size(83, 21);
            this.textBox_autosave_interval.TabIndex = 4;
            this.textBox_autosave_interval.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Interval :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(154, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "ms (min, 100ms)";
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.label3);
            this.groupBox6.Controls.Add(this.textBox_autosave_interval);
            this.groupBox6.Controls.Add(this.button_set_path_folder_autosave);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.textBox_autosave_path);
            this.groupBox6.Controls.Add(this.checkBox_autosave_enable);
            this.groupBox6.Location = new System.Drawing.Point(3, 492);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(302, 101);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Auto save (in Continuous)";
            this.groupBox6.Visible = false;
            // 
            // LaserVisionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.button_file_saveRecipe);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.button_new_vision_recipe);
            this.Controls.Add(this.textBox_recipe_path);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.button_file_loadRecipe);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.checkBox_vision_visible);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Enabled = false;
            this.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Name = "LaserVisionControl";
            this.Size = new System.Drawing.Size(308, 751);
            this.Load += new System.EventHandler(this.LaserVisionControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_one_shot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_continuous;
        private System.Windows.Forms.Button button_continuous_stop;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_file_loadImage;
        private System.Windows.Forms.Button button_file_saveImage;
        private System.Windows.Forms.CheckBox checkBox_vision_visible;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button button_processing_viewer;
        private System.Windows.Forms.Button button_origin_viewer;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button_template_match;
        private System.Windows.Forms.Button button_result_reset;
        private System.Windows.Forms.Button button_result_viewer;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button_moveNmark_obj;
        private System.Windows.Forms.Button button_mark_obj;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_markobj_index;
        private System.Windows.Forms.Button button_move_obj;
        private System.Windows.Forms.Button button_processing_result;
        private System.Windows.Forms.ListView listView_image_process;
        private System.Windows.Forms.Button button_one_cycle;
        private System.Windows.Forms.Button button_list_clear;
        private System.Windows.Forms.Button button_fit_circle;
        private System.Windows.Forms.Button button_file_loadRecipe;
        private System.Windows.Forms.Button button_file_saveRecipe;
        private System.Windows.Forms.CheckBox checkBox_markobj_index;
        private System.Windows.Forms.CheckBox checkBox_markgroup_index;
        private System.Windows.Forms.TextBox textBox_markgroup_index;
        private System.Windows.Forms.TextBox textBox_recipe_path;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_new_vision_recipe;
        private System.Windows.Forms.Button button_Line;
        private System.Windows.Forms.Button button_Straight;
        private System.Windows.Forms.Button button_Clear;
        private System.Windows.Forms.CheckBox checkBox_autosave_enable;
        private System.Windows.Forms.TextBox textBox_autosave_path;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_set_path_folder_autosave;
        private System.Windows.Forms.TextBox textBox_autosave_interval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button button_hough_line;

    }
}
