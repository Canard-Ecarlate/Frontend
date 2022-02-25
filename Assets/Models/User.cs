namespace Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }

        public void ChangeUser(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Email = user.Email;
            this.Token = user.Token;
            this.Error = user.Error;
        }
    }
}
