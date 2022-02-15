using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailSender.WebApi.Options;
using EmailSender.WebApi.Services;
using Microsoft.Extensions.Options;

namespace EmailSender.WebApi.Utils
{
    internal class FormClientRepository: IFormClientRepository
    {
        private readonly IOptionsSnapshot<List<FormClientMetadata>> _options;

        public FormClientRepository(IOptionsSnapshot<List<FormClientMetadata>> options)
        {
            _options = options;
        }

        public Task<FormClientMetadata> GetClientAsync(string clientId)
        {
            var result =
                _options.Value.FirstOrDefault(c => StringComparer.OrdinalIgnoreCase.Equals(clientId, c.ClientId));
            return Task.FromResult(result);
        }

        public Task<IList<FormClientMetadata>> GetClientsAsync()
        {
            var result = _options.Value.AsReadOnly();
            return Task.FromResult<IList<FormClientMetadata>>(result);
        }
    }
}