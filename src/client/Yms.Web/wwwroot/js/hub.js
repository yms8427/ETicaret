var connection = new signalR.HubConnectionBuilder()
    .withUrl(yms_hub_url)
    .withAutomaticReconnect()
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.start().then(function () {
    setup();
});

function setup() {
    connection.on("someOneIntroducedItself", function (message) {
        console.log(message);
    });
}