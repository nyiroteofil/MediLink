using System.Text;
using System.Text.Json;

namespace MediLine_FrontEnd.Pages
{
    public partial class RequestAppointmentPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public RequestAppointmentPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (m, c, ch, e) => true
            });
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PatientIDEntry.Text) ||
                string.IsNullOrWhiteSpace(DoctorIDEntry.Text) ||
                string.IsNullOrWhiteSpace(ReasonEntry.Text))
            {
                await DisplayAlert("Error", "Please fill in all required fields", "OK");
                return;
            }

            if (!int.TryParse(PatientIDEntry.Text, out int patientId) ||
                !int.TryParse(DoctorIDEntry.Text, out int doctorId))
            {
                await DisplayAlert("Error", "Patient ID and Doctor ID must be numbers", "OK");
                return;
            }

            var dto = new
            {
                PatientID = patientId,
                SpecialistDoctorID = doctorId,
                ReasonOfRequest = ReasonEntry.Text,
                Status = 0 // Pending
            };

            try
            {
                var json = JsonSerializer.Serialize(dto);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(
                    "https://localhost:7056/api/Appointments/RequestAppointment",
                    content);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Appointment request submitted!", "OK");
                    ClearForm();
                }
                else
                {
                    await DisplayAlert("Error", "Failed to submit request", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Connection failed: {ex.Message}", "OK");
            }
        }

        private void ClearForm()
        {
            PatientIDEntry.Text = string.Empty;
            DoctorIDEntry.Text = string.Empty;
            ReasonEntry.Text = string.Empty;
        }
    }
}