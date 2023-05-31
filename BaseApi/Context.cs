using BaseApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseApi;

public class Context : DbContext
{
	public DbSet<Account> Accounts { get; set; }

	public Context(DbContextOptions options) : base(options) { }
}