if (-not (Get-Command choco -ErrorAction SilentlyContinue)) {
    Write-Host "installing Chocolatey..."
    Set-ExecutionPolicy Bypass -Scope Process -Force
    [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12
    Invoke-WebRequest https://community.chocolatey.org/install.ps1 -UseBasicP... -OutFile install-choco.ps1
    .\install-choco.ps1
    Remove-Item install-choco.ps1 -Force
}

if (-not (Get-Command python -ErrorAction SilentlyContinue)) {
    Write-Host "installing python..."
    choco install python -y
}

$requirementsFile = ".\requirements.txt"
if (Test-Path $requirementsFile) {
    Write-Host "installing requirements.txt..."
    python -m pip install -r $requirementsFile
} else {
    Write-Host "Requirements.txt file not found in current folder."
}
