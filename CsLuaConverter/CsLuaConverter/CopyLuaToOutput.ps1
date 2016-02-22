#
# CopyLuaToOutput.ps1
#
param (
	[string] $projectPath
)
$ErrorActionPreference = "Stop"

$outputFile = (Get-Location).Path + "\CsLua.lua";

echo "Copying: $projectPath => $outputFile" 

$LuaFiles = @()

function getFiles ([string] $path)
{
	get-childitem $path | where {$_.extension -eq ".lua"} | % { $LuaFiles += $_.fullname }
	get-childitem $path | where {$_.PSIsContainer } | % { getFiles $_.Fullname }
}

getFiles $projectPath

#get-childitem $projectPath -recurse | where {$_.extension -eq ".lua"} | % { $LuaFiles += $_.fullname }
cat $LuaFiles > $outputFile