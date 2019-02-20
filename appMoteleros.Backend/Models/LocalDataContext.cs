namespace appMoteleros.Backend.Models
{
    using appMoteleros.Domain.Models;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<appMoteleros.Common.Models.Client> Clients { get; set; }
    }
}