from dataclasses import dataclass
from typing import Optional, Tuple, List
from datasets import load_dataset
from src.domain.dataset import TextDataset


@dataclass(frozen=True)
class DatasetConfig:
    provider: str
    name: str
    text_field: str
    label_field: str
    max_train_samples: Optional[int]
    max_test_samples: Optional[int]
    seed: int


class DatasetLoader:
    def __init__(self, cfg: DatasetConfig):
        self._cfg = cfg

    def load(self) -> Tuple[TextDataset, TextDataset]:
        if self._cfg.provider != "huggingface":
            raise ValueError(f"Unsupported provider: {self._cfg.provider}")

        ds = load_dataset(self._cfg.name)

        train_texts, train_labels = self._extract(ds["train"], self._cfg.max_train_samples)
        test_texts, test_labels = self._extract(ds["test"], self._cfg.max_test_samples)

        if len(train_texts) < 10_000:
            raise ValueError(f"Train size must be >= 10000, got {len(train_texts)}")

        return (
            TextDataset(texts=train_texts, labels=train_labels),
            TextDataset(texts=test_texts, labels=test_labels),
        )

    def _extract(self, split, limit: Optional[int]) -> Tuple[List[str], List[int]]:
        texts = split[self._cfg.text_field]
        labels = split[self._cfg.label_field]

        if limit is not None:
            limit = int(limit)
            texts = texts[:limit]
            labels = labels[:limit]

        return list(texts), list(labels)