using System;

namespace Evergine.Bindings.RenderDoc
{
	/// <summary>
	/// Sets an option that controls how RenderDoc behaves on capture.
	/// Returns 1 if the option and value are valid
	/// Returns 0 if either is invalid and the option is unchanged
	/// </summary>
	public unsafe delegate int pRENDERDOC_SetCaptureOptionU32(
		 RENDERDOC_CaptureOption opt,
		 uint val);

	public unsafe delegate int pRENDERDOC_SetCaptureOptionF32(
		 RENDERDOC_CaptureOption opt,
		 float val);

	/// <summary>
	/// Gets the current value of an option as a uint32_t
	/// If the option is invalid, 0xffffffff is returned
	/// </summary>
	public unsafe delegate uint pRENDERDOC_GetCaptureOptionU32(
		 RENDERDOC_CaptureOption opt);

	/// <summary>
	/// Gets the current value of an option as a float
	/// If the option is invalid, -FLT_MAX is returned
	/// </summary>
	public unsafe delegate float pRENDERDOC_GetCaptureOptionF32(
		 RENDERDOC_CaptureOption opt);

	/// <summary>
	/// Sets which key or keys can be used to toggle focus between multiple windows
	/// If keys is NULL or num is 0, toggle keys will be disabled
	/// </summary>
	public unsafe delegate void pRENDERDOC_SetFocusToggleKeys(
		 RENDERDOC_InputButton* keys,
		 int num);

	/// <summary>
	/// Sets which key or keys can be used to capture the next frame
	/// If keys is NULL or num is 0, captures keys will be disabled
	/// </summary>
	public unsafe delegate void pRENDERDOC_SetCaptureKeys(
		 RENDERDOC_InputButton* keys,
		 int num);

	/// <summary>
	/// returns the overlay bits that have been set
	/// </summary>
	public unsafe delegate uint pRENDERDOC_GetOverlayBits();

	/// <summary>
	/// sets the overlay bits with an and 
	/// &
	/// or mask
	/// </summary>
	public unsafe delegate void pRENDERDOC_MaskOverlayBits(
		 uint And,
		 uint Or);

	/// <summary>
	/// this function will attempt to remove RenderDoc's hooks in the application.
	/// Note: that this can only work correctly if done immediately after
	/// the module is loaded, before any API work happens. RenderDoc will remove its
	/// injected hooks and shut down. Behaviour is undefined if this is called
	/// after any API functions have been called, and there is still no guarantee of
	/// success.
	/// </summary>
	public unsafe delegate void pRENDERDOC_RemoveHooks();

	/// <summary>
	/// This function will unload RenderDoc's crash handler.
	/// If you use your own crash handler and don't want RenderDoc's handler to
	/// intercede, you can call this function to unload it and any unhandled
	/// exceptions will pass to the next handler.
	/// </summary>
	public unsafe delegate void pRENDERDOC_UnloadCrashHandler();

	/// <summary>
	/// Sets the capture file path template
	/// pathtemplate is a UTF-8 string that gives a template for how captures will be named
	/// and where they will be saved.
	/// Any extension is stripped off the path, and captures are saved in the directory
	/// specified, and named with the filename and the frame number appended. If the
	/// directory does not exist it will be created, including any parent directories.
	/// If pathtemplate is NULL, the template will remain unchanged
	/// Example:
	/// SetCaptureFilePathTemplate("my_captures/example");
	/// Capture #1 -> my_captures/example_frame123.rdc
	/// Capture #2 -> my_captures/example_frame456.rdc
	/// </summary>
	public unsafe delegate void pRENDERDOC_SetCaptureFilePathTemplate(
		 char* pathtemplate);

	/// <summary>
	/// returns the current capture path template, see SetCaptureFileTemplate above, as a UTF-8 string
	/// </summary>
	public unsafe delegate char* pRENDERDOC_GetCaptureFilePathTemplate();

	/// <summary>
	/// returns the number of captures that have been made
	/// </summary>
	public unsafe delegate uint pRENDERDOC_GetNumCaptures();

	/// <summary>
	/// This function returns the details of a capture, by index. New captures are added
	/// to the end of the list.
	/// filename will be filled with the absolute path to the capture file, as a UTF-8 string
	/// pathlength will be written with the length in bytes of the filename string
	/// timestamp will be written with the time of the capture, in seconds since the Unix epoch
	/// Any of the parameters can be NULL and they'll be skipped.
	/// The function will return 1 if the capture index is valid, or 0 if the index is invalid
	/// If the index is invalid, the values will be unchanged
	/// Note: when captures are deleted in the UI they will remain in this list, so the
	/// capture path may not exist anymore.
	/// </summary>
	public unsafe delegate uint pRENDERDOC_GetCapture(
		 uint idx,
		 char* filename,
		 uint* pathlength,
		 ulong* timestamp);

