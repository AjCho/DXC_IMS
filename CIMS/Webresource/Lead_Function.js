function PopulateLeadFullName(executionContext) {
    var formContext = executionContext.getFormContext();
    // Get the value to concatinate upon setting up the value.
    var firstname = formContext.getAttribute("firstname").getValue();
    var middlename = formContext.getAttribute("middlename").getValue();
    var lastname = formContext.getAttribute("dxc_candidatelastname").getValue();
    // Concatinate the value to be passed on.
    let fullname = (firstname == null ? "" : firstname) + " "
        + (middlename == null ? "" : middlename) + " "
        + (lastname == null ? "" : lastname);
    // Set Value for Full Name.
    formContext.getAttribute("fullname").setValue(fullname);

}

function PopulateCompanyAddressField(executionContext) {
    var formContext = executionContext.getFormContext();
    r(function () {
        alert('DOM Ready!');
        setTimeout(function () {
            // Get the value for the address fields.
            var address_Main = window.parent.document.getElementById("Address_label");
            var address_Copied = formContext.getAttribute("address1_composite").getValue();
            var finalAddress = "";
            if (address_Main != null) {
                // Get the innerHTML in the label that came from the formview and then split it to get
                // the first main batch that consist of purely address.
                var address_Main_lblSplit1 = (address_Main.innerHTML).split('</div>');
                // Get the 2nd batch wherein it contains the address and concatinate it properly.
                var address_Main_lblSplit2 = address_Main_lblSplit1[1].split('<br>');

                for (var i = 0; i < address_Main_lblSplit2.length; i++) {
                    finalAddress += (address_Main_lblSplit2[i] + " ");
                    CheckAndAssignAddressFieldMapping(executionContext, address_Main_lblSplit2.length, i, address_Main_lblSplit2[i]);
                }
                console.log(finalAddress);

                // Set the final address. 
                formContext.getAttribute("address1_composite").setValue(finalAddress);

            }
        }, 3000);

    });
}
function CheckAndAssignAddressFieldMapping(executionContext, maxLoopValue, currentIndex, value) {
    // Since we make some hierarchy of address 1 composite a "business required" field, we can now insert it in this order:
    //      address1_line1 = "business required"
    //      address1_line2 = "NOT business required"
    //      address1_line3 = "NOT business required"
    //      address1_city = "business required"
    //      address1_stateorprovince = "NOT business required"
    //      address1_postalcode = "business required"
    //      address1_country = "business required"
    var address1_line1 = "address1_line1";
    var address1_line2 = "address1_line2";
    var address1_line3 = "address1_line3";
    var address1_city = "address1_city";
    var address1_stateorprovince = "address1_stateorprovince";
    var address1_postalcode = "address1_postalcode";
    var address1_country = "address1_country";
    // We now need to check the max value of the loop to check, to map it correctly. (value starts as 0).
    // Minimum loop value is 3, and maximum of 5.
    if (maxLoopValue == 3) {
        if (currentIndex == 0)
            MapAddressDetail(executionContext, address1_line1, value);
        if (currentIndex == 1)
            MapAddressDetail(executionContext, address1_city, value);
        if (currentIndex == 2)
            MapAddressDetail(executionContext, address1_postalcode, value);
        if (currentIndex == 3)
            MapAddressDetail(executionContext, address1_country, value);
    } else if (maxLoopValue > 3 && maxLoopValue < 6) {

    }

}

/// This function is used to map the address to the address composite field.
function MapAddressDetail(executionContext, schemaName, value) {
    var formContext = executionContext.getFormContext();
    // Use this to map the address in address 1 composite field.
    //formContext.getControl("address1_composite_compositionLinkControl_" + schemaName).setLabel(value);
    formContext.getControl("address1_composite_compositionLinkControl_" + schemaName).setValue(value);

}


/// Functions to check if page ready.
function ready(callback) {
    // in case the document is already rendered
    if (document.readyState != 'loading') callback();
    // modern browsers
    else if (document.addEventListener) document.addEventListener('DOMContentLoaded', callback);
    // IE <= 8
    else document.attachEvent('onreadystatechange', function () {
        if (document.readyState == 'complete') callback();
    });
}

/// and here's the trick (works everywhere) for page ready.
function r(f) { /in/.test(document.readyState) ? setTimeout('r(' + f + ')', 9) : f() }

