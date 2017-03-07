

$solutionPath = $args[0] + "SystemZZZ\SystemZZZ.csproj";
$outDir = $args[0] + "SystemZZZ\" + $args[1];

echo "Starting translating C# implementation to lua";
$client = $args[0] + "CsLuaSyntaxTranslatorClient\" + $args[1] + "CsLuaSyntaxTranslatorClient.exe";
$targetFile = $args[0] + "..\..\CsLuaConverter\CsLuaConverter\CsLuaConverter\Lua\System\SystemImplementation.lua";
echo "Target file: $targetFile";

& $client $solutionPath $targetFile
if ( $LASTEXITCODE -ne 0)
{
    exit $LASTEXITCODE;
}

$systemImplementation = Get-Content $targetFile;
$systemImplementation = $systemImplementation -replace "ZZZ", "";
Set-Content -Path $targetFile -Value $systemImplementation;

echo "Completed translating C# implementation to lua"
