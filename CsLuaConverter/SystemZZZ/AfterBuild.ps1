

$solutionPath = $args[0] + "SystemZZZ\SytemZZZ.csproj";
$solutionPath = $args[0] + "CsLuaTest\CsLuaTest.csproj"
$outDir = $args[0] + "SystemZZZ\" + $args[1];

echo "Starting translating C# implementation to lua";
$client = "$outDir\CsLuaSyntaxTranslatorClient.exe";
$targetFile = "$solutionDir\..\..\CsLuaConverter\CsLuaConverter\Lua\System\SystemImplementation.lua";

& $client $solutionPath $targetFile
if ( $LASTEXITCODE -ne 0)
{
    exit $LASTEXITCODE;
}

# $csLuaConverter = "$solutionDir..\CsLuaConverter\CsLuaConverter.exe";
# $outDir = "$solutionDir\Out"
#$typeListGenerator = "$solutionDir\TypeListGenerator\bin\Debug\TypeListGenerator.exe";

# throw $solutionDir

#$systemImplementation = Get-Content "$outDir\CsImplementationAddOn\SystemZZZ.lua";
#$systemImplementation = $systemImplementation -replace "ZZZ", "";
#Set-Content -Path "$solutionDir\..\..\CsLuaConverter\CsLuaConverter\Lua\System\SystemImplementation.lua" -Value $systemImplementation;