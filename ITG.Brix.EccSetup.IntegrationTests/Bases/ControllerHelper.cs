using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.Customer.From;
using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using ITG.Brix.EccSetup.IntegrationTests.Extensions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Bases
{
    public static class ControllerHelper
    {
        private static HttpClient _client = ControllerTestsHelper.GetClient();

        public static async Task<CreatedRecordResult> CreateLocation(string source, string site, string warehouse, string gate, string row, string position, string type, string isRack)
        {
            var apiVersion = "1.0";
            var body = new CreateLocationFromBody
            {
                Source = source,
                Site = site,
                Warehouse = warehouse,
                Gate = gate,
                Row = row,
                Position = position,
                Type = type,
                IsRack = isRack
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/locations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateOperationalDepartment(string code, string name, string site, string source)
        {
            var apiVersion = "1.0";
            var body = new CreateOperationalDepartmentFromBody
            {
                Site = site,
                Source = source,
                Code = code,
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/operationaldepartments?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateProductionSite(string code, string name, string source)
        {
            var apiVersion = "1.0";
            var body = new CreateProductionSiteFromBody
            {
                Source = source,
                Code = code,
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/productionsites?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateTypePlanning(string code, string name, string source)
        {
            var apiVersion = "1.0";
            var body = new CreateTypePlanningFromBody
            {
                Source = source,
                Code = code,
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/typeplannings?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateTransportType(string code, string name, string source)
        {
            var apiVersion = "1.0";
            var body = new CreateTransportTypeFromBody
            {
                Source = source,
                Code = code,
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/transporttypes?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateCustomer(string code, string name, string source)
        {
            var apiVersion = "1.0";
            var body = new CreateCustomerFromBody
            {
                Source = source,
                Code = code,
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/customers?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateSite(string code, string name, string source)
        {
            var apiVersion = "1.0";
            var body = new CreateSiteFromBody
            {
                Source = source,
                Code = code,
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/sites?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateStorageStatus(string code, string name, string @default, string source)
        {
            var apiVersion = "1.0";
            var body = new CreateStorageStatusFromBody
            {
                Default = @default,
                Source = source,
                Code = code,
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/storagestatuses?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateDamageCode(string code, string name, string damagedQuantityRequired, string source)
        {
            var apiVersion = "1.0";
            var body = new CreateDamageCodeFromBody
            {
                DamagedQuantityRequired = damagedQuantityRequired,
                Source = source,
                Code = code,
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/damagecodes?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateBusinessUnit(string name)
        {
            var apiVersion = "1.0";
            var body = new CreateBusinessUnitFromBody
            {
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/businessunits?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateFlow(string name, string description, string image)
        {
            var apiVersion = "1.0";
            var body = new CreateFlowFromBody
            {
                Name = name,
                Description = description,
                Image = image
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/flows?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task CreateOperatorActivity()
        {
            var apiVersion = "1.0";
            var json = "{\r\n\t\"Activities\": [\r\n\t\t{\r\n\t\t\"Type\": \"Validation\",\r\n\t\t\"Step\": \"Instruction\",\r\n\t\t\"Name\":\"Test\",\r\n\t\t\"Created\": \"6\\/19\\/2015 10:35:50 AM\",\r\n\t\t\"OperatorName\": \"eOrder\",\r\n\t\t\"OperatorId\": \"a9e5613c-95e8-41a0-a046-ca39b887adbe\",\r\n\t\t\"WorkOrderId\": \"f80d2d56-d130-40b3-bad4-2855960209d6\"\r\n\t\t}\r\n\t]\r\n}";

            var httpContent = new StringContent(json.ToString());
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync($"api/operatorActivities?api-version={apiVersion}", httpContent);
        }

        public static async Task<CreatedRecordResult> CreateIcon(string name, string dataPath)
        {
            var apiVersion = "1.0";
            var body = new CreateIconFromBody
            {
                Name = name,
                DataPath = dataPath
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/icons?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateLayout(string name, string description, string image)
        {
            var apiVersion = "1.0";
            var body = new CreateLayoutFromBody
            {
                Name = name,
                Description = description,
                Image = image
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/layouts?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateSource(string name, string description)
        {
            var apiVersion = "1.0";
            var body = new CreateSourceFromBody
            {
                Name = name,
                Description = description,
                BusinessUnits = null
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/sources?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task<CreatedRecordResult> CreateOperation(string name, string description)
        {
            var apiVersion = "1.0";
            var body = new CreateOperationFromBody
            {
                Name = name,
                Description = description,
                Tags = null
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync(string.Format("api/operations?api-version={0}", apiVersion), new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedRecordResult { Id = id, ETag = eTag };

            return result;
        }
    }
}
