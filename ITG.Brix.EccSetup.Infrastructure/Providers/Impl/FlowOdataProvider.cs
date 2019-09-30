using ITG.Brix.EccSetup.Domain;
using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Internal;
using ITG.Brix.EccSetup.Infrastructure.Providers.Bases.Impl;
using ITG.Brix.EccSetup.Infrastructure.Providers.Bases.Impl.Dtos;
using MongoDB.Driver;
using StringToExpression.Exceptions;
using StringToExpression.LanguageDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static ITG.Brix.EccSetup.Infrastructure.Providers.Impl.Constants.Consts;

namespace ITG.Brix.EccSetup.Infrastructure.Providers.Impl
{
    public class FlowOdataProvider : BaseOdataProvider<Flow>, IFlowOdataProvider
    {
        public FilterDefinition<Flow> GetFilterDefinition(string filter)
        {
            var filterDefinition = Builders<Flow>.Filter.Empty;

            try
            {
                if (!string.IsNullOrWhiteSpace(filter))
                {
                    var matches = filter.Split(new string[] { " and " }, StringSplitOptions.RemoveEmptyEntries);

                    if (matches.Count() == 9)
                    {
                        var properties = GetFilterProperties(matches);
                        var source = (from p in properties
                                      where p.Name == FlowFilterProperties.Source
                                      select p.Value).SingleOrDefault();
                        var operation = (from p in properties
                                         where p.Name == FlowFilterProperties.Operation
                                         select p.Value).SingleOrDefault();
                        var site = (from p in properties
                                    where p.Name == FlowFilterProperties.Site
                                    select p.Value).SingleOrDefault();
                        var operationalDepartment = (from p in properties
                                                     where p.Name == FlowFilterProperties.OperationalDepartment
                                                     select p.Value).SingleOrDefault();
                        var typePlanning = (from p in properties
                                            where p.Name == FlowFilterProperties.TypePlanning
                                            select p.Value).SingleOrDefault();
                        var customer = (from p in properties
                                        where p.Name == FlowFilterProperties.Customer
                                        select p.Value).SingleOrDefault();
                        var productionSite = (from p in properties
                                              where p.Name == FlowFilterProperties.ProductionSite
                                              select p.Value).SingleOrDefault();
                        var transportType = (from p in properties
                                             where p.Name == FlowFilterProperties.TransportType
                                             select p.Value).SingleOrDefault();
                        var driverWait = (from p in properties
                                          where p.Name == FlowFilterProperties.DriverWait
                                          select p.Value).SingleOrDefault();

                        filterDefinition = Builders<Flow>.Filter.Where(x => (x.Filter.Sources.Contains(new FlowSource(source)) || x.Filter.Sources.Contains(new FlowSource("x"))) &&
                                                                            (x.Filter.Operations.Contains(new FlowOperation(operation)) || x.Filter.Operations.Contains(new FlowOperation("x"))) &&
                                                                            (x.Filter.Sites.Contains(new FlowSite(site)) || x.Filter.Sites.Contains(new FlowSite("x"))) &&
                                                                            (x.Filter.OperationalDepartments.Contains(new FlowOperationalDepartment(operationalDepartment)) || x.Filter.OperationalDepartments.Contains(new FlowOperationalDepartment("x"))) &&
                                                                            (x.Filter.TypePlannings.Contains(new FlowTypePlanning(typePlanning)) || x.Filter.TypePlannings.Contains(new FlowTypePlanning("x"))) &&
                                                                            (x.Filter.Customers.Contains(new FlowCustomer(customer)) || x.Filter.Customers.Contains(new FlowCustomer("x"))) &&
                                                                            (x.Filter.ProductionSites.Contains(new FlowProductionSite(productionSite)) || x.Filter.ProductionSites.Contains(new FlowProductionSite("x"))) &&
                                                                            (x.Filter.TransportTypes.Contains(new FlowTransportType(transportType)) || x.Filter.TransportTypes.Contains(new FlowTransportType("x"))) &&
                                                                            (x.Filter.DriverWait == driverWait || x.Filter.DriverWait == "x"));
                    }
                    else
                    {
                        try
                        {
                            var language = new ODataFilterLanguage();
                            var predicate = language.Parse<Flow>(filter);
                            filterDefinition = predicate;
                        }
                        catch (OperationInvalidException ex)
                        {
                            throw new FilterODataException(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FilterODataException(ex.Message);
            }

            return filterDefinition;
        }

        public List<PropertyValue> GetFilterProperties(string[] matches)
        {
            var valueRegex = new Regex(@"(?<=\')(.*?)(?=\')");
            var properties = new List<PropertyValue>();

            foreach (var match in matches)
            {
                var filterMatches = match.Split(null);

                if (filterMatches.Count() < 3)
                {
                    throw new Exception("Invalid filter format");
                }
                properties.Add(new PropertyValue
                {
                    Name = filterMatches.First(),
                    Value = valueRegex.Match(match).Value
                });
            }
            return properties;
        }
    }
}
