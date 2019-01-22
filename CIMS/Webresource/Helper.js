function OnloadForHtml(linkID) {
    LoadTheLinks(linkID);


}

function LoadTheLinks(linkID) {
    var serverUrl = Xrm.Page.context.getClientUrl();
    var webresourceURL = "/WebResources/dxc_Style.css";
    console.log("==============" + serverUrl + webresourceURL);
    document.getElementById(linkID).href = serverUrl + webresourceURL;

}

