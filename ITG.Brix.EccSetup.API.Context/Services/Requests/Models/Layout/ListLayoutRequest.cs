using ITG.Brix.EccSetup.API.Context.Services.Requests.Models.From;
using System;
using System.Collections.Generic;

namespace ITG.Brix.EccSetup.API.Context.Services.Requests.Models
{
    public class ListLayoutRequest
    {
        private readonly ListLayoutFromQuery _query;

        public ListLayoutRequest(ListLayoutFromQuery query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string Filter => _query.Filter;

        public string Top => _query.Top;

        public string Skip => _query.Skip;

        public void UpdateFilter(IDictionary<string, string> fromToSet)
        {
            if (!string.IsNullOrWhiteSpace(_query.Filter))
            {
                foreach (var fromTo in fromToSet)
                {
                    _query.Filter = _query.Filter.Replace(fromTo.Key, fromTo.Value);
                }
            }
            else
            {
                _query.Filter = null;
            }
        }
    }
}
