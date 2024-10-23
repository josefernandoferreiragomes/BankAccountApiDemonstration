### Demonstration API project with example bank account management operations 

The purpose of this project is to demonstrate a gRPC API with Sdk
The Business layer is built to facilitate swapping database or exposing in multiple API types

Further projects include swapping the Sql Server database for MySql, or exposing the service using OpenAPI

Currently initiating the OpenAPI project

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
	it should generate on build to C:\Users\<user>\source\repos\BankAccount.Database\BankAccount.WebApi.gRPCService\obj\Debug\net8.0\Protos\GreetGrpc.cs
	add those files to project to prevent duplication, because they will be generated on each build, unless you configure otherwise
	
	3. Command to generate grpc classes manually, if needed
	```bash
	C:\Users\<user>\.nuget\packages\grpc.tools\2.55.0\tools\windows_x64\protoc.exe --csharp_out=../Generated --grpc_out=../Generated --plugin=protoc-gen-grpc=C:\Users\<user>\.nuget\packages\grpc.tools\2.55.0\tools\windows_x64\grpc_csharp_plugin.exe bank_service.proto
	```
	4. If there are build errors in the generated, files it is due to tool version conflict
12. Create gRPC test client client
	1. Install packages
	```bash
	Install-Package Grpc.Net.Client
	Install-Package Google.Protobuf
	Install-Package Grpc.Tools
	```
	2. copy the protos and configure them to generate client only  

13. Create NUnit test project
	1. install moq
	```bash
	install-package moq
	```
	2. configure easy grpc method discovery
	```bash
	dotnet add package Grpc.AspNetCore.Server.Reflection --version 2.66.0
	 ```
	3. with chocolatey install go 
	```bash
	choco install go
	```
	4. install from go
	```bash
	go install github.com/fullstorydev/grpcurl/cmd/grpcurl@latest
	```
	5. restart the machine
	6. describe the service:
	```bash
	grpcurl localhost:32770 describe
	```
	7. test one method in the command line
	```bash
	grpcurl -d '{\"Name\": 13234 }' localhost:32771 greet.Greeter/SayHello
	```
	8. install grpcui
	```bash
	go install github.com/fullstorydev/grpcui/cmd/grpcui@latest
	```
	9. open grpcui
	```bash
	grpcui localhost:32771
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
	1. start sql server browser 
	
15. create openApi project as another exposure tachnology, using the remaining BL and DAL
	1. in case swagger does not open (the port might change)
	https://localhost:32773/swagger/index.html
16. create sdk project
	1. add newtonsoft.json
	```bash
	dotnet add package Newtonsoft.Json
	```
	1. Generate client sdk
	```bash
	1. nswag openapi2csclient /input:https://localhost:32773/swagger/v1/swagger.json /classname:BankAccountOpenApiSdk /namespace:BankAccountOpenApiSdk.Client /output:BankAccountOpenApiSdk.cs
	```
17. create nunit smoke test
	1. 
#### Notice the ports are still dynaic and must be updated in the smoke test, until configuration for fixed port is made

#### Install entity framework support for migrations (optional)
```bash
Install-Package Microsoft.EntityFrameworkCore.Design
```

### References

Create a gRPC client and server in ASP.NET Core

https://learn.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-8.0&tabs=visual-studio

Primary constructor

https://devblogs.microsoft.com/dotnet/csharp-primary-constructors-refactoring/

Configure service method discovery

https://learn.microsoft.com/en-us/aspnet/core/grpc/test-tools?view=aspnetcore-8.0

### Miscellaneous
Script executed when getting clients from entity framework in sql server
```sql
exec sp_executesql N'SELECT [t].[CustomerId], [t].[CreatedAt], [t].[DateOfBirth], [t].[Email], [t].[FirstName], [t].[LastName], [t].[PhoneNumber], [t].[UpdatedAt], [a].[AccountId], [a].[AccountType], [a].[Balance], [a].[CreatedAt], [a].[Currency], [a].[CustomerId], [a].[UpdatedAt]
FROM (
    SELECT TOP(1) [c].[CustomerId], [c].[CreatedAt], [c].[DateOfBirth], [c].[Email], [c].[FirstName], [c].[LastName], [c].[PhoneNumber], [c].[UpdatedAt]
    FROM [Customer] AS [c]
    WHERE [c].[CustomerId] = @__customerId_0
) AS [t]
LEFT JOIN [Account] AS [a] ON [t].[CustomerId] = [a].[CustomerId]
ORDER BY [t].[CustomerId]',N'@__customerId_0 int',@__customerId_0=1
```
https://learn.microsoft.com/en-us/aspnet/core/tutorials/grpc/grpc-start?view=aspnetcore-8.0&tabs=visual-studio

gRPC vs REST vs OpenAPI
https://cloud.google.com/blog/products/api-management/understanding-grpc-openapi-and-rest-and-when-to-use-them

### Set up ELK
Set up a .NET API application that logs to Serilog, views logs in Kibana, and uses Grafana for monitoring on your local machine

#### Add Serilog to Your Project
```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.Http

