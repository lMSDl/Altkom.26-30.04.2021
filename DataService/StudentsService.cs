using DataService.Interfaces;
using Models;
using System;
using System.Collections.Generic;

namespace DataService
{
    public class StudentsService : Service<Student>, IStudentsService
    {
    }
}
