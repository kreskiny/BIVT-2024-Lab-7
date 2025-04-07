using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_7
{
    public class Blue_1
    {
        public class Response
        {
            // поля
            private string name;
            protected int votes;

            // свойства
            public string Name => name;
            public int Votes => votes;

            // Конструктор
            public Response(string name)
            {
                name = name;
                votes = 0; 
            }

            // Метод для подсчета голосов
            public virtual int CountVotes(Response[] responses)
            {
                if (responses == null || responses.Length==0) return 0;
                int count = 0;
                foreach (var response in responses)
                {
                    if (response.Name == Name)
                    {
                        count++;
                    }
                }
                votes = count;
                return votes;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Votes}");
            }
        }
        public class HumanResponse : Response
        {
            private string surname;
            public string Surname => surname;

            // Конструктор, принимающий имя и фамилию
            public HumanResponse(string name, string surname) : base(name)
            {
                surname = surname; 
            }
            public override int CountVotes(Response[] responses)
            {
                int count = 0;
                if (responses == null || responses.Length == 0) return 0;
                foreach (var response in responses)
                {
                    if (response is HumanResponse humanResponse)
                    {
                        if (humanResponse.Name == Name && humanResponse.Surname == Surname)
                        {
                            count++;
                        }
                    }
                }
                votes = count;
                return count;
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} {Votes}");
            }
        }
    }
}