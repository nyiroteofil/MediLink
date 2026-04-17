using MediLine_FrontEnd.Models;
using System.Text.Json;

namespace MediLine_FrontEnd.Pages
{
    public partial class ChatsListPage : ContentPage
    {
        private readonly ApiHandler _apiHandler;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ChatsListPage(ApiHandler apiHandler)
        {
            InitializeComponent();
            _apiHandler = apiHandler;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadChats();
        }

        private async Task LoadChats()
        {
            int userID = await _apiHandler.GetUserID();
            var res = await _apiHandler.GetAsync($"/Message/GetUserChats?userID={userID}");

            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var chats = JsonSerializer.Deserialize<List<ChatSummaryDTO>>(json, _jsonOptions);

                // Set contact name based on who the current user is
                foreach (var chat in chats)
                {
                    chat.ContactName = chat.DoctorID == userID
                        ? chat.PatientName
                        : chat.DoctorName;

                    // temporary debug:
                    await DisplayAlert("Debug", $"ContactName: {chat.ContactName}, DoctorID: {chat.DoctorID}, UserID: {userID}", "OK");
                }

                ChatsCollection.ItemsSource = chats;
            }
        }

        private async void OnChatTapped(object sender, TappedEventArgs e)
        {
            int chatID = (int)e.Parameter;
            await Shell.Current.GoToAsync($"conversation?chatId={chatID}");
        }
    }
}