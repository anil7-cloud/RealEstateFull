const connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .build();

connection.on("ReceiveMessage", (user, message) => {
    const div = document.getElementById("messages");

    if (!div) return;

    const p = document.createElement("p");
    p.innerText = `${user}: ${message}`;
    div.appendChild(p);
});

connection.start();

function sendMessage() {
    const user = document.getElementById("user").value;
    const message = document.getElementById("msg").value;

    connection.invoke("SendMessage", user, message);
}
