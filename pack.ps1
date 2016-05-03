$root = (split-path -parent $MyInvocation.MyCommand.Definition)

Write-Host $root
Write-Host "$root\src\Scribe\bin\Release\Scribe.dll"

$version = [System.Reflection.Assembly]::LoadFile("$root\src\Scribe\bin\Release\Scribe.dll").GetName().Version
$versionStr = "{0}.{1}.{2}.{3}" -f ($version.Major, $version.Minor, $version.Build, $version.Revision)

Write-Host "Setting .nuspec version tag to $versionStr"

$content = (Get-Content $root\src\Scribe.nuspec) 
$content = $content -replace '\$version\$',$versionStr

$content | Out-File $root\src\Scribe.compiled.nuspec

& $root\build\NuGet.exe pack $root\src\Scribe.compiled.nuspec