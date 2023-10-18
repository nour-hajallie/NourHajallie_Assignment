using log4net;
using log4net.Config;
using Newtonsoft.Json;
using NourHajallie_AutomationProject.DataEntities;
using RestSharp;

namespace NourHajallie_AutomationProject.BackendAPI
{
    public class ApiFunctions
    {
        private RestClient client;

        // Create a logger instance for the test class
        ILog log = LogManager.GetLogger(typeof(ApiFunctions));

        public ApiFunctions()
        {
            client = new RestClient("https://petstore.swagger.io/");

            // Load the Log4Net configuration file
            log4net.Util.LogLog.InternalDebugging = true;
            XmlConfigurator.Configure(new FileInfo("../../../Config/log4net.config"));
        }

        public ApiResponse PostListOfUsers(List<UserResponse> users)
        {
            // Create a request to the specified endpoint using the HTTP POST method
            var request = new RestRequest("/v2/user/createWithList", Method.Post);
            request.RequestFormat = DataFormat.Json;

            // Serialize the list of users to JSON format
            var requestBody = JsonConvert.SerializeObject(users);

            // Add the JSON request body to the HTTP request
            request.AddParameter("application/json", requestBody, ParameterType.RequestBody);

            // Execute the HTTP request and get the response
            var response = client.Execute(request);

            // Check if the response status code is OK (200)
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Deserialize the response content into an ApiResponse object
                var mappedResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return mappedResponse;
            }
            else
            {
                // If the response status code is not OK, throw an error message in the logs
                log.Error("List of users not added successfully");
                return null;
            }
        }

        public PetResponse GetPetById(string petId)
        {
            // Create a request to the specified endpoint using the HTTP GET method
            var request = new RestRequest("/v2/pet/{petId}", Method.Get);
            log.Info("Running Get /v2/pet/{petId} to Get Pet data based on Pet Id.");

            // Replace {id} with the actual pet ID
            request.AddUrlSegment("petId", petId);

            // Execute the HTTP request and get the response
            var response = client.Execute(request);

            // Check if the response status code is OK (200)
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Deserialize the response content into an PetResponse object
                var mappedResponse = JsonConvert.DeserializeObject<PetResponse>(response.Content);
                return mappedResponse;
            }
            else
            {
                // If the response status code is not OK, throw an error message in the logs
                log.Error("Pet with id " + petId + " is not found");
                return null;
            }
        }

        public ApiResponse DeletePetByPetId(string deletePetId, string api_key)
        {

            // Define the request endpoint and method
            var request = new RestRequest("/v2/pet/{id}", Method.Delete);
            log.Info("Running Get /v2/pet/{id} request to Delete a Pet.");

            // Replace {id} with the actual pet ID
            request.AddUrlSegment("id", deletePetId);

            // Add your API key as query parameters
            request.AddParameter("key", api_key);

            // Execute the HTTP request and get the response
            var response = client.Execute(request);

            // Check if the response status code is OK (200)
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                // Deserialize the response content into an ApiResponse object
                var mappedResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);
                return mappedResponse;
            }
            else
            {
                // If the response status code is not OK, throw an error message in the logs
                log.Error("Pet with id " + deletePetId + " is already deleted or not available");
                return null;
            }
        }
    }
}