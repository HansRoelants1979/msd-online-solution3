﻿using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Tc.Crm.CustomWorkflowSteps.GetUsersStore.Service;

namespace Tc.Crm.CustomWorkflowSteps.GetUsersStore
{
    public class GetUsersStoreActivity : CodeActivity
    {
        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService trace = executionContext.GetExtension<ITracingService>();
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

            try
            {
                var getUserStoreService = new GetUserStoreService();
                trace.Trace("getting store by user");
                var store = getUserStoreService.GetStoreByUser(User.Get<EntityReference>(executionContext),
                    service, trace);
                if (store != null)
                {
                    executionContext.SetValue(Store, store);
                }
                else
                {
                    trace.Trace("store is null");
                }
            }
            catch (FaultException<OrganizationServiceFault> ex)
            {
                throw new InvalidPluginExecutionException(ex.ToString());
            }
            catch (TimeoutException ex)
            {
                throw new InvalidPluginExecutionException(ex.ToString());
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.ToString());
            }
        }
        [Input("User")]
        [ReferenceTarget("systemuser")]
        public InArgument<EntityReference> User { get; set; }

        [Output("Store")]
        [ReferenceTarget("tc_store")]
        public OutArgument<EntityReference> Store { get; set; }
    }
}
