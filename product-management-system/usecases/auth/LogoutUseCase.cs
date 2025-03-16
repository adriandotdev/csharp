using System.Net;

public class LogoutUseCase {

    public void Run(ref User user) {
        
        user = null;
    }
}