using Microsoft.Xrm.Sdk;
using System;
using CIMS_CustomWorkflow.Model;
using Microsoft.Xrm.Sdk.Query;

namespace CIMS_CustomWorkflow.Business_Logic
{
    class BL_Systemuser
    {
        public EntityCollection GetAssesmentUser(ITracingService tracer, IOrganizationService service, Guid systemuserid)
        {
            string fetchXML = @"<fetch version='1.0' output-format='xml - platform' mapping='logical' distinct='false' >
                                <entity name = 'systemuser' >
                                    <attribute name = 'fullname' />
                                    <attribute name = 'businessunitid' />
                                    <attribute name = 'dxc_businessareas' />
                                    <attribute name = 'systemuserid' />
                                    <order attribute = 'fullname' descending = 'false' />
                                        <filter type = 'and' >
                                            <condition attribute = 'systemuserid' operator= 'eq' " + @" uitype = 'systemuser' value = '{" + systemuserid + @"}' />
                                        </filter >
                                 </entity >
                               </fetch > ";
            // < condition attribute = 'systemuserid' operator= 'eq' uiname = 'Aidonnel Cho' uitype = 'systemuser' value = '{B60F3950-E147-4F5A-AA5A-CD7F620052E4}' />
            return service.RetrieveMultiple(new FetchExpression(fetchXML));
        }
    }
}
