function PopulateAssessmentValue(executionContext) {
    var formContext = executionContext.getFormContext();
    var dxc_assessment = formContext.getAttribute("dxc_assessment").getValue();
    if (dxc_assessment != null) {
        formContext.getAttribute("dxc_hfassessmentvalue").setValue(dxc_assessment);
    }
}


