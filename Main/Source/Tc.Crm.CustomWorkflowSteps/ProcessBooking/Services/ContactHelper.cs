﻿using System;
using Microsoft.Xrm.Sdk;
using Tc.Crm.CustomWorkflowSteps.ProcessBooking.Models;

namespace Tc.Crm.CustomWorkflowSteps.ProcessBooking.Services
{
    public static class ContactHelper
    {
        public static Entity GetContactEntityForBookingPayload(Customer customer, ITracingService trace)
        {
            if (trace == null) throw new InvalidPluginExecutionException("Tracing service is null;");
            trace.Trace("Contact populate fields - start");
            if (customer == null) return null;


            Entity contact = null;
            if (customer.CustomerIdentifier == null || string.IsNullOrWhiteSpace(customer.CustomerIdentifier.CustomerId))
                contact = new Entity(EntityName.Contact);
            else
                contact = new Entity(EntityName.Contact, Attributes.Contact.SourceSystemId
                                            , customer.CustomerIdentifier.CustomerId);

            PopulateIdentityInformation(contact, customer.CustomerIdentity, trace);

            if (customer.Additional != null)
            {
                trace.Trace("Contact populate Additional details - start");
                contact[Attributes.Contact.Segment] = CommonXrm.GetSegment(customer.Additional.Segment);
                
                if (!string.IsNullOrWhiteSpace(customer.Additional.DateOfDeath))
                    contact[Attributes.Contact.DateOfDeath] = Convert.ToDateTime(customer.Additional.DateOfDeath);
                else
                    contact[Attributes.Contact.DateOfDeath] = null;
                trace.Trace("Contact populate Additional details - end");
            }

            PopulateAddress(contact, customer.Address, trace);
            PopulatePhone(contact, customer.Phone, trace);
            PopulateEmail(contact, customer.Email, trace);            
            contact[Attributes.Contact.SourceMarketId] = (customer.CustomerIdentifier!=null &&
                                                            !string.IsNullOrWhiteSpace(customer.CustomerIdentifier.SourceMarket))
                                                                ?new EntityReference(EntityName.Country
                                                                                    , new Guid(customer.CustomerIdentifier.SourceMarket)) 
                                                                : null;

            contact[Attributes.Contact.SourceSystemId] = (customer.CustomerIdentifier != null 
                                                            && !string.IsNullOrWhiteSpace(customer.CustomerIdentifier.CustomerId )) 
                                                                ?customer.CustomerIdentifier.CustomerId 
                                                                : string.Empty;
            if (customer.CustomerIdentifier != null)
            {
                if (!string.IsNullOrWhiteSpace(customer.Owner))
                    contact[Attributes.Booking.Owner] = new EntityReference(EntityName.Team, new Guid(customer.Owner));
            }
            
            if (customer.CustomerGeneral != null)
            {
                contact[Attributes.Contact.StateCode] = new OptionSetValue((int)Statecode.Active);
                contact[Attributes.Contact.StatusCode] = CommonXrm.GetCustomerStatus(customer.CustomerGeneral.CustomerStatus);
            }

            trace.Trace("Contact populate fields - end");

            return contact;
        }

        private static void PopulateEmail(Entity contact, Email[] emails, ITracingService trace)
        {
            trace.Trace("Contact populate email - start");
            if (trace == null) throw new InvalidPluginExecutionException("Tracing service is null;");
            if (emails == null || emails.Length == 0) { ClearEmailList(contact); return; }

            var email1 = emails[0];
            var email2 = emails.Length > 1 ? emails[1] : ClearEmail2(contact);
            var email3 = emails.Length > 2 ? emails[2] : ClearEmail3(contact);

            if (email1 == null) return;
            trace.Trace("email 1");
            contact[Attributes.Contact.EmailAddress1] = (!string.IsNullOrWhiteSpace(email1.Address )) ? email1.Address : string.Empty ;
            contact[Attributes.Contact.EmailAddress1Type] = CommonXrm.GetEmailType(email1.EmailType);
            if (email2 == null) return;

            trace.Trace("email 2");
            contact[Attributes.Contact.EmailAddress2] = (!string.IsNullOrWhiteSpace(email2.Address )) ? email2.Address:string.Empty;
            contact[Attributes.Contact.EmailAddress2Type] = CommonXrm.GetEmailType(email2.EmailType);

            trace.Trace("email 3");
            if (email3 == null) return;
            contact[Attributes.Contact.EmailAddress3] = (!string.IsNullOrWhiteSpace(email3.Address )) ? email3.Address:string.Empty;
            contact[Attributes.Contact.EmailAddress3Type] = CommonXrm.GetEmailType(email3.EmailType);
            trace.Trace("Contact populate email - end");

        }


        private static Email ClearEmailList(Entity contact)
        {
            ClearEmail1(contact);
            ClearEmail2(contact);
            ClearEmail3(contact);
            return null;
        }

