
# Copy and pack MyAddOn

$i = 1;
while (test-path "$env:TEMP\F$i") 
{
    $i = $i + 1;
}
$tempFolder = "$env:TEMP\F$i";

robocopy ".\BlizzardApi" "$tempFolder\BlizzardApi" "*.*" /S /XD bin obj
robocopy ".\CsLuaConverter" "$tempFolder\CsLuaConverter" "*.*" /S /XD bin obj
robocopy ".\MyAddOn" "$tempFolder\MyAddOn" "*.*" /S /XD bin obj
robocopy ".\MyAddOn.CsLuaTrigger" "$tempFolder\MyAddOn.CsLuaTrigger" "*.*" /S /XD bin obj
robocopy ".\" "$tempFolder" "MyAddOn.sln"

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
ZipFiles "$packsFolder\MyAddOn.zip" $tempFolder