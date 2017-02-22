﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Crm.Common.Constants
{

    public static class EntityName
    {

        public const string Booking = "tc_booking";
        public const string BookingAccommodation = "tc_bookingaccommodation";
        public const string CustomerBookingRole = "tc_customerbookingrole";
        public const string Hotel = "tc_hotel";
        public const string Team = "team";
        public const string User = "systemuser";
        public const string Account = "account";
        public const string Contact = "contact";
    }
    public static class AliasName
    {
        public const string AccommodationAliasName = "accommodation.";
        public const string HotelAliasName = "hotel.";
        public const string RoleAliasName = "role.";
    }
   
}

namespace Tc.Crm.Common.Constants.Attributes
{
    public static class Booking
    {
        public const string BookingId = "tc_bookingid";
        public const string Owner = "ownerid";
        public const string Name = "tc_name";
        public const string DepartureDate = "tc_departuredate";
        public const string DestinationId = "tc_destinationid";
        public const string ReturnDate = "tc_returndate";
    }

    
    public static class BookingAccommodation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Accommodationid")]
        public const string BookingAccommodationid = "tc_bookingaccommodationid";
        public const string BookingId = "tc_bookingid";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dateand")]
        public const string EndDateandTime = "tc_enddateandtime";
        public const string HotelId = "tc_hotelid";
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Dateand")]
        public const string StartDateandTime = "tc_startdateandtime";
        public const string Owner = "ownerid";

    }

    
    public static class CustomerBookingRole
    {
        public const string BookingId = "tc_bookingid";
        public const string Customer = "tc_customer";
        public const string Role = "tc_customerbookingrole";
        public const string CustomerBookingRoleId = "tc_customerbookingroleid";
        public const string Name = "tc_name";
    }

    
    public static class Hotel
    {

        public const string HotelId = "tc_hotelid";
        public const string LocationId = "tc_locationid";
        public const string MasterHotelId = "tc_masterhotelid";
        public const string Name = "tc_name";
        public const string SourceMarketHotelId = "tc_sourcemarkethotelid";
        public const string ResortTeam = "tc_teamid";
        public const string Owner = "ownerid";

    }
    
    
    public static class Team
    {
        public const string TeamId = "teamid";
        public const string Name = "name";
    }

    
    
    public static class Customer
    {
        public const string Owner = "ownerid";
    }
}