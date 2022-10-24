using MongoDB.Bson.Serialization.Attributes;

namespace AddressBookP
{
    public class Information
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.Int32)]
        public int UUID { get; set; }   
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FirmName { get; set; }
        public string PhoneNumber { get; set; } 
        public string Email { get; set; }
        public string Location { get; set; }


        
    }
}
