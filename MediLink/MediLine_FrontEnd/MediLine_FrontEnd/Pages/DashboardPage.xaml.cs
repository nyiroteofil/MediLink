using System.Text.Json;

namespace MediLine_FrontEnd.Pages
{
    public partial class DashboardPage : ContentPage
    {
        private readonly ApiHandler _apiHandler;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public DashboardPage(ApiHandler apiHandler)
        {
            InitializeComponent();
            _apiHandler = apiHandler;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadDashboard();
        }

        private async Task LoadDashboard()
        {
            string role = await _apiHandler.GetUserRole();
            int userID = await _apiHandler.GetUserID();

            RoleLabel.Text = role;

            if (role == "SpecialistDoctor")
            {
                DoctorWidgets.IsVisible = true;
                await LoadDoctorWidgets(userID);
            }
            else if (role == "Patient")
            {
                PatientWidgets.IsVisible = true;
                NextAppointmentBanner.IsVisible = true;
                await LoadPatientWidgets(userID);
            }

            await LoadTodaysEvents(userID);
        }

        private async Task LoadDoctorWidgets(int docID)
        {
            // Hardcoded for demo
            PendingRequestsLabel.Text = "3";
            WeekAppointmentsLabel.Text = "8";
            ActivePatientsLabel.Text = "24";
        }

        /*
        private async Task LoadDoctorWidgets(int docID)
        {

            string username = await _apiHandler.GetUsername();
            string greeting = DateTime.Now.Hour switch
            {
                < 12 => "Good morning",
                < 18 => "Good afternoon",
                _ => "Good evening"
            };

            GreetingLabel.Text = $"{greeting}, {username}! 👋";

            // Pending requests
            var requestsRes = await _apiHandler.GetAsync(
                $"/Info/GetPendingRequestsCount?docID={docID}");
            if (requestsRes.IsSuccessStatusCode)
            {
                var count = JsonSerializer.Deserialize<int>(
                    await requestsRes.Content.ReadAsStringAsync(), _jsonOptions);
                PendingRequestsLabel.Text = count.ToString();
            }

            // This week appointments
            var weekRes = await _apiHandler.GetAsync(
                $"/Info/DoctorsAppointmentsWeekAhead?docID={docID}");
            if (weekRes.IsSuccessStatusCode)
            {
                var count = JsonSerializer.Deserialize<int>(
                    await weekRes.Content.ReadAsStringAsync(), _jsonOptions);
                WeekAppointmentsLabel.Text = count.ToString();
            }

            // Active patients
            var patientsRes = await _apiHandler.GetAsync(
                $"/Info/DocActivePatientNumber?docID={docID}");
            if (patientsRes.IsSuccessStatusCode)
            {
                var count = JsonSerializer.Deserialize<int>(
                    await patientsRes.Content.ReadAsStringAsync(), _jsonOptions);
                ActivePatientsLabel.Text = count.ToString();
            }
        }

        */

        private async Task LoadPatientWidgets(int patientID)
        {
            // Next appointment
            var nextRes = await _apiHandler.GetAsync(
                $"/Info/GetNextAppointmentDate?userID={patientID}");
            if (nextRes.IsSuccessStatusCode)
            {
                var date = JsonSerializer.Deserialize<DateTime?>(
                    await nextRes.Content.ReadAsStringAsync(), _jsonOptions);
                if (date != null)
                {
                    NextAppointmentLabel.Text = date.Value.ToString("MMM dd, HH:mm");
                    NextAppointmentBannerLabel.Text = date.Value.ToString("MMM dd, HH:mm");
                }
            }
        }

        /*
        private async Task LoadTodaysEvents(int userID)
        {
            // TODO: implement when today's events endpoint is ready
        }
        */

        private async Task LoadTodaysEvents(int userID)
        {
            var todayEvents = new List<object>
    {
        new {
            StartTime = DateTime.Today.AddHours(9),
            Description = "Páciens Példa — Lower back pain"
        },
        new {
            StartTime = DateTime.Today.AddHours(11),
            Description = "János Kovács — Blood pressure check"
        },
        new {
            StartTime = DateTime.Today.AddHours(14),
            Description = "Mária Nagy — Follow-up consultation"
        }
    };

            TodaysEventsCollection.ItemsSource = todayEvents;
        }
    }
}