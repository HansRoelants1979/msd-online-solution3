﻿using System;
using System.Collections.Generic;
using System.Linq;
using Tc.Crm.Common.Services;
using Tc.Crm.WebJob.DeallocateResortTeam.Models;

namespace Tc.Crm.WebJob.DeallocateResortTeam.Services
{
    public class DeallocateBookingService : IDeallocateBookingService
    {
        private ILogger logger;
        private IDeallocationService deallocationService;
        private IAllocationConfigurationService configurationService;

        public DeallocateBookingService(ILogger logger, IDeallocationService deallocationService, IAllocationConfigurationService configurationService)
        {
            this.logger = logger;
            this.deallocationService = deallocationService;
            this.configurationService = configurationService;
        }

        /// <summary>
        /// Entry point for booking deallocation service
        /// </summary>
        public void Run()
        {
            DeallocateBookings();
        }

        private void DeallocateBookings()
        {
            logger.LogInformation("Executing DeallocateBookings");

            IList<Guid> destinationGateways = GetDestinationGateways();
            if (destinationGateways == null)
            {
                logger.LogWarning("No Gateways found to process");
                throw new InvalidOperationException("No Gateways found to process");
            }

            logger.LogInformation("Processing for " + destinationGateways.Count.ToString() + " Destination Gateways");

            var request = deallocationService.FetchBookingsForDeallocation(
                new DeallocationRequest
                {
                    Date = DateTime.Now.Date.AddDays(-2),
                    Destination = destinationGateways,
                    UserRolesToAssignCase = configurationService.UserRolesToAssignCase,
                    TeamRolesToAssignCase = configurationService.TeamRolesToAssignCase
                });

            if (request == null || request.TotalItems == 0)
            {
                logger.LogWarning("No booking records found to process in CRM for the schedule.") ;
                return;
            }
            logger.LogInformation(string.Format("Found {0} Bookings, {1} Customers, {2} Cases to process", request.TotalBookings, request.TotalCustomers, request.TotalCases));
            deallocationService.DeallocateEntities(request, configurationService.ExecuteMultipleBatchSize);

            logger.LogInformation("Finished DeallocateBookings");
        }

        private IList<Guid> GetDestinationGateways()
        {
            if (configurationService.DestinationGatewayIds != null)
            {
                var ids = configurationService.DestinationGatewayIds.Split(',');
                if (ids.Length > 0)
                {
                    var result = ids.Where(id => !string.IsNullOrWhiteSpace(id)).Select(id => new Guid(id.Trim())).ToList();
                    return result.Count > 0 ? result : null;
                }
            }
            return null;
        }

        #region Displosable members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                DisposeObject(deallocationService);
                DisposeObject(logger);
                DisposeObject(configurationService);
            }

            disposed = true;
        }

        void DisposeObject(Object obj)
        {
            if (obj != null)
            {
                if (obj is IDisposable)
                    ((IDisposable)obj).Dispose();
                else
                    obj = null;
            }

        }

        #endregion
    }
}
