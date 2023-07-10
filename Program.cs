using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using static System.Console;

namespace DelegatesAndEvents
{
    
    /// <summary>
    /// Делегат для обработки событий
    /// </summary>
    public delegate void CarEventHandler();

    /// <summary>
    /// Абстрактный класс <c>Car</c> (автомобиль)
    /// </summary>
    public abstract class Car
    {
        /// <summary>
        /// Свойство <c>Name</c> автомобиля <c>Car</c> (имя автомобиля). Задаётся пользователем при создании гонки.
        /// </summary>
        ///<returns>Возвращает строку типа <see cref="string"/>  с именем автомобиля</returns>
        public string Name { get; set; }
        /// <summary>
        /// Свойство <c>MaxSpeed</c> автомобиля <c>Car</c> (максимальная скорость автомобиля). 
        /// Установлено разное значение для разных типов автомобилей.
        /// </summary>
        /// <returns>Возвращает число типа <see cref="int"/>. </returns>
        public int MaxSpeed { get; set; }
        //public int carAcceleration { get; set; }

        /// <summary>
        /// Свойство <c>DrivenDistance</c> автомобиля <c>Car</c> (пройденная дистанция). 
        /// Начальное значение - ноль. Увеличивается в процессе "гонки", если автомобиль не сломался.
        /// </summary>
        public int DrivenDistance { get; set; } = 0;

        /// <summary>
        /// Свойство <c>carCrash</c> автомобиля <c>Car</c> (метка о поломке). 
        /// Начальное значение - false. Меняется на true, если автомобиль сломался.
        /// </summary>
        public bool carCrash { get; set; } = false;

        /// <summary>
        /// Свойство <c>carFinish</c> автомобиля <c>Car</c> (метка о финишировании). 
        /// Начальное значение - false. Меняется на true, если автомобиль финишировал.
        /// </summary>
        public bool carFinish { get; set; } = false;

        /// <summary>
        /// Абстрактный метод <c>Drive</c> (ехать). 
        /// Реализует движение автомобиля в зависимости от класса. 
        /// </summary>
        public abstract void Drive();
    }

    /// <summary>
    /// Класс <c>SportCar</c>, наследник <c>Car</c>, устанавливает максимальную скорость <c>MaxSpeed</c> = 8
    /// и содержит реализацию метода <c>Drive</c> для данного типа авто
    /// </summary>
    public class SportCar : Car
    {
        /// <summary>
        /// Свойство <c>MaxSpeed</c> спортивного автомобиля <c>SportCar</c>. 
        /// </summary>
        /// <returns> Для типа <c>SportCar</c> возвращает число типа <see cref="int"/> равное <value>8</value>. </returns>
        public new int MaxSpeed { get; set; } = 8;
        /// <summary>
        /// Событие "финиширование", срабатывает, когда <c>SportCar</c> прошёл дистанцию <c>DrivenDistance</c> равную <value>100</value>, то есть финишировал.
        /// </summary>
        public event CarEventHandler Finish;

        /// <summary>
        /// Событие "поломка", срабатывает, когда скорость автомобиля <c>SportCar</c> упала до нуля.
        /// </summary>
        public event CarEventHandler Crash;

        /// <summary>
        /// Реализация метода <c>Drive</c> (ехать) для <c>SportCar</c>. 
        /// При вызове устанавливает рандомную скорость в пределах максимальной (<c>MaxSpeed</c>) скорости и подсчитывает пройденную дистанцию.
        /// Если скорость оказалась равно нулю, автомобиль "ломается" (срабатывает событие <c>Crash</c>). 
        /// Если пройденная дистанция достигает 100, автомобиль "финиширует" (срабатывает событие <c>Finish</c>. 
        /// </summary>
        public override void Drive()
        {
            int distancePerSec = new Random().Next(MaxSpeed);
            if (distancePerSec == 0)
            {
                Crash();
            }
            else
            {
                DrivenDistance += distancePerSec;

                if (DrivenDistance >= 100)
                {
                    Finish();
                }
            }
        }
    }

    public class PassengerCar : Car
    {
        /// <summary>
        /// Свойство <c>MaxSpeed</c> легкового автомобиля <c>PassengerCar</c>. 
        /// </summary>
        /// <returns> Для типа <c>PassengerCar</c> возвращает число типа <see cref="int"/> равное <value>5</value>. </returns>
        public new int MaxSpeed { get; set; } = 5;
        /// <summary>
        /// Событие "финиширование", срабатывает, когда <c>PassengerCar</c> прошёл дистанцию <c>DrivenDistance</c> равную <value>100</value>, то есть финишировал.
        /// </summary>
        public event CarEventHandler Finish;
        /// <summary>
        /// Событие "поломка", срабатывает, когда скорость автомобиля <c>PassengerCar</c> упала до нуля.
        /// </summary>
        public event CarEventHandler Crash;

