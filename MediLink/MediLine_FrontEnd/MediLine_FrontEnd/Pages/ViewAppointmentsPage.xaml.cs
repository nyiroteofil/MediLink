using System.Text.Json;
using MediLine_FrontEnd.Models;

namespace MediLine_FrontEnd.Pages
{
    public partial class ViewAppointmentsPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ViewAppointmentsPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
            });
        }

        private async void OnSearchClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserIDEntry.Text))
            {
                await DisplayAlert("Error", "Please enter an ID", "OK");
                return;
            }

            if (!int.TryParse(UserIDEntry.Text, out int id))
            {
                await DisplayAlert("Error", "ID must be a number", "OK");
                return;
            }

            try
            {
                string endpoint = PatientRadio.IsChecked
                    ? $"https://localhost:7056/api/Appointments/GetPatientsAppointments?id={id}"
                    : $"https://localhost:7056/api/Appointments/GetDoctorsAppointments?id={id}";

                var response = await _httpClient.GetAsync(endpoint);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var appointments = JsonSerializer.Deserialize<List<AppointmentRequestResponseDTO>>(json, _jsonOptions);
                    AppointmentsCollection.ItemsSource = appointments;
                }
                else
                {
                    await DisplayAlert("Error", "Failed to fetch appointments", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Connection failed: {ex.Message}", "OK");
            }
        }
    }
}