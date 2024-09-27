#### Demonstration project for bank account management

### Project creation walkthrough

 1. Used AI to generate the database tables
 2. Created database project
 3. Created database table files and post deploy from generated code
 4. Used AI to generate the DAL classes
 5. Created Class library project for DAL
 6. Created DAL classes from generated code
 7. Used AI to generate BL classes
 8. Created Class library project for BL
 9. Created BL classes from generated code
10.  


#### Install entity framework core
change to DAL directory
```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```
#### Install protobuf and gRPC tools
```bash
Install-Package Google.Protobuf
Install-Package Grpc.Tools

```

#### Install entity framework support for migrations (optional)
```bash
Install-Package Microsoft.EntityFrameworkCore.Design
```