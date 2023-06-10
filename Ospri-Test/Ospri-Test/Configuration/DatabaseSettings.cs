using Microsoft.Extensions.Hosting;
using OspriTest.Interfaces;
using OspriTest.Models;
using System.Xml.Linq;

namespace OspriTest.Configuration
{
    internal class DatabaseSettings
    {
        public bool UseInMemory { get; set; }
        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string CustomParts { get; set; }

        public string BuildConnectionString() => $"Server={Server};Database={DatabaseName};User Id={UserName};Password={Password};{CustomParts}";
    }
}