using System;
using System.Collections.Generic;
using System.Text;
using NetDimension.NanUI.Resource.Data;
using NetDimension.NanUI.Browser.ResourceHandler;
using Microsoft.Win32;

public class UsbService : DataService
{
    public ResourceResponse DisUsb(ResourceRequest request)
    {
        RegistryKey key = Registry.LocalMachine;
        RegistryKey software = key.OpenSubKey(@"SYSTEM\CurrentControlSet\services\USBSTOR", true);
        software.SetValue("Start", 4);

        return Text("Successfully Disabled");
    }

    public ResourceResponse EnableUsb(ResourceRequest request)
    {
        RegistryKey key = Registry.LocalMachine;
        RegistryKey software = key.OpenSubKey(@"SYSTEM\CurrentControlSet\services\USBSTOR", true);
        software.SetValue("Start", 3);

        return Text("Successfully Enabled");
    }
}