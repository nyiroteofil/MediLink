using System.Text;
using System.Text.Json;

namespace MediLine_FrontEnd.Pages
{
    public partial class RegisterPatientPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public RegisterPatientPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
            });
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
                string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
                string.IsNullOrWhiteSpace(DateOfBirthEntry.Text) ||
                string.IsNullOrWhiteSpace(SexEntry.Text) ||
                string.IsNullOrWhiteSpace(TAJNumberEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all required fields", "OK");
                return;
            }

            var dto = new
            {
                UserName = UsernameEntry.Text,
                PasswordHash = PasswordEntry.Text,
                UserStatus = 0, // Active
                LastName = LastNameEntry.Text,
                FirstName = FirstNameEntry.Text,
                DateOfBirth = DateOfBirthEntry.Text,
                Sex = SexEntry.Text[0],
                Address = AddressEntry.Text,
                TAJNumber = TAJNumberEntry.Text,
                Email = EmailEntry.Text,
                PhoneNumber = PhoneNumberEntry.Text
            };

            try
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(
                    $"{Environment.GetEnvironmentVariable("MEDILINK_URL")}/UserManager/AddPatientUser",
                    content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Patient registered successfully!", "OK");
                    ClearForm();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to register patient", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Connection failed: {ex.Message}", "OK");
            }
        }

        private void ClearForm()
        {
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            LastNameEntry.Text = string.Empty;
            FirstNameEntry.Text = string.Empty;
            DateOfBirthEntry.Text = string.Empty;
            SexEntry.Text = string.Empty;
            AddressEntry.Text = string.Empty;
            TAJNumberEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PhoneNumberEntry.Text = string.Empty;
        }
    }
}