using PRN221_Project_MedAppoint.Model;

namespace PRN221_Project_MedAppoint.Helpers
{
    public class UserWithSpecialtiesViewModel
    {
        public Users User { get; set; }
        public List<string> Specialties { get; set; }

        public static implicit operator UserWithSpecialtiesViewModel?(Users? v)
        {
            throw new NotImplementedException();
        }
    }
}
