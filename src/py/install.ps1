if (-not (Get-Command choco -ErrorAction SilentlyContinue)) {
    Write-Host "installing Chocolatey..."
    Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))
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

Read-Host -Prompt "Press Enter to exit"