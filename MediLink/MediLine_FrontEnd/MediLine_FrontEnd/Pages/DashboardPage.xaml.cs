namespace MediLine_FrontEnd.Pages
{
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterPatientClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//register");

        private async void OnRequestAppointmentClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//appointment");

        private async void OnViewAppointmentsClicked(object sender, EventArgs e)
            => await Shell.Current.GoToAsync("//appointments");
    }
}