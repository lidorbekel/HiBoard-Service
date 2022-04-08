cd ..\src\HiBoard.Persistence
dotnet-ef database update -s ..\HiBoard.Service\HiBoard.Service.csproj

Read-Host -Prompt "Press Enter to exit"