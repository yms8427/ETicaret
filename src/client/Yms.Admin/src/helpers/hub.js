import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";

const connection = new HubConnectionBuilder()
    .withUrl("https://localhost:7001/nh")
    .withAutomaticReconnect()
    .configureLogging(LogLevel.Information)
    .build();
connection.start();

export default connection;