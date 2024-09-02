using MRoom.Models;
using Microsoft.EntityFrameworkCore;

namespace MRoom.Data
{
    public class MRoomDbContext : DbContext
    {
        public MRoomDbContext(DbContextOptions<MRoomDbContext> options) : base(options)
        {

        }

        public DateTime ConvertUtcToIst()
        {
            DateTime utcNow = DateTime.UtcNow;
            TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime istNow = TimeZoneInfo.ConvertTimeFromUtc(utcNow, istZone);
            return istNow;
        }

        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<PropertyCity> PropertyCities { get; set; }
        public virtual DbSet<NearBy> NearBies { get; set; }
        public virtual DbSet<ColonyMuhalla> ColonyMuhallas { get; set; }
        public virtual DbSet<PropertyDetail> PropertyDetails { get; set; }
        public virtual DbSet<PropertyImage> PropertyImages { get; set; }
        public virtual DbSet<CountryMaster> CountryMasters { get; set; }
        public virtual DbSet<StateMaster> StateMasters { get; set; }
        public virtual DbSet<CityMaster> CityMasters { get; set; }
        public virtual DbSet<FurnishedType> FurnishedTypes { get; set; }
        public virtual DbSet<Stair> Stairs { get; set; }
        public virtual DbSet<Roof> Roofs { get; set; }
        public virtual DbSet<FirstPriority> FirstPriorities { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<CookingItem> CookingItems { get; set; }
        public virtual DbSet<WaterSupply> WaterSupplies { get; set; }
        public virtual DbSet<Lpg> Lpgs { get; set; }
        public virtual DbSet<Electricity> Electricities { get; set; }
        public virtual DbSet<PD_Near> PD_Nears { get; set; }
        public virtual DbSet<RailwayStation> RailwayStations { get; set; }
        public virtual DbSet<BusStand> BusStands { get; set; }
        public virtual DbSet<SchoolGov> SchoolGovs { get; set; }
        public virtual DbSet<SchoolPvt> SchoolPvts { get; set; }
        public virtual DbSet<BankGov> BankGovs { get; set; }
        public virtual DbSet<BankPvt> BankPvts { get; set; }
        public virtual DbSet<HospitalGov> HospitalGovs { get; set; }
        public virtual DbSet<HospitalPvt> HospitalPvts { get; set; }
        public virtual DbSet<Market> Markets { get; set; }
        public virtual DbSet<DmOffice> DmOffices { get; set; }
        public virtual DbSet<PublicTpt> PublicTpts { get; set; }
        public virtual DbSet<ToiletType> ToiletTypes { get; set; }
        public virtual DbSet<PropertyVariant> PropertyVariants { get; set; }
        public virtual DbSet<BHKType> BHKTypes { get; set; }
        public virtual DbSet<ParkingType> ParkingTypes { get; set; }
        public virtual DbSet<FloorType> FloorTypes { get; set; }
        public virtual DbSet<MemberType> MemberTypes { get; set; }
        public virtual DbSet<FoodType> FoodTypes { get; set; }
        public virtual DbSet<ParkingVisitor> ParkingVisitors { get; set; }
        public virtual DbSet<ElevatorType> ElevatorTypes { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
    }
}
