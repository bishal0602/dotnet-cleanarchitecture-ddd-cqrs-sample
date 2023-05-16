<#
.SYNOPSIS
    Add a new migration to the project.

.DESCRIPTION
    This command adds a new migration to the project. It requires specifying the name of the migration using the `-Name` parameter.

.PARAMETER Name
    Specifies the name of the migration.

    -Name <string>
        Specifies the name of the migration.

.EXAMPLE
    .\add-migrations.ps1 -Name MyMigration
    Adds a new migration named "MyMigration" to the project.
#>

param (
    [Parameter(Mandatory=$true, HelpMessage="The name of the migration")]
    [Alias("n")]
    [string]$Name
)

dotnet ef migrations add $Name -p ..\src\Books.Infrastructure\  -s ..\src\Books.API\