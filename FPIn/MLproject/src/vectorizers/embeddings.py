from typing import List
import numpy as np
from sentence_transformers import SentenceTransformer
from src.domain.vectorizer import Vectorizer


class EmbeddingVectorizer(Vectorizer):
    def __init__(self, model_name: str, batch_size: int, normalize: bool):
        self._model = SentenceTransformer(model_name)
        self._batch_size = int(batch_size)
        self._normalize = bool(normalize)

    def fit(self, texts: List[str]) -> None:
        return None

    def transform(self, texts: List[str]):
        emb = self._model.encode(
            texts,
            batch_size=self._batch_size,
            show_progress_bar=True,
            convert_to_numpy=True,
            normalize_embeddings=self._normalize,
        )

        return np.asarray(emb, dtype=np.float32)

    @property
    def output_kind(self) -> str:
        return "dense"