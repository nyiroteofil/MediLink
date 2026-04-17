using Syncfusion.Maui.Calendar;

namespace MediLine_FrontEnd.Pages
{
    public class ScheduleEvent
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string EventType { get; set; }
        public Color EventColor { get; set; }
    }

    public partial class UserSchedulePage : ContentPage
    {
        private readonly List<ScheduleEvent> _allEvents;

        public UserSchedulePage()
        {
            InitializeComponent();
            _allEvents = GetHardcodedEvents();
            ShowEventsForDate(DateTime.Today);
        }

        private List<ScheduleEvent> GetHardcodedEvents()
        {
            return new List<ScheduleEvent>
            {
                new ScheduleEvent
                {
                    Title = "Appointment — Dr. Tailor",
                    Description = "Lower back pain follow-up",
                    StartTime = DateTime.Today.AddHours(9),
                    EndTime = DateTime.Today.AddHours(10),
                    EventType = "📅 Appointment",
                    EventColor = Color.FromArgb("#2D4A7A")
                },
                new ScheduleEvent
                {
                    Title = "Appointment — Dr. Tailor",
                    Description = "Blood pressure check",
                    StartTime = DateTime.Today.AddDays(2).AddHours(14),
                    EndTime = DateTime.Today.AddDays(2).AddHours(15),
                    EventType = "📅 Appointment",
                    EventColor = Color.FromArgb("#2D4A7A")
                },
                new ScheduleEvent
                {
                    Title = "Medication Reminder",
                    Description = "Take Amoxicillin 500mg",
                    StartTime = DateTime.Today.AddHours(8),
                    EndTime = DateTime.Today.AddHours(8).AddMinutes(15),
                    EventType = "💊 Reminder",
                    EventColor = Color.FromArgb("#4A2D7A")
                }
            };
        }

        private void ShowEventsForDate(DateTime date)
        {
            SelectedDateLabel.Text = date.ToString("MMMM dd, yyyy");

            var dayEvents = _allEvents
                .Where(e => e.StartTime.Date == date.Date)
                .OrderBy(e => e.StartTime)
                .ToList();

            EventsCollection.ItemsSource = null;
            EventsCollection.ItemsSource = dayEvents;
        }

        private void OnDateSelected(object sender, CalendarSelectionChangedEventArgs e)
        {
            if (e.NewValue is DateTime selectedDate)
                ShowEventsForDate(selectedDate);
        }

        private async void OnAddEventClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Coming Soon",
                "Event creation will be available in a future update!", "OK");
        }

        private void ScheduleCalendar_Tapped(object sender, CalendarTappedEventArgs e)
        {

        }
    }
}