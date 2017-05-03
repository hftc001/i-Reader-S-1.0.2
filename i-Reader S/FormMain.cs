using AForge.Imaging.Filters;
using AForge.Video.DirectShow;
using i_Reader_S.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data;

namespace i_Reader_S
{
    public partial class ReaderS : Form
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;

        //工程师模式
        public int engineerMode = 0;
        //用于触摸键盘传值
        public static string CounterText = "";

        //是否灰度片测试
        public static int LocationCcd = 0;

        //用于切换测试项目
        public static string TestItemText = "";
        //用于确定弹窗形式
        public static string [] MessageType = { "", "" };

        //荧光的数据需要8个数一组自行读取，为间断式数据，用于存储荧光数据
        private readonly List<double> _fluoData = new List<double>();

        //主控、二维码、温湿度、荧光数据、荧光电机、浮球串口
        private readonly string[] _comStr = { "", "", "", "-1", "", "" };

        //荧光测试的seq，打印信息，CCDSeq
        private readonly string[] _otherStr = { "", "", ""};

        //当前仪器测试参数 检测头，片仓,反应时间
        private int[] _MParam = { 0, 1, 300 };
        

        //警告弹窗
        private FormAlert _myAlert;
        private FormMessageBox _myMsab;
        //虚拟键盘操作,当虚拟键盘打开时扫描到条码信号时关闭虚拟键盘
        private KeyboardforIrd _mykeyboard = new KeyboardforIrd();

        //资源文件 根据信息编号读取信息内容
        private ResourceManager _rm;

        //searchcondition 查询列表条件
        private string _searchcondition = string.Format(" and createtime between '{0:yyyy-MM-dd 00:00:00}' and '{1:yyyy-MM-dd 23:59:59}' ", DateTime.Today.AddDays(-7), DateTime.Now);

        //barcodemode,CCD测试次数,状态名称,混匀模式，打印模式，自动测试,是否光源校准,是否中心校准,是否锁定弹窗
        private readonly int[] _otherInt = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        
        //用户权限
        public string UserType = "";//2017-2-24

        //ASU测试是否已经完成的判断变量//2017-3-22
        public bool ASUComplete = true;
        
