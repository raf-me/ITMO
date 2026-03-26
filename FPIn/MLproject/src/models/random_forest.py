from typing import Optional
import numpy as np
from sklearn.ensemble import RandomForestClassifier
from src.domain.classifier import Classifier


class RandomForestTextClassifier(Classifier):
    def __init__(self, n_estimators: int, max_depth: Optional[int], seed: int):
        self._model = RandomForestClassifier(
            n_estimators=int(n_estimators),
            max_depth=None if max_depth is None else int(max_depth),
            random_state=int(seed),
            n_jobs=-1,
        )

    def fit(self, X, y) -> None:
        self._model.fit(X, y)

    def predict(self, X):
        return self._model.predict(X)

    @property
    def supported_input_kinds(self) -> set[str]:
        return {"dense"}

    @property
    def name(self) -> str:
        return "random_forest"