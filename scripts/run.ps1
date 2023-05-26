<#
.SYNOPSIS
Runs api, clients and external services.

.DESCRIPTION
By default, runs the api. Use -RunExternalServices to run the external services and -RunBlazorWasm to run the Blazor frontend.

.PARAMETER RunExternalServices
Indicates whether to run the external services.
.PARAMETER RunBlazorWasm
Indicates whether to run Blazor frontend.

.EXAMPLE
.\RunProjects.ps1
Runs only the api.

.EXAMPLE
.\RunProjects.ps1 -RunExternalServices
Runs both the api and the external services.

.EXAMPLE
.\RunProjects.ps1 -RunBlazorWasm
Runs both the api and the Blazor frontend.


.EXAMPLE
.\RunProjects.ps1 -RunReact
Runs both the API and the React project.`
#>

param (
    [Parameter(HelpMessage = "Indicates whether to run the external services")]
    [Alias("external")]
    [switch]$RunExternalServices,

    [Parameter(HelpMessage = "Indicates whether to run Blazor frontend")]
    [Alias("blazor")]
    [switch]$RunBlazorWasm,

    [Parameter(HelpMessage = "Indicates whether to run the React project")]
    [Alias("react")]
    [switch]$RunReact
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

$api = "..\src\Books.API\"
RunProject $api

if ($RunExternalServices) {
    $externalServicesPath = "..\ExternalServices\"
    $externalServices = Get-ChildItem $externalServicesPath -Directory
    
    foreach ($service in $externalServices) {
        $serviceProject = Join-Path -Path $service.FullName -ChildPath "$($service.Name).csproj"
        RunProject $serviceProject
    }
}

if($RunBlazorWasm){
    $webUI = "..\Clients\Books.BlazorWasm\"
    RunProject $webUI
}

if ($RunReact) {
    $reactUI = "..\Clients\Books.ReactUI\"
    Start-Process "npm" -ArgumentList "run dev" -WorkingDirectory $reactUI -PassThru
}