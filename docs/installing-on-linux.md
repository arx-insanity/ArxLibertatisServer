# Installing and running on Linux

## Installing

```sh
sudo apt update
sudo apt install dotnet6
```

_(source: https://devblogs.microsoft.com/dotnet/dotnet-6-is-now-in-ubuntu-2204/)_

Test dotnet by running the following command:

```sh
dotnet --info
```

If the `.NET SDKs installed` section says `No SDKs were found.` then run the following steps:

```sh
sudo apt remove 'dotnet*'
sudo apt remove 'aspnetcore*'

sudo rm /etc/apt/sources.list.d/microsoft-prod.list

sudo apt update
sudo apt install dotnet-sdk-6.0
```

_(source: https://stackoverflow.com/a/73315318/1806628)_

Check `dotnet --info` again and you should see `6.0.110 [/usr/lib/dotnet/dotnet6-6.0.110/sdk]` or something similar
installed.

## Running the project

First go into the project's folder

```sh
cd ArxLibertatisServer
```

### Compiling

```sh
dotnet build
```

### Running

```sh
dotnet bin/Debug/netcoreapp3.1/ArxLibertatisServer.dll
```
