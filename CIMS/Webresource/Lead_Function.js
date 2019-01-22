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

function MapAddressDetail(executionContext) {
    var formContext = executionContext.getFormContext();




}
