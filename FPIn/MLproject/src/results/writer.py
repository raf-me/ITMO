from typing import List, Dict, Any
import pandas as pd


class ResultsWriter:
    def __init__(self, path: str):
        self._path = path

    def write(self, rows: List[Dict[str, Any]]) -> None:
        df = pd.DataFrame(rows)
        df.to_csv(self._path, index=False)