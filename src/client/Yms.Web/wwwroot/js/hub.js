var connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7001/nh")
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