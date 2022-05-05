using System;

namespace Фоновые61
{
    class Transport
    {
        protected string name;
        protected int fuelUsage;
        protected int fuelAmount;

        protected Transport(string name)
        {
            this.name = name;
            fuelUsage = 1;
            fuelAmount = 100;
        }

        protected Transport(string name, int fuelUs, int fuelAm)
        {
            this.name = name;
            fuelUsage = fuelUs;
            fuelAmount = fuelAm;
        }

        public int fuelFor(int length)
        {
            return length * fuelUsage / 100;
        }

        public string Name
        {
            get { return name; }
        }

        public int MaxDist
        {
            get { return fuelAmount / fuelUsage; }
        }
    }

    class PassengerCar : Transport
    {
        protected enum bodyTypes {Седан, Купе, Хечбек, Универсал, Кабриолет};
        protected bodyTypes body;
        protected int PassengersAmount;

        public PassengerCar()
        : base("Легковушка")
        {
            this.body = (bodyTypes)1;
            this.PassengersAmount = 0;
        }

        public PassengerCar(int bodyType, int PassAm)
        : base("Легковушка")
        {
            this.body = (bodyTypes)bodyType;
            this.PassengersAmount = PassAm;
        }

        public PassengerCar(int bodyType, int PassAm, int fuelUsage, int fuelAmount)
        : base("Легковушка", fuelUsage, fuelAmount)
        {
            this.body = (bodyTypes)bodyType;
            this.PassengersAmount = PassAm;
        }

        public int PassFuelFor(int length)
        {
            return length * (fuelUsage + PassengersAmount);
        }

        public int PassPercent()
        {
            return PassengersAmount * 100 / 27;
        }

        public int PassAmount
        {
            get { return PassengersAmount; }
            set
            {
                if (value < 27) PassengersAmount = value;
            }
        }
    }

    class CargoCar : Transport
    {
        protected int MaxWeight;
        protected int weight;

        public CargoCar() : base("Грузовик")
        {
            MaxWeight = 10;
            weight = 1;
        }

        public CargoCar(int weight, int MaxWeight) : base("Грузовик")
        {
            if (weight < MaxWeight)
            {
                this.MaxWeight = MaxWeight;
                this.weight = weight;
            }
            else
            {
                this.MaxWeight = 10;
                this.weight = 1;
            }
        }

        public CargoCar(int weight, int MaxWeight, int fuelUsage, int fuelAmount) : base("Грузовик", fuelUsage, fuelAmount)
        {
            if (weight < MaxWeight)
            {
                this.MaxWeight = MaxWeight;
                this.weight = weight;
            }
            else
            {
                this.MaxWeight = 10;
                this.weight = 1;
            }
        }

        public int CargoFuelFor(int length)
        {
            return length * (fuelUsage + weight);
        }

        public int CargoAmount
        {
            get { return weight; }
            set
            {
                if (value < MaxWeight) weight = value;
            }
        }

        public int CargoPercent
        {
            get { return weight * 100 / MaxWeight; }
        }
    }

    class Bus : Transport
    {
        protected int passAmount;
        protected int rideCost;

        public Bus() : base("Автобус")
        {
            passAmount = 10;
            rideCost = 50;
        }

        public Bus(int passAm, int rCost) : base("Автобус")
        {
            passAmount = passAm;
            rideCost = rCost;
        }

        public Bus(int passAm, int rCost, int fuelUsage, int fuelAmount) : base("Автобус", fuelUsage, fuelAmount)
        {
            passAmount = passAm;
            rideCost = rCost;
        }

        public int BusFuelFor(int time, int length)
        {
            return (Math.Abs(time - 12) * 100 / 12 + fuelUsage) * length;
        }

        public int Stonks
        {
            get { return rideCost * passAmount; }
        }
        
        public int LoadPerc
        {
            get { return passAmount;}
        }
    }



    class MainClass
    {
        public static void Main(string[] args)
        {
            Random rnd = new Random();
            PassengerCar[] pass = new PassengerCar[3];
            pass[0] = new PassengerCar();
            pass[1] = new PassengerCar(rnd.Next(4), rnd.Next(1, 4));
            pass[2] = new PassengerCar(rnd.Next(4), rnd.Next(4), rnd.Next(10), rnd.Next(100, 1000));
            

            CargoCar[] cargo = new CargoCar[3];
            cargo[0] = new CargoCar();
            cargo[1] = new CargoCar(rnd.Next(10), rnd.Next(10, 100));
            cargo[2] = new CargoCar(rnd.Next(10), rnd.Next(10, 100), rnd.Next(10), rnd.Next(100, 1000));

            Bus[] bus = new Bus[3];
            bus[0] = new Bus();
            bus[1] = new Bus(rnd.Next(100), rnd.Next(100));
            bus[2] = new Bus(rnd.Next(100), rnd.Next(100), rnd.Next(10), rnd.Next(100, 1000));

            Console.WriteLine("ЛЕГКОВУШКА");
            int length = rnd.Next(10);
            Console.WriteLine($"Расстояние: {length}");
            Console.WriteLine($"Количество затраченного топлива: {pass[0].PassFuelFor(length)}");
            Console.WriteLine($"Процент загрузки: {pass[1].PassPercent()}");
            Console.WriteLine($"Количество пассажиров: {pass[2].PassAmount}");
            Console.WriteLine();

            Console.WriteLine("ГРУЗОВИК");
            length = rnd.Next(10);
            Console.WriteLine($"Расстояние: {length}");
            Console.WriteLine($"Количество затраченного топлива: {cargo[0].CargoFuelFor(length)}");
            Console.WriteLine($"Процент загрузки: {cargo[1].CargoPercent}");
            Console.WriteLine($"Масса груза: {cargo[2].CargoAmount}");
            Console.WriteLine();

            Console.WriteLine("АВТОБУС");
            length = rnd.Next(10);
            Console.WriteLine($"Расстояние: {length}");
            int time = rnd.Next(24);
            Console.WriteLine($"Время: {time}");
            Console.WriteLine($"Количество затраченного топлива: {bus[0].BusFuelFor(time, length)}");
            Console.WriteLine($"Выручка: {bus[1].Stonks}");
            Console.WriteLine($"Процент загрузки: {bus[2].LoadPerc}");
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
