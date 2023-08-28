using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ditech.Portal.NET.Models.Logistics;

public class Trip
{
    [Display(Name = "Id")]
    public Guid Id { get; set; }

    [Display(Name = "Name")]
    [Required]
    public string Name { get; set; }

    [Display(Name = "Description")]
    [JsonProperty("description")]
    public string Description { get; set; }

    [Display(Name = "Exetrnal ID")]
    public string ExternalId { get; set; }

    [Display(Name = "Start destination Location ID")]
    public Guid StartLocationID { get; set; }

    //public virtual Location StartLocation { get; set; }

    [Display(Name = "Stop destination Location ID")]
    public Guid StopLocationID { get; set; }

    //public virtual Location StopLocation { get; set; }

    [Display(Name = "Color")]
    [Required]
    public Guid ColorId { get; set; }

    /// <summary>
    /// Actual start date recorded by the ELD or the driver or the admin
    /// </summary>
    [DataType(DataType.DateTime)]
    [Display(Name = ("Actual Start Date"))]
    public DateTime? ActualStartDate { get; set; }

    /// <summary>
    /// Actual end date recorded by the ELD or the driver or the admin
    /// </summary>
    [DataType(DataType.DateTime)]
    [Display(Name = ("Actual Arrival Date"))]
    public DateTime? ActualArrivalDate { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = ("Expected Start Date"))]
    [Required]
    public DateTime ExpectedStartDate { get; set; }

    [DataType(DataType.DateTime)]
    [Display(Name = ("Expected Arrival Date"))]
    [Required]
    public DateTime ExpectedArrivalDate { get; set; }

    public decimal? ExpectedDistanceKm { get; set; }

    public decimal? ExpectedDistanceMiles { get; set; }

    public decimal? ActualDistanceKm { get; set; }

    public decimal? ActualDistanceMiles { get; set; }

    [JsonProperty("currentStatus")]
    public string CurrentStatus { get; set; }
}