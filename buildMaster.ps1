$msbuild = "C:\Program Files (x86)\MSBuild\14.0\Bin\Msbuild.exe";

#& $msbuild @(".\Master.proj", "/target:Build")
function StopOnFailed
{
    if ($LASTEXITCODE -ne 0)
    {
        exit;
    }
}

& $msbuild @(".\CsLuaConverter\CsLuaConverter.sln", "/target:Build")
StopOnFailed
& $msbuild @(".\CsLuaProjects\CsImplementation\CsImplementation.sln", "/target:Build")
StopOnFailed
& $msbuild @(".\CsLuaConverter\CsLuaConverter.sln", "/target:ReBuild")
StopOnFailed
& $msbuild @(".\CsLuaProjects\CsLuaTest.sln", "/target:ReBuild")
StopOnFailed