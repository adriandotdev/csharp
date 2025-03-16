public interface IUserRepository {
    
    public void CreateUser(User user);
    public User? GetUserByUsername(string? username);

    public dynamic GetUsers(int pageNumber = 1, int pageSize = 10);
}