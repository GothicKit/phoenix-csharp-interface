# _phoenix csharp interface_

![License](https://img.shields.io/github/license/GothicKit/phoenix-java-interface?label=License&color=important)
![.NET](https://img.shields.io/static/v1?label=.NET&message=6&color=informational)
![C#](https://img.shields.io/static/v1?label=C%23&message=8&color=informational)
![Version](https://img.shields.io/github/v/tag/GothicKit/phoenix-csharp-interface?label=Version&sort=semver)

C# bindings for the [phoenix](https://github.com/lmichaelis/phoenix) library for parsing game assets of
[PiranhaBytes'](https://www.piranha-bytes.com/) early 2000's games [Gothic](https://en.wikipedia.org/wiki/Gothic_(video_game))
and [Gothic II](https://en.wikipedia.org/wiki/Gothic_II). This library is based on [phoenix-shared-interface](https://github.com/GothicKit/phoenix-shared-interface).

## building

You will need:

* .NET 6.x, anything onward should work as well (tested with 7.x)
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
```sh
dotnet test --environment GOTHIC_ASSET_DIR="PATH_TO_YOUR_GOTHIC_ASSETS_ROOT_FOLDER"
```