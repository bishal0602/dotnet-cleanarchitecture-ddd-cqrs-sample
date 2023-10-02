<#
.SYNOPSIS
Remove EF Core Migrations

.DESCRIPTION
This command removes EF Core migrations. If the -All parameter is provided, it will remove all migrations one by one.

.PARAMETER All
Indicates whether to remove all migrations

.EXAMPLE
.\RemoveMigrations.ps1 -All
Removes all EF Core migrations

.EXAMPLE
.\RemoveMigrations.ps1
Removes the most recent EF Core migration

#>

param(
    [Parameter(HelpMessage = "Whether to remove all migrations")]
    [switch]$All
)
. .\SharedVariables.ps1 
function RemoveMigrations {
    param(
        [string]$ProjectPath,
        [string]$SourcePath
    )

    dotnet ef migrations remove -p $ProjectPath -s $SourcePath
}

if ($All) {
    $migrations = dotnet ef migrations list -p ..\src\Books.Infrastructure\ -s ..\src\Books.API\ --json
    $filtered = Select-String -InputObject $migrations -Pattern '\[([\s\S]+)\]' -AllMatches | ForEach-Object { $_.Matches } | ConvertFrom-Json

    $filtered | ForEach-Object {
        RemoveMigrations -ProjectPath $infrastructureProjectPath -SourcePath $startupProjectPath
    }
}
else {
    RemoveMigrations -ProjectPath $infrastructureProjectPath -SourcePath $startupProjectPath
}
