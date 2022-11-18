using System;

namespace Evergine.Bindings.RenderDoc
{
	/// <summary>
	/// ///////////////////////////////////////////////////////////////////////////////////////////////
	/// RenderDoc capture options
	/// </summary>
	public enum RENDERDOC_CaptureOption
	{

		/// <summary>
		/// Allow the application to enable vsync
		/// Default - enabled
		/// 1 - The application can enable or disable vsync at will
		/// 0 - vsync is force disabled
		/// </summary>
		eRENDERDOC_Option_AllowVSync = 0,

		/// <summary>
		/// Allow the application to enable fullscreen
		/// Default - enabled
		/// 1 - The application can enable or disable fullscreen at will
		/// 0 - fullscreen is force disabled
		/// </summary>
		eRENDERDOC_Option_AllowFullscreen = 1,

		/// <summary>
		/// Record API debugging events and messages
		/// Default - disabled
		/// 1 - Enable built-in API debugging features and records the results into
		/// the capture, which is matched up with events on replay
		/// 0 - no API debugging is forcibly enabled
		/// </summary>
		eRENDERDOC_Option_APIValidation = 2,

		/// <summary>
		/// deprecated name of this enum
		/// </summary>
		eRENDERDOC_Option_DebugDeviceMode = 2,

		/// <summary>
		/// Capture CPU callstacks for API events
		/// Default - disabled
		/// 1 - Enables capturing of callstacks
		/// 0 - no callstacks are captured
		/// </summary>
		eRENDERDOC_Option_CaptureCallstacks = 3,

		/// <summary>
		/// When capturing CPU callstacks, only capture them from actions.
		/// This option does nothing without the above option being enabled
		/// Default - disabled
		/// 1 - Only captures callstacks for actions.
		/// Ignored if CaptureCallstacks is disabled
		/// 0 - Callstacks, if enabled, are captured for every event.
		/// </summary>
		eRENDERDOC_Option_CaptureCallstacksOnlyDraws = 4,

		/// <summary>
		/// When capturing CPU callstacks, only capture them from actions.
		/// This option does nothing without the above option being enabled
		/// Default - disabled
		/// 1 - Only captures callstacks for actions.
		/// Ignored if CaptureCallstacks is disabled
		/// 0 - Callstacks, if enabled, are captured for every event.
		/// </summary>
		eRENDERDOC_Option_CaptureCallstacksOnlyActions = 4,

		/// <summary>
		/// Specify a delay in seconds to wait for a debugger to attach, after
		/// creating or injecting into a process, before continuing to allow it to run.
		/// 0 indicates no delay, and the process will run immediately after injection
		/// Default - 0 seconds
		/// </summary>
		eRENDERDOC_Option_DelayForDebugger = 5,

		/// <summary>
		/// Verify buffer access. This includes checking the memory returned by a Map() call to
		/// detect any out-of-bounds modification, as well as initialising buffers with undefined contents
		/// to a marker value to catch use of uninitialised memory.
		/// NOTE: This option is only valid for OpenGL and D3D11. Explicit APIs such as D3D12 and Vulkan do
		/// not do the same kind of interception 
		/// &
		/// checking and undefined contents are really undefined.
		/// Default - disabled
		/// 1 - Verify buffer access
		/// 0 - No verification is performed, and overwriting bounds may cause crashes or corruption in
		/// RenderDoc.
		/// </summary>
		eRENDERDOC_Option_VerifyBufferAccess = 6,

		/// <summary>
		/// The old name for eRENDERDOC_Option_VerifyBufferAccess was eRENDERDOC_Option_VerifyMapWrites.
		/// This option now controls the filling of uninitialised buffers with 0xdddddddd which was
		/// previously always enabled
		/// </summary>
		eRENDERDOC_Option_VerifyMapWrites = 6,

		/// <summary>
		/// Hooks any system API calls that create child processes, and injects
		/// RenderDoc into them recursively with the same options.
		/// Default - disabled
		/// 1 - Hooks into spawned child processes
		/// 0 - Child processes are not hooked by RenderDoc
		/// </summary>
		eRENDERDOC_Option_HookIntoChildren = 7,

