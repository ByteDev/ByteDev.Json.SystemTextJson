#addin "nuget:?package=Cake.Incubator&version=6.0.0"
#addin "nuget:?package=Cake.Powershell&version=1.0.1"
#tool "nuget:?package=NUnit.ConsoleRunner&version=3.12.0"
#tool "nuget:?package=GitVersion.CommandLine&version=5.6.10"
#load "ByteDev.Utilities.cake"

var solutionName = "ByteDev.Json.SystemTextJson";
var projName = "ByteDev.Json.SystemTextJson";

var solutionFilePath = "../" + solutionName + ".sln";
var projFilePath = "../src/" + projName + "/" + projName + ".csproj";
var nuspecFilePath = projName + ".nuspec";

var target = Argument("target", "Default");

var artifactsDirectory = Directory("../artifacts");
var nugetDirectory = artifactsDirectory + Directory("NuGet");

var configuration = GetBuildConfiguration();

Information("Configurtion: " + configuration);

Task("CheckNuspec")
	.Description("Check nuspec file is valid")
	.Does(() =>
	{
		StartPowershellFile("./nuspec-check.ps1", args =>
        {
            args.Append(projFilePath)
				.Append(nuspecFilePath);
        });
	});

Task("Clean")
    .IsDependentOn("CheckNuspec")
	.Does(() =>
	{
		CleanDirectory(artifactsDirectory);
	
		CleanBinDirectories();
		CleanObjDirectories();
	});

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
		var settings = new NuGetRestoreSettings
		{
			Source = new[] { "https://api.nuget.org/v3/index.json" }
		};

		NuGetRestore(solutionFilePath, settings);
    });

Task("Build")
	.IsDependentOn("Restore")
    .Does(() =>
	{	
		var settings = new DotNetCoreBuildSettings
        {
            Configuration = configuration
        };

        DotNetCoreBuild(solutionFilePath, settings);
	});

Task("UnitTests")
    .IsDependentOn("Build")
    .Does(() =>
	{
		var settings = new DotNetCoreTestSettings
		{
			Configuration = configuration,
			NoBuild = true
		};

		DotNetCoreUnitTests(settings);
	});
	
Task("CreateNuGetPackages")
    .IsDependentOn("UnitTests")
    .Does(() =>
    {
		var nugetVersion = GetNuGetVersion();

		var nugetSettings = new NuGetPackSettings 
		{
			Version = nugetVersion,
			OutputDirectory = nugetDirectory
		};
                
		NuGetPack(nuspecFilePath, nugetSettings);
    });

   
Task("Default")
    .IsDependentOn("CreateNuGetPackages");

RunTarget(target);
