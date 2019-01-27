let Assessment = "Assessment"; 
let CandidateStatus = "Additional_Notes";
function AssessmentSectionVisibility(executionContext) {
    var formContext = executionContext.getFormContext();
    var dxc_status = formContext.getAttribute("dxc_status").getValue();
    var dxc_interviewdate = formContext.getAttribute("dxc_interviewdate").getValue();
    var createdon = formContext.getAttribute("createdon").getValue();
    console.log("createdon = " + createdon);
    if (dxc_status != null && dxc_interviewdate != null) {
        // If dxc_status == 2 means it is confirmed, 4 is Interview Complete, 7 is Candidate Withdrew
        if ((dxc_status == 2 || dxc_status == 4 || dxc_status == 7) && createdon != null) {
            // Show the tab
            ShowTab(executionContext, Assessment);
            ShowTab(executionContext, CandidateStatus);
        } else {
            // Hide the tab
            HideTab(executionContext, Assessment);
            HideTab(executionContext, CandidateStatus);
        }
    } else {
        // Default Hide the tab
        HideTab(executionContext, Assessment);
        HideTab(executionContext, CandidateStatus);
    }
}

function HideTab(executionContext, tabName) {
    var formContext = executionContext.getFormContext();
    // Hide the tab
    formContext.ui.tabs.get(tabName, tabName, false).setVisible(false);
}

function ShowTab(executionContext, tabName) {
    var formContext = executionContext.getFormContext();
    // Show the tab
    formContext.ui.tabs.get(tabName, tabName, false).setVisible(true);
}

