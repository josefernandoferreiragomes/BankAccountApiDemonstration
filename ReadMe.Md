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
	Install-Package Google.Protobuf
	Install-Package Grpc.Tools

	```
	2. Change .proto file properties 
	Build action: ProtoBuf compiler
	Set to generate client and server code
	
#### Install entity framework support for migrations (optional)
```bash
Install-Package Microsoft.EntityFrameworkCore.Design
```