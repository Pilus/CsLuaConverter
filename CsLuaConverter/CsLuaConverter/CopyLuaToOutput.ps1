#
# CopyLuaToOutput.ps1
#
param (
	[string] $projectPath
)
$outputFile = (Get-Location).Path + "\CsLua.lua";

echo "Copying: $projectPath => $outputFile" 

$LuaFiles = @()
get-childitem $projectPath -recurse | where {$_.extension -eq ".lua"} | % { $LuaFiles += $_.fullname }
cat $LuaFiles > $outputFile
