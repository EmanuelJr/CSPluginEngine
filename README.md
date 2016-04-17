# CSPluginEngine
A plugin system based in C#.
This system is using [CS-Script](http://cs-script.net).

The script main function needs return any type and function and class need be publics.

For includes external library use:
```
//dll_include MyLibrary.dll
using MyLibrary;
using System;
...
```

## Example
Program.cs
```
...
static void Main(string[] args)
{
	CSPluginEngine.CSPluginManager plugin = new CSPluginEngine.CSPluginManager("cs","plugins");
	plugin.Execute("example");
}
...
```

plugins/example.cs

```
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

