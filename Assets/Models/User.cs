namespace Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
        public string ContainerId { get; set; }

        public void ChangeUser(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Token = user.Token;
            Error = user.Error;
            ContainerId = user.ContainerId;
        }
    }
}
