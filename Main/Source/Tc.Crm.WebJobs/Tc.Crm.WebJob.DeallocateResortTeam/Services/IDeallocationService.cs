﻿using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tc.Crm.WebJob.DeallocateResortTeam.Models;

namespace Tc.Crm.WebJob.DeallocateResortTeam.Services
{
    public interface IDeallocationService:IDisposable
    {
        IList<BookingDeallocationResponse> GetBookingAllocations(BookingDeallocationRequest bookingDeallocationRequest);
        IList<BookingDeallocationResponse> PrepareBookingDeallocation(EntityCollection bookingCollection);
        StringBuilder GetDestinationGateways(IList<Guid> destinationGateways);
        void ProcessBookingAllocations(IList<BookingDeallocationResortTeamRequest> bookingDeallocationResortTeamRequest);
        void AddBookingResortTeamRequest(BookingResortTeamRequest bookingResortTeamRequest, EntityCollection bookingTeamCollection);
        void AddCustomerResortTeamRequest(CustomerResortTeamRequest customerResortTeamRequest, EntityCollection bookingTeamCollection);
    }
}