using System.Text.Json;

namespace MediLine_FrontEnd.Pages
{
    [QueryProperty(nameof(RequestId), "requestId")]
    public partial class AppointmentRequestDetailPage : ContentPage
    {
        private readonly ApiHandler _apiHandler;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private AppointmentRequestResponseDTO _currentRequest;
        private int _requestId;

        public int RequestId
        {
            get => _requestId;
            set
            {
                _requestId = value;
                LoadRequest(value);
            }
        }

        public AppointmentRequestDetailPage(ApiHandler apiHandler)
        {
            InitializeComponent();
            _apiHandler = apiHandler;
        }

        private async void LoadRequest(int requestId)
        {
            var res = await _apiHandler.GetAsync(
                $"/Appointments/GetAppointmentRequestByID?requestId={requestId}");

            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                _currentRequest = JsonSerializer.Deserialize<AppointmentRequestResponseDTO>(
                    json, _jsonOptions);

                PatientNameLabel.Text = $"Patient: {_currentRequest.PatientName}";
                ReasonLabel.Text = _currentRequest.ReasonOfRequest;
            }
        }

        private async void OnAcceptClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm",
                "Accept this appointment request?", "Yes", "No");
            if (!confirm) return;

            var res = await _apiHandler.PatchAsync(
                $"/Appointments/AcceptAppointmentRequest?requestId={_requestId}");

            if (res.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Request accepted!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Error", "Failed to accept request", "OK");
            }
        }

        private async void OnDenyClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DenialReasonEntry.Text))
            {
                await DisplayAlert("Error", "Please enter a reason for denial", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Confirm",
                "Deny this appointment request?", "Yes", "No");
            if (!confirm) return;

            var res = await _apiHandler.PatchAsync(
                $"/Appointments/DenyAppointmentRequest?requestId={_requestId}&reason={DenialReasonEntry.Text}");

            if (res.IsSuccessStatusCode)
            {
                await DisplayAlert("Success", "Appointment accepted!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await DisplayAlert("Success", "Appointment accepted!", "OK");
            }
        }
    }
}