	/// <summary>
	/// Sets the comments associated with a capture file. These comments are displayed in the
	/// UI program when opening.
	/// filePath should be a path to the capture file to add comments to. If set to NULL or ""
	/// the most recent capture file created made will be used instead.
	/// comments should be a NULL-terminated UTF-8 string to add as comments.
	/// Any existing comments will be overwritten.
	/// </summary>
	public unsafe delegate void pRENDERDOC_SetCaptureFileComments(
		 char* filePath,
		 char* comments);

	/// <summary>
	/// returns 1 if the RenderDoc UI is connected to this application, 0 otherwise
	/// </summary>
	public unsafe delegate uint pRENDERDOC_IsTargetControlConnected();

	/// <summary>
	/// This function will launch the Replay UI associated with the RenderDoc library injected
	/// into the running application.
	/// if connectTargetControl is 1, the Replay UI will be launched with a command line parameter
	/// to connect to this application
	/// cmdline is the rest of the command line, as a UTF-8 string. E.g. a captures to open
	/// if cmdline is NULL, the command line will be empty.
	/// returns the PID of the replay UI if successful, 0 if not successful.
	/// </summary>
	public unsafe delegate uint pRENDERDOC_LaunchReplayUI(
		 uint connectTargetControl,
		 char* cmdline);

	/// <summary>
	/// RenderDoc can return a higher version than requested if it's backwards compatible,
	/// this function returns the actual version returned. If a parameter is NULL, it will be
	/// ignored and the others will be filled out.
	/// </summary>
	public unsafe delegate void pRENDERDOC_GetAPIVersion(
		 int* major,
		 int* minor,
		 int* patch);

	/// <summary>
	/// This sets the RenderDoc in-app overlay in the API/window pair as 'active' and it will
	/// respond to keypresses. Neither parameter can be NULL
	/// </summary>
	public unsafe delegate void pRENDERDOC_SetActiveWindow(
		 IntPtr device,
		 IntPtr wndHandle);

	/// <summary>
	/// capture the next frame on whichever window and API is currently considered active
	/// </summary>
	public unsafe delegate void pRENDERDOC_TriggerCapture();

	/// <summary>
	/// capture the next N frames on whichever window and API is currently considered active
	/// </summary>
	public unsafe delegate void pRENDERDOC_TriggerMultiFrameCapture(
		 uint numFrames);

	/// <summary>
	/// Immediately starts capturing API calls on the specified device pointer and window handle.
	/// If there is no matching thing to capture (e.g. no supported API has been initialised),
	/// this will do nothing.
	/// The results are undefined (including crashes) if two captures are started overlapping,
	/// even on separate devices and/oror windows.
	/// </summary>
	public unsafe delegate void pRENDERDOC_StartFrameCapture(
		 IntPtr device,
		 IntPtr wndHandle);

	/// <summary>
	/// Returns whether or not a frame capture is currently ongoing anywhere.
	/// This will return 1 if a capture is ongoing, and 0 if there is no capture running
	/// </summary>
	public unsafe delegate uint pRENDERDOC_IsFrameCapturing();

	/// <summary>
	/// Ends capturing immediately.
	/// This will return 1 if the capture succeeded, and 0 if there was an error capturing.
	/// </summary>
	public unsafe delegate uint pRENDERDOC_EndFrameCapture(
		 IntPtr device,
		 IntPtr wndHandle);

	/// <summary>
	/// Ends capturing immediately and discard any data stored without saving to disk.
	/// This will return 1 if the capture was discarded, and 0 if there was an error or no capture
	/// was in progress
	/// </summary>
	public unsafe delegate uint pRENDERDOC_DiscardFrameCapture(
		 IntPtr device,
		 IntPtr wndHandle);

	/// <summary>
	/// Requests that the replay UI show itself (if hidden or not the current top window). This can be
	/// used in conjunction with IsTargetControlConnected and LaunchReplayUI to intelligently handle
	/// showing the UI after making a capture.
	/// This will return 1 if the request was successfully passed on, though it's not guaranteed that
	/// the UI will be on top in all cases depending on OS rules. It will return 0 if there is no current
	/// target control connection to make such a request, or if there was another error
	/// </summary>
	public unsafe delegate uint pRENDERDOC_ShowReplayUI();

	/// <summary>
	/// ///////////////////////////////////////////////////////////////////////////////////////////////
	/// RenderDoc API entry point
	/// This entry point can be obtained via GetProcAddress/dlsym if RenderDoc is available.
	/// The name is the same as the typedef - "RENDERDOC_GetAPI"
	/// This function is not thread safe, and should not be called on multiple threads at once.
	/// Ideally, call this once as early as possible in your application's startup, before doing
	/// any API work, since some configuration functionality etc has to be done also before
	/// initialising any APIs.
	/// Parameters:
	/// version is a single value from the RENDERDOC_Version above.
	/// outAPIPointers will be filled out with a pointer to the corresponding struct of function
	/// pointers.
	/// Returns:
	/// 1 - if the outAPIPointers has been filled with a pointer to the API struct requested
	/// 0 - if the requested version is not supported or the arguments are invalid.
	/// </summary>
	public unsafe delegate int pRENDERDOC_GetAPI(
		 RENDERDOC_Version version,
		 void* outAPIPointers);

}
