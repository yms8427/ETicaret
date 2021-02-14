namespace Yms.Common.Contracts
{
    public class Settings
    {
        public AuthenticationConfiguration Authentication { get; set; }
        public DatabaseConfiguration Database { get; set; }
        public MailConfiguration Mail { get; set; }

        public class AuthenticationConfiguration
        {
            public string Secret { get; set; }
        }

        public class DatabaseConfiguration
        {
            public string Default { get; set; }
        }

        public class MailConfiguration
        {
            public string Server { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
        }
    }
}