        private static Email ClearEmail1(Entity contact)
        {
            contact[Attributes.Contact.EmailAddress1] = string.Empty;
            contact[Attributes.Contact.EmailAddress1Type] = null;
            return null;
        }

        private static Email ClearEmail2(Entity contact)
        {
            contact[Attributes.Contact.EmailAddress2] = string.Empty;
            contact[Attributes.Contact.EmailAddress2Type] = null;
            return null;
        }

        private static Email ClearEmail3(Entity contact)
        {
            contact[Attributes.Contact.EmailAddress3] = string.Empty;
            contact[Attributes.Contact.EmailAddress3Type] = null;
            return null;
        }

        private static void PopulatePhone(Entity contact, Phone[] phoneNumbers, ITracingService trace)
        {
            trace.Trace("Contact populate phone - start");
            if (trace == null) throw new InvalidPluginExecutionException("Tracing service is null;");
            if (contact == null) throw new InvalidPluginExecutionException("Populate Phone: contact entity is null.");
            if (phoneNumbers == null || phoneNumbers.Length == 0) { ClearPhoneList(contact); return; };

            var phone1 = phoneNumbers[0];
            var phone2 = phoneNumbers.Length > 1 ? phoneNumbers[1] : ClearPhone2(contact);
            var phone3 = phoneNumbers.Length > 2 ? phoneNumbers[2] : ClearPhone3(contact);

            trace.Trace("phone 1");
            if (phone1 == null) return;
            contact[Attributes.Contact.Telephone1Type] = CommonXrm.GetPhoneType(phone1.PhoneType);
            contact[Attributes.Contact.Telephone1] = (!string.IsNullOrWhiteSpace(phone1.Number )) ? phone1.Number:string.Empty ;

            trace.Trace("phone 2");
            if (phone2 == null) return;
            contact[Attributes.Contact.Telephone2Type] = CommonXrm.GetPhoneType(phone2.PhoneType);
            contact[Attributes.Contact.Telephone2] = (!string.IsNullOrWhiteSpace(phone2.Number )) ? phone2.Number : string.Empty;

            trace.Trace("phone 3");
            if (phone3 == null) return;
            contact[Attributes.Contact.Telephone3Type] = CommonXrm.GetPhoneType(phone3.PhoneType);
            contact[Attributes.Contact.Telephone3] = (!string.IsNullOrWhiteSpace(phone3.Number )) ? phone3.Number : string.Empty;

            trace.Trace("Contact populate phone - end");
        }

        private static void ClearPhoneList(Entity contact)
        {
            ClearPhone1(contact);
            ClearPhone2(contact);
            ClearPhone3(contact);
        }

        private static Phone ClearPhone1(Entity contact)
        {
            contact[Attributes.Contact.Telephone1Type] = null;
            contact[Attributes.Contact.Telephone1] = string.Empty;
            return null;
        }

        private static Phone ClearPhone2(Entity contact)
        {
            contact[Attributes.Contact.Telephone2Type] = null;
            contact[Attributes.Contact.Telephone2] = string.Empty;
            return null;
        }

        private static Phone ClearPhone3(Entity contact)
        {
            contact[Attributes.Contact.Telephone3Type] = null;
            contact[Attributes.Contact.Telephone3] = string.Empty;
            return null;
        }

        private static void PopulateIdentityInformation(Entity contact, CustomerIdentity identity, ITracingService trace)
        {
            trace.Trace("Contact populate idenity - start");
            if (trace == null) throw new InvalidPluginExecutionException("Tracing service is null;");
            if (identity == null) return;
            
            contact[Attributes.Contact.FirstName] = (!string.IsNullOrWhiteSpace(identity.FirstName )) ? identity.FirstName : string.Empty;
            contact[Attributes.Contact.MiddleName] = (!string.IsNullOrWhiteSpace(identity.MiddleName )) ? identity.MiddleName : string.Empty;
            contact[Attributes.Contact.LastName] = (!string.IsNullOrWhiteSpace(identity.LastName )) ? identity.LastName : string.Empty;
            contact[Attributes.Contact.Language] = CommonXrm.GetLanguage(identity.Language);                                                   
            contact[Attributes.Contact.Gender] = CommonXrm.GetGender(identity.Gender);
            contact[Attributes.Contact.AcademicTitle] = (!string.IsNullOrWhiteSpace(identity.AcademicTitle )) ? identity.AcademicTitle : string.Empty;
            contact[Attributes.Contact.Salutation] = CommonXrm.GetSalutation(identity.Salutation);
            contact[Attributes.Contact.Birthdate] = (!string.IsNullOrWhiteSpace(identity.Birthdate )) ? Convert.ToDateTime(identity.Birthdate) : (DateTime?)null;
            trace.Trace("Contact populate idenity - end");
        }

