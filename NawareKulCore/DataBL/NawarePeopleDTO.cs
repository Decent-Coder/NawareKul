namespace NawareKulCore.DataBL
{
    public class NawarePeopleDTO
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string STD { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public string PinCode { get; set; }
        public int Id { get; set; }
    }

    public class PeopleDTO
    {
        public string Name { get; set; }
        public IFormFile PhotoFile { get; set; } // For uploading the photo
        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Pincode { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public DateTime Birthdate { get; set; }
        public string BloodGroup { get; set; }
        public string Education { get; set; }
        public string Job { get; set; }
        public string NativePlace { get; set; }
        public string Gotra { get; set; }
        public string Kuldeveta { get; set; }


    }
}