		/// <summary>
		/// By default RenderDoc only includes resources in the final capture necessary
		/// for that frame, this allows you to override that behaviour.
		/// Default - disabled
		/// 1 - all live resources at the time of capture are included in the capture
		/// and available for inspection
		/// 0 - only the resources referenced by the captured frame are included
		/// </summary>
		eRENDERDOC_Option_RefAllResources = 8,

		/// <summary>
		/// **NOTE**: As of RenderDoc v1.1 this option has been deprecated. Setting or
		/// getting it will be ignored, to allow compatibility with older versions.
		/// In v1.1 the option acts as if it's always enabled.
		/// By default RenderDoc skips saving initial states for resources where the
		/// previous contents don't appear to be used, assuming that writes before
		/// reads indicate previous contents aren't used.
		/// Default - disabled
		/// 1 - initial contents at the start of each captured frame are saved, even if
		/// they are later overwritten or cleared before being used.
		/// 0 - unless a read is detected, initial contents will not be saved and will
		/// appear as black or empty data.
		/// </summary>
		eRENDERDOC_Option_SaveAllInitials = 9,

		/// <summary>
		/// In APIs that allow for the recording of command lists to be replayed later,
		/// RenderDoc may choose to not capture command lists before a frame capture is
		/// triggered, to reduce overheads. This means any command lists recorded once
		/// and replayed many times will not be available and may cause a failure to
		/// capture.
		/// NOTE: This is only true for APIs where multithreading is difficult or
		/// discouraged. Newer APIs like Vulkan and D3D12 will ignore this option
		/// and always capture all command lists since the API is heavily oriented
		/// around it and the overheads have been reduced by API design.
		/// 1 - All command lists are captured from the start of the application
		/// 0 - Command lists are only captured if their recording begins during
		/// the period when a frame capture is in progress.
		/// </summary>
		eRENDERDOC_Option_CaptureAllCmdLists = 10,

		/// <summary>
		/// Mute API debugging output when the API validation mode option is enabled
		/// Default - enabled
		/// 1 - Mute any API debug messages from being displayed or passed through
		/// 0 - API debugging is displayed as normal
		/// </summary>
		eRENDERDOC_Option_DebugOutputMute = 11,

		/// <summary>
		/// Option to allow vendor extensions to be used even when they may be
		/// incompatible with RenderDoc and cause corrupted replays or crashes.
		/// Default - inactive
		/// No values are documented, this option should only be used when absolutely
		/// necessary as directed by a RenderDoc developer.
		/// </summary>
		eRENDERDOC_Option_AllowUnsupportedVendorExtensions = 12,
	}

	public enum RENDERDOC_InputButton
	{

		/// <summary>
		/// '0' - '9' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_0 = 48,

		/// <summary>
		/// '0' - '9' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_1 = 49,

		/// <summary>
		/// '0' - '9' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_2 = 50,

		/// <summary>
		/// '0' - '9' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_3 = 51,

		/// <summary>
		/// '0' - '9' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_4 = 52,

		/// <summary>
		/// '0' - '9' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_5 = 53,

		/// <summary>
		/// '0' - '9' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_6 = 54,

		/// <summary>
		/// '0' - '9' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_7 = 55,

		/// <summary>
		/// '0' - '9' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_8 = 56,

		/// <summary>
		/// '0' - '9' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_9 = 57,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_A = 65,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_B = 66,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_C = 67,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_D = 68,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_E = 69,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_F = 70,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_G = 71,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_H = 72,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_I = 73,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_J = 74,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_K = 75,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_L = 76,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_M = 77,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_N = 78,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_O = 79,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_P = 80,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_Q = 81,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_R = 82,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_S = 83,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_T = 84,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_U = 85,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_V = 86,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_W = 87,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_X = 88,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_Y = 89,

		/// <summary>
		/// 'A' - 'Z' matches ASCII values
		/// </summary>
		eRENDERDOC_Key_Z = 90,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_NonPrintable = 256,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Divide = 257,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Multiply = 258,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Subtract = 259,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Plus = 260,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F1 = 261,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F2 = 262,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F3 = 263,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F4 = 264,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F5 = 265,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F6 = 266,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F7 = 267,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F8 = 268,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F9 = 269,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F10 = 270,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F11 = 271,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_F12 = 272,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Home = 273,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_End = 274,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Insert = 275,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Delete = 276,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_PageUp = 277,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_PageDn = 278,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Backspace = 279,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Tab = 280,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_PrtScrn = 281,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Pause = 282,

