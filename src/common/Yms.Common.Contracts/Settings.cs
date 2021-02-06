namespace Yms.Common.Contracts
{
    public class Settings
    {
        public AuthenticationConfiguration Authentication { get; set; }
        public DatabaseConfiguration Database { get; set; }

        public class AuthenticationConfiguration
        {
            public string Secret { get; set; }
        }

        public class DatabaseConfiguration
        {
            public string Default { get; set; }
        }
    }
}