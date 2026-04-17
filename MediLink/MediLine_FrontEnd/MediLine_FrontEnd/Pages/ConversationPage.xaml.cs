using MediLine_FrontEnd.Models;
using System.Text.Json;

namespace MediLine_FrontEnd.Pages
{
    public class MessageViewModel
    {
        public int ID { get; set; }
        public int SenderID { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
        public bool IsSent { get; set; }
        public bool IsReceived => !IsSent;
    }

    [QueryProperty(nameof(ChatId), "chatId")]
    public partial class ConversationPage : ContentPage
    {
        private readonly ApiHandler _apiHandler;
        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private int _chatId;
        private int _userID;

        public int ChatId
        {
            get => _chatId;
            set
            {
                _chatId = value;
                LoadMessages();
            }
        }

        public ConversationPage(ApiHandler apiHandler)
        {
            InitializeComponent();
            _apiHandler = apiHandler;
        }

        private async void LoadMessages()
        {
            _userID = await _apiHandler.GetUserID();

            var res = await _apiHandler.GetAsync(
                $"/Message/GetMessagesFromChat?chatID={_chatId}");

            if (res.IsSuccessStatusCode)
            {
                var json = await res.Content.ReadAsStringAsync();
                var messages = JsonSerializer.Deserialize<List<MessageResponseDTO>>(
                    json, _jsonOptions);

                // Map to ViewModel with IsSent flag
                var viewModels = messages.Select(m => new MessageViewModel
                {
                    ID = m.ID,
                    SenderID = m.SenderID,
                    Content = m.Content,
                    SentAt = m.SentAt,
                    IsRead = m.IsRead,
                    IsSent = m.SenderID == _userID
                }).ToList();

                MessagesCollection.ItemsSource = viewModels;
            }
        }

        private async void OnSendClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(MessageEntry.Text)) return;

            var dto = new
            {
                ChatID = _chatId,
                SenderID = _userID,
                Content = MessageEntry.Text
            };

            var res = await _apiHandler.PostAsync("/Message/PostMessage", dto);

            if (res.IsSuccessStatusCode)
            {
                MessageEntry.Text = string.Empty;
                LoadMessages(); // reload messages
            }
            else
            {
                await DisplayAlert("Error", "Failed to send message", "OK");
            }
        }
    }
}