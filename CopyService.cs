using System;
using System.Runtime.InteropServices;
using System.IO;
using NetDimension.NanUI.Resource.Data;
using NetDimension.NanUI.Browser.ResourceHandler;

using OSAPI;
using OSAPI.Struct;

public class CopyService : DataService
{
    [DllImport("kernel32.dll")]
    public static extern DriveType GetDriveType([MarshalAs(UnmanagedType.LPStr)] string lpRootPathName);
    private CopyProgressResult CopyProgressHandler(long total, long transferred, long streamSize, long StreamByteTrans, uint dwStreamNumber, CopyProgressCallbackReason reason, IntPtr hSourceFile, IntPtr hDestinationFile, IntPtr lpData)
    {
        return CopyProgressResult.PROGRESS_CONTINUE;
    }
    public ResourceResponse DirCopy(ResourceRequest request)
    {
        [DllImport("shell32.dll")]
        static extern IntPtr SHBrowseForFolder(ref BROWSEINFO lpbi);
        IntPtr pidl = IntPtr.Zero;
        BROWSEINFO bi = new BROWSEINFO();
        bi.lpszTitle = "hello";

        pidl =  SHBrowseForFolder(ref bi);

        return Text(pidl.ToString());
    }

    public ResourceResponse FileCopy(ResourceRequest request)
    {
        [DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GetOpenFileName(ref OpenFileName ofn);

        string dst = "";

        for (char drv = 'A'; drv <= 'Z'; drv++)
        {
            DriveType dt = GetDriveType(drv.ToString() + ":\\");
            if ((int)dt == 2)
            {
                dst += drv.ToString() + ":\\";
                break;
            }
        }

        var ofn = new OpenFileName();
        ofn.lStructSize = Marshal.SizeOf(ofn);
        ofn.lpstrFilter = "All files(*.*)\0\0";
        ofn.lpstrFile = new string(new char[256]);
        ofn.nMaxFile = ofn.lpstrFile.Length;
        ofn.lpstrFileTitle = new string(new char[64]);
        ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
        ofn.lpstrTitle = "Select File";
        if (GetOpenFileName(ref ofn))
        {
            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool CopyFileEx(string lpExistingFileName, string lpNewFileName,
              CopyProgressRoutine lpProgressRoutine, IntPtr lpData, ref Int32 pbCancel,
              CopyFileFlags dwCopyFlags);

            int pbCancel;

            if (CopyFileEx(ofn.lpstrFile, dst + ofn.lpstrFileTitle, new CopyProgressRoutine(this.CopyProgressHandler), IntPtr.Zero, ref pbCancel, CopyFileFlags.COPY_FILE_RESTARTABLE))
            // TODO: Assign dst as the removeable disk
            {
                return Text("Successfully copied "+ ofn.lpstrFileTitle +" to USB drive " + dst);
            }
        }
        return Text("Failed");
    }

    public ResourceResponse FileDel(ResourceRequest request)
    {
        [DllImport("Comdlg32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool GetOpenFileName(ref OpenFileName ofn);

        var ofn = new OpenFileName();
        ofn.lStructSize = Marshal.SizeOf(ofn);
        ofn.lpstrFilter = "All files(*.*)\0\0";
        ofn.lpstrFile = new string(new char[256]);
        ofn.nMaxFile = ofn.lpstrFile.Length;
        ofn.lpstrFileTitle = new string(new char[64]);
        ofn.nMaxFileTitle = ofn.lpstrFileTitle.Length;
        ofn.lpstrTitle = "Select File";
        if (GetOpenFileName(ref ofn))
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            static extern bool DeleteFile(string lpFileName);

            if(DeleteFile(ofn.lpstrFile))
            {
                return Text("Succeed");
            }
        }
        return Text("Failed");
    }

}