from typing import Any, Dict

from src.domain.classifier import Classifier
from src.models.naive_bayes import NaiveBayesClassifier
from src.models.logistic import LogisticClassifier
from src.models.knn import KNNClassifier
from src.models.random_forest import RandomForestTextClassifier


class ModelFactory:
    @staticmethod
    def build(cfg: Dict[str, Any], seed: int) -> Classifier:
        m_type = cfg["type"]
        params = cfg.get("params", {})

        if m_type == "naive_bayes":
            return NaiveBayesClassifier()

        if m_type == "logistic":
            return LogisticClassifier(
                max_iter=int(params.get("max_iter", 2000)),
                C=float(params.get("C", 1.0)),
                seed=int(seed),
            )

        if m_type == "knn":
            return KNNClassifier(
                n_neighbors=int(params.get("n_neighbors", 5))
            )

        if m_type == "random_forest":
            return RandomForestTextClassifier(
                n_estimators=int(params.get("n_estimators", 200)),
                max_depth=params.get("max_depth", None),
                seed=int(seed),
            )

        raise ValueError(f"Unknown model type: {m_type}")