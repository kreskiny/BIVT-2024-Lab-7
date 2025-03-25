using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_4
    {
        public abstract class Team
        {
            // поля
            private string name;
            private int[] scores; 

            //свойства
            public string Name => name;
            public int[] Scores 
            { 
                get
                {
                    if (scores == null) return null;
                    int[] copyArray = new int[scores.Length];
                    Array.Copy(scores, copyArray, scores.Length);
                    return copyArray;
                }
            }

            public int TotalScore
            {
                get
                {
                    if (scores == null) return 0; // Проверка на инициализацию
                    int total = 0;
                    foreach (int score in scores)
                    {
                        total += score;
                    }
                    return total;
                }
            }

            // Конструктор
            public Team(string name)
            {
                this.name = name;
                this.scores = new int[0]; 
            }

            // Методы
            public void PlayMatch(int result)
            {
                if(scores == null) return; // Проверка на инициализацию
                Array.Resize(ref scores, scores.Length + 1);
                scores[scores.Length - 1] = result;
            }
            public void Print()
            {
                Console.Write($"{Name} ");
                foreach (int score in scores)
                {
                    Console.Write($"{score} ");
                }
                Console.WriteLine();
            }
        }

        public class Group
        {
            // Поля
            private string name;
            private Team[] manTeams;
            private Team[] womanTeams;
            private int manIndex;
            private int womanIndex;

            // Свойства
            public string Name => name;
            public Team[] ManTeams => manTeams;
            public Team[] WomanTeams => womanTeams;

            // Конструктор
            public Group(string name)
            {
                this.name = name;
                manTeams = new Team[12];
                womanTeams = new Team[12];
                manIndex = 0;
                womanIndex = 0;
            }

            // Методы
            public void Add(Team team)
            {
                if (team == null) return; // Проверка на инициализацию

                if (team is ManTeam && manIndex < manTeams.Length)
                {
                    manTeams[manIndex] = team;
                    manIndex++;
                }
                else if (team is WomanTeam && womanIndex < womanTeams.Length)
                {
                    womanTeams[womanIndex] = team;
                    womanIndex++;
                }
            }

            public void Add(params Team[] newTeams)
            {
                foreach (var team in newTeams)
                {
                    Add(team);
                }
            }

            private void SortTeams(Team[] teams, int count)
            {
                for (int i = 0; i < count - 1; i++)
                {
                    for (int j = 0; j < count - 1 - i; j++)
                    {
                        if (teams[j].TotalScore < teams[j + 1].TotalScore)
                        {
                            Team temp = teams[j];
                            teams[j] = teams[j + 1];
                            teams[j + 1] = temp;
                        }
                    }
                }
            }

            public void Sort()
            {
                SortTeams(manTeams, manIndex);
                SortTeams(womanTeams, womanIndex);
            }

            public static Group Merge(Group group1, Group group2, int size)
            {
                Group finalists = new Group("Финалисты");

                int halfSize = size / 2;

                MergeTeams(finalists, group1.ManTeams, group2.ManTeams, halfSize);
                MergeTeams(finalists, group1.WomanTeams, group2.WomanTeams, halfSize);

                return finalists;
            }

            private static void MergeTeams(Group finalists, Team[] teams1, Team[] teams2, int halfSize)
            {
                int index1 = 0, index2 = 0;

                while (index1 < halfSize && index2 < halfSize)
                {
                    if (teams1[index1]?.TotalScore >= teams2[index2]?.TotalScore)
                    {
                        finalists.Add(teams1[index1]);
                        index1++;
                    }
                    else
                    {
                        finalists.Add(teams2[index2]);
                        index2++;
                    }
                }

                while (index1 < halfSize)
                {
                    finalists.Add(teams1[index1]);
                    index1++;
                }

                while (index2 < halfSize)
                {
                    finalists.Add(teams2[index2]);
                    index2++;
                }
            }

            // Метод для печати
            public void Print()
            {
                Console.WriteLine(Name);

                Console.WriteLine("Мужские команды:");
                foreach (var team in manTeams)
                {
                    team?.Print();
                }

                Console.WriteLine("Женские команды:");
                foreach (var team in womanTeams)
                {
                    team?.Print();
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
