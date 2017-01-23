﻿using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("OpenCvSharp.DebuggerVisualizers2012")]
[assembly: AssemblyTrademark("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("df57cccb-47f5-41e0-b6ba-1557e3605396")]

// OpenCvSharp objects' debugger visualizer
[assembly: DebuggerVisualizer(
    typeof(OpenCvSharp.DebuggerVisualizers2012.MatDebuggerVisualizer),
    typeof(OpenCvSharp.DebuggerVisualizers2012.MatObjectSource),
    Target = typeof(OpenCvSharp.Mat),
    Description = "Mat Visualizer"
)]