using AutoMapper;
using FluentAssertions;
using ITG.Brix.EccSetup.Application.Bases;
using ITG.Brix.EccSetup.IntegrationTests.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.IntegrationTests.Controllers
{
    [TestClass]
    public class ConfigurationDataControllerTests
    {
        private readonly HttpClient _client;

        public ConfigurationDataControllerTests()
        {
#if DEBUG
            ServiceCollectionExtensions.UseStaticRegistration = false;
            _client = ControllerHelper.GetClient();
#endif
        }

        [TestMethod]
        public async Task GetShouldSucceed()
        {
#if DEBUG
            // Act
            var response = await _client.GetAsync("api/configurationdata");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultOld>(responseAsString);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
#endif
        }

        [TestMethod]
        public async Task PostCustomersShouldSucceed()
        {
#if DEBUG
            // Arrange
            var xml = @"
                <Customers>
                    <Customer>
                        <Name>A</Name>
                        <Description>B</Description>
                        <Source>C</Source>
                    </Customer>
                </Customers>";

            // Act
            var response = await _client.PostAsync("api/configurationdata", new StringContent(xml, Encoding.UTF8, "application/xml"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultOld>(responseAsString);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
#endif
        }

        [TestMethod]
        public async Task PostTransportTypesShouldSucceed()
        {
#if DEBUG
            // Arrange
            var xml = @"
                <TransportTypes>
                    <TransportType>
                        <Name>A</Name>
                        <Description>B</Description>
                        <Source>C</Source>
                    </TransportType>
                </TransportTypes>";

            // Act
            var response = await _client.PostAsync("api/configurationdata", new StringContent(xml, Encoding.UTF8, "application/xml"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultOld>(responseAsString);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
#endif
        }

        [TestMethod]
        public async Task PostProductionSitesShouldSucceed()
        {
#if DEBUG
            // Arrange
            var xml = @"
                <ProductionSites>
                    <ProductionSite>
                        <Name>A</Name>
                        <Description>B</Description>
                        <Source>C</Source>
                    </ProductionSite>
                </ProductionSites>";

            // Act
            var response = await _client.PostAsync("api/configurationdata", new StringContent(xml, Encoding.UTF8, "application/xml"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultOld>(responseAsString);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
#endif
        }

        [TestMethod]
        public async Task PostTypePlanningsShouldSucceed()
        {
#if DEBUG
            // Arrange
            var xml = @"
                <TypePlannings>
                    <TypePlanning>
                        <Name>A</Name>
                        <Description>B</Description>
                        <Source>C</Source>
                    </TypePlanning>
                </TypePlannings>";

            // Act
            var response = await _client.PostAsync("api/configurationdata", new StringContent(xml, Encoding.UTF8, "application/xml"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultOld>(responseAsString);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
#endif
        }

        [TestMethod]
        public async Task PostSitesShouldSucceed()
        {
#if DEBUG
            // Arrange
            var xml = @"
                <Sites>
                    <Site>
                        <Name>A</Name>
                        <Description>B</Description>
                        <Source>C</Source>
                        <OperationalDepartments>
                            <OperationalDepartment>
                                <Name>A</Name>
                                <Description>B</Description>
                            </OperationalDepartment>
                        </OperationalDepartments>
                    </Site>
                </Sites>";

            // Act
            var response = await _client.PostAsync("api/configurationdata", new StringContent(xml, Encoding.UTF8, "application/xml"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultOld>(responseAsString);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
#endif
        }

        [TestMethod]
        public async Task PostUnknownXmlShouldResultInError()
        {
#if DEBUG
            // Arrange
            var xml = "<ThisIsAnXmlNode />";

            // Act
            var response = await _client.PostAsync("api/configurationdata", new StringContent(xml, Encoding.UTF8, "application/xml"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultOld>(responseAsString);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
#endif
        }

        [TestMethod]
        public async Task PostInvalidXmlShouldResultInError()
        {
#if DEBUG
            // Arrange
            var xml = "This is not xml";

            // Act
            var response = await _client.PostAsync("api/configurationdata", new StringContent(xml, Encoding.UTF8, "application/xml"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResultOld>(responseAsString);

            // Assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
#endif
        }
    }
}
