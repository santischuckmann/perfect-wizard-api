namespace perfect_wizard.Application.DTOs
{
    public class UserDtoCreation
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserTokenDto
    {
        public string Token { get; set; }
    }
    public class LoginUserDto
    {
        public string Identifier { get; set; }
        public string Password { get; set; }
    }
    public class UserDto
    {
        public string Username { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}
