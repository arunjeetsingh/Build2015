// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF 
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO 
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A 
// PARTICULAR PURPOSE. 
// 
// Copyright (c) Microsoft Corporation. All rights reserved 

// 
// A JavaScript background task runs a specified JavaScript file. 
// 
(function () {
    "use strict";

    // 
    // The background task instance's activation parameters are available via Windows.UI.WebUI.WebUIBackgroundTaskInstance.current 
    // 
    var cancel = false,
        progress = 0,
        backgroundTaskInstance = Windows.UI.WebUI.WebUIBackgroundTaskInstance.current,
        cancelReason = "";

    var serviceConnection = null;

    console.log("Background " + backgroundTaskInstance.task.name + " Starting...");

    // 
    // Associate a cancellation handler with the background task. 
    // 
    function onCanceled(cancelEventArg) {
        cancel = true;
        cancelReason = cancelEventArg;
    }
    backgroundTaskInstance.addEventListener("canceled", onCanceled);

    if (!cancel) {
        doWork(backgroundTaskInstance);
    }
    else {
        close();
    }

    function doWork(instance) {
        serviceConnection = instance.triggerDetails.appServiceConnection;
        serviceConnection.addEventListener("requestreceived", requestReceived);
    }

    function requestReceived(args) {
        var incomingArgs = args.detail[0];

        var deferral = incomingArgs.getDeferral();

        //Create the response
        var result = new Windows.Foundation.Collections.ValueSet();
        result.insert("Result", "[Out of Proc]" + Math.random());

        //Send the response
        incomingArgs.request.sendResponseAsync(result).then(function (response) {
            deferral.complete();
        });        
    }
})();