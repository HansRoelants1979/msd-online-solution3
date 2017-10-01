﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.Sdk;
using System;
using Tc.Crm.CustomWorkflowSteps.ProcessBooking.Models;
using Tc.Crm.CustomWorkflowSteps.ProcessCustomer.Services;
using Tc.Crm.UnitTests.CustomWorkFlowSteps;

namespace Tc.Crm.CustomWorkFlowSteps.ProcessCustomer.Services.Tests
{
    [TestClass()]
    public class SocialProfileHelperTests
    {
        TestTracingService trace;
        [TestInitialize()]
        public void Setup()
        {
            trace = new TestTracingService();
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidPluginExecutionException), "Tracing service is null.")]
        public void GetSocialProfileEntityFromPayload_TraceIsNull()
        {
            SocialProfileHelper.GetSocialProfileEntityFromPayload(new Customer(), new Guid(), null);
        }          

        [TestMethod()]
        [ExpectedException(typeof(InvalidPluginExecutionException), "Customer Social Profile is null.")]
        public void GetSocialProfileEntityFromPayload_SocialProfileIsNull()
        {
            Customer c = new Customer{Social = null};
            SocialProfileHelper.GetSocialProfileEntityFromPayload(c, new Guid(), trace);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidPluginExecutionException), "Customer Social Profile Length is null.")]
        public void GetSocialProfileEntityFromPayload_SocialProfileLengthIsNull()
        {
            Customer c = new Customer{Social = new Social[] { }};
            SocialProfileHelper.GetSocialProfileEntityFromPayload(c, new Guid(), trace);
        }

        [TestMethod()]        
        public void GetSocialProfileEntityFromPayload_SocialProfileProcess()
        {
            Customer c = new Customer {
                CustomerIdentifier = new CustomerIdentifier
                {
                    CustomerId = "1234"
                },
                Social = new Social[] {
                    new Social { Value = "dummy"}
                }
            };
            SocialProfileHelper.GetSocialProfileEntityFromPayload(c, new Guid(), trace);
        }
    }
}