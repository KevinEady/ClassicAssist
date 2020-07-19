﻿# ClassicAssistAvalonia

WIP POC / Fork of [ClassicAssist](https://github.com/Reetus/ClassicAssist) using
Avalonia instead of WPF.

## Running

Update your ClassicUO `settings.json` to the plugin in the `Output` folder:

```json
{
	"plugins" : [
		"/Projects/ClassicAssistAvalonia/Output/net48/ClassicAssist.dll"
	]
}
```

### Mac OS X-Specific

ClassicUO must be started with custom `DYLD_LIBRARY_PATH` to add its native
shared libraries. Avalonia also uses native libraries that are copied on build
to `Output/osx`. This path must be added as well.

Furthermore, the Avalonia assemblies are placed in the `Output` folder, but are
not automatically picked up by Mono. You must add the `Output` path to the
`MONO_PATH` environmental variable for mono to pick up these assemblies.

For example, if your current working directory is the `ClassicUO` folder, and
`ClassicAssistAvalonia` is built in `../ClassicAssistAvalonia`:

```
OUTPUT=../ClassicAssistAvalonia/Output/net48 MONO_PATH=$OUTPUT DYLD_LIBRARY_PATH=bin/Debug/osx:$OUTPUT/osx mono bin/Debug/ClassicUO.exe
```

## Known Issues

### Mac OS-X

There seems to be some error with the native libraries where a segfault is
thrown on program exit. You may see something like this when closing the
ClassicUO client:


```
xx:xx:xx |  Trace   | Exiting game...
xx:xx:xx |  Trace   | Closing...
=================================================================
        Native Crash Reporting
=================================================================
Got a segv while executing native code. This usually indicates
a fatal error in the mono runtime or one of the native libraries
used by your application.
=================================================================

=================================================================
        Native stacktrace:
=================================================================
        0x1041a6ad9 - /Library/Frameworks/Mono.framework/Versions/Current/Commands/mono : mono_dump_native_crash_info
        0x10413efb5 - /Library/Frameworks/Mono.framework/Versions/Current/Commands/mono : mono_handle_native_crash
        0x1041a0c56 - /Library/Frameworks/Mono.framework/Versions/Current/Commands/mono : altstack_handle_and_restore
        0x7fff381478ac - /System/Library/Frameworks/OpenGL.framework/Versions/A/Libraries/libGL.dylib : glDeleteBuffers
        0x153883c6f - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x1537b7bbf - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x1537d9106 - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x1537d8fa2 - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x1537a6695 - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x1537ad9fc - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x1538c16e3 - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x1535fb1fb - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x1535fae9c - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x1535fb22e - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x1538c9ab0 - /Projects/ClassicAssistAvalonia/Output/net48/osx/libSkiaSharp.dylib : gr_backendrendertarget_get_gl_framebufferinfo
        0x156e2f943 - Unknown
        0x10c9e5ebc - Unknown
        0x104327119 - /Library/Frameworks/Mono.framework/Versions/Current/Commands/mono : mono_gc_run_finalize
        0x10434601c - /Library/Frameworks/Mono.framework/Versions/Current/Commands/mono : sgen_gc_invoke_finalizers
        0x104329009 - /Library/Frameworks/Mono.framework/Versions/Current/Commands/mono : finalizer_thread
        0x1042d56ed - /Library/Frameworks/Mono.framework/Versions/Current/Commands/mono : start_wrapper
        0x7fff6849b109 - /usr/lib/system/libsystem_pthread.dylib : _pthread_start
        0x7fff68496b8b - /usr/lib/system/libsystem_pthread.dylib : thread_start

=================================================================
        Telemetry Dumper:
=================================================================
Pkilling 0x10cbd2dc0 from 0x70000a4db000
Entering thread summarizer pause from 0x70000a4db000
Finished thread summarizer pause from 0x70000a4db000.
Failed to create breadcrumb file (null)/crash_hash_0x4cf2e5da7

Waiting for dumping threads to resume

=================================================================
        External Debugger Dump:
=================================================================

=================================================================
        Basic Fault Address Reporting
=================================================================
Memory around native instruction pointer (0x7fff381478ac):0x7fff3814789c  89 e5 48 89 f2 89 fe 65 48 8b 04 25 f0 00 00 00  ..H....eH..%....
0x7fff381478ac  48 8b 88 20 14 00 00 48 8b 38 5d ff e1 55 48 89  H.. ...H.8]..UH.
0x7fff381478bc  e5 48 89 f2 89 fe 65 48 8b 04 25 f0 00 00 00 48  .H....eH..%....H
0x7fff381478cc  8b 88 28 14 00 00 48 8b 38 5d ff e1 55 48 89 e5  ..(...H.8]..UH..

=================================================================
        Managed Stacktrace:
=================================================================
          at <unknown> <0xffffffff>
          at SkiaSharp.SkiaApi:sk_surface_unref <0x000a2>
          at SkiaSharp.SKSurface:Dispose <0x0008a>
          at SkiaSharp.SKNativeObject:Finalize <0x00039>
          at System.Object:runtime_invoke_virtual_void__this__ <0x000ab>
=================================================================
[1]    20836 abort      OUTPUT=../ClassicAssistAvalonia/Output/net48 MONO_PATH=$OUTPUT = mono
```

## License

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
