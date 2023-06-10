using OspriTest.Features;
using OspriTest.Interfaces;

namespace OspriTest.Configuration
{
    internal class Settings
    {
        public int id { get; set; }
        public DatabaseSettings DatabaseSettings { get; set; }
    }
}