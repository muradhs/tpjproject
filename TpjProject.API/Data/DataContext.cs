using Microsoft.EntityFrameworkCore;
using TpjProject.API.Models; 
namespace TpjProject.API.Data
{
    public class DataContext : DbContext 
    {
       public  DataContext (DbContextOptions<DataContext> options): base (options){ }
       public DbSet<Value> values {get ; set; }


    }
}