Some considerations:

- The database was generated from the included sql script
- the entities and dbcontext were generated through efc scaffolding
- caching was implemented in memory, since this is a small database, but in a real situation other caching services would be prefered.
- run the sql to create the database and update the connectionstring in appsettings to match your database to test the app.
