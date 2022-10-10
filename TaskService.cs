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
        ret += "ID: " + curr.Id + "\n"
            + "Name: " + curr.ProcessName + "\n"
            + "Physical memory usage     : " + curr.WorkingSet64 + " Bytes \n"
            + "Base priority             : " + curr.BasePriority + "\n"
            + "Priority class            : " + curr.PriorityClass + "\n"
            + "User processor time       : " + curr.UserProcessorTime + "\n"
            + "Privileged processor time : " + curr.PrivilegedProcessorTime + "\n"
            + "Total processor time      : " + curr.TotalProcessorTime + "\n"
            + "Paged system memory size  : " + curr.PagedSystemMemorySize64 + " Bytes \n"
            + "Paged memory size         : " + curr.PagedMemorySize64 + " Bytes \n";

        return Text(ret);
    }
}