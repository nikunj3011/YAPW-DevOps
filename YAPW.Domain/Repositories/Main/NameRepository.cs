using System.Net.Http;
using YAPW.Domain.App_Base;
using YAPW.Domain.Interfaces;
using YAPW.Domain.Repositories.Generic;
using YAPW.Domain.Services.Generic;
using YAPW.MainDb;
using YAPW.Models;

namespace YAPW.Domain.Repositories.Main;

public class NameRepository : INameService
{
    private AppBase appBase;
    IHttpClientFactory _httpClientFactory;
    public NameRepository(IHttpClientFactory httpClientFactory)
    {
        appBase = new AppBase();
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<NameDataModel>> GetAll()
    {
        try
        {
            var peopleRequestResult = await appBase.GetListOfElementsFromApiAsync<NameDataModel>("person/api/v1/people", _httpClientFactory, "javaApi");

            return peopleRequestResult.returnedElement;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    #region Helpers

    #endregion Helpers
}