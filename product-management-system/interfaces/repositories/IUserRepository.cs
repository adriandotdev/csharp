public interface IUserRepository {

    public User? GetUserByUsername(string? username);
}