$scriptPath = Split-Path -Path $MyInvocation.MyCommand.Definition -Parent
$pythonScript = "$scriptPath\ws-vendor.py"
Start-Process "python" -ArgumentList $pythonScript -Verb RunAs
