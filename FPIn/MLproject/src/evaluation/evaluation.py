from dataclasses import dataclass
from sklearn.metrics import accuracy_score, f1_score


@dataclass(frozen=True)
class Metrics:
    accuracy: float
    f1_weighted: float


class Evaluator:
    def evaluate(self, y_true, y_pred) -> Metrics:
        return Metrics(
            accuracy=float(accuracy_score(y_true, y_pred)),
            f1_weighted=float(f1_score(y_true, y_pred, average="weighted")),
        )