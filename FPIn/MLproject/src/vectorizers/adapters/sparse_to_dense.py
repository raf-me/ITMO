import numpy as np
from scipy.sparse import issparse


class SparseToDenseAdapter:
    def transform(self, X):
        if issparse(X):
            return X.toarray()
        return np.asarray(X)