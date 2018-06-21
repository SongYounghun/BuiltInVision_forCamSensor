namespace BuiltInVision
{
    partial class MainForm
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

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            devDept.Eyeshot.BoundingBox boundingBox1 = new devDept.Eyeshot.BoundingBox(System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(63))))), ((ushort)(3855)), false, false, new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Point3D(1D, 1D, 1D), "", "(Not applicable)", false);
            devDept.Eyeshot.CancelToolBarButton cancelToolBarButton1 = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ProgressBar progressBar1 = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.CircularWin10, 0, "Idle", System.Drawing.Color.Black, System.Drawing.Color.Transparent, System.Drawing.Color.Green, 1D, true, cancelToolBarButton1, false);
            devDept.Graphics.BackgroundSettings backgroundSettings1 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.Solid, System.Drawing.Color.WhiteSmoke, System.Drawing.Color.White, System.Drawing.Color.Gainsboro, 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.3D);
            devDept.Eyeshot.Camera camera1 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(0D, 0D, 0D), 147.11380000000003D, new devDept.Geometry.Quaternion(0.49999999999999989D, 0.5D, 0.5D, 0.50000000000000011D), devDept.Graphics.projectionType.Orthographic, 50D, 1.8866182861840473D, false);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton1 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton1 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton1 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton1 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton1 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar1 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.VerticalMiddleRight, true, new devDept.Eyeshot.ToolBarButton[] {
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
            devDept.Eyeshot.Viewport viewport1 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(791, 579), backgroundSettings1, camera1, toolBar1, devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid1}, false, rotateSettings1, zoomSettings1, panSettings1, navigationSettings1, savedViewsManager1, devDept.Eyeshot.viewType.Top);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon1 = new devDept.Eyeshot.CoordinateSystemIcon(System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, false);
            devDept.Eyeshot.OriginSymbol originSymbol1 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, null, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon1 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, false, System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(43)))), ((int)(((byte)(226))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl_properties = new System.Windows.Forms.TabControl();
            this.tabPage_vision = new System.Windows.Forms.TabPage();
            this.menuStrip_menu = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ungroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.groupEditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pointToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.drawLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polyLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.circleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ellipseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionByPickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectionByBoxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.snappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridSnapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visionSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.gridSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iOControlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sensingInterfaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iOToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.springToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.viewportLayout1 = new BuiltInVision.DraftingViewportLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl_properties.SuspendLayout();
            this.menuStrip_menu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewportLayout1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.viewportLayout1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl_properties);
            this.splitContainer1.Size = new System.Drawing.Size(1123, 579);
            this.splitContainer1.SplitterDistance = 791;
            this.splitContainer1.TabIndex = 1;
            // 
            // tabControl_properties
            // 
            this.tabControl_properties.Controls.Add(this.tabPage_vision);
            this.tabControl_properties.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_properties.Location = new System.Drawing.Point(0, 0);
            this.tabControl_properties.Name = "tabControl_properties";
            this.tabControl_properties.SelectedIndex = 0;
            this.tabControl_properties.Size = new System.Drawing.Size(328, 579);
            this.tabControl_properties.TabIndex = 0;
            // 
            // tabPage_vision
            // 
            this.tabPage_vision.Location = new System.Drawing.Point(4, 22);
            this.tabPage_vision.Name = "tabPage_vision";
            this.tabPage_vision.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_vision.Size = new System.Drawing.Size(320, 553);
            this.tabPage_vision.TabIndex = 0;
            this.tabPage_vision.Text = "Vision";
            this.tabPage_vision.UseVisualStyleBackColor = true;
            // 
            // menuStrip_menu
            // 
            this.menuStrip_menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.drawToolsToolStripMenuItem,
            this.selectionToolStripMenuItem,
            this.snappingToolStripMenuItem,
            this.optionToolStripMenuItem,
            this.sensingInterfaceToolStripMenuItem});
            this.menuStrip_menu.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_menu.Name = "menuStrip_menu";
            this.menuStrip_menu.Size = new System.Drawing.Size(1147, 24);
            this.menuStrip_menu.TabIndex = 3;
            this.menuStrip_menu.Text = "menuStrip1";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pastToolStripMenuItem,
            this.offsetToolStripMenuItem,
            this.groupToolStripMenuItem,
            this.ungroupToolStripMenuItem,
            this.selectionToolStripMenuItem1,
            this.groupEditToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Visible = false;
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeyDisplayString = "DEL";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // pastToolStripMenuItem
            // 
            this.pastToolStripMenuItem.Name = "pastToolStripMenuItem";
            this.pastToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pastToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.pastToolStripMenuItem.Text = "Past";
            // 
            // offsetToolStripMenuItem
            // 
            this.offsetToolStripMenuItem.Name = "offsetToolStripMenuItem";
            this.offsetToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.offsetToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.F)));
            this.offsetToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.offsetToolStripMenuItem.Text = "Offset";
            // 
            // groupToolStripMenuItem
            // 
            this.groupToolStripMenuItem.Name = "groupToolStripMenuItem";
            this.groupToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.G)));
            this.groupToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.groupToolStripMenuItem.Text = "Group";
            // 
            // ungroupToolStripMenuItem
            // 
            this.ungroupToolStripMenuItem.Name = "ungroupToolStripMenuItem";
            this.ungroupToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.U)));
            this.ungroupToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.ungroupToolStripMenuItem.Text = "Ungroup";
            // 
            // selectionToolStripMenuItem1
            // 
            this.selectionToolStripMenuItem1.Name = "selectionToolStripMenuItem1";
            this.selectionToolStripMenuItem1.Size = new System.Drawing.Size(196, 22);
            this.selectionToolStripMenuItem1.Text = "Move selection";
            // 
            // groupEditToolStripMenuItem
            // 
            this.groupEditToolStripMenuItem.Name = "groupEditToolStripMenuItem";
            this.groupEditToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.groupEditToolStripMenuItem.Text = "Group edit";
            // 
            // drawToolsToolStripMenuItem
            // 
            this.drawToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pointToolStripMenuItem1,
            this.drawLineToolStripMenuItem,
            this.polyLineToolStripMenuItem,
            this.circleToolStripMenuItem,
            this.arcToolStripMenuItem,
            this.splineToolStripMenuItem,
            this.ellipseToolStripMenuItem});
            this.drawToolsToolStripMenuItem.Name = "drawToolsToolStripMenuItem";
            this.drawToolsToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.drawToolsToolStripMenuItem.Text = "Draw Tools";
            this.drawToolsToolStripMenuItem.Visible = false;
            // 
            // pointToolStripMenuItem1
            // 
            this.pointToolStripMenuItem1.Name = "pointToolStripMenuItem1";
            this.pointToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.pointToolStripMenuItem1.Size = new System.Drawing.Size(142, 22);
            this.pointToolStripMenuItem1.Text = "Point";
            // 
            // drawLineToolStripMenuItem
            // 
            this.drawLineToolStripMenuItem.Name = "drawLineToolStripMenuItem";
            this.drawLineToolStripMenuItem.ShortcutKeyDisplayString = "L";
            this.drawLineToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.drawLineToolStripMenuItem.Text = "Line";
            // 
            // polyLineToolStripMenuItem
            // 
            this.polyLineToolStripMenuItem.Name = "polyLineToolStripMenuItem";
            this.polyLineToolStripMenuItem.ShortcutKeyDisplayString = "P";
            this.polyLineToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.polyLineToolStripMenuItem.Text = "Poly line";
            // 
            // circleToolStripMenuItem
            // 
            this.circleToolStripMenuItem.Name = "circleToolStripMenuItem";
            this.circleToolStripMenuItem.ShortcutKeyDisplayString = "C";
            this.circleToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.circleToolStripMenuItem.Text = "Circle";
            // 
            // arcToolStripMenuItem
            // 
            this.arcToolStripMenuItem.Name = "arcToolStripMenuItem";
            this.arcToolStripMenuItem.ShortcutKeyDisplayString = "R";
            this.arcToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.arcToolStripMenuItem.Text = "Arc";
            // 
            // splineToolStripMenuItem
            // 
            this.splineToolStripMenuItem.Name = "splineToolStripMenuItem";
            this.splineToolStripMenuItem.ShortcutKeyDisplayString = "S";
            this.splineToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.splineToolStripMenuItem.Text = "Spline";
            // 
            // ellipseToolStripMenuItem
            // 
            this.ellipseToolStripMenuItem.Name = "ellipseToolStripMenuItem";
            this.ellipseToolStripMenuItem.ShortcutKeyDisplayString = "E";
            this.ellipseToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.ellipseToolStripMenuItem.Text = "Ellipse";
            // 
            // selectionToolStripMenuItem
            // 
            this.selectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectionByPickToolStripMenuItem,
            this.selectionByBoxToolStripMenuItem});
            this.selectionToolStripMenuItem.Name = "selectionToolStripMenuItem";
            this.selectionToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
            this.selectionToolStripMenuItem.Text = "Selection";
            this.selectionToolStripMenuItem.Visible = false;
            // 
            // selectionByPickToolStripMenuItem
            // 
            this.selectionByPickToolStripMenuItem.Name = "selectionByPickToolStripMenuItem";
            this.selectionByPickToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
            this.selectionByPickToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.selectionByPickToolStripMenuItem.Text = "By pick";
            // 
            // selectionByBoxToolStripMenuItem
            // 
            this.selectionByBoxToolStripMenuItem.Name = "selectionByBoxToolStripMenuItem";
            this.selectionByBoxToolStripMenuItem.ShortcutKeyDisplayString = "";
            this.selectionByBoxToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.X)));
            this.selectionByBoxToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.selectionByBoxToolStripMenuItem.Text = "By Box";
            // 
            // snappingToolStripMenuItem
            // 
            this.snappingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gridSnapToolStripMenuItem});
            this.snappingToolStripMenuItem.Name = "snappingToolStripMenuItem";
            this.snappingToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.snappingToolStripMenuItem.Text = "Snapping";
            this.snappingToolStripMenuItem.Visible = false;
            // 
            // gridSnapToolStripMenuItem
            // 
            this.gridSnapToolStripMenuItem.Name = "gridSnapToolStripMenuItem";
            this.gridSnapToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.gridSnapToolStripMenuItem.Text = "Grid snap";
            this.gridSnapToolStripMenuItem.Click += new System.EventHandler(this.gridSnapToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visionSettingToolStripMenuItem,
            this.toolStripSeparator1,
            this.gridSettingToolStripMenuItem,
            this.iOControlToolStripMenuItem});
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // visionSettingToolStripMenuItem
            // 
            this.visionSettingToolStripMenuItem.Name = "visionSettingToolStripMenuItem";
            this.visionSettingToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.visionSettingToolStripMenuItem.Text = "Vision setting";
            this.visionSettingToolStripMenuItem.Click += new System.EventHandler(this.visionSettingToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(144, 6);
            // 
            // gridSettingToolStripMenuItem
            // 
            this.gridSettingToolStripMenuItem.Name = "gridSettingToolStripMenuItem";
            this.gridSettingToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.gridSettingToolStripMenuItem.Text = "Grid Setting";
            this.gridSettingToolStripMenuItem.Visible = false;
            this.gridSettingToolStripMenuItem.Click += new System.EventHandler(this.gridSettingToolStripMenuItem_Click);
            // 
            // iOControlToolStripMenuItem
            // 
            this.iOControlToolStripMenuItem.Name = "iOControlToolStripMenuItem";
            this.iOControlToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.iOControlToolStripMenuItem.Text = "IO Control";
            this.iOControlToolStripMenuItem.Click += new System.EventHandler(this.iOControlToolStripMenuItem_Click);
            // 
            // sensingInterfaceToolStripMenuItem
            // 
            this.sensingInterfaceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iOToolStripMenuItem});
            this.sensingInterfaceToolStripMenuItem.Name = "sensingInterfaceToolStripMenuItem";
            this.sensingInterfaceToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.sensingInterfaceToolStripMenuItem.Text = "Sensing interface";
            // 
            // iOToolStripMenuItem
            // 
            this.iOToolStripMenuItem.Name = "iOToolStripMenuItem";
            this.iOToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.iOToolStripMenuItem.Text = "I/O";
            this.iOToolStripMenuItem.Click += new System.EventHandler(this.iOToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainStatusLabel,
            this.springToolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 596);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(1147, 22);
            this.statusStrip1.TabIndex = 55;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainStatusLabel
            // 
            this.mainStatusLabel.Name = "mainStatusLabel";
            this.mainStatusLabel.Size = new System.Drawing.Size(572, 17);
            this.mainStatusLabel.Text = "Middle Mouse Button = Rotate, Ctrl + Middle = Pan, Shift + Middle = Zoom, Mouse W" +
    "heel = Zoom +/-";
            // 
            // springToolStripStatusLabel
            // 
            this.springToolStripStatusLabel.Name = "springToolStripStatusLabel";
            this.springToolStripStatusLabel.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
            this.springToolStripStatusLabel.Size = new System.Drawing.Size(558, 17);
            this.springToolStripStatusLabel.Spring = true;
            // 
            // viewportLayout1
            // 
            this.viewportLayout1.BoundingBox = boundingBox1;
            this.viewportLayout1.Cursor = System.Windows.Forms.Cursors.Default;
            this.viewportLayout1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewportLayout1.gridSnapEnabled = false;
            this.viewportLayout1.Location = new System.Drawing.Point(0, 0);
            this.viewportLayout1.Name = "viewportLayout1";
            this.viewportLayout1.objectSnapEnabled = false;
            this.viewportLayout1.ProgressBar = progressBar1;
            this.viewportLayout1.Size = new System.Drawing.Size(791, 579);
            this.viewportLayout1.TabIndex = 0;
            this.viewportLayout1.Text = "viewportLayout1";
            coordinateSystemIcon1.LabelFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            viewport1.CoordinateSystemIcon = coordinateSystemIcon1;
            originSymbol1.LabelFont = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            viewport1.OriginSymbol = originSymbol1;
            viewport1.ViewCubeIcon = viewCubeIcon1;
            this.viewportLayout1.Viewports.Add(viewport1);
            this.viewportLayout1.waitingForSelection = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 618);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip_menu);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Camera sensor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl_properties.ResumeLayout(false);
            this.menuStrip_menu.ResumeLayout(false);
            this.menuStrip_menu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.viewportLayout1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DraftingViewportLayout viewportLayout1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TabControl tabControl_properties;
        private System.Windows.Forms.TabPage tabPage_vision;
        private System.Windows.Forms.MenuStrip menuStrip_menu;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pastToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offsetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ungroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem groupEditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drawToolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pointToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem drawLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polyLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem circleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem splineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ellipseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionByPickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectionByBoxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem snappingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gridSnapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visionSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem gridSettingToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel mainStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel springToolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem iOControlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sensingInterfaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iOToolStripMenuItem;

    }
}

