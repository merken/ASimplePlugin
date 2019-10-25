var target = Argument("target", "default");
var configuration = Argument("configuration", "Debug");
var pluginProjectDir = "SimplePlugin";
var destinationDir = "AppHost/bin/Debug/netcoreapp3.0/Plugins";

private void DeleteIfExists(string directory){
     var deleteSettings = new DeleteDirectorySettings{
        Force= true,
        Recursive = true
    };
     if (DirectoryExists(directory))
    {
      DeleteDirectory(directory, deleteSettings);
    }
}

private void CleanProject(string projectDirectory){
    var projectFile = $"{projectDirectory}\\{projectDirectory}.csproj";
    var bin = $"{projectDirectory}\\bin";
    var obj = $"{projectDirectory}\\obj";
    var cleanSettings = new DotNetCoreCleanSettings
    {
        Configuration = configuration
    };
    DeleteIfExists(bin);
    DeleteIfExists(obj);
    DotNetCoreClean(projectFile, cleanSettings);
}

Task("clean").Does( () =>
{ 
    CleanProject(pluginProjectDir);
    DeleteIfExists(destinationDir);
});

Task("build")
  .IsDependentOn("clean")
  .Does( () =>
{ 
    var settings = new DotNetCoreBuildSettings
    {
        Configuration = configuration
    };

    DotNetCoreBuild($"{pluginProjectDir}\\{pluginProjectDir}.csproj", settings);
});

Task("publish")
  .IsDependentOn("build")
  .Does(() =>
  { 
    DotNetCorePublish($"{pluginProjectDir}\\{pluginProjectDir}.csproj", new DotNetCorePublishSettings
    {
        NoBuild = true,
        Configuration = configuration,
        OutputDirectory = "publish",
        NoDependencies = true
    });
  });

Task("copy-to-host")
  .IsDependentOn("publish")
  .Does(() =>
  { 
    CopyDirectory("publish", destinationDir);
  });

Task("default")
  .IsDependentOn("copy-to-host");

RunTarget(target);