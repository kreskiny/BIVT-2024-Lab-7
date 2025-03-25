using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_5
    {
        public class Sportsman
        {
            // поля
            private string name;
            private string surname;
            private int place;
            private bool is_setted;

            // свойства 
            public string Name => name;
            public string Surname => surname;
            public int Place => place;

            // Конструктор
            public Sportsman(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                place = 0;
                is_setted = false;
            }

            // Методы
            public void SetPlace(int place)
            {
                if (this.place == 0)
                {
                    this.place = place;
                    is_setted = true;
                }
                else
                {
                    Console.WriteLine($"Место для {Name} {Surname} уже установлено.");
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} {Place}");
            }
        }

        public abstract class Team
        {
            // поля
            private string name;
            private Sportsman[] sportsmen;
            private int count;

            // свойства 
            public string Name => name;
            public Sportsman[] Sportsmen => sportsmen;

            public int SummaryScore
            {
                get
                {
                    if (sportsmen == null) return 0;
                    int total = 0;
                    foreach (var sportsman in sportsmen)
                    {
                        if (sportsman != null) // Проверка на null
                        {
                            switch (sportsman.Place)
                            {
                                case 1: total += 5; break;
                                case 2: total += 4; break;
                                case 3: total += 3; break;
                                case 4: total += 2; break;
                                case 5: total += 1; break;
                                default: total += 0; break;
                            }
                        }
                    }
                    return total;
                }
            }

            public int TopPlace
            {
                get
                {
                    if (sportsmen == null) return 0;
                    int top = int.MaxValue; // Изменено на MaxValue для правильной инициализации
                    foreach (var sportsman in sportsmen)
                    {
                        if (sportsman != null && sportsman.Place < top && sportsman.Place != 0)
                        {
                            top = sportsman.Place;
                        }
                    }
                    return top == int.MaxValue ? 0 : top; // Если нет установленного места, вернуть 0
                }
            }

            // Конструктор
            public Team(string name)
            {
                this.name = name;
                this.sportsmen = new Sportsman[6];
                this.count = 0;
            }

            // Метод добавления спортсмена
            public void Add(Sportsman sportsman)
            {
                if (count < sportsmen.Length)
                {
                    sportsmen[count++] = sportsman;
                }
                else
                {
                    Console.WriteLine("Команда полна, не удалось добавить спортсмена.");
                }
            }

            public void Add(params Sportsman[] newSportsmen)
            {
                foreach (var sportsman in newSportsmen)
                {
                    Add(sportsman);
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name}");
                foreach (var sportsman in sportsmen)
                {
                    sportsman.Print();
                }
                Console.WriteLine();
            }
        }
        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }
        }

    }
}
