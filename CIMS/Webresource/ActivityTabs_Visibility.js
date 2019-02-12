/// This JS is used to hide or show the Activity Tabs. We just need to list the tabs needed to be hidden,
/// and then populate the "tabs" variable in this example the system will only show the "NOTES" Tab:
///     eg: var tabs = ["ASSISTANT", "POSTS", "ACTIVITIES", "ONENOTE"];
/// So far these are the list of the Activity Tabs depending on which tab will you going to show and which are not:
/// "ASSISTANT", "POSTS", "ACTIVITIES", "ONENOTE".
///     Created/Upgraded by: AJ Cho, 01/30/2019      ///

function HideActivities() {
    var tabs = ["ASSISTANT", "POSTS", "ACTIVITIES", "ONENOTE"];
    for (var tabsid = 0; tabsid < tabs.length; tabsid++) {
        HideTabs(tabs[tabsid]);
    }

}

function HideTabs(socialPaneType) {
    var ctrlElement = document.getElementById('header_notescontrol');
    if (ctrlElement == null) ctrlElement = window.parent.document.getElementById('header_notescontrol');
    if (ctrlElement == null) return;
    if (ctrlElement.children != null && ctrlElement.children.length > 0) {
        for (var ele = 0; ele < ctrlElement.children.length; ele++) {
            var ctrl = ctrlElement.children[ele];
            if (ctrl.title == socialPaneType) {
                ctrl.style.display = "none";
                if (ele + 1 < ctrlElement.children.length) { ctrlElement.children[ele + 1].click(); return; }
                else if (ele - 1 >= 0) {
                    ctrlElement.children[ele - 1].click();
                    return;
                }
            }
        }
    }
}



