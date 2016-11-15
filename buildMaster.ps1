$msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\Msbuild.exe";

& $msbuild @(".\Master.proj", "/target:Build")