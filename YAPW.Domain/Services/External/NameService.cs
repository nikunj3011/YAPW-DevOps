using Microsoft.EntityFrameworkCore;
using YAPW.Domain.App_Base;
using YAPW.Domain.Interfaces.External;
using YAPW.MainDb;
using YAPW.Models;

namespace YAPW.Domain.Services.Internal
{
    public class NameService : INameService
    {
        private AppBase appBase;
        public NameService()
        {
            appBase = new AppBase();
        }

        public async Task<IEnumerable<NameDataModel>> GetAll(IHttpClientFactory httpClientFactory)
        {
            try
            {
                var peopleRequestResult = await appBase.GetListOfElementsFromApiAsync<NameDataModel>("person/api/v1/people", httpClientFactory, "javaApi");

                return peopleRequestResult.returnedElement;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
