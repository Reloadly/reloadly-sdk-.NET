using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reloadly.Core.Dto.Response
{
    public class Page<T> where T : class
    {
        [JsonProperty("content")]
        public IList<T> Content { get; set; } = Array.Empty<T>();

        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("totalElements")]
        public long TotalElements { get; set; }

        [JsonProperty("pageable")]
        public object Pageable { get; set; } = new object();

        [JsonProperty("last")]
        public bool Last { get; set; }

        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("sort")]
        public object Sort { get; set; } = new object();

        [JsonProperty("first")]
        public bool First { get; set; }

        [JsonProperty("numberOfElements")]
        public int NumberOfElements { get; set; }
    }
}
