using System;
using System.Runtime.InteropServices;

namespace Evergine.Bindings.RenderDoc
{
	/// <summary>
	/// API version changelog:
	/// 1.0.0 - initial release
	/// 1.0.1 - Bugfix: IsFrameCapturing() was returning false for captures that were triggered
	/// by keypress or TriggerCapture, instead of Start/EndFrameCapture.
	/// 1.0.2 - Refactor: Renamed eRENDERDOC_Option_DebugDeviceMode to eRENDERDOC_Option_APIValidation
	/// 1.1.0 - Add feature: TriggerMultiFrameCapture(). Backwards compatible with 1.0.x since the new
	/// function pointer is added to the end of the struct, the original layout is identical
	/// 1.1.1 - Refactor: Renamed remote access to target control (to better disambiguate from remote
	/// replay/remote server concept in replay UI)
	/// 1.1.2 - Refactor: Renamed "log file" in function names to just capture, to clarify that these
	/// are captures and not debug logging files. This is the first API version in the v1.0
	/// branch.
	/// 1.2.0 - Added feature: SetCaptureFileComments() to add comments to a capture file that will be
	/// displayed in the UI program on load.
	/// 1.3.0 - Added feature: New capture option eRENDERDOC_Option_AllowUnsupportedVendorExtensions
	/// which allows users to opt-in to allowing unsupported vendor extensions to function.
	/// Should be used at the user's own risk.
	/// Refactor: Renamed eRENDERDOC_Option_VerifyMapWrites to
	/// eRENDERDOC_Option_VerifyBufferAccess, which now also controls initialisation to
	/// 0xdddddddd of uninitialised buffer contents.
	/// 1.4.0 - Added feature: DiscardFrameCapture() to discard a frame capture in progress and stop
	/// capturing without saving anything to disk.
	/// 1.4.1 - Refactor: Renamed Shutdown to RemoveHooks to better clarify what is happening
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public unsafe struct RENDERDOC_API_1_4_1
	{
		public pRENDERDOC_GetAPIVersion GetAPIVersion;
		public pRENDERDOC_SetCaptureOptionU32 SetCaptureOptionU32;
		public pRENDERDOC_SetCaptureOptionF32 SetCaptureOptionF32;
		public pRENDERDOC_GetCaptureOptionU32 GetCaptureOptionU32;
		public pRENDERDOC_GetCaptureOptionF32 GetCaptureOptionF32;
		public pRENDERDOC_SetFocusToggleKeys SetFocusToggleKeys;
		public pRENDERDOC_SetCaptureKeys SetCaptureKeys;
		public pRENDERDOC_GetOverlayBits GetOverlayBits;
		public pRENDERDOC_MaskOverlayBits MaskOverlayBits;

		/// <summary>
		/// Shutdown was renamed to RemoveHooks in 1.4.1.
		/// These unions allow old code to continue compiling without changes
		/// </summary>
		public pRENDERDOC_RemoveHooks RemoveHooks;
		public pRENDERDOC_UnloadCrashHandler UnloadCrashHandler;

		/// <summary>
		/// Get/SetLogFilePathTemplate was renamed to Get/SetCaptureFilePathTemplate in 1.1.2.
		/// These unions allow old code to continue compiling without changes
		/// </summary>
		public pRENDERDOC_SetCaptureFilePathTemplate SetCaptureFilePathTemplate;
		public pRENDERDOC_GetCaptureFilePathTemplate GetCaptureFilePathTemplate;
		public pRENDERDOC_GetNumCaptures GetNumCaptures;
		public pRENDERDOC_GetCapture GetCapture;
		public pRENDERDOC_TriggerCapture TriggerCapture;

		/// <summary>
		/// IsRemoteAccessConnected was renamed to IsTargetControlConnected in 1.1.1.
		/// This union allows old code to continue compiling without changes
		/// </summary>
		public pRENDERDOC_IsTargetControlConnected IsTargetControlConnected;
		public pRENDERDOC_LaunchReplayUI LaunchReplayUI;
		public pRENDERDOC_SetActiveWindow SetActiveWindow;
		public pRENDERDOC_StartFrameCapture StartFrameCapture;
		public pRENDERDOC_IsFrameCapturing IsFrameCapturing;
		public pRENDERDOC_EndFrameCapture EndFrameCapture;

		/// <summary>
		/// new function in 1.1.0
		/// </summary>
		public pRENDERDOC_TriggerMultiFrameCapture TriggerMultiFrameCapture;

		/// <summary>
		/// new function in 1.2.0
		/// </summary>
		public pRENDERDOC_SetCaptureFileComments SetCaptureFileComments;

		/// <summary>
		/// new function in 1.4.0
		/// </summary>
		public pRENDERDOC_DiscardFrameCapture DiscardFrameCapture;
	}

}

