using System;
using UnityEngine;

namespace Utilities
{
    public class TimeManager : MonoBehaviour,IService
    {
        [SerializeField] private int minutesPerTick;
        [SerializeField] private float timeBetweenTick;
        [Header("Start time settings")]
        [SerializeField,Range(1,28)] private int startDate = 1;
        [SerializeField] private Season startSeason = (Season)1;
        [SerializeField, Range(2000,2030)] private int startYear = 2000;
        [SerializeField,Range(0,23)] private int startHour = 12;
        public static Action<DateTime> OnDateTimeChanged;
        public static DateTime DateTime;
        public int MinutesPerTick => minutesPerTick;
        private int startMinutes = 0;
        private float currentTime;
        public bool IsPaused { get; private set; }

        public void Init()
        {
            DateTime = new DateTime(startDate, (int)startSeason, startYear, startHour, startMinutes);
            IsPaused = false;
        }

        private void Start()
        {
            OnDateTimeChanged?.Invoke(DateTime);
        }

        private void Update()
        {
            if(!IsPaused)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= timeBetweenTick)
                {
                    currentTime = 0;
                    DateTime.AddMinutes(minutesPerTick);
                    OnDateTimeChanged?.Invoke(DateTime);
                }
            }
        }

        public float TicksCountInDay()
        {
            return 24 * 60 / (float)minutesPerTick * timeBetweenTick;
        }

        public void PauseTime()
        {
            IsPaused = true;
        }

        public void ContinueTime()
        {
            IsPaused = false;
        }
    }

    [Serializable]
    public struct DateTime
    {
        [SerializeField] private Season season;
        [SerializeField] private int date;
        [SerializeField] private int year;
        [SerializeField] private int hour;
        [SerializeField] private int minutes;
        private Days day;
        public Season Season => season;
        public Days Day => day;
        public int Date => date;
        public int TotalMinutes => hour*60 + minutes;
        public int Hour => hour;
        public int Minute => minutes;

        public DateTime(int date, int season, int year, int hour, int minutes)
        {
            if (date <= 0) date = 1;
            this.date = date;
            this.day = (Days)(date % 7);
            if (day == 0) day = (Days)7;
            this.season = (Season)(season);
            this.year = year;
            this.hour = hour;
            this.minutes = minutes;
            CalculateNormalDate();
        }

        public DateTime(int date, int hour, int minutes)
        {
            if (date <= 0) date = 1;
            this.date = date;
            this.day = (Days)(date % 7);
            if (day == 0) day = (Days)7;
            this.season = (Season)0;
            this.year = 0;
            this.hour = hour;
            this.minutes = minutes;
            CalculateNormalDate();
        }

        private void CalculateNormalDate()
        {
            int tmpMinutes = this.minutes - (this.minutes / 60)*60;
            int tmpHours = this.hour + (this.minutes / 60);
            int tmpDate = this.date + tmpHours / 24;
            tmpHours -= (tmpHours / 24) * 24;
            int tmpSeason = (int)this.season + tmpDate / 28;
            tmpDate -= (tmpDate / 28) * 28;
            int tmpYear = this.year + tmpSeason/4;
            tmpSeason -= (tmpSeason/4)*4;

            this.minutes = tmpMinutes;
            this.hour = tmpHours;
            this.date = tmpDate;
            this.day = (Days)(this.date/ 7);
            this.season = (Season)tmpSeason;
            this.year = tmpYear;
        }

        public void AddMinutes(int minutesToAdd)
        {
            if (minutes + minutesToAdd >= 60)
            {
                minutes = (minutes + minutesToAdd) % 60;
                AddHour();
            }
            else
            {
                minutes += minutesToAdd;
            }
        }

        private void AddHour()
        {
            if (hour + 1 == 24)
            {
                hour = 0;
                AddDate();
            }
            else hour++;
        }

        private void AddDate()
        {
            if (day + 1 > (Days)7)
            {
                day = (Days)1;
            }
            else
            {
                day++;
            }

            if (date + 1 == 29)
            {
                date = 1;
                ChangeSeason();
            }
            else date++;
        }

        private void ChangeSeason()
        {
            if (season + 1 > (Season)4)
            {
                season = (Season)1;
                AddYear();
            }
            else
            {
                season++;
            }
        }

        private void AddYear()
        {
            year++;
        }

        public string TimeToString()
        {
            string AmPm = hour == 0 || hour < 12 ? "AM" : "PM";
            return $"{hour.ToString("D2")}:{minutes.ToString("D2")} {AmPm}";
        }

        public static DateTime operator +(DateTime a, DateTime b)
        {
            return new DateTime(a.date + b.date, (int)a.season + (int)b.season, a.year + b.year, a.hour + b.hour, a.minutes + b.minutes);
        }

        public static DateTime operator -(DateTime a)
        {
            return new DateTime(-a.date, (int)a.season, -a.year, -a.hour, -a.minutes);
        }

        public int DateToMinutes()
        {
            int minutesToReturn = year * 4 * 28 * 24 * 60 + date * 24 * 60 + hour * 60 + minutes;
            return minutesToReturn;
        }
    }

    [Serializable]
    public enum Season
    {
        Весна = 1,
        Лето = 2,
        Осень = 3,
        Зима = 4,
    }

    [Serializable]
    public enum Days
    {
        Понедельник = 1,
        Вторник = 2,
        Среда = 3,
        Четверг = 4,
        Пятница = 5,
        Суббота = 6,
        Воскресение = 7,
    }
}
