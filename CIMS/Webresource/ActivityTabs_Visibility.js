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



