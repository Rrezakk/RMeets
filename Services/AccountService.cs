using RMeets.Models;
using RMeets.Repositories;
using System.Text;
using System.Security.Cryptography;
namespace RMeets;
public class AccountService
{
    private UserRepository _userRepository;
    private ProfileRepository _profileRepository;
    private BlankRepository _blankRepository;
    public AccountService(UserRepository userRepository, ProfileRepository profileRepository, BlankRepository blankRepository)
    {
        _userRepository = userRepository;
        _profileRepository = profileRepository;
        _blankRepository = blankRepository;
    }
    public bool CreateUser(string login,string password)
    {
        var pwh = AccountService.GetStringSha256Hash(password);
        var user = new User() { Login = login, PasswordHash = pwh };
        var u = _userRepository.GetByLogin(login);
        if (u != null) return false;
        _userRepository.AddUser(user);
        return true;
    }
    public bool ValidateUser(string login, string password)
    {
        var pwh = AccountService.GetStringSha256Hash(password);
        var user = _userRepository.GetByLogin(login);
        return user != null&&user.PasswordHash==pwh;
    }
    public static string GetStringSha256Hash(string text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;
        using var sha = new SHA256Managed();
        var textData = Encoding.UTF8.GetBytes(text);
        var hash = sha.ComputeHash(textData);
        return BitConverter.ToString(hash).Replace("-", string.Empty);
    }
    public Profile CreateProfile(Profile p)
    {
        return _profileRepository.Add(p);
    }
    public void EditProfile(Profile profile)
    {
        _profileRepository.Edit(profile);
    }
    public Profile? GetProfileById(int id) => _profileRepository.FindById(id);
}
