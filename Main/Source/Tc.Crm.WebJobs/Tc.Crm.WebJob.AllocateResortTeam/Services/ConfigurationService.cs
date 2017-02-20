﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tc.Crm.WebJob.AllocateResortTeam.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Crm"].ConnectionString;
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string DepartureDateinNextXDays
        {
            get
            {
                return ConfigurationManager.AppSettings["DepartureDateinNextXDays"].ToString();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string DestinationGatewayIds
        {
            get
            {
                return ConfigurationManager.AppSettings["DestinationGatewayIds"].ToString();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}