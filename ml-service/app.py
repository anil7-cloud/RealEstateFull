from flask import Flask, request

app = Flask(__name__)

@app.route("/predict", methods=["POST"])
def predict():
    data = request.json
    name = data["name"]

    score = len(name) * 10 + 5

    return {
        "score": score,
        "status": "hot" if score > 80 else "warm"
    }

if __name__ == "__main__":
    app.run(port=5001)
