using YAPW.Domain.App_Base;
using YAPW.Domain.Interfaces.External;
using YAPW.Models;

namespace YAPW.Domain.Repositories.External;

public class NameRepository : INameService
{
    private readonly INameService _service;

    public NameRepository()
    {
    }

    public async Task<IEnumerable<NameDataModel>> GetAll(IHttpClientFactory httpClientFactory)
    {
        try
        {
            return await _service.GetAll(httpClientFactory);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #region Helpers

    #endregion Helpers
}