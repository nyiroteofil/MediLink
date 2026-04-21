using MediLink_BackEnd.Models;
using MediLink_BackEnd.Data.DTOs;
using MediLink_BackEnd.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMessgageService _messageService;

    public MessageController(IMessgageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    public async Task<IActionResult> PostMessage([FromBody] MessageDTO dto)
    {
        try
        {
            Message message = new Message
            {
                ChatID = dto.ChatID,
                SenderID = dto.SenderID,
                Content = dto.Content
            };

            Message sent = await _messageService.PostMessage(message);
            return Ok(sent);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "\"An internal server error occurred.\"");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetMessagesFromChat(int chatID)
    {
        try
        {
            List<Message> messages = await _messageService.GetMessagesFromChat(chatID);
            return Ok(messages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "\"An internal server error occurred.\"");
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat(int docID, int patientID)
    {
        try
        {
            Chat chat = await _messageService.CreateChat(docID, patientID);
            return Ok(chat);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.ToString());
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUserChats(int userID)
    {
        try
        {
            List<Chat> chats = await _messageService.GetUserChats(userID);
            return Ok(chats);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "\"An internal server error occurred.\"");
        }
    }
}