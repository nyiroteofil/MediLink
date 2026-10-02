using MediLink_BackEnd.Models;
using MediLink_BackEnd.Data;
using MediLink_BackEnd.UtilityClasses;
using Microsoft.EntityFrameworkCore;


namespace MediLink_BackEnd.Services;

interface IDbSeederService
{
    Task SeedDefaultAdmin();
}

public class ConfigurationException : Exception
{
    public ConfigurationException(string message) : base(message)
    {}
}

public class DbSeederService : IDbSeederService
{
    private readonly IConfiguration _config;
    private readonly MediLinkContext _context;
    
    public DbSeederService(IConfiguration config, MediLinkContext context)
    {
        _config = config;
        _context = context;
    }

    public async Task SeedDefaultAdmin()
    {
        if (await _context.Administrators.AnyAsync()) {
            Console.WriteLine("Administrator already exists. Proceeding without modifying it.");
            return;
        }
        if (_config["DefaultPasswd"] == null) throw new ConfigurationException("DefaultPasswd is missing. Forgot to set variable?");
        
         

        Administrator defaultAdmin = new Administrator()
        {
            Username = "Default System Admin",
            Password =  PwdEncryptionHelper.GetHashedPassword(_config["DefaultPasswd"], out string salt),
            PasswordSalt = salt,
            Role = UserRole.Administrator,
            Status = UserStatus.Active,
            ContactInfo = new ContactInfo()
            {
                Email = "",
                PhoneNumber = ""
            },
            DataSheet = new MedicalStaffDataSheet() {
                FirstName = "Default",
                LastName = "Administrator",
                DateOfBirth = DateOnly.FromDateTime(DateTime.Now).ToString(), // This is definitely not the right type. EF supports date to sql date so should modify
                Sex = 'M',
                EmployeeID = "DefSysAdmin",
                Position = "System Generated Default Administrator Account"
            }
        };
        
        await _context.Administrators.AddAsync(defaultAdmin);
        await  _context.SaveChangesAsync();
    }
}