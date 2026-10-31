# Run once on your machine (with .NET 10 SDK installed) inside the repo root, on the correct branch.
# Creates the full solution structure per the agreed Clean Architecture:
# Domain depends on nothing -> Application -> Infrastructure, with Api/Web as independent outer layers.

$ErrorActionPreference = "Stop"

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    Write-Error "dotnet SDK not found on this machine. Install .NET 10 LTS first."
    exit 1
}

Write-Host "== Detected local SDK version =="
dotnet --version

New-Item -ItemType Directory -Force -Path "src" | Out-Null
Set-Location "src"

dotnet new classlib   -n Muhadarati.Domain         -o Muhadarati.Domain
dotnet new classlib   -n Muhadarati.Application    -o Muhadarati.Application
dotnet new classlib   -n Muhadarati.Infrastructure -o Muhadarati.Infrastructure
dotnet new classlib   -n Muhadarati.Shared          -o Muhadarati.Shared
dotnet new webapi     -n Muhadarati.Api             -o Muhadarati.Api --use-controllers
dotnet new blazorwasm -n Muhadarati.Web             -o Muhadarati.Web

Set-Location ..
dotnet new sln -n Muhadarati

$projects = @(
    "src/Muhadarati.Domain",
    "src/Muhadarati.Application",
    "src/Muhadarati.Infrastructure",
    "src/Muhadarati.Shared",
    "src/Muhadarati.Api",
    "src/Muhadarati.Web"
)
foreach ($proj in $projects) {
    dotnet sln add $proj
}

# Strict dependency rule
dotnet add src/Muhadarati.Application    reference src/Muhadarati.Domain
dotnet add src/Muhadarati.Infrastructure reference src/Muhadarati.Application
dotnet add src/Muhadarati.Api            reference src/Muhadarati.Application
dotnet add src/Muhadarati.Api            reference src/Muhadarati.Infrastructure
dotnet add src/Muhadarati.Api            reference src/Muhadarati.Shared

# Web references Shared only -- explicit security rule (never holds a secret, since its code ships fully to the browser)
dotnet add src/Muhadarati.Web reference src/Muhadarati.Shared

Write-Host ""
Write-Host "== Structure created. Now verify: =="
Write-Host "  dotnet build"
Write-Host ""
Write-Host "If the build fails due to NuGet restore issues, check your internet connection on this machine."
