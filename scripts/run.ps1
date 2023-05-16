<#
.SYNOPSIS
Runs the main project and external services.

.DESCRIPTION
By default, runs the main project. If the -RunExternalServices switch is specified, also runs the external services.

.PARAMETER RunExternalServices
Indicates whether to run the external services.

.EXAMPLE
.\RunProjects.ps1
Runs only the main project.

.EXAMPLE
.\RunProjects.ps1 -RunExternalServices
Runs both the main project and the external services.

#>

param (
    [Parameter(HelpMessage = "Indicates whether to run the external services")]
    [Alias("runext")]
    [switch]$RunExternalServices
)

function RunProject($projectPath) {
    try {
        $arguments = @(
            "run",
            "--project",
            $projectPath
        )
        Start-Process -FilePath "dotnet" -ArgumentList $arguments -PassThru
    } catch {
        Write-Error "Failed to run project '$projectPath'. Error: $_"
    }
}

$apiProject = "..\src\Books.API\"
RunProject $apiProject

if ($RunExternalServices) {
    $externalServicesPath = "..\ExternalServices\"
    $externalServices = Get-ChildItem $externalServicesPath -Directory

    foreach ($service in $externalServices) {
        $serviceProject = Join-Path -Path $service.FullName -ChildPath "$($service.Name).csproj"
        RunProject $serviceProject
    }
}