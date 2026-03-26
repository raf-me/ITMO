from typing import Any, Dict, Tuple

from src.domain.vectorizer import Vectorizer
from src.vectorizers.count import CountTextVectorizer
from src.vectorizers.tfidf import TfidfTextVectorizer
from src.vectorizers.embeddings import EmbeddingVectorizer


class VectorizerFactory:
    @staticmethod
    def build(cfg: Dict[str, Any]) -> Vectorizer:
        v_type = cfg["type"]
        params = cfg.get("params", {})

        if v_type == "count":
            return CountTextVectorizer(
                max_features=int(params["max_features"]),
                ngram_range=tuple(params.get("ngram_range", [1, 1])),
            )

        if v_type == "tfidf":
            return TfidfTextVectorizer(
                max_features=int(params["max_features"]),
                ngram_range=tuple(params.get("ngram_range", [1, 1])),
            )

        if v_type == "embeddings":
            return EmbeddingVectorizer(
                model_name=str(params["model_name"]),
                batch_size=int(params.get("batch_size", 64)),
                normalize=bool(params.get("normalize", True)),
            )

        raise ValueError(f"Unknown vectorizer type: {v_type}")
