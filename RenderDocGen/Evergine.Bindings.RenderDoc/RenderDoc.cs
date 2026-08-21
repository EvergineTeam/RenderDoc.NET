using System;
using System.Runtime.InteropServices;
using System.IO;

namespace Evergine.Bindings.RenderDoc
{
    /// <summary>
    /// RenderDoc is a frame-capture based graphics debugger.
    /// </summary>
    public unsafe class RenderDoc
    {
        /// <summary>
        /// The RenderDoc <see cref="http://renderdoc.org/docs/in_application_api.html">API</see>.
        /// </summary>
        public readonly RENDERDOC_API_1_7_0 API;

        /// <summary>
        /// Attempts to load RenderDoc.
        /// </summary>
        /// <param name="renderDoc">The RenderDoc instance.</param>
        /// <returns>Whether RenderDoc was successfully loaded.</returns>
        public static bool Load(out RenderDoc renderDoc)
        {
            var libName = GetRenderDocLibName();
            return Load(libName, out renderDoc);
        }

        /// <summary>
        /// Attempts to load RenderDoc.
        /// </summary>
        /// <param name="libraryName">library .dll or .so name</param>
        /// <param name="renderDoc">The RenderDoc instance.</param>
        /// <returns>Whether RenderDoc was successfully loaded.</returns>
        public static bool Load(string libraryName, out RenderDoc renderDoc)
        {
            if (NativeLibrary.TryLoad(libraryName, out var lib) ||
                NativeLibrary.TryLoad(libraryName, typeof(RenderDoc).Assembly, null, out lib))
            {
                return Load(lib, out renderDoc);
            }

            renderDoc = null;
            return false;
        }

        /// <summary>
        /// Attempts to load RenderDoc.
        /// </summary>
        /// <param name="nativeLib">native handle to loaded native renderdoc library</param>
        /// <param name="renderDoc">The RenderDoc instance.</param>
        /// <returns>Whether RenderDoc was successfully loaded.</returns>
        public static bool Load(nint nativeLib, out RenderDoc renderDoc)
        {
            renderDoc = null;
            if (nativeLib != 0)
            {
                renderDoc = new RenderDoc(nativeLib);
            }
            return renderDoc != null;
        }

        private RenderDoc(nint nativeLib)
        {
            NativeLibrary.TryGetExport(nativeLib, "RENDERDOC_GetAPI", out IntPtr funcPtr);
            var getApiDelegate = Marshal.GetDelegateForFunctionPointer<pRENDERDOC_GetAPI>(funcPtr);
            void* apiPointers;
            int result = getApiDelegate(RENDERDOC_Version.eRENDERDOC_API_Version_1_4_1, &apiPointers);
            if (result != 1)
            {
                throw new InvalidOperationException("Failed to load RenderDoc API.");
            }

            API = Marshal.PtrToStructure<RENDERDOC_API_1_7_0>((IntPtr)apiPointers);
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
