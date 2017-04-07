﻿using System;
using System.Activities;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Messages;
using System.Collections.Generic;
using Tc.Crm.CustomWorkflowSteps.RetrieveParentRecord.Services;


namespace Tc.Crm.CustomWorkflowSteps
{
    public class RetrieveParentRecordActivity : CodeActivity
    {

        protected override void Execute(CodeActivityContext executionContext)
        {
            //Create the tracing service
            ITracingService tracingService = executionContext.GetExtension<ITracingService>();

            //Create the context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            try
            {
                EntityReference RetrivedEntity = null;
                var expression = string.Empty;

                expression = Expression.Get<string>(executionContext);
                RetrivedEntity = RetrieveRecordProcessHelper.RetrieveParentRecord(expression, service, context);
                if (RetrivedEntity != null)
                {
                    if (RetrivedEntity.LogicalName == "tc_locationoffice")
                        OfficeLocation.Set(executionContext, RetrivedEntity);
                    if (RetrivedEntity.LogicalName == "account")
                        Account.Set(executionContext, RetrivedEntity);
                    if (RetrivedEntity.LogicalName == "contact")
                        Contact.Set(executionContext, RetrivedEntity);
                    if (RetrivedEntity.LogicalName == "incident")
                        Case.Set(executionContext, RetrivedEntity);
                    if (RetrivedEntity.LogicalName == "tc_assistancerequest")
                        AssistanceRequest.Set(executionContext, RetrivedEntity);
                    if (RetrivedEntity.LogicalName == "tc_bookingaccommodation")
                        Accommdation.Set(executionContext, RetrivedEntity);
                    if (RetrivedEntity.LogicalName == "tc_caseline")
                        CaseLine.Set(executionContext, RetrivedEntity);
                    if (RetrivedEntity.LogicalName == "tc_hotel")
                        Hotel.Set(executionContext, RetrivedEntity);
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



        //starting entity should be the entity on which the workflow runs
        //incident||incident_customer_contacts;contact||contact_customer_accounts;account

        [Input("String ExpressionSample Eg:incident||incident_customer_contacts;contact||contact_customer_accounts;account;")]
        public InArgument<string> Expression { get; set; }

        //entity reference - string
        //optionset - string



        [Output("EntityReference OfficeLocation")]
        [ReferenceTarget("tc_locationoffice")]
        public OutArgument<EntityReference> OfficeLocation { get; set; }

        [Output("EntityReference Account")]
        [ReferenceTarget("account")]
        public OutArgument<EntityReference> Account { get; set; }

        [Output("EntityReference Contact")]
        [ReferenceTarget("contact")]
        public OutArgument<EntityReference> Contact { get; set; }

        [Output("EntityReference Case")]
        [ReferenceTarget("incident")]
        public OutArgument<EntityReference> Case { get; set; }

        [Output("EntityReference CaseLine")]
        [ReferenceTarget("tc_caseline")]
        public OutArgument<EntityReference> CaseLine { get; set; }

        [Output("EntityReference Accommdation")]
        [ReferenceTarget("tc_bookingaccommodation")]
        public OutArgument<EntityReference> Accommdation { get; set; }

        [Output("EntityReference AssistanceRequest")]
        [ReferenceTarget("tc_assistancerequest")]
        public OutArgument<EntityReference> AssistanceRequest { get; set; }

        [Output("EntityReference Hotel")]
        [ReferenceTarget("tc_hotel")]
        public OutArgument<EntityReference> Hotel { get; set; }


    }

}