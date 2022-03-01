namespace Models
{
    public class TokenAndCurrentContainerIdDto
    {
        public string Token { get; set; }

        public string ContainerId { get; set; }

        public TokenAndCurrentContainerIdDto(string token, string containerId)
        {
            Token = token;
            ContainerId = containerId;
        }
    }
}
