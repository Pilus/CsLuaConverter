#
# CopyLuaToOutput.ps1
#
param (
	[string] $projectPath
)
$outputFile = (Get-Location).Path + "\CsLuaMeta.lua";

echo "Copying: $projectPath => $outputPath" 

$Dir = get-childitem $projectPath -recurse
$List = $Dir | where {$_.extension -eq ".lua"}
$List |ft fullname |out $X

