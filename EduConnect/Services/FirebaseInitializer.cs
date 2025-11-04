using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace EduConnect.Services
{
    public class FirebaseInitializer
    {
        private static bool _initialized = false;

        public static void Initialize()
        {
            if (_initialized) return;

            var projectId = Environment.GetEnvironmentVariable("FIREBASE_PROJECT_ID");
            var clientEmail = Environment.GetEnvironmentVariable("FIREBASE_CLIENT_EMAIL");
            var privateKey = Environment.GetEnvironmentVariable("FIREBASE_PRIVATE_KEY");

            if (string.IsNullOrEmpty(projectId) || string.IsNullOrEmpty(clientEmail) || string.IsNullOrEmpty(privateKey))
                throw new Exception("❌ Firebase environment variables are missing!");

            // Thay thế \n bằng xuống dòng thật nếu bị escape
            privateKey = privateKey.Replace("\\n", "\n");

            var credential = GoogleCredential.FromJson($@"
            {{
              ""type"": ""service_account"",
              ""project_id"": ""{projectId}"",
              ""private_key_id"": ""dummy"",
              ""private_key"": ""{privateKey}"",
              ""client_email"": ""{clientEmail}"",
              ""client_id"": ""dummy"",
              ""token_uri"": ""https://oauth2.googleapis.com/token"",
              ""auth_uri"": ""https://accounts.google.com/o/oauth2/auth"",
              ""auth_provider_x509_cert_url"": ""https://www.googleapis.com/oauth2/v1/certs"",
              ""client_x509_cert_url"": ""https://www.googleapis.com/robot/v1/metadata/x509/{clientEmail.Replace("@", "%40")}""
            }}");

            FirebaseApp.Create(new AppOptions()
            {
                Credential = credential,
                ProjectId = projectId
            });

            _initialized = true;
        }
    }
}
