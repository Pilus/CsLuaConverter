
$solutionDir = $args;
$csLuaConverter = "$solutionDir\..\CsLuaConverter\CsLuaConverter.exe";
$outDir = "$solutionDir\Out"

& $csLuaConverter "$solutionDir\CsImplementation.sln" $outDir
if ( $LASTEXITCODE -ne 0)
{
    exit $LASTEXITCODE;
}