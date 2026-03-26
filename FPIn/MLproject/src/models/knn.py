from sklearn.neighbors import KNeighborsClassifier
from src.domain.classifier import Classifier


class KNNClassifier(Classifier):
    def __init__(self, n_neighbors: int):
        self._model = KNeighborsClassifier(n_neighbors=int(n_neighbors))

    def fit(self, X, y) -> None:
        self._model.fit(X, y)

    def predict(self, X):
        return self._model.predict(X)

    @property
    def supported_input_kinds(self) -> set[str]:
        return {"sparse", "dense"}

    @property
    def name(self) -> str:
        return "knn"