
$solutionDir = $args;
$csLuaConverter = "$solutionDir\..\CsLuaConverter\CsLuaConverter.exe";
$outDir = "$solutionDir\Out"
$typeListGenerator = "$solutionDir\TypeListGenerator\bin\Debug\TypeListGenerator.exe";

& $csLuaConverter "$solutionDir\CsImplementation.sln" $outDir
if ( $LASTEXITCODE -ne 0)
{
    exit $LASTEXITCODE;
}

& $typeListGenerator "$outDir\Types.txt"
if ( $LASTEXITCODE -ne 0)
{
    write-host "Error generating type list";
    exit $LASTEXITCODE;
}
