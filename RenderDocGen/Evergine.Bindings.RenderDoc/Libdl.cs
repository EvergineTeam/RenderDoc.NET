using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Evergine.Bindings.RenderDoc
{
    internal static class Libdl
    {
        public const int RTLD_NOW = 0x002;

        [DllImport("libdl")]
        public static extern IntPtr dlopen(string fileName, int flags);

        [DllImport("libdl")]
        public static extern IntPtr dlsym(IntPtr module, string procName);

        [DllImport("libdl")]
        public static extern void dlclose(IntPtr module);
    }
}
