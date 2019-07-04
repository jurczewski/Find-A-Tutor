using System.Threading.Tasks;

namespace Find_A_Tutor.Infrastructure.Services
{
    public interface IDataInitializer
    {
        Task SeedAsync();
    }
}
