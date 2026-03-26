from sklearn.linear_model import LogisticRegression
from src.domain.classifier import Classifier


class LogisticClassifier(Classifier):
    def __init__(self, max_iter: int, C: float, seed: int):
        self._model = LogisticRegression(
            max_iter = int(max_iter),
            C = float(C),
            random_state = int(seed),
            n_jobs = None,
        )

    def fit(self, X, y) -> None:
        self._model.fit(X, y)

    def predict(self, X):
        return self._model.predict(X)

    @property
    def supported_input_kinds(self) -> set[str]:
        return {"sparse", "dense"}

    @property
    def name(self) -> str:
        return "logistic"