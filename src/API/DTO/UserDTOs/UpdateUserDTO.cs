using Domain.Entities.Users;

namespace API.DTO.UserDTOs
{
    public sealed class UpdateUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public UpdateUserDTO() { }

        public UpdateUserDTO(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            Phone = user.Phone;
        }
    }
}
