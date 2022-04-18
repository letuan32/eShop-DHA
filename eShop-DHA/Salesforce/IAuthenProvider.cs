namespace eShop_DHA.Salesforce;

public interface IAuthenProvider
{
    Task<AuthenResponse> Authen();
   
}