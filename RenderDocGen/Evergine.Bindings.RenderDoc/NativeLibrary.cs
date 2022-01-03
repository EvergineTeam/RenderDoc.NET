using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Evergine.Bindings.RenderDoc
{
    public abstract class NativeLibrary
    {
        private readonly string libraryName;
        private readonly IntPtr libraryHandle;

        public IntPtr NativeHandle => libraryHandle;

        public NativeLibrary(string libraryName)
        {
            this.libraryName = libraryName;
            libraryHandle = LoadLibrary(this.libraryName);
            if (libraryHandle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Could not load " + libraryName);
            }
        }

        public unsafe void LoadFunction<T>(string name, out T field)
        {
            IntPtr funcPtr = LoadFunction(name);

            if (funcPtr != IntPtr.Zero)
            {
                field = Marshal.GetDelegateForFunctionPointer<T>(funcPtr);
            }
            else
            {
                field = default(T);
                Debug.WriteLine($" ===> Error loading function {name}");
            }
        }

        protected abstract IntPtr LoadLibrary(string libraryName);
        protected abstract void FreeLibrary(IntPtr libraryHandle);
        protected abstract IntPtr LoadFunction(string functionName);

        public void Dispose()
        {
            FreeLibrary(libraryHandle);
        }

        public static NativeLibrary Load(string libraryName)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WindowsNativeLibrary(libraryName);
            }
            else
            {
                throw new PlatformNotSupportedException("Cannot load native libraries on this platform: " + RuntimeInformation.OSDescription);
            }
        }

        private class WindowsNativeLibrary : NativeLibrary
        {
            public WindowsNativeLibrary(string libraryName) : base(libraryName)
            {
            }

            protected override IntPtr LoadLibrary(string libraryName)
            {
                return Kernel32.LoadLibrary(libraryName);
            }

            protected override void FreeLibrary(IntPtr libraryHandle)
            {
                Kernel32.FreeLibrary(libraryHandle);
            }

            protected override IntPtr LoadFunction(string functionName)
            {
                Debug.WriteLine("Loading " + functionName);
                return Kernel32.GetProcAddress(NativeHandle, functionName);
            }
        }
    }
}
