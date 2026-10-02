using AshkanIndustrialOperations.Infrastructure;
using AshkanIndustrialOperations.Models;
using AshkanIndustrialOperations.Security;
namespace AshkanIndustrialOperations.Services;
public sealed class AuthenticationService
{
    private readonly IUserStore _users; private readonly IAuditStore _audit; private readonly PasswordHasher _hasher; private readonly SessionService _session;
    public AuthenticationService(IUserStore users, IAuditStore audit, PasswordHasher hasher, SessionService session){_users=users;_audit=audit;_hasher=hasher;_session=session;}
    public async Task EnsureSeedAsync()
    {
        if ((await _users.GetAllAsync()).Count != 0) return;
        var (hash,salt)=_hasher.Hash("1234");
        await _users.SaveAsync(new[]{new UserAccount{Username="admin",DisplayName="System Administrator",Role="Administrator",PasswordHash=hash,PasswordSalt=salt}});
    }
    public async Task<(bool Success,string Message)> SignInAsync(string username,string password)
    {
        await EnsureSeedAsync();
        var user=await _users.FindByUsernameAsync(username.Trim());
        if(user is null || !user.IsActive || !_hasher.Verify(password,user.PasswordHash,user.PasswordSalt))
        { await _audit.AppendAsync(new AuditRecord{Username=username.Trim(),Action="LOGIN_FAILED",Details="Invalid credentials"}); return (false,"Invalid username or password. Demo: admin / 1234"); }
        _session.SignIn(user);
        await _audit.AppendAsync(new AuditRecord{Username=user.Username,Action="LOGIN_SUCCESS",Details=$"Role: {user.Role}"});
        return (true,string.Empty);
    }
}
