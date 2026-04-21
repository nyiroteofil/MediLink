using System.Text.Json;

namespace MediLine_FrontEnd.Pages
{
    public partial class AppointmentRequestsPage : ContentPage
    {
        private readonly ApiHandler _apiHandler;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public AppointmentRequestsPage(ApiHandler apiHandler)
        {
            InitializeComponent();
            _apiHandler = apiHandler;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadRequests();
        }

        private async Task LoadRequests()
        {
            RequestsCollection.ItemsSource = null;

            int userID = await _apiHandler.GetUserID();
            var res = await _apiHandler.GetAsync(
                $"/Appointments/GetPendingAppointments?userID={userID}");


            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var requests = JsonSerializer.Deserialize<List<AppointmentRequestResponseDTO>>(
                    json, _jsonOptions);
                RequestsCollection.ItemsSource = requests;
            }
        }

        private async void OnViewRequestClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int requestID = (int)button.CommandParameter;
            await Shell.Current.GoToAsync($"appointmentRequestDetail?requestId={requestID}");
        }
    }
}