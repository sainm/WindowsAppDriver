using System;
using System.Runtime.InteropServices;

namespace EngineLib.Core
{
    public static class RotationManager
    {
        private const int CurrentSettingsMode = -1;


        [StructLayout(LayoutKind.Sequential)]
        struct POINTL
        {
            [MarshalAs(UnmanagedType.I4)] public int x;
            [MarshalAs(UnmanagedType.I4)] public int y;
        }

        [StructLayout(LayoutKind.Sequential,
            CharSet = CharSet.Ansi)]
        struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;

            [MarshalAs(UnmanagedType.U2)] public UInt16 dmSpecVersion;

            [MarshalAs(UnmanagedType.U2)] public UInt16 dmDriverVersion;

            [MarshalAs(UnmanagedType.U2)] public UInt16 dmSize;

            [MarshalAs(UnmanagedType.U2)] public UInt16 dmDriverExtra;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmFields;

            public POINTL dmPosition;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmDisplayOrientation;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmDisplayFixedOutput;

            [MarshalAs(UnmanagedType.I2)] public Int16 dmColor;

            [MarshalAs(UnmanagedType.I2)] public Int16 dmDuplex;

            [MarshalAs(UnmanagedType.I2)] public Int16 dmYResolution;

            [MarshalAs(UnmanagedType.I2)] public Int16 dmTTOption;

            [MarshalAs(UnmanagedType.I2)] public Int16 dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;

            [MarshalAs(UnmanagedType.U2)] public UInt16 dmLogPixels;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmBitsPerPel;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmPelsWidth;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmPelsHeight;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmDisplayFlags;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmDisplayFrequency;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmICMMethod;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmICMIntent;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmMediaType;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmDitherType;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmReserved1;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmReserved2;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmPanningWidth;

            [MarshalAs(UnmanagedType.U4)] public UInt32 dmPanningHeight;
        }


        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean EnumDisplaySettings(
            [param: MarshalAs(UnmanagedType.LPTStr)]
            string lpszDeviceName,
            [param: MarshalAs(UnmanagedType.U4)] int iModeNum,
            [In, Out] ref DEVMODE lpDevMode);

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.I4)]
        private static extern int ChangeDisplaySettings(
            [In, Out] ref DEVMODE lpDevMode,
            [param: MarshalAs(UnmanagedType.U4)] uint dwflags);


        public static DisplayOrientation GetCurrentOrientation()
        {
            var currentSettings = new DEVMODE();
            currentSettings.dmSize = (ushort) Marshal.SizeOf(currentSettings);

            EnumDisplaySettings(null, CurrentSettingsMode, ref currentSettings);

            return (DisplayOrientation) currentSettings.dmDisplayOrientation;
        }


        public static int SetOrientation(DisplayOrientation orientation)
        {
            var currentSettings = new DEVMODE();
            currentSettings.dmSize = (ushort) Marshal.SizeOf(currentSettings);

            EnumDisplaySettings(null, CurrentSettingsMode, ref currentSettings);

            if (currentSettings.dmDisplayOrientation == (int) orientation)
            {
                return 0;
            }

            var newSettings = currentSettings;

            newSettings.dmDisplayOrientation = (uint) orientation;

            var bigDimension = Math.Max(newSettings.dmPelsHeight, newSettings.dmPelsWidth);
            var smallDimension = Math.Min(newSettings.dmPelsHeight, newSettings.dmPelsWidth);

            if (orientation == DisplayOrientation.LANDSCAPE)
            {
                newSettings.dmPelsHeight = smallDimension;
                newSettings.dmPelsWidth = bigDimension;
            }
            else
            {
                newSettings.dmPelsHeight = bigDimension;
                newSettings.dmPelsWidth = smallDimension;
            }

            return ChangeDisplaySettings(ref newSettings, 0);
        }
    }
}