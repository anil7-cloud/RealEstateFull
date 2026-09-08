const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

connection.on("ReceiveMessage", function (senderId, message) {
    console.log("Mesaj:", senderId, message);
});

connection.start();
