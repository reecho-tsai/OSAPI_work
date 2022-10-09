using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using OSAPI;

namespace OSAPI.Struct
{
    enum CopyProgressResult : uint
    {
        PROGRESS_CONTINUE = 0,
        PROGRESS_CANCEL = 1,
        PROGRESS_STOP = 2,
        PROGRESS_QUIET = 3
    }

    enum CopyProgressCallbackReason : uint
    {
        CALLBACK_CHUNK_FINISHED = 0x00000000,
        CALLBACK_STREAM_SWITCH = 0x00000001
    }

    delegate CopyProgressResult CopyProgressRoutine(
    long TotalFileSize,
    long TotalBytesTransferred,
    long StreamSize,
    long StreamBytesTransferred,
    uint dwStreamNumber,
    CopyProgressCallbackReason dwCallbackReason,
    IntPtr hSourceFile,
    IntPtr hDestinationFile,
    IntPtr lpData);

    [Flags]
    enum CopyFileFlags : uint
    {
        COPY_FILE_FAIL_IF_EXISTS = 0x00000001,
        COPY_FILE_RESTARTABLE = 0x00000002,
        COPY_FILE_OPEN_SOURCE_FOR_WRITE = 0x00000004,
        COPY_FILE_ALLOW_DECRYPTED_DESTINATION = 0x00000008,
        COPY_FILE_COPY_SYMLINK = 0x00000800, //NT 6.0+
        COPY_FILE_NO_BUFFERING = 0x00001000 //NT 6.0+
    }
}
