function QualifyLead() {
    // Set the convert to contact to yes (1).
    Xrm.Page.getAttribute("dxc_converttocontact").setValue(1);
    // Deactivate the Lead record.
    Xrm.Page.getAttribute("statecode").setValue(1);
    Xrm.Page.getAttribute("statuscode").setValue(2);

    SaveAndRefresh();
}

function DisqualifyLead() {
    //$webresource:MarketingSales/_imgs/ribbon/DisqualifyLead_16.png
    //$webresource:MarketingSales/_imgs/ribbon/DisqualifyLead_32.png
    // Set the convert to contact to no (2).
    Xrm.Page.getAttribute("dxc_converttocontact").setValue(2);
    // Reactivate the Lead record.
    Xrm.Page.getAttribute("statecode").setValue(0);
    Xrm.Page.getAttribute("statuscode").setValue(1);

    SaveAndRefresh();
}

function SaveAndRefresh() {
    //Needed to set form dirty to false explicitly as it is not done by platform
    Xrm.Page.data.setFormDirty(false);
    // Save and refresh the record.
    Xrm.Page.data.entity.save();
    Xrm.Page.data.refresh();
    //setTimeout(function () {
    //    window.parent.Xrm.Page.data.refresh();
    //}, 3000);
    
}