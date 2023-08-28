using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.ViewModels
{
    public class GroupedDailyViewModel
    {
        [Display(Name = "Day")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy }")]
        [JsonProperty("day")]
        public DateTime Day { get; set; }

        [Display(Name = "DT")]
        [JsonProperty("downTime")]
        public TimeSpan DownTime { get; set; }

        [Display(Name = "DTOB")]
        [JsonProperty("downTimeWithOverBreak")]
        public TimeSpan DownTimeWithOverBreak { get; set; }

        [Display(Name = "OB")]
        [JsonProperty("overBreak")]
        public TimeSpan OverBreak { get; set; }

        public double DownTimeFormated => Math.Round(DownTimeWithOverBreak.TotalMinutes);

        public string DateFormated
        {
            get
            {
                var date = Day;
                var sufix = (date.Day % 10 == 1 && date.Day % 100 != 11) ? "st"
                : (date.Day % 10 == 2 && date.Day % 100 != 12) ? "nd"
                : (date.Day % 10 == 3 && date.Day % 100 != 13) ? "rd"
                : "th";

                return date.ToString("ddd MMM dd ").Trim('.') + sufix;
            }
        }

        public string DayFormated => Day.ToShortDateString();
    }
}