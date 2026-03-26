from abc import ABC, abstractmethod
from typing import Any, Iterable


class Classifier(ABC):
    @abstractmethod
    def fit(self, X: Any, y: Iterable[int]) -> None:
        raise NotImplementedError

    @abstractmethod
    def predict(self, X: Any):
        raise NotImplementedError

    @property
    @abstractmethod
    def supported_input_kinds(self) -> set[str]:
        raise NotImplementedError

    @property
    @abstractmethod
    def name(self) -> str:
        raise NotImplementedError