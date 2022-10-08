using NetDimension.NanUI.Browser.ResourceHandler;
using NetDimension.NanUI.Resource.Data;
using System.Runtime.InteropServices;
using System.IO;

public class DrvInfoService : DataService
{
    [DllImport("kernel32.dll")]
    public static extern DriveType GetDriveType([MarshalAs(UnmanagedType.LPStr)] string lpRootPathName);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetDiskFreeSpaceEx(string lpDirectoryName,
    out ulong lpFreeBytesAvailable,
    out ulong lpTotalNumberOfBytes,
    out ulong lpTotalNumberOfFreeBytes);

    //GET /hello/hi
    public ResourceResponse DrvStatus(ResourceRequest request)
    {
        DriveType dt;
        string rtn = "";
        int drvCount = 0;

        ulong FreeBytesAvailable;
        ulong TotalNumberOfBytes;
        ulong TotalNumberOfFreeBytes;

        for (char drv = 'A'; drv <= 'Z'; drv ++)
        {
            dt = GetDriveType(drv.ToString() + ":\\");
            if (((int)dt) != 0 && (int)dt != 1)
            {
                rtn += (drv.ToString() + ":\\ " + dt.ToString() + "\n");
                drvCount++;
                GetDiskFreeSpaceEx(drv.ToString() + ":\\", out FreeBytesAvailable, out TotalNumberOfBytes,
                   out TotalNumberOfFreeBytes);
                rtn += ((double)FreeBytesAvailable / 1024 / 1024 / 1024 + "\n");
            }
        }



        rtn += drvCount;
        return Text(rtn);
    }
}