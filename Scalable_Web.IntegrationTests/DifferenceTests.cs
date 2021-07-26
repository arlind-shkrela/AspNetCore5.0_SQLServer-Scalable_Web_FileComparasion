using Newtonsoft.Json;
using Scalable_Web.DTO.Request;
using Scalable_Web.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Scalable_Web.IntegrationTests
{
    public class DifferenceTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public DifferenceTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact]
        public async Task TestDifferencesAsync()
        {
            // Arrange
            var request = "v1/diff/1";

            // Act
            var response = await Client.GetAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task TestPostLeftAsync()
        {
            // Arrange
            var request = new
            {
                Url = "v1/diff/3/left",
                Body = new
                {
                    Id=3,
                    Left = "SGVsbG8gdGhlcmU="
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task TestPostRightAsync()
        {
            // Arrange
            var request = new
            {
                Url = "v1/diff/3/right",
                Body = new
                {
                    Id = 3,
                    Left = "SGVsbG8gdGhlcmU="
                }
            };

            // Act
            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task TestDeleteDifferenceAsync()
        {
            // Arrange

            var postRequest = new
            {
                Url = "v1/diff/58/left",
                Body = new
                {
                    Id =58,
                    Left= "SGVsbG8gdGhlcmU="
                }
            };

            // Act
            var postResponse = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            var jsonFromPostResponse = await postResponse.Content.ReadAsStringAsync();

            var singleResponse = JsonConvert.DeserializeObject<DifferencePostLeft>(jsonFromPostResponse);

            var deleteResponse = await Client.DeleteAsync(string.Format("v1/diff/{0}",singleResponse.Id));

            // Assert
            postResponse.EnsureSuccessStatusCode();

            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}
