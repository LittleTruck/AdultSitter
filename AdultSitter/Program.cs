using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Line.Messaging;
using Line.Messaging.Webhooks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace AdultSitter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public class LineBotConfig
    {
        public string channelSecret { get; set; }
        public string accessToken { get; set; }
    }
    public class LineBotApp : WebhookApplication
    {
        private readonly LineMessagingClient _messagingClient;
        public LineBotApp(LineMessagingClient lineMessagingClient)
        {
            _messagingClient = lineMessagingClient;
        }

        protected override async Task OnMessageAsync(MessageEvent ev)
        {
            var result = null as List<ISendMessage>;

            switch (ev.Message)
            {
                //文字訊息
                case TextEventMessage textMessage:
                    {
                        //頻道Id
                        var channelId = ev.Source.Id;
                        //使用者Id
                        var userId = ev.Source.UserId;

                        //回傳 hellow
                        result = new List<ISendMessage>
                    {
                        new TextMessage("？？？？")
                    };
                    }
                    break;
            }

            if (result != null)
                await _messagingClient.ReplyMessageAsync(ev.ReplyToken, result);
        }
    }
}
