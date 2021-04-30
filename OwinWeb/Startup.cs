using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OwinWeb
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                await next();
            });

            app.MapWhen(context => context.WebSockets.IsWebSocketRequest, appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    var websocket = await context.WebSockets.AcceptWebSocketAsync();
                    var helloMessage = Encoding.UTF8.GetBytes("Hello in WebSocket");
                    await websocket.SendAsync(new ArraySegment<byte>(helloMessage, 0, helloMessage.Length), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);

                    byte[] buffer = new byte[1024];
                    do
                    {
                        try
                        {
                            var received = await websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                            await websocket.SendAsync(new ArraySegment<byte>(buffer, 0, received.Count), received.MessageType, received.EndOfMessage, CancellationToken.None);
                        }
                        catch
                        {
                            //ignore
                        }

                    } while (!websocket.CloseStatus.HasValue);

                    await websocket.CloseAsync(websocket.CloseStatus.Value, websocket.CloseStatusDescription, CancellationToken.None);
                });
            });

            app.UseOwin(owin => owin(next => HelloOwin));
        }

        private Task HelloOwin(IDictionary<string, object> environment)
        {
            var responseStream = (Stream)environment["owin.ResponseBody"];

            string response = "Hello from Owin";
            var responseBytes = Encoding.UTF8.GetBytes(response);

            var headers = (IDictionary<string, string[]>)environment["owin.ResponseHeaders"];
            headers["Content-Length"] = new string[] { responseBytes.Length.ToString() };
            headers["Content-Type"] = new string[] { "text/plain" };

            return responseStream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }
    }
}
