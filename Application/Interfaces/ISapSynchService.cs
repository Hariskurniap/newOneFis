namespace Application.Interfaces;

public interface ISapSynchService
{
    Task<string> GetAccessTokenAsync();
    Task<string> GetListDOFromSapAsync(object listDoParameters, string accessToken);
    Task<string> GetDetailDOFromSapAsync(object detailDoParameters, string accessToken);
    Task<string> GetCustomerFromSapAsync(object customerParams, string accessToken);

}

