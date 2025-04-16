using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab_7
{
    public class Blue_2
    {
        public abstract class WaterJump
        {
            private string name;
            private int bank;
            private Participant[] participants;

            // Конструктор 
            protected WaterJump(string name, int bank)
            {
                this.name = name;
                this.bank = bank;
                participants = new Participant[0];
            }

            // Свойства
            public string Name => name;

            public int Bank => bank;

            public Participant[] Participants => participants;
            

            public abstract double[] Prize { get; }



            public void Add(Participant New_Part)
            {
                if (participants == null) return;
                Array.Resize(ref participants, participants.Length + 1);
                participants[participants.Length - 1] = New_Part;

            }

            // Метод для добавления нескольких участников
            public void Add(Participant[] newParticipants)
            {
                if (participants==null) return;
                foreach (var participant in newParticipants)
                {
                    Add(participant); 
                }
            }
        }
        public struct Participant
        {
            //поля
            private string name;
            private string surname;
            private int[,] marks; // Матрица оценок судей (2 прыжка, 5 судей)

            //свойства 
            public string Name => name;
            public string Surname => surname;
            public int[,] Marks
            {
                get
                {
                    if (marks == null) return null;
                    int[,] copyArray = new int[2, 5];
                    Array.Copy(marks, copyArray, marks.Length);
                    return copyArray;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (marks == null) return 0; 

                    int total = 0;
                    for (int i = 0; i <2; i++)
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            total += marks[i, j];
                        }
                    }
                    return total;
                }
            }

            // Конструктор
            public Participant(string name, string surname)
            {
                this.name = name;
                this.surname = surname;
                marks = new int[2, 5];
            }

            // Методы
            public void Jump(int[] result)
            {
                if (result == null || result.Length == 0 || marks == null)
                {
                    return;
                }

                //количество оценок для записи (максимум 5)
                int scoresToTake = 0;
                if (result.Length < 5)
                {
                    scoresToTake = result.Length;
                }
                else
                {
                    scoresToTake = 5;
                }
                
                for (int i = 0; i < 2; i++)
                {
                    bool isEmpty = true;
                    for (int j = 0; j < 5; j++)
                    {
                        if (marks[i, j] != 0)
                        {
                            isEmpty = false;
                            break;
                        }
                    }

                    if (isEmpty)
                    {
                        for (int j = 0; j < scoresToTake; j++)
                        {
                            marks[i, j] = result[j];
                        }
                        return;
                    }
                }

                return;
            }


            public static void Sort(Participant[] array)
            {
                if (array == null || array.Length==0) return;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    for (int j = 0; j < array.Length - i - 1; j++)
                    {
                        if (array[j].TotalScore < array[j + 1].TotalScore)
                        {
                            // Меняем местами участников
                            Participant temp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = temp;
                        }
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine($"{Name} {Surname} {TotalScore}");
            }
        }
        public class WaterJump3m : WaterJump
        {
            // Конструктор
            public WaterJump3m(string name, int bank)
                : base(name, bank)
            {
            }

            // Переопределение свойства Prize
            public override double[] Prize
            {
                get
                {
                    if (Participants == null) return null;
                    // Если участников меньше 3-х, возвращаем пустой массив
                    if (Participants.Length < 3) return null;

                    double firstPlacePrize = Bank * 0.5;
                    double secondPlacePrize = Bank * 0.3;
                    double thirdPlacePrize = Bank * 0.2;

                    return new double[] { firstPlacePrize, secondPlacePrize, thirdPlacePrize };
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank) :
                base(name, bank)
            { }
            public override double[] Prize
            {
                get
                {
                    if (Participants == null) return null;
                    if (Participants.Length < 3) return null;
                    int topParc = Participants.Length / 2;
                    if (topParc > 10)
                    {
                        topParc = 10;
                    }

                    double[] prizes = new double[topParc];
                    double othersPlacePrise = Bank * 0.2 / topParc;
                    prizes[0] = Bank * 0.4 + othersPlacePrise;
                    prizes[1] = Bank * 0.25 + othersPlacePrise;
                    prizes[2] = Bank * 0.15 + othersPlacePrise;
                    for (int i = 3; i < prizes.Length; i++)
                    {
                        prizes[i] = othersPlacePrise;
                    }
                    return prizes;
                }
            }
        }
    }
}
