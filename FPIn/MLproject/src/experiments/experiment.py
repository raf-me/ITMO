from dataclasses import dataclass
from typing import Any
import time

from src.domain.dataset import TextDataset
from src.domain.vectorizer import Vectorizer
from src.domain.classifier import Classifier
from src.evaluation.evaluator import Evaluator


@dataclass(frozen=True)
class ExperimentResult:
    accuracy: float
    f1_weighted: float
    vectorization_time_sec: float
    train_time_sec: float
    inference_time_sec: float


class Experiment:
    def __init__(self, evaluator: Evaluator):
        self._evaluator = evaluator

    def run(
        self,
        vectorizer: Vectorizer,
        classifier: Classifier,
        train: TextDataset,
        test: TextDataset,
        seed: int,
    ) -> dict:
        self._validate_compatibility(vectorizer, classifier)

        t0 = time.time()
        X_train = vectorizer.fit_transform(train.texts)
        X_test = vectorizer.transform(test.texts)
        vec_time = time.time() - t0

        t1 = time.time()
        classifier.fit(X_train, train.labels)
        train_time = time.time() - t1

        t2 = time.time()
        preds = classifier.predict(X_test)
        inf_time = time.time() - t2

        metrics = self._evaluator.evaluate(test.labels, preds)

        res = ExperimentResult(
            accuracy=metrics.accuracy,
            f1_weighted=metrics.f1_weighted,
            vectorization_time_sec=vec_time,
            train_time_sec=train_time,
            inference_time_sec=inf_time,
        )
        return {
            "accuracy": res.accuracy,
            "f1_weighted": res.f1_weighted,
            "vectorization_time_sec": res.vectorization_time_sec,
            "train_time_sec": res.train_time_sec,
            "inference_time_sec": res.inference_time_sec,
        }

    @staticmethod
    def _validate_compatibility(vectorizer: Vectorizer, classifier: Classifier) -> None:
        if vectorizer.output_kind not in classifier.supported_input_kinds:
            raise ValueError(
                f"Incompatible: vectorizer output '{vectorizer.output_kind}' "
                f"not supported by model '{classifier.name}' "
                f"(supports {classifier.supported_input_kinds})"
            )