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
                        if (sportsman != null) 
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
                    int top = int.MaxValue;
                    foreach (var sportsman in sportsmen)
                    {
                        if (sportsman != null && sportsman.Place < top && sportsman.Place != 0)
                        {
                            top = sportsman.Place;
                        }
                    }
                    return top == int.MaxValue ? 0 : top; 
                }
            }

            // Конструктор
            public Team(string name)
            {
                this.name = name;
                sportsmen = new Sportsman[6];
                count = 0;
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

            public void Add(Sportsman[] newSportsmen)
            {
                foreach (var sportsman in newSportsmen)
                {
                    Add(sportsman);
                }
            }

            public static void Sort(Team[] teams)
            {
                if (teams == null) return;
                for (int i = 0; i < teams.Length - 1; i++)
                {
                    for (int j = i + 1; j < teams.Length; j++)
                    {
                        if (teams[i].SummaryScore < teams[j].SummaryScore ||
                            (teams[i].SummaryScore == teams[j].SummaryScore && teams[i].TopPlace > teams[j].TopPlace))
                        {
                            Team temp = teams[i];
                            teams[i] = teams[j];
                            teams[j] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name}");
                foreach (var sportsman in sportsmen)
                {
                    sportsman?.Print();
                }
                Console.WriteLine();
            }

            protected abstract double GetTeamStrength();

            public static Team[] GetChampion(Team[] teams)
            {
                if (teams == null || teams.Length == 0)
                    return null;

                Team maleChampion = null;
                Team femaleChampion = null;
                double maxMaleStrength = double.MinValue;
                double maxFemaleStrength = double.MinValue;

                foreach (var team in teams)
                {
                    if (team == null) continue;

                    double strength = team.GetTeamStrength();

                    if (team is ManTeam)
                    {
                        if (strength > maxMaleStrength)
                        {
                            maxMaleStrength = strength;
                            maleChampion = team;
                        }
                    }
                    else if (team is WomanTeam)
                    {
                        if (strength > maxFemaleStrength)
                        {
                            maxFemaleStrength = strength;
                            femaleChampion = team;
                        }
                    }
                }

                return new Team[] { maleChampion, femaleChampion };
            }
        }

        public class ManTeam : Team
        {
            public ManTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                int validCount = 0;
                double sum = 0;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null && sportsman.Place != 0)
                    {
                        sum += sportsman.Place;
                        validCount++;
                    }
                }

                if (validCount == 0) return 0;
                return 100 / (sum / validCount);
            }
        }

        public class WomanTeam : Team
        {
            public WomanTeam(string name) : base(name) { }

            protected override double GetTeamStrength()
            {
                int validCount = 0;
                double sum = 0;
                double product = 1;

                foreach (var sportsman in Sportsmen)
                {
                    if (sportsman != null && sportsman.Place != 0)
                    {
                        sum += sportsman.Place;
                        product *= sportsman.Place;
                        validCount++;
                    }
                }

                if (validCount == 0 || product == 0) return 0;
                return 100 * sum * validCount / product;
            }
        }
    }
}