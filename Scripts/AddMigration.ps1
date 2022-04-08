cd ..\src\HiBoard.Persistence

$migrationName = Read-Host -Prompt 'Enter Migration Name'

Write-Host "Executing command: dotnet-ef migrations add '$migrationName' -s ..\HiBoard.Service\HiBoard.Service.csproj"
dotnet-ef migrations add $migrationName -s ..\HiBoard.Service\HiBoard.Service.csproj

Read-Host -Prompt "Press Enter to exit"