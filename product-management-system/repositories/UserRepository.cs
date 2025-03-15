public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository () : base() {

    }
    public User? GetUserByUsername(string? username)
    {
        var result =  this.productManagementContext.Users.Where(user => user.Username == username).ToArray();

        if (result == null || result.Length == 0) return null;
        
        return result[0];
    }
}