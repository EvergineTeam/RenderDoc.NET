using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Evergine.Bindings.RenderDoc
{
    internal static class Libdl
    {
        [DllImport("libdl")]
        public static extern IntPtr dlopen(string fileName);

        [DllImport("libdl")]
        public static extern IntPtr dlsym(IntPtr module, string procName);

        [DllImport("libdl")]
        public static extern void dlclose(IntPtr module);
    }
}
