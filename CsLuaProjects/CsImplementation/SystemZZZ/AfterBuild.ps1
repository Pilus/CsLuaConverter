

$solutionDir = $args;
$csLuaConverter = "$solutionDir..\CsLuaConverter\CsLuaConverter.exe";
$outDir = "$solutionDir\Out"
$typeListGenerator = "$solutionDir\TypeListGenerator\bin\Debug\TypeListGenerator.exe";

Write-host "Running cs lua converter at $csLuaConverter"
& $csLuaConverter "$solutionDir\CsImplementation.sln" $outDir
if ( $LASTEXITCODE -ne 0)
{
    exit $LASTEXITCODE;
}
$systemImplementation = Get-Content "$outDir\CsImplementationAddOn\SystemZZZ.lua";
$systemImplementation = $systemImplementation -replace "ZZZ", "";
Set-Content -Path "$solutionDir\..\..\CsLuaConverter\CsLuaConverter\Lua\System\SystemImplementation.lua" -Value $systemImplementation;


& $typeListGenerator "$solutionDir\..\..\CsLuaConverter\CsLuaConverter\SystemImplementation.types"
if ( $LASTEXITCODE -ne 0)
{
    write-host "Error generating type list";
    exit $LASTEXITCODE;
}