using System;
using System.Runtime.InteropServices;
using System.Text;

namespace i_Reader_S
{
    internal class CCam
    {
        //设置默认参数
        public const int Recv = 0x0400 + 101;
        public const int Trigger = 0x0400 + 103;
        //设置曝光时间范围，单位毫秒
        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraGetExposureTimeMS(ref float pExposureTime);

        //保存一副TIF格式的图片，filename保存路径及名称，例如“d:/hello.tif”，pRGBData为将要保存的RGB格式数据，wide，height图片尺寸，bDataWide数据位宽度，根据保存的pRGBData是24BIT还是48BIT而定，24为false,48则为true。
        //获取当前自动曝光目标值
        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraGetExposureTimeRange(ref float pMinVal, ref float pMaxVal, ref float pStepVal);

        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraGetFrameCount(ref int icount);

        //获取指定的分辨率 名称，index从0~icount-1;可参考DEMO中的代码。str为返回的字符串，例如“1024X768”
        //获取相机支持的帧速个数
        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraGetFrameItemName(int index, StringBuilder str);

        //获取一帧RGB数据，pRGB48 是16BIT下才能带出的，BGR格式。
        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraGetImageData(byte[] pRaw, ref bool bDataWide);

        //获取相机每个帧率的名称。例如“High”,"Normal"等
        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraGetImageSize(ref int pWidth, ref int pHeight);

        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraGetResCount(ref int icount);

        //设置数据位，true为16BIT，false为8BIT
        //获取相机支持的分辨率个数，icount为返回变量
        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraGetResItemName(int index, StringBuilder str);

        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraInit(IntPtr callback, byte uiResolution, IntPtr hWndDisplay, IntPtr hReceive);

        //相机初始化，第一个使用的函数，callback回调函数，uiResolution分辨率，hWndDisplay显示窗口句柄，hReceive接收消息的窗口句柄

        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraPlay();

        //获取帧率大小
        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraSetDataWide(bool bDataWide);

        //停止相机
        //获取曝光时间范围
        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraSetExposureTimeMS(float uiExposureTime);

        //获取镜像的启用情况
        [DllImport("HSDK.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern TsCameraStatus CameraSetFrameSpeed(byte uiSpeed);
    }
}

internal enum TsCameraStatus
{
    StatusOk = 1,                         //动作成功
    StatusInternalError = 0,             //内部错误
    StatusNoDeviceFind = -1,            //没有发现相机
    StatusNotEnoughSystemMemory = -2,  //没有足够系统内存
    StatusHwIoError = -3,               //硬件IO错误
    StatusParameterInvalid = -4,         //参数无效
    StatusParameterOutOfBound = -5,    //参数越界
    StatusFileCreateError = -6,         //创建文件失败
    StatusFileInvalid = -7,              //文件格式无效
}
