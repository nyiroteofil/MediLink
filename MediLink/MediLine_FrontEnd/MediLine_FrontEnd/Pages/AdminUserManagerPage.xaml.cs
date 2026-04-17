using MediLine_FrontEnd.Models;
using System.Text.Json;

namespace MediLine_FrontEnd.Pages
{
    public partial class AdminUserManagerPage : ContentPage
    {
        private readonly ApiHandler _apiHandler;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public AdminUserManagerPage(ApiHandler apiHandler)
        {
            InitializeComponent();
            _apiHandler = apiHandler;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadActiveUsers("Patient");
        }

        // Tab switching
        private async void OnAddUserTabClicked(object sender, EventArgs e)
        {
            AddUserTab.IsVisible = true;
            ActiveUsersTab.IsVisible = false;
            SuspendedUsersTab.IsVisible = false;
            SetTabHighlight(AddUserTabBtn);
        }

        private async void OnActiveUsersTabClicked(object sender, EventArgs e)
        {
            AddUserTab.IsVisible = false;
            ActiveUsersTab.IsVisible = true;
            SuspendedUsersTab.IsVisible = false;
            SetTabHighlight(ActiveUsersTabBtn);
            await LoadActiveUsers(GetActiveFilter());
        }

        private async void OnSuspendedUsersTabClicked(object sender, EventArgs e)
        {
            AddUserTab.IsVisible = false;
            ActiveUsersTab.IsVisible = false;
            SuspendedUsersTab.IsVisible = true;
            SetTabHighlight(SuspendedUsersTabBtn);
            await LoadSuspendedUsers(GetSuspendedFilter());
        }

        private void SetTabHighlight(Button activeBtn)
        {
            AddUserTabBtn.BackgroundColor = Color.FromArgb("#1E1E2E");
            AddUserTabBtn.TextColor = Color.FromArgb("#A0A0B0");
            ActiveUsersTabBtn.BackgroundColor = Color.FromArgb("#1E1E2E");
            ActiveUsersTabBtn.TextColor = Color.FromArgb("#A0A0B0");
            SuspendedUsersTabBtn.BackgroundColor = Color.FromArgb("#1E1E2E");
            SuspendedUsersTabBtn.TextColor = Color.FromArgb("#A0A0B0");

            activeBtn.BackgroundColor = Color.FromArgb("#6C63FF");
            activeBtn.TextColor = Colors.White;
        }

        // Role radio helpers
        private string GetActiveFilter()
        {
            if (ActiveDoctorRadio.IsChecked) return "Doctor";
            if (ActiveAssistantRadio.IsChecked) return "Assistant";
            if (ActiveAdminRadio.IsChecked) return "Admin";
            return "Patient";
        }

        private string GetSuspendedFilter()
        {
            if (SuspendedDoctorRadio.IsChecked) return "Doctor";
            if (SuspendedAssistantRadio.IsChecked) return "Assistant";
            if (SuspendedAdminRadio.IsChecked) return "Admin";
            return "Patient";
        }

