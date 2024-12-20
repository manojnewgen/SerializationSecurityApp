using System.Text.Json;
using System.Security.Cryptography;
using System.Text;

public class Program{

    public class User{
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        
        public string GenerateHash(){
            using(SHA256 sha256= SHA256.Create()){
                Byte[] hashBytes= sha256.ComputeHash(Encoding.UTF8.GetBytes(ToString()));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public void EncryptData(){
            Password= Convert.ToBase64String(Encoding.UTF8.GetBytes(Password));

        }

        public static string SerializeUserData(User user){
            if(string.IsNullOrWhiteSpace(user.Name)
            ||string.IsNullOrWhiteSpace(user.Email)
            || string.IsNullOrWhiteSpace(user.Password)
            ){
                Console.WriteLine("Invalid Data, serialization process is aborted");
                return string.Empty;
            }
            user.EncryptData();
            return JsonSerializer.Serialize(user);
        }

        public static User DeserializeUserData(string jsonData, bool isTrustedSource){
            if(!isTrustedSource){
                Console.WriteLine("Blocked");
                return null;
            }
            return JsonSerializer.Deserialize<User>(jsonData);
        }
        
        public static void Main(){
            var user= new User{
                Name="Manoj",
                Email="kumar.manoj@thinksys.com",
                Password="My@1234"
            };
            string generatedHash= user.GenerateHash();
            string serilazedData= SerializeUserData(user);
            User deserializedData= DeserializeUserData(serilazedData, true);
            Console.WriteLine("Serialized Data:\n"+ generatedHash);
        }
        
        
        
    }
}

