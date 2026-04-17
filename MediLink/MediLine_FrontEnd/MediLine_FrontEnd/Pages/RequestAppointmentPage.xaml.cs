using MediLine_FrontEnd.Models;
using System.Text;
using System.Text.Json;

namespace MediLine_FrontEnd.Pages
{
    public partial class RequestAppointmentPage : ContentPage
    {
        private readonly ApiHandler _apiHandler;
        private List<UserSummaryDTO> _doctors = new();
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public RequestAppointmentPage(ApiHandler apiHandler)
        {
            InitializeComponent();
            _apiHandler = apiHandler;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDoctors();
        }

        private async Task LoadDoctors()
        {
            var res = await _apiHandler.GetAsync("/Administrator/GetActiveDoctors");

            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                _doctors = JsonSerializer.Deserialize<List<UserSummaryDTO>>(json, _jsonOptions);
                DoctorPicker.ItemsSource = _doctors;
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            if (DoctorPicker.SelectedItem == null)
            {
                await DisplayAlert("Error", "Please select a doctor", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(ReasonEntry.Text))
            {
                await DisplayAlert("Error", "Please enter a reason", "OK");
                return;
            }

            var selectedDoctor = (UserSummaryDTO)DoctorPicker.SelectedItem;
            int patientID = await _apiHandler.GetUserID();

            var dto = new
            {
                PatientID = patientID,
                SpecialistDoctorID = selectedDoctor.ID,
                ReasonOfRequest = ReasonEntry.Text,
                Status = 0 // Pending
            };

            try
            {
                var res = await _apiHandler.PostAsync(
                    "/Appointments/RequestAppointment", dto);

                if (res.IsSuccessStatusCode)
                {
                    await DisplayAlert("Success", "Appointment request submitted!", "OK");
                    ReasonEntry.Text = string.Empty;
                    DoctorPicker.SelectedItem = null;
                }
                else
                {
                    string errorMessage = await res.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Status: {res.StatusCode}\n{errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Connection failed: {ex.Message}", "OK");
            }
        }
    }
}