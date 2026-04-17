using System.Text.Json;
using MediLine_FrontEnd.Models;

namespace MediLine_FrontEnd.Pages
{
    public partial class ViewAppointmentsPage : ContentPage
    {
        private readonly ApiHandler _apiHandler;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ViewAppointmentsPage(ApiHandler apiHandler)
        {
            InitializeComponent();
            _apiHandler = apiHandler;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadAppointments();
        }

        private async Task LoadAppointments()
        {
            AppointmentsCollection.ItemsSource = null;

            int userID = await _apiHandler.GetUserID();
            string role = await _apiHandler.GetUserRole();

            string endpoint = role == "SpecialistDoctor"
                ? $"/Appointments/GetDoctorsAppointments?id={userID}"
                : $"/Appointments/GetPatientsAppointments?id={userID}";

            var response = await _apiHandler.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var appointments = JsonSerializer.Deserialize<List<AppointmentRequestResponseDTO>>(
                    json, _jsonOptions);
                AppointmentsCollection.ItemsSource = appointments;
            }
            else
            {
                await DisplayAlert("Error", "Failed to fetch appointments", "OK");
            }
        }
    }
}