        /// <summary>
        /// Реализация метода <c>Drive</c> (ехать) для <c>PassengerCar</c>. 
        /// При вызове устанавливает рандомную скорость в пределах максимальной (<c>MaxSpeed</c>) скорости и подсчитывает пройденную дистанцию.
        /// Если скорость оказалась равно нулю, автомобиль "ломается" (срабатывает событие <c>Crash</c>). 
        /// Если пройденная дистанция достигает 100, автомобиль "финиширует" (срабатывает событие <c>Finish</c>. 
        /// </summary>
        public override void Drive()
        {
            int distancePerSec = (new Random()).Next(MaxSpeed);
            if (distancePerSec == 0)
            {
                Crash();
            }
            else
            {
                DrivenDistance += distancePerSec;

                if (DrivenDistance >= 100)
                {
                    Finish();
                }
            }
        }
    }


    public class CargoCar : Car
    {
        /// <summary>
        /// Свойство <c>MaxSpeed</c> грузового автомобиля <c>CargoCar</c>. 
        /// </summary>
        /// <returns> Для типа <c>CargoCar</c> возвращает число типа <see cref="int"/> равное <value>3</value>. </returns>
        public new int MaxSpeed { get; set; } = 3;
        /// <summary>
        /// Событие "финиширование", срабатывает, когда <c>CargoCar</c> прошёл дистанцию <c>DrivenDistance</c> равную <value>100</value>, то есть финишировал.
        /// </summary>
        public event CarEventHandler Finish;
        /// <summary>
        /// Событие "поломка", срабатывает, когда скорость автомобиля <c>CargoCar</c> упала до нуля.
        /// </summary>
        public event CarEventHandler Crash;

        /// <summary>
        /// Реализация метода <c>Drive</c> (ехать) для <c>CargoCar</c>. 
        /// При вызове устанавливает рандомную скорость в пределах максимальной (<c>MaxSpeed</c>) скорости и подсчитывает пройденную дистанцию.
        /// Если скорость оказалась равно нулю, автомобиль "ломается" (срабатывает событие <c>Crash</c>). 
        /// Если пройденная дистанция достигает 100, автомобиль "финиширует" (срабатывает событие <c>Finish</c>. 
        /// </summary>
        public override void Drive()
        {
            int distancePerSec = (new Random()).Next(MaxSpeed);
            if (distancePerSec == 0)
            {
                Crash();
            }
            else
            {
                DrivenDistance += distancePerSec;

                if (DrivenDistance >= 100)
                {
                    Finish();
                }
            }
        }
    }


    public class Bus : Car
    {
        /// <summary>
        /// Свойство <c>MaxSpeed</c> автобуса <c>Bus</c>. 
        /// </summary>
        /// <returns> Для типа <c>Bus</c> возвращает число типа <see cref="int"/> равное <value>4</value>. </returns>
        public new int MaxSpeed { get; set; } = 4;
        /// <summary>
        /// Событие "финиширование", срабатывает, когда <c>Bus</c> прошёл дистанцию <c>DrivenDistance</c> равную <value>100</value>, то есть финишировал.
        /// </summary>
        public event CarEventHandler Finish;
        /// <summary>
        /// Событие "поломка", срабатывает, когда скорость автобуса <c>Bus</c> упала до нуля.
        /// </summary>
        public event CarEventHandler Crash;

        /// <summary>
        /// Реализация метода <c>Drive</c> (ехать) для <c>Bus</c>. 
        /// При вызове устанавливает рандомную скорость в пределах максимальной (<c>MaxSpeed</c>) скорости и подсчитывает пройденную дистанцию.
        /// Если скорость оказалась равно нулю, автобус "ломается" (срабатывает событие <c>Crash</c>). 
        /// Если пройденная дистанция достигает 100, автобус "финиширует" (срабатывает событие <c>Finish</c>. 
        /// </summary>
        public override void Drive()
        {
            int distancePerSec = (new Random()).Next(MaxSpeed);
            if (distancePerSec == 0)
            {
                Crash();
            }
            else
            {
                DrivenDistance += distancePerSec;

                if (DrivenDistance >= 100)
                {
                    Finish();
                }
            }
        }
    }


