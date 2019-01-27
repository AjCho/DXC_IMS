using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIMS_Models;

namespace CIMS_CW001_Candidate.Business_Logic
{
    class BL_Interviewer
    {
        Helper.Helper help = new Helper.Helper();
        protected const string systemuser = "systemuser";
        protected const string dxc_interviewername = "dxc_interviewername";
        protected const string contact = "contact";
        protected const string dxc_candidate = "dxc_candidate";
        protected const string dxc_interviewer = "dxc_interviewer";
        protected const string dxc_name = "dxc_name";
        protected const string dxc_type = "dxc_type";
        protected const string dxc_systemuser_dxc_interviewer_InterviewerName = "dxc_systemuser_dxc_interviewer_InterviewerName";
        protected const string dxc_contact_dxc_interviewer_Candidate = "dxc_contact_dxc_interviewer_Candidate";

        public void CreateInterviewer(ITracingService tracer, IOrganizationService service, string name, int criteriaType, Guid systemuserID, Guid candidateID)
        {
            try
            {
                Entity newInterviewer = new Entity(dxc_interviewer);
                newInterviewer[dxc_name] = name;
                newInterviewer[dxc_interviewername] = new EntityReference(systemuser, systemuserID);
                newInterviewer[dxc_candidate] = new EntityReference(contact, candidateID);
                // 1 = initial interview
                newInterviewer[dxc_type] = new OptionSetValue(1);

                service.Create(newInterviewer);
                tracer.Trace(help.SuccessfulTraceMsg("CreateInterviewer, " + name));
            }
            catch (Exception e)
            {
                tracer.Trace(help.UnsuccessfulTraceMsg("CreateInterviewer, " + name));
                throw new InvalidPluginExecutionException(e.Message);
            }
            finally
            {
                tracer.Trace(help.SuccessfulTraceMsg("CreateInterviewer, " + name));
            }
        }


    }
}
