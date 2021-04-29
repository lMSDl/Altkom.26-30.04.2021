using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataService.Interfaces;
using Grpc.Core;
using Grpc.Protos;

namespace Grpc.Services
{
    public class StudentsService : GrpcStrudentsService.GrpcStrudentsServiceBase
    {
        private IService<Models.Student> _service;
        private IMapper _mapper;

        public StudentsService(IService<Models.Student> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public override Task<Student> Create(Student request, ServerCallContext context)
        {
            var student = _mapper.Map<Models.Student>(request);
            student = _service.Create(student);
            var response = _mapper.Map<Student>(student);
            return Task.FromResult(response);
        }

        //private static Models.Student ConvertFromStudent(Student request)
        //{
        //    return new Models.Student() { LastName = request.LastName, FirstName = request.FirstName };
        //}

        //private static Student ConvertToStudent(Models.Student student)
        //{
        //    return new Student { Id = student.Id, FirstName = student.FirstName, LastName = student.LastName };
        //}

        public override Task<None> Delete(Student request, ServerCallContext context)
        {
            _service.Delete(request.Id);
            return Task.FromResult(new None());
        }

        public override Task<Students> Read(None request, ServerCallContext context)
        {
            var students = _service.Read();
            var response = new Students();
            response.Collection.AddRange(students.Select(_mapper.Map<Student>));

            return Task.FromResult(response);
        }

        public override Task<Student> ReadById(Student request, ServerCallContext context)
        {
            var student = _service.Read(request.Id);
            var response = _mapper.Map<Student>(student);
            return Task.FromResult(response);
        }

        public override Task<None> Update(Student request, ServerCallContext context)
        {
            _service.Update(request.Id, _mapper.Map<Models.Student>(request));
            return Task.FromResult(new None());
        }
    }
}
