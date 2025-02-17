Some considerations:

- The database was generated from the included sql script
- the entities and dbcontext were generated through efc scaffolding
- MVC was prefered over other tecnologies mostly due to lack of time and the developer being very familiar with this tecnology,
  but i could as well have developed it with a webapi / minimal api + react or blazor
- caching was implemented in memory, since this is a small database, but in a real situation other caching services would be prefered.
- there was not much time to test, so i hope the implementation of caching did not create any unexpected bug.