		/// <summary>
		/// leave the rest of the ASCII range free
		/// in case we want to use it later
		/// </summary>
		eRENDERDOC_Key_Max = 283,
	}

	public enum RENDERDOC_OverlayBits
	{

		/// <summary>
		/// This single bit controls whether the overlay is enabled or disabled globally
		/// </summary>
		eRENDERDOC_Overlay_Enabled = 1,

		/// <summary>
		/// Show the average framerate over several seconds as well as min/max
		/// </summary>
		eRENDERDOC_Overlay_FrameRate = 2,

		/// <summary>
		/// Show the current frame number
		/// </summary>
		eRENDERDOC_Overlay_FrameNumber = 4,

		/// <summary>
		/// Show a list of recent captures, and how many captures have been made
		/// </summary>
		eRENDERDOC_Overlay_CaptureList = 8,

		/// <summary>
		/// Default values for the overlay mask
		/// </summary>
		eRENDERDOC_Overlay_Default = 15,

		/// <summary>
		/// Enable all bits
		/// </summary>
		eRENDERDOC_Overlay_All = -1,

		/// <summary>
		/// Disable all bits
		/// </summary>
		eRENDERDOC_Overlay_None = 0,
	}

	/// <summary>
	/// RenderDoc uses semantic versioning (http://semver.org/).
	/// MAJOR version is incremented when incompatible API changes happen.
	/// MINOR version is incremented when functionality is added in a backwards-compatible manner.
	/// PATCH version is incremented when backwards-compatible bug fixes happen.
	/// Note that this means the API returned can be higher than the one you might have requested.
	/// e.g. if you are running against a newer RenderDoc that supports 1.0.1, it will be returned
	/// instead of 1.0.0. You can check this with the GetAPIVersion entry point
	/// </summary>
	public enum RENDERDOC_Version
	{

		/// <summary>
		/// RENDERDOC_API_1_0_0 = 1 00 00
		/// </summary>
		eRENDERDOC_API_Version_1_0_0 = 10000,

		/// <summary>
		/// RENDERDOC_API_1_0_1 = 1 00 01
		/// </summary>
		eRENDERDOC_API_Version_1_0_1 = 10001,

		/// <summary>
		/// RENDERDOC_API_1_0_2 = 1 00 02
		/// </summary>
		eRENDERDOC_API_Version_1_0_2 = 10002,

		/// <summary>
		/// RENDERDOC_API_1_1_0 = 1 01 00
		/// </summary>
		eRENDERDOC_API_Version_1_1_0 = 10100,

		/// <summary>
		/// RENDERDOC_API_1_1_1 = 1 01 01
		/// </summary>
		eRENDERDOC_API_Version_1_1_1 = 10101,

		/// <summary>
		/// RENDERDOC_API_1_1_2 = 1 01 02
		/// </summary>
		eRENDERDOC_API_Version_1_1_2 = 10102,

		/// <summary>
		/// RENDERDOC_API_1_2_0 = 1 02 00
		/// </summary>
		eRENDERDOC_API_Version_1_2_0 = 10200,

		/// <summary>
		/// RENDERDOC_API_1_3_0 = 1 03 00
		/// </summary>
		eRENDERDOC_API_Version_1_3_0 = 10300,

		/// <summary>
		/// RENDERDOC_API_1_4_0 = 1 04 00
		/// </summary>
		eRENDERDOC_API_Version_1_4_0 = 10400,

		/// <summary>
		/// RENDERDOC_API_1_4_1 = 1 04 01
		/// </summary>
		eRENDERDOC_API_Version_1_4_1 = 10401,

		/// <summary>
		/// RENDERDOC_API_1_4_2 = 1 04 02
		/// </summary>
		eRENDERDOC_API_Version_1_4_2 = 10402,

		/// <summary>
		/// RENDERDOC_API_1_5_0 = 1 05 00
		/// </summary>
		eRENDERDOC_API_Version_1_5_0 = 10500,
	}

}
