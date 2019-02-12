function ViewNotes(executionContext) {
    var id = "e0b45a9a-bb23-e911-a985-000d3aa2c251";
    var formContext = executionContext.getFormContext();
    /// Note: openFileOptions - openMode: Specify 1 to open; 2 to save.
    /// If you do not specify this parameter, by default 1(open) is passed.
    /// This parameter is only supported on Unified Interface.
    var openFileOptions = {
        openMode: 2
    };
    var file = {
        //openInNewWindow: true,
        //entityName: "annotation",
        //entityId: id
        fileContent: "Tm90ZTogd2UgY2FuIHVzZSBsZWFkcyA+IG9wcG9ydHVuaXR5ID4gY29udGFjdA0KTGVhZHM6DQpUeXBlIChtc2R5bl9vcmRlcnR5cGUpID0gY2hhbmdlIHRvIG9wdGlvbmFsIC0gd2lsbCBub3QgdXNlDQpUb3BpYyAoc3ViamVjdCkgPSBjaGFuZ2UgdG8gb3B0aW9uYWwgLSB3aWxsIG5vdCB1c2UNCmNoYW5nZSBsYWJlbCBvZiAibmFtZSIgdG8gImNhbmRpZGF0ZSBuYW1lIi4NCmNoYW5nZSBsYWJlbCBmb3IgImpvYiB0aXRsZSIgdG8gIlBvc2l0aW9uL1RhbGVvIE51bWJlciINCmR4Y19BY2NvdW50dG9iZWFwcGxpZWRmb3IgPSBsb29rdXAgZm9yIGFjY291bnQuDQoNCg0KDQoNCg0K",
        fileName: "Resume_CV.txt",
        fileSize: 330,
        mimeType: "text/plain"
    };
    //if (annotationID != null)
    //formContext.Utility.openEntityForm("annotation", id);
    //Xrm.Navigation.openForm(windowOptions);
    Xrm.Navigation.openFile(file, 1);
}




