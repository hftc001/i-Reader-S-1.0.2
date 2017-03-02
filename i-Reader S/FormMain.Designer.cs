namespace i_Reader_S
{
    partial class ReaderS
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (_myAlert != null)
                {
                    _myAlert.Dispose();
                    _myAlert = null;
                }
                if (_mykeyboard != null)
                {
                    _mykeyboard.Dispose();
                    _mykeyboard = null;
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReaderS));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonMessage = new System.Windows.Forms.Button();
            this.buttonSetting = new System.Windows.Forms.Button();
            this.buttonQC = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonHome = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageLogin = new System.Windows.Forms.TabPage();
            this.textBoxOpenState = new System.Windows.Forms.TextBox();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.labelLoding = new System.Windows.Forms.Label();
            this.buttonShowPassword = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxLoginName = new System.Windows.Forms.TextBox();
            this.textBoxLoginPassword = new System.Windows.Forms.TextBox();
            this.panelLoding = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.labelCountDown = new System.Windows.Forms.Label();
            this.panelLoding5 = new System.Windows.Forms.Panel();
            this.panelLoding4 = new System.Windows.Forms.Panel();
            this.panelLoding3 = new System.Windows.Forms.Panel();
            this.panelLoding2 = new System.Windows.Forms.Panel();
            this.panelLoding1 = new System.Windows.Forms.Panel();
            this.labelStep = new System.Windows.Forms.Label();
            this.tabPageUserMode = new System.Windows.Forms.TabPage();
            this.labelUserStep = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonUserLiquidCheck = new System.Windows.Forms.Button();
            this.buttonUserClean2 = new System.Windows.Forms.Button();
            this.buttonUserClean1 = new System.Windows.Forms.Button();
            this.buttonUserTest = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tabPageDetail = new System.Windows.Forms.TabPage();
            this.buttonFluoTest = new System.Windows.Forms.Button();
            this.textBoxASU = new System.Windows.Forms.TextBox();
            this.buttonGetIDCode = new System.Windows.Forms.Button();
            this.buttonStartTH = new System.Windows.Forms.Button();
            this.labelIDRead = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.videoSourcePlayer1 = new AForge.Controls.VideoSourcePlayer();
            this.buttonCCDTest = new System.Windows.Forms.Button();
            this.buttonLiquidSensor = new System.Windows.Forms.Button();
            this.buttonReagentSensor = new System.Windows.Forms.Button();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.textBoxQR = new System.Windows.Forms.TextBox();
            this.textBoxTH = new System.Windows.Forms.TextBox();
            this.textBoxMain = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.textBoxCCDRef = new System.Windows.Forms.TextBox();
            this.buttonFluoFix = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxFluoRef = new System.Windows.Forms.TextBox();
            this.textBoxCMD = new System.Windows.Forms.TextBox();
            this.buttonCMDSend = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label35 = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.buttonTest = new System.Windows.Forms.Button();
            this.buttonMaintain = new System.Windows.Forms.Button();
            this.buttonMidFix = new System.Windows.Forms.Button();
            this.buttonDilutionMode = new System.Windows.Forms.Button();
            this.buttonLigthFix = new System.Windows.Forms.Button();
            this.chartFluo = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonSaveLog = new System.Windows.Forms.Button();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.tabPageHome = new System.Windows.Forms.TabPage();
            this.panelPic = new System.Windows.Forms.Panel();
            this.buttonPicClose = new System.Windows.Forms.Button();
            this.pictureBoxResult = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelCleanStatus = new System.Windows.Forms.Label();
            this.labelDilutionStatus = new System.Windows.Forms.Label();
            this.labelReagentStatus = new System.Windows.Forms.Label();
            this.labelWasteStatus = new System.Windows.Forms.Label();
            this.buttonSupply = new System.Windows.Forms.Button();
            this.buttonReagent = new System.Windows.Forms.Button();
            this.tabControlMainRight = new System.Windows.Forms.TabControl();
            this.tabPageReagent = new System.Windows.Forms.TabPage();
            this.panelReagent5 = new System.Windows.Forms.Panel();
            this.labelExt5 = new System.Windows.Forms.Label();
            this.labelReagentLeft5 = new System.Windows.Forms.Label();
            this.labelLotNo5 = new System.Windows.Forms.Label();
            this.labelProduct5 = new System.Windows.Forms.Label();
            this.panelReagent4 = new System.Windows.Forms.Panel();
            this.labelExt4 = new System.Windows.Forms.Label();
            this.labelReagentLeft4 = new System.Windows.Forms.Label();
            this.labelLotNo4 = new System.Windows.Forms.Label();
            this.labelProduct4 = new System.Windows.Forms.Label();
            this.panelReagent1 = new System.Windows.Forms.Panel();
            this.labelExt1 = new System.Windows.Forms.Label();
            this.labelReagentLeft1 = new System.Windows.Forms.Label();
            this.labelLotNo1 = new System.Windows.Forms.Label();
            this.labelProduct1 = new System.Windows.Forms.Label();
            this.panelReagent3 = new System.Windows.Forms.Panel();
            this.labelExt3 = new System.Windows.Forms.Label();
            this.labelReagentLeft3 = new System.Windows.Forms.Label();
            this.labelLotNo3 = new System.Windows.Forms.Label();
            this.labelProduct3 = new System.Windows.Forms.Label();
            this.panelReagent2 = new System.Windows.Forms.Panel();
            this.labelExt2 = new System.Windows.Forms.Label();
            this.labelReagentLeft2 = new System.Windows.Forms.Label();
            this.labelLotNo2 = new System.Windows.Forms.Label();
            this.labelProduct2 = new System.Windows.Forms.Label();
            this.tabPageSupply = new System.Windows.Forms.TabPage();
            this.panelWasteDisable = new System.Windows.Forms.Panel();
            this.label33 = new System.Windows.Forms.Label();
            this.buttonModifyLeft = new System.Windows.Forms.Button();
            this.buttonModifyVolume = new System.Windows.Forms.Button();
            this.panelClean3 = new System.Windows.Forms.Panel();
            this.labelWashingLiquid = new System.Windows.Forms.Label();
            this.panelDilution3 = new System.Windows.Forms.Panel();
            this.labelDilutionLiquid = new System.Windows.Forms.Label();
            this.panelWasteReagent3 = new System.Windows.Forms.Panel();
            this.labelWasteReagent = new System.Windows.Forms.Label();
            this.panelWaste3 = new System.Windows.Forms.Panel();
            this.labelWasteLiquid = new System.Windows.Forms.Label();
            this.panelWasteReagent2 = new System.Windows.Forms.Panel();
            this.panelWasteReagent1 = new System.Windows.Forms.Panel();
            this.labelWasteReagent2 = new System.Windows.Forms.Label();
            this.labelWasteReagent1 = new System.Windows.Forms.Label();
            this.panelClean2 = new System.Windows.Forms.Panel();
            this.panelClean1 = new System.Windows.Forms.Panel();
            this.labelClean2 = new System.Windows.Forms.Label();
            this.labelClean1 = new System.Windows.Forms.Label();
            this.buttonClean2 = new System.Windows.Forms.Button();
            this.buttonClean1 = new System.Windows.Forms.Button();
            this.panelWaste2 = new System.Windows.Forms.Panel();
            this.panelWaste1 = new System.Windows.Forms.Panel();
            this.labelWaste2 = new System.Windows.Forms.Label();
            this.labelWaste1 = new System.Windows.Forms.Label();
            this.buttonWaste2 = new System.Windows.Forms.Button();
            this.buttonWaste1 = new System.Windows.Forms.Button();
            this.panelDilution2 = new System.Windows.Forms.Panel();
            this.panelDilution1 = new System.Windows.Forms.Panel();
            this.labelDilution2 = new System.Windows.Forms.Label();
            this.labelDilution1 = new System.Windows.Forms.Label();
            this.buttonDilution2 = new System.Windows.Forms.Button();
            this.buttonDilution1 = new System.Windows.Forms.Button();
            this.tabPageReagentOpen = new System.Windows.Forms.TabPage();
            this.labelReagentInfo = new System.Windows.Forms.Label();
            this.buttonSetFirst = new System.Windows.Forms.Button();
            this.labelReagentOperation = new System.Windows.Forms.Label();
            this.labelQR = new System.Windows.Forms.Label();
            this.tabPageQRAlert = new System.Windows.Forms.TabPage();
            this.labelQR2 = new System.Windows.Forms.Label();
            this.labelQRAlert = new System.Windows.Forms.Label();
            this.buttonQRAlertOK = new System.Windows.Forms.Button();
            this.tabPageSupplyFloatBall = new System.Windows.Forms.TabPage();
            this.labelFloatBallWasteReagent = new System.Windows.Forms.Label();
            this.labelFloatBallWaste = new System.Windows.Forms.Label();
            this.labelFloatBallClean = new System.Windows.Forms.Label();
            this.labelFloatBallDilution = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelExceptionNo = new System.Windows.Forms.Label();
            this.dataGridViewMain = new System.Windows.Forms.DataGridView();
            this.labelNextTestItem = new System.Windows.Forms.Label();
            this.labelNextSampleNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelOther = new System.Windows.Forms.Label();
            this.labelTestitem = new System.Windows.Forms.Label();
            this.labelSample = new System.Windows.Forms.Label();
            this.buttonException = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.buttonDoing = new System.Windows.Forms.Button();
            this.tabPageQC = new System.Windows.Forms.TabPage();
            this.dataGridViewQC2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewQC1 = new System.Windows.Forms.DataGridView();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelQCInfo = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelSampleCount = new System.Windows.Forms.Label();
            this.labelSearchCostTime = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.textBoxSearchEndDate = new System.Windows.Forms.TextBox();
            this.textBoxSearchTestitem = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonSearchDetail = new System.Windows.Forms.Button();
            this.textBoxSearchStartDate = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSearchSampleLike = new System.Windows.Forms.Button();
            this.buttonSearchSample = new System.Windows.Forms.Button();
            this.textBoxSearchSample = new System.Windows.Forms.TextBox();
            this.textBoxPage = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.labelpage = new System.Windows.Forms.Label();
            this.dataGridViewSearch = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.修改压积ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.发送结果ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageSetting = new System.Windows.Forms.TabPage();
            this.tabControlSetting = new System.Windows.Forms.TabControl();
            this.tabPageGeneralSetting = new System.Windows.Forms.TabPage();
            this.buttonCheckAllUsers = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxStartSample = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxSleepTime = new System.Windows.Forms.TextBox();
            this.buttonCRPDebug = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.buttonAutoPrint = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.buttonWasteMode = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.tabPageTestItemSetting = new System.Windows.Forms.TabPage();
            this.buttonAddTestItem = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.textBoxPreDilu = new System.Windows.Forms.TextBox();
            this.labelPreDilu = new System.Windows.Forms.Label();
            this.buttonFixTestItem = new System.Windows.Forms.Button();
            this.textBoxSettingUnitRatio = new System.Windows.Forms.TextBox();
            this.textBoxSettingUnitDefault = new System.Windows.Forms.TextBox();
            this.textBoxSettingAccurancy = new System.Windows.Forms.TextBox();
            this.textBoxSettingWarning = new System.Windows.Forms.TextBox();
            this.textBoxSettingRatio = new System.Windows.Forms.TextBox();
            this.textBoxSettingUnit = new System.Windows.Forms.TextBox();
            this.textBoxSettingTestItemName = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.dataGridViewTestItem = new System.Windows.Forms.DataGridView();
            this.label22 = new System.Windows.Forms.Label();
            this.tabPageQCSetting = new System.Windows.Forms.TabPage();
            this.label30 = new System.Windows.Forms.Label();
            this.buttonAutoQC = new System.Windows.Forms.Button();
            this.panelQCPoint = new System.Windows.Forms.Panel();
            this.textBoxQCSD = new System.Windows.Forms.TextBox();
            this.textBoxQCTarget = new System.Windows.Forms.TextBox();
            this.textBoxQCSampleNo = new System.Windows.Forms.TextBox();
            this.buttonFixQCData = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAddQCPoint = new System.Windows.Forms.Button();
            this.dataGridViewQCSetting = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageOtherSetting = new System.Windows.Forms.TabPage();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.labelDriveLeft = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tabPageUser = new System.Windows.Forms.TabPage();
            this.groupBoxUser = new System.Windows.Forms.GroupBox();
            this.buttonUserDelete = new System.Windows.Forms.Button();
            this.buttonUserChange = new System.Windows.Forms.Button();
            this.comboBoxUserType = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.textBoxRepeatPassword = new System.Windows.Forms.TextBox();
            this.textBoxNewPassword = new System.Windows.Forms.TextBox();
            this.textBoxNewUserName = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.panelAllUsers = new System.Windows.Forms.Panel();
            this.paneluser = new System.Windows.Forms.Panel();
            this.label42 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.dataGridViewAllUsers = new System.Windows.Forms.DataGridView();
            this.tabPageMessage = new System.Windows.Forms.TabPage();
            this.dataGridViewLog = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPageStop = new System.Windows.Forms.TabPage();
            this.buttonMinWindow = new System.Windows.Forms.Button();
            this.buttonPowerOff = new System.Windows.Forms.Button();
            this.buttonStopRecovery = new System.Windows.Forms.Button();
            this.buttonStopConfirm = new System.Windows.Forms.Button();
            this.labelStopStatus = new System.Windows.Forms.Label();
            this.panelTopBack = new System.Windows.Forms.Panel();
            this.labelLock = new System.Windows.Forms.Label();
            this.panelOther = new System.Windows.Forms.Panel();
            this.labelMenu = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.labelSystemTime = new System.Windows.Forms.Label();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.labelMeachineStatus = new System.Windows.Forms.Label();
            this.timerPageUp = new System.Windows.Forms.Timer(this.components);
            this.timerPageDown = new System.Windows.Forms.Timer(this.components);
            this.serialPortMain = new System.IO.Ports.SerialPort(this.components);
            this.serialPortQR = new System.IO.Ports.SerialPort(this.components);
            this.serialPortTH = new System.IO.Ports.SerialPort(this.components);
            this.serialPortFluoMotor = new System.IO.Ports.SerialPort(this.components);
            this.serialPortFluo = new System.IO.Ports.SerialPort(this.components);
            this.labelTH = new System.Windows.Forms.Label();
            this.timerLoding = new System.Windows.Forms.Timer(this.components);
            this.labelSupplyLeft = new System.Windows.Forms.Label();
            this.timerLoad = new System.Windows.Forms.Timer(this.components);
            this.timerPageAdjust = new System.Windows.Forms.Timer(this.components);
            this.labelReagentNow = new System.Windows.Forms.Label();
            this.labelSupplyLeft2 = new System.Windows.Forms.Label();
            this.labelSupplyLeft3 = new System.Windows.Forms.Label();
            this.labelSupplyLeft4 = new System.Windows.Forms.Label();
            this.timerSupplyAlert = new System.Windows.Forms.Timer(this.components);
            this.serialPortME = new System.IO.Ports.SerialPort(this.components);
            this.timerSampleReady = new System.Windows.Forms.Timer(this.components);
            this.timerMainPort = new System.Windows.Forms.Timer(this.components);
            this.serialPortFloatBall = new System.IO.Ports.SerialPort(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.labeluser = new System.Windows.Forms.Label();
            this.timerLiquidCheck = new System.Windows.Forms.Timer(this.components);
            this.tabControlMain.SuspendLayout();
            this.tabPageLogin.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.panelLoding.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPageUserMode.SuspendLayout();
            this.tabPageDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFluo)).BeginInit();
            this.tabPageHome.SuspendLayout();
            this.panelPic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabControlMainRight.SuspendLayout();
            this.tabPageReagent.SuspendLayout();
            this.panelReagent5.SuspendLayout();
            this.panelReagent4.SuspendLayout();
            this.panelReagent1.SuspendLayout();
            this.panelReagent3.SuspendLayout();
            this.panelReagent2.SuspendLayout();
            this.tabPageSupply.SuspendLayout();
            this.panelWasteDisable.SuspendLayout();
            this.panelClean3.SuspendLayout();
            this.panelDilution3.SuspendLayout();
            this.panelWasteReagent3.SuspendLayout();
            this.panelWaste3.SuspendLayout();
            this.panelWasteReagent2.SuspendLayout();
            this.panelWasteReagent1.SuspendLayout();
            this.panelClean2.SuspendLayout();
            this.panelClean1.SuspendLayout();
            this.panelWaste2.SuspendLayout();
            this.panelWaste1.SuspendLayout();
            this.panelDilution2.SuspendLayout();
            this.panelDilution1.SuspendLayout();
            this.tabPageReagentOpen.SuspendLayout();
            this.tabPageQRAlert.SuspendLayout();
            this.tabPageSupplyFloatBall.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabPageQC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQC2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQC1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.tabPageSearch.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearch)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabPageSetting.SuspendLayout();
            this.tabControlSetting.SuspendLayout();
            this.tabPageGeneralSetting.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPageTestItemSetting.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTestItem)).BeginInit();
            this.tabPageQCSetting.SuspendLayout();
            this.panelQCPoint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQCSetting)).BeginInit();
            this.tabPageOtherSetting.SuspendLayout();
            this.tabPageUser.SuspendLayout();
            this.groupBoxUser.SuspendLayout();
            this.panelAllUsers.SuspendLayout();
            this.paneluser.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllUsers)).BeginInit();
            this.tabPageMessage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLog)).BeginInit();
            this.tabPageStop.SuspendLayout();
            this.panelTopBack.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonStop
            // 
            this.buttonStop.BackgroundImage = global::i_Reader_S.Properties.Resources.Stop_Normal;
            this.buttonStop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonStop.FlatAppearance.BorderSize = 0;
            this.buttonStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            resources.ApplyResources(this.buttonStop, "buttonStop");
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // buttonMessage
            // 
            this.buttonMessage.BackgroundImage = global::i_Reader_S.Properties.Resources.Message_Normal;
            this.buttonMessage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonMessage.FlatAppearance.BorderSize = 0;
            this.buttonMessage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            resources.ApplyResources(this.buttonMessage, "buttonMessage");
            this.buttonMessage.Name = "buttonMessage";
            this.buttonMessage.UseVisualStyleBackColor = true;
            this.buttonMessage.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // buttonSetting
            // 
            this.buttonSetting.BackgroundImage = global::i_Reader_S.Properties.Resources.Setting_Normal;
            this.buttonSetting.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonSetting.FlatAppearance.BorderSize = 0;
            this.buttonSetting.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonSetting.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            resources.ApplyResources(this.buttonSetting, "buttonSetting");
            this.buttonSetting.Name = "buttonSetting";
            this.buttonSetting.UseVisualStyleBackColor = true;
            this.buttonSetting.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // buttonQC
            // 
            this.buttonQC.BackgroundImage = global::i_Reader_S.Properties.Resources.QC_Normal;
            this.buttonQC.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonQC.FlatAppearance.BorderSize = 0;
            this.buttonQC.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonQC.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            resources.ApplyResources(this.buttonQC, "buttonQC");
            this.buttonQC.Name = "buttonQC";
            this.buttonQC.UseVisualStyleBackColor = true;
            this.buttonQC.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackgroundImage = global::i_Reader_S.Properties.Resources.Search_Normal;
            this.buttonSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonSearch.FlatAppearance.BorderSize = 0;
            this.buttonSearch.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            resources.ApplyResources(this.buttonSearch, "buttonSearch");
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // buttonHome
            // 
            this.buttonHome.BackgroundImage = global::i_Reader_S.Properties.Resources.Home_Normal;
            this.buttonHome.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonHome.FlatAppearance.BorderSize = 0;
            this.buttonHome.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            this.buttonHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(127)))), ((int)(((byte)(191)))));
            resources.ApplyResources(this.buttonHome, "buttonHome");
            this.buttonHome.Name = "buttonHome";
            this.buttonHome.UseVisualStyleBackColor = true;
            this.buttonHome.Click += new System.EventHandler(this.buttonMenu_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageLogin);
            this.tabControlMain.Controls.Add(this.tabPageUserMode);
            this.tabControlMain.Controls.Add(this.tabPageDetail);
            this.tabControlMain.Controls.Add(this.tabPageHome);
            this.tabControlMain.Controls.Add(this.tabPageQC);
            this.tabControlMain.Controls.Add(this.tabPageSearch);
            this.tabControlMain.Controls.Add(this.tabPageSetting);
            this.tabControlMain.Controls.Add(this.tabPageMessage);
            this.tabControlMain.Controls.Add(this.tabPageStop);
            resources.ApplyResources(this.tabControlMain, "tabControlMain");
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            // 
            // tabPageLogin
            // 
            this.tabPageLogin.Controls.Add(this.textBoxOpenState);
            this.tabPageLogin.Controls.Add(this.panelLogin);
            this.tabPageLogin.Controls.Add(this.panelLoding);
            this.tabPageLogin.Controls.Add(this.labelStep);
            resources.ApplyResources(this.tabPageLogin, "tabPageLogin");
            this.tabPageLogin.Name = "tabPageLogin";
            this.tabPageLogin.UseVisualStyleBackColor = true;
            // 
            // textBoxOpenState
            // 
            this.textBoxOpenState.BackColor = System.Drawing.Color.White;
            this.textBoxOpenState.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBoxOpenState, "textBoxOpenState");
            this.textBoxOpenState.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBoxOpenState.Name = "textBoxOpenState";
            this.textBoxOpenState.ReadOnly = true;
            this.textBoxOpenState.MouseClick += new System.Windows.Forms.MouseEventHandler(this.textBoxOpenState_MouseClick);
            this.textBoxOpenState.TextChanged += new System.EventHandler(this.textBoxOpenState_TextChanged);
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.labelLoding);
            this.panelLogin.Controls.Add(this.buttonShowPassword);
            this.panelLogin.Controls.Add(this.label7);
            this.panelLogin.Controls.Add(this.label8);
            this.panelLogin.Controls.Add(this.buttonLogin);
            this.panelLogin.Controls.Add(this.textBoxLoginName);
            this.panelLogin.Controls.Add(this.textBoxLoginPassword);
            resources.ApplyResources(this.panelLogin, "panelLogin");
            this.panelLogin.Name = "panelLogin";
            // 
            // labelLoding
            // 
            this.labelLoding.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.labelLoding, "labelLoding");
            this.labelLoding.Name = "labelLoding";
            // 
            // buttonShowPassword
            // 
            this.buttonShowPassword.BackColor = System.Drawing.Color.Transparent;
            this.buttonShowPassword.BackgroundImage = global::i_Reader_S.Properties.Resources.ShowPassword;
            resources.ApplyResources(this.buttonShowPassword, "buttonShowPassword");
            this.buttonShowPassword.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonShowPassword.FlatAppearance.BorderSize = 0;
            this.buttonShowPassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonShowPassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.buttonShowPassword.ForeColor = System.Drawing.Color.White;
            this.buttonShowPassword.Name = "buttonShowPassword";
            this.buttonShowPassword.UseVisualStyleBackColor = false;
            this.buttonShowPassword.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.buttonShowPassword.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label8.Name = "label8";
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.Color.Transparent;
            this.buttonLogin.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black;
            this.buttonLogin.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.buttonLogin, "buttonLogin");
            this.buttonLogin.ForeColor = System.Drawing.Color.White;
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.button_Click);
            // 
            // textBoxLoginName
            // 
            resources.ApplyResources(this.textBoxLoginName, "textBoxLoginName");
            this.textBoxLoginName.Name = "textBoxLoginName";
            this.textBoxLoginName.Click += new System.EventHandler(this.textBox_Click);
            // 
            // textBoxLoginPassword
            // 
            resources.ApplyResources(this.textBoxLoginPassword, "textBoxLoginPassword");
            this.textBoxLoginPassword.Name = "textBoxLoginPassword";
            this.textBoxLoginPassword.Click += new System.EventHandler(this.textBox_Click);
            // 
            // panelLoding
            // 
            this.panelLoding.Controls.Add(this.panel5);
            this.panelLoding.Controls.Add(this.panelLoding5);
            this.panelLoding.Controls.Add(this.panelLoding4);
            this.panelLoding.Controls.Add(this.panelLoding3);
            this.panelLoding.Controls.Add(this.panelLoding2);
            this.panelLoding.Controls.Add(this.panelLoding1);
            resources.ApplyResources(this.panelLoding, "panelLoding");
            this.panelLoding.Name = "panelLoding";
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::i_Reader_S.Properties.Resources.countdown;
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Controls.Add(this.labelCountDown);
            this.panel5.Name = "panel5";
            // 
            // labelCountDown
            // 
            resources.ApplyResources(this.labelCountDown, "labelCountDown");
            this.labelCountDown.ForeColor = System.Drawing.Color.Black;
            this.labelCountDown.Name = "labelCountDown";
            // 
            // panelLoding5
            // 
            resources.ApplyResources(this.panelLoding5, "panelLoding5");
            this.panelLoding5.Name = "panelLoding5";
            // 
            // panelLoding4
            // 
            resources.ApplyResources(this.panelLoding4, "panelLoding4");
            this.panelLoding4.Name = "panelLoding4";
            // 
            // panelLoding3
            // 
            resources.ApplyResources(this.panelLoding3, "panelLoding3");
            this.panelLoding3.Name = "panelLoding3";
            // 
            // panelLoding2
            // 
            resources.ApplyResources(this.panelLoding2, "panelLoding2");
            this.panelLoding2.Name = "panelLoding2";
            // 
            // panelLoding1
            // 
            resources.ApplyResources(this.panelLoding1, "panelLoding1");
            this.panelLoding1.Name = "panelLoding1";
            // 
            // labelStep
            // 
            resources.ApplyResources(this.labelStep, "labelStep");
            this.labelStep.Name = "labelStep";
            // 
            // tabPageUserMode
            // 
            this.tabPageUserMode.Controls.Add(this.labelUserStep);
            this.tabPageUserMode.Controls.Add(this.label6);
            this.tabPageUserMode.Controls.Add(this.buttonUserLiquidCheck);
            this.tabPageUserMode.Controls.Add(this.buttonUserClean2);
            this.tabPageUserMode.Controls.Add(this.buttonUserClean1);
            this.tabPageUserMode.Controls.Add(this.buttonUserTest);
            this.tabPageUserMode.Controls.Add(this.textBox1);
            resources.ApplyResources(this.tabPageUserMode, "tabPageUserMode");
            this.tabPageUserMode.Name = "tabPageUserMode";
            this.tabPageUserMode.UseVisualStyleBackColor = true;
            // 
            // labelUserStep
            // 
            resources.ApplyResources(this.labelUserStep, "labelUserStep");
            this.labelUserStep.Name = "labelUserStep";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // buttonUserLiquidCheck
            // 
            resources.ApplyResources(this.buttonUserLiquidCheck, "buttonUserLiquidCheck");
            this.buttonUserLiquidCheck.Name = "buttonUserLiquidCheck";
            this.buttonUserLiquidCheck.UseVisualStyleBackColor = true;
            this.buttonUserLiquidCheck.Click += new System.EventHandler(this.buttonUserLiquidCheck_Click);
            // 
            // buttonUserClean2
            // 
            resources.ApplyResources(this.buttonUserClean2, "buttonUserClean2");
            this.buttonUserClean2.Name = "buttonUserClean2";
            this.buttonUserClean2.UseVisualStyleBackColor = true;
            this.buttonUserClean2.Click += new System.EventHandler(this.buttonUserClean2_Click);
            // 
            // buttonUserClean1
            // 
            resources.ApplyResources(this.buttonUserClean1, "buttonUserClean1");
            this.buttonUserClean1.Name = "buttonUserClean1";
            this.buttonUserClean1.UseVisualStyleBackColor = true;
            this.buttonUserClean1.Click += new System.EventHandler(this.buttonUserClean1_Click);
            // 
            // buttonUserTest
            // 
            resources.ApplyResources(this.buttonUserTest, "buttonUserTest");
            this.buttonUserTest.Name = "buttonUserTest";
            this.buttonUserTest.UseVisualStyleBackColor = true;
            this.buttonUserTest.Click += new System.EventHandler(this.buttonUserTest_Click);
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // tabPageDetail
            // 
            this.tabPageDetail.Controls.Add(this.buttonFluoTest);
            this.tabPageDetail.Controls.Add(this.textBoxASU);
            this.tabPageDetail.Controls.Add(this.buttonGetIDCode);
            this.tabPageDetail.Controls.Add(this.buttonStartTH);
            this.tabPageDetail.Controls.Add(this.labelIDRead);
            this.tabPageDetail.Controls.Add(this.pictureBox1);
            this.tabPageDetail.Controls.Add(this.videoSourcePlayer1);
            this.tabPageDetail.Controls.Add(this.buttonCCDTest);
            this.tabPageDetail.Controls.Add(this.buttonLiquidSensor);
            this.tabPageDetail.Controls.Add(this.buttonReagentSensor);
            this.tabPageDetail.Controls.Add(this.comboBox3);
            this.tabPageDetail.Controls.Add(this.textBoxQR);
            this.tabPageDetail.Controls.Add(this.textBoxTH);
            this.tabPageDetail.Controls.Add(this.textBoxMain);
            this.tabPageDetail.Controls.Add(this.label29);
            this.tabPageDetail.Controls.Add(this.textBoxCCDRef);
            this.tabPageDetail.Controls.Add(this.buttonFluoFix);
            this.tabPageDetail.Controls.Add(this.label16);
            this.tabPageDetail.Controls.Add(this.textBoxFluoRef);
            this.tabPageDetail.Controls.Add(this.textBoxCMD);
            this.tabPageDetail.Controls.Add(this.buttonCMDSend);
            this.tabPageDetail.Controls.Add(this.comboBox2);
            this.tabPageDetail.Controls.Add(this.comboBox1);
            this.tabPageDetail.Controls.Add(this.label35);
            this.tabPageDetail.Controls.Add(this.labelResult);
            this.tabPageDetail.Controls.Add(this.buttonTest);
            this.tabPageDetail.Controls.Add(this.buttonMaintain);
            this.tabPageDetail.Controls.Add(this.buttonMidFix);
            this.tabPageDetail.Controls.Add(this.buttonDilutionMode);
            this.tabPageDetail.Controls.Add(this.buttonLigthFix);
            this.tabPageDetail.Controls.Add(this.chartFluo);
            this.tabPageDetail.Controls.Add(this.buttonSaveLog);
            this.tabPageDetail.Controls.Add(this.textBoxLog);
            resources.ApplyResources(this.tabPageDetail, "tabPageDetail");
            this.tabPageDetail.Name = "tabPageDetail";
            this.tabPageDetail.UseVisualStyleBackColor = true;
            // 
            // buttonFluoTest
            // 
            resources.ApplyResources(this.buttonFluoTest, "buttonFluoTest");
            this.buttonFluoTest.Name = "buttonFluoTest";
            this.buttonFluoTest.UseVisualStyleBackColor = true;
            this.buttonFluoTest.Click += new System.EventHandler(this.buttonFluoTest_Click);
            // 
            // textBoxASU
            // 
            resources.ApplyResources(this.textBoxASU, "textBoxASU");
            this.textBoxASU.Name = "textBoxASU";
            this.textBoxASU.ReadOnly = true;
            // 
            // buttonGetIDCode
            // 
            resources.ApplyResources(this.buttonGetIDCode, "buttonGetIDCode");
            this.buttonGetIDCode.Name = "buttonGetIDCode";
            this.buttonGetIDCode.UseVisualStyleBackColor = true;
            this.buttonGetIDCode.Click += new System.EventHandler(this.buttonGetIDCode_Click);
            // 
            // buttonStartTH
            // 
            resources.ApplyResources(this.buttonStartTH, "buttonStartTH");
            this.buttonStartTH.Name = "buttonStartTH";
            this.buttonStartTH.UseVisualStyleBackColor = true;
            this.buttonStartTH.Click += new System.EventHandler(this.buttonStartTH_Click);
            // 
            // labelIDRead
            // 
            resources.ApplyResources(this.labelIDRead, "labelIDRead");
            this.labelIDRead.Name = "labelIDRead";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // videoSourcePlayer1
            // 
            resources.ApplyResources(this.videoSourcePlayer1, "videoSourcePlayer1");
            this.videoSourcePlayer1.Name = "videoSourcePlayer1";
            this.videoSourcePlayer1.VideoSource = null;
            this.videoSourcePlayer1.Click += new System.EventHandler(this.videoSourcePlayer1_Click);
            // 
            // buttonCCDTest
            // 
            resources.ApplyResources(this.buttonCCDTest, "buttonCCDTest");
            this.buttonCCDTest.Name = "buttonCCDTest";
            this.buttonCCDTest.UseVisualStyleBackColor = true;
            this.buttonCCDTest.Click += new System.EventHandler(this.buttonCCDTest_Click);
            // 
            // buttonLiquidSensor
            // 
            resources.ApplyResources(this.buttonLiquidSensor, "buttonLiquidSensor");
            this.buttonLiquidSensor.Name = "buttonLiquidSensor";
            this.buttonLiquidSensor.UseVisualStyleBackColor = true;
            this.buttonLiquidSensor.Click += new System.EventHandler(this.buttonLiquidSensor_Click);
            // 
            // buttonReagentSensor
            // 
            resources.ApplyResources(this.buttonReagentSensor, "buttonReagentSensor");
            this.buttonReagentSensor.Name = "buttonReagentSensor";
            this.buttonReagentSensor.UseVisualStyleBackColor = true;
            this.buttonReagentSensor.Click += new System.EventHandler(this.buttonReagentSensor_Click);
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox3, "comboBox3");
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            resources.GetString("comboBox3.Items"),
            resources.GetString("comboBox3.Items1"),
            resources.GetString("comboBox3.Items2"),
            resources.GetString("comboBox3.Items3"),
            resources.GetString("comboBox3.Items4")});
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox3_SelectedIndexChanged);
            // 
            // textBoxQR
            // 
            resources.ApplyResources(this.textBoxQR, "textBoxQR");
            this.textBoxQR.Name = "textBoxQR";
            this.textBoxQR.ReadOnly = true;
            // 
            // textBoxTH
            // 
            resources.ApplyResources(this.textBoxTH, "textBoxTH");
            this.textBoxTH.Name = "textBoxTH";
            this.textBoxTH.ReadOnly = true;
            // 
            // textBoxMain
            // 
            resources.ApplyResources(this.textBoxMain, "textBoxMain");
            this.textBoxMain.Name = "textBoxMain";
            this.textBoxMain.ReadOnly = true;
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // textBoxCCDRef
            // 
            resources.ApplyResources(this.textBoxCCDRef, "textBoxCCDRef");
            this.textBoxCCDRef.Name = "textBoxCCDRef";
            // 
            // buttonFluoFix
            // 
            resources.ApplyResources(this.buttonFluoFix, "buttonFluoFix");
            this.buttonFluoFix.Name = "buttonFluoFix";
            this.buttonFluoFix.UseVisualStyleBackColor = true;
            this.buttonFluoFix.Click += new System.EventHandler(this.button_Click);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // textBoxFluoRef
            // 
            resources.ApplyResources(this.textBoxFluoRef, "textBoxFluoRef");
            this.textBoxFluoRef.Name = "textBoxFluoRef";
            // 
            // textBoxCMD
            // 
            resources.ApplyResources(this.textBoxCMD, "textBoxCMD");
            this.textBoxCMD.Name = "textBoxCMD";
            this.textBoxCMD.Click += new System.EventHandler(this.textBox_Click);
            // 
            // buttonCMDSend
            // 
            resources.ApplyResources(this.buttonCMDSend, "buttonCMDSend");
            this.buttonCMDSend.Name = "buttonCMDSend";
            this.buttonCMDSend.UseVisualStyleBackColor = true;
            this.buttonCMDSend.Click += new System.EventHandler(this.button_Click);
            // 
            // comboBox2
            // 
            resources.ApplyResources(this.comboBox2, "comboBox2");
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Name = "comboBox2";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            resources.GetString("comboBox1.Items"),
            resources.GetString("comboBox1.Items1"),
            resources.GetString("comboBox1.Items2"),
            resources.GetString("comboBox1.Items3"),
            resources.GetString("comboBox1.Items4"),
            resources.GetString("comboBox1.Items5")});
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label35
            // 
            resources.ApplyResources(this.label35, "label35");
            this.label35.Name = "label35";
            // 
            // labelResult
            // 
            resources.ApplyResources(this.labelResult, "labelResult");
            this.labelResult.Name = "labelResult";
            // 
            // buttonTest
            // 
            resources.ApplyResources(this.buttonTest, "buttonTest");
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonMaintain
            // 
            resources.ApplyResources(this.buttonMaintain, "buttonMaintain");
            this.buttonMaintain.Name = "buttonMaintain";
            this.buttonMaintain.UseVisualStyleBackColor = true;
            this.buttonMaintain.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonMidFix
            // 
            resources.ApplyResources(this.buttonMidFix, "buttonMidFix");
            this.buttonMidFix.Name = "buttonMidFix";
            this.buttonMidFix.UseVisualStyleBackColor = true;
            this.buttonMidFix.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonDilutionMode
            // 
            resources.ApplyResources(this.buttonDilutionMode, "buttonDilutionMode");
            this.buttonDilutionMode.Name = "buttonDilutionMode";
            this.buttonDilutionMode.UseVisualStyleBackColor = true;
            this.buttonDilutionMode.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonLigthFix
            // 
            resources.ApplyResources(this.buttonLigthFix, "buttonLigthFix");
            this.buttonLigthFix.Name = "buttonLigthFix";
            this.buttonLigthFix.UseVisualStyleBackColor = true;
            this.buttonLigthFix.Click += new System.EventHandler(this.button_Click);
            // 
            // chartFluo
            // 
            this.chartFluo.BorderlineColor = System.Drawing.Color.Black;
            this.chartFluo.BorderlineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.Name = "ChartArea1";
            this.chartFluo.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chartFluo.Legends.Add(legend1);
            resources.ApplyResources(this.chartFluo, "chartFluo");
            this.chartFluo.Name = "chartFluo";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartFluo.Series.Add(series1);
            // 
            // buttonSaveLog
            // 
            resources.ApplyResources(this.buttonSaveLog, "buttonSaveLog");
            this.buttonSaveLog.Name = "buttonSaveLog";
            this.buttonSaveLog.UseVisualStyleBackColor = true;
            this.buttonSaveLog.Click += new System.EventHandler(this.button_Click);
            // 
            // textBoxLog
            // 
            resources.ApplyResources(this.textBoxLog, "textBoxLog");
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            // 
            // tabPageHome
            // 
            this.tabPageHome.Controls.Add(this.panelPic);
            this.tabPageHome.Controls.Add(this.panel3);
            this.tabPageHome.Controls.Add(this.panel1);
            resources.ApplyResources(this.tabPageHome, "tabPageHome");
            this.tabPageHome.Name = "tabPageHome";
            this.tabPageHome.UseVisualStyleBackColor = true;
            // 
            // panelPic
            // 
            this.panelPic.Controls.Add(this.buttonPicClose);
            this.panelPic.Controls.Add(this.pictureBoxResult);
            resources.ApplyResources(this.panelPic, "panelPic");
            this.panelPic.Name = "panelPic";
            // 
            // buttonPicClose
            // 
            this.buttonPicClose.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonPicClose, "buttonPicClose");
            this.buttonPicClose.FlatAppearance.BorderSize = 0;
            this.buttonPicClose.ForeColor = System.Drawing.Color.White;
            this.buttonPicClose.Name = "buttonPicClose";
            this.buttonPicClose.UseVisualStyleBackColor = true;
            this.buttonPicClose.Click += new System.EventHandler(this.button_Click);
            // 
            // pictureBoxResult
            // 
            resources.ApplyResources(this.pictureBoxResult, "pictureBoxResult");
            this.pictureBoxResult.Name = "pictureBoxResult";
            this.pictureBoxResult.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::i_Reader_S.Properties.Resources.background1;
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.labelCleanStatus);
            this.panel3.Controls.Add(this.labelDilutionStatus);
            this.panel3.Controls.Add(this.labelReagentStatus);
            this.panel3.Controls.Add(this.labelWasteStatus);
            this.panel3.Controls.Add(this.buttonSupply);
            this.panel3.Controls.Add(this.buttonReagent);
            this.panel3.Controls.Add(this.tabControlMainRight);
            this.panel3.Name = "panel3";
            // 
            // labelCleanStatus
            // 
            this.labelCleanStatus.BackColor = System.Drawing.Color.Red;
            this.labelCleanStatus.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.labelCleanStatus, "labelCleanStatus");
            this.labelCleanStatus.Name = "labelCleanStatus";
            // 
            // labelDilutionStatus
            // 
            this.labelDilutionStatus.BackColor = System.Drawing.Color.Red;
            this.labelDilutionStatus.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.labelDilutionStatus, "labelDilutionStatus");
            this.labelDilutionStatus.Name = "labelDilutionStatus";
            // 
            // labelReagentStatus
            // 
            this.labelReagentStatus.BackColor = System.Drawing.Color.Red;
            this.labelReagentStatus.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.labelReagentStatus, "labelReagentStatus");
            this.labelReagentStatus.Name = "labelReagentStatus";
            // 
            // labelWasteStatus
            // 
            this.labelWasteStatus.BackColor = System.Drawing.Color.Red;
            this.labelWasteStatus.ForeColor = System.Drawing.Color.White;
            resources.ApplyResources(this.labelWasteStatus, "labelWasteStatus");
            this.labelWasteStatus.Name = "labelWasteStatus";
            // 
            // buttonSupply
            // 
            this.buttonSupply.BackgroundImage = global::i_Reader_S.Properties.Resources.Button_Normal;
            resources.ApplyResources(this.buttonSupply, "buttonSupply");
            this.buttonSupply.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonSupply.FlatAppearance.BorderSize = 0;
            this.buttonSupply.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonSupply.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonSupply.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonSupply.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.buttonSupply.Name = "buttonSupply";
            this.buttonSupply.UseVisualStyleBackColor = true;
            this.buttonSupply.Click += new System.EventHandler(this.buttonHomeRight_Click);
            // 
            // buttonReagent
            // 
            this.buttonReagent.BackgroundImage = global::i_Reader_S.Properties.Resources.Button_Press;
            resources.ApplyResources(this.buttonReagent, "buttonReagent");
            this.buttonReagent.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonReagent.FlatAppearance.BorderSize = 0;
            this.buttonReagent.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonReagent.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonReagent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonReagent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.buttonReagent.Name = "buttonReagent";
            this.buttonReagent.UseVisualStyleBackColor = true;
            this.buttonReagent.Click += new System.EventHandler(this.buttonHomeRight_Click);
            // 
            // tabControlMainRight
            // 
            this.tabControlMainRight.Controls.Add(this.tabPageReagent);
            this.tabControlMainRight.Controls.Add(this.tabPageSupply);
            this.tabControlMainRight.Controls.Add(this.tabPageReagentOpen);
            this.tabControlMainRight.Controls.Add(this.tabPageQRAlert);
            this.tabControlMainRight.Controls.Add(this.tabPageSupplyFloatBall);
            resources.ApplyResources(this.tabControlMainRight, "tabControlMainRight");
            this.tabControlMainRight.Name = "tabControlMainRight";
            this.tabControlMainRight.SelectedIndex = 0;
            // 
            // tabPageReagent
            // 
            this.tabPageReagent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.tabPageReagent.Controls.Add(this.panelReagent5);
            this.tabPageReagent.Controls.Add(this.panelReagent4);
            this.tabPageReagent.Controls.Add(this.panelReagent1);
            this.tabPageReagent.Controls.Add(this.panelReagent3);
            this.tabPageReagent.Controls.Add(this.panelReagent2);
            resources.ApplyResources(this.tabPageReagent, "tabPageReagent");
            this.tabPageReagent.Name = "tabPageReagent";
            // 
            // panelReagent5
            // 
            resources.ApplyResources(this.panelReagent5, "panelReagent5");
            this.panelReagent5.Controls.Add(this.labelExt5);
            this.panelReagent5.Controls.Add(this.labelReagentLeft5);
            this.panelReagent5.Controls.Add(this.labelLotNo5);
            this.panelReagent5.Controls.Add(this.labelProduct5);
            this.panelReagent5.Name = "panelReagent5";
            this.panelReagent5.Click += new System.EventHandler(this.panelReagent_Click);
            // 
            // labelExt5
            // 
            this.labelExt5.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelExt5, "labelExt5");
            this.labelExt5.Name = "labelExt5";
            // 
            // labelReagentLeft5
            // 
            this.labelReagentLeft5.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelReagentLeft5, "labelReagentLeft5");
            this.labelReagentLeft5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelReagentLeft5.Name = "labelReagentLeft5";
            // 
            // labelLotNo5
            // 
            this.labelLotNo5.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelLotNo5, "labelLotNo5");
            this.labelLotNo5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelLotNo5.Name = "labelLotNo5";
            // 
            // labelProduct5
            // 
            this.labelProduct5.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelProduct5, "labelProduct5");
            this.labelProduct5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelProduct5.Name = "labelProduct5";
            // 
            // panelReagent4
            // 
            resources.ApplyResources(this.panelReagent4, "panelReagent4");
            this.panelReagent4.Controls.Add(this.labelExt4);
            this.panelReagent4.Controls.Add(this.labelReagentLeft4);
            this.panelReagent4.Controls.Add(this.labelLotNo4);
            this.panelReagent4.Controls.Add(this.labelProduct4);
            this.panelReagent4.Name = "panelReagent4";
            this.panelReagent4.Click += new System.EventHandler(this.panelReagent_Click);
            // 
            // labelExt4
            // 
            this.labelExt4.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelExt4, "labelExt4");
            this.labelExt4.Name = "labelExt4";
            // 
            // labelReagentLeft4
            // 
            this.labelReagentLeft4.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelReagentLeft4, "labelReagentLeft4");
            this.labelReagentLeft4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelReagentLeft4.Name = "labelReagentLeft4";
            // 
            // labelLotNo4
            // 
            this.labelLotNo4.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelLotNo4, "labelLotNo4");
            this.labelLotNo4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelLotNo4.Name = "labelLotNo4";
            // 
            // labelProduct4
            // 
            this.labelProduct4.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelProduct4, "labelProduct4");
            this.labelProduct4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelProduct4.Name = "labelProduct4";
            // 
            // panelReagent1
            // 
            resources.ApplyResources(this.panelReagent1, "panelReagent1");
            this.panelReagent1.Controls.Add(this.labelExt1);
            this.panelReagent1.Controls.Add(this.labelReagentLeft1);
            this.panelReagent1.Controls.Add(this.labelLotNo1);
            this.panelReagent1.Controls.Add(this.labelProduct1);
            this.panelReagent1.Name = "panelReagent1";
            this.panelReagent1.Click += new System.EventHandler(this.panelReagent_Click);
            // 
            // labelExt1
            // 
            this.labelExt1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelExt1, "labelExt1");
            this.labelExt1.Name = "labelExt1";
            // 
            // labelReagentLeft1
            // 
            this.labelReagentLeft1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelReagentLeft1, "labelReagentLeft1");
            this.labelReagentLeft1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelReagentLeft1.Name = "labelReagentLeft1";
            // 
            // labelLotNo1
            // 
            this.labelLotNo1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelLotNo1, "labelLotNo1");
            this.labelLotNo1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelLotNo1.Name = "labelLotNo1";
            // 
            // labelProduct1
            // 
            this.labelProduct1.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelProduct1, "labelProduct1");
            this.labelProduct1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelProduct1.Name = "labelProduct1";
            // 
            // panelReagent3
            // 
            resources.ApplyResources(this.panelReagent3, "panelReagent3");
            this.panelReagent3.Controls.Add(this.labelExt3);
            this.panelReagent3.Controls.Add(this.labelReagentLeft3);
            this.panelReagent3.Controls.Add(this.labelLotNo3);
            this.panelReagent3.Controls.Add(this.labelProduct3);
            this.panelReagent3.Name = "panelReagent3";
            this.panelReagent3.Click += new System.EventHandler(this.panelReagent_Click);
            // 
            // labelExt3
            // 
            this.labelExt3.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelExt3, "labelExt3");
            this.labelExt3.Name = "labelExt3";
            // 
            // labelReagentLeft3
            // 
            this.labelReagentLeft3.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelReagentLeft3, "labelReagentLeft3");
            this.labelReagentLeft3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelReagentLeft3.Name = "labelReagentLeft3";
            // 
            // labelLotNo3
            // 
            this.labelLotNo3.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelLotNo3, "labelLotNo3");
            this.labelLotNo3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelLotNo3.Name = "labelLotNo3";
            // 
            // labelProduct3
            // 
            this.labelProduct3.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelProduct3, "labelProduct3");
            this.labelProduct3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelProduct3.Name = "labelProduct3";
            // 
            // panelReagent2
            // 
            resources.ApplyResources(this.panelReagent2, "panelReagent2");
            this.panelReagent2.Controls.Add(this.labelExt2);
            this.panelReagent2.Controls.Add(this.labelReagentLeft2);
            this.panelReagent2.Controls.Add(this.labelLotNo2);
            this.panelReagent2.Controls.Add(this.labelProduct2);
            this.panelReagent2.Name = "panelReagent2";
            this.panelReagent2.Click += new System.EventHandler(this.panelReagent_Click);
            // 
            // labelExt2
            // 
            this.labelExt2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelExt2, "labelExt2");
            this.labelExt2.Name = "labelExt2";
            // 
            // labelReagentLeft2
            // 
            this.labelReagentLeft2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelReagentLeft2, "labelReagentLeft2");
            this.labelReagentLeft2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelReagentLeft2.Name = "labelReagentLeft2";
            // 
            // labelLotNo2
            // 
            this.labelLotNo2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelLotNo2, "labelLotNo2");
            this.labelLotNo2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelLotNo2.Name = "labelLotNo2";
            // 
            // labelProduct2
            // 
            this.labelProduct2.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.labelProduct2, "labelProduct2");
            this.labelProduct2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelProduct2.Name = "labelProduct2";
            // 
            // tabPageSupply
            // 
            this.tabPageSupply.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.tabPageSupply.Controls.Add(this.panelWasteDisable);
            this.tabPageSupply.Controls.Add(this.buttonModifyLeft);
            this.tabPageSupply.Controls.Add(this.buttonModifyVolume);
            this.tabPageSupply.Controls.Add(this.panelClean3);
            this.tabPageSupply.Controls.Add(this.panelDilution3);
            this.tabPageSupply.Controls.Add(this.panelWasteReagent3);
            this.tabPageSupply.Controls.Add(this.panelWaste3);
            this.tabPageSupply.Controls.Add(this.panelWasteReagent2);
            this.tabPageSupply.Controls.Add(this.panelClean2);
            this.tabPageSupply.Controls.Add(this.panelWaste2);
            this.tabPageSupply.Controls.Add(this.panelDilution2);
            resources.ApplyResources(this.tabPageSupply, "tabPageSupply");
            this.tabPageSupply.Name = "tabPageSupply";
            // 
            // panelWasteDisable
            // 
            this.panelWasteDisable.Controls.Add(this.label33);
            resources.ApplyResources(this.panelWasteDisable, "panelWasteDisable");
            this.panelWasteDisable.Name = "panelWasteDisable";
            // 
            // label33
            // 
            resources.ApplyResources(this.label33, "label33");
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label33.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.label33.Name = "label33";
            this.label33.Tag = "";
            // 
            // buttonModifyLeft
            // 
            this.buttonModifyLeft.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonModifyLeft, "buttonModifyLeft");
            this.buttonModifyLeft.Name = "buttonModifyLeft";
            this.buttonModifyLeft.UseVisualStyleBackColor = false;
            this.buttonModifyLeft.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonModifyVolume
            // 
            this.buttonModifyVolume.BackColor = System.Drawing.Color.Gainsboro;
            resources.ApplyResources(this.buttonModifyVolume, "buttonModifyVolume");
            this.buttonModifyVolume.Name = "buttonModifyVolume";
            this.buttonModifyVolume.UseVisualStyleBackColor = false;
            this.buttonModifyVolume.Click += new System.EventHandler(this.button_Click);
            // 
            // panelClean3
            // 
            this.panelClean3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(89)))), ((int)(((byte)(143)))));
            this.panelClean3.Controls.Add(this.labelWashingLiquid);
            resources.ApplyResources(this.panelClean3, "panelClean3");
            this.panelClean3.Name = "panelClean3";
            // 
            // labelWashingLiquid
            // 
            resources.ApplyResources(this.labelWashingLiquid, "labelWashingLiquid");
            this.labelWashingLiquid.BackColor = System.Drawing.Color.Transparent;
            this.labelWashingLiquid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelWashingLiquid.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelWashingLiquid.Name = "labelWashingLiquid";
            this.labelWashingLiquid.Tag = "";
            // 
            // panelDilution3
            // 
            this.panelDilution3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(89)))), ((int)(((byte)(143)))));
            this.panelDilution3.Controls.Add(this.labelDilutionLiquid);
            resources.ApplyResources(this.panelDilution3, "panelDilution3");
            this.panelDilution3.Name = "panelDilution3";
            // 
            // labelDilutionLiquid
            // 
            resources.ApplyResources(this.labelDilutionLiquid, "labelDilutionLiquid");
            this.labelDilutionLiquid.BackColor = System.Drawing.Color.Transparent;
            this.labelDilutionLiquid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelDilutionLiquid.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelDilutionLiquid.Name = "labelDilutionLiquid";
            this.labelDilutionLiquid.Tag = "";
            // 
            // panelWasteReagent3
            // 
            this.panelWasteReagent3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(89)))), ((int)(((byte)(143)))));
            this.panelWasteReagent3.Controls.Add(this.labelWasteReagent);
            resources.ApplyResources(this.panelWasteReagent3, "panelWasteReagent3");
            this.panelWasteReagent3.Name = "panelWasteReagent3";
            // 
            // labelWasteReagent
            // 
            resources.ApplyResources(this.labelWasteReagent, "labelWasteReagent");
            this.labelWasteReagent.BackColor = System.Drawing.Color.Transparent;
            this.labelWasteReagent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelWasteReagent.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelWasteReagent.Name = "labelWasteReagent";
            this.labelWasteReagent.Tag = "";
            // 
            // panelWaste3
            // 
            this.panelWaste3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(89)))), ((int)(((byte)(143)))));
            this.panelWaste3.Controls.Add(this.labelWasteLiquid);
            resources.ApplyResources(this.panelWaste3, "panelWaste3");
            this.panelWaste3.Name = "panelWaste3";
            // 
            // labelWasteLiquid
            // 
            resources.ApplyResources(this.labelWasteLiquid, "labelWasteLiquid");
            this.labelWasteLiquid.BackColor = System.Drawing.Color.Transparent;
            this.labelWasteLiquid.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelWasteLiquid.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelWasteLiquid.Name = "labelWasteLiquid";
            this.labelWasteLiquid.Tag = "";
            // 
            // panelWasteReagent2
            // 
            this.panelWasteReagent2.BackgroundImage = global::i_Reader_S.Properties.Resources.ReagentFull4;
            resources.ApplyResources(this.panelWasteReagent2, "panelWasteReagent2");
            this.panelWasteReagent2.Controls.Add(this.panelWasteReagent1);
            this.panelWasteReagent2.Controls.Add(this.labelWasteReagent1);
            this.panelWasteReagent2.Name = "panelWasteReagent2";
            // 
            // panelWasteReagent1
            // 
            this.panelWasteReagent1.BackColor = System.Drawing.Color.Transparent;
            this.panelWasteReagent1.BackgroundImage = global::i_Reader_S.Properties.Resources.ReagentEmpty1;
            this.panelWasteReagent1.Controls.Add(this.labelWasteReagent2);
            resources.ApplyResources(this.panelWasteReagent1, "panelWasteReagent1");
            this.panelWasteReagent1.Name = "panelWasteReagent1";
            // 
            // labelWasteReagent2
            // 
            resources.ApplyResources(this.labelWasteReagent2, "labelWasteReagent2");
            this.labelWasteReagent2.BackColor = System.Drawing.Color.Transparent;
            this.labelWasteReagent2.Name = "labelWasteReagent2";
            // 
            // labelWasteReagent1
            // 
            resources.ApplyResources(this.labelWasteReagent1, "labelWasteReagent1");
            this.labelWasteReagent1.BackColor = System.Drawing.Color.Transparent;
            this.labelWasteReagent1.Name = "labelWasteReagent1";
            // 
            // panelClean2
            // 
            this.panelClean2.BackgroundImage = global::i_Reader_S.Properties.Resources.BottleFull;
            resources.ApplyResources(this.panelClean2, "panelClean2");
            this.panelClean2.Controls.Add(this.panelClean1);
            this.panelClean2.Controls.Add(this.labelClean1);
            this.panelClean2.Controls.Add(this.buttonClean2);
            this.panelClean2.Controls.Add(this.buttonClean1);
            this.panelClean2.Name = "panelClean2";
            // 
            // panelClean1
            // 
            this.panelClean1.BackColor = System.Drawing.Color.Transparent;
            this.panelClean1.BackgroundImage = global::i_Reader_S.Properties.Resources.BottleEmpty;
            this.panelClean1.Controls.Add(this.labelClean2);
            resources.ApplyResources(this.panelClean1, "panelClean1");
            this.panelClean1.Name = "panelClean1";
            // 
            // labelClean2
            // 
            resources.ApplyResources(this.labelClean2, "labelClean2");
            this.labelClean2.BackColor = System.Drawing.Color.Transparent;
            this.labelClean2.Name = "labelClean2";
            // 
            // labelClean1
            // 
            resources.ApplyResources(this.labelClean1, "labelClean1");
            this.labelClean1.BackColor = System.Drawing.Color.Transparent;
            this.labelClean1.Name = "labelClean1";
            // 
            // buttonClean2
            // 
            this.buttonClean2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.buttonClean2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.buttonClean2.FlatAppearance.BorderSize = 0;
            this.buttonClean2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.buttonClean2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            resources.ApplyResources(this.buttonClean2, "buttonClean2");
            this.buttonClean2.Name = "buttonClean2";
            this.buttonClean2.UseVisualStyleBackColor = false;
            this.buttonClean2.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonClean1
            // 
            this.buttonClean1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            this.buttonClean1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            this.buttonClean1.FlatAppearance.BorderSize = 0;
            this.buttonClean1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            this.buttonClean1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            resources.ApplyResources(this.buttonClean1, "buttonClean1");
            this.buttonClean1.Name = "buttonClean1";
            this.buttonClean1.UseVisualStyleBackColor = false;
            this.buttonClean1.Click += new System.EventHandler(this.button_Click);
            // 
            // panelWaste2
            // 
            this.panelWaste2.BackgroundImage = global::i_Reader_S.Properties.Resources.BottleFull;
            resources.ApplyResources(this.panelWaste2, "panelWaste2");
            this.panelWaste2.Controls.Add(this.panelWaste1);
            this.panelWaste2.Controls.Add(this.labelWaste1);
            this.panelWaste2.Controls.Add(this.buttonWaste2);
            this.panelWaste2.Controls.Add(this.buttonWaste1);
            this.panelWaste2.Name = "panelWaste2";
            // 
            // panelWaste1
            // 
            this.panelWaste1.BackColor = System.Drawing.Color.Transparent;
            this.panelWaste1.BackgroundImage = global::i_Reader_S.Properties.Resources.BottleEmpty;
            this.panelWaste1.Controls.Add(this.labelWaste2);
            resources.ApplyResources(this.panelWaste1, "panelWaste1");
            this.panelWaste1.Name = "panelWaste1";
            // 
            // labelWaste2
            // 
            resources.ApplyResources(this.labelWaste2, "labelWaste2");
            this.labelWaste2.BackColor = System.Drawing.Color.Transparent;
            this.labelWaste2.Name = "labelWaste2";
            // 
            // labelWaste1
            // 
            resources.ApplyResources(this.labelWaste1, "labelWaste1");
            this.labelWaste1.BackColor = System.Drawing.Color.Transparent;
            this.labelWaste1.Name = "labelWaste1";
            // 
            // buttonWaste2
            // 
            this.buttonWaste2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.buttonWaste2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.buttonWaste2.FlatAppearance.BorderSize = 0;
            this.buttonWaste2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.buttonWaste2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            resources.ApplyResources(this.buttonWaste2, "buttonWaste2");
            this.buttonWaste2.Name = "buttonWaste2";
            this.buttonWaste2.UseVisualStyleBackColor = false;
            this.buttonWaste2.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonWaste1
            // 
            this.buttonWaste1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            this.buttonWaste1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            this.buttonWaste1.FlatAppearance.BorderSize = 0;
            this.buttonWaste1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            this.buttonWaste1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            resources.ApplyResources(this.buttonWaste1, "buttonWaste1");
            this.buttonWaste1.Name = "buttonWaste1";
            this.buttonWaste1.UseVisualStyleBackColor = false;
            this.buttonWaste1.Click += new System.EventHandler(this.button_Click);
            // 
            // panelDilution2
            // 
            this.panelDilution2.BackgroundImage = global::i_Reader_S.Properties.Resources.BottleFull;
            resources.ApplyResources(this.panelDilution2, "panelDilution2");
            this.panelDilution2.Controls.Add(this.panelDilution1);
            this.panelDilution2.Controls.Add(this.labelDilution1);
            this.panelDilution2.Controls.Add(this.buttonDilution2);
            this.panelDilution2.Controls.Add(this.buttonDilution1);
            this.panelDilution2.Name = "panelDilution2";
            // 
            // panelDilution1
            // 
            this.panelDilution1.BackColor = System.Drawing.Color.Transparent;
            this.panelDilution1.BackgroundImage = global::i_Reader_S.Properties.Resources.BottleEmpty;
            this.panelDilution1.Controls.Add(this.labelDilution2);
            resources.ApplyResources(this.panelDilution1, "panelDilution1");
            this.panelDilution1.Name = "panelDilution1";
            // 
            // labelDilution2
            // 
            resources.ApplyResources(this.labelDilution2, "labelDilution2");
            this.labelDilution2.BackColor = System.Drawing.Color.Transparent;
            this.labelDilution2.Name = "labelDilution2";
            // 
            // labelDilution1
            // 
            resources.ApplyResources(this.labelDilution1, "labelDilution1");
            this.labelDilution1.BackColor = System.Drawing.Color.Transparent;
            this.labelDilution1.Name = "labelDilution1";
            // 
            // buttonDilution2
            // 
            this.buttonDilution2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.buttonDilution2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.buttonDilution2.FlatAppearance.BorderSize = 0;
            this.buttonDilution2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            this.buttonDilution2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(242)))), ((int)(((byte)(233)))));
            resources.ApplyResources(this.buttonDilution2, "buttonDilution2");
            this.buttonDilution2.Name = "buttonDilution2";
            this.buttonDilution2.UseVisualStyleBackColor = false;
            this.buttonDilution2.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonDilution1
            // 
            this.buttonDilution1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            this.buttonDilution1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            this.buttonDilution1.FlatAppearance.BorderSize = 0;
            this.buttonDilution1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            this.buttonDilution1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(190)))), ((int)(((byte)(188)))));
            resources.ApplyResources(this.buttonDilution1, "buttonDilution1");
            this.buttonDilution1.Name = "buttonDilution1";
            this.buttonDilution1.UseVisualStyleBackColor = false;
            this.buttonDilution1.Click += new System.EventHandler(this.button_Click);
            // 
            // tabPageReagentOpen
            // 
            this.tabPageReagentOpen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.tabPageReagentOpen.Controls.Add(this.labelReagentInfo);
            this.tabPageReagentOpen.Controls.Add(this.buttonSetFirst);
            this.tabPageReagentOpen.Controls.Add(this.labelReagentOperation);
            this.tabPageReagentOpen.Controls.Add(this.labelQR);
            resources.ApplyResources(this.tabPageReagentOpen, "tabPageReagentOpen");
            this.tabPageReagentOpen.Name = "tabPageReagentOpen";
            // 
            // labelReagentInfo
            // 
            resources.ApplyResources(this.labelReagentInfo, "labelReagentInfo");
            this.labelReagentInfo.Name = "labelReagentInfo";
            // 
            // buttonSetFirst
            // 
            this.buttonSetFirst.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.buttonSetFirst, "buttonSetFirst");
            this.buttonSetFirst.ForeColor = System.Drawing.Color.White;
            this.buttonSetFirst.Name = "buttonSetFirst";
            this.buttonSetFirst.UseVisualStyleBackColor = false;
            this.buttonSetFirst.Click += new System.EventHandler(this.button_Click);
            // 
            // labelReagentOperation
            // 
            resources.ApplyResources(this.labelReagentOperation, "labelReagentOperation");
            this.labelReagentOperation.Name = "labelReagentOperation";
            // 
            // labelQR
            // 
            resources.ApplyResources(this.labelQR, "labelQR");
            this.labelQR.Name = "labelQR";
            // 
            // tabPageQRAlert
            // 
            this.tabPageQRAlert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.tabPageQRAlert.Controls.Add(this.labelQR2);
            this.tabPageQRAlert.Controls.Add(this.labelQRAlert);
            this.tabPageQRAlert.Controls.Add(this.buttonQRAlertOK);
            resources.ApplyResources(this.tabPageQRAlert, "tabPageQRAlert");
            this.tabPageQRAlert.Name = "tabPageQRAlert";
            // 
            // labelQR2
            // 
            resources.ApplyResources(this.labelQR2, "labelQR2");
            this.labelQR2.Name = "labelQR2";
            // 
            // labelQRAlert
            // 
            resources.ApplyResources(this.labelQRAlert, "labelQRAlert");
            this.labelQRAlert.Name = "labelQRAlert";
            // 
            // buttonQRAlertOK
            // 
            this.buttonQRAlertOK.BackColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.buttonQRAlertOK, "buttonQRAlertOK");
            this.buttonQRAlertOK.ForeColor = System.Drawing.Color.White;
            this.buttonQRAlertOK.Name = "buttonQRAlertOK";
            this.buttonQRAlertOK.UseVisualStyleBackColor = false;
            this.buttonQRAlertOK.Click += new System.EventHandler(this.button_Click);
            // 
            // tabPageSupplyFloatBall
            // 
            this.tabPageSupplyFloatBall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.tabPageSupplyFloatBall.Controls.Add(this.labelFloatBallWasteReagent);
            this.tabPageSupplyFloatBall.Controls.Add(this.labelFloatBallWaste);
            this.tabPageSupplyFloatBall.Controls.Add(this.labelFloatBallClean);
            this.tabPageSupplyFloatBall.Controls.Add(this.labelFloatBallDilution);
            resources.ApplyResources(this.tabPageSupplyFloatBall, "tabPageSupplyFloatBall");
            this.tabPageSupplyFloatBall.Name = "tabPageSupplyFloatBall";
            // 
            // labelFloatBallWasteReagent
            // 
            this.labelFloatBallWasteReagent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(169)))), ((int)(((byte)(223)))));
            resources.ApplyResources(this.labelFloatBallWasteReagent, "labelFloatBallWasteReagent");
            this.labelFloatBallWasteReagent.ForeColor = System.Drawing.Color.White;
            this.labelFloatBallWasteReagent.Name = "labelFloatBallWasteReagent";
            // 
            // labelFloatBallWaste
            // 
            this.labelFloatBallWaste.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(169)))), ((int)(((byte)(223)))));
            resources.ApplyResources(this.labelFloatBallWaste, "labelFloatBallWaste");
            this.labelFloatBallWaste.ForeColor = System.Drawing.Color.White;
            this.labelFloatBallWaste.Name = "labelFloatBallWaste";
            // 
            // labelFloatBallClean
            // 
            this.labelFloatBallClean.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(169)))), ((int)(((byte)(223)))));
            resources.ApplyResources(this.labelFloatBallClean, "labelFloatBallClean");
            this.labelFloatBallClean.ForeColor = System.Drawing.Color.White;
            this.labelFloatBallClean.Name = "labelFloatBallClean";
            this.labelFloatBallClean.Click += new System.EventHandler(this.labelFloatBallClean_Click);
            // 
            // labelFloatBallDilution
            // 
            this.labelFloatBallDilution.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(169)))), ((int)(((byte)(223)))));
            resources.ApplyResources(this.labelFloatBallDilution, "labelFloatBallDilution");
            this.labelFloatBallDilution.ForeColor = System.Drawing.Color.White;
            this.labelFloatBallDilution.Name = "labelFloatBallDilution";
            this.labelFloatBallDilution.Click += new System.EventHandler(this.labelFloatBallDilution_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::i_Reader_S.Properties.Resources.background1;
            this.panel1.Controls.Add(this.labelExceptionNo);
            this.panel1.Controls.Add(this.dataGridViewMain);
            this.panel1.Controls.Add(this.labelNextTestItem);
            this.panel1.Controls.Add(this.labelNextSampleNo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.buttonException);
            this.panel1.Controls.Add(this.buttonDone);
            this.panel1.Controls.Add(this.buttonDoing);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // labelExceptionNo
            // 
            resources.ApplyResources(this.labelExceptionNo, "labelExceptionNo");
            this.labelExceptionNo.BackColor = System.Drawing.Color.Red;
            this.labelExceptionNo.ForeColor = System.Drawing.Color.White;
            this.labelExceptionNo.Name = "labelExceptionNo";
            // 
            // dataGridViewMain
            // 
            this.dataGridViewMain.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewMain.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewMain.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.dataGridViewMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewMain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewMain.ColumnHeadersVisible = false;
            resources.ApplyResources(this.dataGridViewMain, "dataGridViewMain");
            this.dataGridViewMain.Name = "dataGridViewMain";
            this.dataGridViewMain.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewMain.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewMain.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewMain.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewMain.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewMain.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewMain.RowTemplate.Height = 54;
            this.dataGridViewMain.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseClick);
            // 
            // labelNextTestItem
            // 
            resources.ApplyResources(this.labelNextTestItem, "labelNextTestItem");
            this.labelNextTestItem.Name = "labelNextTestItem";
            // 
            // labelNextSampleNo
            // 
            resources.ApplyResources(this.labelNextSampleNo, "labelNextSampleNo");
            this.labelNextSampleNo.Name = "labelNextSampleNo";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::i_Reader_S.Properties.Resources.background2;
            this.panel2.Controls.Add(this.labelOther);
            this.panel2.Controls.Add(this.labelTestitem);
            this.panel2.Controls.Add(this.labelSample);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // labelOther
            // 
            resources.ApplyResources(this.labelOther, "labelOther");
            this.labelOther.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelOther.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelOther.Name = "labelOther";
            // 
            // labelTestitem
            // 
            resources.ApplyResources(this.labelTestitem, "labelTestitem");
            this.labelTestitem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelTestitem.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelTestitem.Name = "labelTestitem";
            // 
            // labelSample
            // 
            resources.ApplyResources(this.labelSample, "labelSample");
            this.labelSample.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelSample.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelSample.Name = "labelSample";
            this.labelSample.Tag = "";
            // 
            // buttonException
            // 
            this.buttonException.BackgroundImage = global::i_Reader_S.Properties.Resources.Button_Press1;
            resources.ApplyResources(this.buttonException, "buttonException");
            this.buttonException.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonException.FlatAppearance.BorderSize = 0;
            this.buttonException.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonException.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonException.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonException.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.buttonException.Name = "buttonException";
            this.buttonException.UseVisualStyleBackColor = true;
            this.buttonException.Click += new System.EventHandler(this.buttonHomeLeft_Click);
            // 
            // buttonDone
            // 
            this.buttonDone.BackgroundImage = global::i_Reader_S.Properties.Resources.Button_Normal;
            resources.ApplyResources(this.buttonDone, "buttonDone");
            this.buttonDone.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonDone.FlatAppearance.BorderSize = 0;
            this.buttonDone.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonDone.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonDone.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonDone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonHomeLeft_Click);
            // 
            // buttonDoing
            // 
            this.buttonDoing.BackgroundImage = global::i_Reader_S.Properties.Resources.Button_Press;
            resources.ApplyResources(this.buttonDoing, "buttonDoing");
            this.buttonDoing.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonDoing.FlatAppearance.BorderSize = 0;
            this.buttonDoing.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonDoing.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonDoing.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonDoing.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.buttonDoing.Name = "buttonDoing";
            this.buttonDoing.UseVisualStyleBackColor = true;
            this.buttonDoing.Click += new System.EventHandler(this.buttonHomeLeft_Click);
            // 
            // tabPageQC
            // 
            resources.ApplyResources(this.tabPageQC, "tabPageQC");
            this.tabPageQC.Controls.Add(this.dataGridViewQC2);
            this.tabPageQC.Controls.Add(this.dataGridViewQC1);
            this.tabPageQC.Controls.Add(this.labelQCInfo);
            this.tabPageQC.Controls.Add(this.chart1);
            this.tabPageQC.Name = "tabPageQC";
            this.tabPageQC.UseVisualStyleBackColor = true;
            // 
            // dataGridViewQC2
            // 
            this.dataGridViewQC2.AllowUserToAddRows = false;
            this.dataGridViewQC2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewQC2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewQC2.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridViewQC2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewQC2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.Column19,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16});
            resources.ApplyResources(this.dataGridViewQC2, "dataGridViewQC2");
            this.dataGridViewQC2.Name = "dataGridViewQC2";
            this.dataGridViewQC2.RowHeadersVisible = false;
            this.dataGridViewQC2.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewQC2.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewQC2.RowTemplate.Height = 26;
            // 
            // dataGridViewTextBoxColumn1
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn1, "dataGridViewTextBoxColumn1");
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // Column19
            // 
            resources.ApplyResources(this.Column19, "Column19");
            this.Column19.Name = "Column19";
            // 
            // dataGridViewTextBoxColumn2
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn2, "dataGridViewTextBoxColumn2");
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn3, "dataGridViewTextBoxColumn3");
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn4, "dataGridViewTextBoxColumn4");
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn5, "dataGridViewTextBoxColumn5");
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn6, "dataGridViewTextBoxColumn6");
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn7, "dataGridViewTextBoxColumn7");
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn8, "dataGridViewTextBoxColumn8");
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            // 
            // dataGridViewTextBoxColumn9
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn9, "dataGridViewTextBoxColumn9");
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            // 
            // dataGridViewTextBoxColumn10
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn10, "dataGridViewTextBoxColumn10");
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            // 
            // dataGridViewTextBoxColumn11
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn11, "dataGridViewTextBoxColumn11");
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            // 
            // dataGridViewTextBoxColumn12
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn12, "dataGridViewTextBoxColumn12");
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            // 
            // dataGridViewTextBoxColumn13
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn13, "dataGridViewTextBoxColumn13");
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn14, "dataGridViewTextBoxColumn14");
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            // 
            // dataGridViewTextBoxColumn15
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn15, "dataGridViewTextBoxColumn15");
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            // 
            // dataGridViewTextBoxColumn16
            // 
            resources.ApplyResources(this.dataGridViewTextBoxColumn16, "dataGridViewTextBoxColumn16");
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            // 
            // dataGridViewQC1
            // 
            this.dataGridViewQC1.AllowUserToAddRows = false;
            this.dataGridViewQC1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewQC1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewQC1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridViewQC1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewQC1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18});
            resources.ApplyResources(this.dataGridViewQC1, "dataGridViewQC1");
            this.dataGridViewQC1.Name = "dataGridViewQC1";
            this.dataGridViewQC1.RowHeadersVisible = false;
            this.dataGridViewQC1.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewQC1.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewQC1.RowTemplate.Height = 26;
            // 
            // Column2
            // 
            resources.ApplyResources(this.Column2, "Column2");
            this.Column2.Name = "Column2";
            // 
            // Column4
            // 
            resources.ApplyResources(this.Column4, "Column4");
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            resources.ApplyResources(this.Column5, "Column5");
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            resources.ApplyResources(this.Column6, "Column6");
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            resources.ApplyResources(this.Column7, "Column7");
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            resources.ApplyResources(this.Column8, "Column8");
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            resources.ApplyResources(this.Column9, "Column9");
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            resources.ApplyResources(this.Column10, "Column10");
            this.Column10.Name = "Column10";
            // 
            // Column11
            // 
            resources.ApplyResources(this.Column11, "Column11");
            this.Column11.Name = "Column11";
            // 
            // Column12
            // 
            resources.ApplyResources(this.Column12, "Column12");
            this.Column12.Name = "Column12";
            // 
            // Column13
            // 
            resources.ApplyResources(this.Column13, "Column13");
            this.Column13.Name = "Column13";
            // 
            // Column14
            // 
            resources.ApplyResources(this.Column14, "Column14");
            this.Column14.Name = "Column14";
            // 
            // Column15
            // 
            resources.ApplyResources(this.Column15, "Column15");
            this.Column15.Name = "Column15";
            // 
            // Column16
            // 
            resources.ApplyResources(this.Column16, "Column16");
            this.Column16.Name = "Column16";
            // 
            // Column17
            // 
            resources.ApplyResources(this.Column17, "Column17");
            this.Column17.Name = "Column17";
            // 
            // Column18
            // 
            resources.ApplyResources(this.Column18, "Column18");
            this.Column18.Name = "Column18";
            // 
            // labelQCInfo
            // 
            resources.ApplyResources(this.labelQCInfo, "labelQCInfo");
            this.labelQCInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelQCInfo.Name = "labelQCInfo";
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.chart1.BorderlineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisX.LineColor = System.Drawing.Color.White;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.WhiteSmoke;
            chartArea2.AxisX.MinorGrid.LineColor = System.Drawing.Color.White;
            chartArea2.AxisX.MinorTickMark.LineColor = System.Drawing.Color.White;
            chartArea2.AxisX.Title = "时间";
            chartArea2.AxisX.TitleForeColor = System.Drawing.Color.White;
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisY.LineColor = System.Drawing.Color.White;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.White;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea2.AxisY.Title = "SD";
            chartArea2.AxisY.TitleForeColor = System.Drawing.Color.White;
            chartArea2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            legend2.ForeColor = System.Drawing.Color.White;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            resources.ApplyResources(this.chart1, "chart1");
            this.chart1.Name = "chart1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chart1.Series.Add(series2);
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(this.groupBox2);
            this.tabPageSearch.Controls.Add(this.groupBox1);
            this.tabPageSearch.Controls.Add(this.textBoxPage);
            this.tabPageSearch.Controls.Add(this.label28);
            this.tabPageSearch.Controls.Add(this.labelpage);
            this.tabPageSearch.Controls.Add(this.dataGridViewSearch);
            resources.ApplyResources(this.tabPageSearch, "tabPageSearch");
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.labelSampleCount);
            this.groupBox2.Controls.Add(this.labelSearchCostTime);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.textBoxSearchEndDate);
            this.groupBox2.Controls.Add(this.textBoxSearchTestitem);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.buttonSearchDetail);
            this.groupBox2.Controls.Add(this.textBoxSearchStartDate);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // labelSampleCount
            // 
            resources.ApplyResources(this.labelSampleCount, "labelSampleCount");
            this.labelSampleCount.Name = "labelSampleCount";
            // 
            // labelSearchCostTime
            // 
            resources.ApplyResources(this.labelSearchCostTime, "labelSearchCostTime");
            this.labelSearchCostTime.Name = "labelSearchCostTime";
            // 
            // label31
            // 
            resources.ApplyResources(this.label31, "label31");
            this.label31.Name = "label31";
            // 
            // textBoxSearchEndDate
            // 
            resources.ApplyResources(this.textBoxSearchEndDate, "textBoxSearchEndDate");
            this.textBoxSearchEndDate.Name = "textBoxSearchEndDate";
            this.textBoxSearchEndDate.Click += new System.EventHandler(this.textBox_Click);
            // 
            // textBoxSearchTestitem
            // 
            resources.ApplyResources(this.textBoxSearchTestitem, "textBoxSearchTestitem");
            this.textBoxSearchTestitem.Name = "textBoxSearchTestitem";
            this.textBoxSearchTestitem.Click += new System.EventHandler(this.otherControl_TickorClick);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // buttonSearchDetail
            // 
            this.buttonSearchDetail.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonSearchDetail, "buttonSearchDetail");
            this.buttonSearchDetail.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonSearchDetail.FlatAppearance.BorderSize = 0;
            this.buttonSearchDetail.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonSearchDetail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.buttonSearchDetail.ForeColor = System.Drawing.Color.White;
            this.buttonSearchDetail.Name = "buttonSearchDetail";
            this.buttonSearchDetail.UseVisualStyleBackColor = true;
            this.buttonSearchDetail.Click += new System.EventHandler(this.button_Click);
            // 
            // textBoxSearchStartDate
            // 
            resources.ApplyResources(this.textBoxSearchStartDate, "textBoxSearchStartDate");
            this.textBoxSearchStartDate.Name = "textBoxSearchStartDate";
            this.textBoxSearchStartDate.Click += new System.EventHandler(this.textBox_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSearchSampleLike);
            this.groupBox1.Controls.Add(this.buttonSearchSample);
            this.groupBox1.Controls.Add(this.textBoxSearchSample);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // buttonSearchSampleLike
            // 
            this.buttonSearchSampleLike.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonSearchSampleLike, "buttonSearchSampleLike");
            this.buttonSearchSampleLike.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonSearchSampleLike.FlatAppearance.BorderSize = 0;
            this.buttonSearchSampleLike.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonSearchSampleLike.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.buttonSearchSampleLike.ForeColor = System.Drawing.Color.White;
            this.buttonSearchSampleLike.Name = "buttonSearchSampleLike";
            this.buttonSearchSampleLike.UseVisualStyleBackColor = true;
            this.buttonSearchSampleLike.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonSearchSample
            // 
            this.buttonSearchSample.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonSearchSample, "buttonSearchSample");
            this.buttonSearchSample.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonSearchSample.FlatAppearance.BorderSize = 0;
            this.buttonSearchSample.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.buttonSearchSample.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.buttonSearchSample.ForeColor = System.Drawing.Color.White;
            this.buttonSearchSample.Name = "buttonSearchSample";
            this.buttonSearchSample.UseVisualStyleBackColor = true;
            this.buttonSearchSample.Click += new System.EventHandler(this.button_Click);
            // 
            // textBoxSearchSample
            // 
            resources.ApplyResources(this.textBoxSearchSample, "textBoxSearchSample");
            this.textBoxSearchSample.Name = "textBoxSearchSample";
            this.textBoxSearchSample.Click += new System.EventHandler(this.textBox_Click);
            // 
            // textBoxPage
            // 
            resources.ApplyResources(this.textBoxPage, "textBoxPage");
            this.textBoxPage.Name = "textBoxPage";
            this.textBoxPage.Click += new System.EventHandler(this.textBox_Click);
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // labelpage
            // 
            resources.ApplyResources(this.labelpage, "labelpage");
            this.labelpage.BackColor = System.Drawing.Color.Transparent;
            this.labelpage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelpage.Name = "labelpage";
            // 
            // dataGridViewSearch
            // 
            this.dataGridViewSearch.AllowUserToAddRows = false;
            this.dataGridViewSearch.AllowUserToDeleteRows = false;
            this.dataGridViewSearch.AllowUserToResizeColumns = false;
            this.dataGridViewSearch.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewSearch.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewSearch.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewSearch.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSearch.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(this.dataGridViewSearch, "dataGridViewSearch");
            this.dataGridViewSearch.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewSearch.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridViewSearch.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.dataGridViewSearch.Name = "dataGridViewSearch";
            this.dataGridViewSearch.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewSearch.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewSearch.RowTemplate.Height = 40;
            this.dataGridViewSearch.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSearch.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseClick);
            // 
            // contextMenuStrip1
            // 
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.修改压积ToolStripMenuItem,
            this.发送结果ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            this.toolStripMenuItem1.Click += new System.EventHandler(this.otherControl_TickorClick);
            // 
            // 修改压积ToolStripMenuItem
            // 
            this.修改压积ToolStripMenuItem.Name = "修改压积ToolStripMenuItem";
            resources.ApplyResources(this.修改压积ToolStripMenuItem, "修改压积ToolStripMenuItem");
            this.修改压积ToolStripMenuItem.Click += new System.EventHandler(this.修改压积ToolStripMenuItem_Click);
            // 
            // 发送结果ToolStripMenuItem
            // 
            this.发送结果ToolStripMenuItem.Name = "发送结果ToolStripMenuItem";
            resources.ApplyResources(this.发送结果ToolStripMenuItem, "发送结果ToolStripMenuItem");
            this.发送结果ToolStripMenuItem.Click += new System.EventHandler(this.发送结果ToolStripMenuItem_Click);
            // 
            // tabPageSetting
            // 
            this.tabPageSetting.Controls.Add(this.tabControlSetting);
            resources.ApplyResources(this.tabPageSetting, "tabPageSetting");
            this.tabPageSetting.Name = "tabPageSetting";
            this.tabPageSetting.UseVisualStyleBackColor = true;
            // 
            // tabControlSetting
            // 
            this.tabControlSetting.Controls.Add(this.tabPageGeneralSetting);
            this.tabControlSetting.Controls.Add(this.tabPageTestItemSetting);
            this.tabControlSetting.Controls.Add(this.tabPageQCSetting);
            this.tabControlSetting.Controls.Add(this.tabPageOtherSetting);
            this.tabControlSetting.Controls.Add(this.tabPageUser);
            resources.ApplyResources(this.tabControlSetting, "tabControlSetting");
            this.tabControlSetting.Name = "tabControlSetting";
            this.tabControlSetting.SelectedIndex = 0;
            // 
            // tabPageGeneralSetting
            // 
            this.tabPageGeneralSetting.Controls.Add(this.buttonCheckAllUsers);
            this.tabPageGeneralSetting.Controls.Add(this.groupBox4);
            this.tabPageGeneralSetting.Controls.Add(this.groupBox3);
            this.tabPageGeneralSetting.Controls.Add(this.label21);
            resources.ApplyResources(this.tabPageGeneralSetting, "tabPageGeneralSetting");
            this.tabPageGeneralSetting.Name = "tabPageGeneralSetting";
            this.tabPageGeneralSetting.UseVisualStyleBackColor = true;
            // 
            // buttonCheckAllUsers
            // 
            this.buttonCheckAllUsers.BackColor = System.Drawing.Color.White;
            this.buttonCheckAllUsers.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonCheckAllUsers, "buttonCheckAllUsers");
            this.buttonCheckAllUsers.FlatAppearance.BorderSize = 0;
            this.buttonCheckAllUsers.ForeColor = System.Drawing.Color.White;
            this.buttonCheckAllUsers.Name = "buttonCheckAllUsers";
            this.buttonCheckAllUsers.UseVisualStyleBackColor = false;
            this.buttonCheckAllUsers.Click += new System.EventHandler(this.button_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.textBoxStartSample);
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label15.Name = "label15";
            // 
            // textBoxStartSample
            // 
            resources.ApplyResources(this.textBoxStartSample, "textBoxStartSample");
            this.textBoxStartSample.Name = "textBoxStartSample";
            this.textBoxStartSample.Click += new System.EventHandler(this.textBox_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBoxTime);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.textBoxSleepTime);
            this.groupBox3.Controls.Add(this.buttonCRPDebug);
            this.groupBox3.Controls.Add(this.label34);
            this.groupBox3.Controls.Add(this.label32);
            this.groupBox3.Controls.Add(this.buttonAutoPrint);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.buttonWasteMode);
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // textBoxTime
            // 
            resources.ApplyResources(this.textBoxTime, "textBoxTime");
            this.textBoxTime.Name = "textBoxTime";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label13.Name = "label13";
            // 
            // textBoxSleepTime
            // 
            resources.ApplyResources(this.textBoxSleepTime, "textBoxSleepTime");
            this.textBoxSleepTime.Name = "textBoxSleepTime";
            this.textBoxSleepTime.Click += new System.EventHandler(this.textBox_Click);
            // 
            // buttonCRPDebug
            // 
            this.buttonCRPDebug.BackColor = System.Drawing.Color.White;
            this.buttonCRPDebug.BackgroundImage = global::i_Reader_S.Properties.Resources.switch_on;
            resources.ApplyResources(this.buttonCRPDebug, "buttonCRPDebug");
            this.buttonCRPDebug.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonCRPDebug.FlatAppearance.BorderSize = 0;
            this.buttonCRPDebug.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonCRPDebug.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonCRPDebug.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonCRPDebug.Name = "buttonCRPDebug";
            this.buttonCRPDebug.UseVisualStyleBackColor = false;
            this.buttonCRPDebug.Click += new System.EventHandler(this.button_Click);
            // 
            // label34
            // 
            resources.ApplyResources(this.label34, "label34");
            this.label34.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label34.Name = "label34";
            // 
            // label32
            // 
            resources.ApplyResources(this.label32, "label32");
            this.label32.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label32.Name = "label32";
            // 
            // buttonAutoPrint
            // 
            this.buttonAutoPrint.BackColor = System.Drawing.Color.White;
            this.buttonAutoPrint.BackgroundImage = global::i_Reader_S.Properties.Resources.switch_on;
            resources.ApplyResources(this.buttonAutoPrint, "buttonAutoPrint");
            this.buttonAutoPrint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonAutoPrint.FlatAppearance.BorderSize = 0;
            this.buttonAutoPrint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonAutoPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonAutoPrint.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonAutoPrint.Name = "buttonAutoPrint";
            this.buttonAutoPrint.UseVisualStyleBackColor = false;
            this.buttonAutoPrint.Click += new System.EventHandler(this.button_Click);
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label12.Name = "label12";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label14.Name = "label14";
            // 
            // buttonWasteMode
            // 
            this.buttonWasteMode.BackColor = System.Drawing.Color.White;
            this.buttonWasteMode.BackgroundImage = global::i_Reader_S.Properties.Resources.switch_off;
            resources.ApplyResources(this.buttonWasteMode, "buttonWasteMode");
            this.buttonWasteMode.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonWasteMode.FlatAppearance.BorderSize = 0;
            this.buttonWasteMode.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonWasteMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonWasteMode.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonWasteMode.Name = "buttonWasteMode";
            this.buttonWasteMode.UseVisualStyleBackColor = false;
            this.buttonWasteMode.Click += new System.EventHandler(this.button_Click);
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label21.Name = "label21";
            // 
            // tabPageTestItemSetting
            // 
            this.tabPageTestItemSetting.Controls.Add(this.buttonAddTestItem);
            this.tabPageTestItemSetting.Controls.Add(this.panel4);
            this.tabPageTestItemSetting.Controls.Add(this.dataGridViewTestItem);
            this.tabPageTestItemSetting.Controls.Add(this.label22);
            resources.ApplyResources(this.tabPageTestItemSetting, "tabPageTestItemSetting");
            this.tabPageTestItemSetting.Name = "tabPageTestItemSetting";
            this.tabPageTestItemSetting.UseVisualStyleBackColor = true;
            // 
            // buttonAddTestItem
            // 
            this.buttonAddTestItem.BackColor = System.Drawing.Color.White;
            this.buttonAddTestItem.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonAddTestItem, "buttonAddTestItem");
            this.buttonAddTestItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonAddTestItem.FlatAppearance.BorderSize = 0;
            this.buttonAddTestItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonAddTestItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonAddTestItem.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonAddTestItem.Name = "buttonAddTestItem";
            this.buttonAddTestItem.UseVisualStyleBackColor = false;
            this.buttonAddTestItem.Click += new System.EventHandler(this.button_Click);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.textBoxPreDilu);
            this.panel4.Controls.Add(this.labelPreDilu);
            this.panel4.Controls.Add(this.buttonFixTestItem);
            this.panel4.Controls.Add(this.textBoxSettingUnitRatio);
            this.panel4.Controls.Add(this.textBoxSettingUnitDefault);
            this.panel4.Controls.Add(this.textBoxSettingAccurancy);
            this.panel4.Controls.Add(this.textBoxSettingWarning);
            this.panel4.Controls.Add(this.textBoxSettingRatio);
            this.panel4.Controls.Add(this.textBoxSettingUnit);
            this.panel4.Controls.Add(this.textBoxSettingTestItemName);
            this.panel4.Controls.Add(this.label27);
            this.panel4.Controls.Add(this.label26);
            this.panel4.Controls.Add(this.label25);
            this.panel4.Controls.Add(this.label24);
            this.panel4.Controls.Add(this.label23);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // textBoxPreDilu
            // 
            resources.ApplyResources(this.textBoxPreDilu, "textBoxPreDilu");
            this.textBoxPreDilu.Name = "textBoxPreDilu";
            this.textBoxPreDilu.Click += new System.EventHandler(this.textBox_Click);
            // 
            // labelPreDilu
            // 
            resources.ApplyResources(this.labelPreDilu, "labelPreDilu");
            this.labelPreDilu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelPreDilu.Name = "labelPreDilu";
            // 
            // buttonFixTestItem
            // 
            this.buttonFixTestItem.BackColor = System.Drawing.Color.White;
            this.buttonFixTestItem.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonFixTestItem, "buttonFixTestItem");
            this.buttonFixTestItem.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonFixTestItem.FlatAppearance.BorderSize = 0;
            this.buttonFixTestItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonFixTestItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonFixTestItem.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonFixTestItem.Name = "buttonFixTestItem";
            this.buttonFixTestItem.UseVisualStyleBackColor = false;
            this.buttonFixTestItem.Click += new System.EventHandler(this.button_Click);
            // 
            // textBoxSettingUnitRatio
            // 
            resources.ApplyResources(this.textBoxSettingUnitRatio, "textBoxSettingUnitRatio");
            this.textBoxSettingUnitRatio.Name = "textBoxSettingUnitRatio";
            this.textBoxSettingUnitRatio.ReadOnly = true;
            // 
            // textBoxSettingUnitDefault
            // 
            resources.ApplyResources(this.textBoxSettingUnitDefault, "textBoxSettingUnitDefault");
            this.textBoxSettingUnitDefault.Name = "textBoxSettingUnitDefault";
            this.textBoxSettingUnitDefault.ReadOnly = true;
            // 
            // textBoxSettingAccurancy
            // 
            resources.ApplyResources(this.textBoxSettingAccurancy, "textBoxSettingAccurancy");
            this.textBoxSettingAccurancy.Name = "textBoxSettingAccurancy";
            this.textBoxSettingAccurancy.ReadOnly = true;
            this.textBoxSettingAccurancy.Click += new System.EventHandler(this.textBox_Click);
            // 
            // textBoxSettingWarning
            // 
            resources.ApplyResources(this.textBoxSettingWarning, "textBoxSettingWarning");
            this.textBoxSettingWarning.Name = "textBoxSettingWarning";
            this.textBoxSettingWarning.ReadOnly = true;
            this.textBoxSettingWarning.Click += new System.EventHandler(this.textBox_Click);
            // 
            // textBoxSettingRatio
            // 
            resources.ApplyResources(this.textBoxSettingRatio, "textBoxSettingRatio");
            this.textBoxSettingRatio.Name = "textBoxSettingRatio";
            this.textBoxSettingRatio.ReadOnly = true;
            // 
            // textBoxSettingUnit
            // 
            resources.ApplyResources(this.textBoxSettingUnit, "textBoxSettingUnit");
            this.textBoxSettingUnit.Name = "textBoxSettingUnit";
            this.textBoxSettingUnit.ReadOnly = true;
            // 
            // textBoxSettingTestItemName
            // 
            resources.ApplyResources(this.textBoxSettingTestItemName, "textBoxSettingTestItemName");
            this.textBoxSettingTestItemName.Name = "textBoxSettingTestItemName";
            this.textBoxSettingTestItemName.ReadOnly = true;
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label27.Name = "label27";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label26.Name = "label26";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label25.Name = "label25";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label24.Name = "label24";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label23.Name = "label23";
            // 
            // dataGridViewTestItem
            // 
            this.dataGridViewTestItem.AllowUserToAddRows = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewTestItem.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewTestItem.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewTestItem.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTestItem.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(this.dataGridViewTestItem, "dataGridViewTestItem");
            this.dataGridViewTestItem.Name = "dataGridViewTestItem";
            this.dataGridViewTestItem.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTestItem.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTestItem.RowHeadersVisible = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewTestItem.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewTestItem.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewTestItem.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewTestItem.RowTemplate.Height = 54;
            this.dataGridViewTestItem.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseClick);
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label22.Name = "label22";
            // 
            // tabPageQCSetting
            // 
            this.tabPageQCSetting.Controls.Add(this.label30);
            this.tabPageQCSetting.Controls.Add(this.buttonAutoQC);
            this.tabPageQCSetting.Controls.Add(this.panelQCPoint);
            this.tabPageQCSetting.Controls.Add(this.dataGridViewQCSetting);
            this.tabPageQCSetting.Controls.Add(this.label3);
            resources.ApplyResources(this.tabPageQCSetting, "tabPageQCSetting");
            this.tabPageQCSetting.Name = "tabPageQCSetting";
            this.tabPageQCSetting.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label30.Name = "label30";
            // 
            // buttonAutoQC
            // 
            resources.ApplyResources(this.buttonAutoQC, "buttonAutoQC");
            this.buttonAutoQC.Name = "buttonAutoQC";
            this.buttonAutoQC.UseVisualStyleBackColor = true;
            this.buttonAutoQC.Click += new System.EventHandler(this.buttonAutoQC_Click);
            // 
            // panelQCPoint
            // 
            this.panelQCPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelQCPoint.Controls.Add(this.textBoxQCSD);
            this.panelQCPoint.Controls.Add(this.textBoxQCTarget);
            this.panelQCPoint.Controls.Add(this.textBoxQCSampleNo);
            this.panelQCPoint.Controls.Add(this.buttonFixQCData);
            this.panelQCPoint.Controls.Add(this.label11);
            this.panelQCPoint.Controls.Add(this.label4);
            this.panelQCPoint.Controls.Add(this.label2);
            this.panelQCPoint.Controls.Add(this.buttonAddQCPoint);
            resources.ApplyResources(this.panelQCPoint, "panelQCPoint");
            this.panelQCPoint.Name = "panelQCPoint";
            // 
            // textBoxQCSD
            // 
            resources.ApplyResources(this.textBoxQCSD, "textBoxQCSD");
            this.textBoxQCSD.Name = "textBoxQCSD";
            this.textBoxQCSD.Click += new System.EventHandler(this.textBox_Click);
            // 
            // textBoxQCTarget
            // 
            resources.ApplyResources(this.textBoxQCTarget, "textBoxQCTarget");
            this.textBoxQCTarget.Name = "textBoxQCTarget";
            this.textBoxQCTarget.Click += new System.EventHandler(this.textBox_Click);
            // 
            // textBoxQCSampleNo
            // 
            resources.ApplyResources(this.textBoxQCSampleNo, "textBoxQCSampleNo");
            this.textBoxQCSampleNo.Name = "textBoxQCSampleNo";
            this.textBoxQCSampleNo.ReadOnly = true;
            // 
            // buttonFixQCData
            // 
            this.buttonFixQCData.BackColor = System.Drawing.Color.White;
            this.buttonFixQCData.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonFixQCData, "buttonFixQCData");
            this.buttonFixQCData.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonFixQCData.FlatAppearance.BorderSize = 0;
            this.buttonFixQCData.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonFixQCData.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonFixQCData.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonFixQCData.Name = "buttonFixQCData";
            this.buttonFixQCData.UseVisualStyleBackColor = false;
            this.buttonFixQCData.Click += new System.EventHandler(this.button_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label11.Name = "label11";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label4.Name = "label4";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label2.Name = "label2";
            // 
            // buttonAddQCPoint
            // 
            this.buttonAddQCPoint.BackColor = System.Drawing.Color.White;
            this.buttonAddQCPoint.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonAddQCPoint, "buttonAddQCPoint");
            this.buttonAddQCPoint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonAddQCPoint.FlatAppearance.BorderSize = 0;
            this.buttonAddQCPoint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonAddQCPoint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.buttonAddQCPoint.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonAddQCPoint.Name = "buttonAddQCPoint";
            this.buttonAddQCPoint.UseVisualStyleBackColor = false;
            this.buttonAddQCPoint.Click += new System.EventHandler(this.button_Click);
            // 
            // dataGridViewQCSetting
            // 
            this.dataGridViewQCSetting.AllowUserToAddRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewQCSetting.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridViewQCSetting.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewQCSetting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewQCSetting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridViewQCSetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            resources.ApplyResources(this.dataGridViewQCSetting, "dataGridViewQCSetting");
            this.dataGridViewQCSetting.Name = "dataGridViewQCSetting";
            this.dataGridViewQCSetting.ReadOnly = true;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewQCSetting.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridViewQCSetting.RowHeadersVisible = false;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewQCSetting.RowsDefaultCellStyle = dataGridViewCellStyle13;
            this.dataGridViewQCSetting.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.White;
            this.dataGridViewQCSetting.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewQCSetting.RowTemplate.Height = 54;
            this.dataGridViewQCSetting.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseClick);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label3.Name = "label3";
            // 
            // tabPageOtherSetting
            // 
            this.tabPageOtherSetting.Controls.Add(this.label20);
            this.tabPageOtherSetting.Controls.Add(this.label19);
            this.tabPageOtherSetting.Controls.Add(this.label18);
            this.tabPageOtherSetting.Controls.Add(this.labelDriveLeft);
            this.tabPageOtherSetting.Controls.Add(this.label17);
            resources.ApplyResources(this.tabPageOtherSetting, "tabPageOtherSetting");
            this.tabPageOtherSetting.Name = "tabPageOtherSetting";
            this.tabPageOtherSetting.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label20.Name = "label20";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label19.Name = "label19";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label18.Name = "label18";
            // 
            // labelDriveLeft
            // 
            resources.ApplyResources(this.labelDriveLeft, "labelDriveLeft");
            this.labelDriveLeft.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelDriveLeft.Name = "labelDriveLeft";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label17.Name = "label17";
            // 
            // tabPageUser
            // 
            this.tabPageUser.Controls.Add(this.groupBoxUser);
            this.tabPageUser.Controls.Add(this.panelAllUsers);
            resources.ApplyResources(this.tabPageUser, "tabPageUser");
            this.tabPageUser.Name = "tabPageUser";
            this.tabPageUser.UseVisualStyleBackColor = true;
            // 
            // groupBoxUser
            // 
            this.groupBoxUser.Controls.Add(this.buttonUserDelete);
            this.groupBoxUser.Controls.Add(this.buttonUserChange);
            this.groupBoxUser.Controls.Add(this.comboBoxUserType);
            this.groupBoxUser.Controls.Add(this.label39);
            this.groupBoxUser.Controls.Add(this.textBoxRepeatPassword);
            this.groupBoxUser.Controls.Add(this.textBoxNewPassword);
            this.groupBoxUser.Controls.Add(this.textBoxNewUserName);
            this.groupBoxUser.Controls.Add(this.label38);
            this.groupBoxUser.Controls.Add(this.label37);
            this.groupBoxUser.Controls.Add(this.label36);
            resources.ApplyResources(this.groupBoxUser, "groupBoxUser");
            this.groupBoxUser.Name = "groupBoxUser";
            this.groupBoxUser.TabStop = false;
            // 
            // buttonUserDelete
            // 
            this.buttonUserDelete.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonUserDelete, "buttonUserDelete");
            this.buttonUserDelete.FlatAppearance.BorderSize = 0;
            this.buttonUserDelete.ForeColor = System.Drawing.Color.White;
            this.buttonUserDelete.Name = "buttonUserDelete";
            this.buttonUserDelete.UseVisualStyleBackColor = true;
            // 
            // buttonUserChange
            // 
            this.buttonUserChange.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black1;
            resources.ApplyResources(this.buttonUserChange, "buttonUserChange");
            this.buttonUserChange.FlatAppearance.BorderSize = 0;
            this.buttonUserChange.ForeColor = System.Drawing.Color.White;
            this.buttonUserChange.Name = "buttonUserChange";
            this.buttonUserChange.UseVisualStyleBackColor = true;
            // 
            // comboBoxUserType
            // 
            this.comboBoxUserType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBoxUserType, "comboBoxUserType");
            this.comboBoxUserType.FormattingEnabled = true;
            this.comboBoxUserType.Items.AddRange(new object[] {
            resources.GetString("comboBoxUserType.Items"),
            resources.GetString("comboBoxUserType.Items1")});
            this.comboBoxUserType.Name = "comboBoxUserType";
            // 
            // label39
            // 
            resources.ApplyResources(this.label39, "label39");
            this.label39.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label39.Name = "label39";
            // 
            // textBoxRepeatPassword
            // 
            resources.ApplyResources(this.textBoxRepeatPassword, "textBoxRepeatPassword");
            this.textBoxRepeatPassword.Name = "textBoxRepeatPassword";
            // 
            // textBoxNewPassword
            // 
            resources.ApplyResources(this.textBoxNewPassword, "textBoxNewPassword");
            this.textBoxNewPassword.Name = "textBoxNewPassword";
            // 
            // textBoxNewUserName
            // 
            resources.ApplyResources(this.textBoxNewUserName, "textBoxNewUserName");
            this.textBoxNewUserName.Name = "textBoxNewUserName";
            // 
            // label38
            // 
            resources.ApplyResources(this.label38, "label38");
            this.label38.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label38.Name = "label38";
            // 
            // label37
            // 
            resources.ApplyResources(this.label37, "label37");
            this.label37.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label37.Name = "label37";
            // 
            // label36
            // 
            resources.ApplyResources(this.label36, "label36");
            this.label36.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.label36.Name = "label36";
            // 
            // panelAllUsers
            // 
            this.panelAllUsers.Controls.Add(this.paneluser);
            this.panelAllUsers.Controls.Add(this.dataGridViewAllUsers);
            resources.ApplyResources(this.panelAllUsers, "panelAllUsers");
            this.panelAllUsers.Name = "panelAllUsers";
            // 
            // paneluser
            // 
            this.paneluser.BackColor = System.Drawing.Color.MidnightBlue;
            this.paneluser.Controls.Add(this.label42);
            this.paneluser.Controls.Add(this.label40);
            this.paneluser.Controls.Add(this.label41);
            resources.ApplyResources(this.paneluser, "paneluser");
            this.paneluser.Name = "paneluser";
            // 
            // label42
            // 
            resources.ApplyResources(this.label42, "label42");
            this.label42.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label42.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label42.Name = "label42";
            // 
            // label40
            // 
            resources.ApplyResources(this.label40, "label40");
            this.label40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label40.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label40.Name = "label40";
            // 
            // label41
            // 
            resources.ApplyResources(this.label41, "label41");
            this.label41.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label41.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label41.Name = "label41";
            this.label41.Tag = "";
            // 
            // dataGridViewAllUsers
            // 
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewAllUsers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle14;
            this.dataGridViewAllUsers.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewAllUsers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewAllUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAllUsers.ColumnHeadersVisible = false;
            resources.ApplyResources(this.dataGridViewAllUsers, "dataGridViewAllUsers");
            this.dataGridViewAllUsers.Name = "dataGridViewAllUsers";
            this.dataGridViewAllUsers.ReadOnly = true;
            this.dataGridViewAllUsers.RowHeadersVisible = false;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewAllUsers.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dataGridViewAllUsers.RowTemplate.Height = 60;
            // 
            // tabPageMessage
            // 
            this.tabPageMessage.Controls.Add(this.dataGridViewLog);
            resources.ApplyResources(this.tabPageMessage, "tabPageMessage");
            this.tabPageMessage.Name = "tabPageMessage";
            this.tabPageMessage.UseVisualStyleBackColor = true;
            // 
            // dataGridViewLog
            // 
            this.dataGridViewLog.AllowUserToAddRows = false;
            this.dataGridViewLog.AllowUserToDeleteRows = false;
            this.dataGridViewLog.AllowUserToResizeColumns = false;
            this.dataGridViewLog.AllowUserToResizeRows = false;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            dataGridViewCellStyle16.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewLog.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle16;
            this.dataGridViewLog.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewLog.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("SimSun-ExtB", 9F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewLog.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle17;
            resources.ApplyResources(this.dataGridViewLog, "dataGridViewLog");
            this.dataGridViewLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3});
            this.dataGridViewLog.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.dataGridViewLog.Name = "dataGridViewLog";
            this.dataGridViewLog.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridViewLog.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dataGridViewLog.RowTemplate.Height = 40;
            this.dataGridViewLog.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column1
            // 
            resources.ApplyResources(this.Column1, "Column1");
            this.Column1.Name = "Column1";
            // 
            // Column3
            // 
            resources.ApplyResources(this.Column3, "Column3");
            this.Column3.Name = "Column3";
            // 
            // tabPageStop
            // 
            this.tabPageStop.Controls.Add(this.buttonMinWindow);
            this.tabPageStop.Controls.Add(this.buttonPowerOff);
            this.tabPageStop.Controls.Add(this.buttonStopRecovery);
            this.tabPageStop.Controls.Add(this.buttonStopConfirm);
            this.tabPageStop.Controls.Add(this.labelStopStatus);
            resources.ApplyResources(this.tabPageStop, "tabPageStop");
            this.tabPageStop.Name = "tabPageStop";
            this.tabPageStop.UseVisualStyleBackColor = true;
            // 
            // buttonMinWindow
            // 
            this.buttonMinWindow.BackColor = System.Drawing.Color.White;
            this.buttonMinWindow.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black;
            resources.ApplyResources(this.buttonMinWindow, "buttonMinWindow");
            this.buttonMinWindow.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonMinWindow.FlatAppearance.BorderSize = 0;
            this.buttonMinWindow.ForeColor = System.Drawing.Color.White;
            this.buttonMinWindow.Name = "buttonMinWindow";
            this.buttonMinWindow.UseVisualStyleBackColor = false;
            this.buttonMinWindow.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonPowerOff
            // 
            this.buttonPowerOff.BackColor = System.Drawing.Color.White;
            this.buttonPowerOff.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black;
            resources.ApplyResources(this.buttonPowerOff, "buttonPowerOff");
            this.buttonPowerOff.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonPowerOff.FlatAppearance.BorderSize = 0;
            this.buttonPowerOff.ForeColor = System.Drawing.Color.White;
            this.buttonPowerOff.Name = "buttonPowerOff";
            this.buttonPowerOff.UseVisualStyleBackColor = false;
            this.buttonPowerOff.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonStopRecovery
            // 
            this.buttonStopRecovery.BackColor = System.Drawing.Color.White;
            this.buttonStopRecovery.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black;
            this.buttonStopRecovery.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonStopRecovery.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.buttonStopRecovery, "buttonStopRecovery");
            this.buttonStopRecovery.ForeColor = System.Drawing.Color.White;
            this.buttonStopRecovery.Name = "buttonStopRecovery";
            this.buttonStopRecovery.UseVisualStyleBackColor = false;
            this.buttonStopRecovery.Click += new System.EventHandler(this.button_Click);
            // 
            // buttonStopConfirm
            // 
            this.buttonStopConfirm.BackColor = System.Drawing.Color.White;
            this.buttonStopConfirm.BackgroundImage = global::i_Reader_S.Properties.Resources.button_Black;
            this.buttonStopConfirm.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonStopConfirm.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.buttonStopConfirm, "buttonStopConfirm");
            this.buttonStopConfirm.ForeColor = System.Drawing.Color.White;
            this.buttonStopConfirm.Name = "buttonStopConfirm";
            this.buttonStopConfirm.UseVisualStyleBackColor = false;
            this.buttonStopConfirm.Click += new System.EventHandler(this.button_Click);
            // 
            // labelStopStatus
            // 
            resources.ApplyResources(this.labelStopStatus, "labelStopStatus");
            this.labelStopStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStopStatus.Name = "labelStopStatus";
            this.labelStopStatus.Tag = "";
            // 
            // panelTopBack
            // 
            this.panelTopBack.BackColor = System.Drawing.Color.White;
            this.panelTopBack.BackgroundImage = global::i_Reader_S.Properties.Resources.topbg;
            this.panelTopBack.Controls.Add(this.labelLock);
            this.panelTopBack.Controls.Add(this.panelOther);
            this.panelTopBack.Controls.Add(this.labelMenu);
            this.panelTopBack.Controls.Add(this.buttonStop);
            this.panelTopBack.Controls.Add(this.buttonMessage);
            this.panelTopBack.Controls.Add(this.buttonSetting);
            this.panelTopBack.Controls.Add(this.buttonQC);
            this.panelTopBack.Controls.Add(this.buttonSearch);
            this.panelTopBack.Controls.Add(this.buttonHome);
            resources.ApplyResources(this.panelTopBack, "panelTopBack");
            this.panelTopBack.Name = "panelTopBack";
            // 
            // labelLock
            // 
            resources.ApplyResources(this.labelLock, "labelLock");
            this.labelLock.Name = "labelLock";
            // 
            // panelOther
            // 
            this.panelOther.BackColor = System.Drawing.Color.Transparent;
            resources.ApplyResources(this.panelOther, "panelOther");
            this.panelOther.Name = "panelOther";
            this.panelOther.Click += new System.EventHandler(this.otherControl_TickorClick);
            // 
            // labelMenu
            // 
            resources.ApplyResources(this.labelMenu, "labelMenu");
            this.labelMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.labelMenu.Name = "labelMenu";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.buttonSubMenu_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.button2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            resources.ApplyResources(this.button2, "button2");
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.buttonSubMenu_Click);
            this.button2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.button2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            resources.ApplyResources(this.button3, "button3");
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.button3.Name = "button3";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.buttonSubMenu_Click);
            this.button3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_MouseDown);
            this.button3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_MouseUp);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Transparent;
            this.button4.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            this.button4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(242)))), ((int)(((byte)(231)))));
            resources.ApplyResources(this.button4, "button4");
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(119)))), ((int)(((byte)(114)))), ((int)(((byte)(105)))));
            this.button4.Name = "button4";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.buttonSubMenu_Click);
            // 
            // labelSystemTime
            // 
            resources.ApplyResources(this.labelSystemTime, "labelSystemTime");
            this.labelSystemTime.Name = "labelSystemTime";
            // 
            // timerMain
            // 
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.otherControl_TickorClick);
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // labelMeachineStatus
            // 
            resources.ApplyResources(this.labelMeachineStatus, "labelMeachineStatus");
            this.labelMeachineStatus.Name = "labelMeachineStatus";
            // 
            // timerPageUp
            // 
            this.timerPageUp.Interval = 500;
            this.timerPageUp.Tick += new System.EventHandler(this.otherControl_TickorClick);
            // 
            // timerPageDown
            // 
            this.timerPageDown.Interval = 500;
            this.timerPageDown.Tick += new System.EventHandler(this.otherControl_TickorClick);
            // 
            // serialPortMain
            // 
            this.serialPortMain.BaudRate = 57600;
            this.serialPortMain.DiscardNull = true;
            this.serialPortMain.PortName = "COM2";
            this.serialPortMain.WriteBufferSize = 4096;
            this.serialPortMain.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortMain_DataReceived);
            // 
            // serialPortQR
            // 
            this.serialPortQR.PortName = "COM6";
            this.serialPortQR.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortQR_DataReceived);
            // 
            // serialPortTH
            // 
            this.serialPortTH.BaudRate = 38400;
            this.serialPortTH.PortName = "COM4";
            this.serialPortTH.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortTH_DataReceived);
            // 
            // serialPortFluoMotor
            // 
            this.serialPortFluoMotor.BaudRate = 38400;
            this.serialPortFluoMotor.PortName = "COM8";
            this.serialPortFluoMotor.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFluoMotor_DataReceived);
            // 
            // serialPortFluo
            // 
            this.serialPortFluo.BaudRate = 57600;
            this.serialPortFluo.PortName = "COM6";
            this.serialPortFluo.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFluo_DataReceived);
            // 
            // labelTH
            // 
            resources.ApplyResources(this.labelTH, "labelTH");
            this.labelTH.BackColor = System.Drawing.Color.Transparent;
            this.labelTH.Name = "labelTH";
            // 
            // timerLoding
            // 
            this.timerLoding.Interval = 10;
            this.timerLoding.Tick += new System.EventHandler(this.timerLoding_Tick);
            // 
            // labelSupplyLeft
            // 
            resources.ApplyResources(this.labelSupplyLeft, "labelSupplyLeft");
            this.labelSupplyLeft.BackColor = System.Drawing.Color.Transparent;
            this.labelSupplyLeft.Name = "labelSupplyLeft";
            // 
            // timerLoad
            // 
            this.timerLoad.Interval = 1000;
            this.timerLoad.Tick += new System.EventHandler(this.timerLoad_Tick);
            // 
            // timerPageAdjust
            // 
            this.timerPageAdjust.Enabled = true;
            this.timerPageAdjust.Tick += new System.EventHandler(this.timerPageAdjust_Tick);
            // 
            // labelReagentNow
            // 
            resources.ApplyResources(this.labelReagentNow, "labelReagentNow");
            this.labelReagentNow.BackColor = System.Drawing.Color.Transparent;
            this.labelReagentNow.Name = "labelReagentNow";
            // 
            // labelSupplyLeft2
            // 
            resources.ApplyResources(this.labelSupplyLeft2, "labelSupplyLeft2");
            this.labelSupplyLeft2.BackColor = System.Drawing.Color.Transparent;
            this.labelSupplyLeft2.Name = "labelSupplyLeft2";
            // 
            // labelSupplyLeft3
            // 
            resources.ApplyResources(this.labelSupplyLeft3, "labelSupplyLeft3");
            this.labelSupplyLeft3.BackColor = System.Drawing.Color.Transparent;
            this.labelSupplyLeft3.Name = "labelSupplyLeft3";
            // 
            // labelSupplyLeft4
            // 
            resources.ApplyResources(this.labelSupplyLeft4, "labelSupplyLeft4");
            this.labelSupplyLeft4.BackColor = System.Drawing.Color.Transparent;
            this.labelSupplyLeft4.Name = "labelSupplyLeft4";
            // 
            // timerSupplyAlert
            // 
            this.timerSupplyAlert.Enabled = true;
            this.timerSupplyAlert.Interval = 1000;
            this.timerSupplyAlert.Tick += new System.EventHandler(this.timerSupplyAlert_Tick);
            // 
            // serialPortME
            // 
            this.serialPortME.Parity = System.IO.Ports.Parity.Even;
            this.serialPortME.PortName = "COM3";
            this.serialPortME.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortME_DataReceived);
            // 
            // timerSampleReady
            // 
            this.timerSampleReady.Interval = 5000;
            this.timerSampleReady.Tick += new System.EventHandler(this.timerSampleReady_Tick);
            // 
            // timerMainPort
            // 
            this.timerMainPort.Enabled = true;
            this.timerMainPort.Interval = 500;
            this.timerMainPort.Tick += new System.EventHandler(this.timerMainPort_Tick);
            // 
            // serialPortFloatBall
            // 
            this.serialPortFloatBall.BaudRate = 38400;
            this.serialPortFloatBall.PortName = "COM3";
            this.serialPortFloatBall.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortFloatBall_DataReceived);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
            // 
            // labeluser
            // 
            resources.ApplyResources(this.labeluser, "labeluser");
            this.labeluser.BackColor = System.Drawing.Color.Transparent;
            this.labeluser.Name = "labeluser";
            // 
            // timerLiquidCheck
            // 
            this.timerLiquidCheck.Interval = 60000;
            this.timerLiquidCheck.Tick += new System.EventHandler(this.timerLiquidCheck_Tick);
            // 
            // ReaderS
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::i_Reader_S.Properties.Resources.Background;
            this.Controls.Add(this.labeluser);
            this.Controls.Add(this.labelSupplyLeft4);
            this.Controls.Add(this.labelSupplyLeft3);
            this.Controls.Add(this.labelSupplyLeft2);
            this.Controls.Add(this.labelReagentNow);
            this.Controls.Add(this.labelSupplyLeft);
            this.Controls.Add(this.labelTH);
            this.Controls.Add(this.labelMeachineStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelSystemTime);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelTopBack);
            this.Controls.Add(this.tabControlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "ReaderS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReaderS_FormClosing);
            this.Load += new System.EventHandler(this.i_Reader_S_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageLogin.ResumeLayout(false);
            this.tabPageLogin.PerformLayout();
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.panelLoding.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPageUserMode.ResumeLayout(false);
            this.tabPageUserMode.PerformLayout();
            this.tabPageDetail.ResumeLayout(false);
            this.tabPageDetail.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartFluo)).EndInit();
            this.tabPageHome.ResumeLayout(false);
            this.panelPic.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxResult)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tabControlMainRight.ResumeLayout(false);
            this.tabPageReagent.ResumeLayout(false);
            this.panelReagent5.ResumeLayout(false);
            this.panelReagent4.ResumeLayout(false);
            this.panelReagent1.ResumeLayout(false);
            this.panelReagent3.ResumeLayout(false);
            this.panelReagent2.ResumeLayout(false);
            this.tabPageSupply.ResumeLayout(false);
            this.panelWasteDisable.ResumeLayout(false);
            this.panelWasteDisable.PerformLayout();
            this.panelClean3.ResumeLayout(false);
            this.panelClean3.PerformLayout();
            this.panelDilution3.ResumeLayout(false);
            this.panelDilution3.PerformLayout();
            this.panelWasteReagent3.ResumeLayout(false);
            this.panelWasteReagent3.PerformLayout();
            this.panelWaste3.ResumeLayout(false);
            this.panelWaste3.PerformLayout();
            this.panelWasteReagent2.ResumeLayout(false);
            this.panelWasteReagent2.PerformLayout();
            this.panelWasteReagent1.ResumeLayout(false);
            this.panelWasteReagent1.PerformLayout();
            this.panelClean2.ResumeLayout(false);
            this.panelClean2.PerformLayout();
            this.panelClean1.ResumeLayout(false);
            this.panelClean1.PerformLayout();
            this.panelWaste2.ResumeLayout(false);
            this.panelWaste2.PerformLayout();
            this.panelWaste1.ResumeLayout(false);
            this.panelWaste1.PerformLayout();
            this.panelDilution2.ResumeLayout(false);
            this.panelDilution2.PerformLayout();
            this.panelDilution1.ResumeLayout(false);
            this.panelDilution1.PerformLayout();
            this.tabPageReagentOpen.ResumeLayout(false);
            this.tabPageQRAlert.ResumeLayout(false);
            this.tabPageSupplyFloatBall.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewMain)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPageQC.ResumeLayout(false);
            this.tabPageQC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQC2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQC1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageSearch.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSearch)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabPageSetting.ResumeLayout(false);
            this.tabControlSetting.ResumeLayout(false);
            this.tabPageGeneralSetting.ResumeLayout(false);
            this.tabPageGeneralSetting.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPageTestItemSetting.ResumeLayout(false);
            this.tabPageTestItemSetting.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTestItem)).EndInit();
            this.tabPageQCSetting.ResumeLayout(false);
            this.tabPageQCSetting.PerformLayout();
            this.panelQCPoint.ResumeLayout(false);
            this.panelQCPoint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewQCSetting)).EndInit();
            this.tabPageOtherSetting.ResumeLayout(false);
            this.tabPageOtherSetting.PerformLayout();
            this.tabPageUser.ResumeLayout(false);
            this.groupBoxUser.ResumeLayout(false);
            this.groupBoxUser.PerformLayout();
            this.panelAllUsers.ResumeLayout(false);
            this.paneluser.ResumeLayout(false);
            this.paneluser.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAllUsers)).EndInit();
            this.tabPageMessage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLog)).EndInit();
            this.tabPageStop.ResumeLayout(false);
            this.tabPageStop.PerformLayout();
            this.panelTopBack.ResumeLayout(false);
            this.panelTopBack.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonHome;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonMessage;
        private System.Windows.Forms.Button buttonSetting;
        private System.Windows.Forms.Button buttonQC;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageHome;
        private System.Windows.Forms.TabPage tabPageQC;
        private System.Windows.Forms.Panel panelTopBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonDoing;
        private System.Windows.Forms.Button buttonException;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Label labelMenu;
        private System.Windows.Forms.TabPage tabPageSearch;
        private System.Windows.Forms.TabPage tabPageSetting;
        private System.Windows.Forms.TabPage tabPageMessage;
        private System.Windows.Forms.TabPage tabPageStop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label labelOther;
        private System.Windows.Forms.Label labelTestitem;
        private System.Windows.Forms.Label labelSample;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.DataGridView dataGridViewMain;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button buttonSupply;
        private System.Windows.Forms.Button buttonReagent;
        private System.Windows.Forms.Panel panelReagent5;
        private System.Windows.Forms.Panel panelReagent4;
        private System.Windows.Forms.Panel panelReagent3;
        private System.Windows.Forms.Panel panelReagent2;
        private System.Windows.Forms.Panel panelReagent1;
        private System.Windows.Forms.Label labelSystemTime;
        private System.Windows.Forms.TabControl tabControlMainRight;
        private System.Windows.Forms.TabPage tabPageReagent;
        private System.Windows.Forms.TabPage tabPageSupply;
        private System.Windows.Forms.Panel panelWasteReagent2;
        private System.Windows.Forms.Panel panelClean2;
        private System.Windows.Forms.Panel panelWaste2;
        private System.Windows.Forms.Panel panelDilution2;
        private System.Windows.Forms.Panel panelWasteReagent3;
        private System.Windows.Forms.Label labelWasteReagent;
        private System.Windows.Forms.Panel panelWaste3;
        private System.Windows.Forms.Label labelWasteLiquid;
        private System.Windows.Forms.Panel panelClean3;
        private System.Windows.Forms.Label labelWashingLiquid;
        private System.Windows.Forms.Panel panelDilution3;
        private System.Windows.Forms.Label labelDilutionLiquid;
        private System.Windows.Forms.Button buttonClean1;
        private System.Windows.Forms.Button buttonWaste1;
        private System.Windows.Forms.Button buttonDilution1;
        private System.Windows.Forms.Button buttonClean2;
        private System.Windows.Forms.Button buttonDilution2;
        private System.Windows.Forms.Button buttonModifyLeft;
        private System.Windows.Forms.Button buttonModifyVolume;
        private System.Windows.Forms.Panel panelWasteReagent1;
        private System.Windows.Forms.Panel panelClean1;
        private System.Windows.Forms.Panel panelWaste1;
        private System.Windows.Forms.Panel panelDilution1;
        private System.Windows.Forms.Label labelExt5;
        private System.Windows.Forms.Label labelReagentLeft5;
        private System.Windows.Forms.Label labelLotNo5;
        private System.Windows.Forms.Label labelProduct5;
        private System.Windows.Forms.Label labelExt4;
        private System.Windows.Forms.Label labelReagentLeft4;
        private System.Windows.Forms.Label labelLotNo4;
        private System.Windows.Forms.Label labelProduct4;
        private System.Windows.Forms.Label labelExt1;
        private System.Windows.Forms.Label labelReagentLeft1;
        private System.Windows.Forms.Label labelLotNo1;
        private System.Windows.Forms.Label labelProduct1;
        private System.Windows.Forms.Label labelExt3;
        private System.Windows.Forms.Label labelReagentLeft3;
        private System.Windows.Forms.Label labelLotNo3;
        private System.Windows.Forms.Label labelProduct3;
        private System.Windows.Forms.Label labelExt2;
        private System.Windows.Forms.Label labelReagentLeft2;
        private System.Windows.Forms.Label labelLotNo2;
        private System.Windows.Forms.Label labelProduct2;
        private System.Windows.Forms.Timer timerMain;
        private System.Windows.Forms.DataGridView dataGridViewLog;
        private System.Windows.Forms.Label labelpage;
        private System.Windows.Forms.DataGridView dataGridViewSearch;
        private System.Windows.Forms.Button buttonStopConfirm;
        private System.Windows.Forms.Label labelStopStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelMeachineStatus;
        private System.Windows.Forms.TabPage tabPageLogin;
        private System.Windows.Forms.TextBox textBoxLoginPassword;
        private System.Windows.Forms.TextBox textBoxLoginName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelStep;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.Label labelNextTestItem;
        private System.Windows.Forms.Label labelNextSampleNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerPageUp;
        private System.Windows.Forms.Timer timerPageDown;
        private System.Windows.Forms.Button buttonSearchSample;
        private System.Windows.Forms.TextBox textBoxSearchSample;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonSearchDetail;
        private System.Windows.Forms.TextBox textBoxSearchStartDate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxSearchTestitem;
        private System.Windows.Forms.Button buttonSearchSampleLike;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button buttonAutoPrint;
        private System.Windows.Forms.Button buttonAddQCPoint;
        private System.Windows.Forms.Button buttonStopRecovery;
        private System.Windows.Forms.Button buttonWaste2;
        private System.Windows.Forms.TabControl tabControlSetting;
        private System.Windows.Forms.TabPage tabPageGeneralSetting;
        private System.Windows.Forms.TabPage tabPageTestItemSetting;
        private System.Windows.Forms.TabPage tabPageQCSetting;
        private System.Windows.Forms.TabPage tabPageOtherSetting;
        private System.Windows.Forms.DataGridView dataGridViewQCSetting;
        private System.Windows.Forms.Panel panelQCPoint;
        private System.Windows.Forms.Button buttonFixQCData;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxQCSD;
        private System.Windows.Forms.TextBox textBoxQCTarget;
        private System.Windows.Forms.TextBox textBoxQCSampleNo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label labelQCInfo;
        private System.Windows.Forms.Button buttonWasteMode;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBoxStartSample;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelDriveLeft;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridViewTestItem;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox textBoxSettingAccurancy;
        private System.Windows.Forms.TextBox textBoxSettingWarning;
        private System.Windows.Forms.TextBox textBoxSettingRatio;
        private System.Windows.Forms.TextBox textBoxSettingUnit;
        private System.Windows.Forms.TextBox textBoxSettingTestItemName;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button buttonAddTestItem;
        private System.Windows.Forms.Button buttonFixTestItem;
        private System.Windows.Forms.TextBox textBoxSettingUnitRatio;
        private System.Windows.Forms.TextBox textBoxSettingUnitDefault;
        private System.Windows.Forms.Panel panelOther;
        private System.Windows.Forms.TextBox textBoxPage;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Button buttonShowPassword;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.TextBox textBoxSearchEndDate;
        private System.Windows.Forms.Label labelDilution1;
        private System.Windows.Forms.Label labelWasteReagent1;
        private System.Windows.Forms.Label labelClean1;
        private System.Windows.Forms.Label labelWaste1;
        private System.Windows.Forms.Label labelWasteReagent2;
        private System.Windows.Forms.Label labelClean2;
        private System.Windows.Forms.Label labelWaste2;
        private System.Windows.Forms.Label labelDilution2;
        private System.Windows.Forms.Panel panelWasteDisable;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Button buttonCRPDebug;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TabPage tabPageDetail;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.IO.Ports.SerialPort serialPortMain;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button buttonSaveLog;
        private System.IO.Ports.SerialPort serialPortQR;
        private System.IO.Ports.SerialPort serialPortTH;
        private System.IO.Ports.SerialPort serialPortFluoMotor;
        private System.IO.Ports.SerialPort serialPortFluo;
        private System.Windows.Forms.Button buttonPowerOff;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartFluo;
        private System.Windows.Forms.Button buttonLigthFix;
        private System.Windows.Forms.Button buttonMidFix;
        private System.Windows.Forms.Button buttonDilutionMode;
        private System.Windows.Forms.Button buttonMaintain;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.Button buttonMinWindow;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button buttonCMDSend;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox textBoxCMD;
        private System.Windows.Forms.TextBox textBoxSleepTime;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label labelTH;
        private System.Windows.Forms.Label labelSearchCostTime;
        private System.Windows.Forms.Button buttonFluoFix;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxFluoRef;
        private System.Windows.Forms.TabPage tabPageUserMode;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox textBoxCCDRef;
        private System.Windows.Forms.Panel panelLoding;
        private System.Windows.Forms.Panel panelLoding5;
        private System.Windows.Forms.Panel panelLoding4;
        private System.Windows.Forms.Panel panelLoding3;
        private System.Windows.Forms.Panel panelLoding2;
        private System.Windows.Forms.Panel panelLoding1;
        private System.Windows.Forms.Timer timerLoding;
        private System.Windows.Forms.Label labelCountDown;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label labelSupplyLeft;
        private System.Windows.Forms.TabPage tabPageReagentOpen;
        private System.Windows.Forms.TabPage tabPageQRAlert;
        private System.Windows.Forms.Label labelQR;
        private System.Windows.Forms.Label labelReagentOperation;
        private System.Windows.Forms.Button buttonQRAlertOK;
        private System.Windows.Forms.Button buttonSetFirst;
        private System.Windows.Forms.Label labelReagentInfo;
        private System.Windows.Forms.Label labelQRAlert;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label labelQR2;
        private System.Windows.Forms.TextBox textBoxMain;
        private System.Windows.Forms.TextBox textBoxQR;
        private System.Windows.Forms.TextBox textBoxTH;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Label labelExceptionNo;
        private System.Windows.Forms.Label labelCleanStatus;
        private System.Windows.Forms.Label labelDilutionStatus;
        private System.Windows.Forms.Label labelReagentStatus;
        private System.Windows.Forms.Label labelWasteStatus;
        private System.Windows.Forms.Button buttonUserTest;
        private System.Windows.Forms.Button buttonUserClean1;
        private System.Windows.Forms.Button buttonUserClean2;
        private System.Windows.Forms.Button buttonUserLiquidCheck;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonReagentSensor;
        private System.Windows.Forms.Button buttonLiquidSensor;
        private System.Windows.Forms.Timer timerLoad;
        private System.Windows.Forms.Label labelLoding;
        private System.Windows.Forms.Timer timerPageAdjust;
        private System.Windows.Forms.Button buttonCCDTest;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer1;
        private System.Windows.Forms.Label labelIDRead;
        private System.Windows.Forms.Button buttonAutoQC;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label labelReagentNow;
        private System.Windows.Forms.Button buttonStartTH;
        private System.Windows.Forms.Button buttonGetIDCode;
        private System.Windows.Forms.Label labelSupplyLeft2;
        private System.Windows.Forms.Label labelSupplyLeft3;
        private System.Windows.Forms.Label labelSupplyLeft4;
        private System.Windows.Forms.Timer timerSupplyAlert;
        private System.IO.Ports.SerialPort serialPortME;
        private System.Windows.Forms.Timer timerSampleReady;
        private System.Windows.Forms.TextBox textBoxASU;
        private System.Windows.Forms.ToolStripMenuItem 修改压积ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridViewQC2;
        private System.Windows.Forms.DataGridView dataGridViewQC1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column19;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column17;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column18;
        private System.Windows.Forms.Timer timerMainPort;
        private System.Windows.Forms.Button buttonFluoTest;
        private System.Windows.Forms.TextBox textBoxPreDilu;
        private System.Windows.Forms.Label labelPreDilu;
        private System.IO.Ports.SerialPort serialPortFloatBall;
        private System.Windows.Forms.TabPage tabPageSupplyFloatBall;
        private System.Windows.Forms.Label labelFloatBallDilution;
        private System.Windows.Forms.Label labelFloatBallWasteReagent;
        private System.Windows.Forms.Label labelFloatBallWaste;
        private System.Windows.Forms.Label labelFloatBallClean;
        private System.Windows.Forms.Label labelLock;
        private System.Windows.Forms.Label labelUserStep;
        private System.Windows.Forms.ToolStripMenuItem 发送结果ToolStripMenuItem;
        private System.Windows.Forms.Panel panelPic;
        private System.Windows.Forms.Button buttonPicClose;
        private System.Windows.Forms.PictureBox pictureBoxResult;
        private System.Windows.Forms.Label labelSampleCount;
        private System.Windows.Forms.Button buttonCheckAllUsers;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.Label labeluser;
        private System.Windows.Forms.TextBox textBoxOpenState;
        private System.Windows.Forms.Timer timerLiquidCheck;
        private System.Windows.Forms.TabPage tabPageUser;
        private System.Windows.Forms.GroupBox groupBoxUser;
        private System.Windows.Forms.Button buttonUserDelete;
        private System.Windows.Forms.Button buttonUserChange;
        private System.Windows.Forms.ComboBox comboBoxUserType;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox textBoxRepeatPassword;
        private System.Windows.Forms.TextBox textBoxNewPassword;
        private System.Windows.Forms.TextBox textBoxNewUserName;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Panel panelAllUsers;
        private System.Windows.Forms.Panel paneluser;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.DataGridView dataGridViewAllUsers;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxTime;
    }
}

