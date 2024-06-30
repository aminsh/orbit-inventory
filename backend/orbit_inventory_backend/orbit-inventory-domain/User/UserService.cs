namespace orbit_inventory_domain.user;

public class UserService
{
   public Task<User> Login()
   {
      return Task.FromResult(new User());
   }
}