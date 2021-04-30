using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DobrePraktyki.SOLID.L
{
    abstract class Vehicle
    {
        public string Name { get; set; }
        public abstract void Move();
    }

    class Airplane : Vehicle
    {
        public void Fly()
        {
            Console.WriteLine("I am flying!");
        }

        public override void Move()
        {
            Fly();
        }
    }

    class Car : Vehicle
    {
        public override void Move()
        {
            Ride();
        }

        public void Ride()
        {
            Console.WriteLine("I am riding!");
        }
    }

    class Program
    {
        void Move(Vehicle vehicle)
        {
            vehicle.Move();

        }
    }
}
