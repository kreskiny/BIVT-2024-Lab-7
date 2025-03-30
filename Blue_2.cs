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
            private int participantCount;

            // Конструктор 
            protected WaterJump(string name, int bank)
            {
                name = name;
                bank = bank;
                participants = new Participant[participantCount];
                participantCount = 0; // Счетчик участников
            }

            // Свойства
            public string Name => name;

            public int Bank => bank;

            public Participant[] Participants
            {
                get { return participants; }
            }

            public int ParticipantCount => participantCount;
            public abstract double[] Prize { get; }



            public void Add(Participant participant)
            {
                if (participantCount < participants.Length)
                {
                    participants[participantCount] = participant;
                    participantCount++;
                }
                else
                {
                    Console.WriteLine("достигнут лимит участников");
                }
            }

            // Метод для добавления нескольких участников
            public void Add(params Participant[] newParticipants)
            {
                foreach (var participant in newParticipants)
                {
                    Add(participant); // Используем метод для добавления одного участника
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
                    if (marks == null || marks.Length == 0) return null;
                    int[,] copyArray = new int[marks.GetLength(0), marks.GetLength(1)];
                    Array.Copy(marks, copyArray, marks.Length);
                    return copyArray;
                }
            }
            public int TotalScore
            {
                get
                {
                    if (marks == null || marks.Length == 0) return 0; // Проверка на инициализацию

                    int total = 0;
                    for (int i = 0; i < marks.GetLength(0); i++)
                    {
                        for (int j = 0; j < marks.GetLength(1); j++)
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
                this.marks = new int[2, 5];
            }

            // Методы
            public void Jump(int[] result)
            {
                if (result == null || result.Length == 0)
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
                if (marks == null || marks.Length == 0)
                {
                    return;
                }
                for (int i = 0; i < marks.GetLength(0); i++)
                {
                    bool isEmpty = true;
                    for (int j = 0; j < marks.GetLength(1); j++)
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
                if (array == null) return;
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
            public WaterJump3m(string name, int bank, int numberOfParticipants)
                : base(name, bank, numberOfParticipants)
            {
            }

            // Переопределение свойства Prize
            public override double[] Prize
            {
                get
                {
                    // Если участников меньше 3-х, возвращаем пустой массив
                    if (ParticipantCount < 3)
                    {
                        return new double[0];
                    }

                    double firstPlacePrize = Bank * 0.5;
                    double secondPlacePrize = Bank * 0.3;
                    double thirdPlacePrize = Bank * 0.2;

                    return new double[] { firstPlacePrize, secondPlacePrize, thirdPlacePrize };
                }
            }
        }

        public class WaterJump5m : WaterJump
        {
            public WaterJump5m(string name, int bank, int numberOfParticipants) :
                base(name, bank, numberOfParticipants)
            { }
            public override double[] Prize
            {
                get
                {
                    if (ParticipantCount < 3)
                    {
                        return new double[0];
                    }
                    Participant.Sort(Participants);
                    int topParc = Participants.Length / 2;
                    double[] prizes = null;
                    if (topParc <= 3)
                    {
                        prizes = new double[3];
                    }
                    else if (topParc <= 10)
                    {
                        prizes = new double[topParc];
                    }
                    else
                    {
                        prizes = new double[10];
                    }
                    double firstPlacePrize = Bank * 0.4;
                    double secondPlacePrize = Bank * 0.25;
                    double thirdPlacePrize = Bank * 0.15;
                    double othersPlacePrise = Bank * 20 / topParc / 100;
                    prizes[0] = firstPlacePrize;
                    prizes[1] = secondPlacePrize;
                    prizes[2] = thirdPlacePrize;
                    for (int i = 0; i < topParc; i++)
                    {
                        prizes[i] = othersPlacePrise;
                    }
                    return prizes;
                }
            }
        }
    }
}
