# Code Generator

<p align="center">
	<img alt="GitHub package.json version" src ="https://img.shields.io/github/package-json/v/Thundernerd/Unity3D-CodeGenerator" />
	<a href="https://github.com/Thundernerd/Unity3D-CodeGenerator/issues">
		<img alt="GitHub issues" src ="https://img.shields.io/github/issues/Thundernerd/Unity3D-CodeGenerator" />
	</a>
	<a href="https://github.com/Thundernerd/Unity3D-CodeGenerator/pulls">
		<img alt="GitHub pull requests" src ="https://img.shields.io/github/issues-pr/Thundernerd/Unity3D-CodeGenerator" />
	</a>
	<a href="https://github.com/Thundernerd/Unity3D-CodeGenerator/blob/master/LICENSE.md">
		<img alt="GitHub license" src ="https://img.shields.io/github/license/Thundernerd/Unity3D-CodeGenerator" />
	</a>
	<img alt="GitHub last commit" src ="https://img.shields.io/github/last-commit/Thundernerd/Unity3D-CodeGenerator" />
</p>

Provides a collection of classes that will aid you with generating code.

## Installation
1. The package is available on the [openupm registry](https://openupm.com). You can install it via [openupm-cli](https://github.com/openupm/openupm-cli).
```
openupm add net.tnrd.codegenerator
```

2. Installing through a [Unity Package](http://package-installer.glitch.me/v1/installer/package.openupm.com/net.tnrd.codegenerator?registry=https://package.openupm.com) created by the [Package Installer Creator](https://package-installer.glitch.me) from [Needle](https://needle.tools)

[<img src="https://img.shields.io/badge/-Download-success?style=for-the-badge"/>](http://package-installer.glitch.me/v1/installer/package.openupm.com/net.tnrd.codegenerator?registry=https://package.openupm.com)

## Usage
_The following example is based on the package Layers & Tags Generator_

```c#
public void Generate()
{
    var tags = InternalEditorUtility.tags;

    var generator = new Generator();
    var tagsClass = new Class("Tags");

    foreach (var tag in tags)
    {
        tagsClass.AddField(
            new Field(Utilities.GetScreamName(tag), tag, typeof(string))
        {
            IsConst = true,
        });
    }

    generator.AddClass(tagsClass);
    generator.SaveToFile(Application.dataPath + "/Generated/Tags.cs");

    AssetDatabase.Refresh();
}
```

In the example above you can see the generator being used.

It starts with a Generator, which holds all of the items that will be generated. In this example a class named Tags will be added.

The Tags class contains some simple fields that have the name of the tag in SCREAM_CASE and are assigned the value. The type of the fields are of string and they are marked as constant for easy access.


## Support
**Code Generator** is an open-source project that I hope helps other people. It is by no means necessary but if you feel generous you can support me by donating.

[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/J3J11GEYY)

## Contributions
Pull requests are welcomed. Please feel free to fix any issues you find, or add new features.

