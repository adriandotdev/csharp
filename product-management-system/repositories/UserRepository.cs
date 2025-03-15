
public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository () : base() {

    }

    public void CreateUser(User user)
    {
        if (user == null) throw new Exception("User must be provided");

        this.productManagementContext.Users.Add(user);

        this.productManagementContext.SaveChanges();
    }

    public User? GetUserByUsername(string? username)
    {
        var result =  this.productManagementContext.Users.Where(user => user.Username == username).ToArray();

        if (result == null || result.Length == 0) return null;
        
        return result[0];
    }

    public List<User> GetUsers()
    {
       return this.productManagementContext.Users
            .Select(u => new User() {Name = u.Name, Username = u.Username, DateCreated = u.DateCreated})
            .OrderBy(p => p.Id)
            .ToList();
    }
}