using System.Collections.Generic;
using System.Threading.Tasks;
using EmailSender.WebApi.Options;

namespace EmailSender.WebApi.Services
{
    public interface IFormClientRepository
    {
        Task<FormClientMetadata> GetClientAsync(string clientId);
        Task<IList<FormClientMetadata>> GetClientsAsync();
    }
}