        //触摸屏美观要求，需要隐藏鼠标
        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]

        
        public static extern void ShowCursor(int status);

        public ReaderS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 更新配置文件函数
        /// </summary>
        /// <param name="newKey">要修改或者插入的配置项</param>
        /// <param name="newValue">配置值</param>
        private static void UpdateAppConfig(string newKey, string newValue)
        {
            var isModified = ConfigurationManager.AppSettings.Cast<string>().Any(key => key == newKey);
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (isModified)
            {
                config.AppSettings.Settings.Remove(newKey);
            }
            config.AppSettings.Settings.Add(newKey, newValue);
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");

            if (File.Exists(Application.StartupPath + "\\configbackup\\i-Reader S.exe.config"))
            {
                File.Delete(Application.StartupPath + "\\configbackup\\i-Reader S.exe.config");
            }
            File.Copy(Application.StartupPath + "\\i-Reader S.exe.config", Application.StartupPath + "\\configbackup\\i-Reader S.exe.config");

        }

        /// <summary>
        /// 将获得的二维码进行提取转换
        /// </summary>
        /// <param name="str">定标信息二维码</param>
        private void Base64Decode(string str)
        {
            try
            {
                //将二维码字符串转为可用的byte数组
                var outputb = Convert.FromBase64String(str);
                //第一个字节保留
                //第二个字节的前5位表示0-31的产品编号
                var a = Convert.ToString(outputb[1], 2).PadLeft(8, '0') + Convert.ToString(outputb[2], 2).PadLeft(8, '0');
                var cpmc = Convert.ToInt32(a.Substring(0, 5), 2).ToString();
                //第三字节的第2到第5位表示0-15*3月的保质期
                var bzs = Convert.ToInt32(a.Substring(9, 4), 2).ToString();
                //第二字节的第6-8位表示公式类型0-7
                var funtype = Convert.ToInt32(a.Substring(13), 2).ToString();
                //第四第五字节表示批号新系
                var lot = (Convert.ToInt32(outputb[3]) * 256 + Convert.ToInt32(outputb[4])).ToString();
                a = Convert.ToString(outputb[5], 2).PadLeft(8, '0') + Convert.ToString(outputb[6], 2).PadLeft(8, '0');
                //第6第7字节表示生产日期，前7位为年份+2000，接着4位为月份，最后5位为日
                var year = Convert.ToInt32(a.Substring(0, 7), 2);
                var month = Convert.ToInt32(a.Substring(7, 4), 2);
                var day = Convert.ToInt32(a.Substring(11), 2);
                //第8字节开始后的56个字节，没8个字节对应一个double数字，共7个参数
                var param = new double[7];
                for (var i = 0; i < 7; i++)
                {
                    var aa = new byte[8];
                    Array.Copy(outputb, 7 + i * 8, aa, 0, 8);
                    param[i] = BitConverter.ToDouble(aa, 0);
                }

                var dtPdate = DateTime.Parse(string.Format("{0}-{1}-{2}", year, month, day));
                var epdate = dtPdate.AddMonths(3 * int.Parse(bzs));
                //2016年1月11之前规则：+15月的前一月最后一天。  2016年1月11号改为+15月-1天
                epdate = dtPdate < DateTime.Parse("2016-01-11 00:00:00") ? epdate.AddDays(-epdate.Day) : epdate.AddDays(-1);
                //将定标信息插入到定标数据表中
                var calibdataid = SqlData.InsertIntoCalibdata(cpmc, lot, dtPdate, epdate, funtype, param);
                //
                var strCalibdataid = calibdataid == 0
                    ? "(select max(CalibDataID)  from CalibData)"
                    : calibdataid.ToString();
                if (calibdataid > -1)
                {
                    var labelQrText = Resources.labelQRText;
                    labelQrText = labelQrText?.Replace("[m]", lot);
                    var productName = SqlData.SelectTestItemNameById(cpmc);
                    labelQrText = labelQrText?.Replace("[n]", productName);
                    var reagentStoreId = "";
                    Invoke(new Action(() =>
                    {
                        labelQR.Text = labelQrText;
                        labelQR2.Text = labelQrText;
                        reagentStoreId = labelReagentOperation.Text.Substring(labelReagentOperation.Text.IndexOf("#", StringComparison.Ordinal) + 1, 1);
                    }));
                    SqlData.UpdateReagentCalibData(strCalibdataid, reagentStoreId);
                    UpdateReagentStore();
                }
                else
                    Invoke(new Action(() => labelQR.Text = Resources.QRError));
            }
            catch (Exception)
            {
                Invoke(new Action(() => labelQR.Text = Resources.QRError));
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = ((Button)sender);
                if (btn == buttonAddQCPoint)
                {
                    if (textBoxQCSD.Text == "" | textBoxQCTarget.Text == "")
                    {
                        ShowMyAlert("值不能为空"); return;
                    }
                    var i = 0;
                    var productname = "";
                    while (i > -1)
                    {
                        var btn1 = (Button)Controls.Find("buttonQC" + i, true)[0];
                        if (btn1.BackColor == Color.FromArgb(210, 210, 210))
                        {
                            productname = btn1.Text;
                            break;
                        }
                        i++;
                    }
                    try
                    {
                        SqlData.InsertQcSetting(SqlData.SelectProductIdByname(productname).Rows[0][0].ToString(),
                            textBoxQCSampleNo.Text,
                            textBoxQCTarget.Text, textBoxQCSD.Text);
                        dataGridViewQCSetting.DataSource = SqlData.SelectQcSetting(productname);
                        dataGridViewQCSetting.Visible = true;
                    }
                    catch (Exception)
                    {
                        ShowMyAlert("QC样本号不可重复");
                    }
                }
                else if (btn == buttonAddTestItem)
                {
                    CounterText = "AddNewItem";
                }
                else if (btn == buttonAutoPrint)
                {
                    var autoPrint = ConfigRead("AutoPrint");
                    UpdateAppConfig("AutoPrint", (1 - int.Parse(autoPrint)).ToString());
                    buttonAutoPrint.BackgroundImage = ConfigRead("AutoPrint") == "0"
                        ? Resources.switch_off
                        : Resources.switch_on;
                }
                else if (btn == buttonCMDSend)
                {
                    if (comboBox2.Text.IndexOf("#41", StringComparison.Ordinal) == -1)
                    {
                        var aaa = "";
                        if (comboBox2.Text.IndexOf(":") == -1)
                            aaa = comboBox2.Text;
                        else
                            aaa = comboBox2.Text.Substring(0, comboBox2.Text.IndexOf(":", StringComparison.Ordinal));
                        var str3 = aaa + (textBoxCMD.Text == @"$" ? "" : textBoxCMD.Text);
                        serialPort_DataSend(serialPortMain, str3);
                    }
                    else
                    {
                        FluoCmd(comboBox2.Text + textBoxCMD.Text);
                    }
                }
                else if (btn == buttonDilutionMode)
                {
                    _otherInt[3] = 1 - _otherInt[3];
                    serialPort_DataSend(serialPortMain, "#3020$" + _otherInt[3]);
                }
                else if (btn == buttonFixQCData)
                {
                    var rowindex = dataGridViewQCSetting.CurrentCell.RowIndex;
                    try
                    {
                        SqlData.UpdateQcSetting(dataGridViewQCSetting[0, rowindex].Value.ToString(),
                            dataGridViewQCSetting[1, rowindex].Value.ToString(), textBoxQCSampleNo.Text,
                            textBoxQCTarget.Text,
                            textBoxQCSD.Text);
                        dataGridViewQCSetting.DataSource =
                            SqlData.SelectQcSetting(dataGridViewQCSetting[4, rowindex].Value.ToString());
                    }
                    catch (Exception)
                    {
                        ShowMyAlert("QC样本号不可重复");
                    }
                }
                else if (btn == buttonFixTestItem)
                {
                    buttonFixTestItem.Enabled = false;
                    var rowindex1 = dataGridViewTestItem.CurrentCell.RowIndex;

                    SqlData.UpdateTestIteminfo(textBoxSettingTestItemName.Text, textBoxSettingUnit.Text,
                        textBoxSettingUnitRatio.Text,
                        textBoxSettingRatio.Text, textBoxSettingAccurancy.Text,
                        dataGridViewTestItem[0, rowindex1].Value.ToString(),
                        dataGridViewTestItem[1, rowindex1].Value.ToString());

                    if (labelNextTestItem.Text == textBoxSettingTestItemName.Text)
                        labelNextTestItem.Text = textBoxSettingTestItemName.Text;
                    if (textBoxSettingTestItemName.Text == "CRPDIL")
                    {
                        UpdateAppConfig("PreDilu", textBoxPreDilu.Text);
                    }
                    textBoxSettingTestItemName.Clear();
                    textBoxSettingUnit.Clear();
                    textBoxSettingUnitDefault.Clear();
                    textBoxSettingUnitRatio.Clear();
                    textBoxSettingRatio.Clear();
                    textBoxSettingWarning.Clear();
                    textBoxSettingAccurancy.Clear();
                    textBoxPreDilu.Clear();
                    buttonSubMenu_Click(button2, null);
                }
                else if (btn == buttonFluoFix)
                {
                    buttonFluoFix.BackColor = Color.LightGray;
                    _fluoData.Clear();
                    FluoNewTest("0");
                }
                else if (btn == buttonCRPDebug)
                {
                    var CRPDebug = int.Parse(ConfigRead("CRPDebug"));
                    UpdateAppConfig("CRPDebug", (1 - CRPDebug).ToString());
                    buttonCRPDebug.BackgroundImage = ConfigRead("CRPDebug") == "0"
                        ? Resources.switch_off
                        : Resources.switch_on;
                }
                else if (btn == buttonLigthFix)
                {
                    if (_otherInt[2] != 4)
                    {
                        MessageBox.Show(@"需进入维护状态");
                        return;
                    }
                    LocationCcd = -1;
                    _otherInt[6] = -1;
                    serialPort_DataSend(serialPortMain, "#4011$0$600$500");
                }
                else if (btn == buttonLogin)
                {
                    var userTable = SqlData.DbCheckUserinfo(textBoxLoginName.Text, textBoxLoginPassword.Text);
                    if (userTable.Rows.Count == 0)
                    {
                        // ReSharper disable once ResourceItemNotResolved
                        Log_Add(Resources.LoginError, true);
                        textBoxLoginPassword.Text = "";
                        textBoxLoginName.Text = "";
                    }
                    else
                    {
                        UserType = SqlData.SelectOneUserName(textBoxLoginName.Text.ToString()).Rows[0][2].ToString();
                        labeluser.Text = "操作员：" + SqlData.SelectOneUserName(textBoxLoginName.Text.ToString()).Rows[0][0].ToString();
                        panelLogin.Visible = false;
                        labelStep.Visible = true;
                        textBoxOpenState.Visible = true;//2017-2-27
                        // ReSharper disable once ResourceItemNotResolved
                        var msg = Resources.CC06;
                        if (msg != null)
                        {
                            msg = msg.Replace("[1]", textBoxLoginName.Text);
                            Invoke(new Action(() => Log_Add(msg, false)));
                        }
                        // ReSharper disable once ResourceItemNotResolved
                        labelStep.Text = Resources.LoadingStep1;
                        labelStep.Location = new Point(1024 / 2 - labelStep.Size.Width / 2, labelStep.Location.Y);
                        labelMenu.Text = "";
                        timerMain.Enabled = true;
                        timerLoding.Start();
                    }
                }
                else if (btn == buttonMaintain)
                {
                    SwitchWorkState(_otherInt[2], 4);
                }
                else if (btn == buttonMidFix)
                {
                    if (_otherInt[2] != 4)
                    {
                        MessageBox.Show(@"需进入维护状态");
                        return;
                    }
                    //_otherInt[7] = 1;
                    if (LocationCcd == -1)
                        LocationCcd = 0;
                    var str = CalTy("0");
                    var tx = str.Substring(str.IndexOf("T(", StringComparison.Ordinal) + 2);
                    var midY = str.Substring(str.IndexOf("nstartY", StringComparison.Ordinal) + 8);
                    var midy = int.Parse(midY.Substring(0, midY.IndexOf(")", StringComparison.Ordinal))) - 428;

                    //var Rdiff = str.Substring(str.IndexOf("Rdiff", StringComparison.Ordinal) + 6);
                    //var Turn = Rdiff.Substring(0, Rdiff.IndexOf(")", StringComparison.Ordinal));

                    //Log_Add(string.Format(@"中心位置：（{0}，{1}）旋转：{2}", int.Parse(tx.Substring(0, tx.IndexOf(",", StringComparison.Ordinal))) - 638, midy, Turn), false);
                    Log_Add(string.Format(@"中心偏移量为：（{0},{1}）", int.Parse(tx.Substring(0, tx.IndexOf(",", StringComparison.Ordinal))) - 638, midy), false);
                    Log_Add(str, false);
                    //labelResult.Text = string.Format(@"中心位置：（{0}，{1}）旋转：{2}", int.Parse(tx.Substring(0, tx.IndexOf(",", StringComparison.Ordinal))) - 638, midy, Turn);
                    labelResult.Text = string.Format(@"中心偏移量为：（{0},{1}）", int.Parse(tx.Substring(0, tx.IndexOf(",", StringComparison.Ordinal))) - 638, midy);
                    //_otherInt[7] = 0;
                }
                else if (btn == buttonMinWindow)
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (btn == buttonModifyLeft)
                {
                    if (buttonModifyLeft.Text == @"更改存余")
                    {
                        buttonModifyLeft.Text = @"确定";
                        buttonDilution1.Text = @"+10%";
                        buttonDilution2.Text = @"-10%";
                        buttonClean1.Text = @"+10%";
                        buttonClean2.Text = @"-10%";
                        buttonWaste1.Text = @"+10%";
                        buttonWaste2.Text = @"-10%";
                        buttonWaste2.Visible = true;
                        buttonModifyVolume.Visible = false;
                    }
                    else
                    {
                        buttonModifyVolume.Visible = true;
                        buttonModifyLeft.Text = @"更改存余";
                        buttonDilution1.Text = @"加满";
                        buttonDilution2.Text = @"灌注";
                        buttonClean1.Text = @"加满";
                        buttonClean2.Text = @"灌注";
                        buttonWaste1.Text = @"清空";
                        buttonWaste2.Text = @"";

                        var supplyVolume = ConfigRead("SupplyVolume").Split('-');

                        var dilutionLeft = labelDilution1.Text.Substring(0, labelDilution1.Text.Length - 1);
                        dilutionLeft = (int.Parse(dilutionLeft) * int.Parse(supplyVolume[0]) / 100).ToString();

                        var cleanLeft = labelClean1.Text.Substring(0, labelClean1.Text.Length - 1);
                        cleanLeft = (int.Parse(cleanLeft) * int.Parse(supplyVolume[1]) / 100).ToString();

                        var wasteLeft = labelWaste1.Text.Substring(0, labelWaste1.Text.Length - 1);
                        wasteLeft = (int.Parse(wasteLeft) * int.Parse(supplyVolume[2]) / 100).ToString();

                        var supplyLeft = ConfigRead("SupplyLeft").Split('-');
                        UpdateAppConfig("SupplyLeft",
                            string.Format("{0}-{1}-{2}-{3}", dilutionLeft, cleanLeft, wasteLeft, supplyLeft[3]));

                        UpdateSupplyUi();
                    }
                }
                else if (btn == buttonModifyVolume)
                {
                    if (buttonModifyVolume.Text == @"更改容量")
                    {
                        buttonModifyVolume.Text = @"确定";
                        buttonDilution1.Text = @"+1L";
                        buttonDilution2.Text = @"-1L";
                        buttonClean1.Text = @"+1L";
                        buttonClean2.Text = @"-1L";
                        buttonWaste1.Text = @"+1L";
                        buttonWaste2.Text = @"-1L";
                        var supplyVolume = ConfigRead("SupplyVolume").Split('-');
                        labelDilution1.Text = (int.Parse(supplyVolume[0]) / 1000) + @"L";
                        labelDilution2.Text = (int.Parse(supplyVolume[0]) / 1000) + @"L";

                        labelClean1.Text = (int.Parse(supplyVolume[1]) / 1000) + @"L";
                        labelClean2.Text = (int.Parse(supplyVolume[1]) / 1000) + @"L";

                        labelWaste1.Text = (int.Parse(supplyVolume[2]) / 1000) + @"L";
                        labelWaste2.Text = (int.Parse(supplyVolume[2]) / 1000) + @"L";

                        panelWasteReagent3.Visible = false;
                        panelWasteReagent2.Visible = false;
                        panelWasteReagent1.Visible = false;
                        buttonModifyLeft.Visible = false;
                        buttonWaste2.Visible = true;


                        panelDilution1.Visible = false;
                        panelDilution2.Visible = false;
                        panelDilution3.Visible = false;
                        panelClean1.Visible = false;
                        panelClean2.Visible = false;
                        panelClean3.Visible = false;
                    }
                    else
                    {
                        buttonModifyVolume.Text = @"更改容量";
                        buttonDilution1.Text = @"加满";
                        buttonDilution2.Text = @"灌注";
                        buttonClean1.Text = @"加满";
                        buttonClean2.Text = @"灌注";
                        buttonWaste1.Text = @"清空";
                        buttonWaste2.Text = @"";

                        var dilutionVolume = labelDilution1.Text.Substring(0, labelDilution1.Text.Length - 1);
                        dilutionVolume = (int.Parse(dilutionVolume) * 1000).ToString();

                        var cleanVolume = labelClean1.Text.Substring(0, labelClean1.Text.Length - 1);
                        cleanVolume = (int.Parse(cleanVolume) * 1000).ToString();

                        var wasteVolume = labelWaste1.Text.Substring(0, labelWaste1.Text.Length - 1);
                        wasteVolume = (int.Parse(wasteVolume) * 1000).ToString();

                        var supplyVolume = ConfigRead("SupplyVolume").Split('-');

                        UpdateAppConfig("SupplyVolume",
                            string.Format("{0}-{1}-{2}-{3}", dilutionVolume, cleanVolume, wasteVolume, supplyVolume[3]));

                        panelWasteReagent3.Visible = true;
                        panelWasteReagent2.Visible = true;
                        panelWasteReagent1.Visible = true;

                        panelDilution1.Visible = true;
                        panelDilution2.Visible = true;
                        panelDilution3.Visible = true;
                        panelClean1.Visible = true;
                        panelClean2.Visible = true;
                        panelClean3.Visible = true;

                        UpdateSupplyUi();
                        buttonModifyLeft.Visible = true;
                    }
                }
                else if (btn == buttonPowerOff)
                {
                    button_Click(buttonSaveLog, null);
                    if (MessageBox.Show(@"请点击确定后关机，待屏幕熄灭后请关闭电源开关", @"关机确认?", MessageBoxButtons.OKCancel) ==
                        DialogResult.OK)
                    {
                        byte[] th = Encoding.ASCII.GetBytes("\x02" + "PumpWork" + "\x03");
                        serialPortTH.Write(th, 0, th.Length);
                        Invoke(new Action(() => { PortLog("TH", "S", "<STX>PumpWork<ETX>"); MessageboxShow("排水中|排水完毕将自动关机,屏幕熄灭后请切断电源"); }));
                        this.Enabled = false;
                    }
                }
                else if (btn == buttonQRAlertOK)
                {
                    // ReSharper disable once ResourceItemNotResolved
                    var msgLog = Resources.CC03;
                    if (msgLog != null)
                    {
                        msgLog = msgLog.Replace("[n]",
                            labelReagentOperation.Text.Substring(
                                labelReagentOperation.Text.IndexOf("#", 1, StringComparison.Ordinal) + 1, 1));
                        Invoke(new Action(() => Log_Add("QR03" + msgLog, false, Color.Red)));
                        tabControlMainRight.SelectedTab = tabPageReagent;
                        ReagentStatus = 0;
                    }
                }
                else if (btn == buttonSaveLog)
                {
                    LogSave("Run" + DateTime.Now.ToString("yyyyMMdd") + ".txt", textBoxLog.Text);
                    LogSave("Main" + DateTime.Now.ToString("yyyyMMdd") + ".txt", textBoxMain.Text);
                    LogSave("QR" + DateTime.Now.ToString("yyyyMMdd") + ".txt", textBoxQR.Text);
                    LogSave("TH" + DateTime.Now.ToString("yyyyMMdd") + ".txt", textBoxTH.Text);
                    if (ConfigRead("ASUEnable") == "1")
                        LogSave("ASU" + DateTime.Now.ToString("yyyyMMdd") + ".txt", textBoxASU.Text);

                    var strlog = new StringBuilder();
                    for (int i = 0; i < dataGridViewLog.RowCount; i++)
                    {
                        strlog.Append(dataGridViewLog[0, i].Value.ToString().PadRight(15));
                        strlog.Append(dataGridViewLog[1, i].Value.ToString());
                        strlog.Append("\r\n");
                    }

                    LogSave("Log" + DateTime.Now.ToString("yyyyMMdd") + ".txt", strlog.ToString());

                    textBoxLog.Text = "";
                    textBoxTH.Text = "";
                    textBoxQR.Text = "";
                    textBoxASU.Text = "";
                    textBoxMain.Text = "";
                    dataGridViewLog.Rows.Clear();

                }
                else if (btn == buttonSearchDetail)
                {
                    var time1 = textBoxSearchStartDate.Text;
                    var time2 = textBoxSearchEndDate.Text;
                    var createtime1 = time1 + " 00:00:00:000";
                    var createtime2 = time2 + " 23:59:59:999";
                    createtime1 = createtime1.Replace('/', '-');
                    createtime2 = createtime2.Replace('/', '-');
                    if (textBoxSearchTestitem.Text == @"All" | textBoxSearchTestitem.Text == "")
                        _searchcondition = string.Format(" and createtime between '{0}' and '{1}' ", createtime1, createtime2);
                    else
                        _searchcondition = string.Format(" and createtime between '{0}' and '{1}' and testitemname = '{2}' ", createtime1, createtime2, textBoxSearchTestitem.Text);
                    Invoke(new Action(() => UpdatedataGridViewSearch("1", "12")));
                    var resultcount = int.Parse(SqlData.SelectResultCount(_searchcondition).Rows[0][0].ToString());
                    labelSampleCount.Text = "测试数量：  " + resultcount;//2017-2-24
                    textBoxPage.Text = @"1";
                    labelpage.Text = @"/" + (resultcount / 12 + (resultcount % 12 == 0 ? 0 : 1));
                }
                else if (btn == buttonSearchSample)
                {
                    _searchcondition = string.Format(" and sampleno = '{0}' ", textBoxSearchSample.Text);
                    Invoke(new Action(() => UpdatedataGridViewSearch("1", "12")));
                    var resultcount1 = int.Parse(SqlData.SelectResultCount(_searchcondition).Rows[0][0].ToString());
                    labelSampleCount.Text = "测试数量：  " + resultcount1;//2017-2-24
                    labelpage.Text = @"/" + (resultcount1 / 12 + (resultcount1 % 12 == 0 ? 0 : 1));
                }
                else if (btn == buttonSearchSampleLike)
                {
                    _searchcondition = string.Format(" and sampleno like '%{0}%' ", textBoxSearchSample.Text);
                    Invoke(new Action(() => UpdatedataGridViewSearch("1", "12")));
                    var resultcount2 = int.Parse(SqlData.SelectResultCount(_searchcondition).Rows[0][0].ToString());
                    labelSampleCount.Text = "测试数量：  " + resultcount2;//2017-2-24
                    labelpage.Text = @"/" + (resultcount2 / 12 + (resultcount2 % 12 == 0 ? 0 : 1));
                    textBoxPage.Text = @"1";
                }
                else if (btn == buttonSetFirst)
                {
                    var str2 = labelReagentOperation.Text;
                    str2 = str2.Substring(str2.IndexOf("#", StringComparison.Ordinal) + 1, 1);
                    serialPort_DataSend(serialPortMain, "#3002$" + str2);
                }
                else if (btn == buttonStopConfirm)
                {
                    serialPort_DataSend(serialPortMain, "#0005$1");
                }
                else if (btn == buttonStopRecovery)
                {
                    serialPort_DataSend(serialPortMain, "#0005$0");
                }
                else if (btn == buttonTest)
                {
                    SwitchWorkState(_otherInt[2], 3);
                }
                else if (btn == buttonWasteMode)
                {
                    var wasteMode = int.Parse(ConfigRead("WasteMode"));
                    UpdateAppConfig("WasteMode", (1 - wasteMode).ToString());
                    UpdateSupplyUi();
                }
                else if (btn == buttonDilution1)
                {
                    if (buttonDilution1.Text == @"加满")
                    {
                        var supplyVolume = ConfigRead("SupplyVolume").Split('-');
                        var supplyLeft = ConfigRead("SupplyLeft").Split('-');
                        UpdateAppConfig("SupplyLeft",
                            string.Format("{0}-{1}-{2}-{3}", supplyVolume[0], supplyLeft[1], supplyLeft[2], supplyLeft[3]));
                        UpdateSupplyUi();
                    }
                    else if (buttonDilution1.Text == @"+1L")
                    {
                        labelDilution1.Text =
                            (int.Parse(labelDilution1.Text.Substring(0, labelDilution1.Text.Length - 1)) + 1) + @"L";
                        labelDilution2.Text = labelDilution1.Text;
                    }
                    else if (buttonDilution1.Text == @"+10%")
                    {
                        var percent = int.Parse(labelDilution1.Text.Substring(0, labelDilution1.Text.Length - 1));
                        percent = percent % 10 == 0 ? percent + 10 : percent / 10 * 10 + 10;
                        percent = Math.Min(100, percent);
                        labelDilution1.Text = percent + @"%";
                        labelDilution2.Text = labelDilution1.Text;
                    }
                }
                else if (btn == buttonDilution2)
                {
                    if (buttonDilution2.Text == @"灌注")
                    {
                        serialPort_DataSend(serialPortMain, "#3060");
                        UpdateSupplyLeft(2, 0);
                    }
                    else if (buttonDilution2.Text == @"-1L")
                    {
                        var volume = int.Parse(labelDilution1.Text.Substring(0, labelDilution1.Text.Length - 1));
                        volume--;
                        volume = Math.Max(0, volume);
                        labelDilution1.Text = volume + @"L";
                        labelDilution2.Text = labelDilution1.Text;
                    }
                    else if (buttonDilution2.Text == @"-10%")
                    {
                        var percent = int.Parse(labelDilution1.Text.Substring(0, labelDilution1.Text.Length - 1));
                        percent = percent % 10 == 0 ? percent - 10 : percent / 10 * 10;
                        percent = Math.Max(0, percent);
                        labelDilution1.Text = percent + @"%";
                        labelDilution2.Text = labelDilution1.Text;
                    }
                }
                else if (btn == buttonClean1)
                {
                    if (buttonClean1.Text == @"加满")
                    {
                        var supplyVolume = ConfigRead("SupplyVolume").Split('-');
                        var supplyLeft = ConfigRead("SupplyLeft").Split('-');
                        UpdateAppConfig("SupplyLeft",
                            string.Format("{0}-{1}-{2}-{3}", supplyLeft[0], supplyVolume[1], supplyLeft[2], supplyLeft[3]));
                        UpdateSupplyUi();
                    }
                    else if (buttonClean1.Text == @"+1L")
                    {
                        labelClean1.Text =
                            (int.Parse(labelClean1.Text.Substring(0, labelClean1.Text.Length - 1)) + 1) + @"L";
                        labelClean2.Text = labelClean1.Text;
                    }
                    else if (buttonClean1.Text == @"+10%")
                    {
                        var percent = int.Parse(labelClean1.Text.Substring(0, labelClean1.Text.Length - 1));
                        percent = percent % 10 == 0 ? percent + 10 : percent / 10 * 10 + 10;
                        percent = Math.Min(100, percent);
                        labelClean1.Text = percent + @"%";
                        labelClean2.Text = labelClean1.Text;
                    }
                }
                else if (btn == buttonClean2)
                {
                    if (buttonClean2.Text == @"灌注")
                    {
                        serialPort_DataSend(serialPortMain, "#3061");
                        UpdateSupplyLeft(0, 2);
                    }
                    else if (buttonClean2.Text == @"-1L")
                    {
                        var volume = int.Parse(labelClean1.Text.Substring(0, labelClean1.Text.Length - 1));
                        volume--;
                        volume = Math.Max(0, volume);
                        labelClean1.Text = volume + @"L";
                        labelClean2.Text = labelClean1.Text;
                    }
                    else if (buttonClean2.Text == @"-10%")
                    {
                        var percent = int.Parse(labelClean1.Text.Substring(0, labelClean1.Text.Length - 1));
                        percent = percent % 10 == 0 ? percent - 10 : percent / 10 * 10;
                        percent = Math.Max(0, percent);
                        labelClean1.Text = percent + @"%";
                        labelClean2.Text = labelClean1.Text;
                    }
                }

                else if (btn == buttonWaste1)
                {
                    if (buttonWaste1.Text == @"清空")
                    {
                        var supplyLeft = ConfigRead("SupplyLeft").Split('-');
                        UpdateAppConfig("SupplyLeft", string.Format("{0}-{1}-0-{2}", supplyLeft[0], supplyLeft[1], supplyLeft[3]));
                        UpdateSupplyUi();
                    }
                    else if (buttonWaste1.Text == @"+1L")
                    {
                        labelWaste1.Text =
                            (int.Parse(labelWaste1.Text.Substring(0, labelWaste1.Text.Length - 1)) + 1) + @"L";
                        labelWaste2.Text = labelWaste1.Text;
                    }
                    else if (buttonWaste1.Text == @"+10%")
                    {
                        var percent = int.Parse(labelWaste1.Text.Substring(0, labelWaste1.Text.Length - 1));
                        percent = percent % 10 == 0 ? percent + 10 : percent / 10 * 10 + 10;
                        percent = Math.Min(100, percent);
                        labelWaste1.Text = percent + @"%";
                        labelWaste2.Text = labelWaste1.Text;
                    }
                }
                else if (btn == buttonWaste2)
                {
                    if (buttonWaste2.Text == @"-1L")
                    {
                        var volume = int.Parse(labelWaste1.Text.Substring(0, labelWaste1.Text.Length - 1));
                        volume--;
                        volume = Math.Max(0, volume);
                        labelWaste1.Text = volume + @"L";
                        labelWaste2.Text = labelWaste1.Text;
                    }
                    else if (buttonWaste2.Text == @"-10%")
                    {
                        var percent = int.Parse(labelWaste1.Text.Substring(0, labelWaste1.Text.Length - 1));
                        percent = percent % 10 == 0 ? percent - 10 : percent / 10 * 10;
                        percent = Math.Max(0, percent);
                        labelWaste1.Text = percent + @"%";
                        labelWaste2.Text = labelWaste1.Text;
                    }
                }
                //2017-2-24
                //用户界面按键
                else if (btn == buttonPicClose)
                {
                    panelPic.Visible = false;
                }
                else if (btn == buttonCheckAllUsers)
                {
                    tabControlSetting.SelectedTab = tabPageUser;
                    panelAllUsers.Visible = true;
                    ShowAllUsers();
                }
                else if (btn == buttonUserDelete)
                {
                    if (SqlData.SelectUsertype("1").Rows.Count <= 1 & SqlData.SelectUserName(textBoxNewUserName.Text.ToString()).Rows[0][1].ToString() == "1")
                    {
                        MessageboxShow("不能删除所有管理员");
                    }
                    else
                    {
                        SqlData.DeleteUser(textBoxNewUserName.Text.ToString());
                        MessageboxShow("用户已经删除");
                        ShowAllUsers();
                        SetUsersetting();
                    }
                }
                else if (btn == buttonUserChange)
                {
                    if (buttonUserChange.Text == "新建")
                    {
                        if (textBoxNewPassword.Text == textBoxRepeatPassword.Text)
                        {
                            if (SqlData.SelectUserName(textBoxNewUserName.Text).Rows.Count >= 1)
                            {
                                MessageboxShow("该用户已经存在");
                            }
                            else if (SqlData.SelectallUserName().Rows.Count >= 10)
                            {
                                MessageboxShow("用户数量已达上限");
                            }
                            else if (comboBoxUserType.Text == "")
                            {
                                MessageboxShow("请选择用户类型");
                            }
                            else if (textBoxNewPassword.Text != "" && textBoxRepeatPassword.Text != "" && textBoxNewUserName.Text != "")
                            {
                                try
                                {
                                    var usertype = "";
                                    if (comboBoxUserType.Text == "操作员")
                                    {
                                        usertype = "0";
                                    }
                                    else if (comboBoxUserType.Text == "管理员")
                                    {
                                        usertype = "1";
                                    }
                                    SqlData.InsertNewUser(textBoxNewUserName.Text, textBoxNewPassword.Text, usertype);
                                    MessageboxShow("新用户创建成功");
                                    ShowAllUsers();
                                }
                                catch (Exception ee)
                                {
                                    MessageboxShow("新用户创建失败" + ee.ToString());
                                }
                            }
                            else if (textBoxNewPassword.Text == "" || textBoxRepeatPassword.Text == "" || textBoxNewUserName.Text == "")
                            {
                                MessageboxShow("用户名或密码不能为空");
                            }
                            textBoxNewPassword.Clear();
                            textBoxNewUserName.Clear();
                            textBoxRepeatPassword.Clear();
                        }
                        else
                        {
                            MessageboxShow("两次密码输入不一致");
                            textBoxNewPassword.Clear();
                            textBoxRepeatPassword.Clear();
                        }
                        comboBoxUserType.Text = "";
                    }
                    else if (buttonUserChange.Text == "更改")
                    {
                        if (textBoxNewPassword.Text == textBoxRepeatPassword.Text && textBoxNewPassword.Text != "" && textBoxRepeatPassword.Text != "")
                        {
                            SqlData.ChangePassword(textBoxNewUserName.Text.ToString(), textBoxNewPassword.Text.ToString());
                            MessageboxShow("密码已经更改");
                            ShowAllUsers();
                            SetUsersetting();
                        }
                        else if (textBoxNewPassword.Text == "" | textBoxRepeatPassword.Text == "")
                        {
                            MessageboxShow("密码不能为空");
                            textBoxNewPassword.Clear();
                            textBoxRepeatPassword.Clear();
                        }
                        else
                        {
                            MessageboxShow("两次密码输入不一致");
                            textBoxNewPassword.Clear();
                            textBoxRepeatPassword.Clear();
                        }
                    }

                }//2017-2-24
                else if (btn == buttonMinOpen)
                {
                    WindowState = FormWindowState.Minimized;
                }
            }
            catch (Exception ee)
            {
                Log_Add("751" + ee.ToString(), false);
            }
        }

        private void button_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender == button2)
            {
                if (tabControlMain.SelectedTab == tabPageHome | tabControlMain.SelectedTab == tabPageMessage |
    tabControlMain.SelectedTab == tabPageSearch)
                {
                    timerPageUp.Interval = 500;
                    timerPageUp.Start();
                }
            }
            else if (sender == button3)
            {
                if (tabControlMain.SelectedTab == tabPageHome | tabControlMain.SelectedTab == tabPageMessage |
    tabControlMain.SelectedTab == tabPageSearch)
                {
                    timerPageDown.Start();
                }
            }
            else if (sender == buttonShowPassword)
            {
                textBoxLoginPassword.PasswordChar = '\0';
            }
        }

        private void button_MouseUp(object sender, MouseEventArgs e)
        {
            if (sender == button2)
            {
                if (tabControlMain.SelectedTab == tabPageHome | tabControlMain.SelectedTab == tabPageMessage |
       tabControlMain.SelectedTab == tabPageSearch)
                {
                    timerPageDown.Interval = 500;
                    timerPageUp.Stop();
                }
            }
            else if (sender == button3)
            {
                if (tabControlMain.SelectedTab == tabPageHome | tabControlMain.SelectedTab == tabPageMessage |
    tabControlMain.SelectedTab == tabPageSearch)
                {
                    timerPageDown.Stop();
                }
            }
            else if (sender == buttonShowPassword)
            {
                textBoxLoginPassword.PasswordChar = '*';
            }
        }

        //主菜单下左侧tabcontrol切换至待测样本
        private void buttonHomeLeft_Click(object sender, EventArgs e)
        {
            try
            {
                var btn = (Button)sender;
                var buttonName = btn.Name.Substring(6);
                dataGridViewMain.ReadOnly = true;
                buttonDoing.BackgroundImage = Resources.Button_Normal;
                buttonDoing.Font = new Font("微软雅黑", 18, FontStyle.Bold);
                buttonDone.BackgroundImage = Resources.Button_Normal;
                buttonDone.Font = new Font("微软雅黑", 18, FontStyle.Bold);
                buttonException.BackgroundImage = Resources.Button_Normal11;
                buttonException.Font = new Font("微软雅黑", 18, FontStyle.Bold);
                var columnText = GetResxString(buttonName, "ColumnText");
                var buttonText = GetResxString(buttonName, "ButtonText").Split('|');
                switch (buttonName)
                {
                    case "Doing":
                        buttonDoing.BackgroundImage = Resources.Button_Press;
                        buttonDoing.Font = new Font("微软雅黑", 15, FontStyle.Bold);
                        dataGridViewMain.Size = new Size(464, 380);
                        if (ConfigRead("ASUEnable") == "1")
                        {
                            dataGridViewMain.RowTemplate.Height = 38;
                        }
                        UpdatedataGridViewMain("Doing");
                        panelPic.Visible = false;//2017-2-24
                        break;
                        
                    case "Done":
                        buttonDone.BackgroundImage = Resources.Button_Press;
                        buttonDone.Font = new Font("微软雅黑", 15, FontStyle.Bold);
                        dataGridViewMain.Size = new Size(464, 432);
                        dataGridViewMain.RowTemplate.Height = 54;
                        UpdatedataGridViewMain("Done");
                        panelPic.Visible = false;//2017-2-24
                        break;

                    case "Exception":
                        buttonException.BackgroundImage = Resources.Button_Press1;
                        buttonException.Font = new Font("微软雅黑", 15, FontStyle.Bold);
                        dataGridViewMain.Size = new Size(464, 432);
                        dataGridViewMain.RowTemplate.Height = 54;
                        UpdatedataGridViewMain("Exception");
                        labelExceptionNo.Visible = false;
                        labelExceptionNo.Text = "0";
                        break;
                }
                labelOther.Text = columnText;
                button1.Text = buttonText[0];
                button2.Text = buttonText[1];
                button3.Text = buttonText[2];
                button4.Text = buttonText[3];
            }
            catch (Exception)
            {
                Log_Add("主表切换时出错", true);
            }
        }
        int ReagentStatus = 0;
        private void buttonHomeRight_Click(object sender, EventArgs e)
        {
            var buttonName = ((Button)sender).Name.Substring(6);
            buttonReagent.BackgroundImage = Resources.Button_Normal;
            buttonSupply.BackgroundImage = Resources.Button_Normal;
            buttonSupply.Font = new Font("微软雅黑", 18, FontStyle.Bold);
            buttonReagent.Font = new Font("微软雅黑", 18, FontStyle.Bold);
            switch (buttonName)
            {
                case "Reagent":
                    buttonReagent.BackgroundImage = Resources.Button_Press;
                    buttonReagent.Font = new Font("微软雅黑", 15, FontStyle.Bold);
                    if (tabControlMainRight.SelectedTab == tabPageSupply | tabControlMainRight.SelectedTab == tabPageSupplyFloatBall)
                    {
                        switch (ReagentStatus)
                        {
                            case 0: tabControlMainRight.SelectedTab = tabPageReagent; break;
                            case 1: tabControlMainRight.SelectedTab = tabPageReagentOpen; break;
                            case 2: tabControlMainRight.SelectedTab = tabPageQRAlert; break;
                            default:
                                break;
                        }
                        UpdateReagentStore();
                    }
                    break;

                case "Supply":
                    buttonSupply.BackgroundImage = Resources.Button_Press;
                    buttonSupply.Font = new Font("微软雅黑", 15, FontStyle.Bold);
                    tabControlMainRight.SelectedTab = ConfigRead("FloatBallEnable") == "0" ? tabPageSupply : tabPageSupplyFloatBall;
                    break;
            }
        }

        //导航按钮操作切换主tabcontrol的UI设置，主要为主菜单、查询菜单、质控、设置、信息、紧急停止

        private void buttonMenu_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            if (btn != buttonHome)
            {
                buttonHome.BackgroundImage = Resources.Home_Normal;
                buttonSearch.BackgroundImage = Resources.Search_Normal;
                buttonQC.BackgroundImage = Resources.QC_Normal;
                buttonSetting.BackgroundImage = Resources.Setting_Normal;
                buttonMessage.BackgroundImage = Resources.Message_Normal;
            }
            var buttonText = GetResxString(btn.Name.Substring(6), "ButtonText").Split('|');
            if (buttonText.Length < 4)
            {
                button1.Text = "";
                button2.Text = "";
                button3.Text = "";
                button4.Text = "";
            }
            else
            {
                button1.Text = buttonText[0];
                button2.Text = buttonText[1];
                button3.Text = buttonText[2];
                button4.Text = buttonText[3];
            }
            switch (btn.Name)
            {
                case "buttonHome":
                    if (_otherInt[2] != 3)
                    {
                        Log_Add("请进入测试模式", true); return;
                    }
                    buttonSearch.BackgroundImage = Resources.Search_Normal;
                    buttonQC.BackgroundImage = Resources.QC_Normal;
                    buttonSetting.BackgroundImage = Resources.Setting_Normal;
                    buttonMessage.BackgroundImage = Resources.Message_Normal;
                    buttonHome.BackgroundImage = Resources.Home_Press;
                    buttonHomeLeft_Click(buttonDoing, null);
                    panelPic.Visible = false;//2017-2-23
                    break;

                case "buttonSearch":
                    buttonSearch.BackgroundImage = Resources.Search_Press;
                    UpdatedataGridViewSearch("1", "12");
                    var resultcount = int.Parse(SqlData.SelectResultCount(_searchcondition).Rows[0][0].ToString());
                    labelSampleCount.Text = "测试数量：  " + resultcount;//2017-2-24
                    labelpage.Text = @"/" + (resultcount / 12 + (resultcount % 12 == 0 ? 0 : 1));
                    textBoxPage.Text = @"1";
                    break;

                case "buttonQC":
                    buttonQC.BackgroundImage = Resources.QC_Press;
                    labelQCInfo.Text = string.Format(@"{0}年{1}月CRP 质控统计：", DateTime.Now.Year, DateTime.Now.Month.ToString().PadLeft(2, '0'));
                    QcChartShow(DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString(), "0");
                    break;

                case "buttonSetting":
                    buttonSetting.BackgroundImage = Resources.Setting_Press;
                    //2017-2-24
                    tabControlSetting.SelectedTab = tabPageGeneralSetting;
                    comboBoxUserType.Text = "";
                    comboBoxUserType.Enabled = true;
                    if (UserType == "1")
                    {
                        buttonCheckAllUsers.Visible = true;
                        SetUsersetting();
                    }
                    else
                    {
                        buttonCheckAllUsers.Visible = false;
                        buttonCheckAllUsers.Enabled = false;
                    }//2017-2-24
                    if (ConfigRead("ASUEnable") == "1")
                    {
                        groupBox5.Visible = true;
                    }//20
                    break;

                case "buttonMessage":
                    buttonMessage.BackgroundImage = Resources.Message_Press; break;
            }
            labelMenu.Text = GetResxString(btn.Name.Substring(6), "MenuText");

            tbtemp = (TabPage)Controls.Find("tabPage" + btn.Name.Substring(6), true)[0];
        }

        private void buttonQCTestItem_Click(object sender, EventArgs e)
        {
            var qcTestItem = SqlData.SelectQcTestItem();
            var qcCount = qcTestItem.Rows.Count;
            for (var i = 0; i < qcCount; i++)
            {
                var btns = (Button)Controls.Find("buttonQC" + i, true)[0];
                btns.ForeColor = Color.FromArgb(210, 210, 210);
                btns.BackColor = Color.FromArgb(40, 40, 40);
            }
            var btn = (Button)sender;
            btn.BackColor = Color.FromArgb(210, 210, 210);
            btn.ForeColor = Color.FromArgb(40, 40, 40);
            var qcsetting = SqlData.SelectQcSetting(btn.Text);
            textBoxQCSD.Text = "";
            textBoxQCSampleNo.Text = "";
            textBoxQCTarget.Text = "";
            panelQCPoint.Visible = true;

            if (qcsetting.Rows.Count <= 0)
            {
                dataGridViewQCSetting.Visible = false;
                return;
            }
            buttonFixQCData.Visible = true;
            dataGridViewQCSetting.DataSource = qcsetting;
            dataGridViewQCSetting.Columns[1].Width = 180;
            dataGridViewQCSetting.Columns[2].Width = 100;
            dataGridViewQCSetting.Columns[3].Width = 100;
            dataGridViewQCSetting.Columns[0].Visible = false;
            dataGridViewQCSetting.Columns[4].Visible = false;
            dataGridViewQCSetting.CurrentCell = null;
            dataGridViewQCSetting.Visible = true;
            buttonFixQCData.Visible = false;
        }
        

        private void buttonSubMenu_Click(object sender, EventArgs e)
        {
            var btn = ((Button)sender).Name;
            if (btn == "button1")
            {            //正在测试菜单1
                if (tabControlMain.SelectedTab == tabPageHome & labelOther.Text == GetResxString("Doing", "ColumnText"))
                {
                    var dttestitem = SqlData.SelectTestItem();
                    if (dttestitem.Rows.Count <= 0)
                    {
                        // ReSharper disable once ResourceItemNotResolved
                        Log_Add(Resources.SwitchTestItemError, true);
                        return;
                    }
                    if (labelhuman.Visible == true)
                    {
                        TestItemText = "人工进样" + "|";
                    }
                    else
                    {
                        TestItemText = labelNextTestItem.Text + "|";
                    }
                    for (var i = 0; i < dttestitem.Rows.Count; i++)
                    {
                        TestItemText += dttestitem.Rows[i][0] + "|";
                    }
                    
                    var newTestitem = Mytestitem();
                    //空项目名不处理
                    if (newTestitem == null) return;
                    if (newTestitem == "") return;
                    if (labelNextTestItem.Text == newTestitem)
                    {
                        //if (newTestitem != "人工进样" & newTestitem != "CRPQC" & !serialPortME.IsOpen)
                        if (labelhuman.Visible == true & !serialPortME.IsOpen)//2017-4-1
                        {
                            labelhuman.Visible = false;
                            serialPortME.Open();
                            serialPort_DataSend(serialPortMain, "#3024$0");
                        }
                        return;
                    } 
                    if (ConfigRead("ASUEnable") == "1")
                    {
                        if (newTestitem == "人工进样" | newTestitem == "CRPQC" & serialPortME.IsOpen)
                        {
                            if (newTestitem == "人工进样")
                            {
                                DataTable dt = SqlData.SelectWorkRunlistforASU();
                                if (dt.Rows.Count == 0)
                                {
                                    ASUComplete = true;
                                }
                                if (ASUComplete == false)
                                {
                                    MessageboxShow("ASU测试尚未完成，请勿开启人工进样模式");
                                    return;
                                }
                                else
                                {
                                    labelhuman.Visible = true;
                                    serialPortME.Close();
                                    serialPort_DataSend(serialPortMain, "#3024$1");
                                }
                            } 
                        }
                        else if (newTestitem != "人工进样" & newTestitem != "CRPQC" & !serialPortME.IsOpen)
                        {
                            labelhuman.Visible = false;
                            serialPortME.Open();
                            serialPort_DataSend(serialPortMain, "#3024$0");
                        }
                    }
                    if(newTestitem != "人工进样")
                    labelNextTestItem.Text = newTestitem;
                    var reagentStoreId = SqlData.SelectproductidbyTestItemName(labelNextTestItem.Text).Rows[0][0].ToString();
                    SwitchTestItem(reagentStoreId);
                    // ReSharper disable once ResourceItemNotResolved
                    Invoke(new Action(() => Log_Add(Resources.CC07 + labelNextTestItem.Text, false, Color.Red)));
                    if (newTestitem !=  "人工进样" & newTestitem != "CRPQC" )
                    UpdateAppConfig("TestItem", labelNextTestItem.Text);
                    if (newTestitem.Substring(newTestitem.Length - 2) == "QC")
                    {
                        labelNextSampleNo.Text =
                            SqlData.SelectQcSampleno(newTestitem.Substring(0, newTestitem.Length - 2)).Rows[0][0].ToString();
                    }
                    else
                    {
                        labelNextSampleNo.Text = ConfigRead("StartSampleNo");
                    }
                }
                //当日结果菜单
                else if (tabControlMain.SelectedTab == tabPageHome & labelStep.Text == GetResxString("Done", "ColumnText"))
                {
                }
                else if (tabControlMain.SelectedTab == tabPageSetting)
                {
                    tabControlSetting.SelectedTab = tabPageGeneralSetting;
                }
                else if (tabControlMain.SelectedTab == tabPageQC)
                {
                    var dttestitem = SqlData.SelectQcTestItem();
                    var productname = labelQCInfo.Text.Substring(labelQCInfo.Text.IndexOf("月", StringComparison.Ordinal) + 1);
                    productname = productname.Substring(0, productname.IndexOf(" ", StringComparison.Ordinal));
                    TestItemText = string.Format("QC{0}|", productname);
                    for (var i = 0; i < dttestitem.Rows.Count; i++)
                    {
                        TestItemText += dttestitem.Rows[i][0] + "|";
                    }
                    var newTestitem = Mytestitem();
                    labelQCInfo.Text = labelQCInfo.Text.Replace(productname, newTestitem);
                    var buttonText = GetResxString("QC", "ButtonText").Split('|');

                    if (button4.Text == buttonText[3])
                        QcChartShow(labelQCInfo.Text.Substring(5, 2), labelQCInfo.Text.Substring(0, 4),
                            SqlData.SelectProductIdByname(newTestitem).Rows[0][0].ToString());
                    else
                    {
                        buttonSubMenu_Click(button4, null);
                        buttonSubMenu_Click(button4, null);
                    }
                }
                else if (tabControlMain.SelectedTab == tabPageMessage)
                {
                    dataGridViewLog.Rows.Clear();
                }
                else if (tabControlMain.SelectedTab == tabPageSearch)
                {
                    //用于将结果发送至ireader.txt 主要用于lis传输过程中异常时需要再次发送结果。
                    var txt = "";
                    var history = "";
                    if (File.Exists(Application.StartupPath + "\\ireader.txt"))
                    {
                        StreamReader sr = new StreamReader(Application.StartupPath + "\\ireader.txt");
                        history = sr.ReadToEnd();
                        sr.Close();
                    }

                    for (int i = 0; i < dataGridViewSearch.RowCount; i++)
                    {
                        txt = dataGridViewSearch[0, i].Value.ToString() + "," + dataGridViewSearch[1, i].Value.ToString() + ",";

                        if (dataGridViewSearch[2, i].Value.ToString().IndexOf(".") > -1)
                            txt += dataGridViewSearch[2, i].Value.ToString().Substring(0, dataGridViewSearch[2, i].Value.ToString().IndexOf(".")+3).Replace("*","");
                        else
                            txt += "Error";
                        txt += "," + dataGridViewSearch[3, i].Value.ToString();
                        if (history.IndexOf(txt) == -1)
                            history += txt + "\r\n";
                    }

                    if (history != "")
                    {
                        StreamWriter sw = new StreamWriter(Application.StartupPath + "\\ireader.txt");
                        sw.Write(history);
                        sw.Close();
                        Log_Add("已将该页结果重新发送到结果文件",true);
                    }

                }
            }
            else if (btn == "button2")
            {
                //长按上一页时能够每隔500ms上一页，取消首页尾页
                if (tabControlMain.SelectedTab == tabPageHome)
                {
                    if (dataGridViewMain.RowCount > 0)
                    {
                        dataGridViewMain.FirstDisplayedScrollingRowIndex =
                            Math.Max(dataGridViewMain.FirstDisplayedScrollingRowIndex - 7, 0);
                    }
                }
                else if (tabControlMain.SelectedTab == tabPageMessage)
                {
                    if (dataGridViewLog.RowCount > 0)
                    {
                        dataGridViewLog.FirstDisplayedScrollingRowIndex = Math.Max(0,
                            dataGridViewLog.FirstDisplayedScrollingRowIndex - 14);
                    }
                }
                else if (tabControlMain.SelectedTab == tabPageSearch)
                {
                    var page = textBoxPage.Text;
                    if (int.Parse(page) > 1)
                    {
                        Invoke(new Action(() => UpdatedataGridViewSearch((int.Parse(page) - 1).ToString(), "12")));
                        textBoxPage.Text = (int.Parse(page) - 1).ToString();
                    }
                }
                else if (tabControlMain.SelectedTab == tabPageSetting)
                {
                    tabControlSetting.SelectedTab = tabPageTestItemSetting;
                    dataGridViewTestItem.DataSource = SqlData.SelectTestIteminfo();
                    dataGridViewTestItem.Columns[0].Visible = false;
                    dataGridViewTestItem.Columns[1].Visible = false;
                    dataGridViewTestItem.Columns[7].Visible = false;
                    dataGridViewTestItem.Columns[8].Visible = false;
                    dataGridViewTestItem.Columns[2].Width = 100;
                    dataGridViewTestItem.Columns[3].Width = 100;
                    dataGridViewTestItem.Columns[4].Width = 100;
                    dataGridViewTestItem.Columns[5].Width = 100;
                    dataGridViewTestItem.Columns[6].Width = 100;
                }
                else if (tabControlMain.SelectedTab == tabPageQC)
                {
                    var year = labelQCInfo.Text.Substring(0, 4);
                    var month = labelQCInfo.Text.Substring(5, 2);
                    var productname = labelQCInfo.Text.Substring(labelQCInfo.Text.IndexOf("月", StringComparison.Ordinal) + 1);
                    productname = productname.Substring(0, productname.IndexOf(" ", StringComparison.Ordinal));
                    var dt = DateTime.Parse(string.Format("{0}/{1}-01", year, month)).AddMonths(-1);
                    labelQCInfo.Text = string.Format(@"{0}年{1}月{2} 质控统计：", dt.Year, dt.Month.ToString().PadLeft(2, '0'), productname);
                    QcChartShow(dt.Month.ToString(), dt.Year.ToString(), SqlData.SelectProductIdByname(productname).Rows[0][0].ToString());

                }
            }
            else if (btn == "button3")
            {
                if (tabControlMain.SelectedTab == tabPageHome)
                {
                    if (dataGridViewMain.RowCount > 0)
                    {
                        dataGridViewMain.FirstDisplayedScrollingRowIndex =
                            Math.Min(dataGridViewMain.FirstDisplayedScrollingRowIndex + 7, dataGridViewMain.RowCount - 1);
                    }
                }
                else if (tabControlMain.SelectedTab == tabPageMessage)
                {
                    if (dataGridViewLog.RowCount > 0)
                    {
                        dataGridViewLog.FirstDisplayedScrollingRowIndex = Math.Min(dataGridViewLog.RowCount - 1,
                            dataGridViewLog.FirstDisplayedScrollingRowIndex + 14);
                    }
                }
                else if (tabControlMain.SelectedTab == tabPageSearch)
                {
                    var page = textBoxPage.Text;
                    var pageall = labelpage.Text.Substring(1);
                    if (int.Parse(page) < int.Parse(pageall))
                    {
                        Invoke(new Action(() => UpdatedataGridViewSearch((int.Parse(page) + 1).ToString(), "12")));
                        textBoxPage.Text = (int.Parse(page) + 1).ToString();
                    }
                }
                else if (tabControlMain.SelectedTab == tabPageSetting)
                {
                    tabControlSetting.SelectedTab = tabPageQCSetting;
                    QcSettingShow();
                }
                else if (tabControlMain.SelectedTab == tabPageQC)
                {
                    var year = labelQCInfo.Text.Substring(0, 4);
                    var month = labelQCInfo.Text.Substring(5, 2);
                    var productname = labelQCInfo.Text.Substring(labelQCInfo.Text.IndexOf("月", StringComparison.Ordinal) + 1);
                    productname = productname.Substring(0, productname.IndexOf(" ", StringComparison.Ordinal));
                    var dt = DateTime.Parse(string.Format("{0}/{1}-01", year, month)).AddMonths(+1);
                    labelQCInfo.Text = string.Format(@"{0}年{1}月{2} 质控统计：", dt.Year, dt.Month.ToString().PadLeft(2, '0'), productname);
                    QcChartShow(dt.Month.ToString(), dt.Year.ToString(),
                        SqlData.SelectProductIdByname(productname).Rows[0][0].ToString());
                }
            }
            else if (btn == "button4")
            {
                //button4为下一样本
                if (tabControlMain.SelectedTab == tabPageHome & labelOther.Text == GetResxString("Doing", "ColumnText"))
                {
                    if (labelNextTestItem.Text.IndexOf("QC", StringComparison.Ordinal) > -1)
                    {
                        var dt =
                            SqlData.SelectQcSampleno(labelNextTestItem.Text.Substring(0,
                                labelNextTestItem.Text.IndexOf("QC", StringComparison.Ordinal)));
                        if (labelNextSampleNo.Text == dt.Rows[dt.Rows.Count - 1][0].ToString())
                            labelNextSampleNo.Text = dt.Rows[0][0].ToString();
                        else
                        {
                            for (var i = 0; i < dt.Rows.Count; i++)
                            {
                                if (labelNextSampleNo.Text == dt.Rows[i][0].ToString())
                                {
                                    labelNextSampleNo.Text = dt.Rows[i + 1][0].ToString();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        CounterText = string.Format("AllnoDot|{0}|18|92", labelNextSampleNo.Text);
                        var str = VirtualKeyBoard();
                        if (str == "Cancel") return;
                        if (str == "") { ShowMyAlert("样本号不能为空"); return; }
                        if (str.Length > 20) { ShowMyAlert("样本号长度不能大于20"); return; }
                        labelNextSampleNo.Text = str;
                        // ReSharper disable once ResourceItemNotResolved
                        var msgLog = Resources.CC05;
                        msgLog += labelNextSampleNo.Text;
                        Invoke(new Action(() => Log_Add(msgLog, false, Color.FromArgb(128, 128, 128))));
                    }
                }
                if (tabControlMain.SelectedTab == tabPageSearch)
                {
                    var page = int.Parse(labelpage.Text.Substring(1));
                    if (page < 1) return;
                    var resulttext = "样本号,测试项目,结果,测试时间\r\n";
                    for (var i = 1; i <= page; i++)
                    {
                        var dt = SqlData.SelectResultList(i.ToString(), 12.ToString(), _searchcondition);
                        for (var j = 0; j < dt.Rows.Count; j++)
                        {
                            resulttext += string.Format("{0},{1},{2},{3}\r\n", dt.Rows[j][0], dt.Rows[j][1], dt.Rows[j][2], dt.Rows[j][3]);
                        }
                    }
                    var fs = new FileStream(string.Format("{0}/Data{1:yyMMdd}.csv", Application.StartupPath, DateTime.Now), FileMode.Create,
                        FileAccess.Write);
                    var sw = new StreamWriter(fs, Encoding.UTF8);
                    sw.Write(resulttext);
                    sw.Flush();
                    sw.Close();
                    fs.Close();
                    MessageBox.Show(string.Format("数据成功导出到安装目录，文件名为Data{0:yyMMdd}.csv", DateTime.Now));
                }
                else if (tabControlMain.SelectedTab == tabPageSetting)
                {
                    tabControlSetting.SelectedTab = tabPageOtherSetting;
                    labelDriveLeft.Text =
                        string.Format(@"{0:F2}GB/{1:F2}GB", (double)DriveInfo.GetDrives()[0].AvailableFreeSpace / 1024 / 1024 / 1024, (double)DriveInfo.GetDrives()[0].TotalSize / 1024 / 1024 / 1024);

                    if (Directory.Exists(Application.StartupPath + @"\Rawdata"))
                    {
                        if (!Directory.Exists(Application.StartupPath + @"\Rawdata\Error"))
                        {
                            Directory.CreateDirectory(Application.StartupPath + "/Rawdata/Error");
                        }

                        //定义一个DirectoryInfo对象
                        var di = new DirectoryInfo(Application.StartupPath + @"\Rawdata"); 

                        //通过GetFiles方法,获取di目录中的所有文件的大小
                        var len = di.GetFiles().Aggregate<FileInfo, double>(0, (current, fi) => current + fi.Length);
                        if (len > 0)
                        {
                            if (len > 1024 * 1024 * 1024)
                                // ReSharper disable once LocalizableElement
                                labelDriveLeft.Text += string.Format(@"{0}数据文件大小：{1:F2}GB", Environment.NewLine, len / 1024 / 1024 / 1024);
                            else if (len > 1024 * 1024)
                                // ReSharper disable once LocalizableElement
                                labelDriveLeft.Text += string.Format(@"{0}数据文件大小：{1:F2}MB", Environment.NewLine, len / 1024 / 1024);
                            else if (len > 1024)
                                // ReSharper disable once LocalizableElement
                                labelDriveLeft.Text += string.Format(@"{0}数据文件大小：{1:F2}KB", Environment.NewLine, len / 1024);
                        }
                    }
                    else
                    {

                        Directory.CreateDirectory(Application.StartupPath + "/rawdata");
                    }
                    if (Directory.Exists(Application.StartupPath + @"\fluodata"))
                    {
                        //定义一个DirectoryInfo对象
                        var di = new DirectoryInfo(Application.StartupPath + @"\fluodata");

                        //通过GetFiles方法,获取di目录中的所有文件的大小
                        var len = di.GetFiles().Aggregate<FileInfo, double>(0, (current, fi) => current + fi.Length);
                        if (len > 0)
                        {
                            if (len > 1024 * 1024 * 1024)
                                // ReSharper disable once LocalizableElement
                                labelDriveLeft.Text += string.Format(@"{0}数据文件大小：{1:F2}GB", Environment.NewLine, len / 1024 / 1024 / 1024);
                            else if (len > 1024 * 1024)
                                // ReSharper disable once LocalizableElement
                                labelDriveLeft.Text += string.Format(@"
                                                       数据文件大小：{0:F2}MB", len / 1024 / 1024);
                            else if (len > 1024)
                                // ReSharper disable once LocalizableElement
                                labelDriveLeft.Text += string.Format(@"
                                                       数据文件大小：{0:F2}KB", len / 1024);
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory(Application.StartupPath + "/fluodata");
                    }
                }
                else if (tabControlMain.SelectedTab == tabPageQC)
                {
                    var month = labelQCInfo.Text.Substring(5, 2);
                    var year = labelQCInfo.Text.Substring(0, 4);
                    var productname = labelQCInfo.Text.Substring(labelQCInfo.Text.IndexOf("月") + 1);
                    productname = productname.Substring(0, productname.IndexOf(" "));
                    var productid = SqlData.SelectProductIdByname(productname).Rows[0][0].ToString();
                    var dt = SqlData.SelectQcResult(month, year, productid);
                    if (dt.Rows.Count == 0) return;
                    var data = new string[DateTime.DaysInMonth(int.Parse(year), int.Parse(month)), 4];
                    var datastr = "";
                    //select (result-target)/sd,qcsampleno,productname,day(testdate),result,target,sd
                    if (dt.Rows[0][1].ToString() != dt.Rows[dt.Rows.Count - 1][1].ToString())
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int row = int.Parse(dt.Rows[i][3].ToString()) - 1;
                            if (dt.Rows[i][1].ToString() == dt.Rows[0][1].ToString())
                            {
                                data[row, 0] = dt.Rows[i][4].ToString();
                                data[row, 1] = double.Parse(dt.Rows[i][0].ToString()).ToString("F2");
                            }
                            else
                            {
                                data[row, 2] = dt.Rows[i][4].ToString();
                                data[row, 3] = double.Parse(dt.Rows[i][0].ToString()).ToString("F2");
                            }
                        }

                        datastr = "Date," + dt.Rows[0][1].ToString() + ",," + dt.Rows[dt.Rows.Count - 1][1].ToString() + ",,Result\r\n";
                        for (int i = 0; i < data.GetLength(0); i++)
                        {
                            datastr += (i + 1).ToString() + "," + data[i, 0] + "," + data[i, 1] + "," + data[i, 2] + "," + data[i, 3] + ",";
                            if (i < 15)
                                datastr += dataGridViewQC1[i + 1, dataGridViewQC1.RowCount - 1].Value.ToString() + "\r\n";
                            else
                                datastr += dataGridViewQC2[i - 14, dataGridViewQC1.RowCount - 1].Value.ToString() + "\r\n";
                        }
                        datastr = labelQCInfo.Text + "\r\n" + datastr;
                        datastr += "\r\n";
                        datastr += ", 靶值, SD , 靶值 , SD\r\n";
                        datastr += ", " + dt.Rows[0][5].ToString() + "," + dt.Rows[0][6].ToString() + "," + dt.Rows[dt.Rows.Count - 1][5].ToString() + "," + dt.Rows[dt.Rows.Count - 1][6].ToString() + "\r\n";

                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int row = int.Parse(dt.Rows[i][3].ToString()) - 1;

                            data[row, 0] = dt.Rows[i][4].ToString();
                            data[row, 1] = double.Parse(dt.Rows[i][0].ToString()).ToString("F2");

                        }

                        datastr = "Date," + dt.Rows[0][1].ToString() + "\r\n";
                        for (int i = 0; i < data.GetLength(0); i++)
                        {
                            datastr += (i + 1).ToString() + "," + data[i, 0] + "," + data[i, 1] + ",";
                            if (i < 15)
                                datastr += dataGridViewQC1[i + 1, dataGridViewQC1.RowCount - 1].Value.ToString() + "\r\n";
                            else
                                datastr += dataGridViewQC2[i - 14, dataGridViewQC1.RowCount - 1].Value.ToString() + "\r\n";
                        }
                        datastr = labelQCInfo.Text + "\r\n" + datastr;
                        datastr += "\r\n";
                        datastr += ", 靶值, SD \r\n";
                        datastr += ", " + dt.Rows[0][5].ToString() + "," + dt.Rows[0][6].ToString() + "," + dt.Rows[dt.Rows.Count - 1][5].ToString() + "," + dt.Rows[dt.Rows.Count - 1][6].ToString() + "\r\n";

                    }

                    StreamWriter sw = new StreamWriter(Application.StartupPath + @"\" + year + "-" + month + "QCReport.csv", true, Encoding.UTF8);
                    sw.Write(datastr);
                    sw.Close();
                    ShowMyAlert("质控数据已保存为" + Application.StartupPath + @"\" + year + "-" + month + "QCReport.csv");
                }
                else if (tabControlMain.SelectedTab == tabPageMessage)
                {
                    button_Click(buttonSaveLog, null);
                }
            }
        }

        /// <summary>
        /// 计算7参数给定浓度的吸光度值
        /// </summary>
        /// <param name="a">浓度</param>
        /// <param name="calparam">吸光度</param>
        /// <returns></returns>
        private double Cal(double a, string calparam)
        {
            var dbxx1 = calparam.Split(',');
            var b = double.Parse(dbxx1[0]) * Math.Log(a) * a * a + double.Parse(dbxx1[1]) * Math.Log(a) * a +
                    double.Parse(dbxx1[2]) * Math.Log(a) + double.Parse(dbxx1[3]) * a * a * a + double.Parse(dbxx1[4]) * a * a +
                    double.Parse(dbxx1[5]) * a + double.Parse(dbxx1[6]);
            return b;
        }

        private double Cal(double a, string calparam,string style)
        {
            var dbxx1 = calparam.Split(',');
            var b = 0.0;
            if (style == "min")
            {
                b = double.Parse(dbxx1[0]) * a + double.Parse(dbxx1[1]);
            }
            else if (style == "max")
            {
                b = (double.Parse(dbxx1[3]) - double.Parse(dbxx1[6])) / 
                    (1 + Math.Pow((a / double.Parse(dbxx1[4])), double.Parse(dbxx1[5]))) + 
                    double.Parse(dbxx1[6]);
            }
            return b;
        }

        /// <summary>
        /// 计算胶体金的浓度值
        /// </summary>
        /// <param name="ty">吸光度</param>
        /// <param name="tyfixid">校准曲线编号</param>
        /// <param name="calibdataid">定标曲线编号</param>
        /// <param name="min">最小浓度值限定</param>
        /// <param name="max">最大浓度值限定</param>
        /// <param name="accurancy">保留的小数位</param>
        /// <returns></returns>
        private double CalResult(double ty, string calibdataid, double min, double max, string accurancy)
        {
            const double j = 0;
            var dtCalibdata = SqlData.SelectCalibData(int.Parse(calibdataid));
            var a = double.Parse(dtCalibdata.Rows[0][1].ToString());
            var b = double.Parse(dtCalibdata.Rows[0][2].ToString());
            var c = double.Parse(dtCalibdata.Rows[0][3].ToString());
            var d = double.Parse(dtCalibdata.Rows[0][4].ToString());
            var e = double.Parse(dtCalibdata.Rows[0][5].ToString());
            var f = double.Parse(dtCalibdata.Rows[0][6].ToString());
            var g = double.Parse(dtCalibdata.Rows[0][7].ToString());
            var calparam = string.Format("6^{0},{1},{2},{3},{4},{5},{6}", a, b, c, d, e, f, g);
            var fun = calparam.Split('^');
            if (int.Parse(dtCalibdata.Rows[0][0].ToString()) == 6)//CRP项目
            {
                if (Cal(min, fun[1]) > ty) return min;
                if (Cal(max, fun[1]) < ty) return max;

                for (var i = 0; i < 10000; i++)
                {
                    var a1 = Cal(min + (max - min) / 10000 * i, fun[1]);
                    var a2 = Cal(min + (max - min) / 10000 * (i + 1), fun[1]);
                    if (!(ty >= a1 & ty <= a2)) continue;
                    return min + (max - min) / 10000 * i;
                }
            }
            else if (int.Parse(dtCalibdata.Rows[0][0].ToString()) == 5)//PCT项目
            {
                if (Cal(min, fun[1],"min") > ty) return min;
                if (Cal(max, fun[1],"max") < ty) return max;
                if (ty <= c)
                {
                    return (ty - b) / a;
                }
                else if (ty > c)
                {
                    try
                    {
                        return (Math.Pow(((d - g) / (ty - g) - 1), (1 / e)) * f);
                    }
                    catch (Exception)
                    {
                        return -9;
                    }
                }
            }
            return double.Parse(j.ToString("f" + accurancy));
        }


        /// <summary>
        /// 像素点阵转换为bitmap
        /// </summary>
        /// <param name="rawValues">byte[]数组</param>
        /// <param name="width">图片的宽度</param>
        /// <param name="height">图片的高度</param>
        /// <returns>bitmap图片</returns>
        public static Bitmap ToGrayBitmap(byte[] rawValues, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            //// 获取图像参数  
            //bmpData.Stride = width;
            int stride = bmpData.Stride;  // 扫描线的宽度  
            int offset = stride - width;  // 显示宽度与扫描线宽度的间隙  
            IntPtr iptr = bmpData.Scan0;  // 获取bmpData的内存起始位置  
            int scanBytes = stride * height;// 用stride宽度，表示这是内存区域的大小  
                                            //// 下面把原始的显示大小字节数组转换为内存中实际存放的字节数组  
            int posScan = 0, posReal = 0;// 分别设置两个位置指针，指向源数组和目标数组  
            byte[] pixelValues = new byte[scanBytes];  //为目标数组分配内存  
            for (int x = 0; x < height; x++)
            {
                //// 下面的循环节是模拟行扫描  
                for (int y = 0; y < width; y++)
                {
                    pixelValues[posScan++] = rawValues[posReal++];
                }
                posScan += offset;  //行扫描结束，要将目标位置指针移过那段“间隙”  
            }
            //// 用Marshal的Copy方法，将刚才得到的内存字节数组复制到BitmapData中  
            System.Runtime.InteropServices.Marshal.Copy(pixelValues, 0, iptr, scanBytes);
            bmp.UnlockBits(bmpData);  // 解锁内存区域  
                                      //// 下面的代码是为了修改生成位图的索引表，从伪彩修改为灰度  
            ColorPalette tempPalette;
            using (Bitmap tempBmp = new Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format8bppIndexed))
            {
                tempPalette = tempBmp.Palette;
            }
            for (int i = 0; i < 256; i++)
            {
                tempPalette.Entries[i] = System.Drawing.Color.FromArgb(i, i, i);
            }

            bmp.Palette = tempPalette;

            //// 算法到此结束，返回结果  
            return bmp;
        }


        /// <summary>
        /// 计算胶体金的吸光度值
        /// </summary>
        /// <returns></returns>
        private string CalTy(string seq)
        {
            var dt = SqlData.Selectresultinfo(seq);
            var sampleno = dt.Rows.Count == 0 ? seq : dt.Rows[0][0].ToString(); ;
            Thread.Sleep(1000);
            CCam.CameraPlay();
            // System.Threading.Thread.Sleep(1000);
            var bDataWide = false;
            var w = 0;
            var h = 0;
            CCam.CameraGetImageSize(ref w, ref h);
            var rawStr = new byte[w * h * 2];

           
            CCam.CameraGetImageData(rawStr, ref bDataWide);

            // CCam.CameraSaveTifFile("d:/1111.tif", rawStr, 1272, 1016, false);
            var bit = ToGrayBitmap(rawStr.Where((s, index) => index % 2 == 1).ToArray(), w, h);
            //var bit = new Bitmap(w, h);
            //byte[] pRgb24 = new byte[w * h * 3];
            var data = new int[h, w];
            //C0AB装还为ABC 16bit转12bit
            for (var i = 0; i < w * h; i++)
            {
                data[i / w, i % w] = 4096 - (rawStr[i * 2] >> 4) - (rawStr[i * 2 + 1] << 4);
                //bit.SetPixel(i % w, i / w, Color.FromArgb(rawStr[i * 2 + 1], rawStr[i * 2 + 1], rawStr[i * 2 + 1]));
            }

            //保存图像
            var str = CalMethods.CalCcdod(data);
            var sb = new StringBuilder();

            if (str.IndexOf("-9", StringComparison.Ordinal) > -1)
            {
                var path2 = string.Format("Error/No-{0}-{1:yyyyMMddHHmmss}.jpg", sampleno, DateTime.Now);
                var filename = string.Format("{0}/RawData/{1}", Application.StartupPath, path2);
                bit.Save(filename);
                str = string.Format("{0}||{1}", str, path2);
                return str;
            }

            if (_otherInt[6] == 0)
            {
                //中心校准
                if (_otherInt[7] == 1)
                {
                    str = CalMethods.CalTurn(data, str);
                }

                var midy = str.Substring(str.IndexOf("nstartY(", StringComparison.Ordinal) + 8);
                var midY = int.Parse(midy.Substring(0, midy.IndexOf(")", StringComparison.Ordinal)));

                for (var i = 0; i < w; i++)
                {
                    var datasave = 0;
                    for (var j = 0; j < 160; j++)
                    {
                        datasave += data[midY + j, i];
                    }
                    datasave /= 160;
                    sb.Append(datasave);
                    sb.Append(i == w - 1 ? "" : ",");
                }
                var ee = "";
                if (Math.Abs(midY - 428) > 30 | str.IndexOf("-11") > -1) ee = "Error/";
                var path1 = string.Format("{0}No-{1}-{2:yyyyMMddHHmmss}.csv", ee, sampleno, DateTime.Now);
                var path2 = string.Format("{0}No-{1}-{2:yyyyMMddHHmmss}-Mid-{3}.jpg", ee, sampleno, DateTime.Now, midY);
                var sw = new StreamWriter(string.Format("{0}/RawData/{1}", Application.StartupPath, path1), true,
                    Encoding.ASCII);
                sw.Write(sb);
                sw.Flush();
                sw.Close();
                var filename = string.Format("{0}/RawData/{1}", Application.StartupPath, path2);
                bit.Save(filename, ImageFormat.Jpeg);
                str = string.Format("{0}|{1}|{2}", str, path1, path2);
            }
            else
            {
                var c1X = str.Substring(str.IndexOf("C1", StringComparison.Ordinal) + 3);
                c1X = c1X.Substring(c1X.IndexOf(",", StringComparison.Ordinal) + 1);
                var c1Y = int.Parse(c1X.Substring(0, c1X.IndexOf(")", StringComparison.Ordinal)));
                var c2X = str.Substring(str.IndexOf("C2", StringComparison.Ordinal) + 3);
                c2X = c2X.Substring(c2X.IndexOf(",", StringComparison.Ordinal) + 1);
                var c2Y = int.Parse(c2X.Substring(0, c2X.IndexOf(")", StringComparison.Ordinal)));
                var tx = str.Substring(str.IndexOf("T(", StringComparison.Ordinal) + 2);
                tx = tx.Substring(tx.IndexOf(",", StringComparison.Ordinal) + 1);
                var ty = int.Parse(tx.Substring(0, tx.IndexOf(")", StringComparison.Ordinal)));
                Invoke(new Action(() =>
                {
                    PortLog("Log", "F", string.Format("(C1Y,TY,C2Y)=({0},{1},{2})", c1Y, ty, c2Y));
                }));
                FixLight(c1Y, ty, c2Y);
            }
            return str;
        }

        /// <summary>
        /// 进行一次CMOS数据采集并计算
        /// </summary>
        /// <param name="seq">采集的样本的seq</param>
        private void CcdNewTest(string seq)
        {
            var resultStr = CalTy(seq);
            var path1 = resultStr.Split('|')[1];
            var path2 = resultStr.Split('|')[2];
            resultStr = resultStr.Split('|')[0];
            if (resultStr.Substring(0, 1) == "-")
            {
                var str = resultStr == "-9" ? "CCD计算异常" : "质控线异常" + resultStr.Substring(4);
                Invoke(new Action(() => Log_Add(str + @"^" + seq, false)));
                DrawResult(resultStr == "-9" ? "-9" : "-11", seq, resultStr, "", path2);
                return;
            }
            var resultinfo = SqlData.Selectresultinfo(seq);
            if (resultinfo.Rows.Count <= 0) return;
            var midY = resultStr.Substring(resultStr.IndexOf("nstartY", StringComparison.Ordinal) + 8);
            midY = midY.Substring(0, midY.IndexOf(")", StringComparison.Ordinal));
            if (Math.Abs(int.Parse(midY) - 428) > 30)
            {
                if (_otherInt[1] == 1)
                    DrawResult("-4", seq, "", "", "");
                else if (_otherInt[1] == 0)
                {
                    Invoke(new Action(() => Log_Add("第一次测试位置错误，将重测", true)));
                    serialPort_DataSend(serialPortMain, "#3054");
                }
                return;
            }
            var ty = resultStr.Substring(resultStr.IndexOf("T(", StringComparison.Ordinal) + 2);
            ty = ty.Substring(ty.IndexOf(",", StringComparison.Ordinal) + 1);
            ty = ty.Substring(0, ty.IndexOf(")", StringComparison.Ordinal));

            var Basey = resultStr.Substring(resultStr.IndexOf("Min(", StringComparison.Ordinal) + 4);
            Basey = Basey.Substring(Basey.IndexOf(",") + 1);
            Basey = Basey.Substring(0, Basey.IndexOf(")"));

            var Thit = int.Parse(ty) - int.Parse(Basey);

            var tyFixStr = ConfigRead("CMOSFix");

            var trueThit = double.Parse(ty) * double.Parse(tyFixStr.Split('|')[0]) + double.Parse(tyFixStr.Split('|')[1]);
            Invoke(new Action(() =>
            {
                Log_Add(resultStr + "^" + seq, false);
                labelResult.Text = @"TY:" + ty + @" THit:" + Thit + @" Base:" + Basey;//2017-04-24
                //labelResult.Text = @"TY:" + ty;
            }));
            DrawResult(string.Format("{0}|{1}", ty, Basey), seq, resultStr, path1, path2);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    {
                        comboBox2.Items.Add("#0000:Echo");
                        comboBox2.Items.Add("#0001:上电");
                        comboBox2.Items.Add("#0002$1:机械自检");
                        comboBox2.Items.Add("#0002$2:液路自检");
                        comboBox2.Items.Add("#0002$3:进入测试");
                        comboBox2.Items.Add("#0002$4:进入维护");
                        comboBox2.Items.Add("#0003:查询工作状态");
                        comboBox2.Items.Add("#0004:退出当前状态");
                        comboBox2.Items.Add("#0010:开启片仓");
                        break;
                    }
                case 3:
                    {
                        //comboBox2.Items.Add("#3001:SetTestParamter");
                        comboBox2.Items.Add("#3002:设置片仓");
                        comboBox2.Items.Add("#3003:设置稀释比例");//有对照字典:1=1,2=2,3=5,4=10,5=20,6=50,7=100,8=200,9=500,10=1000,11=2000,12=5000,13=10000,14,15,16...==10000
                        comboBox2.Items.Add("#3004:反应时间");// 单位是秒
                        //comboBox2.Items.Add("#3005:SetReadTime2");// 单位是秒
                        comboBox2.Items.Add("#3006:设置检测头");
                        comboBox2.Items.Add("#3007:设置滴样量");// 单位是uL
                        comboBox2.Items.Add("#3011:查询仓状态");
                        comboBox2.Items.Add("#3020:混匀模式");//混匀暂停
                        comboBox2.Items.Add("#0010:开启片仓");
                        comboBox2.Items.Add("#0004:退出");
                        break;
                    }
                case 4:
                    {
                        comboBox2.Items.Add("#4001:TurnPlateAdjust");
                        comboBox2.Items.Add("#4002:NeedleOnMixSideAdjust");
                        comboBox2.Items.Add("#4003:NeedleOnMixCentreAdjust");
                        comboBox2.Items.Add("#4004:NeedleHeightAdjust");
                        comboBox2.Items.Add("#4005:CardLoadStartAdjust");
                        comboBox2.Items.Add("#4007:CardUnloadStartAdjust");
                        comboBox2.Items.Add("#4009:LiquidPhotoAdjust");
                        comboBox2.Items.Add("#4010:CardStorePhotoAdjust");
                        comboBox2.Items.Add("#4011:SetLampLum");
                        comboBox2.Items.Add("#4012:GetLampLum");
                        comboBox2.Items.Add("#4013:TurnOnLamp");
                        comboBox2.Items.Add("#4014:TurnOffLamp");
                        comboBox2.Items.Add("#4015:AdjustDiluteRatio");
                        comboBox2.Items.Add("#4020:TurnPlateCheck");
                        comboBox2.Items.Add("#4021:NeedleTurnCheck");
                        comboBox2.Items.Add("#4022:NeedleUpdownCheck");
                        comboBox2.Items.Add("#4023:CardStoreMoveCheck");
                        comboBox2.Items.Add("#4024:CardTakeHookCheck");
                        comboBox2.Items.Add("#4025:CardLoadCheck");
                        comboBox2.Items.Add("#4026:CardUnloadCheck");
                        comboBox2.Items.Add("#4029:DiluentPumpCheck");
                        comboBox2.Items.Add("#4030:LeanerPumpCheck");
                        comboBox2.Items.Add("#4031:EffluentPumpCheck");
                        comboBox2.Items.Add("#4032:SampSyringeCheck");
                        comboBox2.Items.Add("#4033:LiquidPhotoCheck");
                        comboBox2.Items.Add("#4034:CardStorePhotoCheck");
                        comboBox2.Items.Add("#4050:DiluentQuantifyTest");
                        comboBox2.Items.Add("#4051:LeanerQuantifyTest");
                        comboBox2.Items.Add("#4052:SampQuantifyTest");

                        comboBox2.Items.Add("#4160:FluoReConnect");
                        comboBox2.Items.Add("#4161:FluoLED1On");
                        comboBox2.Items.Add("#4162:FluoLED1Off");
                        comboBox2.Items.Add("#4163:FluoLED2On");
                        comboBox2.Items.Add("#4164:FluoLED2Off");
                        comboBox2.Items.Add("#4167:LED1Current");
                        comboBox2.Items.Add("#4168:LED2Current");
                        comboBox2.Items.Add("#4169:TEST");
                        comboBox2.Items.Add("#4170:FluoMotorReInit");
                        comboBox2.Items.Add("#4171:FluoMotorCW");
                        comboBox2.Items.Add("#4172:FluoMotorCCW");
                        comboBox2.Items.Add("#4173:FluoMotorStop");
                        comboBox2.Items.Add("#4174 FluoMotorMID");
                        comboBox2.Items.Add("#0004:Quit");
                        break;
                    }
            }
            if (comboBox2.Items.Count > 0)
                comboBox2.SelectedIndex = 0;
        }

        private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sender == dataGridViewMain)
            {
                if (labelOther.Text == GetResxString("Doing", "ColumnText") & e.RowIndex > -1)
                {
                    dataGridViewMain.Rows[e.RowIndex].Selected = true;
                    var seq = dataGridViewMain.Rows[e.RowIndex].Cells[0].Value.ToString();
                    CounterText = string.Format("Del|{0}|{1}", dataGridViewMain.Rows[e.RowIndex].Cells[1].Value, seq);
                    _otherInt[0] = 2;
                    var str = VirtualKeyBoard();
                    if (str == "Cancel") return;
                    if (str == "") { ShowMyAlert("样本号不能为空"); return; }
                    if (str.Length > 20) { ShowMyAlert("样本号长度不能大于20"); return; }
                    var sampleno = str;
                    if (sampleno == "Del|")
                    {
                        if (MessageBox.Show("是否删除样本？", "样本删除确认", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            SqlData.DeleteSampleNo(seq);
                            Invoke(new Action(() => UpdatedataGridViewMain("Doing")));
                            // ReSharper disable once ResourceItemNotResolved
                            var msgLog = Resources.CC02;
                            if (msgLog == null) return;
                            msgLog = msgLog.Replace("[1]", seq);
                            msgLog = msgLog.Replace("[2]", textBoxLoginName.Text);
                            Invoke(new Action(() => Log_Add(msgLog, false, Color.FromArgb(128, 128, 128))));
                        }
                    }
                    else if (sampleno != "")
                    {
                        SqlData.UpdateWorkRunlistSampleNobySeq(seq, sampleno);
                        Invoke(new Action(() => UpdatedataGridViewMain("Doing")));
                        dataGridViewMain.CurrentCell = null;
                        // ReSharper disable once ResourceItemNotResolved
                        var msgLog = Resources.CC01;
                        if (msgLog == null) return;
                        msgLog = msgLog.Replace("[1]", seq);
                        msgLog = msgLog.Replace("[2]", sampleno);
                        msgLog = msgLog.Replace("[3]", textBoxLoginName.Text);
                        Invoke(new Action(() => Log_Add(msgLog, false, Color.FromArgb(128, 128, 128))));
                    }
                    _otherInt[0] = 0;
                }
                else if (labelOther.Text == GetResxString("Done", "ColumnText") & e.RowIndex > -1)
                {
                    contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                    CounterText = e.RowIndex.ToString();
                }
                else if (labelOther.Text == GetResxString("Exception", "ColumnText") & e.RowIndex > -1)
                {
                    var str = "";
                    try
                    {
                        if (e.RowIndex < 0) return; 
                        var rowIndex = e.RowIndex;
                        var createtime = "";
                        if (tabControlMain.SelectedTab == tabPageHome)
                        {
                            createtime = dataGridViewMain.Rows[rowIndex].Cells[0].Value.ToString();
                            str = SqlData.SelectPicNum(createtime).Rows[0][0].ToString();
                            Image img = Image.FromFile(string.Format("{0}/Rawdata/{1}", Application.StartupPath, str));//双引号里是图片的路径
                            pictureBoxResult.Image = img;
                        }
                    }
                    catch (Exception ee)
                    {
                        str = "";
                    }
                    if (str != "")
                    {
                        panelPic.Visible = true;
                    }
                }
            }
            else if (sender == dataGridViewQCSetting)
            {
                if (e.RowIndex > -1)
                {
                    textBoxQCSD.Text = dataGridViewQCSetting[3, e.RowIndex].Value.ToString();
                    textBoxQCTarget.Text = dataGridViewQCSetting[2, e.RowIndex].Value.ToString();
                    textBoxQCSampleNo.Text = dataGridViewQCSetting[1, e.RowIndex].Value.ToString();
                    buttonFixQCData.Visible = true;
                }
            }
            else if (sender == dataGridViewSearch)
            {
                contextMenuStrip1.Show(MousePosition.X, MousePosition.Y);
                CounterText = e.RowIndex.ToString();
            }
            else if (sender == dataGridViewTestItem)
            {
                buttonFixTestItem.Enabled = true;
                if (e.RowIndex > -1)
                {
                    textBoxSettingTestItemName.Text = dataGridViewTestItem.Rows[e.RowIndex].Cells[2].Value.ToString();
                    textBoxSettingAccurancy.Text = dataGridViewTestItem.Rows[e.RowIndex].Cells[6].Value.ToString();
                    textBoxSettingRatio.Text = dataGridViewTestItem.Rows[e.RowIndex].Cells[4].Value.ToString();
                    textBoxSettingUnit.Text = dataGridViewTestItem.Rows[e.RowIndex].Cells[3].Value.ToString();
                    textBoxSettingUnitDefault.Text = dataGridViewTestItem.Rows[e.RowIndex].Cells[7].Value.ToString();
                    textBoxSettingUnitRatio.Text = dataGridViewTestItem.Rows[e.RowIndex].Cells[8].Value.ToString();
                    textBoxSettingWarning.Text = dataGridViewTestItem.Rows[e.RowIndex].Cells[5].Value.ToString();
                    if (textBoxSettingTestItemName.Text == "CRPDIL")
                    {
                        labelPreDilu.Visible = true;
                        textBoxPreDilu.Visible = true;
                        textBoxPreDilu.Text = ConfigRead("PreDilu");
                    }
                    else
                    {
                        labelPreDilu.Visible = false;
                        textBoxPreDilu.Visible = false;
                    }
                }
            }
        }

        //提取荧光结果信息
        private void DrawFluoResult(string cy, string sumcbase, string sumtbase,
            string seq, string odData, string path1, string path2)
        {
            try
            {
                var resultinfo = SqlData.Selectresultinfo(seq);
                if (resultinfo.Rows.Count <= 0) return;
                var sampleNo = resultinfo.Rows[0][0].ToString();
                var createtime = resultinfo.Rows[0][1].ToString();
                var testItemId = resultinfo.Rows[0][2].ToString();
                var calibDataId = resultinfo.Rows[0][3].ToString();
                var reagentStoreId = resultinfo.Rows[0][4].ToString();
                var turnPlateId = resultinfo.Rows[0][5].ToString();
                if (turnPlateId == "")
                    turnPlateId = "0";
                var shelfId = resultinfo.Rows[0][6].ToString();
                if (shelfId == "")
                    shelfId = "0";
                var unit = resultinfo.Rows[0][7].ToString();
                var ratio = resultinfo.Rows[0][8].ToString();
                var unitRatio = resultinfo.Rows[0][9].ToString();
                var minValue = resultinfo.Rows[0][10].ToString();
                var maxValue = resultinfo.Rows[0][11].ToString();
                var tyFixStr = resultinfo.Rows[0][12].ToString();
                var accurancy = resultinfo.Rows[0][13].ToString();
                var testitemname = resultinfo.Rows[0][14].ToString();
                var productid = resultinfo.Rows[0][15].ToString();
                var dtparam = SqlData.SelectFluoCalMethod(seq);
                var value = 0.00;
                var parama = double.Parse(tyFixStr.Split('|')[0]);
                var paramb = double.Parse(tyFixStr.Split('|')[1]);
                switch (dtparam.Rows[0][0].ToString())
                {
                    case "0":
                        value = parama * double.Parse(cy) + paramb;
                        break;

                    case "1":
                        value = (parama * 5000 * double.Parse(sumtbase) + paramb) / (parama * double.Parse(sumtbase) + paramb + parama * double.Parse(sumcbase) + paramb);
                        break;

                    case "2":
                        value = (parama * double.Parse(sumtbase) + paramb) / (parama * double.Parse(sumcbase) + paramb);
                        break;
                }
                var result = 0.0;
                if (dtparam.Rows[0][0].ToString() == "0")
                    result = double.Parse(dtparam.Rows[0][6].ToString()) * value + double.Parse(dtparam.Rows[0][7].ToString());
                else
                    result = value >
                                    Math.Max(double.Parse(dtparam.Rows[0][3].ToString()), double.Parse(dtparam.Rows[0][7].ToString())) ? double.Parse(value > double.Parse(dtparam.Rows[0][4].ToString()) ? maxValue : (Math.Pow((double.Parse(dtparam.Rows[0][4].ToString()) - value) / (value - double.Parse(dtparam.Rows[0][7].ToString())), 1 / double.Parse(dtparam.Rows[0][5].ToString())) * double.Parse(dtparam.Rows[0][6].ToString())).ToString("F" + accurancy)) : double.Parse(((value - double.Parse(dtparam.Rows[0][2].ToString())) / double.Parse(dtparam.Rows[0][1].ToString())).ToString("F" + accurancy));
                result = Math.Min(Math.Max(result, double.Parse(minValue)), double.Parse(maxValue) / double.Parse(ratio));
                var flag = "";
                if (result == double.Parse(minValue)) flag = "<";
                else if (result == double.Parse(maxValue)) flag = ">";
                result = result * double.Parse(ratio) * double.Parse(unitRatio);

                // ReSharper disable once ResourceItemNotResolved
                var msglog = Resources.M0001;
                msglog = msglog?.Replace("[1]", sampleNo);
                msglog += result.ToString(CultureInfo.InvariantCulture) + unit;
                SqlData.InsertIntoNewResult(sampleNo, createtime, testItemId, result.ToString(CultureInfo.InvariantCulture), unit,
                   path1, path2, tyFixStr, calibDataId, reagentStoreId, turnPlateId, shelfId, odData, flag);
                SqlData.DeleteFromWrokrunlist(seq);
                Invoke(new Action(() =>
                {
                    Log_Add(msglog, false, Color.FromArgb(128, 128, 128));
                    if (GetResxString("Done", "ColumnText") == labelOther.Text)
                        UpdatedataGridViewMain("Done");
                    else if (GetResxString("Doing", "ColumnText") == labelOther.Text)
                        UpdatedataGridViewMain("Doing");
                    else if (GetResxString("Exception", "ColumnText") == labelOther.Text)
                        UpdatedataGridViewMain("Exception");
                }));
                if (ConfigRead("AutoPrint") == "1")
                {
                    ResultPrintOut(createtime);
                }
                if (SqlData.WarningValue(createtime))
                    Invoke(new Action(() =>
                    {
                        if (GetResxString("Exception", "ColumnText") != labelOther.Text)
                        {
                            labelExceptionNo.Text = (int.Parse(labelExceptionNo.Text) + 1).ToString();
                            labelExceptionNo.Visible = true;
                        }
                    }
    ));
                if (result >= 0)
                {
                    exportResult(sampleNo, resultinfo.Rows[0][14].ToString(), flag + result.ToString("F" + accurancy), createtime);
                }

                if (testitemname.IndexOf("QC") > -1)
                {
                    SqlData.InsertQCResult(sampleNo, productid, DateTime.Now.ToString("yyyy-MM-dd"));
                }

            }
            catch (Exception ee)
            {
                Log_Add("1796" + ee.ToString(), false);
            }
        }

        //提取胶体金结果信息
        private void DrawResult(string oddata, string seq, string odData, string path1, string path2)
        {
            try
            {
                var ty = oddata.Split('|')[0];
                var resultinfo = SqlData.Selectresultinfo(seq);
                if (resultinfo.Rows.Count == 0) return;
                var sampleNo = resultinfo.Rows[0][0].ToString();
                var createtime = resultinfo.Rows[0][1].ToString();
                var testItemId = resultinfo.Rows[0][2].ToString();
                var calibDataId = resultinfo.Rows[0][3].ToString();
                var reagentStoreId = resultinfo.Rows[0][4].ToString();
                var turnPlateId = resultinfo.Rows[0][5].ToString();
                if (turnPlateId == "")
                    turnPlateId = "0";
                var shelfId = resultinfo.Rows[0][6].ToString();
                if (shelfId == "")
                    shelfId = "0";
                var unit = resultinfo.Rows[0][7].ToString();
                var ratio = resultinfo.Rows[0][8].ToString();
                var unitRatio = resultinfo.Rows[0][9].ToString();
                var minValue = resultinfo.Rows[0][10].ToString();
                var maxValue = resultinfo.Rows[0][11].ToString();
                var tyFixStr = resultinfo.Rows[0][12].ToString();
                var accurancy = resultinfo.Rows[0][13].ToString();
                var testitemname = resultinfo.Rows[0][14].ToString();
                var productid = resultinfo.Rows[0][15].ToString();
                var dtparam = SqlData.SelectFluoCalMethod(seq);
                double result;
                var flag = "";

                if (LocationCcd == -2)
                    result = double.Parse(ty);
                else if (double.Parse(ty) < 0)
                {
                    result = double.Parse(ty);
                    Invoke(new Action(() =>
                    {
                        if (GetResxString("Exception", "ColumnText") != labelOther.Text)
                        {
                            labelExceptionNo.Text = (int.Parse(labelExceptionNo.Text) + 1).ToString();
                            labelExceptionNo.Visible = true;
                        }
                    }
                    ));
                }
                else
                {
                    var value = 0.0;
                    //TY计算
                    if (dtparam.Rows[0][0].ToString() == "0")
                    {
                        value = double.Parse(ty) * double.Parse(tyFixStr.Split('|')[0]) + double.Parse(tyFixStr.Split('|')[1]);
                    }
                    //Thit计算
                    else
                    {
                        var basey = oddata.Split('|')[1];
                        value = double.Parse(ty) - double.Parse(basey);
                        //value = value * double.Parse(tyFixStr.Split('|')[0]) + double.Parse(tyFixStr.Split('|')[1]);
                        value = value * double.Parse(tyFixStr.Split('|')[0]);
                    }

                    result =
                        CalResult(value, calibDataId, double.Parse(minValue) / double.Parse(ratio), double.Parse(maxValue) / double.Parse(ratio),
                            accurancy) *
                        double.Parse(ratio) * double.Parse(unitRatio);
                    if (result / double.Parse(unitRatio) == double.Parse(minValue))
                    {
                        flag = "<";
                    }
                    if (result / double.Parse(unitRatio) == double.Parse(maxValue))
                    {
                        flag = ">";
                    }


                    // ReSharper disable once ResourceItemNotResolved
                    var msglog = Resources.M0001;
                    msglog = msglog?.Replace("[1]", sampleNo);
                    msglog += flag + result.ToString(CultureInfo.InvariantCulture) + unit;
                    Invoke(new Action(() => Log_Add(msglog, false, Color.FromArgb(128, 128, 128))));
                    Log_Add("4", false);

                }

                if (testitemname == "CRPDIL")
                    result = result * double.Parse(ConfigRead("PreDilu")) / 5;

                var flag1 = "";
                var result1 = 0.0;

                if (testitemname == "CRP500")
                {
                    if (ConfigRead("CRPDebug") == "1"&result>0)
                    {
                        if (result == 2.5)
                        {
                            flag1 = "<";
                            result1 = 2.5;
                            result = 5;
                        }
                        else if (result <= 5)
                        {
                            result1 = result;
                            result = 5;
                            flag = "<";
                        }
                        else
                        {
                            flag1 = ">";
                            result1 = 5;
                        }
                        exportResult(sampleNo, "HsCRP", flag1+result1.ToString("F2"), DateTime.Parse(createtime).AddSeconds(1).ToString("yyyy/MM/dd HH:mm:ss"));
                    }
                }


                if (result1 != 0)
                {
                    SqlData.InsertIntoNewResult(sampleNo, DateTime.Parse(createtime).AddSeconds(1).ToString("yyyy/MM/dd HH:mm:ss"), "99", result1.ToString(CultureInfo.InvariantCulture), unit,
path1, path2, tyFixStr, calibDataId, reagentStoreId, turnPlateId, shelfId, odData, flag1);
                }
                SqlData.InsertIntoNewResult(sampleNo, createtime, testItemId, result.ToString(CultureInfo.InvariantCulture), unit,
                    path1, path2, tyFixStr, calibDataId, reagentStoreId, turnPlateId, shelfId, odData, flag);

                SqlData.DeleteFromWrokrunlist(seq);
                Invoke(new Action(() =>
                {
                    if (GetResxString("Done", "ColumnText") == labelOther.Text)
                        UpdatedataGridViewMain("Done");
                    else if (GetResxString("Doing", "ColumnText") == labelOther.Text)
                        UpdatedataGridViewMain("Doing");
                    else
                        UpdatedataGridViewMain("Exception");
                }));
                if (ConfigRead("AutoPrint") == "1")
                {
                    ResultPrintOut(createtime);
                }
                if (SqlData.WarningValue(createtime))
                {
                    Invoke(new Action(() =>
                    {
                        if (GetResxString("Exception", "ColumnText") != labelOther.Text)
                        {
                            Log_Add(labelExceptionNo.Text, false);
                            labelExceptionNo.Text = (int.Parse(labelExceptionNo.Text) + 1).ToString();
                            labelExceptionNo.Visible = true;
                        }
                    }
));
                }

                if (result >= 0)
                {

                        exportResult(sampleNo, resultinfo.Rows[0][14].ToString(), flag + result.ToString("F" + accurancy), createtime);
                    
                }
                else
                {
                    exportResult(sampleNo, resultinfo.Rows[0][14].ToString(), "Error", createtime);
                }

                if (testitemname.IndexOf("QC") > -1)
                {
                    SqlData.InsertQCResult(sampleNo, productid, DateTime.Now.ToString("yyyy-MM-dd"));
                }

            }
            catch (Exception ee)
            {
                Log_Add("1916" + ee.ToString(), false);
            }
        }

        internal static double[] DriveSpaceLeft()
        {
            var driveleft = double.Parse(((double)DriveInfo.GetDrives()[0].AvailableFreeSpace / 1024 / 1024 / 1024).ToString("F2"));
            var driveall = double.Parse(((double)DriveInfo.GetDrives()[0].TotalSize / 1024 / 1024 / 1024).ToString("F2"));
            var rawspace = 0.0;
            var fluospace = 0.0;
            if (Directory.Exists(Application.StartupPath + @"\Rawdata"))
            {
                //定义一个DirectoryInfo对象
                var di = new DirectoryInfo(Application.StartupPath + @"\Rawdata");

                //通过GetFiles方法,获取di目录中的所有文件的大小
                var len = di.GetFiles().Aggregate<FileInfo, double>(0, (current, fi) => current + fi.Length);
                rawspace = double.Parse((len / 1024 / 1024 / 1024).ToString("F2"));
            }
            if (Directory.Exists(Application.StartupPath + @"\fluodata"))
            {
                //定义一个DirectoryInfo对象
                var di = new DirectoryInfo(Application.StartupPath + @"\fluodata");

                //通过GetFiles方法,获取di目录中的所有文件的大小
                var len = di.GetFiles().Aggregate<FileInfo, double>(0, (current, fi) => current + fi.Length);
                fluospace = double.Parse((len / 1024 / 1024 / 1024).ToString("F2"));
            }
            var space = new double[3];
            space[0] = driveleft;
            space[1] = driveall;
            space[2] = rawspace + fluospace;
            return space;
        }

        private List<double> lighttemp = new List<double>();

        private void FixLight(int c1y, int ty, int c2y)
        {
            //光源校准步骤
            //前提建设：Led1对C1的影响程度=Led2对C2的影响程度
            //----------Led1对C2的影响程度=Led2对C1的影响程度
            //----------Led1对T的影响程度 =Led2对T 的影响程度

            //C1=a*Led1+b*Led2+CCC111
            //C2=b*Led1+a*Led1+CCC222
            //T =c*Led1+c*Led2+CCC333
            //差分
            //△C1=a△Led1+b△Led2
            //△C2=b△Led1+a△Led2
            //△T =c△Led1+c△Led2

            //通过两组测试数据可以计算处a，b，c
            //a,b,c已知的情况下，取上一次的测试数据
            //C1+△C1=C2+△C2  T+△T=TYRef 可以计算出△Led1 △Led2 从而可以计算出新的Led1、Led2 
            if (Math.Abs(c2y - c1y) <= 5 & Math.Abs(int.Parse(textBoxCCDRef.Text) - ty) <= 5)
            {
                Log_Add("[F]光源调整成功,当前光亮值为 " + ConfigRead("CCDLed1") + "," + ConfigRead("CCDLed2"), false);
                LocationCcd = 0;
                _otherInt[6] = 0;
                lighttemp = new List<double>();
                return;
            }

            if (lighttemp.Count() == 0)
            {
                var Led1 = int.Parse(ConfigRead("CCDLed1"));
                var Led2 = int.Parse(ConfigRead("CCDLed2"));

                if (ty > 1000 & ty < 3500)
                {
                    lighttemp.Add(Led1);
                    lighttemp.Add(Led2);
                    lighttemp.Add(c1y);
                    lighttemp.Add(ty);
                    lighttemp.Add(c2y);
                    if (ty > 2200)
                    {
                        Led1 += 100;
                        Led2 += 50;
                    }
                    else
                    {
                        Led1 -= 100;
                        Led2 -= 50;
                    }
                }
                else if (ty <= 1000)
                {
                    Led1 -= 100;
                    Led2 -= 100;
                }
                else
                {
                    Led1 += 100;
                    Led2 += 100;
                }
                serialPort_DataSend(serialPortMain, string.Format("#4011$0${0}${1}", Led1.ToString(), Led2.ToString()));
                // ReSharper disable once LocalizableElement
                Log_Add(string.Format(@"[F]光源设定为：({0}, {1})", Led1.ToString(), Led2.ToString()), false);
            }
            else
            {
                if (lighttemp.Count() == 5)
                {
                    if (Math.Abs(c1y - lighttemp[2]) > Math.Abs(c2y - lighttemp[4]))
                    {
                        lighttemp.Add(1);
                    }
                    else
                    {
                        lighttemp.Add(-1);
                    }
                }
                var L12 = double.Parse(ConfigRead("CCDLed1"));
                var L22 = double.Parse(ConfigRead("CCDLed2"));
                //C1|T|C2=a*L1+b*L2

                var sum1 = lighttemp[0] + lighttemp[1];
                var sum2 = L12 + L22;
                var ts = int.Parse(textBoxCCDRef.Text);

                var te3 = (sum2 - sum1) * (ts - lighttemp[3]) / (ty - lighttemp[3]) + sum1-sum2;
                var dc1 = c1y - lighttemp[2];
                var dc2 = c2y - lighttemp[4];
                var dl1 = L12 - lighttemp[0];
                var dl2 = L22 - lighttemp[1];

                var te1 = (dc1 + dc2) / (dl1 + dl2);
                var te2 = (dc1 - dc2) / (dl1 - dl2);

                var a = (te1 + te2) / 2;
                var b = (te1 - te2) / 2;

                var te4 = (c2y - c1y) / (a - b);

                dl1 = (te3 + te4) / 2;
                dl2 = (te3 - te4) / 2;

                var L1 =(int)( L12 + dl1);
                var L2 =(int)( L22 + dl2);

                L1 = Math.Min(1020, Math.Max(0, L1));
                L2 = Math.Min(1020, Math.Max(0, L2));
            
                if (Math.Abs(L1 - L12) + Math.Abs(L2 - L22) == 0)
                {
                    // ReSharper disable once LocalizableElement
                    Log_Add("[F]无法继续调节", true);
                    LocationCcd = 0;
                    _otherInt[6] = 0;
                    lighttemp = new List<double>();
                    return;
                }
                else
                {
                    serialPort_DataSend(serialPortMain, string.Format("#4011$0${0}${1}", L1, L2));
                    // ReSharper disable once LocalizableElement
                    Log_Add(string.Format(@"[F]光源设定为：({0}, {1})", L1, L2), false);
                    lighttemp[0] = L12;
                    lighttemp[1] = L22;
                    lighttemp[2] = c1y;
                    lighttemp[3] = ty;
                    lighttemp[4] = c2y;
                }
            }
        }

        private void FixLight2(int c1Y, int ty, int c2Y)
        {
            var l1A = -0.3015;
            var l2A = -0.3296;

            if (Math.Abs(ty - int.Parse(textBoxCCDRef.Text)) > 5 | Math.Abs(c1Y - c2Y) > 5)
            {
                var l1 = (int)((c1Y - c2Y) * l1A + (ty - int.Parse(textBoxCCDRef.Text)) * 0.1507);
                var l2 = (int)(-(c1Y - c2Y) * l2A + (ty - int.Parse(textBoxCCDRef.Text)) * 0.1422);

                if (ConfigRead("LedReverse") == "1")
                {
                    l1 = (int)(-(c1Y - c2Y) * l1A + (ty - int.Parse(textBoxCCDRef.Text)) * 0.1507);
                    l2 = (int)(+(c1Y - c2Y) * l2A + (ty - int.Parse(textBoxCCDRef.Text)) * 0.1422);

                }

                var LED1 = int.Parse(ConfigRead("CCDLed1"));
                var LED2 = int.Parse(ConfigRead("CCDLed2"));
                var LED11 = Math.Max(0, Math.Min(LED1 + l1, 1020));
                var LED22 = Math.Max(0, Math.Min(LED2 + l2, 1020));
                l1 = LED11 - LED1;
                l2 = LED22 - LED2;
                UpdateAppConfig("CCDLed1", LED11.ToString());
                UpdateAppConfig("CCDLed2", LED22.ToString());
                if (l1 == 0 & l2 == 0)
                {
                    // ReSharper disable once LocalizableElement
                    Log_Add("[F]无法继续调节", true);
                    LocationCcd = 0;
                    _otherInt[6] = 0;
                }
                else
                {
                    serialPort_DataSend(serialPortMain, string.Format("#4011$0${0}${1}", ConfigRead("CCDLed1"), ConfigRead("CCDLed2")));
                    // ReSharper disable once LocalizableElement
                    Log_Add(string.Format(@"[F]光源设定为：({0}, {1})", LED1, LED2), false);
                }
            }
            else
            {
                // ReSharper disable once LocalizableElement
                Log_Add("[F]光源调整成功", false);
                LocationCcd = 0;
                _otherInt[6] = 0;
            }
        }

        private void FluoCmd(string cmdstr)
        {
            if (ConfigRead("FluoEnable") == "0")
            {
                ShowMyAlert(@"荧光端口未打开");
                return;
            }
            var param = cmdstr.Split('$');
            switch (cmdstr.Substring(0, 5))
            {
                case "#4160":
                    FluoConnect(1);
                    serialPort_DataSend(serialPortFluoMotor, "010611");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4161":
                    serialPort_DataSend(serialPortFluo, "000602020001F5");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4162":
                    serialPort_DataSend(serialPortFluo, "000602020000F6");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4163":
                    serialPort_DataSend(serialPortFluo, "000602030001F4");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4164":
                    serialPort_DataSend(serialPortFluo, "000602030000F5");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4167":
                    var param1 = int.Parse(param[1]);
                    param1 = Math.Min(214, Math.Max(param1, 24));
                    // ReSharper disable once LocalizableElement
                    FluoParam(18, param1.ToString());
                    serialPort_DataSend(serialPortFluo, "000602030000F5");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4168":
                    FluoParam(19, cmdstr.Split('$')[1]);
                    serialPort_DataSend(serialPortFluo, "000602030000F5");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4169":
                    FluoNewTest("0");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4170":
                    FluoConnect(2);
                    serialPort_DataSend(serialPortFluoMotor, "010611");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4171":
                    serialPort_DataSend(serialPortFluoMotor, "010601");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4172":
                    serialPort_DataSend(serialPortFluoMotor, "010610");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4173":
                    serialPort_DataSend(serialPortFluoMotor, "010602");
                    Log_Add("[S]" + cmdstr, false);
                    break;

                case "#4174":
                    serialPort_DataSend(serialPortFluoMotor, "010605");
                    Log_Add("[S]" + cmdstr, false);
                    break;
            }
        }

        private void FluoConnect(int type)
        {
            //初始化需要发送的代码
            string[] fluoConnetData2 =
            {
                        ":000300000001FC", ":000300010001FB", ":000300020001FA", ":000300030001F9",
                        ":000300040001F8", ":000300050001F7", ":000300060001F6", ":000300070001F5",
                        ":000300080002F3", ":000300090001F3", ":0003000A0002F1", ":0003000B0001F1",
                        ":0003000C0002EF", ":0003000D0001EF", ":0003000E0002ED", ":0003000F0001ED",
                        ":000300100002EB", ":000300110001EB", ":000300120002E9", ":000300130001E9",
                        ":000300140001E8", ":000300150001E7", ":000300160001E6", ":000300170001E5",
                        ":000300180001E4", ":000300190001E3", ":0003001A0001E2", ":0003001B0001E1",
                        ":0003001C0001E0", ":0003001D0001DF", ":0003001E0001DE", ":0003001F0001DD",
                        ":000300200001DC"
                    };
            if (ConfigRead("FluoEnable") == "0")
            {
                ShowMyAlert(@"荧光端口未打开");
                return;
            }
            foreach (var t in fluoConnetData2)
            {
                serialPort_DataSend(serialPortFluo, t);
            }
            switch (type)
            {
                // ReSharper disable once LocalizableElement
                case 0: Log_Add("FluoConnectOK", false); break;
                // ReSharper disable once LocalizableElement
                case 1: Log_Add("FluoReConnectOK", false); break;
                // ReSharper disable once LocalizableElement
                case 2: Log_Add("FluoMotorReinitOK", false); break;
            }
        }

        //启动荧光测试
        private void FluoNewTest(string seq)
        {
            var fluoEnable = ConfigRead("FluoEnable");
            if (fluoEnable == "0")
            {
                ShowMyAlert("荧光端口未开启");
                return;
            }
            _otherStr[0] = seq;
            var fluoParam = ConfigRead("FluoParam");
            var fluoparam = fluoParam.Split('|');
            var vD = new string[33];
            //反应参数共33个
            vD[0] = int.Parse(fluoparam[0]).ToString("X4");
            vD[1] = int.Parse(fluoparam[1]).ToString("X4");
            vD[7] = int.Parse(fluoparam[7]).ToString("X4");
            vD[20] = int.Parse(fluoparam[14]).ToString("X4");
            vD[21] = int.Parse(fluoparam[15]).ToString("X4");
            vD[22] = int.Parse(fluoparam[16]).ToString("X4");
            vD[23] = int.Parse(fluoparam[17]).ToString("X4");
            vD[32] = int.Parse(fluoparam[26]).ToString("X4");
            vD[2] = int.Parse(fluoparam[2]).ToString("X2") + "00";
            vD[3] = int.Parse(fluoparam[3]).ToString("X2") + "00";
            vD[4] = int.Parse(fluoparam[4]).ToString("X2") + "00";
            vD[5] = int.Parse(fluoparam[5]).ToString("X2") + "00";
            vD[6] = int.Parse(fluoparam[6]).ToString("X2") + "00";
            vD[24] = int.Parse(fluoparam[18]).ToString("X2") + "00";
            vD[25] = int.Parse(fluoparam[19]).ToString("X2") + "00";
            vD[26] = int.Parse(fluoparam[20]).ToString("X2") + "00";
            vD[27] = int.Parse(fluoparam[21]).ToString("X2") + "00";
            vD[28] = int.Parse(fluoparam[22]).ToString("X2") + "00";
            vD[29] = int.Parse(fluoparam[23]).ToString("X2") + "00";
            vD[30] = int.Parse(fluoparam[24]).ToString("X2") + "00";
            vD[31] = int.Parse(fluoparam[25]).ToString("X2") + "00";
            for (var i = 8; i < 14; i++)
            {
                var b = BitConverter.GetBytes(float.Parse(fluoparam[i]));
                var a = b[3].ToString("X2") + b[2].ToString("X2") + b[1].ToString("X2") + b[0].ToString("X2");
                vD[i * 2 - 8] = a.Substring(0, 4);
                vD[i * 2 - 7] = a.Substring(4, 4);
            }
            //写入反应参数
            for (var i = 0; i < 33; i++)
            {
                var strcmd = string.Format("0006{0:X4}{1}{2}", i, vD[i], LeftCheck1(string.Format("0006{0:X4}{1}", i, vD[i])));
                serialPort_DataSend(serialPortFluo, strcmd);
            }
            //写入初始参数
            serialPort_DataSend(serialPortFluo, "000600070000F3");
            serialPort_DataSend(serialPortFluo, "000300000001FC");
            serialPort_DataSend(serialPortFluo, "000301000002FA");
            //执行读取操作
            serialPort_DataSend(serialPortFluoMotor, "010603");
            //serialPort_DataSend(serialPortFluo, "000602000001F7");
            //serialPort_DataSend(serialPortFluo, "000301000002FA");
            //serialPort_DataSend(serialPortFluo, "000301000002FA");
            //serialPort_DataSend(serialPortFluo, "000301000002FA");
            //serialPort_DataSend(serialPortFluo, "000302000001FA");
        }

        private void FluoParam(int index, string param)
        {
            var fluop = ConfigRead("FluoParam").Split('|');
            var param1 = "";
            for (var i = 0; i < index; i++)
            {
                param1 += fluop[i] + "|";
            }
            param1 += param + "|";
            for (var i = index + 1; i < fluop.Length; i++)
            {
                param1 += fluop[i] + "|";
            }
            param1 = param1.Substring(0, param1.Length - 1);
            UpdateAppConfig("FluoParam", param1);
        }

        //都取资源文件中的字符串信息函数
        private string GetResxString(string buttonName, string stringname)
        {
            stringname = _rm.GetString(stringname);
            if (stringname == null) return null;
            if (stringname.IndexOf(string.Format("[{0}]", buttonName)) == -1) return "";
            stringname =
                stringname.Substring(stringname.IndexOf(string.Format("[{0}]", buttonName), StringComparison.Ordinal) + 2 +
                                     buttonName.Length);
            stringname = stringname.Substring(0, stringname.IndexOf("^", StringComparison.Ordinal));
            return stringname;
        }
        private readonly string[] _mEstr = new string[17];
        bool sampleready = true;
        string tempSend = "";
        private string _mEseq = "";
        private string[] MEStatus = { };
        private int seqtemp = -1;
        private void i_Reader_S_Load(object sender, EventArgs e)
        {
            var process = Process.GetProcessesByName("i-Reader S");
            if (process.Count() > 1)
            {
                MessageBox.Show("检测到已经有i-Reader S在运行");
                Close();  
            }

            _mEstr[0] = "<STX>i[ｼｰｹﾝｽ No]00<ETX>";
            _mEstr[1] = "<STX>i[ｼｰｹﾝｽ No]01<ETX>";

            _mEstr[2] = "<STX>a[ｼｰｹﾝｽ No]000000<ETX>";
            _mEstr[3] = "<STX>a[ｼｰｹﾝｽ No]000001<ETX>";
            _mEstr[4] = "<STX>a[ｼｰｹﾝｽ No]000100<ETX>";
            _mEstr[5] = "<STX>a[ｼｰｹﾝｽ No]000101<ETX>";
            _mEstr[6] = "<STX>a[ｼｰｹﾝｽ No]010000<ETX>";
            _mEstr[7] = "<STX>a[ｼｰｹﾝｽ No]010001<ETX>";
            _mEstr[8] = "<STX>a[ｼｰｹﾝｽ No]010100<ETX>";
            _mEstr[9] = "<STX>a[ｼｰｹﾝｽ No]010101<ETX>";

            _mEstr[10] = "<STX>o[ｼｰｹﾝｽ No]00[検体ＩＤ]<ETX>";
            _mEstr[11] = "<STX>o[ｼｰｹﾝｽ No]01[検体ＩＤ]<ETX>";
            _mEstr[12] = "<STX>o[ｼｰｹﾝｽ No]02[検体ＩＤ]<ETX>";
            _mEstr[13] = "<STX>o[ｼｰｹﾝｽ No]03[検体ＩＤ]<ETX>";
            _mEstr[14] = "<STX>o[ｼｰｹﾝｽ No]04[検体ＩＤ]<ETX>";

            _mEstr[15] = "<STX>s[ｼｰｹﾝｽ No]00[検体ＩＤ]<ETX>";
            _mEstr[16] = "<STX>s[ｼｰｹﾝｽ No]01[検体ＩＤ]<ETX>";
            tbtemp = tabPageLogin;
            Size = new Size(1024, 768);
            timerLoad.Start();

            if (File.Exists(Application.StartupPath + "/ExceptionChar.txt"))
            {
                File.Delete(Application.StartupPath + "/ExceptionChar.txt");
            }
        }

        //荧光数据校验位计算
        private string LeftCheck1(string strcmd)
        {
            var checkNum = 0;
            for (var i = 0; i < strcmd.Length; i = i + 2)
            {
                checkNum += Convert.ToInt32(strcmd.Substring(i, 2), 16);
            }
            checkNum = 256 - checkNum & 0x00FF;
            return checkNum.ToString("X2");
        }

        private string LeftCheck(string cmdstr)
        {
            var cmdbyte = Encoding.ASCII.GetBytes(cmdstr);
            int str = cmdbyte.Aggregate(0, (current, t) => current ^ t);
            return str.ToString("X2");
        }

        /// <summary>
        /// 添加信息到日志列表、日志文本框中
        /// </summary>
        /// <param name="logstr">添加的日志信息</param>
        /// <param name="logstropen">添加开机状态的日志信息</param>
        /// <param name="alert">是否报警</param>
        /// &gt;
        /// <param name="logtype">写入日志列表的编号</param>
        /// <param name="rowcolor">日志列表的颜色</param>
        private void Log_Add(string logstr, bool alert, Color rowcolor)
        {
            // ReSharper disable once LocalizableElement
            textBoxLog.Text = string.Format("{0:[yyyy-MM-dd HH:mm:ss.fff]}{1}\r\n{2}", DateTime.Now, logstr, textBoxLog.Text);
            if (tabControlMain.SelectedTab == tabPageLogin)
            {
                var logstropen = logstr.Substring(14);
                if (logstropen.Substring(0, 2) != "湿度")
                {
                    textBoxOpenState.Text = string.Format("{0}\r\n{1}", logstropen, textBoxOpenState.Text);
                }
                if (alert)
                {
                    ShowMyAlert(logstr);
                    textBoxOpenState.Height = 80;
                    textBoxOpenState.ForeColor = Color.Red;
                }
            }
            dataGridViewLog.Rows.Insert(0, 1);
            dataGridViewLog[0, 0].Value = DateTime.Now.ToString("HH:mm:ss");
            dataGridViewLog[1, 0].Value = logstr;
            dataGridViewLog.Rows[0].DefaultCellStyle.ForeColor = rowcolor;
        }

        /// <summary>
        /// 仅添加到日志文本框
        /// </summary>
        /// <param name="logstr"></param>
        /// <param name="alert"></param>
        private void Log_Add(string logstr, bool alert)
        {
            Invoke(new Action(() =>
            {             // ReSharper disable once LocalizableElement
                textBoxLog.Text = string.Format("{0:[yyyy-MM-dd HH:mm:ss.fff]}{1}\r\n{2}", DateTime.Now, logstr, textBoxLog.Text);
                if (alert)
                    ShowMyAlert(logstr);
            }));
            if (textBoxLog.Lines.Count() > 1000)
            {
                button_Click(buttonSaveLog, null);
            }
        }

        private string Mytestitem()
        {
            var mytestitem = new FormTestItem();
            mytestitem.ShowDialog();
            TestItemText = "";
            return FormTestItem.Returnstr;
        }

        // 信息处理模块后的后续处理
        private void OperationAfterDealMsgWithSeq(string msgType, string seq, string param)
        {
            var msgLog = "";
            if (msgType == "*3103")
            {
                if (ConfigRead("ASUEnable") == "0" | tempSend == "")
                {
                    var sampleNo = "";
                    var testItemName = "";
                    Invoke(new Action(() =>
                    {
                        sampleNo = labelNextSampleNo.Text;
                        testItemName = labelNextTestItem.Text;
                    }));
                    var dtTestItem = SqlData.SelectProductIdItemIdByname(testItemName);
                    if (dtTestItem.Rows.Count == 0)
                        Log_Add("不存在此测试项目", true);
                    var testItemId = dtTestItem.Rows[0][1].ToString();
                    var createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var reactionTime = int.Parse(param.Split('$')[2]).ToString();
                    var dilutionRatio = param.Split('$')[0];
                    var reagentStoreId = param.Split('$')[1];
                    // ReSharper disable once ResourceItemNotResolved
                    var workingStatus = Resources.I3103;
                    var tyFixData = ConfigRead(_MParam[0] == 0 ? "FluoFix" : "CMOSFix");
                    var dtCalibDataLotno = SqlData.SelectCalibDataIdLotNoByReagentId(reagentStoreId);
                    var lotNo = dtCalibDataLotno.Rows[0][0].ToString();
                    var calibDataId = dtCalibDataLotno.Rows[0][1].ToString();
                    try
                    {
                        SqlData.InsertIntoRunlist(seq, sampleNo, testItemId, createTime, workingStatus, tyFixData,
        reagentStoreId, dilutionRatio, reactionTime, calibDataId);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    // ReSharper disable once ResourceItemNotResolved
                    msgLog = Resources.NewSampleText;
                    Debug.Assert(msgLog != null, "msgLog != null");
                    msgLog = msgLog.Replace("[1]", sampleNo);
                    msgLog = msgLog.Replace("[2]", testItemName);
                    msgLog = msgLog.Replace("[3]", lotNo);
                    msgLog = msgLog.Replace("[4]", reactionTime);
                    Invoke(new Action(() =>
                    {
                        if (GetResxString("Doing", "ColumnText") == labelOther.Text)
                            UpdatedataGridViewMain("Doing");
                    }));

                    Invoke(new Action(() =>
                    {
                        if (labelNextTestItem.Text.IndexOf("QC", StringComparison.Ordinal) == -1)
                        {
                            int aa;
                            var bb = int.TryParse(labelNextSampleNo.Text, out aa);
                            if (bb)
                                labelNextSampleNo.Text = (aa + 1).ToString().PadLeft(labelNextSampleNo.Text.Length, '0');
                        }
                    }
                        ));
                    UpdateSupplyLeft(1, 1);
                }
                else
                {
                    var sampleNo = tempSend.Substring(7, 20).Replace(" ", "");
                    var testItemName = "";
                    Invoke(new Action(() =>
                    {
                        //sampleNo = labelNextSampleNo.Text;
                        testItemName = labelNextTestItem.Text;
                    }));
                    var dtTestItem = SqlData.SelectProductIdItemIdByname(testItemName);
                    if (dtTestItem.Rows.Count == 0)
                        Log_Add("不存在此测试项目", true);
                    var testItemId = dtTestItem.Rows[0][1].ToString();
                    var createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    var reactionTime = int.Parse(param.Split('$')[2]).ToString();
                    var dilutionRatio = param.Split('$')[0];
                    var reagentStoreId = param.Split('$')[1];
                    // ReSharper disable once ResourceItemNotResolved
                    var workingStatus = Resources.I3103;
                    var tyFixDataId = ConfigRead(_MParam[0] == 0 ? "FluoFix" : "CMOSFix");
                    var dtCalibDataLotno = SqlData.SelectCalibDataIdLotNoByReagentId(reagentStoreId);
                    var lotNo = dtCalibDataLotno.Rows[0][0].ToString();
                    var calibDataId = dtCalibDataLotno.Rows[0][1].ToString();
                    try
                    {
                        var seqdel = SqlData.SelectSeq(sampleNo);
                        SqlData.DeleteFromWrokrunlist(seqdel.Rows[0][0].ToString());
                        createTime = seqdel.Rows[0][1].ToString();
                        SqlData.InsertIntoRunlist(seq, sampleNo, testItemId, createTime, workingStatus, tyFixDataId,
                            reagentStoreId, dilutionRatio, reactionTime, calibDataId);

                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    // ReSharper disable once ResourceItemNotResolved
                    msgLog = Resources.NewSampleText;
                    Debug.Assert(msgLog != null, "msgLog != null");
                    msgLog = msgLog.Replace("[1]", sampleNo);
                    msgLog = msgLog.Replace("[2]", testItemName);
                    msgLog = msgLog.Replace("[3]", lotNo);
                    msgLog = msgLog.Replace("[4]", reactionTime);
                    Invoke(new Action(() =>
                    {
                        if (GetResxString("Doing", "ColumnText") == labelOther.Text)
                            UpdatedataGridViewMain("Doing");
                    }));

                    Invoke(new Action(() =>
                    {
                        if (labelNextTestItem.Text.IndexOf("QC", StringComparison.Ordinal) == -1)
                        {
                            int aa;
                            var bb = int.TryParse(labelNextSampleNo.Text, out aa);
                            if (bb)
                                labelNextSampleNo.Text = (aa + 1).ToString().PadLeft(labelNextSampleNo.Text.Length, '0');
                        }
                    }
                        ));
                }
            }
            //测试出错处理，清洗液不足、稀释液不足、取片失败
            else if (msgType == "!3901" | msgType == "!3903" | msgType == "!3540" | msgType == "!3927" | msgType == "!3928" | msgType == "!3925" | msgType == "!3926")
            {
                var dtSampleNoTime = SqlData.SelectSampleNoTimeBySeq(seq);
                if (dtSampleNoTime.Rows.Count == 0) return;
                var sampleNo = dtSampleNoTime.Rows[0][0].ToString();
                msgLog = _rm.GetString("E" + msgType.Substring(1));
                var ty = "";
                switch (msgType)
                {
                    case "!3901":
                        ty = "-3";
                        break;
                    case "!3903":
                        ty = "-2";
                        break;
                    case "!3540":
                        ty = "-5";
                        break;
                    case "!3925":
                        ty = "-12";
                        break;
                    case "!3926":
                        ty = "-13";
                        break;
                    case "!3927":
                        ty = "-14";
                        break;
                    case "!3928":
                        ty = "-15";
                        break;
                }
                DrawResult(ty, seq, "", "", "");
                Invoke(new Action(() =>
                {
                    if (GetResxString("Exception", "ColumnText") == labelOther.Text)
                        UpdatedataGridViewMain("Exception");
                    if (msgType == "!3901" | msgType == "!3903")
                    {
                        MessageType[1] = "1";
                        Log_Add(sampleNo + msgLog, true);
                    }
                    if (msgType == "!3540")
                        Log_Add(sampleNo + msgLog, true);
                }));
            }
            Invoke(new Action(() => Log_Add(msgType + msgLog, false, Color.FromArgb(128, 128, 128))));
        }

        private void otherControl_TickorClick(object sender, EventArgs e)
        {
            if (sender == toolStripMenuItem1)
            {
                if (PrinterSettings.InstalledPrinters.Count == 0)
                {
                    MessageboxShow("打印机未安装");
                }
                else
                {
                    try
                    {
                        var rowIndex = int.Parse(CounterText);
                        DateTime time;
                        if (tabControlMain.SelectedTab == tabPageHome)
                        {
                            dataGridViewMain.Rows[rowIndex].Selected = true;
                            time = DateTime.Parse(dataGridViewMain.Rows[rowIndex].Cells[0].Value.ToString());
                        }
                        else
                        {
                            dataGridViewSearch.Rows[rowIndex].Selected = true;
                            time = DateTime.Parse(dataGridViewSearch.Rows[rowIndex].Cells[3].Value.ToString());
                        }
                        ResultPrintOut(time.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    catch (Exception)
                    {
                        MessageboxShow("打印机驱动未安装");
                    }
                }
            }
            else if (sender == timerPageUp)
            {
                buttonSubMenu_Click(button2, null);
                //长按时间隔时间不断减小，最少100ms
                timerPageUp.Interval = Math.Max(timerPageUp.Interval - 50, 100);
            }
            else if (sender == timerPageDown)
            {
                buttonSubMenu_Click(button3, null);
                //长按时间隔时间不断减小，最少100ms
                timerPageDown.Interval = Math.Max(timerPageDown.Interval - 50, 100);
            }
            else if (sender == timerMain)
            {
                if (engineerMode > 0)
                    engineerMode--;
                if (tempSend != "" & sampleready == true )
                {
                    timerSampleStart.Start();
                    sampleready = false;
                }

                labelCountDown.Text = Math.Max(0, int.Parse(labelCountDown.Text) - 1).ToString();
                if (labelCountDown.Text == "0")
                {
                    buttonMinOpen.Visible = true;
                }
                labelCountDown.Location = new Point(40 / 2 - labelCountDown.Size.Width / 2, labelCountDown.Location.Y);
                //系统时间更新
                labelSystemTime.Text = DateTime.Now.ToString("yyyy-MM-dd ") + DateTime.Now.ToString("HH:mm:ss");
                //登录后进入测试状态loading过程
                //未知仪器状态5秒查询仪器状态
                // ReSharper disable once ResourceItemNotResolved
                if (labelStep.Text == Resources.LoadingStep1 & labelMeachineStatus.Text == "" & DateTime.Now.Second % 5 == 0)
                {
                    serialPort_DataSend(serialPortMain, "#0003");
                }
                //异常状态需要上电
                else if (_otherInt[2] == 255)
                {
                    serialPort_DataSend(serialPortMain, "#0001");
                }
                //仪器状态已知时分情况处理，若仪器状态为机械自检或液路自检不处理等待，若仪器状态为测试直接进入主界面，若在维护状态退出维护进入空闲状态，若空闲状态进入测试状态。
                else if (labelStep.Text == Resources.LoadingStep1 & labelMeachineStatus.Text != "")
                {
                    if (_otherInt[2] == 0)
                    {
                        serialPort_DataSend(serialPortMain, "#0002$3");
                        // ReSharper disable once ResourceItemNotResolved
                        labelStep.Text = Resources.LoadingStep2;
                        labelStep.Location = new Point(1024 / 2 - labelStep.Size.Width / 2, labelStep.Location.Y);
                    }
                    else if (_otherInt[2] == 3)
                    {
                        // ReSharper disable once ResourceItemNotResolved
                        labelStep.Text = Resources.LoadingStep4;
                        labelStep.Location = new Point(1024 / 2 - labelStep.Size.Width / 2, labelStep.Location.Y);
                    }
                    else if (_otherInt[2] == 4)
                    {
                        serialPort_DataSend(serialPortMain, "#0004");
                    }

                }
                //准备就绪后进入主界面
                else if (labelStep.Text == Resources.LoadingStep4 & tabControlMain.SelectedTab == tabPageLogin)
                {
                    timerLoding.Enabled = false;
                    buttonHome.Visible = true;
                    buttonQC.Visible = true;
                    buttonSearch.Visible = true;
                    buttonSetting.Visible = true;
                    buttonMessage.Visible = true;
                    buttonStop.Visible = true;
                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;
                    labelpage.Visible = true;

                    buttonMenu_Click(buttonHome, null);
                    if (ConfigRead("ASUEnable") == "1")
                    {
                        serialPort_DataSend(serialPortMain, "#3024$0");
                    }
                    var reagentstoreid = SqlData.SelectDefaultReagentStoreId().Rows[0][0].ToString();
                    SwitchTestItem(reagentstoreid);
                    ReagentCloseTime = DateTime.Now.AddSeconds(+10);
                    serialPort_DataSend(serialPortMain, "#3011$0");

                    string str = ConfigRead("SleepTime");
                    var time = int.Parse(str) * 60;
                    serialPort_DataSend(serialPortMain, "#3052$" + time);

                    UpdateReagentStore();

                    UpdateSupplyUi();



                    labelNextTestItem.Text = ConfigRead("TestItem");
                    labelNextSampleNo.Text = ConfigRead("StartSampleNo");
                    var dtrunlist = SqlData.SelectWorkRunlist();
                    for (var i = 0; i < dtrunlist.Rows.Count; i++)
                    {
                        DrawResult("-1", dtrunlist.Rows[i][0].ToString(), "", "", "");
                    }
                }
                //不再登陆界面时，每3小时自动保存一次日志，不断更新反应倒计时
                else if (tabControlMain.SelectedTab != tabPageLogin)
                {
                    if (DateTime.Now.Hour % 4 + DateTime.Now.Minute + DateTime.Now.Second == 0)
                    {
                        button_Click(buttonSaveLog, null);
                        serialPort_DataSend(serialPortMain, "#3060");
                    }
                    else if (DateTime.Now.Hour % 4 == 0 & DateTime.Now.Minute == 1 & DateTime.Now.Second == 0)
                    {
                        serialPort_DataSend(serialPortMain, "#3061");
                    }

                    //每秒钟更新下反应倒计时。
                    if (SqlData.UpdateReactionTime() & labelOther.Text == GetResxString("Doing", "ColumnText"))
                        UpdatedataGridViewMain("Doing");
                }
            }
            else if (sender == textBoxSearchTestitem)
            {
                TestItemText = "SearchResult|";
                var newTestitem = Mytestitem();
                textBoxSearchTestitem.Text = newTestitem;
            }
            else if (sender == panelOther)
            {
                if (buttonHome.Visible == false) return;
                tbtemp =( ConfigRead("Debug") == "1"|engineerMode>0) ? tabPageDetail : tabPageUserMode;
                button1.Text = "";
                button2.Text = "";
                button3.Text = "";
                button4.Text = "";
                labelMenu.Text = "";
                comboBox3.SelectedIndex = 0;
            }
        }

        private string ConfigRead(string str)
        {
            try
            {
                try
                {
                    str = ConfigurationManager.AppSettings[str];
                    return str;
                }
                catch (Exception)
                {

                    if (File.Exists(Application.StartupPath + @"/i-Reader S.exe.config") & File.Exists(Application.StartupPath + @"/configbackup/i-Reader S.exe.config"))
                    {
                        File.Delete(Application.StartupPath + @"/i-Reader S.exe.config");
                        File.Move(Application.StartupPath + @"/configbackup/i-Reader S.exe.config", Application.StartupPath + @"/i-Reader S.exe.config");

                        MessageboxShow("配置文件已损坏，软件将复制备份文件到安装目录，请稍后手动启动软件或重启电脑");
                        Close();
                    }
                    else if (File.Exists(Application.StartupPath + @"/configbackup/i-Reader S.exe.config"))
                    {
                        File.Move(Application.StartupPath + @"/configbackup/i-Reader S.exe.config", Application.StartupPath + @"/i-Reader S.exe.config");
                        MessageboxShow("配置文件不存在，软件将复制备份文件到安装目录，请稍后手动启动软件或重启电脑");
                        Close();
                    }
                    else
                    {
                        MessageboxShow("配置文件已损坏，请检查");
                        Close();
                    }
                    MessageBox.Show("4");

                    return "";
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show("5");

                MessageBox.Show("完全出错" + ee.ToString());
                MessageBox.Show("6");

            }
            return "";
        }

        // 主菜单下控件按下事件
        private void panelReagent_Click(object sender, EventArgs e)
        {
            panelReagent1.Enabled = false;
            panelReagent2.Enabled = false;
            panelReagent3.Enabled = false;
            panelReagent4.Enabled = false;
            panelReagent5.Enabled = false;

            var no = ((Panel)sender).Name.Substring(((Panel)sender).Name.Length - 1);
            serialPort_DataSend(serialPortMain, "#0010$" + no);
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            var x = 0;
            var y = 0;
            var rowGap = 10;
            var font = new Font("新宋体", 10);
            Brush brush = new SolidBrush(Color.Black);
            if (_otherStr[1] != "")
            {
                x = 10;
                var printInfos = _otherStr[1].Split('&');
                e.Graphics.DrawString("------------------------", font, brush, x, y);
                y += rowGap;
                foreach (var t in printInfos)
                {
                    e.Graphics.DrawString(t, font, brush, x, y);
                    y += 18;
                }
            }
            y -= 8;
            e.Graphics.DrawString("------------------------", font, brush, x, y);
        }

        //qc图表显示
        private void QcChartShow(string month, string year, string productid)
        {
            try
            {
                var dt = SqlData.SelectQcResult(month, year, productid);

                chart1.Series.Clear();
                dataGridViewQC1.Rows.Clear();
                dataGridViewQC2.Rows.Clear();
                var productname = SqlData.SelectTestItemNameById(productid);
                if (dt.Rows.Count == 0)
                { labelQCInfo.Text = $"{year}年{month}月{productname} 暂时无质控记录"; return; }
                dataGridViewQC1.Rows.Add(dt.Rows[0][1].ToString(), "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无");
                dataGridViewQC2.Rows.Add(dt.Rows[0][1].ToString(), "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无");

                chart1.Series.Add(dt.Rows[0][1].ToString());
                chart1.Series[0].ChartType = SeriesChartType.Line;
                chart1.Series.Add("1-" + dt.Rows[0][1].ToString());
                chart1.Series[1].ChartType = SeriesChartType.Point;
                chart1.Series[1].IsVisibleInLegend = false;

                if (dt.Rows[dt.Rows.Count - 1][1].ToString() != dt.Rows[0][1].ToString())
                {
                    chart1.Series.Add(dt.Rows[dt.Rows.Count - 1][1].ToString());
                    chart1.Series[2].ChartType = SeriesChartType.Line;
                    chart1.Series.Add("1-" + dt.Rows[dt.Rows.Count - 1][1].ToString());
                    chart1.Series[3].ChartType = SeriesChartType.Point;
                    chart1.Series[3].IsVisibleInLegend = false;

                    dataGridViewQC1.Rows.Add(dt.Rows[dt.Rows.Count - 1][1].ToString(), "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无");
                    dataGridViewQC2.Rows.Add(dt.Rows[dt.Rows.Count - 1][1].ToString(), "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无", "无");

                }

                dataGridViewQC1.Rows.Add("Result", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");
                dataGridViewQC2.Rows.Add("Result", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "");


                dataGridViewQC2.Size = new Size(1019 - 58 * (31 - DateTime.DaysInMonth(int.Parse(year), int.Parse(month))), 110);


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (int.Parse(dt.Rows[i][3].ToString()) < 16)
                    {
                        dataGridViewQC1[int.Parse(dt.Rows[i][3].ToString()), dataGridViewQC1[0, 0].Value.ToString() == dt.Rows[i][1].ToString() ? 0 : 1].Value = double.Parse(dt.Rows[i][0].ToString()).ToString("F2");
                        if (Math.Abs(double.Parse(dt.Rows[i][0].ToString())) > 2)
                            dataGridViewQC1[int.Parse(dt.Rows[i][3].ToString()), dataGridViewQC1[0, 0].Value.ToString() == dt.Rows[i][1].ToString() ? 0 : 1].Style.BackColor = Math.Abs(double.Parse(dt.Rows[i][0].ToString())) > 3 ? Color.Red : Color.Yellow;

                    }
                    else
                    {
                        dataGridViewQC2[int.Parse(dt.Rows[i][3].ToString()) - 15, dataGridViewQC2[0, 0].Value.ToString() == dt.Rows[i][1].ToString() ? 0 : 1].Value = double.Parse(dt.Rows[i][0].ToString()).ToString("F2");
                        if (Math.Abs(double.Parse(dt.Rows[i][0].ToString())) > 2)
                            dataGridViewQC2[int.Parse(dt.Rows[i][3].ToString()) - 15, dataGridViewQC2[0, 0].Value.ToString() == dt.Rows[i][1].ToString() ? 0 : 1].Style.BackColor = Math.Abs(double.Parse(dt.Rows[i][0].ToString())) > 3 ? Color.Red : Color.Yellow;
                    }


                    chart1.Series[dt.Rows[i][1].ToString()].Points.AddXY(int.Parse(dt.Rows[i][3].ToString()), double.Parse(double.Parse(dt.Rows[i][0].ToString()).ToString("F2")));
                    chart1.Series["1-" + dt.Rows[i][1].ToString()].Points.AddXY(int.Parse(dt.Rows[i][3].ToString()), double.Parse(double.Parse(dt.Rows[i][0].ToString()).ToString("F2")));
                    if (Math.Abs(double.Parse(dt.Rows[i][0].ToString())) > 2)
                    {
                        chart1.Series["1-" + dt.Rows[i][1].ToString()].Points[chart1.Series["1-" + dt.Rows[i][1].ToString()].Points.Count - 1].Color = Math.Abs(double.Parse(dt.Rows[i][0].ToString())) > 3 ? Color.Red : Color.Yellow;
                        chart1.Series["1-" + dt.Rows[i][1].ToString()].Points[chart1.Series["1-" + dt.Rows[i][1].ToString()].Points.Count - 1].IsValueShownAsLabel = true;
                        chart1.Series["1-" + dt.Rows[i][1].ToString()].Points[chart1.Series["1-" + dt.Rows[i][1].ToString()].Points.Count - 1].LabelForeColor = Math.Abs(double.Parse(dt.Rows[i][0].ToString())) > 3 ? Color.Red : Color.Yellow;
                    }
                    else
                    {
                        chart1.Series["1-" + dt.Rows[i][1].ToString()].Points[chart1.Series["1-" + dt.Rows[i][1].ToString()].Points.Count - 1].Color = Color.White;
                    }
                }



                var v1 = new List<double>();
                var v2 = new List<double>();

                for (int i = 1; i < DateTime.DaysInMonth(int.Parse(year), int.Parse(month)) + 1; i++)
                {


                    if (i < 16)
                        v1.Add(double.Parse(dataGridViewQC1[i, 0].Value.ToString() != "无" ? dataGridViewQC1[i, 0].Value.ToString() : "0"));
                    else
                        v1.Add(double.Parse(dataGridViewQC2[i - 15, 0].Value.ToString() != "无" ? dataGridViewQC2[i - 15, 0].Value.ToString() : "0"));


                }

                if (dataGridViewQC1.RowCount == 3)
                {
                    for (int i = 1; i < DateTime.DaysInMonth(int.Parse(year), int.Parse(month)) + 1; i++)
                    {
                        if (i < 16)
                            v2.Add(double.Parse(dataGridViewQC1[i, 1].Value.ToString() != "无" ? dataGridViewQC1[i, 1].Value.ToString() : "0"));
                        else
                            v2.Add(double.Parse(dataGridViewQC2[i - 15, 1].Value.ToString() != "无" ? dataGridViewQC2[i - 15, 1].Value.ToString() : "0"));
                    }
                }
                else
                {
                    for (int i = 1; i < DateTime.DaysInMonth(int.Parse(year), int.Parse(month)) + 1; i++)
                    {
                        v2.Add(0);
                    }
                }

                if (ConfigRead("Westgard") == "1")
                {
                    var v3 = new List<string>();
                    for (int i = 0; i < v1.Count; i++)
                    {
                        var result = "OK";
                        //质控分为(-inf,-3),[-3,-2),[-2,2],(2,3],[3,inf)五种状态，双质控为25种可能，依次判断
                        //OK   1种可能
                        if (Math.Abs(v1[i]) <= 2 & Math.Abs(v2[i]) <= 2)
                        {
                            result = "OK";
                        }
                        //1_3S  16种可能
                        else if (Math.Abs(v1[i]) > 3 | Math.Abs(v2[i]) > 3)
                        {
                            result = "1_3S";
                        }
                        //1_2S  两个质控同方向超出   2种可能
                        else if (Math.Abs(v1[i]) > 2 & Math.Abs(v2[i]) > 2 & v1[i] * v2[i] > 0)
                        {
                            result = "2_2S";
                        }
                        //1个超出2S,一个不超出2S
                        else if (Math.Abs(v1[i]) > 2 & Math.Abs(v2[i]) <= 2)
                        {
                            if (i > 0)
                            {
                                if (Math.Abs(v1[i - 1]) > 2 & v1[i] * v1[i - 1] > 0)
                                {
                                    result = "2_2S";
                                }
                                else if (Math.Abs(v1[i - 1]) > 1 & v1[i] * v1[i - 1] > 0 & Math.Abs(v2[i - 1]) > 1 & Math.Abs(v2[i]) > 1 & v2[i] * v1[i] > 0 & v2[i - 1] * v1[i] > 0)
                                {
                                    result = "1_4S";
                                }
                                else
                                {
                                    if (i > 3)
                                    {

                                        if (Math.Abs(v1.Take(i).Reverse().Take(4).Max()) > 1 & Math.Abs(v1.Take(i).Reverse().Take(4).Min()) > 1 & v1.Take(i).Reverse().Take(4).Min() * v1.Take(i).Reverse().Take(4).Max() > 0)
                                        {
                                            result = "4_1S";
                                        }
                                        else if (i > 4)
                                        {
                                            var v11 = v1.Take(i).Reverse().Take(5);
                                            var v21 = v2.Take(i).Reverse().Take(5);
                                            if (v11.Max() * v11.Min() > 0 & v21.Max() * v21.Min() > 0 & v11.Max() * v21.Max() > 0)
                                            {
                                                result = "10_X";
                                            }
                                            else
                                            {
                                                if (i > 9)
                                                {
                                                    var v12 = v1.Take(i).Reverse().Take(10);
                                                    if (v12.Min() * v12.Max() > 0)
                                                        result = "10_X";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (Math.Abs(v1[i]) > 2 & Math.Abs(v2[i]) > 2)
                        {
                            result = "R_4S";
                        }
                        else if (Math.Abs(v1[i]) <= 2 & Math.Abs(v2[i]) > 2)
                        {
                            if (i > 0)
                            {
                                if (Math.Abs(v2[i - 1]) > 2 & v2[i] * v2[i - 1] > 0)
                                {
                                    result = "2_2S";
                                }
                                else if (Math.Abs(v2[i - 1]) > 1 & v2[i] * v2[i - 1] > 0 & Math.Abs(v1[i - 1]) > 1 & Math.Abs(v1[i]) > 1 & v1[i] * v2[i] > 0 & v1[i - 1] * v2[i] > 0)
                                {
                                    result = "1_4S";
                                }
                                else
                                {
                                    if (i > 3)
                                    {

                                        if (Math.Abs(v2.Take(i).Reverse().Take(4).Max()) > 1 & Math.Abs(v2.Take(i).Reverse().Take(4).Min()) > 1 & v2.Take(i).Reverse().Take(4).Min() * v2.Take(i).Reverse().Take(4).Max() > 0)
                                        {
                                            result = "4_1S";
                                        }
                                        else if (i > 4)
                                        {
                                            var v21 = v2.Take(i).Reverse().Take(5);
                                            var v11 = v1.Take(i).Reverse().Take(5);
                                            if (v21.Max() * v21.Min() > 0 & v11.Max() * v11.Min() > 0 & v21.Max() * v11.Max() > 0)
                                            {
                                                result = "10_X";
                                            }
                                            else
                                            {
                                                if (i > 9)
                                                {
                                                    var v22 = v2.Take(i).Reverse().Take(10);
                                                    if (v22.Min() * v22.Max() > 0)
                                                        result = "10_X";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (dataGridViewQC1.RowCount == 2)
                        {
                            if (i < 15)
                            {
                                dataGridViewQC1[i + 1, 1].Value = dataGridViewQC1[i + 1, 0].Value.ToString() == "无" ? "" : result;
                                if (result != "" & result != "OK")
                                    dataGridViewQC1[i + 1, 1].Style.BackColor = Color.Red;
                            }
                            else
                            {
                                dataGridViewQC2[i - 14, 1].Value = dataGridViewQC2[i - 14, 0].Value.ToString() == "无" ? "" : result;
                                if (result != "" & result != "OK")
                                    dataGridViewQC2[i - 14, 1].Style.BackColor = Color.Red;
                            }
                        }
                        else
                        {
                            if (i < 15)
                            {
                                dataGridViewQC1[i + 1, 2].Value = dataGridViewQC1[i + 1, 0].Value.ToString() == "无" & dataGridViewQC1[i, 1].Value.ToString() == "无" ? "" : result;
                                if (result != "" & result != "OK")
                                    dataGridViewQC1[i + 1, 2].Style.BackColor = Color.Red;
                            }
                            else
                            {
                                dataGridViewQC2[i - 14, 2].Value = dataGridViewQC2[i - 14, 0].Value.ToString() == "无" & dataGridViewQC2[i - 14, 1].Value.ToString() == "无" ? "" : result;
                                if (result != "" & result != "OK")
                                    dataGridViewQC2[i - 14, 2].Style.BackColor = Color.Red;
                            }
                        }


                    }
                }
                else
                {
                    for (int i = 0; i < v1.Count; i++)
                    {

                        var result = "OK";

                        if (Math.Abs(v1[i]) <= 2 & Math.Abs(v2[i]) <= 2)
                        {
                            result = "OK";
                        }
                        else if (Math.Abs(v1[i]) <= 3 & Math.Abs(v2[i]) <= 3)
                        {
                            result = "!";
                        }
                        else
                        {
                            result = "No";
                        }

                        if (dataGridViewQC1.RowCount == 2)
                        {
                            if (i < 15)
                            {
                                dataGridViewQC1[i + 1, 1].Value = dataGridViewQC1[i + 1, 0].Value.ToString() == "无" ? "" : result;
                                if (result == "!")
                                    dataGridViewQC1[i + 1, 1].Style.BackColor = Color.Yellow;
                                else if (result == "No")
                                    dataGridViewQC1[i + 1, 1].Style.BackColor = Color.Red;

                            }
                            else
                            {
                                dataGridViewQC2[i - 14, 1].Value = dataGridViewQC2[i - 14, 0].Value.ToString() == "无" ? "" : result;
                                if (result == "!")
                                    dataGridViewQC2[i - 14, 1].Style.BackColor = Color.Yellow;
                                else if (result == "No")
                                    dataGridViewQC2[i - 14, 1].Style.BackColor = Color.Red;
                            }


                        }
                        else
                        {
                            if (i < 15)
                            {
                                dataGridViewQC1[i + 1, 2].Value = dataGridViewQC1[i + 1, 0].Value.ToString() == "无" & dataGridViewQC1[i, 1].Value.ToString() == "无" ? "" : result;
                                if (result == "!")
                                    dataGridViewQC1[i + 1, 2].Style.BackColor = Color.Yellow;
                                else if (result == "No")
                                    dataGridViewQC1[i + 1, 2].Style.BackColor = Color.Red;
                            }
                            else
                            {
                                dataGridViewQC2[i - 14, 2].Value = dataGridViewQC2[i - 14, 0].Value.ToString() == "无" & dataGridViewQC2[i - 14, 1].Value.ToString() == "无" ? "" : result;
                                if (result == "!")
                                    dataGridViewQC2[i - 14, 2].Style.BackColor = Color.Yellow;
                                else if (result == "No")
                                    dataGridViewQC2[i - 14, 2].Style.BackColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                //Log_Add("质控显示出错"+ee.ToString(), true);
            }
        }

        private void QcSettingShow()
        {
            try
            {
                var qcTestItem = SqlData.SelectQcTestItem();
                var qcCount = qcTestItem.Rows.Count;
                if (qcCount > 0)
                {
                    var btns = new Button[qcCount];
                    for (var i = 0; i < qcCount; i++)
                    {
                        btns[i] = new Button
                        {
                            BackColor = Color.FromArgb(40, 40, 40)
                        };
                        btns[i].FlatAppearance.BorderSize = 0;
                        btns[i].FlatStyle = FlatStyle.Flat;
                        btns[i].Font = new Font("微软雅黑", 21.75F, FontStyle.Regular, GraphicsUnit.Point, 134);
                        btns[i].ForeColor = Color.White;
                        btns[i].Location = new Point(30 + 138 * (i % 7), 90 * (i / 7) + 90);
                        btns[i].Name = "buttonQC" + i;
                        btns[i].Size = new Size(120, 80);
                        btns[i].Text = qcTestItem.Rows[i][0].ToString();
                        tabPageQCSetting.Controls.Add(btns[i]);
                        btns[i].Click += buttonQCTestItem_Click;
                    }
                    dataGridViewQCSetting.Location = new Point(30, 90 * (qcCount / 7) + 200);
                    panelQCPoint.Location = new Point(panelQCPoint.Location.X, 90 * (qcCount / 7) + 200);
                    dataGridViewQCSetting.Visible = false;
                    panelQCPoint.Visible = false;
                }
            }
            catch (Exception)
            {
                Log_Add("QC设置出错", true);
            }
        }

        //软件关闭前保存日志,关闭端口
        private void ReaderS_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.SignalToStop();
            videoSourcePlayer1.WaitForStop();
            videoSourcePlayer1.Stop();
            //关闭软件时保存日志
            button_Click(buttonSaveLog, null);
        }

        DateTime ReagentCloseTime = DateTime.Now.AddMinutes(10);

        //收到片仓关闭命令显示片仓操作panel
        private void ReagentClose(string reagentStoreId)
        {
            ReagentCloseTime = DateTime.Now.AddSeconds(5);

            SqlData.UpdateReagentLoadTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),reagentStoreId);

            try
            {
                Invoke(new Action(() =>
                {
                    // ReSharper disable once ResourceItemNotResolved
                    var str = Resources.textBoxQRAlertText;
                    // ReSharper disable once ResourceItemNotResolved
                    if (labelQR.Text == "" | labelQR.Text == Resources.QRError)
                    {
                        tabControlMainRight.SelectedTab = tabPageQRAlert;
                        ReagentStatus = 2;
                        var labelproduct = (Label)(Controls.Find("labelProduct" + reagentStoreId, true)[0]);
                        var labellotno = (Label)(Controls.Find("labelLotNo" + reagentStoreId, true)[0]);
                        if (str == null) return;
                        str = str.Replace("[n]", labelproduct.Text);
                        str = str.Replace("[m]", labellotno.Text);
                        labelQRAlert.Text = str;
                    }
                    else
                    {
                        tabControlMainRight.SelectedTab = tabPageReagent;
                        ReagentStatus = 0;

                    }
                    if (tempSend != "")
                        timerSampleStart.Start();
                    buttonSetFirst.Visible = false;

                }));
            }
            catch (Exception)
            {
                Log_Add("仓门关闭时出错", true);
            }
        }

        //收到片仓打开命令显示片仓操作panel
        private void ReagentOpen(string reagentStoreId)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    labelReagentInfo.Text = "";
                    labelReagentOperation.Text = "";
                    labelQR.Text = "";
                    labelQR2.Text = "";
                    labelQRAlert.Text = "";
                    tabControlMainRight.SelectedTab = tabPageReagentOpen;
                    ReagentStatus = 1;
                    var productname = SqlData.SelectTestItemNameById(SqlData.SelectproductidbyTestItemName1(labelNextTestItem.Text).Rows[0][0].ToString());
                    // ReSharper disable once ResourceItemNotResolved
                    var str = Resources.labelReagentOperationText;
                    str = str.Replace("[n]", reagentStoreId);
                    labelReagentOperation.Text = str;
                    var productname1 = SqlData.SelectReagentinfo().Rows[int.Parse(reagentStoreId) - 1][1].ToString();
                    var lotno1 = SqlData.SelectReagentinfo().Rows[int.Parse(reagentStoreId) - 1][2].ToString();
                    var lotLeft = int.Parse(SqlData.SelectReagentinfo().Rows[int.Parse(reagentStoreId) - 1][3].ToString());

                    if (productname1 != productname)
                        buttonSetFirst.Visible = false;
                    else
                        buttonSetFirst.Visible = true;

                    if (SqlData.SelectReagentStoreLeft(reagentStoreId).Rows[0][0].ToString() == "0")
                        buttonSetFirst.Visible = false;
                    // ReSharper disable once ResourceItemNotResolved
                    str = Resources.labelReagentInfoText;
                    if (str != null)
                    {
                        str = str.Replace("[n]", productname1);
                        str = str.Replace("[m]", lotno1);
                    }
                    labelReagentInfo.Text = str;
                    labelQR.Text = "";
                    if (lotLeft > 0)
                        Log_Add(reagentStoreId + "#片仓有剩余，请注意批号、产品的一致性", true);

                    panelReagent1.Enabled = true;
                    panelReagent2.Enabled = true;
                    panelReagent3.Enabled = true;
                    panelReagent4.Enabled = true;
                    panelReagent5.Enabled = true;

                    var reagentlock = ConfigRead("ReagentLock").Split('-');
                    if (reagentlock.Length == 5)
                    {
                        panelReagent1.Enabled = reagentlock[0] == "0";
                        panelReagent2.Enabled = reagentlock[1] == "0";
                        panelReagent3.Enabled = reagentlock[2] == "0";
                        panelReagent4.Enabled = reagentlock[3] == "0";
                        panelReagent5.Enabled = reagentlock[4] == "0";
                    }

                }));
            }
            catch (Exception)
            {
                Log_Add("仓门打开时出错", true);
            }
        }

        //数据打印工具
        private void ResultPrintOut(string createtime)
        {
            var resultInfo = SqlData.SelectPrintInfo(createtime);
            if (resultInfo.Rows.Count == 0) return;
            var sampleNo = resultInfo.Rows[0][1].ToString();
            var time = DateTime.Parse(createtime);
            var testitem = resultInfo.Rows[0][2].ToString();
            string result = resultInfo.Rows[0][3].ToString() + resultInfo.Rows[0][4].ToString();
            _otherStr[1] = string.Format("{0}{1}&", testitem, sampleNo.PadLeft(24 - testitem.Length));
            _otherStr[1] += time.ToString("yy/MM/dd HH:mm:ss").PadLeft(24) + "&";
            if (_otherInt[4] == 0)
            {
                _otherStr[1] += "Result=" + result;
            }
            else
            {
                var oddata = resultInfo.Rows[0][5].ToString();
                if (oddata.IndexOf("C(", StringComparison.Ordinal) + oddata.IndexOf("C1", StringComparison.Ordinal) == -2) return;
                if (oddata.IndexOf("C1", StringComparison.Ordinal) > -1)
                {
                    var str = oddata.Substring(oddata.IndexOf("Max", StringComparison.Ordinal) + 4);
                    var maxx = str.Substring(0, str.IndexOf(",", StringComparison.Ordinal));
                    str = str.Substring(str.IndexOf(",", StringComparison.Ordinal) + 1);
                    var maxy = str.Substring(0, str.IndexOf(")", StringComparison.Ordinal));
                    str = str.Substring(str.IndexOf("Min", StringComparison.Ordinal) + 4);
                    var minx = str.Substring(0, str.IndexOf(",", StringComparison.Ordinal));
                    str = str.Substring(str.IndexOf(",", StringComparison.Ordinal) + 1);
                    var miny = str.Substring(0, str.IndexOf(")", StringComparison.Ordinal));
                    str = str.Substring(str.IndexOf("C1", StringComparison.Ordinal) + 3);
                    var c1X = str.Substring(0, str.IndexOf(",", StringComparison.Ordinal));
                    str = str.Substring(str.IndexOf(",", StringComparison.Ordinal) + 1);
                    var c1Y = str.Substring(0, str.IndexOf(")", StringComparison.Ordinal));
                    str = str.Substring(str.IndexOf("T", StringComparison.Ordinal) + 2);
                    var tx = str.Substring(0, str.IndexOf(",", StringComparison.Ordinal));
                    str = str.Substring(str.IndexOf(",", StringComparison.Ordinal) + 1);
                    var ty = str.Substring(0, str.IndexOf(")", StringComparison.Ordinal));
                    str = str.Substring(str.IndexOf("C2", StringComparison.Ordinal) + 3);
                    var c2X = str.Substring(0, str.IndexOf(",", StringComparison.Ordinal));
                    str = str.Substring(str.IndexOf(",", StringComparison.Ordinal) + 1);
                    var c2Y = str.Substring(0, str.IndexOf(")", StringComparison.Ordinal));
                    _otherStr[1] += string.Format("{0}{1}x:{2}y:{3}&", "".PadLeft(1), "max:".PadRight(6), maxx.PadRight(9), maxy);
                    _otherStr[1] += string.Format("{0}x:{1}y:{2}&", "base:".PadRight(7), minx.PadRight(9), miny);
                    _otherStr[1] += string.Format("{0}{1}{2}&", "C1".PadLeft(8), "T".PadLeft(6), "C2".PadLeft(8));
                    _otherStr[1] += string.Format("{0}{1}{2}{3}{4}&", "".PadLeft(3), "x:".PadRight(3), c1X.PadRight(7), tx.PadRight(7), c2X);
                    _otherStr[1] += string.Format("{0}{1}{2}{3}{4}&", "".PadLeft(3), "y:".PadRight(3), c1Y.PadRight(7), ty.PadRight(7), c2Y);
                    _otherStr[1] += "high:".PadRight(6) + (int.Parse(c1Y) - int.Parse(miny)).ToString().PadRight(7) + (int.Parse(ty) - int.Parse(miny)).ToString().PadRight(7) + (int.Parse(c2Y) - int.Parse(miny)).ToString().PadRight(7);
                }
                else
                {
                    //"C(%1,%2); T(%3,%4); SumCBase(%5); SumTBase(%6)"
                    var str1 = oddata.Substring(oddata.IndexOf("C(", StringComparison.Ordinal) + 2);
                    var cx = str1.Substring(0, str1.IndexOf(",", StringComparison.Ordinal));
                    str1 = str1.Substring(str1.IndexOf(",", StringComparison.Ordinal) + 1);
                    var cy = str1.Substring(0, str1.IndexOf(")", StringComparison.Ordinal));
                    str1 = str1.Substring(str1.IndexOf("T(", StringComparison.Ordinal) + 2);
                    var tx = str1.Substring(0, str1.IndexOf(",", StringComparison.Ordinal));
                    str1 = str1.Substring(str1.IndexOf(",", StringComparison.Ordinal) + 1);
                    var ty = str1.Substring(0, str1.IndexOf(")", StringComparison.Ordinal));
                    str1 = str1.Substring(str1.IndexOf("e(", StringComparison.Ordinal) + 2);
                    var cb = str1.Substring(0, str1.IndexOf(")", StringComparison.Ordinal));
                    str1 = str1.Substring(str1.IndexOf("e(", StringComparison.Ordinal) + 2);
                    var tb = str1.Substring(0, str1.IndexOf(")", StringComparison.Ordinal));
                    var cb1 = double.Parse(cb);
                    var tb1 = double.Parse(tb);
                    _otherStr[1] += string.Format("{0}{1}&", "C".PadLeft(10), "T".PadLeft(10));
                    _otherStr[1] += string.Format("{0}{1}{2}&", "X".PadLeft(3), cx.PadLeft(8), tx.PadLeft(10));
                    _otherStr[1] += string.Format("{0}{1}{2}&", "Y".PadLeft(3), cy.PadLeft(10), ty.PadLeft(10));
                    _otherStr[1] += string.Format("{0}{1}{2}&", "Ar".PadLeft(3), cb.PadLeft(10), tb.PadLeft(10));
                    _otherStr[1] += string.Format("Tap{0}&", (5000 * tb1 / (tb1 + cb1)).ToString("F2").PadLeft(10));
                    _otherStr[1] += "Tar" + (tb1 / cb1).ToString("F2").PadLeft(10);
                }
            }
            var printDocument = new PrintDocument();
            printDocument.PrintPage += printDocument_PrintPage;
            printDocument.Print();
        }

        private void serialPort_DataMakeUp(SerialPort sr)
        {
            if (sr == serialPortFluo)
            {
                //荧光需要采集的点数从配置文件中读取
                var fluoPointCount = int.Parse(ConfigRead("FluoParam").Split('|')[0]);
                while (_comStr[3].IndexOf(Environment.NewLine, StringComparison.Ordinal) > -1)
                {
                    var str = _comStr[3].Substring(0, _comStr[3].IndexOf(Environment.NewLine, StringComparison.Ordinal));
                    _comStr[3] = _comStr[3].Substring(_comStr[3].IndexOf(Environment.NewLine, StringComparison.Ordinal) + 2);
                    if (str.Length >= 17 & (str.Substring(0, 7) == ":000320" | str.Substring(0, 7) == ":000310") & LeftCheck1(str.Substring(1, str.Length - 3)) == str.Substring(str.Length - 2))
                    {
                        str = str.Substring(7);
                        str = str.Substring(0, str.Length - 2);
                        //对数据字符每8个提取一次作为一组数
                        while (str.Length > 0)
                        {
                            //一个数据的字符
                            var str1 = str.Substring(0, 8);
                            str = str.Substring(8);
                            //将字符依照给定规则转换成数字
                            var dFactor = ((double)Convert.ToInt32(str1, 16)) * 2500 / 0x7fffff;
                            //添加到荧光数据中
                            _fluoData.Add(dFactor);
                        }
                    }
                }
                if (_fluoData.Count == fluoPointCount)
                {
                    Invoke(new Action(() => { chartFluo.Series[0].Points.Clear(); }));
                    var str = "";
                    for (var i = 0; i < _fluoData.Count; i++)
                    {
                        var i1 = i;
                        Invoke(new Action(() => { chartFluo.Series[0].Points.AddXY(i1, _fluoData[i1]); }));
                        str += _fluoData[i] + Environment.NewLine;
                    }
                    var dt = SqlData.Selectresultinfo(_otherStr[0]);
                    var sampleno = dt.Rows.Count == 0 ? _otherStr[0] : dt.Rows[0][0].ToString();
                    str = str.Substring(0, str.Length - 2);
                    var path = string.Format("No-{0}-{1:yyyyMMddHHmmss}.csv", sampleno, DateTime.Now);
                    var path2 = string.Format("No-{0}-{1:yyyyMMddHHmmss}.jpg", sampleno, DateTime.Now);
                    Invoke(new Action(() => { chartFluo.SaveImage(string.Format("{0}/FluoData/{1}.jpg", Application.StartupPath, path2), ChartImageFormat.Jpeg); }));
                    //保存数据
                    var sw = new StreamWriter(string.Format("{0}/FluoData/{1}", Application.StartupPath, path), true, Encoding.ASCII);
                    sw.Write(str);
                    sw.Flush();
                    sw.Close();

                    //提取完毕对数据进行运算，得到荧光OD详细数据
                    var odData = CalMethods.CalFluo(_fluoData);
                    //清空荧光数据
                    _fluoData.Clear();
                    //初始化荧光字符串
                    _comStr[3] = "-1";
                    if (odData.IndexOf("Error", StringComparison.Ordinal) > -1)
                    {
                        Invoke(new Action(() => Log_Add(odData, true)));

                        DrawResult("-10", _otherStr[0], odData, "", "");
                        return;
                    }
                    //提取cx cy tx ty sumtbase sumcbase用于计算
                    var cy = odData.Substring(odData.IndexOf("C(", StringComparison.Ordinal) + 2);
                    var ty = odData.Substring(odData.IndexOf("T(", StringComparison.Ordinal) + 2);
                    var cx = cy.Substring(0, cy.IndexOf(",", StringComparison.Ordinal));
                    var tx = ty.Substring(0, ty.IndexOf(",", StringComparison.Ordinal));
                    var sumCBase =
                        odData.Substring(odData.IndexOf("SumCBase(", StringComparison.Ordinal) + 9);
                    var sumTBase =
                        odData.Substring(odData.IndexOf("SumTBase(", StringComparison.Ordinal) + 9);
                    cy = cy.Substring(cy.IndexOf(",", StringComparison.Ordinal) + 1);
                    ty = ty.Substring(ty.IndexOf(",", StringComparison.Ordinal) + 1);
                    cy = cy.Substring(0, cy.IndexOf(")", StringComparison.Ordinal));
                    ty = ty.Substring(0, ty.IndexOf(")", StringComparison.Ordinal));
                    sumCBase = sumCBase.Substring(0, sumCBase.IndexOf(")", StringComparison.Ordinal));
                    sumTBase = sumTBase.Substring(0, sumTBase.IndexOf(")", StringComparison.Ordinal));
                    Invoke(new Action(() =>
                    {
                        // ReSharper disable once LocalizableElement
                        Log_Add(string.Format(@"{0}^{1}", odData, _otherStr[0]), false);
                        labelResult.Text = string.Format(@"C({0},{1}),T({2},{3});TX-CX={4}", cx, cy, tx, ty, int.Parse(tx) - int.Parse(cx));
                        if (buttonFluoFix.BackColor == Color.LightGray)
                        {
                            if (Math.Abs(int.Parse(cy.Substring(0, cy.Length - 3)) - int.Parse(textBoxFluoRef.Text)) > 20)
                            {
                                //#4167:LED1Current
                                var param = ((int.Parse(textBoxFluoRef.Text) - int.Parse(cy.Substring(0, cy.Length - 3))) / 12 + int.Parse(ConfigRead("FluoParam").Split('|')[18]));
                                param = Math.Min(214, Math.Max(param, 24));
                                FluoCmd("#4167:LED1Current$" + param);
                                Thread.Sleep(100);
                                FluoNewTest("0");
                            }
                            else
                            {
                                buttonFluoFix.BackColor = Color.Transparent;
                            }
                        }
                    }));
                    //进行结果计算与存储
                    DrawFluoResult(cy, sumCBase, sumTBase, _otherStr[0], odData, path, path2);
                }
            }
            else if (sr == serialPortFluoMotor)
            {
                try
                {
                    //荧光电机数据以\r\n结尾
                    while (_comStr[4].IndexOf(Environment.NewLine, StringComparison.Ordinal) > -1)
                    {
                        var str = _comStr[4].Substring(0, _comStr[4].IndexOf(Environment.NewLine, StringComparison.Ordinal));
                        _comStr[4] = _comStr[4].Substring(_comStr[4].IndexOf(Environment.NewLine, StringComparison.Ordinal) + 2);
                        //采集数据完毕时需执行读取数据操作
                        if (str == "FLUOMotorRunOK")
                        {
                            //此时需将荧光数据字符串有-1改成空以便执行读取操作
                            _comStr[3] = "";
                            var fluoPointCount = int.Parse(ConfigRead("FluoParam").Split('|')[0]);
                            //读取的荧光以8个点为一组，命令开头为513，513+16后跟上点数*2 都以x4格式
                            for (var i = 0; i < (fluoPointCount - 1) / 8 + 1; i++)
                            {
                                var strcmd = "0003" + (513 + i * 16).ToString("X4");
                                strcmd += i == (fluoPointCount - 1) / 8
                                    ? ((fluoPointCount - 8 * i) * 2).ToString("X4")
                                    : 16.ToString("X4");
                                //加上校验符号
                                strcmd += LeftCheck1(strcmd);
                                //发送读取命令
                                serialPort_DataSend(serialPortFluo, strcmd);
                            }
                        }
                        else if (str == "FLUOMotorRunRx")
                        {
                            serialPort_DataSend(serialPortFluo, "000602000001F7");
                            serialPort_DataSend(serialPortFluo, "000301000002FA");
                            serialPort_DataSend(serialPortFluo, "000301000002FA");
                            serialPort_DataSend(serialPortFluo, "000301000002FA");
                            serialPort_DataSend(serialPortFluo, "000302000001FA");
                        }
                    }
                }
                catch (Exception ee)
                {
                    Log_Add("3105" + ee.ToString(), false);
                }
            }
        }

        //荧光端口接收
        private void serialPortFluo_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //为-1时，不对采集的数据进行处理

                var currentline = new StringBuilder();
                while (serialPortFluo.BytesToRead > 0)
                {
                    var ch = (char)serialPortFluo.ReadByte();
                    currentline.Append(ch);
                }
                if (currentline.Length == 0) return;
                if (_comStr[3] == "-1")
                {
                }
                else
                //将采集的数据放入荧光数据字符串
                {
                    _comStr[3] += currentline.ToString();
                    serialPort_DataMakeUp(serialPortFluo);
                }
            }
            catch (Exception ee)
            {
                Log_Add("3136" + ee.ToString(), false);
            }
        }

        //荧光电机接收数据
        private void serialPortFluoMotor_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var currentline = new StringBuilder();
                while (serialPortFluoMotor.BytesToRead > 0)
                {
                    var ch = (char)serialPortFluoMotor.ReadByte();
                    currentline.Append(ch);
                }
                if (currentline.Length == 0) return;
                _comStr[4] += currentline.ToString();
                //对荧光点击数据进行整理
                serialPort_DataMakeUp(serialPortFluoMotor);
            }
            catch (Exception ee)
            {
                Log_Add("3158" + ee.ToString(), false);
            }
        }



        //串口数据发送
        private void serialPort_DataSend(SerialPort sr, string strCmd)
        {
            var frontstr = "";
            var backstr = "";
            if (sr == serialPortMain)
            {
                if (strCmd == "") return;
                cmdlist.Add(strCmd);
                return;


            }
            else if (serialPortTH == sr)
            {
                Invoke(new Action(() => PortLog("TH", "S", strCmd)));
            }
            else if (sr == serialPortFluo)
            {
                frontstr = ":";
                backstr = Environment.NewLine;
            }
            else if (sr == serialPortFluoMotor)
            {
                backstr = Environment.NewLine;
            }
            try
            {
                //荧光发送数据的规则是:0003....\r\n  :0006....\r\n
                var writeBuffer = Encoding.ASCII.GetBytes(frontstr + strCmd + backstr);
                sr.Write(writeBuffer, 0, writeBuffer.Length);
                Thread.Sleep(50);
            }
            catch (Exception ee)
            {
                Log_Add("3199" + ee.ToString(), false);
            }
        }

        //主控端口校验
        private string MainPortCheck(string strcmd)
        {
            var check = 0;
            for (int i = 0; i < strcmd.Length; i++)
            {
                check += strcmd[i];
            }
            var a = (char)((check & 0x0F) + 'A');
            var b = (char)(((check & 0xF0) >> 4) + 'A');
            return a + b.ToString();
        }

        //异常字符写入单独的文件
        private void ExeptionChar(string ch)
        {
            var line = "";
            if (!File.Exists(Application.StartupPath + "/ExceptionChar.txt"))
            {
                StreamWriter sw1 = new StreamWriter(Application.StartupPath + "/ExceptionChar.txt");
                sw1.Write("");
                sw1.Close();
            }
            else
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\ExceptionChar.txt");
                line = sr.ReadToEnd();

                sr.Close();
            }
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\ExceptionChar.txt");
            line += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "\t" + ch + "\r\n";
            sw.Write(line);
            sw.Close();
        }

        //主控端口数据的接收
        private void serialPortMain_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                while (serialPortMain.BytesToRead > 0)
                {
                    var ch = (char)serialPortMain.ReadByte();
                    if (ch == '\x02')
                    {
                        _comStr[0] += "<STX>";
                    }
                    else if (ch == '\x03')
                    {
                        _comStr[0] += "<ETX>";
                    }
                    else if (ch == '\x0A')
                    {
                        _comStr[0] += "<LF>";
                    }
                    else if (ch != '\x06' & ch != '\x15' & ch != '\x0d' & (ch < ' ' | ch > '~'))
                    {
                        _comStr[0] += "<" + ((int)ch).ToString("X2") + ">";
                        ExeptionChar("<" + ((int)ch).ToString("X2") + ">");

                    }
                    else if (ch == '\x06')
                    {
                        _comStr[0] += "<ACK>";
                    }
                    else if (ch == '\x15')
                    {
                        ExeptionChar("<NAK>");
                        _comStr[0] += "<NAK>";
                    }
                    else
                    {
                        _comStr[0] += ch.ToString();
                        if (ch == '\r')
                        {

                            while (_comStr[0].IndexOf("\r", StringComparison.Ordinal) > -1)
                            {
                                var strTemp = _comStr[0].Substring(0, _comStr[0].IndexOf("\r", StringComparison.Ordinal));
                                _comStr[0] = _comStr[0].Substring(_comStr[0].IndexOf("\r", StringComparison.Ordinal) + 1);


                                if (strTemp.IndexOf("*", StringComparison.Ordinal) > -1 |
                                    strTemp.IndexOf("!", StringComparison.Ordinal) > -1)
                                {
                                    Invoke(new Action(() => PortLog("Main", "R", strTemp)));
                                    //表示*或者!所处的位置，注意此处考虑同一语句中不可能同时出现* !
                                    var index = strTemp.IndexOf("*", StringComparison.Ordinal) +
                                                strTemp.IndexOf("!", StringComparison.Ordinal) + 1;
                                    //提取* !到结尾的字符
                                    strTemp = strTemp.Substring(index);
                                    //交付信息处理模块进行处理
                                    serialPort_DataDeal(strTemp, "Main");
                                }
                                else if (strTemp.IndexOf("<ACK>") > -1)
                                {
                                    Invoke(new Action(() => PortLog("Main", "R", strTemp.Substring(0, strTemp.IndexOf("<ACK>")))));
                                    Invoke(new Action(() => PortLog("Main", "R", "<ACK>")));
                                    Invoke(new Action(() => PortLog("Main", "R", strTemp.Substring(strTemp.IndexOf("<ACK>") + 5))));
                                    if (cmdlist.Count > 0)
                                        cmdlist.RemoveAt(0);
                                    cmdlist.Insert(0, "\x06");
                                }
                                else if (strTemp.IndexOf("NAK") > -1)
                                {
                                    Invoke(new Action(() => PortLog("Main", "R", strTemp.Substring(0, strTemp.IndexOf("<NAK>")))));
                                    Invoke(new Action(() => PortLog("Main", "R", "<NAK>")));
                                    Invoke(new Action(() => PortLog("Main", "R", strTemp.Substring(strTemp.IndexOf("<NAK>") + 5))));

                                }
                                else
                                {
                                    Invoke(new Action(() => PortLog("Main", "R", strTemp)));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }

        private void PortLog(string portname, string type, string msg)
        {
            if (portname == "FluoMotor") return;
            if (portname == "Fluo") return;
            msg = msg.Replace("\x02", "<STX>");
            msg = msg.Replace("\x03", "<ETX>");
            msg = msg.Replace("\n", "<LF>");
            msg = msg.Replace("\r", "<CR>");
            msg = msg.Replace("<LF>", "");
            msg = msg.Replace("<CR>", "");
            msg = msg.Replace("\x06", "<ACK>");
            msg = msg.Replace("\x15", "<NAK>");
            if (msg.Length == 0) return;
            TextBox tb = (TextBox)(this.Controls.Find("textBox" + portname, true)[0]);
            tb.Text = string.Format("{0:[yyyy-MM-dd HH:mm:ss.fff][}{1}]{2}\r\n{3}", DateTime.Now, type, msg, tb.Text);
        }

        private void exportResult(string sampleno, string testitem, string result, string createtime)
        {
            if (!File.Exists(Application.StartupPath + "/iReader.txt"))
            {
                StreamWriter sw1 = new StreamWriter(Application.StartupPath + "/iReader.txt");
                sw1.Write("");
                sw1.Close();
            }
            StreamReader sr = new StreamReader(Application.StartupPath + "/iReader.txt");
            var str = sr.ReadToEnd();
            str += string.Format("{0},{1},{2},{3}\r\n", sampleno, testitem, result, createtime);
            sr.Close();
            StreamWriter sw = new StreamWriter(Application.StartupPath + "/iReader.txt");
            sw.Write(str);
            sw.Close();
        }

        //二维码端口的接收
        private void serialPortQR_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (serialPortQR.BytesToRead > 0)
            {
                var ch = (char)serialPortQR.ReadByte();
                _comStr[1] += ch.ToString();
                if (ch == '\r')
                {
                    var strTemp = _comStr[1];
                    Invoke(new Action(() => PortLog("QR", "R", strTemp)));
                    _comStr[1] = "";
                    //对一条二维码数据进行处理
                    serialPort_DataDeal(strTemp, "QR");
                }
            }
        }

        //温湿度端口的接收
        private void serialPortTH_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //主串口发送的信息以<STX><ETX>开头结尾
            while (serialPortTH.BytesToRead > 0)
            {
                var ch = (char)serialPortTH.ReadByte();
                _comStr[2] += ch.ToString();
                if (ch == '\x03')
                {
                    var strTemp = _comStr[2].Substring(1, _comStr[2].Length - 2);
                    Invoke(new Action(() => PortLog("TH", "R", strTemp)));
                    _comStr[2] = "";
                    //判断温湿度数据是否合乎规定,1含有*0111，2含有5组参数 湿度，温度，制冷片温度，转盘温度占空比
                    if (strTemp.IndexOf("PumpWorkOK", StringComparison.Ordinal) > -1)
                    {
                        try
                        {
                            var startInfo = new ProcessStartInfo("cmd.exe")
                            {
                                UseShellExecute = false,
                                RedirectStandardInput = true,
                                RedirectStandardOutput = true,
                                RedirectStandardError = true,
                                CreateNoWindow = true
                            };
                            var myProcess = new Process { StartInfo = startInfo };
                            myProcess.Start();
                            myProcess.StandardInput.WriteLine("shutdown -s -t 0");
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show("3351" + ee.ToString());
                        }
                        return;
                    }
                    //2016-08-30 加入制冷片一场
                    else if (strTemp.IndexOf("ColdPieceTepError") > -1)
                    {
                        Invoke(new Action(() => Log_Add("制冷片温度异常，请确认水冷是否正常", true)));
                        labelTH.Text = "除湿系统异常";
                        labelTH.BackColor = Color.Red;
                    }
                    if (strTemp.IndexOf("*0111", StringComparison.Ordinal) < 0) return;

                    strTemp = strTemp.Substring(strTemp.IndexOf("*0111", StringComparison.Ordinal));
                    var msg = strTemp.Split('$');
                    if (msg.Length < 6) return;
                    double result;
                    if (double.TryParse(msg[1], out result) & double.TryParse(msg[2], out result) & double.TryParse(msg[3], out result) & double.TryParse(msg[4], out result) & double.TryParse(msg[5], out result))
                    {
                        serialPort_DataDeal(strTemp, "TH");
                    }
                    else
                    {
                        // ReSharper disable once ResourceItemNotResolved
                        Log_Add(string.Format("[R][TH]{0}:{1}", Resources.D0004, strTemp), true);
                    }
                }
            }
        }
        double InnerTemprature = 0;
        private void serialPort_DataDeal(string strCmd, string type)
        {
            if (type == "TH")
            {
                try
                {
                    var msg = strCmd.Split('$');
                    Invoke(new Action(() =>
                    {
                        // ReSharper disable once ResourceItemNotResolved
                        var msglog = Resources.I0111;
                        // ReSharper disable once PossibleNullReferenceException
                        //湿度
                        msglog = msglog.Replace("[n]", msg[1]);
                        //温度
                        msglog = msglog.Replace("[m]", msg[2]);
                        //内部转盘温度
                        msglog = msglog.Replace("[s]", msg[4]);
                        InnerTemprature = double.Parse(msg[4]);
                        var alertstr = "";
                        var alertval = ConfigRead("THAlert").Split('-');
                        if (double.Parse(msg[4]) > int.Parse(alertval[2]))
                            alertstr += string.Format("仪器内部温度超过{0}℃，当前温度为{1}℃\r\n", int.Parse(alertval[2]), msg[4]);
                        if (double.Parse(msg[2]) > int.Parse(alertval[1]))
                            alertstr += string.Format("片仓温度超过{0}℃，当前温度为{1}℃\r\n", int.Parse(alertval[1]), msg[2]);
                        if (double.Parse(msg[1]) > int.Parse(alertval[0]))
                            alertstr += string.Format("片仓相对湿度超过{0}%，当前湿度为{1}%\r\n", int.Parse(alertval[0]), msg[1]);
                        //2016-11-02 加入显示开启判断，不开启时不报警
                        if (alertstr != "" & ConfigRead("THEnable") == "1")
                            Log_Add(alertstr, true);
                        labelTH.Text = msglog;
                        labelTH.BackColor = Color.Transparent;
                        if (ConfigRead("THEnable") == "1")
                            Log_Add("[R][TH]*0111" + msglog, false, Color.FromArgb(128, 128, 128));
                    }));
                }
                catch (Exception ee)
                {
                    Log_Add("3416" + ee.ToString(), false);
                }
            }
            else if (type == "QR")
            {
                try
                {
                    //去掉多余字符，二维码数据以<CR>结尾
                    strCmd = strCmd.Replace("\r", "");
                    Invoke(new Action(() =>
                    {
                        //添加新项目
                        if (CounterText == "AddNewItem")
                        {
                            var detail = strCmd.Split(',');
                            if (detail.Length == 17)
                            {
                                try
                                {
                                    SqlData.SelectTestItemNameById(detail[0]);
                                }
                                catch (Exception)
                                {
                                    SqlData.InsertNewProductInfo(detail);
                                }
                                try
                                {
                                    SqlData.InsertNewTestItem(detail);
                                }
                                catch (Exception)
                                {
                                    Log_Add("该项目已经存在", true);
                                }
                                CounterText = "";
                            }
                        }
                        //如果此窗口打开，说明有试剂仓打开，此时扫描的应该为定标信息
                        else if (tabControlMainRight.SelectedTab == tabPageReagentOpen)
                        {
                            //二维码长度限制为84
                            if (strCmd.Length == 84)
                            {
                                //对定标信息进行解码
                                Base64Decode(strCmd);
                            }
                            else
                            {
                                //对错误长度的信息进行处理
                                // ReSharper disable once ResourceItemNotResolved
                                labelQR.Text = Resources.QRError;
                            }
                        }
                        //条码
                        else
                        {
                            //此时仓门已经关闭，用户未扫描正确二维码，用户依旧可以扫描二维码
                            if (tabControlMainRight.SelectedTab == tabPageQRAlert)
                            {
                                //二维码长度限制84
                                if (strCmd.Length == 84)
                                {
                                    Base64Decode(strCmd);
                                    Invoke(new Action(() => { tabControlMainRight.SelectedTab = tabPageReagent; labelQR.Text = ""; ReagentStatus = 2; }));
                                }
                                else
                                {
                                    //此时错误通过弹窗提示
                                    // ReSharper disable once ResourceItemNotResolved
                                    Log_Add(Resources.QRError, true);
                                }
                            }
                            //条码的不同情形1、点击下一样本修改样本号  2、点击待测列表修改样本号  3、查找样本号
                            else if (strCmd == "LifeTimeTest:1")
                            {
                                serialPort_DataSend(serialPortMain, "#3333$1");
                                Log_Add("开启老化测试", true);
                            }
                            else if (strCmd == "LifeTimeTest:0")
                            {
                                serialPort_DataSend(serialPortMain, "#3333$0");
                                Log_Add("老化测试关闭", true);
                            }
                            else if (strCmd == "AutoTest:1")
                            {
                                _otherInt[5] = 1;
                                Log_Add("开启自动模式", true);
                            }
                            else if (strCmd == "AutoTest:0")
                            {
                                _otherInt[5] = 0;
                                Log_Add("自动模式关闭", true);
                            }
                            else if (strCmd == "PrintMode:1")
                            {
                                _otherInt[4] = 1;
                                Log_Add("开启吸光度打印模式", true);
                            }
                            else if (strCmd == "PrintMode:0")
                            {
                                _otherInt[4] = 0;
                                Log_Add("开启浓度打印模式", true);
                            }
                            else if (strCmd == "EngineerMode")
                            {
                                engineerMode = 300;
                                Log_Add("开启工程师模式,限时5分钟",true);
                            }
                            else if (CounterText != "")
                            {
                                //限定样本号最大长度为20
                                if (strCmd.Length > 20)
                                {
                                    Log_Add("条码信息不正确", true);
                                    return;
                                }
                                Thread.Sleep(100);
                                var detail = CounterText.Split('|');
                                CounterText = strCmd;
                                _mykeyboard.Close();
                                switch (_otherInt[0])
                                {
                                    //点击下一样本时更改下一样本的值
                                    case 0:
                                        if (labelNextTestItem.Text.IndexOf("QC") == -1)
                                            labelNextSampleNo.Text = strCmd;
                                        break;
                                    //对待测样本的值进行修改
                                    case 2:
                                        var seq = detail[2];
                                        var sampleno = strCmd;
                                        SqlData.UpdateWorkRunlistSampleNobySeq(seq, sampleno);
                                        UpdatedataGridViewMain("Doing");
                                        break;
                                    //搜索样本号
                                    case 1:
                                        textBoxSearchSample.Text = strCmd;
                                        break;
                                }
                                _otherInt[0] = 0;

                                CounterText = "";
                            }
                            //其他时刻修改的样本号为下一样本号
                            else
                            {
                                //限定样本号最大长度为20
                                if (strCmd.Length > 20)
                                {
                                    Log_Add("条码信息不正确", true);
                                    return;
                                }
                                if (labelNextTestItem.Text.IndexOf("QC") == -1)
                                    labelNextSampleNo.Text = strCmd;
                                CounterText = "";
                            }
                        }
                    }));
                }
                catch (Exception ee)
                {
                    Log_Add("3569" + ee.ToString(), false);
                }
            }
            else if (type == "MainSend")
            {
                try
                {
                    var msgtype = strCmd.Substring(1, 4);
                    var detail = strCmd.Split('$');
                    var msglog = _rm.GetString("C" + msgtype);
                    //稀释比例
                    //2016-07-18张志武调试荧光稀释比例添加新比例1：40，第12比例1：5000修改为1：3，第13比例1：10000修改为1：4
                    string[] ratio = { "0", "1", "2", "5", "10", "20", "50", "100", "200", "500", "1000", "2000", "3", "4", "40" };
                    //切换状态
                    if (msgtype == "0002")
                    {
                        //切换前状态 切换后状态都需要显示，显示为状态由前状态切换为后状态
                        Invoke(new Action(() =>
                        {
                            var msginfo = _otherInt[2].ToString();
                            msginfo = msginfo.Replace(" ", "");
                            if (msglog != null)
                            {
                                msglog = msglog.Replace("[1]", _rm.GetString("MeachineStatus" + msginfo));
                                msglog = msglog.Replace("[2]", _rm.GetString("MeachineStatus" + detail[1]));
                            }
                        }));
                    }
                    //退出当前状态命令
                    else if (msgtype == "0004")
                    {
                        Invoke(new Action(() =>
                        {
                            var msginfo = _otherInt[2].ToString();
                            msglog = msglog?.Replace("[n]", _rm.GetString("MeachineStatus" + msginfo));
                        }));
                    }
                    //此类状态由于参数不一，则解释需要变化，比如0005$0恢复 0005$1暂停
                    else if (msgtype == "0005" | msgtype == "3020" | msgtype == "3053")
                        msglog = _rm.GetString(string.Format("C{0}_{1}", msgtype, detail[1]));
                    //此类命令可直接替换参数，比如0010$1表示打开1片仓，直接替换即可。
                    else if (msgtype == "0010" | msgtype == "3002" | msgtype == "3004" | msgtype == "3005" |
                             msgtype == "3006" | msgtype == "3007" | msgtype == "3013" | msgtype == "3014" |
                             msgtype == "3052")
                        msglog = msglog?.Replace("[n]", detail[1]);
                    //更改稀释比例、片仓、检测头、反应时间命令
                    else if (msgtype == "3001")
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        msglog = msglog.Replace("[1]", ratio[int.Parse(detail[1])]);
                        msglog = msglog.Replace("[2]", detail[2]);
                        msglog = msglog.Replace("[3]", detail[3]);
                        msglog = msglog.Replace("[4]", detail[4]);
                        msglog = msglog.Replace("[5]", detail[5]);
                    }
                    //更改稀释比例
                    else if (msgtype == "3003")
                        msglog = msglog?.Replace("[n]", ratio[int.Parse(detail[1])]);
                    //查询片仓状态
                    else if (msgtype == "3011")
                        // ReSharper disable once ResourceItemNotResolved
                        msglog = detail[1] != "0" ? msglog?.Replace("[n]", detail[1]) : Resources.C3011_0;
                    //将命令信息写入日志表
                    Invoke(new Action(() =>
                    {
                        Log_Add(string.Format("[S][Main]#{0}{1}", msgtype, msglog), false, Color.FromArgb(128, 128, 128));
                        // ReSharper disable once LocalizableElement
                    }));
                }
                catch (Exception ee)
                {
                    Log_Add("3640" + ee.ToString(), false);
                }
            }
            else if (type == "Main")
            {
                var msgInfo = strCmd;
                try
                {
                    //处理空格以及换行等字符
                    msgInfo = msgInfo.Replace(" ", "");
                    msgInfo = msgInfo.Replace("\r", "");
                    msgInfo = msgInfo.Replace("\n", "");
                    //信息编号及信息
                    var msgType = msgInfo.Substring(0, 5);
                    var detail = msgInfo.Split('$');
                    var msgLog = "";
                    //无参数的信息,直接显示即可，无需后续操作
                    string[] msgnoparam1 =
                    {
                        "*0199", "*1101", "*2100", "*3153", "*3154", "*3160", "*3161", "*3162", "*3163", "*3190",
                        "*3191", "*3135", "!3501", "!3525", "!3526", "!3527",
                        "*2103", "*2104", "*2107", "*2108", "*2109", "*2113", "*2114", "*2115", "*2116", "*2118",
                        "!2501", "!2502", "!2505", "!2506", "!2510", "!2511", "!2512", "!2513", "!2514", "!2520", "!2521",
                        "!2530", "!2531", "!2532", "!2533", "!2534", "!2535", "!2550", "!2901", "!2902", "!2905", "!2906",
                        "!2907", "!2910", "!2911", "!2912", "!3911", "!3912"
                    };
                    //无参数的信息,需后续操作
                    string[] msgnoparam2 = 
                    { "*3101", "*3102", "*0105", "*0106", "*2101", "*2102", "*3146", "*2120", "!3902" ,"!3904"};
                  
                    //无需特殊处理的含参数信息
                    string[] msgwithparam =
                    {
                        "*3130", "*3131", "*3132", "*3133", "*3134", "*3137", "*3138", "*3139",
                        "*3140", "*3141", "*3152", "*3136","*3106"
                    };
                    //无需特殊处理的含参数错误信息
                    string[] errorwithparam = { "!3520", "!3521", "!3522" };
                    //需处理的带猜数信息
                    string[] msgdealwithparam = { "*3104", "*3107", "*3110", "*3111", "*3112", "*3113" };
                    //不含seq的其他信息
                    string[] msgnoseq = { "*0110", "*0200", "*3120", "*3116" };
                    if (msgnoparam1.Contains(msgType))
                    {
                        //提取转译后的信息
                        msgLog = _rm.GetString((msgType[0] == '*' ? "I" : "E") + msgType.Substring(1));
                    }
                    else if (msgnoparam2.Contains(msgType))
                    {
                        msgLog = _rm.GetString((msgType[0] == '*' ? "I" : "E") + msgType.Substring(1));
                        switch (msgType)
                        {
                            //开机时自动按下以进入工作状态
                            case "*3101":
                                Invoke(new Action(() =>
                                {
                                    // ReSharper disable once ResourceItemNotResolved
                                    if (labelStep.Text != Resources.LoadingStep2) return;
                                    // ReSharper disable once ResourceItemNotResolved
                                    labelStep.Text = _rm.GetString(@"LoadingStep3");
                                    labelStep.Location = new Point(1024 / 2 - labelStep.Size.Width / 2, labelStep.Location.Y);

                                }
                                    ));
                                serialPort_DataSend(serialPortMain, "#3051");
                                break;
                            //开启了自动模式则自动按下此键,也表示开机时准备就绪
                            case "*3102":
                                Invoke(new Action(() =>
                                {
                                    var loadingstep = labelStep.Text;
                                    // ReSharper disable once ResourceItemNotResolved
                                    if (loadingstep != Resources.LoadingStep3)
                                    {
                                        if (_otherInt[5] == 1)
                                            //自动模式下自动按下吸样键
                                            serialPort_DataSend(serialPortMain, "#3051");
                                        else   //ASU下需要判断是否可以吸样
                                            sampleready = true;
                                    }
                                    else
                                    {
                                        labelStep.Text = Resources.LoadingStep4;
                                        labelStep.Location = new Point(1024 / 2 - labelStep.Size.Width / 2, labelStep.Location.Y);
                                    }
                                }));
                                break;
                            //休眠中恢复
                            case "*0106":
                                Invoke(new Action(() =>
                                {
                                    //休眠中禁用其他操作
                                    buttonHome.Visible = false;
                                    buttonQC.Visible = false;
                                    buttonSearch.Visible = false;
                                    buttonMessage.Visible = false;
                                    buttonSetting.Visible = false;
                                    buttonStopConfirm.Visible = false;
                                    buttonStopRecovery.Visible = true;
                                    // ReSharper disable once ResourceItemNotResolved
                                    labelStopStatus.Text = Resources.StopTip2;
                                }));
                                break;
                            //休眠
                            case "*0105":
                                Invoke(new Action(() =>
                                {
                                    //恢复后启用其他操作
                                    buttonHome.Visible = true;
                                    buttonQC.Visible = true;
                                    buttonSearch.Visible = true;
                                    buttonMessage.Visible = true;
                                    buttonSetting.Visible = true;
                                    buttonStopConfirm.Visible = true;
                                    buttonStopRecovery.Visible = false;
                                    // ReSharper disable once ResourceItemNotResolved
                                    labelStopStatus.Text = Resources.StopTip1;
                                }));
                                break;
                            //清洗液灌注完毕，更新清洗液量
                            case "*2101":
                                UpdateSupplyLeft(0, 2);
                                break;
                            //稀释液灌注完毕，更新稀释液量
                            case "*2102":
                                UpdateSupplyLeft(2, 0);
                                break;
                            case "*3146":
                                if (ConfigRead("IDRead") == "1")
                                {
                                    //Thread.Sleep(1000);
                                    idReadcnt = idReadcnt == '0' ? '1' : '2';
                                    Invoke(new Action(() =>
                                    {
                                        Log_Add("", false);

                                        Bitmap bit = videoSourcePlayer1.GetCurrentVideoFrame();
                                        var idcode = IDReader(bit);
                                        bit = new Grayscale(0.2125, 0.7154, 0.0721).Apply(bit);
                                        bit.Save("img/" + (idcode == "Error" ? "Error/" : "") + idcode + "-" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg", ImageFormat.Jpeg);

                                        labelIDRead.Text = idcode;
                                        Log_Add(idcode, false);
                                        if (idcode == "Error" & idReadcnt == '1')
                                        {
                                            serialPort_DataSend(serialPortMain, "#3018");
                                        }
                                        //当出现2次时会出现错误bug 2016-11-04
                                        //else if (idcode != "Error")
                                        else
                                        {
                                            idReadcnt = '0';
                                        }
                                        Log_Add("", false);
                                    }
                                    ));
                                }
                                break;
                            //清洗液灌注失败
                            case "!3902":
                                MessageboxShow("清洗液灌注失败，请更换清洗液\r\n之后点击确定进行灌注", true);
                                Log_Add("清洗液再次灌注",false);
                                serialPort_DataSend(serialPortMain, "#3051");
                                break;
                            //稀释液灌注失败
                            case "!3904":
                                MessageboxShow("稀释液灌注失败，请更换稀释液\r\n之后点击确定进行灌注", true);
                                Log_Add("稀释液再次灌注", false);
                                serialPort_DataSend(serialPortMain, "#3051");
                                break;
                        }
                    }
                    else if (msgwithparam.Contains(msgType) | errorwithparam.Contains(msgType))
                    {
                        var param = detail[1];
                        for (var i = 2; i < detail.Length; i++)
                        {
                            param += "^" + detail[i];
                        }
                        //稀释比例信息在第二个参数上，第一个参数为稀释比例编号
                        if (msgType == "*3136")
                        {
                            param = detail[2];
                        }
                        //仪器返回的信息转换为可理解信息
                        var param1 = param.Split('^');
                        var msg = _rm.GetString((msgType[0] == '*' ? "I" : "E") + msgType.Substring(1));
                        if (msg != null)
                        {
                            var len = msg.Split('[').Length - 1;
                            for (var i = 0; i < len; i++)
                            {
                                msg = msg.Substring(0, msg.IndexOf("[n]", StringComparison.Ordinal)) + param1[i] +
                                      msg.Substring(msg.IndexOf("[n]", StringComparison.Ordinal) + 3);
                            }
                        }
                        msgLog = msg;
                        //休眠时间为0表示不休眠
                        if (msgType == "*3152" & detail[1] == "0")
                        {
                            // ReSharper disable once ResourceItemNotResolved
                            msgLog = Resources.I3152_1;
                        }
                        else if (msgType == "*3140")
                        {
                            _MParam[0] = int.Parse(detail[1]);
                        }
                        else if (msgType == "*3138")
                        {
                            _MParam[2] = int.Parse(detail[1]);
                        }
                        //需要执行后续操作的信息
                        if (msgType == "*3130" | msgType == "*3131" | msgType == "*3137" | msgType == "*3132" |
                            msgType == "*3133" | msgType == "*3134" | msgType == "*3106")
                        //执行后续操作
                        {
                            switch (msgType)
                            {
                                //3105
                                case "*3106":
                                    break;
                                //片仓打开需要提示用于扫码等操作
                                case "*3130": ReagentOpen(detail[1]); break;
                                //片仓关闭后得提示用户是否扫码，未扫码需要确认。
                                case "*3131":
                                    ReagentClose(detail[1]); break;
                                //ReagentOpenNext(detail[1]); break;
                                //当前片仓设定需要更改数据库
                                case "*3137":
                                    SqlData.SetDefaultReagentStoreId(detail[1]);
                                    _MParam[1] = int.Parse(detail[1]);
                                    Invoke(new Action(() => labelReagentNow.Text = "当前片仓：" + detail[1]));
                                    break;
                                //片仓满通知
                                case "*3132":
                                    var leftno = int.Parse(SqlData.SelectReagentinfo().Rows[int.Parse(detail[1]) - 1][3].ToString());
                                    //如果有少量或者空变成了多，则容量设定为25
                                    if (DateTime.Now < ReagentCloseTime)
                                    {
                                        if (leftno < 7)
                                        {
                                            leftno = 25;
                                            SqlData.UpdateReagentLeft(detail[1], leftno);
                                            UpdateReagentStore();
                                        }
                                        //如果当前仓不是再用仓且再用仓数量为0.需要切换此仓为使用仓
                                        if (detail[1] != SqlData.SelectDefaultReagentStoreId().Rows[0][0].ToString() &
                                            SqlData.SelectDefaultReagentStoreId().Rows[0][1].ToString() == "0")
                                        {
                                            var dtreagentStoreId = SqlData.SwitchReagentStore();
                                            if (dtreagentStoreId.Rows.Count > 0)
                                                serialPort_DataSend(serialPortMain, "#3002$" + dtreagentStoreId.Rows[0][0]);
                                        }
                                    }
                                    else
                                    {
                                        leftno--;
                                        leftno = Math.Max(7, leftno);
                                        SqlData.UpdateReagentLeft(detail[1], leftno);
                                        UpdateReagentStore();
                                    }

                                    break;
                                //片仓少通知
                                case "*3133":
                                    var leftno1 = int.Parse(SqlData.SelectReagentinfo().Rows[int.Parse(detail[1]) - 1][3].ToString());
                                    if (DateTime.Now > ReagentCloseTime)
                                    {
                                        leftno1--;
                                        leftno1 = Math.Max(1, Math.Min(6, leftno1));
                                        SqlData.UpdateReagentLeft(detail[1], leftno1);
                                        UpdateReagentStore();
                                    }
                                    else
                                    {
                                        if (leftno1 < 1 | leftno1 > 6)
                                        {
                                            leftno1 = 6;
                                            SqlData.UpdateReagentLeft(detail[1], leftno1);
                                            UpdateReagentStore();
                                        }
                                        if (detail[1] != SqlData.SelectDefaultReagentStoreId().Rows[0][0].ToString() &
                                            SqlData.SelectDefaultReagentStoreId().Rows[0][1].ToString() == "0")
                                        {
                                            var dtreagentStoreId = SqlData.SwitchReagentStore();
                                            if (dtreagentStoreId.Rows.Count > 0)
                                                serialPort_DataSend(serialPortMain, "#3002$" + dtreagentStoreId.Rows[0][0]);
                                        }
                                    }
                                    break;
                                //片仓空通知
                                case "*3134":
                                    var leftno2 = int.Parse(SqlData.SelectReagentinfo().Rows[int.Parse(detail[1]) - 1][3].ToString());
                                    if (leftno2 != 0)
                                    {
                                        leftno2 = 0;
                                        SqlData.UpdateReagentLeft(detail[1], leftno2);
                                        UpdateReagentStore();
                                    }
                                    if (detail[1] == SqlData.SelectDefaultReagentStoreId().Rows[0][0].ToString())
                                    {
                                        var dtreagentStoreId = SqlData.SwitchReagentStore();
                                        if (dtreagentStoreId.Rows.Count <= 0)
                                            //片仓空报警
                                            // ReSharper disable once ResourceItemNotResolved
                                            Invoke(new Action(() => Log_Add(Resources.ReagentEmpty, true)));
                                        else
                                            serialPort_DataSend(serialPortMain, "#3002$" + dtreagentStoreId.Rows[0][0]);
                                    }
                                    break;
                            }
                        }
                    }
                    else if (msgnoseq.Contains(msgType))
                    {
                        switch (msgType)
                        {
                            //状态切换
                            case "*0110":
                                
                                msgInfo = msgInfo.Substring(msgInfo.IndexOf("$", StringComparison.Ordinal) + 1);
                                msgInfo = msgInfo.Substring(0, msgInfo.IndexOf("[", StringComparison.Ordinal));
                                var statusId = msgInfo;
                                msgInfo = _rm.GetString("MeachineStatus" + statusId);
                                _otherInt[2] = int.Parse(statusId);
                                Invoke(new Action(() =>
                                {
                                    labelMeachineStatus.Text = msgInfo;
                                    if (statusId != "255")
                                        comboBox1.SelectedIndex = int.Parse(statusId);
                                }));
                                // ReSharper disable once ResourceItemNotResolved
                                msgLog = Resources.I0110;
                                msgLog += msgInfo;
                                break;
                            //废片仓开关显示
                            case "*0200":
                                // ReSharper disable once ResourceItemNotResolved
                                msgLog = Resources.I0200;
                                msgLog = msgInfo.IndexOf("$253", StringComparison.Ordinal) > -1
                                    ? msgLog?.Substring(msgLog.IndexOf("[253]", StringComparison.Ordinal) + 5)
                                    : msgLog?.Substring(msgLog.IndexOf("[2]", StringComparison.Ordinal) + 3);
                                // ReSharper disable once PossibleNullReferenceException
                                if (msgInfo.IndexOf("$253") == -1)
                                {
                                    Invoke(new Action(() =>
                                    {
                                        MessageboxShow("注意！| 废片仓已经打开，请清空废片仓");
                                        if (ConfigRead("FloatBallEnable") == "1")
                                        {
                                            labelReagentStatus.BackColor = Color.FromArgb(49, 169, 223);
                                            labelSupplyLeft4.Text = "废片仓：√";
                                            if (labelLock.Text != "")
                                            {
                                                if (labelSupplyLeft3.Text.IndexOf("×") == -1 & ConfigRead("FloatBallCount").Split('-')[0] != "0" & ConfigRead("FloatBallCount").Split('-')[1] != "0")
                                                {
                                                    serialPort_DataSend(serialPortMain, "#3053$0");
                                                    labelLock.Text = "";
                                                    Log_Add("仪器解锁，可以继续测试", true);

                                                }
                                            }
                                        }
                                    }));
                                    var supplyLeft = ConfigRead("SupplyLeft").Split('-');
                                    UpdateAppConfig("SupplyLeft",
                                        string.Format("{0}-{1}-{2}-{3}", supplyLeft[0], supplyLeft[1], supplyLeft[2], "0"));

                                    UpdateSupplyUi();
                                }
                                msgLog = msgLog.Substring(0, msgLog.IndexOf("^", StringComparison.Ordinal));
                                break;
                            //测试状态是否busy
                            case "*3120":
                                // ReSharper disable once ResourceItemNotResolved
                                msgLog = Resources.I3120;
                                msgLog = msgInfo.IndexOf("$0", StringComparison.Ordinal) > -1
                                    ? msgLog?.Substring(msgLog.IndexOf("[0]", StringComparison.Ordinal) + 3)
                                    : msgLog?.Substring(msgLog.IndexOf("[1]", StringComparison.Ordinal) + 3);
                                // ReSharper disable once PossibleNullReferenceException
                                msgLog = msgLog.Substring(0, msgLog.IndexOf("^", StringComparison.Ordinal));
                                break;
                            //卡片移出对未测试样本进行处理
                            case "*3116":
                                UpdateSupplyLeft(0, 0);
                                DrawResult("-6", msgInfo.Split('$')[1], "", "", "");
                                break;
                        }
                    }
                    if (msgLog != "")
                    {
                        //更新至日志列表中
                        Invoke(new Action(() =>
                        {
                            Log_Add(string.Format("[R][Main]{0}{1}",
                                        msgType,
                                        msgLog), msgType[0] == '!', Color.FromArgb(128, 128, 128));
                        }));
                        return;
                    }
                    //对含有seq的样本需要特殊处理
                    if (msgdealwithparam.Contains(msgType))
                    {
                        var seq = detail[1];
                        var dtSampleNoTime = SqlData.SelectSampleNoTimeBySeq(seq);
                        if (dtSampleNoTime.Rows.Count <= 0) return;
                        var sampleNo = dtSampleNoTime.Rows[0][0].ToString();
                        var reactionTime = dtSampleNoTime.Rows[0][1].ToString();
                        var info = _rm.GetString("I" + msgType.Substring(1));
                        msgLog = sampleNo + info;
                        //反应中需加入时间以便倒计时操作
                        if (msgType == "*3111")
                            info += ":" + reactionTime;
                        else if (msgType == "*3104")
                        {
                            SampleNormal = true;
                            if (serialPortME.IsOpen)
                            {
                                Invoke(new Action(() => timerSampleReady.Start()));
                            }
                        }
                        SqlData.UpdateWorkStatusBySeq(info, seq);
                        //如果处于待测页面需要刷新界面
                        Invoke(new Action(() =>
                        {
                            if (GetResxString("Doing", "ColumnText") == labelOther.Text)
                                UpdatedataGridViewMain("Doing");
                        }));
                        //2016-03-08 更换了CCD与Fluo的顺序，
                        //测试命令启动ccd测试
                        if (msgType == "*3112")
                        {
                            if (ConfigRead("CCDLocation") == "0")
                            {
                                var ccdEnable = ConfigRead("CCDEnable");
                                if (ccdEnable == "0")
                                    DrawResult("-7", seq, "", "", "");
                                else
                                {
                                    //分辨样本是第一次测试还是第二次测试
                                    if (seq == _otherStr[2])
                                    {
                                        _otherInt[1] = 1;
                                    }
                                    else
                                    {
                                        _otherStr[2] = seq;
                                        _otherInt[1] = 0;
                                    }
                                    var productid = SqlData.SelectProductIDbySeq(seq).Rows[0][0].ToString();
                                    if (productid == "15")
                                        LocationCcd = -1;
                                    else if (LocationCcd == -1)
                                        LocationCcd = 0;
                                    CcdNewTest(seq);
                                }
                                Invoke(new Action(() => Log_Add(msgType + msgLog, false, Color.FromArgb(128, 128, 128))));
                            }
                            else
                            {
                                var fluoEnable = ConfigRead("FluoEnable");
                                if (fluoEnable == "0")
                                    DrawResult("-8", seq, "", "", "");
                                else
                                    FluoNewTest(seq);
                            }
                        }
                        else if (msgType == "*3113")
                        {
                            if (ConfigRead("CCDLocation") == "1")
                            {
                                var ccdEnable = ConfigRead("CCDEnable");
                                if (ccdEnable == "0")
                                    DrawResult("-7", seq, "", "", "");
                                else
                                {
                                    //分辨样本是第一次测试还是第二次测试
                                    if (seq == _otherStr[2])
                                    {
                                        _otherInt[1] = 1;
                                    }
                                    else
                                    {
                                        _otherStr[2] = seq;
                                        _otherInt[1] = 0;
                                    }
                                    var productid = SqlData.SelectProductIDbySeq(seq).Rows[0][0].ToString();
                                    if (productid == "15")
                                        LocationCcd = -1;
                                    else if (LocationCcd == -1)
                                        LocationCcd = 0;
                                    CcdNewTest(seq);
                                }
                                Invoke(new Action(() => Log_Add(msgType + msgLog, false, Color.FromArgb(128, 128, 128))));
                            }
                            else
                            {
                                var fluoEnable = ConfigRead("FluoEnable");
                                if (fluoEnable == "0")
                                    DrawResult("-8", seq, "", "", "");
                                else
                                    FluoNewTest(seq);
                            }
                        }
                    }
                    //新样本测试
                    else if (msgType == "*3103")
                    {
                        var sequence = detail[1];
                        OperationAfterDealMsgWithSeq(msgType, sequence, string.Format("{0}${1}${2}", detail[2], detail[3], detail[5]));
                    }

                    //样本测试过程中错误：液体不足、取片失败等
                    else if (msgType == "!3901" | msgType == "!3903" | msgType == "!3540")
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        var seq1 = detail[1];
                        OperationAfterDealMsgWithSeq(msgType, seq1, "");
                    }

                    else if (msgType == "!3927" | msgType == "!3925" | msgType == "!3926" | msgType == "!3928")
                    {
                        try
                        {
                            SampleNormal = false;
                            Invoke(new Action(() => timerSampleReady.Start()));
                            var sampleNo = tempSend.Substring(7, 20).Replace(" ", "");
                            var seqdel = SqlData.SelectSeq(sampleNo);
                            var seq2 = seqdel.Rows[0][0].ToString();
                            OperationAfterDealMsgWithSeq(msgType, seq2, "");

                        }
                        catch (Exception)
                        {

                        }
                    }
                    //光源调整时对收到亮度指令后进行处理
                    else if (msgType == "*4114")
                    {
                        if (_otherInt[6] == -1)
                        {
                            UpdateAppConfig("CCDLed1", detail[1]);
                            UpdateAppConfig("CCDLed2", detail[2]);

                            //执行采集操作
                            CalTy("0");
                        }
                    }
                    //光耦校准需自动从1-5
                    else if (msgType == "*4111")
                    {
                        if (ReagentSensorCheckStr != "")
                        {
                            serialPort_DataSend(serialPortMain, ReagentSensorCheckStr.Substring(0, 7));
                            ReagentSensorCheckStr = ReagentSensorCheckStr.Substring(8);
                            Log_Add(string.Format("#{0}片仓光耦校准完成,开始校准下一片仓光耦", detail[1]), false);
                        }
                        else if (ReagentSensorCheck)
                        {
                            ReagentSensorCheck = false;
                            Log_Add("片仓光耦校准完成", true);
                        }
                        else
                        {
                            Log_Add(string.Format("#{0}片仓光耦校准完成", detail[1]), false);
                        }
                    }
                    else if (msgType == "*4129" | msgType == "*4130")
                    {
                        if (LiquidSensorCheckCMDS != "")
                        {
                            var txt = LiquidSensorCheckCMDS.Substring(0, LiquidSensorCheckCMDS.IndexOf("|"));
                            LiquidSensorCheckCMDS = LiquidSensorCheckCMDS.Substring(LiquidSensorCheckCMDS.IndexOf("|") + 1);
                            serialPort_DataSend(serialPortMain, txt);
                            Log_Add(string.Format("本次{0}泵检查完成，执行下一操作", msgType == "*4130" ? "清洗液" : "稀释液"), false);
                        }
                        else if (LiquidSensorCheck)
                        {
                            LiquidSensorCheck = false;
                            Log_Add("液路光耦校准完成", true);
                            Invoke(new Action(() => buttonLiquidSensor.Text = "液路光耦校准"));
                        }
                        else
                        {
                            Log_Add((msgType == "*4130" ? "清洗液" : "稀释液") + "泵检查完成", false);
                        }
                    }
                    else if (msgType == "!4929" | msgType == "!4930")
                    {
                        if (LiquidSensorCheck)
                        {
                            LiquidSensorCheck = false;
                            LiquidSensorCheckCMDS = "";
                            Log_Add((msgType == "!4930" ? "清洗液" : "稀释液") + "泵检查失败，液路光耦校准中断", true);
                            Invoke(new Action(() => buttonLiquidSensor.Text = "液路光耦校准"));
                        }
                        else
                        {
                            Log_Add((msgType == "!4930" ? "清洗液" : "稀释液") + "泵检查失败", true);
                        }
                    }
                    else if (msgType == "*4109")
                    {
                        var value = double.Parse(detail[3].Substring(0, detail[3].IndexOf("Vol")));
                        var txt1 = "";
                        if (detail[1][1] == '0')
                            txt1 = "稀释液";
                        else if (detail[1][1] == '1')
                        {
                            txt1 = "清洗液";
                        }
                        if (LiquidSensorCheckCMDS != "")
                        {
                            if (value > 230 | value < 220)
                            {
                                LiquidSensorCheck = false;
                                LiquidSensorCheckCMDS = "";
                                Log_Add(txt1 + "光耦校准异常，液路光耦校准中断", true);
                                Invoke(new Action(() => buttonLiquidSensor.Text = "液路光耦校准"));
                            }
                            else if (LiquidSensorCheckCMDS.Substring(0, 5) == "#4009")
                            {
                                var txt = LiquidSensorCheckCMDS.Substring(0, LiquidSensorCheckCMDS.IndexOf("|"));
                                LiquidSensorCheckCMDS = LiquidSensorCheckCMDS.Substring(LiquidSensorCheckCMDS.IndexOf("|") + 1);
                                serialPort_DataSend(serialPortMain, txt);
                                Log_Add(txt1 + "光耦校准完成，执行下一操作", false);
                            }
                            else
                            {
                                Invoke(new Action(() => buttonLiquidSensor.Text = "继续液路光耦校准"));
                                Log_Add("光耦校准完成，请将清洗液、稀释液管移到液面以上,之后点击继续液路光耦校准", true);
                            }
                        }
                        else
                        {
                            if (value > 230 | value < 220)
                            {
                                Log_Add(txt1 + "光耦校准异常", true);
                            }
                            else
                            {
                                Log_Add(txt1 + "光耦校准完成", false);
                            }
                        }
                    }
                    else if (msgType == "*4133")
                    {
                        var value1 = double.Parse(detail[1]);
                        var value2 = double.Parse(detail[2]);
                        if (LiquidSensorCheckCMDS != "")
                        {
                            if (value1 > 120 | value2 > 120)
                            {
                                LiquidSensorCheck = false;
                                LiquidSensorCheckCMDS = "";
                                Invoke(new Action(() => buttonLiquidSensor.Text = "液路光耦校准"));
                                Log_Add("清洗液/稀释液光耦检查值异常，液路光耦校准中断", true);
                            }
                            else
                            {
                                var txt = LiquidSensorCheckCMDS.Substring(0, LiquidSensorCheckCMDS.IndexOf("|"));
                                LiquidSensorCheckCMDS = LiquidSensorCheckCMDS.Substring(LiquidSensorCheckCMDS.IndexOf("|") + 1);
                                serialPort_DataSend(serialPortMain, txt);
                                Log_Add("本次清洗液/稀释液光耦检查完成，执行下一操作", false);
                            }
                        }
                        else
                        {
                            if (value1 > 120 | value2 > 120)
                            {
                                Log_Add("清洗液/稀释液光耦检查值异常", true);
                            }
                            else
                            {
                                Log_Add("清洗液/稀释液光耦检查完成", false);
                            }
                        }
                    }
                    else if (msgType == "*3157")
                    {
                        Invoke(new Action(() => labelUserStep.Text = "开始普通清洗"));
                    }
                    else if (msgType == "*3158")
                    {
                        Invoke(new Action(() => labelUserStep.Text = "开始强力清洗"));
                    }
                    else if (msgType == "!3905")
                    {
                        Invoke(new Action(() => labelUserStep.Text = "清洗液不足"));
                    }
                    else if (msgType == "*3167")
                    {
                        Invoke(new Action(() =>
                        {
                            labelUserStep.Text = "普通清洗完成";
                            buttonUserClean1.Enabled = true;
                            buttonUserClean2.Enabled = true;

                        }));
                    }
                    else if (msgType == "*3168")
                    {
                        Invoke(new Action(() =>
                        {
                            labelUserStep.Text = "强力清洗完成";
                            buttonUserClean1.Enabled = true;
                            buttonUserClean2.Enabled = true;
                        }));
                    }
                    else if (msgType == "*3169")
                    {
                        Invoke(new Action(() =>
                        {
                            labelUserStep.Text = "普通清洗失败，清洗结束";
                            buttonUserClean1.Enabled = true;
                            buttonUserClean2.Enabled = true;

                        }));
                    }
                    else if (msgType == "*3170")
                    {
                        Invoke(new Action(() =>
                        {
                            labelUserStep.Text = "强力清洗失败，清洗结束";
                            buttonUserClean1.Enabled = true;
                            buttonUserClean2.Enabled = true;
                        }));
                    }
                    else if (msgType == "!0910")
                    {
                        Invoke(new Action(() =>
                        {
                            panelReagent1.Enabled = true;
                            panelReagent2.Enabled = true;
                            panelReagent3.Enabled = true;
                            panelReagent4.Enabled = true;
                            panelReagent5.Enabled = true;

                            var reagentlock = ConfigRead("ReagentLock").Split('-');
                            if (reagentlock.Length == 5)
                            {
                                panelReagent1.Enabled = reagentlock[0] == "0";
                                panelReagent2.Enabled = reagentlock[1] == "0";
                                panelReagent3.Enabled = reagentlock[2] == "0";
                                panelReagent4.Enabled = reagentlock[3] == "0";
                                panelReagent5.Enabled = reagentlock[4] == "0";
                            }

                            Log_Add("#" + detail[1] + "片仓打开失败", true);
                        }));
                    }
                    else if (msgType == "*0112")
                    {
                        Invoke(new Action(() =>
                        {
                            panelReagent1.Enabled = true;
                            panelReagent2.Enabled = true;
                            panelReagent3.Enabled = true;
                            panelReagent4.Enabled = true;
                            panelReagent5.Enabled = true;

                            var reagentlock = ConfigRead("ReagentLock").Split('-');
                            if (reagentlock.Length == 5)
                            {
                                panelReagent1.Enabled = reagentlock[0] == "0";
                                panelReagent2.Enabled = reagentlock[1] == "0";
                                panelReagent3.Enabled = reagentlock[2] == "0";
                                panelReagent4.Enabled = reagentlock[3] == "0";
                                panelReagent5.Enabled = reagentlock[4] == "0";
                            }

                            Log_Add("正在取片，请稍后开仓", true);
                        }));
                    }
                }
                catch (Exception ee)
                {
                    Log_Add("4276" + ee.ToString() + strCmd, false);
                }
            }
        }

        //messagebox
        public static List<string> messageboxshowstr = new List<string>();

        private void ShowMyAlert(string alertstr)
        {
            if (_myAlert != null)
            {
                if (!_myAlert.IsDisposed)
                    return;
            }

            try
            {
                CounterText = alertstr;
                _myAlert = new FormAlert { Owner = this };
                _myAlert.Show();
            }
            catch (Exception ee)
            {
                Log_Add("4291" + ee.ToString(), false);
            }
        }
        char idReadcnt = '0';

        private void SwitchTestItem(string reagentstoreid)
        {
            try
            {
                var reactionParam = SqlData.SelectReactionParam(reagentstoreid);
                var dilutionId = labelNextTestItem.Text == "CRPDIL" ? "7" : reactionParam.Rows[0][0].ToString();
                var sensorId = reactionParam.Rows[0][1].ToString();
                var reactionTime = reactionParam.Rows[0][2].ToString();
                var dropVolume = reactionParam.Rows[0][3].ToString();
                var CCDLocation = ConfigRead("CCDLocation");
                if (CCDLocation == "0")
                {
                    sensorId = (1 - int.Parse(sensorId)).ToString();
                }
                serialPort_DataSend(serialPortMain, string.Format("#3001${0}${1}${2}${3}${3}", dilutionId, reagentstoreid, sensorId, reactionTime));
                serialPort_DataSend(serialPortMain, "#3007$" + dropVolume);
            }
            catch (Exception)
            {
                Log_Add("切换项目时出错", true);
            }
        }

        //切换工作状态
        private void SwitchWorkState(int oldState, int newState)
        {
            if (oldState != newState)
            {
                if (oldState == 0)
                {
                    serialPort_DataSend(serialPortMain, "#0002$" + newState);
                }
                if (oldState == 4)
                {
                    serialPort_DataSend(serialPortMain, "#0004");

                    if (newState > 0)
                        serialPort_DataSend(serialPortMain, "#0002$" + newState);
                }
                if (oldState == 3)
                {
                    serialPort_DataSend(serialPortMain, "#0004");
                    for (int i = 0; i < 5000 / timerMainPort.Interval; i++)
                    {
                        serialPort_DataSend(serialPortMain, "TIMESleep");
                    }
                    if (newState > 0)
                        serialPort_DataSend(serialPortMain, "#0002$" + newState);
                    if (newState == 4)
                    {
                        serialPort_DataSend(serialPortMain, "#4013$0");
                    }
                }
            }
        }

        /// <summary>
        /// 文本信息更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Click(object sender, EventArgs e)
        {
            var tbx = (TextBox)sender;
            if (tbx.Name == textBoxLoginPassword.Name | tbx.Name == textBoxNewPassword.Name | tbx.Name == textBoxRepeatPassword.Name)//2017-2-24
                CounterText = "password|";
            else if (tbx.Name == textBoxQCTarget.Name |
                     tbx.Name == textBoxQCSD.Name | tbx.Name == textBoxSettingRatio.Name |
                     tbx.Name == textBoxSettingWarning.Name)
                CounterText = "Num|" + tbx.Text;
            else if (tbx.Name == textBoxSettingAccurancy.Name | tbx.Name == textBoxStartSample.Name |
                      tbx.Name == textBoxPage.Name | tbx.Name == textBoxSleepTime.Name | tbx.Name == textBoxPreDilu.Name | tbx.Name == textBoxBarcodeLength.Name)
                CounterText = "Int|" + tbx.Text;
            else if (tbx.Name == textBoxSearchSample.Name | tbx.Name == textBoxQCSampleNo.Name)
            {
                CounterText = "AllnoDot|" + tbx.Text;
                _otherInt[0] = 1;
            }
            else if (tbx.Name == textBoxSearchStartDate.Name | tbx.Name == textBoxSearchEndDate.Name)
                CounterText = "Date|" + tbx.Text;
            else if (tbx.Name == textBoxCMD.Name)
                CounterText = "CMD|" + tbx.Text;
            else
                CounterText = "All|" + tbx.Text;
            var str = VirtualKeyBoard();

            if (str == "Cancel")
            {
                _otherInt[0] = 0; return;
            }
            if (tbx.Name == textBoxSearchSample.Name | tbx.Name == textBoxStartSample.Name)
            {
                if (str == "") { _otherInt[0] = 0; ShowMyAlert("样本号不能为空"); return; }
                if (str.Length > 20) { _otherInt[0] = 0; ShowMyAlert("样本号长度不能大于20"); return; }
            }
            else if (tbx.Name == textBoxPage.Name)
            {
                _otherInt[0] = 0;
                if (str == "") { _otherInt[0] = 0; ShowMyAlert("页码不能为空"); return; }
                var pageall = labelpage.Text.Substring(1);
                if (pageall == "0") str = "0";
                else if (int.Parse(str) > int.Parse(pageall))
                    str = pageall;
                else if (int.Parse(str) <= 0)
                    str = "1";
                Invoke(new Action(() => UpdatedataGridViewSearch((int.Parse(str)).ToString(), "12")));
            }
            else if (tbx.Name == textBoxSleepTime.Name)
            {
                if (str == "") { _otherInt[0] = 0; ShowMyAlert("休眠时间不能为空"); return; }
                var time = int.Parse(str) * 60;
                UpdateAppConfig("SleepTime", str);
                serialPort_DataSend(serialPortMain, "#3052$" + time);
            }
            else if (tbx.Name == textBoxBarcodeLength.Name)
            {
                if (str == "") { _otherInt[0] = 0; ShowMyAlert("条码长度不能为空"); return; }
                UpdateAppConfig("BarcodeLength", str);
            }
            else if (tbx.Name == textBoxSettingWarning.Name | tbx.Name == textBoxSettingAccurancy.Name)
            {
                if (str == "") { _otherInt[0] = 0; ShowMyAlert("值不能为空"); return; }
            }
            else if (tbx.Name == textBoxQCSD.Name | tbx.Name == textBoxQCTarget.Name | tbx.Name == textBoxQCSampleNo.Name)
            {
                if (str == "") { _otherInt[0] = 0; ShowMyAlert("值不能为空"); return; }
            }
            tbx.Text = str;
            _otherInt[0] = 0;
        }

        /// <summary>
        /// 更新主功能表，待测列表、当日结果、异常结果
        /// </summary>
        /// <param name="type"></param>
        private void UpdatedataGridViewMain(string type)
        {
            try
            {
                switch (type)
                {
                    case "Doing":
                        dataGridViewMain.DataSource = SqlData.SelectWorkRunlist();
                        break;

                    case "Done":
                        dataGridViewMain.DataSource = SqlData.SelectResultListToday();
                        break;

                    case "Exception":
                        dataGridViewMain.DataSource = SqlData.SelectExceptionList();
                        break;
                }
                dataGridViewMain.Columns[0].Visible = false;
                dataGridViewMain.Columns[1].Width = 175;
                dataGridViewMain.Columns[2].Width = 155;
                dataGridViewMain.Columns[3].Width = 135;
            }
            catch (Exception)
            {
                Log_Add("更新主表时出错", true);
            }
        }

        /// <summary>
        /// 更新搜索列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageNo"></param>
        private void UpdatedataGridViewSearch(string page, string pageNo)
        {
            try
            {
                var dt1 = DateTime.Now;
                dataGridViewSearch.DataSource = SqlData.SelectResultList(page, pageNo, _searchcondition);
                dataGridViewSearch.Columns[0].Width = 180;
                dataGridViewSearch.Columns[1].Width = 100;
                dataGridViewSearch.Columns[2].Width = 140;
                dataGridViewSearch.Columns[3].Width = 180;
                var dt2 = DateTime.Now;
                var ts = dt2 - dt1;
                labelSearchCostTime.Text = string.Format(@"查询耗时：{0}ms", ts.Hours * 3600 * 1000 + ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds);
            }
            catch (Exception)
            {
                Log_Add("更新结果列表时出错", true);
            }
        }

        /// <summary>
        /// 刷新片仓信息
        /// </summary>
        private void UpdateReagentStore()
        {
            try
            {
                //查询片仓信息
                var dtReagentStoreInfo = SqlData.SelectReagentinfo();

                //更新1-5片仓信息
                for (var i = 1; i < 6; i++)
                {
                    var productstr = dtReagentStoreInfo.Rows[i - 1][1].ToString();
                    var lotstr = dtReagentStoreInfo.Rows[i - 1][2].ToString();
                    var reagentLeftstr = dtReagentStoreInfo.Rows[i - 1][3].ToString();
                    var expireDatestr = dtReagentStoreInfo.Rows[i - 1][4].ToString();
                    expireDatestr = "Exp." + DateTime.Parse(expireDatestr).ToString("yy/MM/dd");
                    var i1 = i;
                    Invoke(new Action(() =>
                    {
                        ((Label)Controls.Find("labelExt" + i1, true)[0]).Text = expireDatestr;
                        ((Label)Controls.Find("labelLotNo" + i1, true)[0]).Text = lotstr;
                        ((Label)Controls.Find("labelProduct" + i1, true)[0]).Text = productstr;
                        ((Label)Controls.Find("labelReagentLeft" + i1, true)[0]).Text = reagentLeftstr;
                    }));
                }
            }
            catch (Exception)
            {
                Log_Add("更新片仓信息时出错", true);
            }
        }

        /// <summary>
        /// 消耗品剩余更新
        /// </summary>
        /// <param name="dilutionuse"></param>
        /// <param name="cleanuse"></param>
        private void UpdateSupplyLeft(int dilutionuse, int cleanuse)
        {
            if (ConfigRead("FloatBallEnable") == "1")
            {
                Invoke(new Action(() =>
                {
                    if (dilutionuse + cleanuse == 0)
                    {
                        var volume1 = ConfigRead("SupplyVolume").Split('-');
                        var count1 = ConfigRead("SupplyLeft").Split('-');
                        count1[3] = (int.Parse(count1[3]) + 1).ToString();
                        if (int.Parse(count1[3]) >= int.Parse(volume1[3]))
                        {
                            labelFloatBallWasteReagent.BackColor = Color.Red;
                            labelFloatBallWasteReagent.Text = "废片仓\r\n\r\n已满\r\n\r\n请清空";

                            labelSupplyLeft4.Text = "废片仓：×";
                            labelReagentStatus.BackColor = Color.Red;
                            serialPort_DataSend(serialPortMain, "#3053$1");
                            labelLock.Text = "仪器锁定";
                            Log_Add("请清空废片仓后继续测试，当前仪器锁定", true);
                        }
                        else
                        {
                            labelFloatBallWasteReagent.BackColor = Color.FromArgb(41, 169, 223);
                            labelFloatBallWasteReagent.Text = "废片仓\r\n\r\n正常";
                            labelSupplyLeft4.Text = "废片仓：√";
                            labelReagentStatus.BackColor = Color.FromArgb(41, 169, 223);
                        }
                        UpdateAppConfig("SupplyLeft", count1[0] + "-" + count1[1] + "-" + count1[2] + "-" + count1[3]);
                        return;
                    }
                    if (dilutionuse * cleanuse != 1) return;
                    var volume = ConfigRead("FloatBallVolume").Split('-');
                    var count = ConfigRead("FloatBallCount").Split('-');
                    if (int.Parse(count[0]) <= int.Parse(volume[0]))
                    {
                        count[0] = (Math.Max(0, int.Parse(count[0]) - 1)).ToString();
                        labelFloatBallDilution.Text = "稀释液\r\n\r\n" + count[0] + "Test\r\n\r\n" + "点击灌注";
                        labelFloatBallDilution.BackColor = Color.Red;
                        labelDilutionStatus.BackColor = Color.Red;
                        labelSupplyLeft.Text = "稀释液:×";
                        if (count[0] == "0")
                        {
                            serialPort_DataSend(serialPortMain, "#3053$1");
                            labelLock.Text = "仪器锁定";
                            Log_Add("请更换稀释液后继续操作，当前仪器锁定", true);
                        }
                    }
                    else
                    {
                        labelFloatBallDilution.Text = "稀释液\r\n\r\n正常\r\n\r\n" + "点击灌注";
                        labelFloatBallDilution.BackColor = Color.FromArgb(41, 169, 223);
                        labelDilutionStatus.BackColor = Color.FromArgb(41, 169, 223);
                        labelSupplyLeft.Text = "稀释液:√";
                    }

                    if (int.Parse(count[1]) <= int.Parse(volume[1]))
                    {
                        count[1] = (Math.Max(0,int.Parse(count[1]) - 1)).ToString();
                        labelFloatBallClean.Text = "清洗液\r\n\r\n" + count[1] + "Test\r\n\r\n" + "点击灌注";
                        labelFloatBallClean.BackColor = Color.Red;
                        labelCleanStatus.BackColor = Color.Red;
                        labelSupplyLeft2.Text = "清洗液:×";
                        if (count[1] == "0")
                        {
                            serialPort_DataSend(serialPortMain, "#3053$1");
                            labelLock.Text = "仪器锁定";
                            Log_Add("请更换清洗液后继续操作，当前仪器锁定", true);
                        }
                    }
                    else
                    {
                        labelFloatBallClean.Text = "清洗液\r\n\r\n正常\r\n\r\n" + "点击灌注";
                        labelFloatBallClean.BackColor = Color.FromArgb(41, 169, 223);
                        labelCleanStatus.BackColor = Color.FromArgb(41, 169, 223);
                        labelSupplyLeft2.Text = "清洗液:√";
                    }

                    UpdateAppConfig("FloatBallCount", count[0] + "-" + count[1]);
                }));
                return;
            }
            try
            {
                //读取容量、剩余、单位测试消耗
                var supplyVolume = ConfigRead("SupplyVolume").Split('-');
                var supplyLeft = ConfigRead("SupplyLeft").Split('-');
                var supplyUse = ConfigRead("SupplyUse").Split('-');
                var wasteMode = ConfigRead("WasteMode");
                //当前使用率
                var dilutionUsage1 = double.Parse(supplyLeft[0]) / double.Parse(supplyVolume[0]) * 100;
                var cleanUsage1 = double.Parse(supplyLeft[1]) / double.Parse(supplyVolume[1]) * 100;
                var wasteUsage1 = double.Parse(supplyLeft[2]) / double.Parse(supplyVolume[2]) * 100;
                var wasteReagentUsage1 = double.Parse(supplyLeft[3]) / double.Parse(supplyVolume[3]) * 100;
                //不同情形更新消耗品剩余
                if (dilutionuse == 1 & cleanuse == 1)
                {
                    supplyLeft[0] = (int.Parse(supplyLeft[0]) - int.Parse(supplyUse[0])).ToString();
                    supplyLeft[1] = (int.Parse(supplyLeft[1]) - int.Parse(supplyUse[1])).ToString();
                    supplyLeft[2] =
                        (int.Parse(supplyLeft[2]) + int.Parse(supplyUse[0]) + int.Parse(supplyUse[1])).ToString();
                }
                else if (dilutionuse == 2 & cleanuse == 0)
                {
                    supplyLeft[0] = (int.Parse(supplyLeft[0]) - int.Parse(supplyUse[2])).ToString();
                    supplyLeft[2] = (int.Parse(supplyLeft[2]) + int.Parse(supplyUse[2])).ToString();
                }
                if (dilutionuse == 0 & cleanuse == 2)
                {
                    supplyLeft[1] = (int.Parse(supplyLeft[1]) - int.Parse(supplyUse[3])).ToString();
                    supplyLeft[2] = (int.Parse(supplyLeft[2]) + int.Parse(supplyUse[3])).ToString();
                }
                else if (dilutionuse == 0 & cleanuse == 0)
                {
                    supplyLeft[3] = (int.Parse(supplyLeft[3]) + 1).ToString();
                }
                //新的使用率
                var dilutionUsage2 = double.Parse(supplyLeft[0]) / double.Parse(supplyVolume[0]) * 100;
                var cleanUsage2 = double.Parse(supplyLeft[1]) / double.Parse(supplyVolume[1]) * 100;
                var wasteUsage2 = double.Parse(supplyLeft[2]) / double.Parse(supplyVolume[2]) * 100;
                var wasteReagentUsage2 = double.Parse(supplyLeft[3]) / double.Parse(supplyVolume[3]) * 100;
                //使用率必须在0-100之间
                if (dilutionUsage2 < 0)
                    supplyLeft[0] = "0";
                if (cleanUsage2 < 0)
                    supplyLeft[1] = "0";
                if (wasteUsage2 > 100)
                    supplyLeft[2] = supplyVolume[2];
                if (wasteReagentUsage2 > 100)
                    supplyLeft[3] = supplyVolume[3];
                //更新容量
                UpdateAppConfig("SupplyLeft",
                    string.Format("{0}-{1}-{2}-{3}", supplyLeft[0], supplyLeft[1], supplyLeft[2], supplyLeft[3]));
                //达到警戒线预警
                var str = "";
                if (dilutionUsage1 >= 10 & dilutionUsage2 < 10)
                {
                    str += "稀释液不足10%";
                }
                if (cleanUsage1 >= 10 & cleanUsage2 < 10)
                {
                    str = (str != "" ? Environment.NewLine : "") + "清洗液不足10%";
                }
                if (wasteUsage1 <= 90 & wasteUsage2 > 90 & wasteMode == "0")
                {
                    str = (str != "" ? Environment.NewLine : "") + "废液已超90%";
                }
                if (wasteReagentUsage1 <= 90 & wasteReagentUsage2 > 90)
                {
                    str = (str != "" ? Environment.NewLine : "") + "废片仓已超90%";
                }
                //弹出预警信息
                if (str != "")
                {
                    Invoke(new Action(() =>
                    {
                        Log_Add(str, true);
                    }));
                }
                    //更新消耗品图示
                    Invoke(new Action(UpdateSupplyUi));
            }
            catch (Exception)
            {
                Log_Add("更新消耗品时出错", true);
            }
        }

        /// <summary>
        /// 更新消耗品界面
        /// </summary>
        private void UpdateSupplyUi()
        {
            if (ConfigRead("FloatBallEnable") == "1")
            {
                var supplyLeft1 = ConfigRead("SupplyLeft").Split('-');
                var supplyVolume1 = ConfigRead("SupplyVolume").Split('-');

                if (supplyLeft1[3] == supplyVolume1[3])
                {
                    labelReagentStatus.BackColor = Color.Red;
                    labelFloatBallWasteReagent.BackColor = Color.Red;
                    labelFloatBallWasteReagent.Text = "废片仓\r\n\r\n已满\r\n\r\n请清空";
                    labelSupplyLeft4.Text = "废片仓：×";
                }
                else
                {
                    labelReagentStatus.BackColor = Color.FromArgb(41, 169, 223);
                    labelFloatBallWasteReagent.BackColor = Color.FromArgb(41, 169, 223);
                    labelFloatBallWasteReagent.Text = "废片仓\r\n\r\n正常";
                    labelSupplyLeft4.Text = "废片仓：√";

                }

                var supplyLeft2 = ConfigRead("FloatBallCount").Split('-');
                var supplyVolume2 = ConfigRead("FloatBallVolume").Split('-');

                if (int.Parse(supplyLeft2[0]) <= int.Parse(supplyVolume2[0]))
                {
                    labelDilutionStatus.BackColor = Color.Red;
                    labelFloatBallDilution.BackColor = Color.Red;
                    labelFloatBallDilution.Text = "稀释液\r\n\r\n" + supplyLeft2[0] + "Test\r\n\r\n" + "点击灌注";
                    labelSupplyLeft.Text = "稀释液：×";
                }
                else
                {
                    labelDilutionStatus.BackColor = Color.FromArgb(41, 169, 223);
                    labelFloatBallDilution.BackColor = Color.FromArgb(41, 169, 223);
                    labelFloatBallDilution.Text = "稀释液\r\n\r\n正常\r\n\r\n" + "点击灌注";
                    labelSupplyLeft.Text = "稀释液：√";
                }
                if (int.Parse(supplyLeft2[1]) <= int.Parse(supplyVolume2[1]))
                {
                    labelCleanStatus.BackColor = Color.Red;
                    labelFloatBallClean.BackColor = Color.Red;
                    labelFloatBallClean.Text = "清洗液\r\n\r\n" + supplyLeft2[1] + "Test\r\n\r\n" + "点击灌注";
                    labelSupplyLeft2.Text = "清洗液：×";
                }
                else
                {
                    labelCleanStatus.BackColor = Color.FromArgb(41, 169, 223);
                    labelFloatBallClean.BackColor = Color.FromArgb(41, 169, 223);
                    labelFloatBallClean.Text = "清洗液\r\n\r\n正常\r\n\r\n" + "点击灌注";
                    labelSupplyLeft2.Text = "清洗液：×";
                }
                return;
            }
            try
            {
                //读取当前消耗品容量、剩余以及废液排出方式
                var supplyLeft = ConfigRead("SupplyLeft").Split('-');
                var supplyVolume = ConfigRead("SupplyVolume").Split('-');
                var wasteMode = ConfigRead("WasteMode");
                if (wasteMode == "1")
                {
                    panelWasteDisable.Visible = true;
                    buttonWasteMode.BackgroundImage = Resources.switch_on;
                }
                else
                {
                    buttonWasteMode.BackgroundImage = Resources.switch_off;
                    panelWasteDisable.Visible = false;
                }
                //当前使用率
                var dilutionUsage = double.Parse(supplyLeft[0]) / double.Parse(supplyVolume[0]) * 100;
                var cleanUsage = double.Parse(supplyLeft[1]) / double.Parse(supplyVolume[1]) * 100;
                var wasteUsage = double.Parse(supplyLeft[2]) / double.Parse(supplyVolume[2]) * 100;
                var wasteReagentUsage = double.Parse(supplyLeft[3]) / double.Parse(supplyVolume[3]) * 100;

                if(buttonModifyLeft.Text == @"更改存余"& buttonModifyVolume.Text=="更改容量")
                {
                    //整数表示
                    labelDilution1.Text = dilutionUsage.ToString("F0") + @"%";
                    labelDilution2.Text = dilutionUsage.ToString("F0") + @"%";

                    labelClean1.Text = cleanUsage.ToString("F0") + @"%";
                    labelClean2.Text = cleanUsage.ToString("F0") + @"%";

                    labelWaste1.Text = wasteUsage.ToString("F0") + @"%";
                    labelWaste2.Text = wasteUsage.ToString("F0") + @"%";

                    labelWasteReagent1.Text = wasteReagentUsage.ToString("F0") + @"%";
                    labelWasteReagent2.Text = wasteReagentUsage.ToString("F0") + @"%";
                }
                labelSupplyLeft.Text = "稀释液:" + dilutionUsage.ToString("F0") + "%";
                labelSupplyLeft2.Text = "清洗液:" + cleanUsage.ToString("F0") + "%";
                labelSupplyLeft3.Text = "废液:" + wasteUsage.ToString("F0") + "%";
                labelSupplyLeft4.Text = "废片:" + wasteReagentUsage.ToString("F0") + "%";


                //不同警戒线不同颜色表示
                //41 169 223
                if (dilutionUsage < 10)
                {
                    panelDilution2.BackgroundImage = Resources.BottleFull3;
                    labelDilutionStatus.BackColor = Color.Red;
                }
                else if (dilutionUsage < 30)
                {
                    panelDilution2.BackgroundImage = Resources.BottleFull2;
                    labelDilutionStatus.BackColor = Color.Yellow;

                }
                else
                {
                    panelDilution2.BackgroundImage = Resources.BottleFull;
                    labelDilutionStatus.BackColor = Color.FromArgb(41, 169, 223);
                }

                if (cleanUsage < 10)
                {
                    panelClean2.BackgroundImage = Resources.BottleFull3;
                    labelCleanStatus.BackColor = Color.Red;
                }
                else if (cleanUsage < 30)
                {
                    panelClean2.BackgroundImage = Resources.BottleFull2;
                    labelCleanStatus.BackColor = Color.Yellow;
                }
                else
                {
                    panelClean2.BackgroundImage = Resources.BottleFull;
                    labelCleanStatus.BackColor = Color.FromArgb(41, 169, 223);
                }
                if (wasteMode == "0")
                {
                    labelSupplyLeft3.Visible = true;
                    if (wasteUsage > 90)
                    {
                        panelWaste2.BackgroundImage = Resources.BottleFull3;
                        labelWasteStatus.BackColor = Color.Red;
                    }
                    else if (wasteUsage > 70)
                    {
                        panelWaste2.BackgroundImage = Resources.BottleFull2;
                        labelWasteStatus.BackColor = Color.Yellow;
                    }
                    else
                    {
                        panelWaste2.BackgroundImage = Resources.BottleFull;
                        labelWasteStatus.BackColor = Color.FromArgb(41, 169, 223);
                    }
                }
                else
                {
                    labelWasteStatus.BackColor = Color.Gray;
                    labelSupplyLeft3.Visible = false;
                }
                if (wasteReagentUsage > 90)
                {
                    panelWasteReagent2.BackgroundImage = Resources.ReagentFull3;
                    labelReagentStatus.BackColor = Color.Red;
                }
                else if (wasteReagentUsage > 70)
                {
                    panelWasteReagent2.BackgroundImage = Resources.ReagentFull2;
                    labelReagentStatus.BackColor = Color.Yellow;
                }
                else
                {
                    panelWasteReagent2.BackgroundImage = Resources.ReagentFull;
                    labelReagentStatus.BackColor = Color.FromArgb(41, 169, 223);
                }
                //调整图像页面高度
                panelDilution1.Size = new Size(128, (118 - (int)dilutionUsage * 4 / 5));
                panelClean1.Size = new Size(128, (118 - (int)cleanUsage * 4 / 5));
                panelWaste1.Size = new Size(128, (118 - (int)wasteUsage * 4 / 5));
                panelWasteReagent1.Size = new Size(128, (125 - (int)wasteReagentUsage * 86 / 100));
            }
            catch (Exception)
            {
                Log_Add("更新消耗品时出错", true);
            }
        }

        /// <summary>
        /// 虚拟键盘打开
        /// </summary>
        /// <returns></returns>
        private string VirtualKeyBoard()
        {
            _mykeyboard = new KeyboardforIrd();
            _mykeyboard.ShowDialog();
            CounterText = "";
            return KeyboardforIrd.Returnstr;
        }

        //过度加载动画
        private void timerLoding_Tick(object sender, EventArgs e)
        {
            panelLoding1.Size = new Size(Math.Min(40, panelLoding1.Size.Width + 1), Math.Min(40, panelLoding1.Size.Width + 1));
            if (panelLoding1.Size.Width > 10)
            {
                panelLoding2.Size = new Size(Math.Min(40, panelLoding2.Size.Width + 1), Math.Min(40, panelLoding2.Size.Width + 1));
            }
            if (panelLoding2.Size.Width > 10)
            {
                panelLoding3.Size = new Size(Math.Min(40, panelLoding3.Size.Width + 1), Math.Min(40, panelLoding3.Size.Width + 1));
            }
            if (panelLoding3.Size.Width > 10)
            {
                panelLoding4.Size = new Size(Math.Min(40, panelLoding4.Size.Width + 1), Math.Min(40, panelLoding4.Size.Width + 1));
            }
            if (panelLoding4.Size.Width > 10)
            {
                panelLoding5.Size = new Size(Math.Min(40, panelLoding5.Size.Width + 1), Math.Min(40, panelLoding5.Size.Width + 1));
            }
            if (panelLoding5.Size.Width == 40)
            {
                panelLoding1.Size = new Size(0, 0);
                panelLoding2.Size = new Size(0, 0);
                panelLoding3.Size = new Size(0, 0);
                panelLoding4.Size = new Size(0, 0);
                panelLoding5.Size = new Size(0, 0);
            }
            panelLoding1.Location = new Point(20 - panelLoding1.Size.Width / 2, 20 - panelLoding1.Size.Width / 2);
            panelLoding2.Location = new Point(70 - panelLoding2.Size.Width / 2, 20 - panelLoding2.Size.Width / 2);
            panelLoding3.Location = new Point(120 - panelLoding3.Size.Width / 2, 20 - panelLoding3.Size.Width / 2);
            panelLoding4.Location = new Point(170 - panelLoding4.Size.Width / 2, 20 - panelLoding4.Size.Width / 2);
            panelLoding5.Location = new Point(220 - panelLoding5.Size.Width / 2, 20 - panelLoding5.Size.Width / 2);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxLog.Visible = textBoxLog.Name.Substring(7) == comboBox3.Text;
            textBoxTH.Visible = textBoxTH.Name.Substring(7) == comboBox3.Text;
            textBoxQR.Visible = textBoxQR.Name.Substring(7) == comboBox3.Text;
            textBoxMain.Visible = textBoxMain.Name.Substring(7) == comboBox3.Text;
            textBoxASU.Visible = textBoxASU.Name.Substring(7) == comboBox3.Text;
        }

        private void buttonUserClean1_Click(object sender, EventArgs e)
        {
            if (_otherInt[2] != 3)
            {
                ShowMyAlert("此操作须在测试模式下进行"); return;
            }
            //普通清洗模式
            serialPort_DataSend(serialPortMain, "#3055$1");
            Log_Add("开始普通清洗", false);
            labelUserStep.Text = "准备普通清洗，请按下吸样键";
            labelUserStep.Location = new Point(139, 64);
            buttonUserClean1.Enabled = false;
            buttonUserClean2.Enabled = false;
        }

        private void buttonUserClean2_Click(object sender, EventArgs e)
        {
            if (_otherInt[2] != 3)
            {
                ShowMyAlert("此操作须在测试模式下进行"); return;
            }
            //普通清洗模式
            serialPort_DataSend(serialPortMain, "#3055$2");
            Log_Add("开始强力清洗", false);
            labelUserStep.Text = "准备强力清洗，请按下吸样键";
            labelUserStep.Location = new Point(139, 129);
            buttonUserClean1.Enabled = false;
            buttonUserClean2.Enabled = false;
        }

        private void buttonUserLiquidCheck_Click(object sender, EventArgs e)
        {
            SwitchWorkState(_otherInt[2], 2);
        }

        private void buttonUserTest_Click(object sender, EventArgs e)
        {
            SwitchWorkState(_otherInt[2], 3);
        }

        //自动校准需要校准5个，与手动有冲突，需处理
        private bool ReagentSensorCheck = false;

        private string ReagentSensorCheckStr = "";

        private void buttonReagentSensor_Click(object sender, EventArgs e)
        {
            //片仓光耦校准  需要校准1-5个片仓，命令为#4011$1
            ShowMyAlert("开始自动进行试剂片仓光耦校准");
            ReagentSensorCheck = true;

            serialPort_DataSend(serialPortMain, "#4010$1");
            ReagentSensorCheckStr = "#4010$2|#4010$3|#4010$4|#4010$5|";
        }

        private bool LiquidSensorCheck = false;
        private string LiquidSensorCheckCMDS = "";

        private void buttonLiquidSensor_Click(object sender, EventArgs e)
        {
            if (buttonLiquidSensor.Text == "液路光耦校准")
            {
                LiquidSensorCheck = true;
                serialPort_DataSend(serialPortMain, "#4029");
                Log_Add("开始液路光耦校准", false);
                //液路光耦校准，1、#4029 #4030各5次
                LiquidSensorCheckCMDS = "#4029|#4029|#4029|#4029|#4030|#4030|#4030|#4030|#4030|#4009$0|#4009$1|#4029|#4029|#4029|#4029|#4029|#4030|#4030|#4030|#4030|#4030|#4033$0|";
            }
            else
            {
                serialPort_DataSend(serialPortMain, "#4029");
                LiquidSensorCheckCMDS = LiquidSensorCheckCMDS.Substring(6);
                Log_Add("继续液路光耦校准", false);
            }
        }

        private void timerLoad_Tick(object sender, EventArgs e)
        {
            timerLoad.Stop();

            var reagentlock = ConfigRead("ReagentLock").Split('-');
            if (reagentlock.Length == 5)
            {
                panelReagent1.Enabled = reagentlock[0] == "0";
                panelReagent2.Enabled = reagentlock[1] == "0";
                panelReagent3.Enabled = reagentlock[2] == "0";
                panelReagent4.Enabled = reagentlock[3] == "0";
                panelReagent5.Enabled = reagentlock[4] == "0";
            }

            //如果没有系统数据库，则需附加数据库
            if (SqlData.Dbok() == "0")
            {
                var mdfpath = Application.StartupPath + @"/database/i-Reader_S.mdf";
                var logpath = Application.StartupPath + @"/database/i-Reader_S_1.ldf";
                SqlData.Attachdb(mdfpath, logpath);
            }

            if (!Directory.Exists(Application.StartupPath + @"\fluodata"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/fluodata");
            }

            if (!Directory.Exists(Application.StartupPath + @"\rawdata"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/rawdata");
                Directory.CreateDirectory(Application.StartupPath + "/rawdata/error");

            }

            if (!Directory.Exists(Application.StartupPath + @"\logdata"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/logdata");
            }

            if (!Directory.Exists(Application.StartupPath + @"\img"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/img");
            }

            if (ConfigRead("QCAuto") == "1")
            {
                var dt = SqlData.UpdateQCSettingValue(DateTime.Now.Year, DateTime.Now.Month);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        SqlData.UpdateQcSetting(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString());
                    }
                }
            }

            if (ConfigRead("IDRead") == "1")
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    ShowMyAlert("未检测到条码摄像头"); Close(); return;
                }
                videoSource = new VideoCaptureDevice(videoDevices[videoDevices.Count - 1].MonikerString);

                if ((from q in videoSource.VideoCapabilities
                     where
     q.FrameSize == new Size(1280, 720)
                     select q).ToArray().Count() > 0)

                {
                    videoSource.VideoResolution = (from q in videoSource.VideoCapabilities
                                                   where
    q.FrameSize == new Size(1280, 720)
                                                   select q).ToArray()[0];
                    // videoSource.DesiredFrameSize = new Size(1280, 720);
                    videoSourcePlayer1.VideoSource = videoSource;
                    videoSourcePlayer1.Start();
                }
                else
                {
                    ShowMyAlert("条码摄像头无1280x720像素"); Close(); return;
                }
            }
            ShowCursor(int.Parse(ConfigRead("Cursor")));
            buttonAutoPrint.BackgroundImage = ConfigRead("AutoPrint") == "0"
? Resources.switch_off
: Resources.switch_on;
            buttonCRPDebug.BackgroundImage = ConfigRead("CRPDebug") == "0"
? Resources.switch_off
: Resources.switch_on;
            textBoxSleepTime.Text = ConfigRead("SleepTime");
            textBoxStartSample.Text = ConfigRead("StartSampleNo");
            textBoxBarcodeLength.Text = ConfigRead("BarcodeLength");


            //资源文件
            _rm = new ResourceManager("i_Reader_S.Properties.Resources", Assembly.GetExecutingAssembly());

            //获取剩余空间大小
            var space = DriveSpaceLeft();
            if (space[0] <= 10)//如果空间大小小于10G
            {
                //设置下次启动清理
                if (ConfigRead("AutoClean") == "0")
                {
                    // ReSharper disable once ResourceItemNotResolved
                    Log_Add("[D]D0001" + Resources.D0001, true, Color.Red);
                    //设置自动清理配置参数
                    UpdateAppConfig("AutoClean", "1");
                }
                else
                {
                    //保留胶体金60000个样本图片+数据共120000文件
                    if (Directory.Exists(Application.StartupPath + @"\Rawdata"))
                    {
                        //定义一个DirectoryInfo对象
                        var di = new DirectoryInfo(Application.StartupPath + @"\Rawdata");
                        var filelist = di.GetFiles();
                        for (var i = 0; i < filelist.Length - 120000; i++)
                        {
                            File.Delete(filelist[i].FullName);
                        }
                    }
                    //保留荧光60000个样本
                    if (Directory.Exists(Application.StartupPath + @"\fluodata"))
                    {
                        //定义一个DirectoryInfo对象
                        var di = new DirectoryInfo(Application.StartupPath + @"\fluodata");
                        var filelist = di.GetFiles();
                        for (var i = 0; i < filelist.Length - 60000; i++)
                        {
                            File.Delete(filelist[i].FullName);
                        }
                    }
                    //将操作信息写入日志
                    // ReSharper disable once ResourceItemNotResolved
                    Log_Add("[D]D0002" + Resources.D0002, true, Color.Red);
                    UpdateAppConfig("AutoClean", "0");
                }
            }
            //默认搜索的开始、结束日期
            textBoxSearchStartDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd");
            textBoxSearchEndDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            //各端口初始化

            //用于端口报错信息提示
            var initStr = "";
            //是否启用CCD、荧光、温湿度
            var ccdEnable = ConfigRead("CCDEnable");
            var fluoEnable = ConfigRead("FluoEnable");
            var thEnable = ConfigRead("THEnable");
            var ASUEanble = ConfigRead("ASUEnable");
            //主控端口是否正常
            try
            {
                serialPortMain.PortName = ConfigRead("MainPort");
                serialPortMain.Open();
            }
            catch (Exception)
            {
                // ReSharper disable once ResourceItemNotResolved
                initStr += Resources.D0003_1 + Environment.NewLine;
            }
            //二维码端口
            try
            {
                serialPortQR.PortName = ConfigRead("QRPort");
                serialPortQR.Open();
            }
            catch (Exception)
            {
                // ReSharper disable once ResourceItemNotResolved
                initStr += Resources.D0003_2 + Environment.NewLine;
            }
            //除湿端口
            try
            {
                serialPortTH.PortName = ConfigRead("THPort");
                serialPortTH.Open();
            }
            catch (Exception)
            {
                // ReSharper disable once ResourceItemNotResolved
                initStr += Resources.D0003_3 + Environment.NewLine;
            }
            if (thEnable == "1")
            {
                labelTH.Visible = true;
            }
            else
            {
                labelTH.Visible = false;

                labelSupplyLeft.Location = new Point(labelSupplyLeft.Location.X,723);
                labelSupplyLeft4.Location = new Point(labelSupplyLeft4.Location.X, 723);
                labelSupplyLeft2.Location = new Point(labelSupplyLeft2.Location.X, 723);
                labelSupplyLeft3.Location = new Point(labelSupplyLeft3.Location.X, 723);
                labelReagentNow.Location = new Point(labelReagentNow.Location.X, 723);


            }
            if (ASUEanble == "1")
            {
                try
                {
                    serialPortME.PortName = ConfigRead("ASUPort");
                    serialPortME.Open();
                }
                catch (Exception)
                {
                    initStr += "ASU端口连接异常";
                }
            }
            if (fluoEnable == "1")
            {
                //荧光端口
                try
                {
                    serialPortFluo.PortName = ConfigRead("FluoPort");
                    serialPortFluo.Open();
                    FluoConnect(0);
                }
                catch (Exception)
                {
                    // ReSharper disable once ResourceItemNotResolved
                    initStr += Resources.D0003_4 + Environment.NewLine;
                }
                //荧光电机端口
                try
                {
                    serialPortFluoMotor.PortName = ConfigRead("FluoMotorPort");
                    serialPortFluoMotor.Open();
                    serialPort_DataSend(serialPortFluoMotor, "010611");
                }
                catch (Exception)
                {
                    // ReSharper disable once ResourceItemNotResolved
                    initStr += Resources.D0003_5 + Environment.NewLine;
                }
            }

            //CCD检测
            if (ccdEnable == "1")
            {
                if (TsCameraStatus.StatusOk != CCam.CameraInit((IntPtr)0, 0, pictureBox1.Handle, Handle)) // 接收SDK消息句柄
                {
                    // ReSharper disable once ResourceItemNotResolved
                    initStr += Resources.D0003_6;
                }
                else
                {
                    //相机初始化
                    var icount = 0;
                    //相机支持的分辨率
                    CCam.CameraGetResCount(ref icount);
                    var str = new StringBuilder(50);
                    var szRes = new string[icount];
                    for (var i = 0; i < icount; i++)
                    {
                        CCam.CameraGetResItemName(i, str);
                        szRes[i] = str.ToString();
                    }
                    //相机支持的帧率
                    CCam.CameraGetFrameCount(ref icount);
                    var szSpeed = new string[20];
                    for (var i = 0; i < icount; i++)
                    {
                        CCam.CameraGetFrameItemName(i, str);
                        szSpeed[i] = str.ToString();
                    }
                    CCam.CameraSetDataWide(true);
                    // 预览相机
                    CCam.CameraPlay();
                    // 设置新的曝光时间
                    CCam.CameraSetExposureTimeMS(70);
                    //设置帧率为Low型：3fps左右
                    CCam.CameraSetFrameSpeed(2);
                    var fMin = 1.0F;
                    var fMax = 1.0f;
                    var fStep = 1.0f;
                    CCam.CameraGetExposureTimeRange(ref fMin, ref fMax, ref fStep);
                    float fValue = 0;
                    CCam.CameraGetExposureTimeMS(ref fValue);
                }
            }
            if (ConfigRead("FloatBallEnable") == "1")
            {
                try
                {
                    serialPortFloatBall.PortName = ConfigRead("FloatBallPort");
                    serialPortFloatBall.Open();
                }
                catch (Exception ee)
                {
                    initStr += "浮球端口异常" + Environment.NewLine;
                }
            }
            if (initStr != "")
            {
                Log_Add(string.Format("[D]D0003{0}", initStr), true, Color.Red);
                Close();
            }

            //界面大小设定
            Size = new Size(1024, 768);
            comboBox3.SelectedIndex = 0;
            //控制tabcontrol显示
            tabControlMain.Region =
                new Region(new RectangleF(4, 22, tabControlMain.Size.Width - 8, tabControlMain.Size.Height - 26));
            tabControlMainRight.Region =
                new Region(new RectangleF(4, 22, tabControlMainRight.Size.Width - 8,
                    tabControlMainRight.Size.Height - 26));
            //切换至登录界面
            tbtemp = tabPageLogin;
            labelLoding.Visible = false;
        }
        TabPage tbtemp;
        private string _comMeStr = "";

        private void timerPageAdjust_Tick(object sender, EventArgs e)
        {
            if (tabControlMain.SelectedTab != tbtemp)
                tabControlMain.SelectedTab = tbtemp;
        }

        private void buttonCCDTest_Click(object sender, EventArgs e)
        {
            LocationCcd = -1;
            Log_Add(CalTy("0"), false);

            LocationCcd = 0;
            var resultStr = CalTy("0");
            Log_Add(CalTy("0"), false);

            var ty = resultStr.Substring(resultStr.IndexOf("T(", StringComparison.Ordinal) + 2);
            ty = ty.Substring(ty.IndexOf(",", StringComparison.Ordinal) + 1);
            ty = ty.Substring(0, ty.IndexOf(")", StringComparison.Ordinal));

            var Basey = resultStr.Substring(resultStr.IndexOf("Min(", StringComparison.Ordinal) + 4);
            Basey = Basey.Substring(Basey.IndexOf(",") + 1);
            Basey = Basey.Substring(0, Basey.IndexOf(")"));

            var Thit = int.Parse(ty) - int.Parse(Basey);

            var tyFixStr = ConfigRead("CMOSFix");

            var trueThit = double.Parse(ty) * double.Parse(tyFixStr.Split('|')[0]) + double.Parse(tyFixStr.Split('|')[1]);

            Invoke(new Action(() =>
            {
                labelResult.Text = @"TY:" + ty + @" THit:" + Thit + @" Base:" + Basey;//2017-04-24
                //labelResult.Text = @"TY:" + ty;
            }));
        }

        private int ColBorder(byte[] col)
        {
            int[] colx = new int[2];
            for (int i = 15; i < col.Length - 300; i++)
            {
                if (col.Take(300 + i).Reverse().Take(300).Max() - col.Take(300 + i).Reverse().Take(300).Min() < 60)
                {
                    colx[0] = i; break;
                }
            }
            col = col.Reverse().ToArray();
            for (int i = 15; i < col.Length - 300; i++)
            {
                if (col.Take(300 + i).Reverse().Take(300).Max() - col.Take(300 + i).Reverse().Take(300).Min() < 60)
                {
                    colx[1] = col.Length - 1 - i; break;
                }
            }

            if (colx[1] - colx[0] < 100)
            {
                colx[0] = colx[1] - 100;
            }
            return (int)colx.Average();
        }

        private int[] RowBorder(byte[] row)
        {
            int[] rowx = new int[2];
            for (int i = 15; i < row.Length - 15; i++)
            {
                //2016-10-11  将row[i]+40>row.Max()修改成100，2016-10-10出现了白色边界区域灰度值为190情形，白色限定太高无法识别。
                if (row.Take(i + 15).Reverse().Take(30).Max() == row[i] & row[i] + 100 > row.Max())
                {
                    rowx[0] = i; break;
                }
            }

            row = row.Reverse().ToArray();
            for (int i = 15; i < row.Length - 15; i++)
            {
                if (row.Take(i + 15).Reverse().Take(30).Max() == row[i] & row[i] + 100 > row.Max())
                {
                    rowx[1] = row.Length - 1 - i; break;
                }
            }
            return rowx;
        }
        /// <summary>
        /// 将采集到的图像进行分型，输出为最后结果
        /// </summary>
        /// <param name="bit"></param>
        /// <returns></returns>
        /// 

        private string IDReader(Bitmap bit)
        {
            try
            {


                //灰度化处理，人眼对绿色敏感度最高，蓝色敏感度最低，所以权重侧重绿色
                bit = new Grayscale(0.2125, 0.7154, 0.0721).Apply(bit);
                //bit.Save("img/" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg", ImageFormat.Jpeg);
                var result = "";
                //第一步：二值化处理
                var bit1 = new Threshold(200).Apply(bit);
                //转换数据格式，提升都取效率
                var rawdata = BitToByte(bit1);
                //将720x1280分成72x128块,每一块如果全部为255则记录为1，否则记录为0
                var subrawdata = new int[72 * 128];
                var sumcol = new int[72];
                for (int i = 0; i < 72; i++)
                {
                    for (int j = 0; j < 128; j++)
                    {
                        for (int i1 = i * 10; i1 < i * 10 + 10; i1++)
                        {
                            for (int j1 = j * 10; j1 < j * 10 + 10; j1++)
                            {
                                subrawdata[i * 128 + j] += rawdata[i1 * 1280 + j1];
                            }
                        }
                        subrawdata[i * 128 + j] = (subrawdata[i * 128 + j] == 25500 ? 1 : 0);
                    }
                    //如果某行的前一半和后一半都有2块白色则可能为试剂条区域否则为反光区域
                    if (subrawdata.Take(i * 128 + 64).Reverse().Take(64).Sum() < 2 | subrawdata.Take(i * 128 + 128).Reverse().Take(128).Sum() < 2)
                    {
                        for (int j = 0; j < 128; j++)
                        {
                            subrawdata[i * 128 + j] = 0;
                        }
                    }
                    sumcol[i] = subrawdata.Take(i * 128 + 128).Reverse().Take(128).Sum();
                }

                var colx = new int[2];
                for (int i = 0; i < 68; i++)
                {
                    if (sumcol[i] < 2 & sumcol[i + 4] > 4)
                    { colx[0] = i; break; }
                }
                for (int i = 71; i > 3; i--)
                {
                    if (sumcol[i] < 2 & sumcol[i - 4] > 4)
                    { colx[1] = i; break; }
                }
                if (colx[0] == 0 & colx[1] == 0)
                {
                    return "Error";
                }
                if (colx[0] == 0)
                {
                    colx[1] = 10 * colx[1] + 10;
                }
                else if (colx[1] == 0)
                {
                    colx[0] = colx[0] * 10;
                    colx[1] = 719;
                }
                else if (colx[1] - colx[0] <= 0)
                {
                    return "Error";
                }
                else
                {
                    colx[0] = colx[0] * 10;
                    colx[1] = colx[1] * 10 + 10;
                }

                for (int level = 200; level > 100; level = level - 20)
                {
                    bit1 = new Threshold(level).Apply(bit);
                    //转换数据格式，提升都取效率
                    rawdata = BitToByte(bit1);

                    //对选出的数据进行行平均
                    var sumRow = new int[1280];
                    for (int i = 0; i < 1280; i++)
                    {
                        for (int j = colx[0]; j < colx[1]; j++)
                        {
                            sumRow[i] += (rawdata[j * 1280 + i] / 255);
                        }
                        sumRow[i] = (sumRow[i] > (colx[1] - colx[0]) / 2 ? 1 : 0);
                    }



                    //选取横向有效区域
                    var index = new List<int>();
                    for (int i = 0; i < 1279; i++)
                    {
                        if (sumRow[i] + sumRow[i + 1] == 1)
                        {
                            index.Add(sumRow[i] == 0 ? i : -i);
                        }
                    }

                    var lens = new List<int>();
                    for (int i = 0; i < index.Count() - 1; i++)
                    {
                        lens.Add(index[i + 1] + index[i]);
                    }

                    for (int i = 0; i < lens.Count(); i++)
                    {
                        if (lens[i] > 20 & lens[i] < 60)
                        {
                            lens.RemoveRange(0, i);
                            break;
                        }
                    }

                    for (int i = lens.Count() - 1; i > -1; i--)
                    {
                        if (lens[i] > 20 & lens[i] < 60)
                        {
                            lens.RemoveRange(i + 1, lens.Count() - i - 1);
                            break;
                        }
                    }
                    var step = 0;
                    for (int i = 0; i < lens.Count(); i++)
                    {
                        step += Math.Abs(lens[i]);
                    }

                    //if (step < 700) break;
                    step /= 24;

                    for (int i = 0; i < lens.Count(); i++)
                    {
                        var len = (int)((double)Math.Abs(lens[i]) / (double)step + 0.5);
                        result = "".PadLeft(len, lens[i] > 0 ? '1' : '0') + result;
                    }
                    if (result.Length == 24)
                    {
                        if (result.Substring(0, 2) + result.Substring(22, 2) == "1001")
                        {
                            result = Convert.ToInt32(result.Substring(2, 20), 2).ToString("X6");
                            break;
                        }
                    }
                    result = "";
                }
                if (result == "") return "Error";
                return result;
            }
            catch (Exception)
            {
                return "Error";
            }
        }

        private byte[] BitToByte(Bitmap bit)
        {
            BitmapData bmpdata = bit.LockBits(new System.Drawing.Rectangle(0, 0, bit.Width, bit.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);
            int stride = bmpdata.Stride;
            int offset = stride - bit.Width;
            IntPtr iptr = bmpdata.Scan0;
            int scanBytes = stride * bit.Height;
            byte[] mapdata = new byte[scanBytes];
            bit.UnlockBits(bmpdata);
            System.Runtime.InteropServices.Marshal.Copy(iptr, mapdata, 0, scanBytes);
            return mapdata;
        }

        private void videoSourcePlayer1_Click(object sender, EventArgs e)
        {
            if (videoSourcePlayer1.Size != new Size(233, 141))
            {
                videoSourcePlayer1.Size = new Size(233, 141);
                videoSourcePlayer1.Location = new Point(8, 422);
            }
            else
            {
                videoSourcePlayer1.Size = new Size(648, 428);
                videoSourcePlayer1.Location = new Point(8, 47);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Size == new Size(230, 160))
            {
                pictureBox1.Size = new Size(648, 428);
            }
            else
            {
                pictureBox1.Size = new Size(230, 160);
            }
        }

        private void buttonAutoQC_Click(object sender, EventArgs e)
        {
            buttonAutoQC.Text = buttonAutoQC.Text == "" ? "√" : "";
            UpdateAppConfig("QCAuto", buttonAutoQC.Text == "" ? "0" : "1");
        }

        private void buttonStartTH_Click(object sender, EventArgs e)
        {
            serialPort_DataSend(serialPortTH, "\x02" + "StartWork" + "\x03");
        }

        private void buttonGetIDCode_Click(object sender, EventArgs e)
        {
            if (ConfigRead("IDRead") == "1")
            {
                Log_Add("", false);
                Bitmap bit = videoSourcePlayer1.GetCurrentVideoFrame();
                var idcode = IDReader(bit);
                bit = new Grayscale(0.2125, 0.7154, 0.0721).Apply(bit);
                bit.Save("img/" + (idcode == "Error" ? "Error/" : "") + idcode + "-" + DateTime.Now.ToString("yyMMddHHmmss") + ".jpg", ImageFormat.Jpeg);
                labelIDRead.Text = idcode;
                Log_Add(idcode, false);
                Log_Add("", false);
            }
        }

        private void timerSupplyAlert_Tick(object sender, EventArgs e)
        {
            if (ConfigRead("FloatBallEnable") == "1")
            {
                if (labelLock.Text != "")
                {
                    buttonSupply.Font = new Font("微软雅黑", buttonSupply.Font.Size == 24 ? 18 : 24, FontStyle.Bold);
                    buttonSupply.ForeColor = buttonSupply.ForeColor == Color.Red ? Color.FromArgb(119, 114, 105) : Color.Red;
                }
                else
                {
                    buttonSupply.Font = new Font("微软雅黑", tabControlMainRight.SelectedTab == tabPageReagent ? 18 : 15, FontStyle.Bold);
                    buttonSupply.ForeColor = Color.FromArgb(119, 114, 105);
                }
                return;
            }
            else
            {

            }

            var supplyLeft = ConfigRead("SupplyLeft").Split('-');
            var supplyVolume = ConfigRead("SupplyVolume").Split('-');
            var wasteMode = ConfigRead("WasteMode");
            //当前使用率
            var dilutionUsage = double.Parse(supplyLeft[0]) / double.Parse(supplyVolume[0]) * 100;
            var cleanUsage = double.Parse(supplyLeft[1]) / double.Parse(supplyVolume[1]) * 100;
            var wasteUsage = double.Parse(supplyLeft[2]) / double.Parse(supplyVolume[2]) * 100;
            var wasteReagentUsage = double.Parse(supplyLeft[3]) / double.Parse(supplyVolume[3]) * 100;
            if (dilutionUsage < 10)
            {
                labelSupplyLeft.BackColor = labelSupplyLeft.BackColor == Color.Red ? Color.Yellow : Color.Red;
            }
            else
            {
                labelSupplyLeft.BackColor = Color.Transparent;
            }
            if (cleanUsage < 10)
            {
                labelSupplyLeft2.BackColor = labelSupplyLeft2.BackColor == Color.Red ? Color.Yellow : Color.Red;
            }
            else
            {
                labelSupplyLeft2.BackColor = Color.Transparent;
            }
            if (wasteMode == "0" & wasteUsage > 90)
            {
                labelSupplyLeft3.BackColor = labelSupplyLeft3.BackColor == Color.Red ? Color.Yellow : Color.Red;
            }
            else
            {
                labelSupplyLeft3.BackColor = Color.Transparent;
            }
            if (wasteReagentUsage > 90)
            {
                labelSupplyLeft4.BackColor = labelSupplyLeft4.BackColor == Color.Red ? Color.Yellow : Color.Red;
            }
            else
            {
                labelSupplyLeft4.BackColor = Color.Transparent;
            }

            if ((dilutionUsage < 10 | cleanUsage < 10 | wasteReagentUsage > 90 | (wasteMode == "0" & wasteUsage > 90)) & tabControlMainRight.SelectedTab == tabPageReagent)
            {
                buttonSupply.Font = new Font("微软雅黑", buttonSupply.Font.Size == 24 ? 18 : 24, FontStyle.Bold);
                buttonSupply.ForeColor = buttonSupply.ForeColor == Color.Red ? Color.FromArgb(119, 114, 105) : Color.Red;
            }
            else
            {
                buttonSupply.Font = new Font("微软雅黑", tabControlMainRight.SelectedTab == tabPageReagent ? 18 : 15, FontStyle.Bold);
                buttonSupply.ForeColor = Color.FromArgb(119, 114, 105);
            }
        }

        private void serialPortME_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var currentline = new StringBuilder();
            while (serialPortME.BytesToRead > 0)
            {
                int cmdbyte = serialPortME.ReadByte();
                if (cmdbyte == 0x02)
                    currentline.Append("<STX>");
                else if (cmdbyte == 0x03)
                    currentline.Append("<ETX>");
                else if (cmdbyte == 0x06)
                    currentline.Append("<ACK>");
                else if (cmdbyte == 0x15)
                    currentline.Append("<NAK>");
                else
                {
                    char ch = (char)cmdbyte;
                    currentline.Append(ch);
                }
            }
            //将串口接收到的数据存入主控端口数据变量
            _comMeStr += currentline.ToString();
            //对主串口接收的数据进行处理
            serialPortME_DataMakeUp();
        }

        private void serialPortME_DataMakeUp()
        {
            try
            {
                while (_comMeStr.IndexOf("<STX>", StringComparison.Ordinal) == 0 &
                       _comMeStr.IndexOf("<ETX>", StringComparison.Ordinal) > -1 &
                       _comMeStr.Length >= (_comMeStr.IndexOf("<ETX>", StringComparison.Ordinal) + 7))
                {
                    //提取开头到换行结尾的数据
                    var strTemp = _comMeStr.Substring(0, _comMeStr.IndexOf("<ETX>", StringComparison.Ordinal) + 7);
                    //更新主控端口数据字符，防止重复处理
                    _comMeStr = _comMeStr.Substring(_comMeStr.IndexOf("<ETX>", StringComparison.Ordinal) + 7);
                    //如果处理的数据含有*或者!则进行后续处理
                    serialPortME_DataDeal(strTemp);
                }
            }
            catch (Exception ee)
            {

                MessageBox.Show("5542" + ee.ToString());
            }
        }

        private void serialPortME_DataDeal(string strTemp)
        {
            try
            {
                Invoke(new Action(() =>
                {
                    var str = strTemp.Replace("<STX>", ((char)0x02).ToString());
                    str = str.Replace("<ETX>", ((char)0x03).ToString());
                    str = str.Replace("<ACK>", ((char)0x06).ToString());
                    str = str.Replace("<NAK>", ((char)0x15).ToString());
                    //
                    if (str[1] == ((char)0x06) | str[1] == ((char)0x15))
                    {
                        Invoke(new Action(() => PortLog("ASU", "R", strTemp)));
                    }
                    else
                    {

                        if (str.Substring(str.Length - 2) == LeftCheck(str.Substring(1, str.Length - 3)))
                        {
                            var str1 = ((char)0x02) + ((char)0x06).ToString() + str.Substring(1, 1) +
                                       str.Substring(2, 3) + ((char)0x03);
                            str1 = str1 + LeftCheck(str1.Substring(1));
                            byte[] strbyte = Encoding.ASCII.GetBytes(str1);
                            serialPortME.Write(strbyte, 0, strbyte.Length);
                            str1 = str1.Replace(((char)0x02).ToString(), "<STX>");
                            str1 = str1.Replace(((char)0x03).ToString(), "<ETX>");
                            str1 = str1.Replace(((char)0x06).ToString(), "<ACK>");
                            Invoke(new Action(() => PortLog("ASU", "R", strTemp)));
                            DealLater(strTemp);
                        }
                        else
                        {
                            var str1 = ((char)0x02) + ((char)0x15).ToString() + str.Substring(1, 1) +
                                       str.Substring(2, 3) + ((char)0x03);
                            str1 = str1 + LeftCheck(str1.Substring(1));
                            byte[] strbyte = Encoding.ASCII.GetBytes(str1);
                            Invoke(new Action(() => PortLog("ASU", "S", str1)));

                            serialPortME.Write(strbyte, 0, strbyte.Length);
                            str1 = str1.Replace(((char)0x02).ToString(), "<STX>");
                            str1 = str1.Replace(((char)0x03).ToString(), "<ETX>");
                            str1 = str1.Replace(((char)0x15).ToString(), "<ACK>");
                            Invoke(new Action(() => PortLog("ASU", "R", strTemp)));
                        }
                    }
                }
                    ));
            }
            catch (Exception ee)
            {
                MessageBox.Show("5597" + ee.ToString());
            }
        }

        private void DealLater(string strTemp)
        {
            _mEseq = strTemp.Substring(6, 3);
            switch (strTemp.Substring(5, 1))
            {
                //<STX>I00101<ETX>79
                case "I":
                    //当进入测试阶段方可判定为连接ok，可以进行后续测试
                    //要求种别
                    var type = strTemp.Substring(9, 2);
                    var str = "\x02" + "i" + _mEseq + (type == "01" ? "00" : "01") + "\x03";
                    str = str + LeftCheck(str.Substring(1));
                    var strbyte = Encoding.ASCII.GetBytes(str);
                    Invoke(new Action(() => PortLog("ASU", "S", str)));

                    serialPortME.Write(strbyte, 0, strbyte.Length);

                    break;
                case "A":
                    ASUComplete = false;
                    if (SqlData.SelectWorkRunlistforASUNum().Rows.Count == 0)
                    {
                        var ReagentID = SqlData.SwitchReagentStore().Rows[0][0].ToString();
                        serialPort_DataSend(serialPortMain,"#3011$" + ReagentID);
                    }
                    var mestatus = "";
                    mestatus = _otherInt[2] == 3 ? "01" : "00";
                    var linestatus = strTemp.Substring(9, 3);
                    linestatus = "00";
                    var strA = "\x02" + "a" + _mEseq + "00" + mestatus + linestatus + "\x03";
                    strA += LeftCheck(strA.Substring(1));
                    var strbyteA = Encoding.ASCII.GetBytes(strA);
                    Invoke(new Action(() => PortLog("ASU", "S", strA)));

                    serialPortME.Write(strbyteA, 0, strbyteA.Length);
                    break;
                case "O":
                    var type1 = strTemp.Substring(9, 2);
                    var sampleid1 = strTemp.Substring(11, 20).Replace(" ", "");
                    var shelfid1 = strTemp.Substring(31, 10);
                    var locationid1 = strTemp.Substring(41, 2);
                    if (sampleid1.Length != int.Parse(ConfigRead("BarcodeLength")))
                    {
                        type1 = "01";
                    }
                    var strO = "\x02" + "o" + _mEseq + type1 + sampleid1.PadRight(20) + "\x03";
                    strO += LeftCheck(strO.Substring(1));
                    var strbyteO = Encoding.ASCII.GetBytes(strO);
                    Invoke(new Action(() => PortLog("ASU", "S", strO)));
                    serialPortME.Write(strbyteO, 0, strbyteO.Length);

                    var testitem = SqlData.SelectProductIdItemIdByname(labelNextTestItem.Text.ToString()).Rows[0][1].ToString();
                    var TyFixStr = ConfigRead("COMSFix");
                    DataTable ASU = SqlData.SelectASUmessage();
                    var ReagentStoreID = ASU.Rows[0][0].ToString();
                    var DilutionRatio = ASU.Rows[0][1].ToString();
                    var ReactionTime = ASU.Rows[0][2].ToString();
                    var CalibDataID = ASU.Rows[0][3].ToString();
                    //写入待测列表中
                    if (sampleid1.Length == int.Parse(ConfigRead("BarcodeLength")))
                    {
                        SqlData.InsertIntoRunlist(seqtemp.ToString(), sampleid1, testitem, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "ASU传送", TyFixStr,
                            ReagentStoreID, DilutionRatio, ReactionTime, CalibDataID);
                        Log_Add("接收到ASU样本信息，样本号为" + sampleid1, false);
                        seqtemp--;
                        UpdatedataGridViewMain("Doing");
                    }
                    else
                    {
                        SqlData.InsertIntoRunlist(seqtemp.ToString(), sampleid1, testitem, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "条码长度错误", TyFixStr,
                            ReagentStoreID, DilutionRatio, ReactionTime, CalibDataID);
                        var seq = SqlData.SelectBarcodeError(sampleid1).Rows[0][0].ToString();
                        DrawResult("-16", seq, "", "", "");
                        Log_Add("接收到ASU样本信息，样本号为" + sampleid1 + "样本号长度不正确", false);
                        seqtemp--;
                        UpdatedataGridViewMain("Doing");
                    }
                    break;
                case "S":
                    var type2 = strTemp.Substring(9, 2);
                    var sampleid2 = strTemp.Substring(11, 20).Replace(" ", "");
                    var shelfid2 = strTemp.Substring(31, 10);
                    var locationid2 = strTemp.Substring(41, 2);
                    var strS = "\x02" + "s" + _mEseq + type2 + sampleid2.PadRight(20) + "\x03";
                    strS += LeftCheck(strS.Substring(1));
                    tempSend = strS;
                    break;
            }
        }

        bool SampleNormal = true;
        private void timerSampleReady_Tick(object sender, EventArgs e)
        {
            var tempbyte = Encoding.ASCII.GetBytes(tempSend);
            Invoke(new Action(() => PortLog("ASU", "S", tempSend)));
            if (ConfigRead("ASUEnable") == "1")
            {
                serialPortME.Write(tempbyte, 0, tempbyte.Length);
                tempSend = "";
            }
            timerSampleReady.Stop();
            //判断ASU是否已经测试完成
            if (ASUComplete == false)
            {
                DataTable dt = SqlData.SelectWorkRunlistforASU();
                if (dt.Rows.Count == 0)
                {
                    ASUComplete = true;
                }
            }
            if (!SampleNormal)
                sampleready = true;
        }

        private void 修改压积ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rowIndex = int.Parse(CounterText);
            var firstchar = ' ';
            if (tabControlMain.SelectedTab == tabPageHome)
            {
                firstchar = dataGridViewMain.Rows[rowIndex].Cells[3].Value.ToString()[0];
            }
            else
            {
                firstchar = dataGridViewSearch.Rows[rowIndex].Cells[2].Value.ToString()[0];
            }
            if (firstchar == '*')
            {
                Log_Add("样本压积修正只可进行一次", true); return;
            }
            else if (firstchar == '>' | firstchar == '<')
            {
                Log_Add("此样本不可进行压积修正", true); return;
            }
            else
            {
                DateTime time;
                if (tabControlMain.SelectedTab == tabPageHome)
                {
                    dataGridViewMain.Rows[rowIndex].Selected = true;
                    time = DateTime.Parse(dataGridViewMain.Rows[rowIndex].Cells[0].Value.ToString());
                }
                else
                {
                    dataGridViewSearch.Rows[rowIndex].Selected = true;
                    time = DateTime.Parse(dataGridViewSearch.Rows[rowIndex].Cells[3].Value.ToString());
                }

                string unitratio = SqlData.SelectUnitRatio(time.ToString("yyyy-MM-dd HH:mm:ss")).Rows[0][0].ToString();
                if (unitratio == "1")
                {
                    Log_Add("非全血样本不可进行压积修正", true);
                }
                else
                {
                    string yaji = (1.0 - 1.0 / double.Parse(unitratio)).ToString("F2");
                    CounterText = "Num|" + yaji;
                    var str = VirtualKeyBoard();
                    if (str != "" & str != "Cancel" & str != unitratio)
                    {
                        if (double.Parse(str) > 1)
                        {
                            ShowMyAlert("红细胞压积不能大于1"); return;
                        }
                        str = (1.0 / (1.0 - double.Parse(str))).ToString();
                        SqlData.UpdateResultYaji(time.ToString("yyyy-MM-dd HH:mm:ss"), unitratio, str);
                        UpdatedataGridViewMain("Done");
                        UpdatedataGridViewSearch("1", "12");
                    }
                }
            }
        }

        List<string> cmdlist = new List<string>();
        private void timerMainPort_Tick(object sender, EventArgs e)
        {
            try
            {
                if (cmdlist.Count == 0) return;
                var strCmd = cmdlist[0];
                if (strCmd == "") { PortLog("Main", "S", "-----Null-----"); cmdlist.RemoveAt(0); return; }
                if (strCmd == "TIMESleep")
                {
                    cmdlist.RemoveAt(0); return;
                }
                var frontstr = "";
                var backstr = "";
                if (strCmd != "\x06")
                {

                    frontstr = "\x02";
                    backstr = "\x0d\x03" + MainPortCheck(strCmd + "\x0d");

                    serialPort_DataDeal(strCmd, "MainSend");
                }
                else
                {
                    cmdlist.RemoveAt(0);
                }
                PortLog("Main", "S", frontstr + strCmd + backstr);

                //荧光发送数据的规则是:0003....\r\n  :0006....\r\n
                var writeBuffer = Encoding.ASCII.GetBytes(frontstr + strCmd + backstr);
                serialPortMain.Write(writeBuffer, 0, writeBuffer.Length);
            }
            catch (Exception ee)
            {
                Log_Add(ee.Message, true);
            }
        }


        private void buttonFluoTest_Click(object sender, EventArgs e)
        {
            FluoNewTest("0");
        }

        private void LogSave(string filename, string data)
        {

            if (!Directory.Exists(Application.StartupPath + "/logdata"))
            {
                Directory.CreateDirectory(Application.StartupPath + "/logdata");
            }
            if (File.Exists(Application.StartupPath + "/logdata/" + filename))
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "/logdata/" + filename, Encoding.UTF8);
                data += sr.ReadToEnd();
                sr.Close();
                File.Delete(Application.StartupPath + "/logdata/" + filename);
            }
            StreamWriter sw = new StreamWriter(Application.StartupPath + "/logdata/" + filename, true, Encoding.UTF8);
            sw.Write(data);

            sw.Close();
        }
        public static string messstr = "";

        private void MessageboxShow(string str)
        {
            if (_myMsab != null)
            {
                if (!_myMsab.IsDisposed)
                    return;
            }
            try
            {
                messstr = str;
                _myMsab = new FormMessageBox { Owner = this };
                _myMsab.Show();
            }
            catch (Exception ee)
            {
                Log_Add("6224" + ee.Message, false);
            }
        }

        private void MessageboxShow(string str, bool confirm)
        {
            if (confirm == true)
                MessageType[0] = "1";
            if (_myMsab != null)
            {
                if (!_myMsab.IsDisposed)
                    return;
            }
            try
            {
                messstr = str;
                _myMsab = new FormMessageBox { Owner = this };
                _myMsab.ShowDialog();
            }
            catch (Exception ee)
            {
                Log_Add("6225" + ee.Message, false);
            }
        }

        private void serialPortFloatBall_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //主串口发送的信息以<STX><ETX>开头结尾
            while (serialPortFloatBall.BytesToRead > 0)
            {
                var ch = (char)serialPortFloatBall.ReadByte();
                _comStr[5] += ch.ToString();
                if (ch == '\n')
                {
                    Invoke(new Action(() =>
                    {
                        while (_comStr[5].IndexOf("\r\n") > -1)
                        {
                            var str = _comStr[5].Substring(0, _comStr[5].IndexOf("\r\n"));
                            _comStr[5] = _comStr[5].Substring(_comStr[5].IndexOf("\r\n") + 2);
                            if (str.IndexOf("*3301") > -1 & str.Split('$').Length == 4)
                            {
                                Invoke(new Action(() => Log_Add("FloatBall:" + str, false)));
                                var wastestatus = str.Split('$')[1];
                                var dilutionstatus = str.Split('$')[2];
                                var cleanstatus = str.Split('$')[3];
                                if (wastestatus == "1")
                                {
                                    labelFloatBallWaste.Text = "废液\r\n\r\n" + "警报";
                                    labelWasteStatus.BackColor = Color.Red;
                                    labelFloatBallWaste.BackColor = Color.Red;
                                    labelSupplyLeft3.Text = "废液：×";
                                    if (labelLock.Text == "")
                                    {
                                        serialPort_DataSend(serialPortMain, "#3053$1");
                                        labelLock.Text = "仪器锁定";
                                        Log_Add("请清空废液后继续操作，当前仪器锁定", true);
                                        ShowMyAlert("请清空废液后继续操作，\r\n当前仪器锁定");
                                    }
                                }
                                else
                                {
                                    labelFloatBallWaste.Text = "废液\r\n\r\n" + "正常";
                                    labelWasteStatus.BackColor = Color.FromArgb(41, 169, 223);
                                    labelFloatBallWaste.BackColor = Color.FromArgb(41, 169, 223);
                                    labelSupplyLeft3.Text = "废液：√";
                                }
                                var volume = ConfigRead("FloatBallVolume").Split('-');
                                var count = ConfigRead("FloatBallCount").Split('-');

                                if (dilutionstatus == "1")
                                {
                                    if (int.Parse(count[0]) > int.Parse(volume[0]))
                                        count[0] = volume[0];
                                    labelFloatBallDilution.Text = "稀释液\r\n\r\n" + count[0] + "Test\r\n\r\n" + "点击灌注";
                                    labelFloatBallDilution.BackColor = Color.Red;
                                    labelDilutionStatus.BackColor = Color.Red;
                                    labelSupplyLeft.Text = "稀释液:×";
                                }
                                else
                                {
                                    if (int.Parse(count[0]) <= int.Parse(volume[0]))
                                        count[0] = (int.Parse(volume[0]) + 1).ToString();
                                    labelFloatBallDilution.Text = "稀释液\r\n\r\n正常\r\n\r\n" + "点击灌注";
                                    labelFloatBallDilution.BackColor = Color.FromArgb(41, 169, 223);
                                    labelDilutionStatus.BackColor = Color.FromArgb(41, 169, 223);
                                    labelSupplyLeft.Text = "稀释液:√";
                                }

                                if (cleanstatus == "1")
                                {
                                    if (int.Parse(count[1]) > int.Parse(volume[1]))
                                        count[1] = volume[1];
                                    labelFloatBallClean.Text = "清洗液\r\n\r\n" + count[1] + "Test\r\n\r\n" + "点击灌注";
                                    labelFloatBallClean.BackColor = Color.Red;
                                    labelCleanStatus.BackColor = Color.Red;
                                    labelSupplyLeft2.Text = "清洗液:×";
                                }
                                else
                                {
                                    if (int.Parse(count[1]) <= int.Parse(volume[1]))
                                        count[1] = (int.Parse(volume[1]) + 1).ToString();
                                    labelFloatBallClean.Text = "清洗液\r\n\r\n正常\r\n\r\n" + "点击灌注";
                                    labelFloatBallClean.BackColor = Color.FromArgb(41, 169, 223);
                                    labelCleanStatus.BackColor = Color.FromArgb(41, 169, 223);
                                    labelSupplyLeft2.Text = "清洗液:√";
                                }

                                if (labelLock.Text != "")
                                {
                                    if (wastestatus == "0" & count[0] != "0" & count[1] != "0" & ConfigRead("SupplyVolume").Split('-')[3] != ConfigRead("SupplyLeft").Split('-')[3])
                                    {
                                        serialPort_DataSend(serialPortMain, "#3053$0");
                                        labelLock.Text = "";
                                        Log_Add("仪器解锁，可以继续测试", true);
                                    }
                                }

                                UpdateAppConfig("FloatBallCount", count[0] + "-" + count[1]);
                            }
                        }
                    }));
                }
            }
        }

        private void labelFloatBallDilution_Click(object sender, EventArgs e)
        {
            if(labelFloatBallDilution.Text.IndexOf("点击")>-1)
            serialPort_DataSend(serialPortMain, "#3060");
        }

        private void labelFloatBallClean_Click(object sender, EventArgs e)
        {
            serialPort_DataSend(serialPortMain, "#3061");
        }

        private void 发送结果ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rowIndex = int.Parse(CounterText);
            var homeorsearch = tabControlMain.SelectedTab == tabPageHome;

            var sampleNo = homeorsearch ? dataGridViewMain.Rows[rowIndex].Cells[1].Value.ToString(): dataGridViewSearch.Rows[rowIndex].Cells[0].Value.ToString();

            var testitemname = homeorsearch ? dataGridViewMain.Rows[rowIndex].Cells[2].Value.ToString() : dataGridViewSearch.Rows[rowIndex].Cells[1].Value.ToString();

            var result = homeorsearch ? dataGridViewMain.Rows[rowIndex].Cells[3].Value.ToString() : dataGridViewSearch.Rows[rowIndex].Cells[2].Value.ToString();

            var createtime= homeorsearch? dataGridViewMain.Rows[rowIndex].Cells[0].Value.ToString(): dataGridViewSearch.Rows[rowIndex].Cells[3].Value.ToString();

            result = result.Replace("*", "");

            exportResult(sampleNo, testitemname, result.Substring(0,result.IndexOf(".")+3), createtime);

            Log_Add("该条结果发送成功", true);
        }


        //2017-2-27
        //显示所有用户信息
        private void ShowAllUsers()
        {
            try
            {
                DataTable dat = new DataTable();
                dat = SqlData.SelectallUserName();
                dat.Columns.Add("type", typeof(string));
                for (int i = 0; i < dat.Rows.Count; i++)
                {
                    if (dat.Rows[i][2].ToString() == "0")
                    {
                        dat.Rows[i][3] = "操作员";
                    }
                    else if (dat.Rows[i][2].ToString() == "1")
                    {
                        dat.Rows[i][3] = "管理员";
                    }
                }
                dataGridViewAllUsers.DataSource = dat;
                dataGridViewAllUsers.Columns[2].Visible = false;
                dataGridViewAllUsers.Columns[0].Width = 125;
                dataGridViewAllUsers.Columns[1].Width = 125;
                dataGridViewAllUsers.Columns[3].Width = 125;
                dataGridViewAllUsers.AllowUserToAddRows = false;
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
        }
        
        //点击用户列表事件
        private void dataGridViewAllUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxNewUserName.ReadOnly = true;
            comboBoxUserType.Enabled = false;
            comboBoxUserType.Text = "";
            DataTable dat = new DataTable();
            buttonUserDelete.Enabled = true;
            buttonUserChange.Enabled = true;
            buttonUserChange.Visible = true;
            buttonUserDelete.Visible = true;
            int i = e.RowIndex;
            var x = dataGridViewAllUsers.Rows[i].Cells[0].Value.ToString();
            dat = SqlData.SelectOneUserName(x);
            textBoxNewUserName.Text = dat.Rows[0][0].ToString();
            buttonUserChange.Text = "更改";
        }//2017-2-24

        //2017-2-27
        //用户设置界面恢复初始状态
        private void SetUsersetting()
        {
            textBoxNewPassword.Clear();
            textBoxNewUserName.Clear();
            textBoxRepeatPassword.Clear();
            buttonCheckAllUsers.Visible = true;
            buttonUserDelete.Enabled = false;
            buttonUserDelete.Visible = false;
            buttonUserChange.Text = "新建";
            textBoxNewUserName.ReadOnly = false;
            comboBoxUserType.Enabled = true;
            comboBoxUserType.Text = "";
        }//2017-2-27
        

        private void textBoxOpenState_TextChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        int Click_num = 0;
        private void textBoxOpenState_MouseClick(object sender, MouseEventArgs e)
            //加载界面状态显示
        {
            if (Click_num == 0)
            {
                textBoxOpenState.Height = 280;
                textBoxOpenState.Location = new Point(215, 190);
                Click_num = Click_num + 1;
            }
            else if (Click_num == 1)
            {
                if (textBoxOpenState.ForeColor == Color.Red)
                {
                    textBoxOpenState.Height = 80;
                    textBoxOpenState.Location = new Point(215, 430);
                    Click_num = 0;
                }
                else
                {
                    textBoxOpenState.Height = 40;
                    textBoxOpenState.Location = new Point(215, 430);
                    Click_num = 0;
                }
            }
            textBoxOpenState.SelectionStart = 0;
            textBoxOpenState.ScrollToCaret();
            textBox1.Focus();
        }

        private void timerSampleStart_Tick(object sender, EventArgs e)
        {
            timerSampleStart.Stop();
            serialPort_DataSend(serialPortMain, "#3051");
        }
    }

}