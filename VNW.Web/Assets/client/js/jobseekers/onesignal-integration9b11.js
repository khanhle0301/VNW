// Google Analytic customEvent function
// function customEvent(uniqueName, action, label, value)

var OneSignal = window.OneSignal || [];

function initOneSignal(appId, safariWebId) {
    // Initialize OneSignal service
    OneSignal.push(
        [
            "init",
            {
                appId: appId,
                safari_web_id: safariWebId,
                autoRegister: true,
                notifyButton: {
                    enable: true,
                    position: 'bottom-left'
                },
                persistNotification: false,
                welcomeNotification: {
                    "title": "VietnamWorks notifications",
                    "message": "Don’t miss your dream job"
                }
            }
        ]
    );

    // Keep track Browser Player Id on our database
    OneSignal.push(function() {
        OneSignal.getUserId().then(function(playerId) {
            if (playerId != undefined && playerId != null) {
                jQuery.ajax({
                  type: "POST",
                  url: '/jobseekers/ajax.php?action=update-browser-id',
                  data: {oneSignalPlayerId: playerId},
                  success: function() {

                  }
                });
            }
        });
    });

    // Tracking Native Permission Requests with Google Analytics
    OneSignal.push(function() {
        OneSignal.on('notificationPermissionChange', function(permissionChange) {
            var currentPermission = permissionChange.to;

            if (typeof customEvent == 'function') {
                switch (currentPermission) {
                    case 'granted':
                        customEvent('BrowserNotifications', 'Allow', 'Permission');
                        break;
                    case 'denied':
                        customEvent('BrowserNotifications', 'Block', 'Permission');
                        break;
                    case 'default':
                        customEvent('BrowserNotifications', 'Dismiss', 'Permission');
                        break;
                }
            } else {
                console.log('Missing Google Analytics customEvent function!');
            }
        });
    });

    // Tracking Permission Messages of Subscription Bell with Google Analytics
    OneSignal.push(function() {
        OneSignal.on('subscriptionChange', function (isSubscribed) {
            if (isSubscribed) {
                customEvent('BrowserNotifications', 'Allow', 'Permission');
            } else {
                customEvent('BrowserNotifications', 'Block', 'Permission');
            }
        });
    });
}
