# scaffolding db:

dotnet ef dbcontext scaffold "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ReleyeDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;" Microsoft.EntityFrameworkCore.SqlServer --context-dir . --output-dir DbModels --data-annotations --no-onconfiguring --context ReleyeDbContext
