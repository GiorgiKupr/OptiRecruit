using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Models.ChatGPT;
using Newtonsoft.Json;

namespace Application.Abstraction
{
    public interface IChatClient
    {
        Task<ChatResponse> GetResponseAsync(IList<ChatMessage> messages, CancellationToken cancellationToken = default);
    }

   
}
