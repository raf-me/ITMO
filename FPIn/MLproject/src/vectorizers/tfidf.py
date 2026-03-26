from typing import List, Tuple
from sklearn.feature_extraction.text import TfidfVectorizer
from src.domain.vectorizer import Vectorizer


class TfidfTextVectorizer(Vectorizer):
    def __init__(self, max_features: int, ngram_range: Tuple[int, int]):
        self._vectorizer = TfidfVectorizer(
            max_features=max_features,
            ngram_range=ngram_range,
        )

    def fit(self, texts: List[str]) -> None:
        self._vectorizer.fit(texts)

    def transform(self, texts: List[str]):
        return self._vectorizer.transform(texts)

    @property
    def output_kind(self) -> str:
        return "sparse"