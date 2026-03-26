from abc import ABC, abstractmethod
from typing import List, Any


class Vectorizer(ABC):
    @abstractmethod
    def fit(self, texts: List[str]) -> None:
        raise NotImplementedError

    @abstractmethod
    def transform(self, texts: List[str]) -> Any:
        raise NotImplementedError

    def fit_transform(self, texts: List[str]) -> Any:
        self.fit(texts)
        return self.transform(texts)

    @property
    @abstractmethod
    def output_kind(self) -> str:
        raise NotImplementedError