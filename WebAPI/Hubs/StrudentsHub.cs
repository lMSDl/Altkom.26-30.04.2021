using DataService.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Hubs
{
    public class StudentsHub : Hub
    {
        private IService<Student> _service;

        public StudentsHub(IService<Student> service)
        {
            _service = service;
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine(Context.ConnectionId);

            await Clients.All.SendAsync("NewClient", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async Task AddStudent(Student student)
        {
            student = _service.Create(student);
            await NotifyCreated(Clients, student);
        }

        public static async Task NotifyCreated(IHubClients<IClientProxy> clients, Student student)
        {
            await clients.All.SendAsync("Post", student);
        }


        public static async Task NotifyWhenGroupYear2Created(IHubClients<IClientProxy> clients, Student student)
        {
            if(student.StudyYear == 2)
                await clients.Groups("2").SendAsync("Post", student);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
