using System.Net.Http.Headers;
using System.Text;
using eShop_DHA.Salesforce;
using eShop_DHA.Salesforce.Request;
using eShop_DHA.Salesforce.Response;
using eShop_DHA.Settings;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eShop_DHA.Controllers.Salesforces;

[ApiController]
[Route("sf/[Controller]")]
public class ProductController:ControllerBase
{
    private readonly IAuthenProvider _authenProvider;
    private readonly AuthenResponse _authenResponse;
    private SalesforceSetting _salesforceSetting = new SalesforceSetting();

    public ProductController(IConfiguration configuration, IAuthenProvider authenProvider)
    {
        _authenProvider = authenProvider;
        _authenResponse = _authenProvider.Authen().Result;
        configuration.GetSection(SalesforceSetting.Section).Bind(_salesforceSetting);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var query = @"/services/data/v54.0/query?q=select+name,id,price__c,categoryId__r.name+from+Product__c+limit+10";
        var products = new List<SfProductResponse>();
        using (var client = new HttpClient())
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(_salesforceSetting.Endpoint + query),
                Content = null
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authenResponse.AccessToken);
            var responseMessage = await new HttpClient().SendAsync(request).ConfigureAwait(false);
            var response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            products = JsonConvert.DeserializeObject<QueryResponse<SfProductResponse>>(response).Records.ToList();
            
        }

        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync(SfAddProductRequest request)
    {
        string createMessage = $"<root>" +
                               $"<Name>{request.Name}</Name>" +
                               $"<Price__c>{request.Price}</Price__c>" +
                               $"<CategoryId__c>{request.CategoryId}</CategoryId__c>" +
                               $"</root>";
        using (var client = new HttpClient())
        {
            string result = CreateRecord(client, createMessage, "Product__c");
            return Ok(result);
        }

       

    }
    
    private string CreateRecord(HttpClient client, string createMessage, string recordType)
    {
        HttpContent contentCreate = new StringContent(createMessage, Encoding.UTF8, "application/xml");
        string uri = _salesforceSetting.Endpoint+"/services/data/v54.0/sobjects/"+recordType;

        HttpRequestMessage requestCreate = new HttpRequestMessage(HttpMethod.Post, uri);
        requestCreate.Headers.Add("Authorization", "Bearer " + _authenResponse.AccessToken);
        requestCreate.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));
        requestCreate.Content = contentCreate;
        HttpResponseMessage response = client.SendAsync(requestCreate).Result;
        return response.Content.ReadAsStringAsync().Result;
    }
}