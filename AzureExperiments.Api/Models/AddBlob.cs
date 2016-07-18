using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AzureExperiments.Api.Models
{
    public class AddBlob
    {
        public string Name { get; set; }
        public byte[] BlobObject { get; set; }
    }
}