```
#### Configure Serilog in your Program.cs
```csharp
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Http("http://localhost:8080") // Logstash endpoint
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
```


#### Set Up ELK

Option A: Clone

Clone elk from git
```bash
git clone https://github.com/deviantony/docker-elk.git
cd docker-elk

```

Add in default logstash configuration
```yml
http {
		port => 8080
	}
```

Docker start elk
```bash
docker compose up setup

docker compose up

# docker-compose up -d
```

Open kibana in browser 
http://localhost:5601

docker stop elk
```bash
docker-compose down -v
```

test logstash in postman
url: http://localhost:9200/_cat/indices?v
Auth basic
	username: elastic
	password: changeme

#### Other configurations
Set environment variable
```bash
$env:KIBANA_SYSTEM_PASSWORD="your_password"
```

User and pass default
username: elastic
password: changeme

### References

https://www.elastic.co/guide/en/logstash/current/docker.html

https://learn.microsoft.com/en-us/dotnet/architecture/cloud-native/logging-with-elastic-stack


### Troubleshoot

logstash.conf
	input {
		beats {
			port => 5044
		}

		tcp {
			port => 50000
		}

		http {
			port => 8080
		}
	}

	## Add your filters / logstash plugins configuration here

	output {
		elasticsearch {
			hosts => "elasticsearch:9200"
			user => "logstash_internal"
			password => "${LOGSTASH_INTERNAL_PASSWORD}"
			index => "loanapp-logs-%{+YYYY.MM.dd}"
		}
	}

search existing containers
	docker container ls

curl test command
	run command from container with 
		docker exec -it a36f9decf5c9 /bin/bash
		

run command:
	curl -X POST "http://localhost:8080" -H "Content-Type: application/json" -d '{"message": "test log"}' --user elastic:changeme
	curl -X POST "http://localhost:8080" -H "Content-Type: application/json" -d '{"message": "test log"}' -u logstash_internal:changeme
	curl -X POST "http://localhost:8080" -H "Content-Type: application/json" -d '{"message": "test log"}' -u test_log:test_logpassword
	curl -X POST "http://localhost:8080" -H "Content-Type: application/json" -d '{"message": "test log"}' --user test_log:test_logpassword

Index management => create index loanapp-logs
http://localhost:5601/app/discover#/?_g=(filters:!(),refreshInterval:(pause:!t,value:60000),time:(from:now-15m,to:now))&_a=(columns:!(),dataSource:(dataViewId:'83250184-2d73-4e8a-b4ea-15435f9396b3',type:dataView),filters:!(),interval:auto,query:(language:kuery,query:''),sort:!())

kibana test console
http://localhost:5601/app/dev_tools#/console?load_from=data:text/plain,AoeQygKgBA+gRgVwDYGsD8AHATgUwC54CeAUAN5QBEAlgHYAmOAHhVAFxTkUy0PNuVIA9gEMawjBgC0QgOYBnFgF8oishTEBbHBXYUAZoMEUANJTxU8SbbrjCsFVUA

