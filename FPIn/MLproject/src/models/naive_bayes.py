from sklearn.naive_bayes import MultinomialNB
from src.domain.classifier import Classifier


class NaiveBayesClassifier(Classifier):
    def __init__(self):
        self._model = MultinomialNB()

    def fit(self, X, y) -> None:
        self._model.fit(X, y)

    def predict(self, X):
        return self._model.predict(X)

    @property
    def supported_input_kinds(self) -> set[str]:
        return {"sparse"}

    @property
    def name(self) -> str:
        return "naive_bayes"