using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab_7
{
    public class Blue_3
    {
        public class Participant
        {
            //поля
            private string name;
            private string surname;
            protected int[] penalties;
            protected bool is_expelled;

            //свойства
            public string Name => name;
            public string Surname => surname;
            public int[] Penalties
            {
                get
                {
                    if (penalties == null) return null;
                    int[] copyArray = new int[penalties.Length];
                    Array.Copy(penalties, copyArray, penalties.Length);
                    return copyArray;
                }
            }
            public int Total
            {
                
                get
                {
                    if (penalties == null) return 0;
                    int total = 0;
                    foreach (int time in penalties)
                    {
                        total += time;
                    }
                    return total;
                }
            }

            public virtual bool IsExpelled 
            {
                get 
                {
                    if (penalties == null) return false;
                    is_expelled = false;
                    for (int i = 0; i < penalties.Length; i++)
                    {
                        if (penalties[i] == 10)
                        {
                            is_expelled = true;
                            return true;
                        }
                    }
                    return is_expelled;
                }
            }
            

            // Конструктор
            public Participant(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                penalties = new int[0]; 
                is_expelled = false;
            }

            // Методы
            public virtual void PlayMatch(int time)
            {
                if (time < 0) return;
                if (penalties == null) return; 
                Array.Resize(ref penalties, penalties.Length + 1);
                penalties[penalties.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    for (int j = 0; j < array.Length-1-i; j++)
                    {
                        if (array[j].Total > array[j+1].Total)
                        {
                            Participant elem = array[j];
                            array[j] = array[j+1];
                            array[j+1] = elem;
                        }
                    }
                }
                
            }

            public void Print()
            {
                Console.Write($"{Name} {Surname} ");
                foreach (int time in penalties)
                {
                    Console.Write($"{time} ");
                }
                Console.WriteLine();
                if (IsExpelled)
                {
                    Console.WriteLine("Исключен из списка кандидатов.");
                }
                Console.WriteLine();
            }
        }
        public class BasketballPlayer : Participant
        {
            private int matchCount;
            private int foulCount;

            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                matchCount = 0;
                foulCount = 0;
            }

            public override void PlayMatch(int falls)
            {
                if (penalties == null || falls < 0 || falls > 5) return;
                base.PlayMatch(falls);
            }

            public override bool IsExpelled
            {
                get
                {
                    if (penalties == null) return false;
                    int count_5 = 0;
                    int count = 0;
                    foreach (int fall in penalties)
                    {
                        count += fall;
                        if (fall == 5) count_5++;
                    }
                    if (count_5 > penalties.Length * 0.1 || count > penalties.Length * 2)
                    {
                        is_expelled = true;
                        return is_expelled;
                    }
                    return false;
                }

            }

        }
        public class HockeyPlayer : Participant
        {
            private static int totalPenaltyTime;
            private static int matchCount;
            private static int playerCount; // добавлено

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                penalties = new int[0];
                playerCount++; // добавлено
            }

            public override void PlayMatch(int falls)
            {
                if (falls < 0 || penalties == null) return;
                base.PlayMatch(falls);
                totalPenaltyTime += falls;
                matchCount++;
            }

            public override bool IsExpelled
            {
                get
                {
                    if (penalties == null) return false;

                    for (int i = 0; i < penalties.Length; i++)
                    {
                        if (penalties[i] >= 10)
                        {
                            return true;
                        }
                    }

                    if (playerCount == 0) return false;

                    if (Total > 0.1 * totalPenaltyTime / playerCount) // исправлено
                    {
                        return true;
                    }

                    return false;
                }
            }
        }

    }
}
