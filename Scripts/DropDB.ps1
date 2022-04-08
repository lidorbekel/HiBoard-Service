cd ..\src\HiBoard.Persistence
dotnet-ef database drop -s ..\HiBoard.Service\HiBoard.Service.csproj

Read-Host -Prompt "Press Enter to exit"