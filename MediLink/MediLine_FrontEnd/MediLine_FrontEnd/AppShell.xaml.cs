using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using Font = Microsoft.Maui.Font;

namespace MediLine_FrontEnd
{
    public partial class AppShell : Shell
    {
        private readonly ApiHandler _apiHandler;

        public AppShell(ApiHandler apiHandler)
        {
            InitializeComponent();
            _apiHandler = apiHandler;
            //   var currentTheme = Application.Current!.RequestedTheme;
            //   ThemeSegmentedControl.SelectedIndex = currentTheme == AppTheme.Light ? 0 : 1;

            // Register detail page route
            Routing.RegisterRoute("appointmentRequestDetail",
                typeof(AppointmentRequestDetailPage));
            Routing.RegisterRoute("conversation",
                typeof(ConversationPage));
        }
        public static async Task DisplaySnackbarAsync(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            var snackbarOptions = new SnackbarOptions
            {
                BackgroundColor = Color.FromArgb("#FF3300"),
                TextColor = Colors.White,
                ActionButtonTextColor = Colors.Yellow,
                CornerRadius = new CornerRadius(0),
                Font = Font.SystemFontOfSize(18),
                ActionButtonFont = Font.SystemFontOfSize(14)
            };

            var snackbar = Snackbar.Make(message, visualOptions: snackbarOptions);

            await snackbar.Show(cancellationTokenSource.Token);
        }

        public static async Task DisplayToastAsync(string message)
        {
            // Toast is currently not working in MCT on Windows
            if (OperatingSystem.IsWindows())
                return;

            var toast = Toast.Make(message, textSize: 18);

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            await toast.Show(cts.Token);
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await _apiHandler.RemoveToken();
            await Shell.Current.GoToAsync("//Login");
        }

        public async void RefreshMenuVisibility()
        {
            string role = await _apiHandler.GetUserRole();

            // Hide everything first
            RegisterPatientContent.IsVisible = false;
            RequestAppointmentContent.IsVisible = false;
            ViewAppointmentsContent.IsVisible = false;
            AdminContent.IsVisible = false;
            ScheduleContent.IsVisible = false;

            switch (role)
            {
                case "Patient":
                    ScheduleContent.IsVisible = true;
                    RequestAppointmentContent.IsVisible = true;
                    ViewAppointmentsContent.IsVisible = true;
                    break;

                case "SpecialistDoctor":
                    ScheduleContent.IsVisible = true;
                    RegisterPatientContent.IsVisible = true;
                    ViewAppointmentsContent.IsVisible = true;
                    break;

                case "MedicalAssistant":
                    ScheduleContent.IsVisible = true;
                    RegisterPatientContent.IsVisible = true;
                    RequestAppointmentContent.IsVisible = true;
                    ViewAppointmentsContent.IsVisible = true;
                    break;

                case "Administrator":
                    ScheduleContent.IsVisible = true;
                    RegisterPatientContent.IsVisible = true;
                    RequestAppointmentContent.IsVisible = true;
                    ViewAppointmentsContent.IsVisible = true;
                    AdminContent.IsVisible = true;
                    break;
            }
        }
    }
}
