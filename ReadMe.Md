#### Demonstration project for bank account management

### Project creation walkthrough

 1. Used AI to generate the database tables
 2. Created database project
 3. Created database table files and post deploy from generated code
 4. Used AI to generate the DAL classes
	1. Install entity framework core
		select DAL project
		```bash
		dotnet add package Microsoft.EntityFrameworkCore.SqlServer
		```
 5. Created Class library project for DAL
 6. Created DAL classes from generated code
 7. Used AI to generate BL classes
 8. Created Class library project for BL
 9. Created BL classes from generated code
10. Used AI to generated gRPC Web API classes and proto file
11. Created ASP.NET Web API gRPC Service project
	1. Add project references
	```bash
	Install-Package Grpc.AspNetCore -Version 2.55.1
	Install-Package Grpc.Tools -Version 2.55.1
	Install-Package Google.Protobuf -Version 3.27.0
	```
	2. Change .proto file properties 
	Build action: ProtoBuf compiler
	Set to generate client and server code
	it should generate on build to C:\Users\josef\source\repos\BankAccount.Database\BankAccount.WebApi.gRPCService\obj\Debug\net8.0\Protos\GreetGrpc.cs
	add those files to project to prevent duplication, because they will be generated on each build, unless you configure otherwise
	
	3. Command to generate grpc classes manually, if needed
	```bash
	C:\Users\josef\.nuget\packages\grpc.tools\2.55.0\tools\windows_x64\protoc.exe --csharp_out=../Generated --grpc_out=../Generated --plugin=protoc-gen-grpc=C:\Users\josef\.nuget\packages\grpc.tools\2.55.0\tools\windows_x64\grpc_csharp_plugin.exe bank_service.proto
	```
	4. If there are build errors in the generated, files it is due to tool version conflict
12. Create gRPC test client cosole
	1. Install packages
	```bash
	Install-Package Grpc.Net.Client
	Install-Package Google.Protobuf
	Install-Package Grpc.Tools
	```
13. Create NUnit test project
	1. install moq
	```bash
	install-package moq
	```
14. Configure sql server express
	1. create user dockerUser mypass
	2. Open SQL Server Configuration Manager and Enable TCP/IP in Server Network Configuration 
	https://stackoverflow.com/questions/50166869/connect-to-sql-server-in-local-machine-host-from-docker-using-host-docker-inte
	In SQL Server Configuration Manager, right-click TCP/IP under Protocols for SQLEXPRESS and choose Properties.
	1. Go to the IP Addresses tab.
	1. Scroll down to IPAll and ensure that the TCP Port is set to 1433 (or your preferred port).
	1. In SQL Server Configuration Manager, right-click TCP/IP under Protocols for SQLEXPRESS and choose Properties.
	1. Go to the IP Addresses tab.
	1. Scroll down to IPAll and ensure that the TCP Port is set to 1433 (or your preferred port).
	1. Open SQL Server Management Studio (SSMS).
	1. Connect to the Server. Right-click on the Server and choose Properties. Go to the Security tab.
	1. Ensure that SQL Server and Windows Authentication Mode is selected. Restart SQL Server for the changes to take effect.
	 
	

#### Install entity framework support for migrations (optional)
```bash
Install-Package Microsoft.EntityFrameworkCore.Design
```

### References

Create a gRPC client and server in ASP.NET Core

https://learn.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-8.0&tabs=visual-studio