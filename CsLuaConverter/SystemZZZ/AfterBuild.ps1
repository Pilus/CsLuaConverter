

$solutionPath = $args[0] + "SystemZZZ\SytemZZZ.csproj";
$outDir = $args[0] + "SystemZZZ\" + $args[1];

echo "Starting translating C# implementation to lua";

# Load needed dlls
[System.Reflection.Assembly]::LoadFile($outDir + "CsLuaSyntaxTranslator.dll");
[System.Reflection.Assembly]::LoadFile($outDir + "Microsoft.CodeAnalysis.Workspaces.dll");
[System.Reflection.Assembly]::LoadFile($outDir + "Microsoft.CodeAnalysis.dll");

echo "Solution path: $solutionPath";
$namespaceConstructor = new-object CsLuaSyntaxTranslator.NamespaceConstructor;
$namespaceAction = $namespaceConstructor.GetNamespacesFromProject($solutionDir);

# $csLuaConverter = "$solutionDir..\CsLuaConverter\CsLuaConverter.exe";
# $outDir = "$solutionDir\Out"
#$typeListGenerator = "$solutionDir\TypeListGenerator\bin\Debug\TypeListGenerator.exe";

# throw $solutionDir

#$systemImplementation = Get-Content "$outDir\CsImplementationAddOn\SystemZZZ.lua";
#$systemImplementation = $systemImplementation -replace "ZZZ", "";
#Set-Content -Path "$solutionDir\..\..\CsLuaConverter\CsLuaConverter\Lua\System\SystemImplementation.lua" -Value $systemImplementation;