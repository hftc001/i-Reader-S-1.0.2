using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace i_Reader_S
{
    public partial class FormAlert : Form
    {
        //private const int AW_VER_POSITIVE = 0x0004;//自顶向下显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        //private const int AW_VER_NEGATIVE = 0x0008;//自下向上显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志该标志
        // private const int AW_CENTER = 0x0010;//若使用了AW_HIDE标志，则使窗口向内重叠；否则向外扩展
        // private const int AW_HIDE = 0x10000;//隐藏窗口
        //private const int AW_ACTIVE = 0x20000;//激活窗口，在使用了AW_HIDE标志后不要使用这个标志
        //private const int AW_SLIDE = 0x40000;//使用滑动类型动画效果，默认为滚动动画类型，当使用AW_CENTER标志时，这个标志就被忽略
        private const int AwBlend = 0x80000;

        //下面是可用的常量，根据不同的动画效果声明自己需要的
        //private const int AW_HOR_POSITIVE = 0x0001;//自左向右显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志
        private const int AwHorNegative = 0x0002;

        public FormAlert()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体动画函数    注意：要引用System.Runtime.InteropServices;
        /// </summary>
        /// <param name="hwnd">指定产生动画的窗口的句柄</param>
        /// <param name="dwTime">指定动画持续的时间</param>
        /// <param name="dwFlags">指定动画类型，可以是一个或多个标志的组合。</param>
        /// <returns></returns>
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        //自右向左显示窗口，该标志可以在滚动动画和滑动动画中使用。使用AW_CENTER标志时忽略该标志

        //使用淡入淡出效果

        private void btnMessageClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormTest_Load(object sender, EventArgs e)
        {
            lblMessage.Text = ReaderS.CounterText;
            int x = 1024 - Width;
            int y = 150;
            Location = new Point(x, y);//设置窗体在屏幕右下角显示
            AnimateWindow(Handle, 1000, AwBlend | AwHorNegative);
            timer1.Interval = 3000;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Close();
        }
    }
}