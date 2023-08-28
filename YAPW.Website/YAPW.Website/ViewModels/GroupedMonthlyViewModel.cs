using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Ditech.Portal.NET.ViewModels;

public class GroupedMonthlyViewModel
{
    [Display(Name = "Month")]
    public int Month { get; set; }

    [Display(Name = "Down Time")]
    public string DownTime { get; set; }

    [Display(Name = "Down Time with over break")]
    public string DownTimeWithOverBreaks { get; set; }

    [Display(Name = "Over Break")]
    public TimeSpan OverBreaks { get; set; }

    //public double DownTimeFormated => Math.Round(TimeSpan.Parse(DownTimeWithOverBreak).TotalMinutes);
    public double DownTimeFormated => Math.Round(new TimeSpan(int.Parse(DownTimeWithOverBreaks.Split(':')[0]), int.Parse(DownTimeWithOverBreaks.Split(':')[1]), 0).TotalMinutes);
    //public TimeSpan date = new TimeSpan(int.Parse(DownTimeWithOverBreak.Split(':')[0]), int.Parse(DownTimeWithOverBreak.Split(':')[1]), 0);

    public string MonthFormated => new DateTime(2010, Month, Month).ToString("MMM", CultureInfo.InvariantCulture);
}