        // Radio changed handlers
        private async void OnActiveFilterChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value) await LoadActiveUsers(GetActiveFilter());
        }

        private async void OnSuspendedFilterChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value) await LoadSuspendedUsers(GetSuspendedFilter());
        }

        // Role fields visibility
        private void OnRoleRadioChanged(object sender, CheckedChangedEventArgs e)
        {
            bool isPatient = PatientRadio.IsChecked;
            PatientFields.IsVisible = isPatient;
            StaffFields.IsVisible = !isPatient;
        }

        // Load users
        private async Task LoadActiveUsers(string role)
        {
            ActiveUsersCollection.ItemsSource = null;

            string endpoint = role switch
            {
                "Doctor" => "/Administrator/GetActiveDoctors",
                "Assistant" => "/Administrator/GetActiveAssistants",
                "Admin" => "/Administrator/GetActiveAdministrators",
                _ => "/Administrator/GetActivePatients"
            };

            var res = await _apiHandler.GetAsync(endpoint);
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<UserSummaryDTO>>(json, _jsonOptions);
                ActiveUsersCollection.ItemsSource = users;
            }
        }

        private async Task LoadSuspendedUsers(string role)
        {
            SuspendedUsersCollection.ItemsSource = null; // clear first!

            string endpoint = role switch
            {
                "Doctor" => "/Administrator/GetSuspendedDoctors",
                "Assistant" => "/Administrator/GetSuspendedAssistants",
                "Admin" => "/Administrator/GetSuspendedAdministrators",
                _ => "/Administrator/GetSuspendedPatients"
            };

            var res = await _apiHandler.GetAsync(endpoint);
            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var users = JsonSerializer.Deserialize<List<UserSummaryDTO>>(json, _jsonOptions);
                SuspendedUsersCollection.ItemsSource = users;
            }
        }

        // Create user
        private async void OnCreateUserClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UsernameEntry.Text) ||
                string.IsNullOrWhiteSpace(PasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
                string.IsNullOrWhiteSpace(LastNameEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all required fields", "OK");
                return;
            }

            string endpoint;
            object dto;

            if (PatientRadio.IsChecked)
            {
                endpoint = "/UserManager/AddPatientUser";
                dto = new
                {
                    UserName = UsernameEntry.Text,
                    PasswordHash = PasswordEntry.Text,
                    FirstName = FirstNameEntry.Text,
                    LastName = LastNameEntry.Text,
                    DateOfBirth = DateOfBirthEntry.Text,
                    Sex = SexEntry.Text?.FirstOrDefault(),
                    Address = AddressEntry.Text,
                    TAJNumber = TAJNumberEntry.Text,
                    Email = EmailEntry.Text,
                    PhoneNumber = PhoneEntry.Text
                };
            }
            else
            {
                endpoint = DoctorRadio.IsChecked
                    ? "/UserManager/AddSpecialistDoctorUser"
                    : AssistantRadio.IsChecked
                        ? "/UserManager/AddMedicalAssistantUser"
                        : "/UserManager/AddAdministratorUser";

                dto = new
                {
                    UserName = UsernameEntry.Text,
                    PasswordHash = PasswordEntry.Text,
                    FirstName = FirstNameEntry.Text,
                    LastName = LastNameEntry.Text,
                    DateOfBirth = DateOfBirthEntry.Text,
                    Sex = SexEntry.Text?.FirstOrDefault(),
                    Address = AddressEntry.Text,
                    EmployeeID = EmployeeIDEntry.Text,
                    Position = PositionEntry.Text,
                    InstitutionID = int.TryParse(InstitutionIDEntry.Text, out int instId) ? instId : 0,
                    Email = EmailEntry.Text,
                    PhoneNumber = PhoneEntry.Text
                };
            }

            var res = await _apiHandler.PostAsync(endpoint, dto);
            if (res.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "User created successfully!", "OK");
                ClearForm();
            }
            else
            {
                await DisplayAlert("Error", "Failed to create user", "OK");
            }
        }

        // Suspend/Reactivate
        private async void OnSuspendClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int userID = (int)button.CommandParameter;

            bool confirm = await DisplayAlert("Confirm", 
                "Are you sure you want to suspend this user?", "Yes", "No");
            if (!confirm) return;

            var res = await _apiHandler.PatchAsync($"/UserManager/SuspendUser?userID={userID}");
            if (res.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "User suspended!", "OK");
                await LoadActiveUsers(GetActiveFilter());
            }
        }

        private async void OnReactivateClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int userID = (int)button.CommandParameter;

            bool confirm = await DisplayAlert("Confirm",
                "Reactivate this user?", "Yes", "No");
            if (!confirm) return;

            var res = await _apiHandler.PatchAsync($"/UserManager/ActivateUser?userID={userID}");
            if (res.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "User reactivated!", "OK");
                await LoadSuspendedUsers(GetSuspendedFilter());
            }
        }

        private void ClearForm()
        {
            UsernameEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
            FirstNameEntry.Text = string.Empty;
            LastNameEntry.Text = string.Empty;
            DateOfBirthEntry.Text = string.Empty;
            SexEntry.Text = string.Empty;
            AddressEntry.Text = string.Empty;
            TAJNumberEntry.Text = string.Empty;
            EmployeeIDEntry.Text = string.Empty;
            PositionEntry.Text = string.Empty;
            InstitutionIDEntry.Text = string.Empty;
            EmailEntry.Text = string.Empty;
            PhoneEntry.Text = string.Empty;
        }
    }
}