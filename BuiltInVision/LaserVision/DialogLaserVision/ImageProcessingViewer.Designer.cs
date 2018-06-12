namespace GalvoScanner.LaserVision.DialogLaserVision
{
    partial class ImageProcessingViewer
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
            devDept.Eyeshot.BoundingBox boundingBox1 = new devDept.Eyeshot.BoundingBox(System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63))))), ((ushort)(3855)), false, false, new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Point3D(1D, 1D, 1D), "", "(Not applicable)", false);
            devDept.Eyeshot.CancelToolBarButton cancelToolBarButton1 = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ProgressBar progressBar1 = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.CircularWin10, 0, "Idle", System.Drawing.Color.Black, System.Drawing.Color.Transparent, System.Drawing.Color.Green, 1D, true, cancelToolBarButton1, false);
            devDept.Graphics.BackgroundSettings backgroundSettings1 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.Solid, System.Drawing.Color.WhiteSmoke, System.Drawing.Color.White, System.Drawing.Color.Gainsboro, 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.3D);
            devDept.Eyeshot.Camera camera1 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(1.4210854715202004E-14D, -2.8421709430404007E-14D, 72.22029999999998D), 147.11380000000003D, new devDept.Geometry.Quaternion(0.49999999999999989D, 0.5D, 0.5D, 0.50000000000000011D), devDept.Graphics.projectionType.Orthographic, 50D, 1.3294298306107026D, false);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton1 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton1 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton1 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton1 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton1 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar1 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton1))});
            devDept.Eyeshot.Grid grid1 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point3D(-100D, -100D, 0D), new devDept.Geometry.Point3D(100D, 100D, 0D), 1D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32))))), false, false, false, false, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false);
            devDept.Eyeshot.RotateSettings rotateSettings1 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 10D, false, 1D, devDept.Eyeshot.rotationType.Trackball, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings1 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.DeepSkyBlue, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10);
            devDept.Eyeshot.PanSettings panSettings1 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings1 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager1 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
            devDept.Eyeshot.Viewport viewport1 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(521, 408), backgroundSettings1, camera1, toolBar1, devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid1}, false, rotateSettings1, zoomSettings1, panSettings1, navigationSettings1, savedViewsManager1, devDept.Eyeshot.viewType.Top);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon1 = new devDept.Eyeshot.CoordinateSystemIcon(System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", false, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, false);
            devDept.Eyeshot.OriginSymbol originSymbol1 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", false, null, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon1 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, false, System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false);
            this.button_ROI_clear = new System.Windows.Forms.Button();
            this.button_position_offset_clear = new System.Windows.Forms.Button();
            this.richTextBox_string_result = new System.Windows.Forms.RichTextBox();
            this.viewportLayout1 = new GalvoScanner.LaserVision.DialogLaserVision.ImgViewerViewportLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewportLayout1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_ROI_clear
            // 
            this.button_ROI_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_ROI_clear.Location = new System.Drawing.Point(12, 426);
            this.button_ROI_clear.Name = "button_ROI_clear";
            this.button_ROI_clear.Size = new System.Drawing.Size(75, 23);
            this.button_ROI_clear.TabIndex = 1;
            this.button_ROI_clear.Text = "ROI clear";
            this.button_ROI_clear.UseVisualStyleBackColor = true;
            this.button_ROI_clear.Click += new System.EventHandler(this.button_ROI_clear_Click);
            // 
            // button_position_offset_clear
            // 
            this.button_position_offset_clear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_position_offset_clear.Location = new System.Drawing.Point(93, 427);
            this.button_position_offset_clear.Name = "button_position_offset_clear";
            this.button_position_offset_clear.Size = new System.Drawing.Size(124, 23);
            this.button_position_offset_clear.TabIndex = 2;
            this.button_position_offset_clear.Text = "Position offset clear";
            this.button_position_offset_clear.UseVisualStyleBackColor = true;
            this.button_position_offset_clear.Click += new System.EventHandler(this.button_position_offset_clear_Click);
            // 
            // richTextBox_string_result
            // 
            this.richTextBox_string_result.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_string_result.Location = new System.Drawing.Point(539, 12);
            this.richTextBox_string_result.Name = "richTextBox_string_result";
            this.richTextBox_string_result.ReadOnly = true;
            this.richTextBox_string_result.Size = new System.Drawing.Size(233, 408);
            this.richTextBox_string_result.TabIndex = 4;
            this.richTextBox_string_result.Text = "";
            // 
            // viewportLayout1
            // 
            this.viewportLayout1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.viewportLayout1.BoundingBox = boundingBox1;
            this.viewportLayout1.Cursor = System.Windows.Forms.Cursors.Default;
            this.viewportLayout1.Location = new System.Drawing.Point(12, 12);
            this.viewportLayout1.Name = "viewportLayout1";
            this.viewportLayout1.ProgressBar = progressBar1;
            this.viewportLayout1.Size = new System.Drawing.Size(521, 408);
            this.viewportLayout1.TabIndex = 0;
            this.viewportLayout1.Text = "viewportLayout1";
            coordinateSystemIcon1.LabelFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            viewport1.CoordinateSystemIcon = coordinateSystemIcon1;
            originSymbol1.LabelFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            viewport1.OriginSymbol = originSymbol1;
            viewport1.ViewCubeIcon = viewCubeIcon1;
            this.viewportLayout1.Viewports.Add(viewport1);
            this.viewportLayout1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.viewportLayout1_MouseDown);
            this.viewportLayout1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.viewportLayout1_MouseMove);
            this.viewportLayout1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.viewportLayout1_MouseUp);
            // 
            // ImageProcessingViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 462);
            this.Controls.Add(this.richTextBox_string_result);
            this.Controls.Add(this.button_position_offset_clear);
            this.Controls.Add(this.button_ROI_clear);
            this.Controls.Add(this.viewportLayout1);
            this.Name = "ImageProcessingViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImageProcessingViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageProcessingViewer_FormClosing);
            this.Load += new System.EventHandler(this.ImageProcessingViewer_Load);
            this.VisibleChanged += new System.EventHandler(this.ImageProcessingViewer_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.viewportLayout1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GalvoScanner.LaserVision.DialogLaserVision.ImgViewerViewportLayout viewportLayout1;
        private System.Windows.Forms.Button button_ROI_clear;
        private System.Windows.Forms.Button button_position_offset_clear;
        private System.Windows.Forms.RichTextBox richTextBox_string_result;





    }
}