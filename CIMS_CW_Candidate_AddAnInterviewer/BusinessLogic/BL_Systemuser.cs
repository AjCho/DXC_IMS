using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMS_CW001_Candidate.Business_Logic
{
    class BL_Systemuser
    {
        Helper.Helper help = new Helper.Helper();
        // Variables
        protected const string systemuser = "systemuser";
        public const string systemuserid = "systemuserid";
        protected const string dxc_businessarea = "dxc_businessarea";
        protected const string dxc_interviewertype = "dxc_interviewertype";
        
        //protected const string
        public EntityCollection GetSystemusersViaBusinessArea(ITracingService tracer, IOrganizationService service, int businessArea, int interviewerType)
        {
            try
            {
                tracer.Trace("GetSystemusersViaBusinessArea initiated 3 'Execute' !");
                string fetchXML = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
                                        <entity name='systemuser'>
                                        <attribute name='fullname' />
                                        <attribute name='businessunitid' />
                                        <attribute name='firstname' />
                                        <attribute name='middlename' />
                                        <attribute name='lastname' />
                                        <attribute name='dxc_businessarea' />
                                        <attribute name='dxc_interviewertype' />
                                        <attribute name='positionid' />
                                        <attribute name='systemuserid' />
                                        <order attribute='fullname' descending='false' />
                                        <filter type='and'>
                                            <condition attribute='dxc_businessarea' operator='eq' value='" + businessArea + @"' />
                                            <condition attribute='dxc_interviewertype' operator='eq' value='" + interviewerType + @"' />
                                        </filter>
                                        </entity>
                                    </fetch>";
                return service.RetrieveMultiple(new FetchExpression(fetchXML));
            }
            catch (Exception e)
            {
                tracer.Trace(help.UnsuccessfulTraceMsg("GetSystemusersViaBusinessArea"));
                tracer.Trace(e.Message);
                throw new InvalidPluginExecutionException(e.Message);
            }
            finally
            {
                tracer.Trace(help.SuccessfulTraceMsg("GetSystemusersViaBusinessArea"));
            }

        }







    }
}
