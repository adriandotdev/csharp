public interface IUserRepository {
    
    public void CreateUser(User user);
    public User? GetUserByUsername(string? username);

    public List<User> GetUsers();
}