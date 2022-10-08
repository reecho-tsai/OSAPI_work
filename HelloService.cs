using NetDimension.NanUI.Browser.ResourceHandler;
using NetDimension.NanUI.Resource.Data;
using System.Runtime.InteropServices;
using System.IO;

public class HelloService : DataService
{
    [DllImport("kernel32.dll")]
    public static extern DriveType GetDriveType([MarshalAs(UnmanagedType.LPStr)] string lpRootPathName);

    //GET /hello/hi
    public ResourceResponse Hi(ResourceRequest request)
    {
        DriveType dt;
        string rtn = "";
        for(char drv = 'A'; drv <= 'Z'; drv ++)
        {
            dt = GetDriveType(drv.ToString() + ":\\");
            if (((int)dt) != 0 && (int)dt != 1)
                rtn += (drv.ToString() + ":\\ " + dt.ToString() + "\n");
        }

        return Text(rtn);
    }
}