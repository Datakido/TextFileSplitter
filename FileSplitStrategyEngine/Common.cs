using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SystemWidgets.FileSplitStrategyEngine
{
    using System.Globalization;
    using System.IO;
    using System.Text;

    /// <exclude/>
    public class Common
    {
        /// <summary>
        /// Gets the large icon.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public static Icon GetLargeIcon(string fileName)
        {
            // Get the large generic file icon.
            var shfi = new Shell32.SHFILEINFO();
            Shell32.SHGetFileInfo(fileName, Shell32.FILE_ATTRIBUTE_NORMAL, ref shfi, Convert.ToUInt32(Marshal.SizeOf(shfi)), Convert.ToUInt32(Convert.ToInt32(Shell32.SHGFI_USEFILEATTRIBUTES) | Convert.ToInt32(Shell32.SHGFI_ICON)));

            Icon icon = Icon.FromHandle(shfi.hIcon);

            return icon;
        }

        /// <summary>
        /// Determines whether the specified input string is integer.
        /// </summary>
        /// <param name="inputString">The input string.</param>
        /// <returns>
        ///   <c>true</c> if the specified input string is integer; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsInteger(string inputString)
        {
            int number;

            bool isNumber = int.TryParse(inputString, out number);

            return isNumber;
        }

        /// <summary>
        /// Gets the default file pattern.
        /// </summary>
        public static string DefaultFilePattern
        {
            get { return "[FILENAME]-[SEQUENCE:0].[EXT]"; }
        }

        /// <summary>
        /// Gets the version number.
        /// </summary>
        public static string VersionNumber
        {
            get
            {
                string major = Assembly.GetExecutingAssembly().GetName().Version.Major.ToString(CultureInfo.InvariantCulture);
                string minor = Assembly.GetExecutingAssembly().GetName().Version.Minor.ToString(CultureInfo.InvariantCulture);
                string build = Assembly.GetExecutingAssembly().GetName().Version.Build.ToString(CultureInfo.InvariantCulture);

                return major + "." + minor + "." + build;
            }
        }

        /// <summary>
        /// Detects the byte order mark of a file and returns
        /// an appropriate encoding for the file.
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <returns>A TextEncodingMetadata object</returns>
        public static TextEncodingMetadata GetFileEncoding(string sourceFile)
        {
            var metaData = new TextEncodingMetadata();

            EncodingMetaInfo metaInfo = DetectBom(sourceFile);

            metaData.HasBom = metaInfo.HasBom;

            //using (FileStream fs = File.OpenRead(sourceFile))
            //{
            //    var cdet = new Ude.CharsetDetector();
            //    cdet.Feed(fs);
            //    cdet.DataEnd();

            //    if (cdet.Charset != null)
            //    {
            //        metaData.CharacterSet = cdet.Charset;
            //        metaData.DetectionConfidence = cdet.Confidence;
            //    }
            //    else
            //    {
            //        Console.WriteLine("Detection failed.");
            //    }
            //}

            using (var sr = new StreamReader(sourceFile))
            {
                Encoding encoding = sr.CurrentEncoding;
                metaData.FileEncoding = encoding;
            }

            return metaData;
        }

        /// <summary>
        /// Detects the bom.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>An EncodingMetaInfo object</returns>
        public static EncodingMetaInfo DetectBom(string filePath)
        {
            var metaInfo = new EncodingMetaInfo();

            // *** Use Default of Encoding.Default (Ansi CodePage)
            Encoding enc = Encoding.Default;

            // *** Detect byte order mark if any - otherwise assume default

            var buffer = new byte[5];
            var file = new FileStream(filePath, FileMode.Open);

            file.Read(buffer, 0, 5);
            file.Close();

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
            {
                metaInfo.HasBom = true;
                metaInfo.TextEncoding = Encoding.UTF8;
            }
            else if (buffer[0] == 0xff && buffer[1] == 0xfe)
            {
                metaInfo.HasBom = true;
                metaInfo.TextEncoding = Encoding.Unicode; // utf-16le
            }
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
            {
                metaInfo.HasBom = true;
                metaInfo.TextEncoding = Encoding.BigEndianUnicode; // utf-16be
            }
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
            {
                metaInfo.HasBom = true;
                metaInfo.TextEncoding = Encoding.UTF32;
            }
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
            {
                metaInfo.HasBom = true;
                metaInfo.TextEncoding = Encoding.UTF7;
            }

            return metaInfo;
        }
    }

    /// <exclude/>
    public class Shell32
    {
        /// <summary>
        /// Maximum string path
        /// </summary>
        public const int MAX_PATH = 256;

        [StructLayout(LayoutKind.Sequential)]
        public struct SHITEMID
        {
            [MarshalAs(UnmanagedType.LPArray)]
            public ushort cb;
            public byte[] abID;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ITEMIDLIST
        {
            public SHITEMID mkid;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BROWSEINFO
        {
            public IntPtr hwndOwner;
            public IntPtr pidlRoot;
            public IntPtr pszDisplayName;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszTitle;
            public uint ulFlags;
            public IntPtr lpfn;
            public int lParam;
            public IntPtr iImage;
        }

        // Browsing for directory.
        // Note: Function System.Convert.ToUInt32 is not CLS Compliant.
        public static uint BIF_RETURNONLYFSDIRS = Convert.ToUInt32(1);
        public static uint BIF_DONTGOBELOWDOMAIN = Convert.ToUInt32(2);
        public static uint BIF_STATUSTEXT = Convert.ToUInt32(4);
        public static uint BIF_RETURNFSANCESTORS = Convert.ToUInt32(8);
        public static uint BIF_EDITBOX = Convert.ToUInt32(16);
        public static uint BIF_VALIDATE = Convert.ToUInt32(32);
        public static uint BIF_NEWDIALOGSTYLE = Convert.ToUInt32(64);
        public static uint BIF_USENEWUI = Convert.ToUInt32(80);
        public static uint BIF_BROWSEINCLUDEURLS = Convert.ToUInt32(128);
        public static uint BIF_BROWSEFORCOMPUTER = Convert.ToUInt32(4096);
        public static uint BIF_BROWSEFORPRINTER = Convert.ToUInt32(8192);
        public static uint BIF_BROWSEINCLUDEFILES = Convert.ToUInt32(16384);
        public static uint BIF_SHAREABLE = Convert.ToUInt32(32768);

        public string szDisplayName;

        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        // Get Icon
        public static uint SHGFI_ICON = Convert.ToUInt32(256);
        // Get Display Name
        public static uint SHGFI_DISPLAYNAME = Convert.ToUInt32(512);
        // Get Type Name
        public static uint SHGFI_TYPENAME = Convert.ToUInt32(1024);
        // Get Attributes
        public static uint SHGFI_ATTRIBUTES = Convert.ToUInt32(2048);
        // Get Icon Location
        public static uint SHGFI_ICONLOCATION = Convert.ToUInt32(4096);
        // Return Executable Type
        public static uint SHGFI_EXETYPE = Convert.ToUInt32(8192);
        // Get System Icon Index
        public static uint SHGFI_SYSICONINDEX = Convert.ToUInt32(16384);
        // Put a link overlay on the icon.
        public static uint SHGFI_LINKOVERLAY = Convert.ToUInt32(32768);
        // Show the icon in the selected state.
        public static uint SHGFI_SELECTED = Convert.ToUInt32(65536);
        // Get only specified attributes.
        public static uint SHGFI_ATTR_SPECIFIED = Convert.ToUInt32(131072);
        // Get Large Icon
        public static uint SHGFI_LARGEICON = Convert.ToUInt32(0);
        // Get Small Icon
        public static uint SHGFI_SMALLICON = Convert.ToUInt32(1);
        // Get Open Icon
        public static uint SHGFI_OPENICON = Convert.ToUInt32(2);
        // Get Shell Size Icon
        public static uint SHGFI_SHELLICONSIZE = Convert.ToUInt32(4);
        // The pszPath is a PIDL.
        public static uint SHGFI_PIDL = Convert.ToUInt32(8);
        // Use Passed dwFileAttribute
        public static uint SHGFI_USEFILEATTRIBUTES = Convert.ToUInt32(16);
        // Apply the appropriate overlays.
        public static uint SHGFI_ADDOVERLAYS = Convert.ToUInt32(32);
        // Get the index of the overlay.
        public static uint SHGFI_OVERLAYINDEX = Convert.ToUInt32(64);

        public static uint FILE_ATTRIBUTE_DIRECTORY = Convert.ToUInt32(16);
        public static uint FILE_ATTRIBUTE_NORMAL = Convert.ToUInt32(128);

        [DllImport("Shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        [DllImport("Shell32.dll")]
        public static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpbi);

        [DllImport("Shell32.dll")]
        public static extern uint SHGetPathFromIDList(IntPtr pidl, IntPtr pszPath);
    }

    /// <exclude/>
    public class User32
    {
        [DllImport("User32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);
    }
}