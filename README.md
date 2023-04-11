# _phoenix csharp interface_

![License](https://img.shields.io/github/license/GothicKit/phoenix-java-interface?label=License&color=important)
![.NET](https://img.shields.io/static/v1?label=.NET+Standard&message=2.1&color=informational)
![C#](https://img.shields.io/static/v1?label=C%23&message=8&color=informational)
![Version](https://img.shields.io/github/v/tag/GothicKit/phoenix-csharp-interface?label=Version&sort=semver)

C# bindings for the [phoenix](https://github.com/lmichaelis/phoenix) library for parsing game assets of
[PiranhaBytes'](https://www.piranha-bytes.com/) early 2000's games [Gothic](https://en.wikipedia.org/wiki/Gothic_(video_game))
and [Gothic II](https://en.wikipedia.org/wiki/Gothic_II). This library is based on [phoenix-shared-interface](https://github.com/GothicKit/phoenix-shared-interface).

## building

You will need:

* .NET Standard 2.1 (for PxCs) and .NET 6 (for PxCs.Tests), anything onward should work as well (tested with 7.x)
* Git

To build _phoenix-csharp-interface_ from scratch, just open a terminal in a directory of your choice and run

```bash
git clone --recursive https://github.com/GothicKit/phoenix-csharp-interface
cd phoenix-csharp-interface
dotnet build
```

You will find the built library in `bin/Debug/net6/`.


## testing


To run tests on the classlib, run

**preparation**  
You need to have a version of the phoenix-shared-interface library. This won't be bundled by this project.
1. Build the shared library from this project [GothicKit/phoenix-shared-interface](https://github.com/GothicKit/phoenix-shared-interface)
2. Put your .dll/.so into ./PxCs.Tests/ConfigFiles

**execution**  
```sh
dotnet test --environment GOTHIC1_ASSET_DIR="PATH_TO_YOUR_GOTHIC1_ASSETS_ROOT_FOLDER"
```