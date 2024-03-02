using OkVip.Gamble.IdentityUsers;

namespace OkVip.Gamble.Tokens
{
    public class TokenOutputDto
    {
        public string AccessToken { get; set; }

        public IdentityUserCustomDto Profile { get; set; }
    }
}