    public class Game
    {
        /// <summary>
        /// Список автомобилей гонки
        /// </summary>
        protected IEnumerable<Car> _race;
        /// <summary>
        /// конструктор для гонки
        /// </summary>
        /// <param name="race">Принимает список автомобилей гонки</param>
        public Game(IEnumerable<Car> race)
        {
            this._race = race;
        }
        /// <summary>
        /// Метод Старт, запускает и проводит гонку
        /// </summary>
        public void Start()
        {
            //цикл задаёт поведение экземпляром из списка автомобилей при срабатывании событий Finish и Crash
            foreach (var item in _race)
            {
                //для спортивных авто
                if (item.GetType().Equals(typeof(SportCar)))
                {
                    (item as SportCar).Finish += () =>
                    {
                        WriteLine($"Спорткар {item.Name} доехал до финиша!!!");
                    };
                    (item as SportCar).Crash += () =>
                    {
                        WriteLine($"Спорткар  {item.Name} сломался!!!");
                        item.carCrash = true;
                    };
                }

                //для легковушек
                if (item.GetType().Equals(typeof(PassengerCar)))
                {
                    (item as PassengerCar).Finish += () =>
                    {
                        WriteLine($"Легковое авто {item.Name} доехало до финиша!!!");
                    };
                    (item as PassengerCar).Crash += () =>
                    {
                        WriteLine($"Легковое авто  {item.Name} сломалось!!!");
                        item.carCrash = true;
                    };
                }

                //для грузовиков
                if (item.GetType().Equals(typeof(CargoCar)))
                {
                    (item as CargoCar).Finish += () =>
                    {
                        WriteLine($"Грузовое авто {item.Name} доехало до финиша!!!");
                    };
                    (item as CargoCar).Crash += () =>
                    {
                        WriteLine($"Грузовое авто  {item.Name} сломалось!!!");
                        item.carCrash = true;
                    };
                }

                //для автобусов
                if (item.GetType().Equals(typeof(Bus)))
                {
                    (item as Bus).Finish += () =>
                    {
                        WriteLine($"Автобус {item.Name} доехал до финиша!!!");
                    };
                    (item as Bus).Crash += () =>
                    {
                        WriteLine($"Автобус  {item.Name} сломался!!!");
                        item.carCrash = true;
                    };
                }
            }
            
            //формируем верхнюю строчку отчёта по гонке, содержащую названия всех автомобилей
            Write("seconds\t\t");
            foreach (var item in _race)
            {
                Write(item.Name + "\t\t");
            }

            Write("\n________________");
            foreach (var item in _race)
            {
                Write("________________");
            }
            WriteLine();

            int countSec = 1; //следующая строчка отчёта начинается с первой секунды, с каждым шагом увеличивается на одну
            int countOfCrash = 0; //количество поломок. Если сравнивается с количеством участников, гонка прекращается
            int countOfFinish = 0; //количество финишировавших. Если >0, гонка прекращается
            //string winner = "";
            //int winnerTime = 0;
            do
            {
                //печать очередной строки
                Write("{0}\t\t", countSec++);
                foreach (var item in _race)
                {
                    Write(item.DrivenDistance + "\t\t");
                }
                WriteLine();

                //для каждого экземпляра из списка участников гонки вызываем метод Drive, проверяя срабаывание событий Finish и Crash
                foreach (var item in _race)
                {
                    if (!item.carCrash)
                    {
                        item.Drive();
                        if (item.carCrash)
                        {
                            countOfCrash++;
                        }
                        
                    }
                    if (item.DrivenDistance >= 100)
                    {
                        countOfFinish++;
                        WriteLine($"Гонку выиграл {item.Name} на {countSec}-й секунде!"); 
                        Winner winnerOfRace = new Winner(item.Name, countSec); //создаем объект виннер (победитель) гонки
                        winnerOfRace.WinnerLog(); //записываем победу в лог по имени виннера
                        Top10Winners(); // считываем из файла список топ10 победителей
                        AddWinnerToTOP(winnerOfRace); //добавляем в список виннера гонки, если он туда помещается
                        Winner.Top10WinnersToFile(top10winners); //всех топов перезаписываем в файл.
                        break;
                    }
                }
                
                //останавливаем цикл do-while, когда кто-нибудь финишировал, либо когда все сломались
                if (countOfFinish > 0)
                {
                    break;
                }
                if (countOfCrash == _race.Count()) 
                {
                    WriteLine("Никто не сумел добраться до финиша...");
                    break; 
                }
            } while (true);
        }

