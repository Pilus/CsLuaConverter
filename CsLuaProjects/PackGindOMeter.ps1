
# Copy and pack Grind-O-Meter

$i = 1;
while (test-path "$env:TEMP\F$i") 
{
    $i = $i + 1;
}
$tempFolder = "$env:TEMP\F$i";

robocopy ".\BlizzardApi" "$tempFolder\BlizzardApi" "*.*" /S /XD bin obj
robocopy ".\CsLuaConverter" "$tempFolder\CsLuaConverter" "*.*" /S /XD bin obj
robocopy ".\GrindOMeter" "$tempFolder\GrindOMeter" "*.*" /S /XD bin obj
robocopy ".\GrindOMeter.IntegrationTests" "$tempFolder\GrindOMeter.IntegrationTests" "*.*" /S /XD bin obj
robocopy ".\GrindOMeter.UnitTests" "$tempFolder\GrindOMeter.UnitTests" "*.*" /S /XD bin obj
robocopy ".\GrindOMeter.CsLuaTrigger" "$tempFolder\GrindOMeter.CsLuaTrigger" "*.*" /S /XD bin obj
robocopy ".\TestUtils" "$tempFolder\TestUtils" "*.*" /S /XD bin obj
robocopy ".\WoWSimulator" "$tempFolder\WoWSimulator" "*.*" /S /XD bin obj
robocopy ".\" "$tempFolder" "GrindOMeter.sln"

function ZipFiles( $zipfilename, $sourcedir )
{
    if (test-path $zipfilename)
    {
        remove-item $zipfilename
    }

    Add-Type -Assembly System.IO.Compression.FileSystem
    $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
    [System.IO.Compression.ZipFile]::CreateFromDirectory($sourcedir, $zipfilename, $compressionLevel, $false)
    write-host $zipfilename
}


$packsFolder = resolve-path "..\Packs";
ZipFiles "$packsFolder\GrindOMeter.zip" $tempFolder