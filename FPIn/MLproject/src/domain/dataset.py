from dataclasses import dataclass
from typing import List


@dataclass(frozen=True)
class TextDataset:
    texts: List[str]
    labels: List[int]