        /// <summary>
        /// список ТОП10 победителей гонок, каждый элемент содержит ключ - позицию в рейтинге и значение - дату гонки, потраченное время и название автомобиля
        /// </summary>
        SortedList<int, Winner> top10winners = new SortedList<int, Winner>(10) { };

        /// <summary>
        /// Добавление победителей в список <c>top10winners</c> из файла top10winners.txt.
        /// </summary>
        public void Top10Winners()
        {
            string filePath = "top10winners.txt";
            string[] lines = File.ReadAllLines(filePath); //считываем строки из файла в массив
            foreach (string s in lines)
            {
                //WriteLine($"{s}\n"); //проверяем, что считалось
                string[] words = s.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries); //разбиваем строку по табуляции
                //foreach (string w in words) //проверяем, как разбилось
                //{
                //    WriteLine(w);
                //}
                
                top10winners.Add(int.Parse(words[0]), new Winner(DateTime.Parse(words[1]), int.Parse(words[2]), words[3])); //добавляем элемент списка
            }
            //foreach(var t in top10winners) //проверяем, как сохранился список
            //{
            //    WriteLine($"{t.Key} - {t.Value.ToString()}");
            //}
        }

        /// <summary>
        /// Добавление победителя гонки в список рейтинга
        /// </summary>
        /// <param name="winner">Победитель прошедшей гонки</param>
        public void AddWinnerToTOP(Winner winner)
        {
            int wTime = winner._winnerTime;
            int position = top10winners.Count;
            foreach (var t in top10winners) //ищем, в какое место списка поместить нового виннера
            {
                if(t.Value._winnerTime >= wTime)
                {
                    position = t.Key;
                }
            }
            //WriteLine($"position = {position}"); //отладочная информация о позиции виннера
            if(position < top10winners.Count)
            {
                for(int i = top10winners.Count; i > position; i--)
                {
                    top10winners[i] = top10winners[i - 1];
                }
                top10winners[position] = winner;
            }
            else
            {
                top10winners.Add(position, winner);
            }
            do //удаляем лишние элементы
            {
                if (top10winners.Count > 10) { top10winners.RemoveAt(10); }
            } while (top10winners.Count > 10);

            //foreach (var t in top10winners) //проверяем, как сохранился список
            //{
            //    WriteLine($"{t.Key} - {t.Value.ToString()}");
            //}
        }
    }
    /// <summary>
    /// Класс <c>Winner</c> победитель гонки
    /// </summary>
    public class Winner
    {
        public readonly string _winner = "";
        public readonly int _winnerTime = 0;
        public readonly DateTime _date = DateTime.Now;
        /// <summary>
        /// Конструктор, принимающий только название автомобиля и время, за которое он проехал. Дата гонки при этом устанавливается текущая. Используется для новых победителей.
        /// </summary>
        /// <param name="winner">используется для записи названия автомобиля, тип string</param>
        /// <param name="winnerTime">число секунд, за которые автомобиль финишировал</param>
        public Winner(string winner, int winnerTime)
        {
            _winner = winner;
            _winnerTime = winnerTime;
        }
        /// <summary>
        /// Конструктор, принимающий дату гонки, затраченное время и название автомобиля. Используется для восстановления списка победителей из файла
        /// </summary>
        /// <param name="date">Дата гонки</param>
        /// <param name="winnerTime">Затраченное время (в секундах)</param>
        /// <param name="winner">Название автомобиля - победителя гонки</param>
        public Winner(DateTime date, int winnerTime, string winner)
        {
            _winner = winner;
            _winnerTime = winnerTime;
            _date = date;
        }
        /// <summary>
        /// перегрузка вывода
        /// </summary>
        /// <returns>возвращает строку с данными победителя для записи в список и логи</returns>
        public override string ToString()
        {
            return $"{_date}: {_winnerTime}sec - {_winner}.";
        }
        /// <summary>
        /// Создаёт либо дописывает в лог-файл победителя его ноувю победу. Получается список побед конкретного автомобиля.
        /// </summary>
        public void WinnerLog()
        {
            string filePath = _winner +"_log.txt";
            using (FileStream fs = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    sw.WriteLine($"{_date}\t{_winnerTime}\t{_winner}");
                    WriteLine("Data recorded!");
                }
            }
        }
        /// <summary>
        /// Записывает рейтинг победителей из сортированного списка в файл
        /// </summary>
        /// <param name="top10winners">Сортированный список победителей гонок</param>
        public static void Top10WinnersToFile(SortedList<int, Winner> top10winners)
        {
            string filePath = "top10winners.txt";

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {
                    foreach (var t in top10winners) 
                    {
                        sw.WriteLine($"{t.Key}\t{t.Value._date}\t{t.Value._winnerTime}\t{t.Value._winner}");
                    }
                    WriteLine("Data recorded!");
                }
            }
        }
    }
    /// <summary>
    /// Реализация создания набора автомобилей для гонки
    /// </summary>
    class Program
    {
        /// <summary>
        /// Вспомогательный метод проверки выбранного пункта меню, проверяет, что пользователь ввёл допустимое значение, иначе с помощь рекурсии вызывается повторно
        /// </summary>
        /// <param name="c">принимает выбранный пункт меню</param>
        /// <param name="i">принимает общее число пунктов меню</param>
        /// <returns>возвращает выбранный пользователем допустимый номер пункта меню</returns>
        static int yesNo(int c, int i )
        {
            bool mark = false;
            for(int n=0; n<i; n++)
            {
                if (c == n)
                {
                    mark = true;
                    break;
                }
            }
            if (!mark)
            {
                WriteLine("Вы нажали что-то не то. Попробуйте ещё раз.");
                c = int.Parse(ReadLine());
                yesNo(c, i);
            }
            return c;
        }
        /// <summary>
        /// Метод, непосредственно создающий список участников предстоящей гонки
        /// </summary>
        /// <returns>возвращает список автомобилей, каждый автомобиль имеет название и тип</returns>
        static List<Car> RaceMembers()
        {
            List<Car> race = new List<Car> { };

            do
            {
                Write("Добавить участника гонки? 1 - да, 0 -нет: ");
                int c = yesNo(int.Parse(ReadLine()), 2);
                if (c == 1)
                {
                    Write("Введите название: ");
                    string memberName = ReadLine();
                    Write("Выберите тип авто: 1 - спорткар, 2 - легковое авто, 3 - грузовое авто, 4 - автобус. Ваш выбор: ");
                    int carType = int.Parse(ReadLine());
                    switch (carType)
                    {
                        case 1:
                            race.Add(new SportCar() { Name = memberName });
                            break;
                        case 2:
                            race.Add(new PassengerCar() { Name = memberName });
                            break;
                        case 3:
                            race.Add(new CargoCar() { Name = memberName });
                            break;
                        case 4:
                            race.Add(new Bus() { Name = memberName });
                            break;
                        default:
                            WriteLine($"Вы выбрали несуществующий тип. Участник {memberName} не зарегистрирован! Попробуйте ещё раз."); 
                            break;
                    }
                }
                else
                {
                    if(race.Count == 1)
                    {
                        WriteLine($"Вы добавили только одного участника. Для гонки нужны минимум двое.");
                    }
                    else if (race.Count == 0)
                    {
                        Write("Вы не ввели ни одного участника. Хотите выйти в главное меню? 1 - да, 0 - нет. Ваш выбор: ");
                        c = yesNo(int.Parse(ReadLine()), 2);
                        if (c == 1)
                        {
                            return race;
                        }
                    }
                    else
                    {
                        return race;
                    }
                }
            } while (true);
        }
        static void Main(string[] args)
        {
            //List<Car> race = new List<Car>
            //{
            //    new SportCar() { Name = "BMW" }, new PassengerCar() { Name = "Жигули" }, new CargoCar() { Name = "КАМАЗ" }, new Bus() { Name = "Икарус" }, new Bus() { Name = "ЛиАЗ" }
            //};
            WriteLine("Игра \"Гонки\"");
            bool noExit = true;
            do
            {
                Write("\nВы можете создать гонку (1), посмотреть таблицу лидеров (2) либо выйти из игры (0). Введите число: ");
                int c = yesNo(int.Parse(ReadLine()), 3);
                switch (c)
                {
                    case 1:
                        List<Car> race = RaceMembers();
                        if (race.Count > 0)
                        {
                            Game game = new Game(race);
                            game.Start();
                        }
                        break;
                    case 2:
                        string filePath = "top10winners.txt";
                        string[] lines = File.ReadAllLines(filePath); //считываем строки из файла в массив
                        WriteLine();
                        foreach (string s in lines)
                        {
                            WriteLine($"{s}"); //выводим на экран
                        }
                            break;
                    case 0:
                        noExit = false;
                        break;
                    default:
                        WriteLine($"Что-то пошло не так...");
                        break;
                }
                

            } while (noExit);
            Write("\n\nPress any key...");
            ReadKey();
        }
    }
}
