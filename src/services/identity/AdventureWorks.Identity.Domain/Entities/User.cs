namespace AdventureWorks.Identity.Domain.Entities;

public class User : IdentityUser<int>
{
    public User()
    {
    }

    public User(string? username,
                string? normalizedUsername,
                string? email,
                string? normalizedEmail,
                bool emailConfirmed) => (UserName,
                                         NormalizedUserName,
                                         Email,
                                         NormalizedEmail,
                                         EmailConfirmed) = (username,
                                                            normalizedUsername,
                                                            email,
                                                            normalizedEmail,
                                                            emailConfirmed);
}