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
                    if (penalties == null || penalties.Length == 0) return false;
                    for (int i = 0; i < penalties.Length; i++)
                    {
                        if (penalties[i] == 10) return true;
                    }
                    return false;
                }
            }
            

            // Конструктор
            public Participant(string name, string surname)
            {
                name = name;
                surname = surname;
                penalties = new int[0]; 
                is_expelled = false;
            }

            // Методы
            public virtual void PlayMatch(int time)
            {
                if (penalties == null) return; 
                Array.Resize(ref penalties, penalties.Length + 1);
                penalties[penalties.Length - 1] = time;
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length - 1; i++)
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
        public class BasketballPlayer: Participant
        {
            private int matchCount;
            private int foulCount;

            public BasketballPlayer(string name, string surname) : base(name, surname)
            {
                matchCount = 0;
                foulCount = 0;
            }

            public override void PlayMatch(int time)
            {
                base.PlayMatch(time); 
                matchCount++;

                if (time == 5)
                {
                    foulCount++; 
                }
            }

            public override bool IsExpelled=>is_expelled;

        }
        public class HockeyPlayer : Participant
        {
            private int totalPenaltyTime;
            private int matchCount;

            public HockeyPlayer(string name, string surname) : base(name, surname)
            {
                totalPenaltyTime = 0;
                matchCount = 0;
            }

            public override void PlayMatch(int penaltyTime)
            {
                matchCount++; 
                totalPenaltyTime += penaltyTime; 

                
                if (penaltyTime >= 10)
                {
                    is_expelled = true;
                }
            }

            public override bool IsExpelled 
            {
                get
                {
                    if (penalties == null) return false;
                    int sm = 0;

                    for (int i = 0; i < penalties.Length; i++)
                    {
                        sm += penalties[i];
                        if (penalties[i] >= 10) 
                        { 
                            is_expelled = true; 
                            return true; 
                        }
                    }
                    if (sm > 0.1 * totalPenaltyTime / matchCount) return true;

                    return false;
                }
            
            }
        }
    }
}
