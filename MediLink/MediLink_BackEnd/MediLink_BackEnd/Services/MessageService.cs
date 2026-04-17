using MediLink_BackEnd.Data;
using MediLink_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace MediLink_BackEnd.Services
{

    public interface IMessgageService
    {
        public Task<Message> PostMessage(Message message);
        public Task<List<Message>> GetMessagesFromChat(int chatID);
        public Task<Chat> CreateChat(int docID, int patientID);
        public Task<List<Chat>> GetUserChats(int userID);
    }

    public class MessageService : IMessgageService
    {
        private readonly MediLinkContext _dbContext;

        public MessageService(MediLinkContext context)
        {
            _dbContext = context;
        }

        public async Task<Message> PostMessage(Message message)
        {
            try
            {
                message.SentAt = DateTime.Now;
                message.IsRead = false;

                _dbContext.Messages.Add(message);
                await _dbContext.SaveChangesAsync();

                return message;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<List<Message>> GetMessagesFromChat(int chatID)
        {
            try
            {
                List<Message> messages = await _dbContext.Messages
                    .Include(m => m.Sender)
                    .Where(m => m.ChatID == chatID)
                    .OrderBy(m => m.SentAt)
                    .ToListAsync();

                return messages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<Chat> CreateChat(int docID, int patientID)
        {
            try
            {
                // Check if chat already exists between these two
                Chat existingChat = await _dbContext.Chats
                    .FirstOrDefaultAsync(c =>
                        c.DoctorID == docID &&
                        c.PatientID == patientID);

                if (existingChat != null) return existingChat;

                // Create new chat
                Chat chat = new Chat
                {
                    DoctorID = docID,
                    PatientID = patientID,
                    CreatedAt = DateTime.Now
                };

                _dbContext.Chats.Add(chat);
                await _dbContext.SaveChangesAsync();

                return chat;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public async Task<List<Chat>> GetUserChats(int userID)
        {
            return await _dbContext.Chats
                .Include(c => c.Doctor)
                    .ThenInclude(d => d.DataSheet)
                .Include(c => c.Patient)
                    .ThenInclude(p => p.DataSheet)
                .Where(c => c.DoctorID == userID || c.PatientID == userID)
                .ToListAsync();
        }
    }
}