        private static void PopulateAddress(Entity contact, Address[] addresses, ITracingService trace)
        {
            trace.Trace("Contact populate address - start");
            if (trace == null) throw new InvalidPluginExecutionException("Tracing service is null;");
            if (addresses == null || addresses.Length == 0) { ClearAddress(contact); return; }
            var address1 = addresses[0];
            var address2 = addresses.Length > 1 ? addresses[1] : ClearAddress2(contact);

            trace.Trace("Address 1");
            if (address1 == null) return;
            contact[Attributes.Contact.Address1AdditionalInformation] = (!string.IsNullOrWhiteSpace(address1.AdditionalAddressInfo )) ? address1.AdditionalAddressInfo:string.Empty;
            contact[Attributes.Contact.Address1FlatOrUnitNumber] = (!string.IsNullOrWhiteSpace(address1.FlatNumberUnit ))? address1.FlatNumberUnit : string.Empty;
            contact[Attributes.Contact.Address1HouseNumberOrBuilding] = (!string.IsNullOrWhiteSpace(address1.HouseNumberBuilding )) ? address1.HouseNumberBuilding : string.Empty ;
            contact[Attributes.Contact.Address1Town] = (!string.IsNullOrWhiteSpace(address1.Town )) ? address1.Town : string.Empty;
            contact[Attributes.Contact.Address1CountryId] = (!string.IsNullOrWhiteSpace(address1.Country)) ?
                                                                             new EntityReference(EntityName.Country,
                                                                             new Guid(address1.Country)) : null;

            contact[Attributes.Contact.Address1County] = (!string.IsNullOrWhiteSpace(address1.County )) ? address1.County :string.Empty ;
            contact[Attributes.Contact.Address1PostalCode] = (!string.IsNullOrWhiteSpace(address1.PostalCode )) ? address1.PostalCode : string.Empty;
            contact[Attributes.Contact.Address1Street] = (!string.IsNullOrWhiteSpace(address1.Street)) ? address1.Street : string.Empty;

            trace.Trace("Address 2");
            if (address2 == null) return;
            contact[Attributes.Contact.Address2AdditionalInformation] = (!string.IsNullOrWhiteSpace(address2.AdditionalAddressInfo ))?address2.AdditionalAddressInfo:string.Empty;
            contact[Attributes.Contact.Address2FlatOrUnitNumber] = (!string.IsNullOrWhiteSpace(address2.FlatNumberUnit))?address2.FlatNumberUnit:string.Empty;
            contact[Attributes.Contact.Address2HouseNumberOrBuilding] = (!string.IsNullOrWhiteSpace(address2.HouseNumberBuilding ))?address2.HouseNumberBuilding:string.Empty;
            contact[Attributes.Contact.Address2Town] = (!string.IsNullOrWhiteSpace(address2.Town )) ? address2.Town:string.Empty;
            contact[Attributes.Contact.Address2CountryId] = (!string.IsNullOrWhiteSpace(address2.Country)) ?
                                                                             new EntityReference(EntityName.Country,
                                                                             new Guid(address2.Country)) : null;
            contact[Attributes.Contact.Address2County] = (!string.IsNullOrWhiteSpace(address2.County )) ? address2.County:string.Empty;
            contact[Attributes.Contact.Address2PostalCode] = (!string.IsNullOrWhiteSpace(address2.PostalCode )) ? address2.PostalCode :string.Empty;
            contact[Attributes.Contact.Address2Street] = (!string.IsNullOrWhiteSpace(address2.Street)) ? address2.Street : string.Empty;

            trace.Trace("Contact populate address - end");
        }

        private static Address ClearAddress(Entity contact)
        {
            ClearAddress1(contact);
            ClearAddress2(contact);
            return null;
        }

        private static Address ClearAddress1(Entity contact)
        {
            contact[Attributes.Contact.Address1AdditionalInformation] = string.Empty;
            contact[Attributes.Contact.Address1FlatOrUnitNumber] = string.Empty;
            contact[Attributes.Contact.Address1HouseNumberOrBuilding] = string.Empty;
            contact[Attributes.Contact.Address1Town] = string.Empty;
            contact[Attributes.Contact.Address1CountryId] = null;
            contact[Attributes.Contact.Address1County] = string.Empty;
            contact[Attributes.Contact.Address1PostalCode] = string.Empty;
            contact[Attributes.Contact.Address1Street] = string.Empty;
            return null;
        }

        private static Address ClearAddress2(Entity contact)
        {
            contact[Attributes.Contact.Address2AdditionalInformation] = string.Empty;
            contact[Attributes.Contact.Address2FlatOrUnitNumber] = string.Empty;
            contact[Attributes.Contact.Address2HouseNumberOrBuilding] = string.Empty;
            contact[Attributes.Contact.Address2Town] = string.Empty;
            contact[Attributes.Contact.Address2CountryId] = null;
            contact[Attributes.Contact.Address2County] = string.Empty;
            contact[Attributes.Contact.Address2PostalCode] = string.Empty;
            contact[Attributes.Contact.Address2Street] = string.Empty;
            return null;
        }
    }
}
