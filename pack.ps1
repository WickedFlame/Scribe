$root = (split-path -parent $MyInvocation.MyCommand.Definition)

$version = [System.Reflection.Assembly]::LoadFile("$root\src\Scribe.Configuration\bin\Release\Scribe.Configuration.dll").GetName().Version
$versionStr = "{0}.{1}.{2}-RC0{3}" -f ($version.Major, $version.Minor, $version.Build, $version.Revision)

Write-Host "Setting .nuspec version tag to $versionStr"

$content = (Get-Content $root\src\Scribe.Configuration.nuspec) 
$content = $content -replace '\$version\$',$versionStr

$content | Out-File $root\src\Scribe.Configuration.compiled.nuspec

& $root\build\NuGet.exe pack $root\src\Scribe.Configuration.compiled.nuspec