$version="0.0.1"
$program="NGordat-Controlpanel"

# Publish electron.
Write-Host "Electron publish"
$electronExe=$program + " Setup " + $version + ".exe"

if (Test-Path "./dist/electron/Packaged/$($electronExe)")
{
	Write-Host "Exe $($electronExe) found."
	Copy-Item -Path "./dist/electron/Packaged/$($electronExe)" -Destination "./src/statics/builds/$($electronExe)"
	Write-Host "Electron published."
}
else
{
	Write-Host "Exe not found. Electron not pusblished."
}
