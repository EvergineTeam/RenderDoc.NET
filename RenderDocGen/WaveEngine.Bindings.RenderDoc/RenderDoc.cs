using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;

namespace WaveEngine.Bindings.RenderDoc
{
    /// <summary>
    /// RenderDoc is a frame-capture based graphics debugger.
    /// </summary>
    public unsafe class RenderDoc
    {
        /// <summary>
        /// The RenderDoc API functions.
        /// </summary>
        public readonly RENDERDOC_API_1_4_1 API;

        /// <summary>
        /// Attempts to load RenderDoc.
        /// </summary>
        /// <param name="renderDoc">The RenderDoc instance.</param>
        /// <returns>Whether RenderDoc was successfully loaded.</returns>
        public static bool Load(out RenderDoc renderDoc)
        {
            try
            {
                var nativeLib = NativeLibrary.Load(GetRenderDocLibName());
                renderDoc = new RenderDoc(nativeLib);
                return true;
            }
            catch
            {
                renderDoc = null;
                return false;
            }
        }

        private unsafe RenderDoc(NativeLibrary nativeLib)
        {
            nativeLib.LoadFunction("RENDERDOC_GetAPI", out pRENDERDOC_GetAPI getApiDelegate);
            void* apiPointers;
            int result = getApiDelegate(RENDERDOC_Version.eRENDERDOC_API_Version_1_4_1, &apiPointers);
            if (result != 1)
            {
                throw new InvalidOperationException("Failed to load RenderDoc API.");
            }

            API = Marshal.PtrToStructure<RENDERDOC_API_1_4_1>((IntPtr)apiPointers);
        }

        private static string GetRenderDocLibName()
        {
            string programFiles = Environment.GetEnvironmentVariable("ProgramFiles");
            if (programFiles != null)
            {
                string systemInstallPath = Path.Combine(programFiles, "RenderDoc", "renderdoc.dll");
                if (File.Exists(systemInstallPath))
                {
                    return systemInstallPath;
                }
            }

            return "renderdoc.dll";
        }
    }
}
