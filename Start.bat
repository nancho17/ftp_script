::PowerShell ./dotnet-install.ps1
dotnet run Program.cs --framework netcoreapp3.1 --force
echo %tmp%


::Command to execute at PowerShell:
::#dism.exe /online /enable-feature /featurename:NetFX3 /All /Source:c:\temp\sxs /LimitAccess
::#Change c:\temp\sxs to location of sxs folder, if you've pasted it to another location.