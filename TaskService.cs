using System;
using System.Collections.Generic;
using System.Text;
using NetDimension.NanUI.Resource.Data;
using NetDimension.NanUI.Browser.ResourceHandler;
using System.Diagnostics;

using OSAPI;
public class TaskService : DataService
{
    public ResourceResponse TaskInfo(ResourceRequest request)
    {

        Process curr = Process.GetCurrentProcess();
        
        string ret = "";
        ret += "ID: " + curr.Id + "<br />"
            + "Name: " + curr.ProcessName + "<br />"
            + "Physical memory usage     : " + curr.WorkingSet64 + " Bytes <br />"
            + "Base priority             : " + curr.BasePriority + "<br />"
            + "Priority class            : " + curr.PriorityClass + "<br />"
            + "User processor time       : " + curr.UserProcessorTime + "<br />"
            + "Privileged processor time : " + curr.PrivilegedProcessorTime + "<br />"
            + "Total processor time      : " + curr.TotalProcessorTime + "<br />"
            + "Paged system memory size  : " + curr.PagedSystemMemorySize64 + " Bytes <br />"
            + "Paged memory size         : " + curr.PagedMemorySize64 + " Bytes <br />";

        return Text(ret);
    }
}