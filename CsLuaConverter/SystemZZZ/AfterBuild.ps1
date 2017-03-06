

$solutionDir = $args[0];
$outDir = $args[0] + "SystemZZZ\" + $args[1];

echo "??? $solutionDir :: $outDir"

# $csLuaConverter = "$solutionDir..\CsLuaConverter\CsLuaConverter.exe";
# $outDir = "$solutionDir\Out"
#$typeListGenerator = "$solutionDir\TypeListGenerator\bin\Debug\TypeListGenerator.exe";

# throw $solutionDir

#$systemImplementation = Get-Content "$outDir\CsImplementationAddOn\SystemZZZ.lua";
#$systemImplementation = $systemImplementation -replace "ZZZ", "";
#Set-Content -Path "$solutionDir\..\..\CsLuaConverter\CsLuaConverter\Lua\System\SystemImplementation.lua" -Value $systemImplementation;