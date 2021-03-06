﻿using JsonPatch;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tc.Crm.Service.Filters;
using Tc.Crm.Service.Models;
using Tc.Crm.Service.Services;
using System.Linq;
using Tc.Crm.Service.Constants;
namespace Tc.Crm.Service.Controllers
{
    [RequireHttps]
    public class CustomerController : ApiController
    {

        ICustomerService customerService;
        ICrmService crmService;
        IPatchParameterService parameterService;

        public CustomerController(ICustomerService customerService, ICrmService crmService, IPatchParameterService parameterService)
        {
            this.customerService = customerService;
            this.crmService = crmService;
            this.parameterService = parameterService;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [Route("api/customers/customer")]
        [HttpPost]
        [JsonWebTokenAuthorize]
        public HttpResponseMessage Create(CustomerInformation customerInfo)
        {
            try
            {
                var messages = customerService.Validate(customerInfo);
                if (messages != null && messages.Count != 0)
                {
                    var message = customerService.GetStringFrom(messages);
                    Trace.TraceWarning(message);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, message);
                }
                var customer = customerInfo.Customer;
                customerService.ResolveReferences(customer);
                var jsonData = JsonConvert.SerializeObject(customer);
                var response = customerService.Create(jsonData, crmService);
                if (response.Existing)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, Constants.Messages.CustomerCreationError);
                if (response.Create && !string.IsNullOrWhiteSpace(response.Id))
                    return Request.CreateResponse(HttpStatusCode.Created, response.Id);
                else
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
            catch (Exception ex)
            {
                Trace.TraceError("Unexpected error Customer.Create::Message:{0}||Trace:{1}", ex.Message, ex.StackTrace.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        [Route("api/customers/{gbgId}")]
        [HttpPatch]
        [JsonWebTokenAuthorize]
        public HttpResponseMessage Update(string gbgId, JsonPatchDocument<CustomerInformation> customerInfo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(gbgId)) return Request.CreateResponse(HttpStatusCode.BadRequest, Constants.Messages.CustomerIdIsNull);

                if (customerInfo == null) return Request.CreateResponse(HttpStatusCode.BadRequest, Constants.Messages.PayloadReadError);

                CustomerInformation customerInformation = ProcessPatchJsonToActualJson(customerInfo);
                if(customerInformation == null) return Request.CreateResponse(HttpStatusCode.BadRequest, Constants.Messages.PayloadReadError);
                customerService.ResolveReferences(customerInformation.Customer);

                var validationMessages = customerService.ValidateCustomerPatchRequest(customerInformation);

                if (validationMessages.Count > 0)
                {
                    var message = customerService.GetStringFrom(validationMessages);
                    Trace.TraceWarning(message);
                    return Request.CreateResponse(HttpStatusCode.BadRequest, message);
                }

                var response = customerService.Update(gbgId, customerInformation, crmService);

                if (response.Updated)
                    return Request.CreateResponse(HttpStatusCode.NoContent, response.Id);
                if (!response.Existing)
                    return Request.CreateResponse(HttpStatusCode.NotFound, Constants.Messages.CustomerUpdateError);
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                Trace.TraceError("Unexpected error Customer.Update::Message:{0}||Trace:{1}", ex.Message, ex.StackTrace.ToString());
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        private void AssignPatchParameters(JsonPatchDocument<CustomerInformation> customerInfo, 
            CustomerInformation customerInformation){
            customerInformation.Customer.PatchParameters = new List<string>();
            if (customerInformation.Customer.CustomerGeneral.CustomerType.ToString() == Attributes.CustomerType.Customer) {
                customerInfo.Operations.ForEach(item =>
                {
                    if (item.ParsedPath.Count() > 0)
                    {
                        var path = item.Path;
                        string attributeName = string.Empty;
                        if (parameterService.MapCustomer.TryGetValue(item.Path, out attributeName))
                            customerInformation.Customer.PatchParameters.Add(attributeName);
                    }
                });
            }
            else if (customerInformation.Customer.CustomerGeneral.CustomerType.ToString() == Attributes.CustomerType.Account){
                customerInfo.Operations.ForEach(item =>
                {
                    if (item.ParsedPath.Count() > 0)
                    {
                        var path = item.Path;
                        string attributeName = string.Empty;
                        if (parameterService.MapAccount.TryGetValue(item.Path, out attributeName))
                            customerInformation.Customer.PatchParameters.Add(attributeName);
                    }
                });
            }
        }

        private void InitializeCustomer(CustomerInformation customerInfo)
        {
            if (customerInfo == null) return;
            if (customerInfo.Customer == null) return;
            var customer = customerInfo.Customer;

            customer.CustomerIdentity = new CustomerIdentity();
            customer.CustomerIdentifier = new CustomerIdentifier();
            customer.CustomerGeneral = new CustomerGeneral();
            customer.Company = new Company();
            customer.Permissions = new Permission();
            customer.Additional = new Additional();
            customer.Address = new[] { new Address(), new Address(), new Address() };
            customer.Address1 = new Address();
            customer.Address2 = new Address();
            customer.Address3 = new Address();
            customer.Phone = new[] { new Phone(), new Phone(), new Phone() };
            customer.Phone1 = new Phone();
            customer.Phone2 = new Phone();
            customer.Phone3 = new Phone();
            customer.Email = new[] { new Email(), new Email(), new Email() };
            customer.Email1 = new Email();
            customer.Email2 = new Email();
            customer.Email3 = new Email();
        }

        private CustomerInformation ProcessPatchJsonToActualJson(JsonPatchDocument<CustomerInformation> customerInfo)
        {
            CustomerInformation customerInformation = new CustomerInformation();
            var customer = customerInformation.Customer;

            ResolveNullValueForEnumField(customerInfo);
            
            InitializeCustomer(customerInformation);
            try
            {
                customerInfo.ApplyUpdatesTo(customerInformation);
            }
            catch (Exception ex)
            {
                Trace.TraceError($"Message:{ex.Message} StackTrace:{ex.StackTrace.ToString()}");
                return null;
            }
            AssignPatchParameters(customerInfo, customerInformation);

            customer.Address[0] = customer.Address1;
            customer.Address[1] = customer.Address2;
            customer.Address[2] = customer.Address3;
            customer.Address1 = null;
            customer.Address2 = null;
            customer.Address3 = null;
            customer.Email[0] = customer.Email1;
            customer.Email[1] = customer.Email2;
            customer.Email[2] = customer.Email3;
            customer.Email1 = null;
            customer.Email2 = null;
            customer.Email3 = null;
            customer.Phone[0] = customer.Phone1;
            customer.Phone[1] = customer.Phone2;
            customer.Phone[2] = customer.Phone3;
            customer.Phone1 = null;
            customer.Phone2 = null;
            customer.Phone3 = null;
            customerInformation.Customer = customer;
            return customerInformation;
        }

        private void ResolveNullValueForEnumField(JsonPatchDocument<CustomerInformation> customerInfo)
        {
            foreach (var item in customerInfo.Operations)
            {
                if (item.ParsedPath.Last().Name.Equals("PhoneType"))
                {
                    PhoneType phoneType = PhoneType.NotSpecified;
                    var value = item.Value == null ? PhoneType.NotSpecified.ToString() : item.Value.ToString();
                    if (Enum.TryParse<PhoneType>(value, out phoneType))
                        item.Value = value;
                    else
                        item.Value = PhoneType.NotSpecified.ToString();
                }

                if (item.ParsedPath.Last().Name.Equals("EmailType"))
                {
                    EmailType emailType = EmailType.NotSpecified;
                    var value = item.Value == null ? EmailType.NotSpecified.ToString() : item.Value.ToString();
                    if (Enum.TryParse<EmailType>(value, out emailType))
                        item.Value = value;
                    else
                        item.Value = EmailType.NotSpecified.ToString();
                }

                if (item.ParsedPath.Last().Name.Equals("CustomerStatus"))
                {
                    CustomerStatus customerStatus = CustomerStatus.NotSpecified;
                    var value = item.Value == null ? CustomerStatus.NotSpecified.ToString() : item.Value.ToString();
                    if (Enum.TryParse<CustomerStatus>(value, out customerStatus))
                        item.Value = value;
                    else
                        item.Value = CustomerStatus.NotSpecified.ToString();
                }
                if (item.ParsedPath.Last().Name.Equals("Gender"))
                {
                    Gender gender = Gender.NotSpecified;
                    var value = item.Value == null ? Gender.NotSpecified.ToString() : item.Value.ToString();
                    if (Enum.TryParse<Gender>(value, out gender))
                        item.Value = value;
                    else
                        item.Value = Gender.NotSpecified.ToString();
                }

            }


        }

    }
}
