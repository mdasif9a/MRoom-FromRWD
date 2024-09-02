using MRoom.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MRoom.Models
{
    public class PropertyDetail
    {
        [Key]
        public int Id { get; set; }
        public string? PropertyId { get; set; }
        [Required]
        public string? PropertyFor { get; set; }
        [Required]
        public int? UserId { get; set; }
        [Required]
        public int? PropertyTypeId { get; set; }
        [Required]
        public string? Size { get; set; }
        [Required]
        public string? PropertyVariantName { get; set; }
        [Required]
        public string? ToiletTypeName { get; set; }
        [Required]
        public string? ParkingTypeName { get; set; }
        [Required]
        public string? ParkingVisitorName { get; set; }
        [Required]
        public string? CC_TV { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? CountryName { get; set; }
        [Required]
        public string? StateName { get; set; }
        [Required]
        public string? CityName { get; set; }
        [Required]
        public string? ColonyName { get; set; }
        [Required]
        public string? ReligionName { get; set; }
        [Required]
        public string? FurnishedName { get; set; }
        [Required]
        public string? WaterName { get; set; }
        [Required]
        public string? LpgName { get; set; }
        [Required]
        public string? ElectricityName { get; set; }
        [Required]
        public string? StairName { get; set; }
        [Required]
        public string? RoofName { get; set; }
        public string? ElevatorName { get; set; }
        [Required]
        public string? FirstPrioName { get; set; }
        [Required]
        public string? CookingName { get; set; }
        public string? BankGovName { get; set; }
        public string? BankGovDis { get; set; }
        public string? BankPvtName { get; set; }
        public string? BankPvtDis { get; set; }
        public string? SchoolGovName { get; set; }
        public string? SchoolGovDis { get; set; }
        public string? SchoolPvtName { get; set; }
        public string? SchoolPvtDis { get; set; }
        public string? HospitalGovName { get; set; }
        public string? HospitalGovDis { get; set; }
        public string? HospitalPvtName { get; set; }
        public string? HospitalPvtDis { get; set; }
        public string? RailwayName { get; set; }
        public string? RailwayDis { get; set; }
        public string? BusName { get; set; }
        public string? BusDis { get; set; }
        public string? MarketName { get; set; }
        public string? MarketDis { get; set; }
        public string? DmOfficeName { get; set; }
        public string? DmOfficeDis { get; set; }
        public string? PublicTptName { get; set; }
        public string? PublicTptDis { get; set; }
        public string? TimeIn { get; set; }
        public string? TimeOut { get; set; }
        public string? NoOfMembers { get; set; }
        [Required]
        public string? MonthlyRent { get; set; }
        [Required]
        public string? SecurityDeposit { get; set; }
        [Required]
        public string? AgreementFees { get; set; }
        public string? AgreementTime { get; set; }
        public string? InVacantTime { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        [Required]
        public string? BHKTypeName { get; set; }
        [Required]
        public string? FloorTypeName { get; set; }
        [Required]
        public string? Zone { get; set; }

        public string? RoadName { get; set; }
        public string? LandMark { get; set; }
        public string? Image1 { get; set; }
        public string? Image2 { get; set; }
        public string? Image3 { get; set; }
        public string? Image4 { get; set; }
        public string? Image5 { get; set; }
        public string? Image6 { get; set; }
        [NotMapped]
        public List<int>? NearBies { get; set; }
    }

}