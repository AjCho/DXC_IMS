using CIMS_CustomWorkflow.Model;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIMS_CustomWorkflow.Business_Logic
{
    class BL_Interviewer
    {
        Helper.Helper help = new Helper.Helper();
        protected const string systemuser = "systemuser";
        protected const string contact = "contact";
        protected const string dxc_interviewer = "dxc_interviewer";
        protected const string dxc_name = "dxc_name";
        protected const string dxc_systemuser_dxc_interviewer_InterviewerName = "dxc_systemuser_dxc_interviewer_InterviewerName";
        protected const string dxc_contact_dxc_interviewer_Candidate = "dxc_contact_dxc_interviewer_Candidate";


        public void CreateInterviewer(ITracingService tracer, IOrganizationService service, string name, int criteriaType, Guid systemuserID, Guid candidateID)
        {
            try
            {
                Entity newInterviewer = new Entity(dxc_interviewer);
                newInterviewer[dxc_name] = name;
                newInterviewer[dxc_systemuser_dxc_interviewer_InterviewerName] = new EntityReference(systemuser, systemuserID);
                newInterviewer[dxc_contact_dxc_interviewer_Candidate] = new EntityReference(contact, candidateID);

                service.Create(newInterviewer);

                tracer.Trace(help.SuccessfulTraceMsg(name));
            }
            catch (Exception e)
            {
                tracer.Trace(help.UnsuccessfulTraceMsg(name));
                throw new InvalidPluginExecutionException(e.Message);
            }
        }


    }
}
