# CSPluginEngine
A plugin system based in C#.
This system is using [CS-Script](http://www.csscript.net/).

The script main function need return any type. The class and function need be public.

To include external libraries use:
```csharp
//dll_include MyLibrary.dll
using MyLibrary;
using System;
...
```

## Example
Program.cs
```csharp
...
static void Main(string[] args)
{
	CSPluginEngine.CSPluginManager plugin = new CSPluginEngine.CSPluginManager("cs","plugins");
	plugin.Execute("example");
}
...
```

plugins/example.cs

```csharp
using System;
using System.Windows.Forms;

public class Script
{ 
    public int Main()
    {
        MessageBox.Show("Hi.", "CSPluginEngine");
		return 0;
    }
}
```