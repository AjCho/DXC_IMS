using Microsoft.Xrm.Sdk;
using System;
using CIMS_CustomWorkflow.Model;
using Microsoft.Xrm.Sdk.Query;

namespace CIMS_CustomWorkflow.Business_Logic
{
    class BL_AssessmentManager
    {
        // Variables CRM Logical Names
        protected const string dxc_assesmentmanager = "dxc_assesmentmanager";
        protected const string dxc_name = "dxc_name";
        protected const string dxc_criteriatype = "dxc_criteriatype";

        public EntityCollection GetAllAssessmentManagers(ITracingService tracer, IOrganizationService service)
        {
            try
            {
                EntityCollection assessmentManager = null;
                dxc_assesmentmanager dxc_assesmentmanagers = new dxc_assesmentmanager();

                QueryExpression query = new QueryExpression() { };
                query.EntityName = dxc_assesmentmanagers.LogicalName;
                query.ColumnSet = new ColumnSet(dxc_name, dxc_criteriatype);

                assessmentManager = service.RetrieveMultiple(query);
                return assessmentManager;
            }
            catch (Exception e)
            {
                tracer.Trace("e.Message");
                throw new InvalidPluginExecutionException(e.Message);
            }
        }

        public EntityCollection GetAssessmentManagersViaBusArea(ITracingService tracer, IOrganizationService service, Guid id)
        {
            string fetchXML = @"<fetch version='1.0' output-format='xml - platform' mapping='logical' distinct='false'>
                                <entity name = 'dxc_assesmentmanager' >
                                    <attribute name = 'dxc_assesmentmanagerid' />
                                    <attribute name = 'dxc_name' />
                                    <attribute name = 'dxc_businessarea' />
                                    <attribute name = 'dxc_criteriatype' />
                                    <order attribute = 'dxc_name' descending = 'false' />
                                    <filter type = 'and' >
                                        <filter type = 'or' >
                                            <condition attribute = 'dxc_businessarea' operator = 'null' />
                                            <condition attribute = 'dxc_businessarea' operator = 'eq' uitype = 'dxc_businessarea' value = '{" + id + @"}' />
                                        </filter>
                                    </filter>
                                </entity>
                               </fetch>";
            return service.RetrieveMultiple(new FetchExpression(fetchXML));
        }






    }
}
